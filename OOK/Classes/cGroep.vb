Public Class cGroep
    Private _Naam As String
    Private _Code As String
    Private _OOid As Long = -1
    Private _TeamTypeId As Long = -1

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

    Public Property OOid As Long
        Get
            Return _OOid
        End Get
        Set(value As Long)
            _OOid = value
        End Set
    End Property

    Public Property TeamTypeId As Long
        Get
            Return _TeamTypeId
        End Get
        Set(value As Long)
            _TeamTypeId = value
        End Set
    End Property
End Class
