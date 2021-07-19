Public Class cOpleiding

    Private _code As String
    Private _naam As String
    Private _startdatum As Date
    Private _einddatum As Date
    Private _einddatum_werkelijk As Date
    Private _teamCode As String
    Private _teamNaam As String
    Private _cohort As Integer
    Private _leerweg As String = ""
    Private _niveau As Integer = -1
    Private _ople_id As Long = -1    '-1 -> not set

    Public Property Code As String
        Get
            Return _code
        End Get
        Set(value As String)
            _code = value
        End Set
    End Property


    Public Property cohort As Integer
        Get
            Return _cohort
        End Get
        Set(value As Integer)
            _cohort = value
        End Set
    End Property

    Public Property Naam As String
        Get
            Return _naam
        End Get
        Set(value As String)
            _naam = value
        End Set
    End Property

    Public Property Startdatum As Date
        Get
            Return _startdatum
        End Get
        Set(value As Date)
            _startdatum = value
        End Set
    End Property

    Public Property Einddatum As Date
        Get
            Return _einddatum
        End Get
        Set(value As Date)
            _einddatum = value
        End Set
    End Property

    Public Property Einddatum_werkelijk As Date
        Get
            If _einddatum_werkelijk < New Date(2000, 1, 1) Then
                Return Einddatum
            Else
                Return _einddatum_werkelijk
            End If

        End Get
        Set(value As Date)
            _einddatum_werkelijk = value
        End Set
    End Property



    Public Property TeamCode As String
        Get
            Return _teamCode
        End Get
        Set(value As String)
            _teamCode = value
        End Set
    End Property

    Public Property TeamNaam As String
        Get
            Return _teamNaam
        End Get
        Set(value As String)
            _teamNaam = value
        End Set
    End Property

    Public Property Ople_Id As Long
        Get
            Return _ople_id
        End Get
        Set(value As Long)
            _ople_id = value
        End Set
    End Property

    Public Property Leerweg As String
        Get
            Return _leerweg
        End Get
        Set(value As String)
            _leerweg = value
        End Set
    End Property

    Public Property Niveau As Integer
        Get
            Return _niveau
        End Get
        Set(value As Integer)
            _niveau = value
        End Set
    End Property

    Public Function BestaatInOO() As Boolean
        'opleidingen worden gecontroleerd op basis van het veld foreign_id, deze bevat het ople_id van de opleiding
    End Function
End Class
