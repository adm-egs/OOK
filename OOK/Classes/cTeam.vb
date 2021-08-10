Imports Newtonsoft.Json.Linq

Public Class cTeam
    Private _code As String = ""
    Private _naam As String = ""
    Private _id As Long = 0
    Private _kostenplaats As String = ""
    Private _ooID As Long = -1
    Private _ingangsDatum As Date
    Private _afloopdatum As Date

    Public Property Code As String
        Get
            Return _code
        End Get
        Set(value As String)
            _code = value
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

    Public Property Id As Long
        Get
            Return _id
        End Get
        Set(value As Long)
            _id = value
        End Set
    End Property

    Public Property Kostenplaats As String
        Get
            Return _kostenplaats
        End Get
        Set(value As String)
            _kostenplaats = value
        End Set
    End Property

    Public Property OoID As Long
        Get
            If _ooID = -1 Then
                If Me.VraagOOidOp = False Then  'opvragen uit OO indien onbekend

                    Return -1
                Else
                    Return _ooID
                End If
            Else
                Return _ooID
            End If
        End Get

        Set(value As Long)
            _ooID = value
        End Set
    End Property

    Public Property IngangsDatum As Date
        Get
            Return _ingangsDatum
        End Get
        Set(value As Date)
            _ingangsDatum = value
        End Set
    End Property

    Public Property Afloopdatum As Date
        Get
            Return _afloopdatum
        End Get
        Set(value As Date)
            _afloopdatum = value
        End Set
    End Property

    Private Function VraagOOidOp() As Boolean
        ' https://mboutrechttest.onderwijsonline.nl/api/v1/team?name=.Dans Academie&team_type_id=2
        Dim sRequest As String = dURLS("TeamGet") & "?name=" & Me.Naam & "&team_type_id=2"
        Dim sJsonResult As String = i.OO_JSON_REQUEST(sRequest, "Check of team bestaat :" & Naam, -1, RestSharp.Method.GET)
        Dim json As JObject = JObject.Parse(sJsonResult)
        Dim ResponseError As JValue = json.SelectToken("error")
        If CBool(ResponseError.Value) = False Then
            l.LOGTHIS("Opleiding uit OO opgevraagd")
        Else
            l.LOGTHIS("Opleiding opvragen niet gelukt ")
            Return False
        End If
        'opvragen uit JSON response wat het aantal is
        '=1 => id opvragen en retour
        '=0 => team is onbekend, deze proberen aan te maken
        '

    End Function
End Class
