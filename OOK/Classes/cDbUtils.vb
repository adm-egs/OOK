Imports System.Data.Odbc
Imports System.Data.OleDb
Imports System.Data.SqlClient
'Imports Microsoft.data.odbc
Imports Oracle.ManagedDataAccess.Client
Imports System.Security


Public Class cDbUtils
#Region "Properties"


    ' Private _con As New OdbcConnection 'OleDbConnection                 'object om verbinding met de Oracle database te maken
    Private _conOra As New OracleConnection 'object om verbinding met de Oracle database te maken via OracleTools
    Private _conSQL As New OleDbConnection  'object om verbinding met de SQL database te maken via OLEDB
    Private _conOraString As String = ""
    Private _sqlConString As String = ""


    Public ReadOnly Property conOra As OracleConnection ' OleDbConnection
        Get
            Return _conOra
        End Get
    End Property

    Public Property oraConString As String
        'de verbindingsstring om met de database te connecten
        'komt uit de ini file
        Get
            Return _conOraString
        End Get
        Set(value As String)
            _conOraString = value
        End Set
    End Property

    Public Property conSQL As OleDbConnection
        Get
            Return _conSQL
        End Get
        Set(value As OleDbConnection)
            _conSQL = value
        End Set
    End Property

    Public Property sqlConstring As String
        Get
            Return _sqlConString
        End Get
        Set(value As String)
            _sqlConString = value
        End Set
    End Property
#End Region

#Region "DB connect"
#Region "Oracle"


    Public Function oracleCheckConnectionState() As Boolean
        'controleren of er een verbinding naar de database beschikbaar is
        If conOra.State <> ConnectionState.Open Then
            Try
                conOra.ConnectionString = oraConString
                conOra.Open()
                l.LOGTHIS("Verbinding met Oracle server geopend")
            Catch ex As Exception
                Debug.Print(ex.Message)
                Debug.Print(oraConString)

                l.LOGTHIS("Kan de Oracle database niet openen")
                If l.UitgebreideLogging Then
                    l.LOGTHIS(oraConString)
                End If
                Throw New Exception("Kan de database niet openen")
                Return False
            End Try
        End If
        Return True
    End Function

    Public Function oracleQueryUitvoeren(sql As String) As OracleDataReader 'OdbcDataReader 'OleDbDataReader
        'egs 2021-03-10
        'query uitvoeren en teruggeven als datareader
        'niet gelukt -> nothing terug

        'Dim cmd As OdbcCommand 'OleDbCommand
        'Dim reader As OdbcDataReader 'OleDbDataReader
        Dim cmd As OracleCommand
        Dim reader As OracleDataReader

        If oracleCheckConnectionState() = True Then       'verbinding is aanwezig
            Try
                'cmd = New OdbcCommand(sql, Con) 'OleDbCommand(sql, Con)
                cmd = New OracleCommand(sql, conOra)
                reader = cmd.ExecuteReader
                Return reader

            Catch ex As Exception
                If l.UitgebreideLogging Then
                    l.LOGTHIS("Fout bij query uitvoeren : " & ex.Message)
                    l.LOGTHIS(sql)
                End If

                Return Nothing
            End Try
        End If
        Return Nothing

    End Function

    Function oraSafeGetDecimal(rd As OracleDataReader, veld As String) As Decimal

        Dim colIndex As Integer = rd.GetOrdinal(veld)
        If Not rd.IsDBNull(colIndex) Then
            Try
                Return CDec(rd.GetValue(colIndex))
            Catch ex As Exception
                Return rd.GetDecimal(colIndex)

            End Try
        Else
            Return 0
        End If

    End Function

    Function oraSafeGetString(rd As OracleDataReader, veld As String) As String

        Dim colindex As Integer = rd.GetOrdinal(veld)
        If Not rd.IsDBNull(colindex) Then
            Try
                Return rd.GetString(colindex)
            Catch ex As Exception
                Dim sType As String = rd.GetFieldType(colindex).FullName
                Select Case sType
                    Case "System.Decimal"
                        Dim sValue As Decimal = rd.GetDecimal(colindex)
                        Return sValue.ToString
                    Case "System.Int32"
                        Dim sValue As System.Int32 = rd.GetInt32(colindex)
                        Return CStr(sValue)
                    Case Else
                        l.LOGTHIS("Fout bij opvragen waarden van het type " & sType)
                        Return String.Empty
                End Select
            End Try

        Else
            Return String.Empty
        End If

    End Function

    Function oraGetSafeDate(rd As OracleDataReader, veld As String) As Date
        Dim dtHulp As Date
        Dim colIndex As Integer = rd.GetOrdinal(veld)

        Try
            If Not rd.IsDBNull(colIndex) Then
                dtHulp = rd.GetDateTime(colIndex)
                If dtHulp > New Date(2038, 1, 1) Then
                    Return New Date(2038, 1, 1)
                Else
                    Return rd.GetDateTime(colIndex)
                End If
            Else
                Return New Date(1900, 1, 1)
            End If
        Catch ex As Exception
            Try
                Dim sdate As String = rd.GetString(colIndex)
                dtHulp = CDate(Format(sdate))
                If dtHulp > New Date(2038, 1, 1) Then
                    Return New Date(2038, 1, 1)
                Else
                    Return dtHulp
                End If
                Return CDate(Format(sdate))

            Catch ex2 As Exception
                Debug.Print(rd.GetDataTypeName(colIndex))
                Return Nothing
            End Try

        End Try


    End Function

    Function oraGetSafeDate2(rd As OracleDataReader, veld As String) As Date
        Dim dtHulp As Date
        Dim colIndex As Integer = rd.GetOrdinal(veld)

        Try
            Dim sdate As String = rd.GetString(colIndex)
            dtHulp = CDate(Format(sdate))
            If dtHulp > New Date(2038, 1, 1) Then
                Return New Date(2038, 1, 1)
            Else
                Return dtHulp
            End If
            Return CDate(Format(sdate))

        Catch ex2 As Exception
            Return oraGetSafeDate(rd, veld)

        End Try
    End Function
#End Region

#Region "SQL server"

    Public Function sqlCheckConnectionState() As Boolean
        'controleren of er een verbinding naar de database beschikbaar is
        If conSQL.State <> ConnectionState.Open Then
            Try
                conSQL.ConnectionString = sqlConstring
                conSQL.Open()
                l.LOGTHIS("Verbinding met SQL server geopend")
            Catch ex As Exception
                l.LOGTHIS("Kan de database niet openen")
                l.LOGTHIS(sqlConstring)
                Throw New Exception("Kan de database niet openen")
                Return False
            End Try
        End If
        Return True
    End Function

    Public Function sqlQueryUitvoeren(sql As String) As OleDbDataReader
        'egs 2021-03-10
        'query uitvoeren en teruggeven als datareader
        'niet gelukt -> nothing terug

        Dim cmd As OleDbCommand
        Dim reader As OleDbDataReader
        If sqlCheckConnectionState() = True Then       'verbinding is aanwezig
            Try
                cmd = New OleDbCommand(sql, conSQL)
                reader = cmd.ExecuteReader
                Return reader

            Catch ex As Exception
                If l.UitgebreideLogging Then
                    l.LOGTHIS("Fout bij query uitvoeren : " & ex.Message)
                    l.LOGTHIS(sql)
                End If

                Return Nothing
            End Try
        End If
        Return Nothing
    End Function

#End Region
#End Region

#Region "SUBS"
    Public Sub init()
        'opstarten van het object

    End Sub
#End Region
End Class
