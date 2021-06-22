Imports System.IO
Imports System.Net
Imports Newtonsoft.Json.Linq


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

    Public Property SqlStudentenOpvragenOsiris1 As String
        Get
            Return sqlStudentenOpvragenOsiris
        End Get
        Set(value As String)
            sqlStudentenOpvragenOsiris = value
        End Set
    End Property

    Public Function GetToken() As Boolean
        Try
            If AuthenticationToken <> "" Then
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

            'Dim c As cAuthResponse = jsond RestSharp.Deserializers.XmlDeserializer( ' Json2KeyValue.JsonConvert.DeserializeObject(Of cAuthResponse)(response.Content)
            'Dim jss = New RestSharp.Serializers()
            Dim sTokenType As String = json.SelectToken("token_type", False).Value(Of String)
            Dim expires_in As String = json.SelectToken("expires_in", False).Value(Of String)

            AuthenticationToken = json.SelectToken("access_token", False).Value(Of String)
            If l.UitgebreideLogging Then l.LOGTHIS("Authentication token : " & AuthenticationToken)
            If l.UitgebreideLogging Then l.LOGTHIS("Expires in : " & i.AuthenticationTokenGeldigHeid)
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
            getUrls
            BasisUrl = ini.GetString("OO", "BasisURL", "https://mboutrechttest.onderwijsonline.nl/")
            ClientId = ini.GetString("URLS", "client_id", "3")
            ClientSecret = ini.GetString("URLS", "client_secret", "dsKmUa8bbNLxf8YgFoXnVn5LVYsWS666l70sd4ey")

            Try
                sqlStudentenOpvragenOsiris = System.IO.File.ReadAllText(Application.StartupPath & "\queries\01_studenten_opvragen.txt")
            Catch ex As Exception
                FatalError("Kan query studenten opvragen niet openen " & Application.StartupPath & "\queries\01_studenten_opvragen.txt")
                End
            End Try

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

    Public Function GetStudents() As Boolean
        Try

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

            'Dim c As cAuthResponse = jsond RestSharp.Deserializers.XmlDeserializer( ' Json2KeyValue.JsonConvert.DeserializeObject(Of cAuthResponse)(response.Content)
            'Dim jss = New RestSharp.Serializers()
            Dim sTokenType As String = json.SelectToken("token_type", False).Value(Of String)
            Dim expires_in As String = json.SelectToken("expires_in", False).Value(Of String)

            AuthenticationToken = json.SelectToken("access_token", False).Value(Of String)
            If l.UitgebreideLogging Then l.LOGTHIS("Authentication token : " & AuthenticationToken)
            If l.UitgebreideLogging Then l.LOGTHIS("Expires in : " & i.AuthenticationTokenGeldigHeid)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetURLS() As Boolean
        dURLS.Clear()
        dURLS.Add("BasisUrl", "https://mboutrechttest.onderwijsonline.nl/")
        'https://mboutrechttest.onderwijsonline.nl/api/v1/user
        Return True

    End Function
End Class
