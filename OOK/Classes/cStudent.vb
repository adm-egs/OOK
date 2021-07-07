Imports Newtonsoft.Json.Linq

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
        Try
            i.GetToken()    'token opvragen

            Dim client As New RestSharp.RestClient(dURLS("StudentGet") & "?username=" & Me.StudentNummer)
            Dim request As New RestSharp.RestRequest()

            client.Timeout = -1
            request.Method = RestSharp.Method.GET
            request.AddHeader("Authorization", "Bearer " & i.AuthenticationToken)

            Dim response As RestSharp.RestResponse = client.Execute(request)
            Dim json As JObject = JObject.Parse(response.Content)
            Dim dataPage As JObject = json.SelectToken("data")
            Dim dataUser As JArray = dataPage.SelectToken("data")
            Dim UserCountInCurrentPage As Integer = dataUser.Count
            'dPersonen.Clear()

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
        Stop
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

        Dim json As JObject = JObject.Parse(response.Content)
        ' Dim dataUser As JArray = json.SelectToken("error")
        'controleren of dit gelukt is
        Return True
    End Function


    Private Function VoegKlassenToe() As Boolean
        i.GetToken()    'token opvragen        '

        Dim client As RestSharp.RestClient '  New RestSharp.RestClient(dURLS("StudentGet"))
        Dim request As New RestSharp.RestRequest


    End Function
End Class
