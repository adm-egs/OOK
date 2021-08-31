
Imports Newtonsoft.Json.Linq

Public Class cMedewerker
    Inherits cPersoon
    Private _bekendInOO As Boolean = False

    Public Property BekendInOO As Boolean
        Get
            Return _bekendInOO
        End Get
        Set(value As Boolean)
            _bekendInOO = value
        End Set
    End Property

    Private Function CreateMedewerker() As Boolean
        Try

            i.GetToken()    'token opvragen        '
            Dim client As RestSharp.RestClient '  New RestSharp.RestClient(dURLS("StudentGet"))
            Dim request As New RestSharp.RestRequest

            'medewerker aanmaken
            request.Method = RestSharp.Method.POST
            client = New RestSharp.RestClient(dURLS("StudentGet"))

            request.AddHeader("Authorization", "Bearer " & i.AuthenticationToken)
            request.AddParameter("username", Username)
            request.AddParameter("firstname", Firstname)
            request.AddParameter("lastname", Me.Lastname)
            request.AddParameter("lastname_prefix", Me.Lastname_prefix)
            request.AddParameter("email", Code & "@mboutrecht.nl")
            request.AddParameter("code", MyBase.Code)
            request.AddParameter("foreign_id", Code)
            client.Timeout = -1

            Dim response As RestSharp.RestResponse = client.Execute(request)

            Dim jsonLog As New cJsonLogItem(request.Method.ToString, client.BaseUrl.ToString, "Aanmaken medewerker in OO", Code, response.Content, response.StatusCode.ToString)
            jsonLog.Write2database()


            If response.StatusCode <> Net.HttpStatusCode.OK Then
                l.LOGTHIS("Request failed : " & response.StatusCode)
                ' l.LOGTHIS("OOid=" & Me.OOid)
                Return False
            End If

            Dim json As JObject = JObject.Parse(response.Content)
            ' Dim dataUser As JArray = json.SelectToken("error")
            'controleren of dit gelukt is
        Catch ex As Exception
            l.LOGTHIS("Fout bij create medewerker user: " & ex.Message)
            Return False
        End Try
        Return True
    End Function
    Public Function GetMedewerkerClassUitOo() As cMedewerker
        'gegevens student uit OO opvragen
        Dim request As New RestSharp.RestRequest()
        Dim client As New RestSharp.RestClient(dURLS("StudentGet") & "?username=" & Me.Username)
        Dim response As RestSharp.RestResponse
        Try
            i.GetToken()    'token opvragen
            client.Timeout = -1
            request.Method = RestSharp.Method.GET

            request.AddHeader("Authorization", "Bearer " & i.AuthenticationToken)
            response = client.Execute(request)

            Dim json As JObject = JObject.Parse(response.Content)
            Dim dataPage As JObject = json.SelectToken("data")
            Dim dataUser As JArray = dataPage.SelectToken("data")
            Dim UserCountInCurrentPage As Integer = dataUser.Count

            Dim jsonLog As New cJsonLogItem(request.Method.ToString, client.BaseUrl.ToString, "Opvragen medewerker uit OO", Me.Username, response.Content, response.StatusCode.ToString)
            jsonLog.Write2database()

            If dataUser.Count = 0 Then
                BekendInOO = False
                Return Nothing
            Else

                BekendInOO = True
            End If

            For Each jUser In dataUser
                'json object naar user overzetten
                getUserData(jUser)
                'Dim s As New cStudent
                's.getUserData(jUser)    'velden invullen in object
                'If Add2Personen = True Then
                '    If dPersonen.ContainsKey(StudentNummer) Then
                '        dPersonen.Remove(StudentNummer)
                '    End If
                '    dPersonen.Add(StudentNummer, Me)
                'Else
                '    Return Me
                'End If

            Next
            Return Me
        Catch ex As Exception
            Dim jsonLog As New cJsonLogItem(request.Method.ToString, client.BaseUrl.ToString, "Opvragen medewerker uit OO", Me.Username, response.Content, response.StatusCode.ToString)
            jsonLog.Gelukt = "N"
            jsonLog.Write2database()


            Return Nothing
        End Try
    End Function

End Class
