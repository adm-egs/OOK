Public Class cOpleidingsDeelname

    Private _ople_id As Long ' op.ople_id as id_opleiding,
    Private _startDatum As Date 'ook.ingangsdatum as Startdatum,         
    Private _eindDatum As Date ' ook.afloopdatum_verwacht as Einddatum,  
    Private _eindDatumWerkelijk As Date 'ook.afloopdatum_werkelijk as Einddatum_werkelijk,
    Private _teamcode As String 'org.organisatieonderdeel as TeamCodes,
    'org.omschrijving as TeamNamen,
    Private _creboNr As String 'ook.opleiding as Opleidingscode,
    '  Private 'op.naam_nl as Opleidingsnaam,
    Private _cohort As Integer 'ook.cohort as Cohort,

    '  op.leerweg as leerweg,
    ' op.niveau as niveau,
    Private _StudentOokId As Long '    ook.sook_id as ook_id


    Public Property StartDatum As Date
        Get
            Return _startDatum
        End Get
        Set(value As Date)
            _startDatum = value
        End Set
    End Property

    Public Property EindDatum As Date
        Get

            Return _eindDatum
        End Get
        Set(value As Date)
            _eindDatum = value
        End Set
    End Property

    Public Property EindDatumWerkelijk As Date
        Get
            Return _eindDatumWerkelijk
        End Get
        Set(value As Date)
            _eindDatumWerkelijk = value
        End Set
    End Property

    Public Property Teamcode As String
        Get
            Return _teamcode
        End Get
        Set(value As String)
            _teamcode = value
        End Set
    End Property

    Public Property CreboNr As String
        Get
            Return _creboNr
        End Get
        Set(value As String)
            _creboNr = value
        End Set
    End Property

    Public Property Cohort As Integer
        Get
            Return _cohort
        End Get
        Set(value As Integer)
            _cohort = value
        End Set
    End Property

    Public Property Ople_id As Long
        Get
            Return _ople_id
        End Get
        Set(value As Long)
            _ople_id = value
        End Set
    End Property

    Public Property StudentOokId As Long
        Get
            Return _StudentOokId
        End Get
        Set(value As Long)
            _StudentOokId = value
        End Set
    End Property
End Class
