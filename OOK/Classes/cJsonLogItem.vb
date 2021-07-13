Public Class cJsonLogItem
    Private _datumTijd As Date
	Private _omgeving As String = ""
	Private _request As String = ""
	Private _status_code As String = ""
	Private _omschrijving As String = ""
    Private _persoonId As String = ""
    Private _response As String = ""
    Private _actie As String = ""
    Private _gelukt As String = "J"


    Public ReadOnly Property DatumTijd As Date
        Get
            Return Now()
        End Get

    End Property

    Public ReadOnly Property Omgeving As String
        Get
            Return i.OmgevingsNaam
        End Get

    End Property

    Public Property Request As String
        Get
            Return _request
        End Get
        Set(value As String)
            _request = value
        End Set
    End Property

    Public Property Status_code As String
        Get
            Return _status_code
        End Get
        Set(value As String)
            _status_code = value
        End Set
    End Property

    Public Property Omschrijving As String
        Get
            Return _omschrijving
        End Get
        Set(value As String)
            _omschrijving = value
        End Set
    End Property

    Public Property PersoonId As String
        Get
            Return _persoonId
        End Get
        Set(value As String)
            _persoonId = value
        End Set
    End Property

    Public Property Response As String
        Get
            Return _response
        End Get
        Set(value As String)
            _response = value
        End Set
    End Property

    Public Property Actie As String
        Get
            Return _actie
        End Get
        Set(value As String)
            _actie = value
        End Set
    End Property

    Public Property Gelukt As String
        Get
            Return _gelukt
        End Get
        Set(value As String)
            _gelukt = value
        End Set
    End Property

    Public Sub New(sActie As String, sRequest As String, sOmschrijving As String, sPersoonId As String, sResponse As String, sStatusCode As String)

        Me.Request = sRequest
        Me.Omschrijving = sOmschrijving
        Me.PersoonId = sPersoonId
        Me.Response = sResponse
        Me.Actie = sActie
        Me.Status_code = sStatusCode

    End Sub


    Public Sub New()

    End Sub
    Public Function Write2database() As Boolean
        If l.UitgebreideLogging Then l.LOGTHIS("Soap call logitem wegschrijven")

        Dim cmd As New OleDb.OleDbCommand
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "db_oo.update_call_log"

        With cmd.Parameters
            'datum tijd gaat automatisch via de stored procedure in de database
            .AddWithValue("@omgeving", i.OmgevingsNaam)
            .AddWithValue("@request", Request)
            .AddWithValue("@status_code", Status_code)
            .AddWithValue("@response", Response)
            .AddWithValue("@omschrijving", Omschrijving)
            .AddWithValue("@persoonId", PersoonId)
            .AddWithValue("@actie", Actie)
            .AddWithValue("@gelukt", Gelukt)
        End With

        If dbMiddleWare.sqlCheckConnectionState = True Then
            Try
                cmd.Connection = dbMiddleWare.conSQL
                cmd.ExecuteNonQuery()

            Catch ex As Exception
                Return False
            End Try

        Else
            Return False
        End If



        Return False
    End Function
End Class
