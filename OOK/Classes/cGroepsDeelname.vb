Public Class cGroepsDeelname

    Private _IngangsDatum As Date
    Private _AfloopDatum As Date
    Private _groepsCode As String
    Private _groepsNaam As String


    Public Sub New(sGroepNaam As String, dIngangsDatum As Date, dAfloopDatum As Date)
        GroepNaam = sGroepNaam
        IngangsDatum = dIngangsDatum
        AfloopDatum = dAfloopDatum
        If Not dAlleGroepen.ContainsKey(sGroepNaam) Then
            GetDbInfo()
        End If
    End Sub

    Public Sub New()

    End Sub

    Public Property GroepsCode As String
        Get
            Return _groepsCode
        End Get
        Set(value As String)
            _groepsCode = value
        End Set
    End Property
    Public Property GroepNaam As String
        Get
            Return _groepsNaam
        End Get
        Set(value As String)
            _groepsNaam = value
        End Set
    End Property

    Public Property IngangsDatum As Date
        Get
            Return _IngangsDatum
        End Get
        Set(value As Date)
            _IngangsDatum = value
        End Set
    End Property

    Public Property AfloopDatum As Date
        Get
            Return _AfloopDatum
        End Get
        Set(value As Date)
            _AfloopDatum = value
        End Set
    End Property

    Public ReadOnly Property UniqueKey As String
        Get
            Return GroepsCode & "_" & IngangsDatum.ToShortDateString & "_" & AfloopDatum.ToShortDateString
        End Get
    End Property


    Public Overrides Function ToString() As String
        Dim sOut As String = GroepNaam & vbTab & IngangsDatum & vbTab
        If AfloopDatum < New Date(1900, 1, 1) Then
            Return sOut & vbCrLf
        Else
            Return sOut & AfloopDatum & vbCrLf
        End If
    End Function

    Private Function GetDbInfo() As Boolean
        Dim Sql As String = "select * from  OST_CGROEP where Groep ='" & GroepNaam & "'"
        Dim reader As Oracle.ManagedDataAccess.Client.OracleDataReader = dbOsiris.oracleQueryUitvoeren(Sql)
        If IsNothing(reader) Then Return False
        If reader.HasRows = False Then Return False

        Return True
    End Function
    '    Select Case groep, ingangsdatum, afloopdatum from ost_sgroep_student where studentnummer=329879;
End Class
