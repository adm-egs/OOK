
Imports Newtonsoft.Json.Linq

Public Class cMedewerker
    Inherits cPersoon
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


End Class
