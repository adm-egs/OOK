Imports Newtonsoft.Json.Linq

Public Class cStudentBasis
#Region "properties"


    Inherits cPersoon
    Private _WerkMailAdres As String
    Private _opleidingen As New Dictionary(Of String, cOpleidingsDeelname)
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

    Public Property Opleidingen As Dictionary(Of String, cOpleidingsDeelname)
        Get
            Return _opleidingen
        End Get
        Set(value As Dictionary(Of String, cOpleidingsDeelname))
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
            ChangeUserInOO(False)   'update user
        End If

        Return True
    End Function


    Public Function GetStudentUitOoOpBasisVanStudentNummer() As Boolean
        Dim c As cStudentBasis = GetStudentClassUitOoStudentNummer(Me.StudentNummer, True)
        If IsNothing(c) Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function GetStudentClassUitOoStudentNummer(sStudentNummer As String, Add2Personen As Boolean) As cStudentBasis
        'gegevens student uit OO opvragen
        Dim request As New RestSharp.RestRequest()
        Dim client As New RestSharp.RestClient(dURLS("StudentGet") & "?username=" & sStudentNummer)
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
                Return Nothing
            Else
                BekendInOO = True
            End If

            For Each jUser In dataUser
                'json object naar user overzetten
                getUserData(jUser)
                'Dim s As New cStudent
                's.getUserData(jUser)    'velden invullen in object
                If Add2Personen = True Then
                    If dPersonen.ContainsKey(StudentNummer) Then
                        dPersonen.Remove(StudentNummer)
                    End If
                    dPersonen.Add(StudentNummer, Me)
                Else
                    Return Me
                End If

            Next
            Return Me
        Catch ex As Exception
            Dim jsonLog As New cJsonLogItem(request.Method.ToString, client.BaseUrl.ToString, "Opvragen student uit OO", StudentNummer, response.Content, response.StatusCode.ToString)
            jsonLog.Gelukt = "N"
            jsonLog.Write2database()


            Return Nothing
        End Try
    End Function

    Public Function ChangeUserInOO(Optional bolCreate As Boolean = False) As Boolean
        'functie maakt de user aan in OO of update
        If BekendInOO = True Then
            If CreateOrUpdateUserNAW(False) = False Then
                l.LOGTHIS("Update student niet gelukt")
                Return False
            End If
        Else
            If CreateOrUpdateUserNAW(True) = False Then
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
        '2do zit nog niet goed in elkaar, werkt wel
        'student aan opleiding koppelen
        For Each kv In Opleidingen
            'check of de opleiding bestaat
            If dAlleOpleidingen.ContainsKey(kv.Value.Ople_id) Then
                'controleren of deze jaarversie ook bestaat
                Dim o As cOpleiding = dAlleOpleidingen(kv.Value.Ople_id)
                If Not o.VersiesInOO.ContainsKey(kv.Value.Cohort) Then
                    'of deze versie bestaat is niet bekend -> check in OO
                    ' o.BestaatinOO(kv.Value.Cohort)
                    If o.BestaatinOO(kv.Value.Cohort) Then  'Check in OO of deze versie bestaat
                    End If
                End If
            Else
                'zou niet voormogen komen
                l.LOGTHIS("opleiding bestaat niet ID=" & kv.Value.Ople_id)
            End If
        Next
        VoegOpleidingenToeAanStudentInOO()

        'Organisatorische eenheden toevoegen in OO zijn dit teams
        Dim first As Boolean = False    'sync is al gedaan bij groepen
        For Each kv In Opleidingen
            'check of team bekend is in OO : kv.Value.Teamcode
            Dim sTeamcode As String = kv.Value.Teamcode
            If dAlleTeams.ContainsKey(sTeamcode) Then
                Dim t As cTeam = dAlleTeams(sTeamcode)
                If t.OoID = -1 Then
                    'ID in ondewijsonline is onbekend en kon niet worden aangemaakt
                    l.LOGTHIS("Kan student niet aan team koppelen : Team bestaat niet in OO:" & sTeamcode)
                    Return False
                Else
                    'student aan team koppelen
                    Dim sOOsyncTeam As String = dURLS("StudentGet") & "/" & Me.OOid & "/teams/sync?team_ids[]=" & t.OoID
                    Dim sOOAddTeam As String = dURLS("StudentGet") & "/" & Me.OOid & "/teams/attach?team_ids[]=" & t.OoID

                    Dim sJsonResult As String = ""
                    If first Then
                        sJsonResult = i.OO_JSON_REQUEST(sOOsyncTeam, "Student aan team sync :" & t.Naam, StudentNummer, RestSharp.Method.POST)
                        first = False
                    Else
                        sJsonResult = i.OO_JSON_REQUEST(sOOAddTeam, "Student aan team koppelen :" & t.Naam, StudentNummer, RestSharp.Method.POST)
                    End If
                    Dim json As JObject = JObject.Parse(sJsonResult)
                    Dim ResponseError As JValue = json.SelectToken("error")
                    If CBool(ResponseError.Value) = False Then
                        l.LOGTHIS("Student aan team gekoppeld : " & t.Code, 1)

                    Else
                        l.LOGTHIS("Student aan team koppelen niet gelukt :" & t.Code, 1)
                        Return False
                    End If
                End If
            Else
                l.LOGTHIS("Onbekend team gevonden : " & sTeamcode, 1)
                Return False
            End If
        Next

        Return True

    End Function
    Private Function CreateOrUpdateUserNAW(Optional bolCreate As Boolean = False) As Boolean

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
        request.AddParameter("foreign_id", Me.StudentNummer)
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
        Dim first As Boolean = True
        For Each kv In GroepsDeelnames
            'stap 1 - controleer of de groep bestaat
            If BestaatGroepInOO(kv.Value.GroepsCode) = True Then  'controleer of de groep bestaat true als deze bestaat of aangemaakt is
                'groepsdeelname bij student toevoegen
                If VoegGroepBijStudentToeInOO(kv.Value, first) = False Then
                    Okresult = False
                Else
                    first = False
                End If
            End If

        Next
        Return Okresult
    End Function

    Public Function BestaatGroepInOO(Groep As String) As Boolean
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
            'groep bestaat niet 
            If i.CheckenOpUnderscoreGroepen = False Then    'als we niet verder checken, dan de groep aanmaken in OO
                Return GroepInOoAanmaken(Groep)
            End If

            'We checken of de groep[ met een - in plaats van een _ in de code bestaat
            Dim sGroepsCodeOrigineel As String = Groep
            Groep = Strings.Replace(Groep, "-", "_")
            clsGroep = VraagGroepOpuitOO(Groep)

            If IsNothing(clsGroep) Then
                'ook met - in de code bestaat deze niet 
                'dus -> groep aanmaken
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
        'Groep aanmaken in OO, indien gelukt de gegevens opslaan in de dictionary

        Dim json As JObject = i.OO_JSON_REQUEST(dURLS("TeamGet") & "?name=" & sGroepscode & "&team_type_id=1", "Groep aanmaken in OO " & sGroepscode, StudentNummer, RestSharp.Method.POST)
        Dim ResponseError As JValue = json.SelectToken("error")
        If CBool(ResponseError.Value) = False Then
            'geldige response
            Dim dataValue As JObject = json.SelectToken("data.data[0]")
            If IsNothing(dataValue) Then
                Return False
            End If

            Dim sContent As String = dataValue.ToString
            Dim jsonContent As JObject = JObject.Parse(sContent)
            jsonContent.CreateReader()
            'groep is aangemaakt. meegeleverde data van OO opslaan
            Dim cReturnGroep As New cGroep
            cReturnGroep.OOid = CLng(jsonContent("id").ToString)
            cReturnGroep.Code = jsonContent("name").ToString
            cReturnGroep.TeamTypeId = CLng(jsonContent("team_type_id").ToString)

            If Not dAlleGroepen.ContainsKey(cReturnGroep.Code) Then
                '2do groepsdata opslaan in temp tabel van de Middleware voor de volgende keer
                dAlleGroepen.Add(cReturnGroep.Code, cReturnGroep)
            End If
            Return True
        Else
            Return False
        End If

    End Function

    Private Function VoegGroepBijStudentToeInOO(Groep As cGroepsDeelname, first As Boolean) As Boolean

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

        Dim sAttach As String = ""
        If first Then
            sAttach = "/" & OOid & "/teams/sync?team_ids[]=" & dAlleGroepen(Groep.GroepsCode).OOid
        Else
            sAttach = "/" & OOid & "/teams/attach?team_ids[]=" & dAlleGroepen(Groep.GroepsCode).OOid
        End If

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



    Public Function Osiris_node() As TreeNode
        'maakt een node aan voor een treeview met studentdata
        Dim ndHoofd As New TreeNode(Me.StudentNummer)
        Dim ndOsiris As New TreeNode(Me.StudentNummer & " - Osiris")
        Dim ndNaw As TreeNode = ndOsiris.Nodes.Add(Me.VolledigeNaam)
        Dim ndOpleidingen As New TreeNode("Opleiding")
        Dim ndKlassen As New TreeNode("Klassen")

        For Each kv In Me.GroepsDeelnames
            ndKlassen.Nodes.Add(kv.Value.GroepsCode & " " & kv.Value.IngangsDatum & " - " & kv.Value.AfloopDatum)
        Next
        For Each kv In Me.Opleidingen
            Dim ndOpleiding As New TreeNode(kv.Value.CreboNr)
            ndOpleiding.Nodes.Add("Cohort :" & kv.Value.cohort)
            ndOpleiding.Nodes.Add("Crebo  :" & kv.Value.CreboNr)
            ndOpleiding.Nodes.Add("Opleiding ID: " & kv.Key & " - " & kv.Value.Ople_id)
            Dim o As cOpleiding = dAlleOpleidingen(kv.Value.Ople_id)

            ndOpleiding.Nodes.Add("Naam   : " & o.Naam)
            ndOpleiding.Nodes.Add("Niveau : " & o.Niveau)
            ndOpleiding.Nodes.Add("Leeweg " & o.Leerweg)
            ndOpleiding.Nodes.Add("Bestaat in OO:" & o.BestaatinOO(kv.Value.Cohort))

            ndOpleiding.Nodes.Add("Team   : " & o.TeamCode & " - " & o.Naam)
            ndOpleiding.Nodes.Add("Start  : " & kv.Value.StartDatum)
            ndOpleiding.Nodes.Add("Eind   : " & kv.Value.EindDatumWerkelijk)
            ndOpleidingen.Nodes.Add(ndOpleiding)
        Next
        ndOsiris.Nodes.Add(ndKlassen)
        ndOsiris.Nodes.Add(ndOpleidingen)
        ndOsiris.ExpandAll()
        Return ndOsiris

    End Function

    Function VoegOpleidingenToeAanStudentInOO() As Boolean
        Dim first As Boolean = True
        Try
            For Each o In Me.Opleidingen
                Dim startJaar As Integer = o.Value.Cohort

                Dim oo_opleiding_id As Long = dAlleOpleidingen(o.Value.Ople_id).VersiesInOO(startJaar)
                Dim sOOupdate As String = dURLS("Opleidingen") & "/" & oo_opleiding_id & "/users/attach?user_ids[]=" & Me.OOid
                Dim sOOupdateFirst As String = dURLS("StudentGet") & "/" & Me.OOid & "/programs/sync?program_ids[]=" & oo_opleiding_id
                ' https://mboutrechttest.onderwijsonline.nl/api/v1/program/3597/users/attach?user_ids[]=44736

                'request aan webservice aanbieden
                Dim jsonString As String = ""
                If first = True Then
                    jsonString = i.OO_JSON_REQUEST(sOOupdateFirst, "Opleiding aan student syncen", Me.StudentNummer, RestSharp.Method.POST)
                    first = False
                Else
                    jsonString = i.OO_JSON_REQUEST(sOOupdate, "Opleiding aan student koppelen", Me.StudentNummer, RestSharp.Method.POST)
                End If

                Dim json As JObject = JObject.Parse(jsonString)
                Dim ResponseError As JValue = json.SelectToken("error")
                If CBool(ResponseError.Value) = False Then
                    l.LOGTHIS("Student " & Me.StudentNummer & " aan opleiding gekoppeld " & dAlleOpleidingen(o.Value.Ople_id).Naam & " jaar: " & startJaar)
                Else
                    l.LOGTHIS("Koppelen niet gelukt")
                End If
            Next
        Catch ex As Exception
            l.LOGTHIS("Fout bij koppelen student " & Me.StudentNummer & " aan zijn opleidingen: " & ex.Message)
            Return False
        End Try
        Return True
    End Function
End Class
