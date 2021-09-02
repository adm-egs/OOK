Imports System.Data.OleDb
Imports System.IO
Imports System
Imports System.Windows.Forms

Public Class frmMain
    Public Delegate Sub LogTekstDelegate(ByVal tekst As String)
    Public pbStudentMutatiesVertikaal As New VerticalProgressBar
    Public Sub LogTekst(ByVal strTekst As String)
        'mogelijk maken dat Control wordt aangestuurd vanuit andere Thread ivm crossthread error
        If Me.txtLog.InvokeRequired Then
            Me.Invoke(New LogTekstDelegate(AddressOf LogTekst), strTekst)
        Else
            Me.txtLog.AppendText(Now() & vbTab & "   " & strTekst & vbCrLf)
            Me.txtLog.SelectionStart = Len(Me.txtLog.Text)
            Me.txtLog.ScrollToCaret()
        End If
    End Sub

    Public Class VerticalProgressBar
        Inherits ProgressBar
        Protected Overloads Overrides ReadOnly Property CreateParams() As CreateParams
            Get
                Dim cp As CreateParams = MyBase.CreateParams
                cp.Style = cp.Style Or &H4
                Return cp
            End Get
        End Property
    End Class
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        l = New cLogging(Application.StartupPath & "\OOK-Log.txt", Me, txtLog)
        ini = New cIniFile(Application.StartupPath & "instellingen.ini")
        i = New cInstellingen
        l.LOGTHIS("***************** System startup ******************")



        dbOsiris = New cDbUtils
        dbMiddleWare = New cDbUtils
        'dbOsiris.oraConString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.69.31)(PORT=1523))(CONNECT_DATA=(SERVICE_NAME=utrtsts)));User Id=MROMBOUTS;Password=Pretpark_draaimolen_5891;"
        'dbOsiris.oraConString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.69.31)(PORT=1523))(CONNECT_DATA=(SERVICE_NAME=utrtsts)));User Id=MBOUMW21;Password=Appelmoes_Zwemmen_93355;"
        dbOsiris.oraConString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.69.31)(PORT=1523))(CONNECT_DATA=(SERVICE_NAME=utrtsts)));User Id=MBOUMW21;Password=Appelmoes_Zwemmen_93355;"
        dbOsiris.oraConString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.69.32)(PORT=1522))(CONNECT_DATA=(SERVICE_NAME=utrprds)));User Id=MBOUMW21;Password=Multomap_Vuilnisbak_55920;"
        'ini.WriteString("connect", "&^2345", i.Decrypt(Me.txtUserName.Text))
        'ini.WriteString("connect", "562-0", i.Decrypt(Me.txtPassWord.Text))
        'ini.WriteString("connect", "_2456!", i.Decrypt(Me.txtDataBase.Text))
        'ini.WriteString("connect", "&^2345", i.Decrypt(Me.txtOsirisConnectString.Text))
        'ini.WriteString("connect", "562-0", i.Decrypt(Me.txtMiddlewareConnectString.Text))
        dbOsiris.oraConString = i.Decrypt(ini.GetString("connect", "&^2345", ""))
        'dbMiddleWare.sqlConstring = "Provider=MSOLEDBSQL;Server=SQL803354-PRD;Database=Koppel;UID=ook_user_middleware;PWD=v@!SExwku5BTOa%tWq!3"
        dbMiddleWare.sqlConstring = i.Decrypt(ini.GetString("connect", "562-0", ""))

        Me.txtStudentNummer.Text = ini.GetString("algemeen", "laststudent", "")
        Me.txtTriggerNiveau.Text = ini.GetString("algemeen", "triggerniveau", "1")
        If i.LoadDefaults = False Then
            l.LOGTHIS("Opvragen instellingen niet gelukt", 25)

        End If
        Dim sLoggingState As String = ini.GetString("algemeen", "uitgbreidelogging", "Nee")
        If sLoggingState = "Ja" Then
            Me.chkUitgebreideLogging.Checked = True
        Else
            Me.chkUitgebreideLogging.Checked = False
        End If

        'bestand stoppen.txt verwijderen
        Dim fi As New FileInfo(Application.StartupPath & "\stoppen.txt")
        If fi.Exists Then
            l.LOGTHIS("Stoppen.txt verwijderd bij opstarten", 50)
            Try
                fi.Delete()
            Catch ex As Exception

            End Try
        End If


        pbStudentMutatiesVertikaal.Left = Me.txtLog.Left + Me.txtLog.Width + 10
        pbStudentMutatiesVertikaal.Top = 200
        pbStudentMutatiesVertikaal.Width = 30
        pbStudentMutatiesVertikaal.Visible = True


        StopTimerStarten()
        CheckOpstartArgumenten()

    End Sub
    Sub CheckOpstartArgumenten()
        'controleren of er commandline argumenten gegeven zijn
        If My.Application.CommandLineArgs.Count = 0 Then Exit Sub

        Dim strArgument As String = ""
        Dim strResult As String = ""

        strArgument = My.Application.CommandLineArgs(0).ToString
        strArgument = LCase(Trim(strArgument))

        'controleren 1e argument
        If strArgument = "verwerken_mutaties" Then
            l.LOGTHIS("verwerken mutaties op basis van opstart argument")
            Me.chkAutomatischChecken.Checked = True
            'start_timer()
            Exit Sub
        End If

    End Sub
    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged

    End Sub

    Private Sub btnGetToken_Click(sender As Object, e As EventArgs) Handles btnGetToken.Click
        l.LOGTHIS(i.GetToken, 10)
    End Sub

    Private Sub btnOpvragenStudenten_Click(sender As Object, e As EventArgs) Handles btnOpvragenStudenten.Click
        i.GetStudentsOO()
    End Sub

    Private Sub btnOsirisStudentenOpvragen_Click(sender As Object, e As EventArgs) Handles btnOsirisStudentenOpvragen.Click
        i.GetStudentsOsiris()
        l.LOGTHIS(dictOsirisStudentenKeyStudentNr.Count & " studenten ingeladen", 10)
        Dim count As Long = 0
        For Each student In dictOsirisStudentenKeyStudentNr
            Debug.Print(student.Value.StudentNummer & ";" & student.Value.VolledigeNaam)
            count += 1
            'If count >= 50 Then Exit For
        Next
        'vergelijken studenten met OO

    End Sub

    Private Sub btnStuurStudentNaarOO_Click(sender As Object, e As EventArgs) Handles btnStuurStudentNaarOO.Click
        'student opvragen uit Osiris en vervolgens doorzenden naar Onderwijs Online
        '20210705 - EGS
        Dim s As New cStudentBasis
        If Me.txtStudentNummer.Text = "" Then
            MsgBox("Vul een studentnummer in")
            Exit Sub
        End If

        'student opvragen uit OO
        Dim bolGelukt As Boolean = i.GetStudentsOsiris(Me.txtStudentNummer.Text)
        Dim student As cStudentBasis
        If bolGelukt = True Then
            Try
                If dictOsirisStudentenKeyStudentNr.ContainsKey(Me.txtStudentNummer.Text) Then   'zijn de gegevens van de student bekend?
                    student = dictOsirisStudentenKeyStudentNr(Me.txtStudentNummer.Text)         'Ja
                    Try
                        If student.Send2OO() = False Then                           'versturen naar OO
                            l.LOGTHIS("Student niet naar OO gestuurd", 25)
                        End If
                    Catch ex2 As Exception
                        l.LOGTHIS("Fout bij verzenden student naar OO:" & ex2.Message, 25)
                    End Try
                End If
            Catch ex As Exception
                l.LOGTHIS("Fout bij opvragen student : " & ex.Message, 25)
            End Try
        Else
            l.LOGTHIS("Student niet gevonden in OSIRIS", 10)

        End If


    End Sub

    Private Sub txtStudentNummer_TextChanged(sender As Object, e As EventArgs) Handles txtStudentNummer.TextChanged
        ini.WriteString("algemeen", "lastStudent", Me.txtStudentNummer.Text)
    End Sub

    Private Sub btnDatabaseLogin_Click(sender As Object, e As EventArgs) Handles btnDatabaseLogin.Click

        Dim sQuery As String = "select * from koppel.db_oo.mutatielog"

        Dim rd As OleDbDataReader = dbMiddleWare.sqlQueryUitvoeren(sQuery)
        If rd.HasRows = False Then
            rd.Close()
            l.LOGTHIS("geen mutaties gevonden", 10)
            Exit Sub
        End If
        Dim count As Long = 0
        While rd.Read
            count += 1
        End While
        l.LOGTHIS(count & " mutaties gevonden", 10)

    End Sub

    Private Sub btnCheckMutaties_Click(sender As Object, e As EventArgs) Handles btnCheckMutaties.Click

        'controleren of er mutaties zijn die gedaan moeten worden
        If dictOsirisStudentenKeyStudentNr.ContainsKey(Me.txtStudentNummer.Text) Then
            dictOsirisStudentenKeyStudentNr.Remove(Me.txtStudentNummer.Text)
        End If

        i.GetStudentsOsiris(Me.txtStudentNummer.Text)       'opvragen student gegevens en toevoegen aan dictionary studenten

        If dictOsirisStudentenKeyStudentNr.ContainsKey(Me.txtStudentNummer.Text) Then
            Dim s As cStudentBasis = dictOsirisStudentenKeyStudentNr(Me.txtStudentNummer.Text)
            Me.tvStudentData.Nodes.Add(s.Osiris_node)
        Else
            MsgBox("Student niet gevonden")
        End If


    End Sub

    Private Sub btnCheckBeschikbareMutatiesOsiris_Click(sender As Object, e As EventArgs) Handles btnCheckBeschikbareMutatiesOsiris.Click
        i.CheckOpenstaandeMutaties()
    End Sub

    Private Sub chkUitgebreideLogging_CheckedChanged(sender As Object, e As EventArgs) Handles chkUitgebreideLogging.CheckedChanged
        l.UitgebreideLogging = Me.chkUitgebreideLogging.Checked
        If l.UitgebreideLogging = True Then
            ini.WriteString("algemeen", "uitgbreidelogging", "Ja")
        Else
            ini.WriteString("algemeen", "uitgbreidelogging", "Nee")
        End If

    End Sub

    Private Sub chkUitgebreideLogging_Validated(sender As Object, e As EventArgs) Handles chkUitgebreideLogging.Validated

    End Sub

    Private Sub btnSyncOsiris2OO_Click(sender As Object, e As EventArgs) Handles btnSyncOsiris2OO.Click
        'vullen lijst met studenten

        i.GetStudentsOsiris()
        l.LOGTHIS("Alle studenten checken : " & dictOsirisStudentenKeyStudentNr.Count, 25)
        Dim iniCheck As New cIniFile(Application.StartupPath & "\checklist-" & Now.Date.ToShortDateString & ".ini")
        Me.tsCurrentState.Text = ">> Working"
        For Each student In dictOsirisStudentenKeyStudentNr
            Try
                If Me.chkStudentenVandaagGechecktOverslaan.Checked = True Then
                    If iniCheck.GetString("studenten", student.Value.StudentNummer, "") = "checked" Then
                        GoTo next_rec
                    End If
                End If



                If student.Value.Send2OO() = False Then                           'versturen naar OO
                    l.LOGTHIS("Student niet naar OO gestuurd", 25)
                    iniCheck.WriteString("studenten", student.Value.StudentNummer, "not checked")
                Else
                    l.LOGTHIS("Checked " & student.Value.StudentNummer & ";" & student.Value.VolledigeNaam, 20)
                    iniCheck.WriteString("studenten", student.Value.StudentNummer, "checked")
                End If

            Catch ex2 As Exception
                l.LOGTHIS("Fout bij verzenden student naar OO:" & ex2.Message, 25)
            End Try
next_rec:
        Next
    End Sub

    Private Sub txtTriggerNiveau_TextChanged(sender As Object, e As EventArgs) Handles txtTriggerNiveau.TextChanged
        ini.WriteString("Algemeen", "triggerniveau", txtTriggerNiveau.Text)

        Try
            l.LogLevelTrigger = CInt(Me.txtTriggerNiveau.Text)
        Catch ex As Exception
            l.LogLevelTrigger = 0
        End Try


    End Sub

    Private Sub chkStudentenVandaagGechecktOverslaan_CheckedChanged(sender As Object, e As EventArgs) Handles chkStudentenVandaagGechecktOverslaan.CheckedChanged

    End Sub

    Sub start_timer()
        l.LOGTHIS("Automatisch verwerken gestart")
        Me.timTimerCheck.Interval = 5000   '5 seconden na starten wordt de 1e check gedaan
        Me.timTimerCheck.Enabled = True

    End Sub

    Sub Stop_timer()

        Me.timTimerCheck.Enabled = False
        l.LOGTHIS("Automatisch verwerken gestopt")
    End Sub
    Private Sub timTimerCheck_Tick(sender As Object, e As EventArgs) Handles timTimerCheck.Tick
        Me.timTimerCheck.Enabled = False
        'tijdelijk de timer uitzetten
        Me.UseWaitCursor = True
        If i.Stoppen = False Then
            i.CheckOpenstaandeMutaties()
        Else
            Me.UseWaitCursor = False
            Me.timTimerCheck.Interval = 60000       '1 minuut
            Me.timTimerCheck.Enabled = False
        End If

        'systeem weer activeren
        Me.UseWaitCursor = False
        Me.timTimerCheck.Interval = 60000       '1 minuut
        Me.timTimerCheck.Enabled = True
    End Sub

    Private Sub chkAutomatischChecken_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutomatischChecken.CheckedChanged
        If chkAutomatischChecken.Checked = True Then
            Me.chkAutomatischChecken.Text = "Stoppen automatisch verwerken"
            InterfaceEnable(False)
            i.Stoppen = False
            start_timer()

        Else
            Me.chkAutomatischChecken.Text = "Starten automatisch verwerken"
            InterfaceEnable(True)
            i.Stoppen = True
            Stop_timer()
        End If
    End Sub

    Private Function InterfaceEnable(bStatus As Boolean) As Boolean
        'interface aan / uit zetten tbv automatisch verwerken
        btnCheckBeschikbareMutatiesOsiris.Enabled = bStatus
        txtStudentNummer.Enabled = bStatus
        btnInlogGegevensOpgeven.Enabled = bStatus
        btnInlogOsiris.Enabled = bStatus
        btnSyncOsiris2OO.Enabled = bStatus
        chkStudentenVandaagGechecktOverslaan.Enabled = bStatus
        btnStuurStudentNaarOO.Enabled = bStatus
        btnCheckMutaties.Enabled = bStatus
        Application.DoEvents()
        Return True
    End Function
    Private Sub btnInlogGegevensOpgeven_Click(sender As Object, e As EventArgs) Handles btnInlogGegevensOpgeven.Click
        If frmInlog.ShowDialog() <> DialogResult.OK Then
            l.LOGTHIS("No change in inlogdata Onderwijs Online", 10)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnInlogOsiris.Click
        If frmInlogOsiris.ShowDialog <> DialogResult.OK Then
            l.LOGTHIS("No change in inlogdata OSIRIS", 10)
        End If
    End Sub


    Function StopTimerStarten() As Boolean
        Me.timStoppen.Interval = 5000
        Me.timStoppen.Enabled = True
        Return True
    End Function

    Private Sub timStoppen_Tick(sender As Object, e As EventArgs) Handles timStoppen.Tick
        'check of stoppen.txt bestaat
        Dim fi As New FileInfo(Application.StartupPath & "\stoppen.txt")
        If fi.Exists Then
            l.LOGTHIS("Stoppen aangevraagd via bestand stoppen.txt", 50)
            Try
                fi.Delete()
            Catch ex As Exception

            End Try
            i.Stoppen = True
            For x As Integer = 1001 To 0 Step -1
                Application.DoEvents()
                Threading.Thread.Sleep(10)
                If x Mod 100 = 0 Then
                    l.LOGTHIS("ONCD-002 >> SHUTDOWN : " & x / 100)
                End If
                If x = 0 Then End
            Next
        Else
            Me.tsAliveTime.Text = Format(Now, "HH:mm")
        End If
    End Sub

    Private Sub btnMedewerker2OO_Click(sender As Object, e As EventArgs) Handles btnMedewerker2OO.Click
        Dim sQuery As String = "Select * from  [Koppel].[db_umra_2021].[medewerkers] where personeelscode like '" & Me.txtLetterCode.Text & "'"
        Dim rd As OleDbDataReader = dbMiddleWare.sqlQueryUitvoeren(sQuery)
        If IsNothing(rd) Then
            MsgBox("niet gevonden")
            Exit Sub
        End If

        If rd.HasRows = False Then
            MsgBox("niet gevonden")
            Exit Sub
        End If



        l.LOGTHIS("medewerker " & Me.txtLetterCode.Text & " bestaat in de Middleware", 2)
        'controleren of medewerker in OO bestaat
        Dim m As cMedewerker = i.GetMedewerkerClassUitOo(Me.txtLetterCode.Text)


        If m.BekendInOO Then
            Debug.Print(m.Achternaam)
        Else
            'bestaat nog niet
            Dim m2OO As New cMedewerker         'medewerker naar onderwijs online object 
            rd.Read()
            With m2OO
                m2OO.Id = dbMiddleWare.sqlSafeGetDecimal(rd, "ID")
                m2OO.Username = dbMiddleWare.sqlSafeGetString(rd, "Personeelscode")
                m2OO.Code = m2OO.Username
                m2OO.Roepnaam = dbMiddleWare.sqlSafeGetString(rd, "Personeelscode")
                m2OO.Tussenvoegsels = dbMiddleWare.sqlSafeGetString(rd, "Personeelscode")
                m2OO.Achternaam = dbMiddleWare.sqlSafeGetString(rd, "Personeelscode")
            End With

            Dim mwId As Long = i.CreateMedewerker(m2OO)

        End If

    End Sub
End Class
