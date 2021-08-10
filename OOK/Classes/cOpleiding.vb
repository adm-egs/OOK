Imports Newtonsoft.Json.Linq

Public Class cOpleiding

    Private _code As String
    Private _naam As String
    Private _startdatum As Date
    Private _einddatum As Date
    Private _einddatum_werkelijk As Date
    Private _teamCode As String
    Private _teamNaam As String
    Private _StartjaarOpleiding As Integer
    Private _leerweg As String = ""
    Private _niveau As Integer = -1
    Private _ople_id As Long = -1    '-1 -> not set
    Private _bestaatinOO As Boolean = False
    Private _OO_key As Long = -1
    Private _versiesInOO As New Dictionary(Of Integer, String)

    Public Property Code As String
        Get
            Return _code
        End Get
        Set(value As String)
            _code = value
        End Set
    End Property


    Public Property StartJaarOpleiding As Integer
        Get
            Return _StartjaarOpleiding
        End Get
        Set(value As Integer)
            _StartjaarOpleiding = value
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

    Public Property Startdatum As Date
        Get
            Return _startdatum
        End Get
        Set(value As Date)
            _startdatum = value
        End Set
    End Property

    Public Property Einddatum As Date
        Get
            Return _einddatum
        End Get
        Set(value As Date)
            _einddatum = value
        End Set
    End Property

    Public Property Einddatum_werkelijk As Date
        Get
            If _einddatum_werkelijk < New Date(2000, 1, 1) Then
                Return Einddatum
            Else
                Return _einddatum_werkelijk
            End If

        End Get
        Set(value As Date)
            _einddatum_werkelijk = value
        End Set
    End Property



    Public Property TeamCode As String
        Get
            Return _teamCode
        End Get
        Set(value As String)
            _teamCode = value
        End Set
    End Property

    Public Property TeamNaam As String
        Get
            Return _teamNaam
        End Get
        Set(value As String)
            _teamNaam = value
        End Set
    End Property

    Public Property Ople_Id As Long
        Get
            Return _ople_id
        End Get
        Set(value As Long)
            _ople_id = value
        End Set
    End Property

    Public Property Leerweg As String
        Get
            Return _leerweg
        End Get
        Set(value As String)
            _leerweg = value
        End Set
    End Property

    Public Property Niveau As Integer
        Get
            Return _niveau
        End Get
        Set(value As Integer)
            _niveau = value
        End Set
    End Property

    Public Property BestaatinOO(jaar As Integer) As Boolean
        Get
            If _bestaatinOO = True Then Return True


            'functie controleert of de opleiding bestaat, zo niet -> aanmaken
            'true -> aangemaakt of bestaat al
            'false -> bestaat niet en kon niet aangemaakt worden
            'Opleidingen het in Onderwijs Online programs
            'voorbeeld call: https://mboutrechttest.onderwijsonline.nl/api/v1/program?foreign_id=2846


            'stap 1 - al eerder vandaag gecheckt? -> bestaat -> true
            If dAlleOpleidingen.ContainsKey(Ople_Id) Then   'is deze opleiding al bekend ?
                If VersiesInOO.ContainsKey(jaar) Then
                    Return True 'bestaat -> ok
                End If
            End If

            'controleren of de opleiding bestaat in OO, nog niet eerder gecontroleerd
            'request naar OO sturen

            i.GetToken()    'token opvragen        '

            Dim client As RestSharp.RestClient
            Dim request As New RestSharp.RestRequest

            request.Method = RestSharp.Method.GET
            client = New RestSharp.RestClient(dURLS("Opleidingen") & "?foreign_id=" & jaar & Ople_Id)   'samengestelde sleutel gebruiken
            request.AddHeader("Authorization", "Bearer " & i.AuthenticationToken)
            client.Timeout = -1
            Dim response As RestSharp.RestResponse = client.Execute(request)

            If response.StatusCode <> Net.HttpStatusCode.OK Then
                l.LOGTHIS("Request failed : " & response.StatusCode)
                Return False        'lukt niet of bestaat niet 
            End If

            'Dim jsonLog As New cJsonLogItem(request.Method.ToString, client.BaseUrl.ToString, "Groep opvragen " & sGroep, StudentNummer, response.Content, response.StatusCode.ToString)
            'jsonLog.Write2database()

            Dim json As JObject = JObject.Parse(response.Content)
            Dim ResponseError As JValue = json.SelectToken("error")
            If CBool(ResponseError.Value) = False Then
                'geldige response
                Dim MetaData As JObject = json.SelectToken("data")
                If IsNothing(MetaData) Then Return False
                Dim lAantal As Long = CLng(MetaData("total").ToString)

                If lAantal = 0 Then
                    Return OpleidingAanmakenInOO(jaar)  'niet gevonden -> aanmaken in OO
                Else
                    'gevonden id bijwerken in dictionary
                    Dim dataValue As JObject = json.SelectToken("data.data[0]")
                    If IsNothing(dataValue) Then
                        Return OpleidingAanmakenInOO(jaar)
                    End If
                    Dim lngForeignId As Long = CLng(dataValue("id"))
                    dAlleOpleidingen(Ople_Id).VersiesInOO.Add(jaar, lngForeignId)

                    Return True 'deze bestaat
                End If

            Else
                Return OpleidingAanmakenInOO(jaar)
            End If


        End Get
        Set(value As Boolean)
            _bestaatinOO = value
        End Set
    End Property

    Public ReadOnly Property OO_naam As String
        Get
            Return Me.Naam & " " & Me.Leerweg & " N" & Niveau
        End Get
    End Property

    Public ReadOnly Property OO_Code As String
        Get
            Return Me.Code & " (" & Me.Ople_Id & ")"
        End Get
    End Property

    Public Property VersiesInOO As Dictionary(Of Integer, String)
        Get
            Return _versiesInOO
        End Get
        Set(value As Dictionary(Of Integer, String))
            _versiesInOO = value
        End Set
    End Property

    Private Function OpleidingAanmakenInOO(sJaar As String) As Boolean
        'proberen de opleiding in OO te maken
        'via put call de meta gegevens opsturen
        'api documentatie: https://mboutrechttest.onderwijsonline.nl/apidoc/#api-Program-PostProgram
        'Naam =  Osiris nl_OpleidingsNaam uit ost_opleiding
        'Code = Me.Code & "-" & Me.Leerweg & " (" & Me.Ople_Id & ")"
        'year  = startjaar opleiding
        'vraagstuk waar Bas nog over nadenkt:
        'Wat is het startjaar dat wordt ingevuld? Cohort van de student / Startjaar van de opleiding / Code eventueel uitbreiden met startjaar

        Dim sAanmaken As String = "?name=" & Me.Naam & " (OSIRIS)" & "&code=" & Me.OO_Code & "&year=" & sJaar & "&foreign_id=" & sJaar & Ople_Id

        Dim sJson As String = i.OO_JSON_REQUEST(dURLS("Opleidingen") & sAanmaken, "opleiding aanmaken in OO " & Me.Naam, OO_Code, RestSharp.Method.POST)
        Dim json As JObject = JObject.Parse(sJson)
        Dim ResponseError As JValue = json.SelectToken("error")
        If CBool(ResponseError.Value) = False Then
            Dim sData As JObject = json.SelectToken("data")
            Dim id As Integer = sData.SelectToken("id").Value(Of Integer)
            Me.VersiesInOO.Add(sJaar, id)
            Return True
        Else
            Return False
        End If

    End Function


End Class