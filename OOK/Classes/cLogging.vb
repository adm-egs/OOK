Option Explicit On
Imports System.IO

Public Class cLogging
    'class voor logging. Kan naar textbox loggen en naar file tegelijk


    Public ts As StreamWriter
    Private _Naam As String
    Private _textbox As Object
    Private _bolTextboxAanwezig As Boolean = False
    Private _frm As frmMain
    Private _UitgebreideLogging As Boolean = False
    Private _logLevelTrigger As Integer = 1



    Public Sub New(ByVal NaamBestand As String)
        'openen / aanmaken van een logbestand
        Dim strLogBestand As String = NaamBestand
        _Naam = NaamBestand
        'strLogBestand = _Naam & "-log"
        Dim fi As New FileInfo(strLogBestand)
        If fi.Exists Then
            If fi.Length > 5000000 Then
                Try   'groter dan 10 Mbits
                    Dim dt As String
                    dt = Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & "_" & Now.Hour.ToString & "-" & Now.Minute & "-"
                    If System.IO.Directory.Exists(fi.DirectoryName & "\old-logs") = False Then
                        Try
                            System.IO.Directory.CreateDirectory(fi.DirectoryName & "\old-logs")
                        Catch ex3 As Exception

                        End Try
                    End If
                    fi.MoveTo(fi.DirectoryName & "\old-logs\" & "LOG_tot_" & dt & ".txt")
                Catch ex As Exception

                End Try


            End If
        End If
        Try
            ts = New StreamWriter(strLogBestand, True)
        Catch ex As Exception
            Try
                ts = New StreamWriter(strLogBestand & "-1.txt", True)
            Catch ex2 As Exception
                Try
                    ts = New StreamWriter(strLogBestand & "-2.txt", True)
                Catch ex3 As Exception
                    Try
                        ts = New StreamWriter(strLogBestand & "-3.txt", True)
                    Catch ex4 As Exception

                    End Try
                End Try
            End Try
        End Try

    End Sub

    Public Sub New(ByVal Naambestand As String, ByVal eigenNaam As Boolean)
        If eigenNaam = True Then
            ts = New StreamWriter(Naambestand, True)
        Else
            Dim strLogBestand As String
            _Naam = Naambestand
            strLogBestand = _Naam & "-log"
            ts = New StreamWriter(Application.StartupPath & "\" & strLogBestand & ".txt", True)
        End If
    End Sub

    Public Sub New(ByVal NaamBestand As String, ByVal TextboxControl As TextBox)
        'orginele constructor aanroepen
        Me.New(NaamBestand)

        'controleren of er een textbox control is meegegeven
        If TypeOf TextboxControl Is TextBox Then
            _bolTextboxAanwezig = True
            _textbox = TextboxControl

        End If

    End Sub

    Public Sub New(ByVal Naambestand As String, ByVal frm As frmMain)
        Me.New(Naambestand)

        'controleren of er een textbox control is meegegeven

        _bolTextboxAanwezig = False
        _frm = frm

    End Sub
    Public Sub New(ByVal Naambestand As String, ByVal frm As frmMain, ByVal TextBoxControl As TextBox)
        Me.New(Naambestand)

        'controleren of er een textbox control is meegegeven

        _bolTextboxAanwezig = True
        _frm = frm
        _textbox = TextBoxControl

    End Sub

    Public Property UitgebreideLogging As Boolean
        Get
            Return _UitgebreideLogging
        End Get
        Set(value As Boolean)
            _UitgebreideLogging = value
        End Set
    End Property

    Public Property LogLevelTrigger As Integer
        Get
            Return _logLevelTrigger
        End Get
        Set(value As Integer)
            _logLevelTrigger = value
        End Set
    End Property

    Sub LOGTHIS(ByRef strTekst As String, Optional ByVal level As Integer = 1)
        'opgegeven tekst loggen
        Try
            If level < LogLevelTrigger Then
                Exit Sub
            End If

            Dim GeefGebruikersNaam As String
            Dim strLogTekst As String
            'routine schrijft de opgegeven tekst weg in het logbestand
            'automatisch worden de naam en tijd toegevoegd aan de melding

            GeefGebruikersNaam = Environment.UserName.ToString ' gebruikersnaam binnen windows opvragen
            strLogTekst = level.ToString & ")-" & GeefGebruikersNaam & "   " & Now.ToString & vbTab & strTekst
            ts.WriteLine(strLogTekst)
            ts.Flush()
            Application.DoEvents()

            If _bolTextboxAanwezig = True Then
                'text loggen naar textbox
                _textbox.AppendText(Now.ToString & vbTab & strTekst & vbCrLf)

                _textbox.SelectionStart = Len(_textbox.Text)
                _textbox.ScrollToCaret()  'zorgen dat de laatste logvermelding getoond wordt (ivm wegscrollen vd tekst)
            Else
                '_frm.LogTekst(level.ToString & " )-" & GeefGebruikersNaam & "   " & Now.ToString & vbTab & strTekst)
                If IsNothing(_frm) = False Then
                    _frm.LogTekst(strTekst)
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

End Class

