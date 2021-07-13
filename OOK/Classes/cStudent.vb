﻿Imports Newtonsoft.Json.Linq

Public Class cStudent
#Region "properties"


    Inherits cPersoon
    Private _WerkMailAdres As String
    Private _opleidingen As New Dictionary(Of String, cOpleiding)
    Private _GroepsDeelnames As New Dictionary(Of String, cGroepsDeelname)
    Private _OOid As Long = -1
    Private _bekendInOO As Boolean = False

    Public Property WerkMailAdres As String
        Get
            If _WerkMailAdres = "" Then
                If StudentNummer <> -1 Then
                    _WerkMailAdres = StudentNummer & "@student.mboutrecht.nl"
                End If
            End If
            Return _WerkMailAdres
        End Get
        Set(value As String)
            _WerkMailAdres = value
        End Set
    End Property

    Public Property Opleidingen As Dictionary(Of String, cOpleiding)
        Get
            Return _opleidingen
        End Get
        Set(value As Dictionary(Of String, cOpleiding))
            _opleidingen = value
        End Set
    End Property

    Public Property GroepsDeelnames As Dictionary(Of String, cGroepsDeelname)
        Get
            Return _GroepsDeelnames
        End Get
        Set(value As Dictionary(Of String, cGroepsDeelname))
            _GroepsDeelnames = value
        End Set
    End Property



    Public Property BekendInOO As Boolean
        Get
            Return _bekendInOO
        End Get
        Set(value As Boolean)
            _bekendInOO = value
        End Set
    End Property

    Public Property StudentNummer As Long
        Get
            Return Username
        End Get
        Set(value As Long)
            Username = value
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

    Public Sub AddTeam(sTeamcode As String, sTeamNaam As String)
        If Not MyBase.Teams.ContainsKey(sTeamcode) Then
            Teams.Add(sTeamcode, sTeamNaam)
        End If
    End Sub
#End Region
    Public Overrides Function getUserData(jUser As JObject) As Boolean
        Try

            'functie leest het JuserObject uit naar de verschillende velden
            Me.OOid = MyBase.SafeGetJsonData("id", jUser)
            Me.Username = SafeGetJsonData("username", jUser)
            Me.StudentNummer = Me.Username
            Me.Firstname = SafeGetJsonData("firstname", jUser)
            Me.Lastname = SafeGetJsonData("lastname", jUser)
            Me.Email = SafeGetJsonData("email", jUser)
            Me.Created_at = SafeGetJsonData("created_at", jUser)
            Me.Updated_at = SafeGetJsonData("updated_at", jUser)
            Me.Foreign_id = SafeGetJsonData("foreign_id", jUser)
            Me.Deleted_at = SafeGetJsonData("deleted_at", jUser)
            Me.Organisation = SafeGetJsonData("organisation", jUser)
            Me.Function = SafeGetJsonData("function", jUser)
            Me.Phone = SafeGetJsonData("phone", jUser)
            Me.Mobile = SafeGetJsonData("mobile", jUser)
            Me.Date_of_birth = SafeGetJsonData("date_of_birth", jUser)
            Me.Place_of_birth = SafeGetJsonData("place_of_birth", jUser)
            Me.Lastname_prefix = SafeGetJsonData("lastname_prefix", jUser)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function Send2OO() As Boolean
        'stap 1 controleren of het OO id bekend is
        If OOid = -1 Then
            'niet bekend -> opvragen uit Onderwijs Online
            If GetStudentUitOoOpBasisVanStudentNummer() = False Then
                If BekendInOO = False Then
                    'toevoegen aan OO
                    ChangeUserInOO(True)    'create user
                Else
                    ChangeUserInOO(False)   'update user

                End If
            Else
                ChangeUserInOO(False)   'update user
            End If
        Else
            'bekend in OO -> updaten
        End If

        Return True
    End Function


    Public Function GetStudentUitOoOpBasisVanStudentNummer() As Boolean
        'gegevens student uit OO opvragen
        Dim request As New RestSharp.RestRequest()
        Dim client As New RestSharp.RestClient(dURLS("StudentGet") & "?username=" & Me.StudentNummer)
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

            Dim jsonLog As New cJsonLogItem(request.Method.ToString, client.BaseUrl.ToString, "Opvragen student uit OO", StudentNummer, response.Content, response.StatusCode.ToString)
            jsonLog.Write2database()

            If dataUser.Count = 0 Then
                BekendInOO = False
                Return False
            Else
                BekendInOO = True
            End If

            For Each jUser In dataUser
                'json object naar user overzetten
                getUserData(jUser)
                'Dim s As New cStudent
                's.getUserData(jUser)    'velden invullen in object
                If dPersonen.ContainsKey(StudentNummer) Then
                    dPersonen.Remove(StudentNummer)
                End If
                dPersonen.Add(StudentNummer, Me)
            Next


            Return True
        Catch ex As Exception
            Dim jsonLog As New cJsonLogItem(request.Method.ToString, client.BaseUrl.ToString, "Opvragen student uit OO", StudentNummer, response.Content, response.StatusCode.ToString)
            jsonLog.Gelukt = "N"
            jsonLog.Write2database()


            Return False
        End Try
    End Function

    Public Function ChangeUserInOO(Optional bolCreate As Boolean = False) As Boolean
        'functie maakt de user aan in OO of update
        If BekendInOO = True Then
            If CreateOrUpdateUser(False) = False Then
                l.LOGTHIS("Update student niet gelukt")
                Return False
            End If
        Else
            If CreateOrUpdateUser(True) = False Then
                l.LOGTHIS("Create student niet gelukt")
                Return False
            End If
        End If

        If OOid = -1 Then
            GetStudentUitOoOpBasisVanStudentNummer()
        End If
        'groep student toevoegen
        If VoegPermissieGroepStudentToe() = False Then
            l.LOGTHIS("Fout bij toevoegen permissiegroep student")
            Return False
        End If


        'overige klassen toevoegen
        If VoegKlassenToe() = False Then
            l.LOGTHIS("Fout bij toevoegen klassen bij student in OO")
            Return False
        End If

        'opleidingen toevoegen

        'Organisatorische eenheden toevoegen


    End Function
    Private Function CreateOrUpdateUser(Optional bolCreate As Boolean = False) As Boolean

        i.GetToken()    'token opvragen        '

        Dim client As RestSharp.RestClient '  New RestSharp.RestClient(dURLS("StudentGet"))
        Dim request As New RestSharp.RestRequest

        If bolCreate = True Then
            'persoon aanmaken
            request.Method = RestSharp.Method.POST
            client = New RestSharp.RestClient(dURLS("StudentGet"))
        Else
            'persoon updaten
            If OOid = -1 Then Stop
            request.Method = RestSharp.Method.PUT
            client = New RestSharp.RestClient(dURLS("StudentGet") & "/" & OOid)
        End If

        request.AddHeader("Authorization", "Bearer " & i.AuthenticationToken)
        request.AddParameter("username", Me.Username)
        request.AddParameter("firstname", Me.Firstname)
        request.AddParameter("lastname", Me.Lastname)
        request.AddParameter("lastname_prefix", Me.Lastname_prefix)
        request.AddParameter("email", Me.WerkMailAdres)
        request.AddParameter("code", Me.StudentNummer)
        client.Timeout = -1

        Dim response As RestSharp.RestResponse = client.Execute(request)
        If bolCreate = True Then
            Dim jsonLog As New cJsonLogItem(request.Method.ToString, client.BaseUrl.ToString, "Aanmaken student in OO", StudentNummer, response.Content, response.StatusCode.ToString)
            jsonLog.Write2database()
        Else
            Dim jsonLog As New cJsonLogItem(request.Method.ToString, client.BaseUrl.ToString, "Updaten student in OO", StudentNummer, response.Content, response.StatusCode.ToString)
            jsonLog.Write2database()
        End If

        If response.StatusCode <> Net.HttpStatusCode.OK Then
            l.LOGTHIS("Request failed : " & response.StatusCode)
            l.LOGTHIS("OOid=" & Me.OOid)
            Return False
        End If

        Dim json As JObject = JObject.Parse(response.Content)
        ' Dim dataUser As JArray = json.SelectToken("error")
        'controleren of dit gelukt is
        Return True
    End Function

    Private Function VoegPermissieGroepStudentToe() As Boolean

        i.GetToken()    'token opvragen        '

        Dim client As RestSharp.RestClient
        Dim request As New RestSharp.RestRequest
        If OOid = -1 Then Stop

        request.Method = RestSharp.Method.POST
        client = New RestSharp.RestClient(dURLS("StudentGet") & "/" & OOid & "/groups/attach?group_ids[]=1")
        request.AddHeader("Authorization", "Bearer " & i.AuthenticationToken)
        client.Timeout = -1

        Dim response As RestSharp.RestResponse = client.Execute(request)

        If response.StatusCode <> Net.HttpStatusCode.OK Then
            l.LOGTHIS("Request failed : " & response.StatusCode)
            l.LOGTHIS("OOid=" & Me.OOid)
            Return False
        End If
        Dim jsonLog As New cJsonLogItem(request.Method.ToString, client.BaseUrl.ToString, "Toevoegen persmissieGroep student", StudentNummer, response.Content, response.StatusCode.ToString)
        jsonLog.Write2database()

        Dim json As JObject = JObject.Parse(response.Content)
        ' Dim dataUser As JArray = json.SelectToken("error")
        'controleren of dit gelukt is
        If response.StatusCode = Net.HttpStatusCode.OK Then
            Return True
        Else
            Return False
        End If

    End Function


    Private Function VoegKlassenToe() As Boolean
        'alle klassen die bij de student zijn geladen naar TP sturen
        Dim Okresult As Boolean = True

        For Each kv In GroepsDeelnames
            'stap 1 - controleer of de groep bestaat
            If BestaatGroepInOO(kv.Value.GroepsCode) = True Then  'controleer of de groep bestaat true als deze bestaat of aangemaakt is
                'groepsdeelname bij student toevoegen
                If VoegGroepBijStudentToeInOO(kv.Value) = False Then
                    Okresult = False
                End If
            End If


        Next
        Return Okresult
    End Function

    Private Function BestaatGroepInOO(Groep As String) As Boolean
        'functie controleert of de groep bestaat, zo niet -> aanmaken
        'true -> aangemaakt of bestaat al
        'false -> bestaat niet en kon niet aangemaakt worden

        'stap 1 - al eerder vandaag gecheckt? -> bestaat -> true
        If dAlleGroepen.ContainsKey(Groep) Then
            If dAlleGroepen(Groep).OOid <> -1 Then
                Return True
            End If
        End If

        'stap 2 - checken of deze bestaat in OO-> bestaat -> true
        Dim clsGroep As cGroep = VraagGroepOpuitOO(Groep)
        If IsNothing(clsGroep) Then
            'bestaat niet 
            'checken of die met een _ bestaat
            Dim sGroepsCodeOrigineel As String = Groep
            Groep = Strings.Replace(Groep, "-", "_")
            clsGroep = VraagGroepOpuitOO(Groep)
            If IsNothing(clsGroep) Then
                'ook met _ in de code bestaat deze niet 
                'groep aanmaken
                Return GroepInOoAanmaken(sGroepsCodeOrigineel)
            Else
                'groep bestaat  in OO dus dictGroepen aanvullen voor verdere snelle verwerking
                If Not dAlleGroepen.ContainsKey(clsGroep.Code) Then
                    dAlleGroepen.Add(clsGroep.Code, clsGroep)
                End If
                If Not dAlleGroepen.ContainsKey(sGroepsCodeOrigineel) Then  'ook de variant met _ toevoegen
                    dAlleGroepen.Add(sGroepsCodeOrigineel, clsGroep)
                End If
                Return True
            End If


        Else
            dAlleGroepen.Add(clsGroep.Code, clsGroep)
            Return True     'groep gevonden
        End If

        'stap 3 - bestaat niet -> groep aanmaken resultaat terug geven
    End Function

    Private Function VraagGroepOpuitOO(sGroep As String) As cGroep
        Dim cReturnGroep As New cGroep
        i.GetToken()    'token opvragen        '

        Dim client As RestSharp.RestClient
        Dim request As New RestSharp.RestRequest
        If OOid = -1 Then Stop

        request.Method = RestSharp.Method.GET
        client = New RestSharp.RestClient(dURLS("TeamGet") & "?name=" & sGroep)
        request.AddHeader("Authorization", "Bearer " & i.AuthenticationToken)
        client.Timeout = -1

        Dim response As RestSharp.RestResponse = client.Execute(request)

        If response.StatusCode <> Net.HttpStatusCode.OK Then
            l.LOGTHIS("Request failed : " & response.StatusCode)
            l.LOGTHIS("OOid=" & Me.OOid)
            Return Nothing
        End If
        Dim jsonLog As New cJsonLogItem(request.Method.ToString, client.BaseUrl.ToString, "Groep opvragen " & sGroep, StudentNummer, response.Content, response.StatusCode.ToString)
        jsonLog.Write2database()

        Dim json As JObject = JObject.Parse(response.Content)
        Dim ResponseError As JValue = json.SelectToken("error")
        If CBool(ResponseError.Value) = False Then
            'geldige response
            Dim dataValue As JObject = json.SelectToken("data.data[0]")
            If IsNothing(dataValue) Then
                Return Nothing
            End If

            Dim sContent As String = dataValue.ToString
            Dim jsonContent As JObject = JObject.Parse(sContent)
            jsonContent.CreateReader()
            cReturnGroep.OOid = CLng(jsonContent("id").ToString)
            cReturnGroep.Code = jsonContent("name").ToString
            cReturnGroep.TeamTypeId = CLng(jsonContent("team_type_id").ToString)
            Return cReturnGroep

        Else
            Application.DoEvents()
        End If

    End Function

    Private Function GroepInOoAanmaken(sGroepscode As String) As Boolean
        'put request doen naar OO
        l.LOGTHIS("Aan te maken groep : " & sGroepscode)
        'https://mboutrechttest.onderwijsonline.nl/api/v1/team?name=ENG-TEC4B&team_type_id=1
        i.GetToken()    'token opvragen        '

        Dim client As RestSharp.RestClient
        Dim request As New RestSharp.RestRequest

        request.Method = RestSharp.Method.POST
        client = New RestSharp.RestClient(dURLS("TeamGet") & "?name=" & sGroepscode & "&team_type_id=1")
        request.AddHeader("Authorization", "Bearer " & i.AuthenticationToken)
        client.Timeout = -1

        Dim response As RestSharp.RestResponse = client.Execute(request)
        If response.StatusCode <> Net.HttpStatusCode.OK Then
            Return False
        End If

        Dim jsonLog As New cJsonLogItem(request.Method.ToString, client.BaseUrl.ToString, "Groep aanmaken in OO " & sGroepscode, StudentNummer, response.Content, response.StatusCode.ToString)
        jsonLog.Write2database()

        Dim json As JObject = JObject.Parse(response.Content)
        Dim ResponseError As JValue = json.SelectToken("error")
        If CBool(ResponseError.Value) = False Then
            'geldige response
            Dim dataValue As JObject = json.SelectToken("data.data[0]")
            If IsNothing(dataValue) Then
                Return Nothing
            End If

            Dim sContent As String = dataValue.ToString
            Dim jsonContent As JObject = JObject.Parse(sContent)
            jsonContent.CreateReader()
            Dim cReturnGroep As New cGroep
            cReturnGroep.OOid = CLng(jsonContent("id").ToString)
            cReturnGroep.Code = jsonContent("name").ToString
            cReturnGroep.TeamTypeId = CLng(jsonContent("team_type_id").ToString)
            If Not dAlleGroepen.ContainsKey(cReturnGroep.Code) Then
                dAlleGroepen.Add(cReturnGroep.Code, cReturnGroep)
            End If
            Return True
        Else
            Return False
        End If

    End Function

    Private Function VoegGroepBijStudentToeInOO(Groep As cGroepsDeelname) As Boolean

        'https://mboutrechttest.onderwijsonline.nl/api/v1/user/:id/groups/attach?group_ids[]=3&group_ids[]=6&group_ids[]=8 
        'Dim cJsonLog As New cJsonLogItem
        l.LOGTHIS("Student koppelen aan groep : " & Me.StudentNummer & " groep:" & Groep.GroepsCode)

        If Groep.IngangsDatum > Now() Then
            '2do groep in mutatietabel zetten voor latere verwerking
            Return Groep.WriteMe2MutatieLog(StudentNummer)  'start en einde naar database schrijven voor latere verwerking
            Return True
        End If

        If Not dAlleGroepen.ContainsKey(Groep.GroepsCode) Then
            Return False
        End If

        i.GetToken()    'token opvragen        '
        Dim sAttach As String = "/" & OOid & "/teams/attach?team_ids[]=" & dAlleGroepen(Groep.GroepsCode).OOid
        Dim client As RestSharp.RestClient
        Dim request As New RestSharp.RestRequest

        request.Method = RestSharp.Method.POST
        client = New RestSharp.RestClient(dURLS("StudentGet") & sAttach)
        request.AddHeader("Authorization", "Bearer " & i.AuthenticationToken)
        client.Timeout = -1

        Dim response As RestSharp.RestResponse = client.Execute(request)
        Dim jsonLog As New cJsonLogItem(request.Method.ToString, client.BaseUrl.ToString, "Toevoegen groep " & Groep.GroepsCode, StudentNummer, response.Content, response.StatusCode.ToString)
        jsonLog.Write2database()

        If response.StatusCode <> Net.HttpStatusCode.OK Then
            Return False
        End If

        Dim json As JObject = JObject.Parse(response.Content)
        Dim ResponseError As JValue = json.SelectToken("error")
        Return True

    End Function

End Class
