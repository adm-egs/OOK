﻿Imports System.IO
Imports System.Net
Imports Newtonsoft.Json.Linq
Imports Oracle.ManagedDataAccess.Client

Public Class cInstellingen
    Private Property _BasisUrl As String
    Private Property _clientId As String
    Private Property _clientSecret As String
    Private Property _GrantSoort As String = "client_credentials"
    Private Property _AuthenticationTokenGeldigHeid As Long

    Private _AuthenticationToken As String

    Private WithEvents timGeldigHeid As New Timer
    Private sqlStudentenOpvragenOsiris As String = ""
    'Private sqlStudentenOpvragenMw As String = ""
    Private _SqlStudentenOsirisOpvragen As String = ""
    Private _SqlStudentOpleidingen As String = ""
    Private _SqlStudentKlasLesgroepen As String = ""
    Private _tokengeldigheid As Long = 0
    Private _omgevingsNaam As String = ""
    Private _CheckenOpUnderscoreGroepen As Boolean = False

    Public Property GrantType As String
        Get
            Return _GrantSoort
        End Get
        Set(value As String)
            _GrantSoort = value
        End Set
    End Property
    Public Property AuthenticationTokenGeldigHeid As Integer
        'een gegeven token is maximaal 60 minuten geldig
        'teller houdt bij hoe lang deze nog geldig is
        Get
            Return _AuthenticationTokenGeldigHeid
        End Get
        Set(value As Integer)

            _AuthenticationTokenGeldigHeid = value
            timGeldigHeid.Interval = 1000
            timGeldigHeid.Enabled = True

        End Set
    End Property

    Public Sub timGeldigHeidTick() Handles timGeldigHeid.Tick
        _AuthenticationTokenGeldigHeid -= 1
        If _AuthenticationTokenGeldigHeid <= 0 Then
            GetToken()
        End If
    End Sub
    Public Property BasisUrl As String
        Get
            Return _BasisUrl
        End Get
        Set(value As String)
            If value = "" Then Throw New Exception("Basis URL niet opgegeven in ini file")
            _BasisUrl = value
        End Set
    End Property

    Public Property ClientId As String
        Get
            Return _clientId
        End Get
        Set(value As String)
            If value = "" Then Throw New Exception("Geen username voor de webservice")
            _clientId = value
        End Set
    End Property

    Public Property ClientSecret As String
        Get
            Return _clientSecret
        End Get

        Set(value As String)
            If value = "" Then Throw New Exception("Geen password voor de webservice")
            _clientSecret = value
        End Set
    End Property
    Public ReadOnly Property AuthenticatieUrl As String
        Get
            Return BasisUrl & "oauth/token"
        End Get
    End Property
    Public Property AuthenticationToken As String
        Get
            If _AuthenticationToken = "" Then
                GetToken()
            End If

            Return _AuthenticationToken
        End Get
        Set(value As String)
            _AuthenticationToken = value
        End Set
    End Property



    Public Property SqlStudentenOsirisOpvragen As String
        Get
            Return _SqlStudentenOsirisOpvragen
        End Get
        Set(value As String)
            _SqlStudentenOsirisOpvragen = value
        End Set
    End Property

    Public ReadOnly Property sqlEenStudentOpvragen(sStudentNr As String) As String
        Get
            Dim shulp As String = SqlStudentenOsirisOpvragen
            shulp = Strings.Replace(shulp, "s.studentnummer > 0", "s.studentnummer = " & sStudentNr)
            Return shulp
        End Get
    End Property

    Public ReadOnly Property SqlStudentOsirisOpvragen(sStudentNummer As String) As String
        Get
            Dim sHulp As String = _SqlStudentenOsirisOpvragen
            sHulp = Strings.Replace(sHulp, " > 0", "=" & sStudentNummer)
            Return sHulp
        End Get
    End Property
    Public Property SqlStudentOpleidingenAlleStudenten As String
        Get
            Return _SqlStudentOpleidingen
        End Get
        Set(value As String)
            _SqlStudentOpleidingen = value
        End Set
    End Property

    Public ReadOnly Property sqlStudentOpleidingenEenStudent(sStudentNr As String) As String
        Get
            Dim shulp = i.SqlStudentOpleidingenAlleStudenten
            shulp = Strings.Replace(shulp, "and s.studentnummer > 0", "and s.studentnummer=" & sStudentNr)
            Return shulp
        End Get
    End Property
    Public Property SqlStudentenKlasLesgroepen As String
        Get
            Return _SqlStudentKlasLesgroepen
        End Get
        Set(value As String)
            _SqlStudentKlasLesgroepen = value
        End Set
    End Property

    Public ReadOnly Property SqlStudentKlasLesgroepenEenStudent(sStudentNr As String) As String
        Get
            Dim shulp As String = SqlStudentenKlasLesgroepen & "and sgr.studentnummer = " & sStudentNr
            Return shulp
        End Get

    End Property

    Public ReadOnly Property sqlStudentKlasLesgroep(sStudentNummer As String) As String
        'geeft de klassen / lesgroepen van 1 student terug
        Get
            Dim sHulp As String = SqlStudentenKlasLesgroepen
            sHulp = Strings.Replace(sHulp, " > 0", "=" & sStudentNummer)
            Return sHulp
        End Get
    End Property

    Public Property Tokengeldigheid As Long
        Get
            Return _tokengeldigheid
        End Get
        Set(value As Long)
            _tokengeldigheid = value
        End Set
    End Property

    Public Property OmgevingsNaam As String
        Get
            Return _omgevingsNaam
        End Get
        Set(value As String)
            _omgevingsNaam = value
        End Set
    End Property

    Public Property CheckenOpUnderscoreGroepen As Boolean
        Get
            Return _CheckenOpUnderscoreGroepen
        End Get
        Set(value As Boolean)
            _CheckenOpUnderscoreGroepen = value
        End Set
    End Property

    Public Sub New()

        Try
            SqlStudentenOsirisOpvragen = getQuery("osiris\01_studenten_opvragen.txt")
        Catch ex As Exception
            Throw New Exception("Kan sql studenten niet opvragen")
        End Try

        Try
            SqlStudentOpleidingenAlleStudenten = getQuery("osiris\02_studenten_opleidingen.txt")
        Catch ex As Exception
            Throw New Exception("Kan sql studenten_opleidingen niet opvragen")
        End Try

        Try
            SqlStudentenKlasLesgroepen = getQuery("osiris\03_studenten_klas-lesgroep.txt")
        Catch ex As Exception
            Throw New Exception("Kan bestand niet inlezen : 03_studenten_klas-lesgroep.txt")
        End Try

        Try
            Me.CheckenOpUnderscoreGroepen = ini.GetBoolean("Algemeen", "CheckOpUnderscoreInGroepCode", False)
        Catch ex As Exception
            Throw New Exception("Kan waarde van checken op underscore groepen niet opvragen: " & ex.Message)
        End Try
    End Sub
    Public Function GetToken() As Boolean
        Try
            'If AuthenticationToken <> "" Then
            '    Return True
            'End If

            If AuthenticationTokenGeldigHeid > 0 Then
                Return True
            End If

            Dim client As New RestSharp.RestClient("https://mboutrechttest.onderwijsonline.nl/oauth/token")
            Dim request As New RestSharp.RestRequest()

            client.Timeout = -1
            request.Method = RestSharp.Method.POST

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded")
            request.AddHeader("Cookie", "simplesaml=5e71be56aea3236f30ebca99590b2518")
            request.AddParameter("client_id", "3")
            request.AddParameter("client_secret", "dsKmUa8bbNLxf8YgFoXnVn5LVYsWS666l70sd4ey")
            request.AddParameter("grant_type", "client_credentials")

            Dim response As RestSharp.RestResponse = client.Execute(request)
            Dim json As JObject = JObject.Parse(response.Content)
            Dim sTokenType As String = json.SelectToken("token_type", False).Value(Of String)
            Dim expires_in As String = json.SelectToken("expires_in", False).Value(Of String)

            AuthenticationToken = json.SelectToken("access_token", False).Value(Of String)
            If l.UitgebreideLogging Then l.LOGTHIS("Authentication token : " & AuthenticationToken)
            If l.UitgebreideLogging Then l.LOGTHIS("Expires in : " & i.AuthenticationTokenGeldigHeid)

            Me.Tokengeldigheid = CLng(expires_in)
            If l.UitgebreideLogging Then l.LOGTHIS("Authenticatie token opgevraagd")

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function


    Private Function DoeRequest(Url As String) As String
        'algemene functie om data op te vragen via een webrequest
        'geeft de json string terug

        '  Dim sURL As String = AanmeldingenUrl(New Date(2020, 10, 1))
        Dim myRequest As HttpWebRequest = CType(WebRequest.Create(Url), HttpWebRequest)
        myRequest.Headers.Add("Auth_token", i.AuthenticationToken)

        Dim myResponse As HttpWebResponse = CType(myRequest.GetResponse, HttpWebResponse)
        Dim myStreamReader As StreamReader = New StreamReader(myResponse.GetResponseStream(), True)
        Dim json As String = myStreamReader.ReadToEnd
        myResponse.Close()
        myStreamReader.Close()

        Return json
    End Function
    Function LoadDefaults() As Boolean
        Try
            GetURLS()

            ClientId = ini.GetString("connect", "client_id", "")
            ClientSecret = ini.GetString("connect", "client_secret", "")
            OmgevingsNaam = ini.GetString("algemeen", "omgeving", "onbekend")

        Catch ex As Exception
            l.LOGTHIS("Fout bij opvragen default instellingen")
            Return False
        End Try

        Return True
    End Function
    Private Function FatalError(sMessage As String) As Boolean
        l.LOGTHIS(sMessage)
        MsgBox(sMessage,, "Fatal error")
        End
        Return True
    End Function
    Function GetStudentsOsiris(Optional sStudentNr As String = "") As Boolean
        Dim bolKlassen As Boolean = False
        Dim bolOpleidingen As Boolean = False


        Dim rd As OracleDataReader
        If sStudentNr = "" Then
            rd = dbOsiris.oracleQueryUitvoeren(i.SqlStudentenOsirisOpvragen) 'alle studenten opvragen
        Else
            rd = dbOsiris.oracleQueryUitvoeren(i.sqlEenStudentOpvragen(sStudentNr))
        End If

        If rd.HasRows = False Then
            rd.Close()
            l.LOGTHIS("Geen studenten beschikbaar")
            Return False
        End If

        ' functie vraagt de studenten uit Osiris op
        Try
            If sStudentNr = "" Then
                dictOsirisStudentenKeyStudentNr.Clear()     'dictionary leeg maken
            Else
                If dictOsirisStudentenKeyStudentNr.ContainsKey(sStudentNr) Then
                    dictOsirisStudentenKeyStudentNr.Remove(sStudentNr)
                End If
            End If
            While rd.Read
                'studentobject vullen
                Dim s As cStudent = GetStudentDataOsiris(rd)

                If Not dictOsirisStudentenKeyStudentNr.ContainsKey(s.Id) Then
                    dictOsirisStudentenKeyStudentNr.Add(s.Id, s)
                Else
                    l.LOGTHIS("Fout dubbele student uit query gekomen " & s.Username)
                End If

            End While
        Catch ex As Exception
            l.LOGTHIS("Fout bij opvragen studenten : " & ex.Message)
            Return False
        End Try

        If sStudentNr = "" Then
            'alle studenten opvragen
            bolOpleidingen = GetStudentOpleidingenOsiris()
            bolKlassen = GetStudentKlassen()
        Else
            'alleen van 1 student opvragen
            bolOpleidingen = GetStudentOpleidingenOsiris(sStudentNr)
            bolKlassen = GetStudentKlassen(sStudentNr)
        End If



        If bolOpleidingen = True And bolKlassen = True Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function GetStudentDataOsiris(rd As OracleDataReader) As cStudent

        Dim s As New cStudent
        With s
            .Id = dbOsiris.oraSafeGetDecimal(rd, "ID")
            .Username = .Id
            .Roepnaam = dbOsiris.oraSafeGetString(rd, "Roepnaam")
            .Tussenvoegsels = dbOsiris.oraSafeGetString(rd, "Tussenvoegsels")
            .Achternaam = dbOsiris.oraSafeGetString(rd, "Achternaam")
            .Geslacht = dbOsiris.oraSafeGetString(rd, "Geslacht")
            .geboorteDatum = dbOsiris.oraGetSafeDate(rd, "Geboortedatum")
            .Initialen = dbOsiris.oraSafeGetString(rd, "initialen")
            .Mobiel_Nummer = dbOsiris.oraSafeGetString(rd, "mobiel_telefoonnummer")
            .WerkMailAdres = dbOsiris.oraSafeGetString(rd, "e_mail_adres")
            .Voornamen = "" 'dbOsiris.oraSafeGetString(rd, "voornamen") -> was ook zijn in de vorige versie, niet gevuld
            .PrimaireLocatieCode = dbOsiris.oraSafeGetString(rd, "PrimaireLocatieCode")
            .PrimaireLocatieNaam = dbOsiris.oraSafeGetString(rd, "PrimaireLocatieNaam")

        End With
        Return s

    End Function
    Private Function GetStudentOpleidingenOsiris(Optional sStudentNr As String = "") As Boolean

        'alle opleidingen van de studenten opvragen en deze aanvullen bij de studenten uit de dictOsirisStudenten

        Dim rd As OracleDataReader
        If sStudentNr = "" Then
            'alle studenten opvragen
            l.LOGTHIS("Opleidingen bij de student(en) opvragen en invullen")
            rd = dbOsiris.oracleQueryUitvoeren(i.SqlStudentOpleidingenAlleStudenten)
        Else
            l.LOGTHIS("Opleidingen bij student " & sStudentNr & " invullen")
            rd = dbOsiris.oracleQueryUitvoeren(i.sqlStudentOpleidingenEenStudent(sStudentNr))
        End If


        If rd.HasRows = False Then
            rd.Close()
            l.LOGTHIS("Geen data beschikbaar")
            Return False
        End If
        Try
            Dim studentNummer As String = ""
            While rd.Read
                Dim opl As New cOpleiding
                studentNummer = dbOsiris.oraSafeGetDecimal(rd, "studentnummer")
                opl.Code = dbOsiris.oraSafeGetString(rd, "Opleidingscode")
                opl.Naam = dbOsiris.oraSafeGetString(rd, "Opleidingsnaam")
                opl.Startdatum = dbOsiris.oraGetSafeDate(rd, "Startdatum")
                opl.Einddatum = dbOsiris.oraGetSafeDate(rd, "Einddatum")
                opl.Einddatum_werkelijk = dbOsiris.oraGetSafeDate(rd, "Einddatum_werkelijk")
                opl.TeamCode = dbOsiris.oraSafeGetString(rd, "Teamcodes")
                opl.TeamNaam = dbOsiris.oraSafeGetString(rd, "Teamnamen")
                opl.cohort = dbOsiris.oraSafeGetDecimal(rd, "Cohort")
                'aanvullende velden bij de student toevoegen
                If dictOsirisStudentenKeyStudentNr.ContainsKey(studentNummer) Then
                    'opleidingen bij de student toevoegen
                    If Not dictOsirisStudentenKeyStudentNr(studentNummer).Opleidingen.ContainsKey(opl.UniqueKey) Then
                        dictOsirisStudentenKeyStudentNr(studentNummer).Opleidingen.Add(opl.UniqueKey, opl)
                    End If
                    'teams toevoegen
                    dictOsirisStudentenKeyStudentNr(studentNummer).AddTeam(opl.TeamNaam, opl.TeamNaam)

                Else
                    l.LOGTHIS("student niet gevonden voor opleiding: " & studentNummer)
                End If

                'teams toevoegen bij de student zelf

            End While
        Catch ex As Exception
            Return False

        End Try
        Return True
    End Function

    Private Function GetStudentKlassen(Optional sStudentNr As String = "") As Boolean
        l.LOGTHIS("Klassen bij de studenten opvragen en invullen")
        Dim rd As OracleDataReader
        If sStudentNr = "" Then
            rd = dbOsiris.oracleQueryUitvoeren(i.SqlStudentenKlasLesgroepen)
        Else
            rd = dbOsiris.oracleQueryUitvoeren(i.SqlStudentKlasLesgroepenEenStudent(sStudentNr))
        End If

        If rd.HasRows = False Then
            rd.Close()
            l.LOGTHIS("Geen relatie student - klas - lesgroep beschikbaar")
            Return False
        End If

        Try
            Dim studentNummer As String = ""
            While rd.Read
                Dim deelname As New cGroepsDeelname
                studentNummer = dbOsiris.oraSafeGetDecimal(rd, "studentnummer")
                deelname.GroepsCode = dbOsiris.oraSafeGetString(rd, "groepscode")
                deelname.GroepNaam = dbOsiris.oraSafeGetString(rd, "groepsnaam")
                deelname.IngangsDatum = dbOsiris.oraGetSafeDate(rd, "ingangsdatum")
                deelname.AfloopDatum = dbOsiris.oraGetSafeDate(rd, "afloopdatum")


                'alle velden binnen bij de student de opleiding toevoegen
                If dictOsirisStudentenKeyStudentNr.ContainsKey(studentNummer) Then
                    If Not dictOsirisStudentenKeyStudentNr(studentNummer).GroepsDeelnames.ContainsKey(deelname.UniqueKey) Then
                        dictOsirisStudentenKeyStudentNr(studentNummer).GroepsDeelnames.Add(deelname.UniqueKey, deelname)
                    End If
                Else
                    ' l.LOGTHIS("student niet gevonden voor groepsdeelname: " & studentNummer)
                End If
            End While
        Catch ex As Exception
            Return False

        End Try
        Return True
    End Function

    Public Function GetStudentsOO() As Boolean
        Try
            GetToken()
            '

            Dim client As New RestSharp.RestClient(dURLS("StudentGet"))
            Dim request As New RestSharp.RestRequest()

            client.Timeout = -1
            request.Method = RestSharp.Method.GET

            request.AddHeader("Authorization", "Bearer " & i.AuthenticationToken)

            Dim response As RestSharp.RestResponse = client.Execute(request)
            Dim json As JObject = JObject.Parse(response.Content)
            Dim dataPage As JObject = json.SelectToken("data")
            Dim dataUser As JArray = dataPage.SelectToken("data")
            Dim UserCountInCurrentPage As Integer = dataUser.Count
            dPersonen.Clear()

            For Each jUser In dataUser
                'json object naar user overzetten
                Dim p As New cPersoon
                p.getUserData(jUser)
                dPersonen.Add(p.Id, p)
            Next


            '            Dim sTokenType As String = json.SelectToken("token_type", False).Value(Of String)
            '           Dim expires_in As String = json.SelectToken("expires_in", False).Value(Of String)

            '           AuthenticationToken = json.SelectToken("access_token", False).Value(Of String)
            '           If l.UitgebreideLogging Then l.LOGTHIS("Authentication token : " & AuthenticationToken)
            '          If l.UitgebreideLogging Then l.LOGTHIS("Expires in : " & i.AuthenticationTokenGeldigHeid)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetURLS() As Boolean
        dURLS.Clear()
        '
        dURLS.Add("BasisUrl", ini.GetString("OO", "BasisURL", ""))
        dURLS.Add("StudentGet", ini.GetString("OO", "StudentURL", ""))
        dURLS.Add("TeamGet", ini.GetString("OO", "TeamURL", ""))

        BasisUrl = dURLS("BasisUrl")

        Return True

    End Function

    Private Function getQuery(sfile As String) As String
        Dim fi As New System.IO.FileInfo(Application.StartupPath & "queries\" & sfile)
        If fi.Exists = False Then
            Throw New Exception("Kan een vereist bestand niet inlezen : " & sfile)
            End
        End If
        Try
            Dim sHulp As String = My.Computer.FileSystem.ReadAllText(Application.StartupPath & "queries\" & sfile)
            Return sHulp
        Catch ex As Exception
            Throw New Exception("Fout : " & ex.Message)
        End Try
    End Function

    Public Function CheckOsirisVoorMutaties()
        'functie controleert diverse tabellen op relevante mutatis
        'ost_student - STUDENT
        'OST 
    End Function

    Public Function OO_JSON_REQUEST(urlRequest As String, sOmschrijving As String, sStudentNummer As String) As String
        'standaard deel van een request 
        i.GetToken()    'token opvragen        '
        Dim client As RestSharp.RestClient
        Dim request As New RestSharp.RestRequest

        request.Method = RestSharp.Method.POST
        client = New RestSharp.RestClient(urlRequest)
        request.AddHeader("Authorization", "Bearer " & i.AuthenticationToken)
        client.Timeout = -1

        Dim response As RestSharp.RestResponse = client.Execute(request)
        If response.StatusCode <> Net.HttpStatusCode.OK Then
            Return False
        End If

        Dim jsonLog As New cJsonLogItem(request.Method.ToString, client.BaseUrl.ToString, sOmschrijving, sStudentNummer, response.Content, response.StatusCode.ToString)
        jsonLog.Write2database()
        Return response.Content

    End Function
End Class
