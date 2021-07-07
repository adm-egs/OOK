Public Class cGroep
    Private _Naam As String
    Private _Code As String

    Public Property Naam As String
        Get
            Return _Naam
        End Get
        Set(value As String)
            _Naam = value
        End Set
    End Property

    Public Property Code As String
        Get
            Return _Code
        End Get
        Set(value As String)
            _Code = value
        End Set
    End Property
End Class
