Module modAlgemeen
    Public l As cLogging
    Public ini As cIniFile
    Public i As cInstellingen
    Public dURLS As New Dictionary(Of String, String)
    Public dPersonen As New Dictionary(Of Long, cPersoon)
    Public dbOsiris As cDbUtils
    Public dbMiddleWare As cDbUtils
    Public dictOsirisStudentenKeyStudentNr As New Dictionary(Of String, cStudentBasis)
    Public dAlleGroepen As New Dictionary(Of String, cGroep)
    Public dAlleOpleidingen As New Dictionary(Of Long, cOpleiding)
    Public dAlleTeams As New Dictionary(Of String, cTeam)


    'oude code:
    ' l.LOGTHIS("Aan te maken groep : " & sGroepscode)
    'voorbeeld request : (put) https://mboutrechttest.onderwijsonline.nl/api/v1/team?name=ENG-TEC4B&team_type_id=1

    'i.GetToken()    'token opvragen        '
    'Dim client As RestSharp.RestClient
    'Dim request As New RestSharp.RestRequest

    'request.Method = RestSharp.Method.POST
    'client = New RestSharp.RestClient(dURLS("TeamGet") & "?name=" & sGroepscode & "&team_type_id=1")
    'request.AddHeader("Authorization", "Bearer " & i.AuthenticationToken)
    'client.Timeout = -1

    'Dim response As RestSharp.RestResponse = client.Execute(request)
    'If response.StatusCode <> Net.HttpStatusCode.OK Then
    '    Return False
    'End If

    'Dim jsonLog As New cJsonLogItem(request.Method.ToString, client.BaseUrl.ToString, "Groep aanmaken in OO " & sGroepscode, StudentNummer, response.Content, response.StatusCode.ToString)
    'jsonLog.Write2database()

    'Dim json As JObject = JObject.Parse(response.Content)

End Module
