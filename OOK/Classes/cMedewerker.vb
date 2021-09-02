
Imports Newtonsoft.Json.Linq

Public Class cMedewerker
    Inherits cPersoon
    Private _bekendInOO As Boolean = False
    Private _OnderwijsOnlineId As Long
    Public Property BekendInOO As Boolean
        Get
            Return _bekendInOO
        End Get
        Set(value As Boolean)
            _bekendInOO = value
        End Set
    End Property

    Public Property OnderwijsOnlineId As Long
        Get
            Return _OnderwijsOnlineId
        End Get
        Set(value As Long)
            _OnderwijsOnlineId = value
        End Set
    End Property
End Class
