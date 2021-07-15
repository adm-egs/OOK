Imports System.Data.OleDb

Public Class frmMain
    Public Delegate Sub LogTekstDelegate(ByVal tekst As String)

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
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub frmMain_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        l = New cLogging(Application.StartupPath & "\OOK-Log.txt", Me, txtLog)
        ini = New cIniFile(Application.StartupPath & "instellingen.ini")
        i = New cInstellingen
        l.LOGTHIS("***************** System startup ******************")

        If i.LoadDefaults = False Then
            l.LOGTHIS("Opvragen instellingen niet gelukt")
            End
        End If

        dbOsiris = New cDbUtils
        dbMiddleWare = New cDbUtils
        'dbOsiris.oraConString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.69.31)(PORT=1523))(CONNECT_DATA=(SERVICE_NAME=utrtsts)));User Id=MROMBOUTS;Password=Pretpark_draaimolen_5891;"
        dbOsiris.oraConString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.69.31)(PORT=1523))(CONNECT_DATA=(SERVICE_NAME=utrtsts)));User Id=MBOUMW21;Password=Appelmoes_Zwemmen_93355;"
        dbMiddleWare.sqlConstring = "Provider=MSOLEDBSQL;Server=SQL803354-PRD;Database=Koppel;UID=ook_user_middleware;PWD=v@!SExwku5BTOa%tWq!3"
        Me.txtStudentNummer.Text = ini.GetString("algemeen", "laststudent", "")

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged

    End Sub

    Private Sub btnGetToken_Click(sender As Object, e As EventArgs) Handles btnGetToken.Click
        l.LOGTHIS(i.GetToken)
    End Sub

    Private Sub btnOpvragenStudenten_Click(sender As Object, e As EventArgs) Handles btnOpvragenStudenten.Click
        i.GetStudentsOO()
    End Sub

    Private Sub btnOsirisStudentenOpvragen_Click(sender As Object, e As EventArgs) Handles btnOsirisStudentenOpvragen.Click
        If dictOsirisStudentenKeyStudentNr.Count = 0 Then i.GetStudentsOsiris()
        l.LOGTHIS(dictOsirisStudentenKeyStudentNr.Count & " studenten ingeladen")
        'vergelijken studenten met OO

    End Sub

    Private Sub btnStuurStudentNaarOO_Click(sender As Object, e As EventArgs) Handles btnStuurStudentNaarOO.Click
        'student opvragen uit Osiris en vervolgens doorzenden naar Onderwijs Online
        '20210705 - EGS
        Dim s As New cStudent
        If Me.txtStudentNummer.Text = "" Then
            MsgBox("Vul een studentnummer in")
            Exit Sub
        End If

        'student opvragen uit OO
        Dim bolGelukt As Boolean = i.GetStudentsOsiris(Me.txtStudentNummer.Text)
        Dim student As cStudent
        If bolGelukt = True Then
            Try
                If dictOsirisStudentenKeyStudentNr.ContainsKey(Me.txtStudentNummer.Text) Then   'zijn de gegevens van de student bekend?
                    student = dictOsirisStudentenKeyStudentNr(Me.txtStudentNummer.Text)         'Ja
                    Try
                        If student.Send2OO() = False Then                           'versturen naar OO
                            l.LOGTHIS("Student niet naar OO gestuurd")
                        End If
                    Catch ex2 As Exception
                        l.LOGTHIS("Fout bij verzenden student naar OO:" & ex2.Message)
                    End Try
                End If
            Catch ex As Exception
                MsgBox("Fout bij opvragen student : " & ex.Message)
            End Try


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
            l.LOGTHIS("geen mutaties gevonden")
            Exit Sub
        End If
        Dim count As Long = 0
        While rd.Read
            count += 1
        End While
        l.LOGTHIS(count & " mutaties gevonden")
    End Sub

    Private Sub btnCheckMutaties_Click(sender As Object, e As EventArgs) Handles btnCheckMutaties.Click
        'controleren of er mutaties zijn die gedaan moeten worden

    End Sub
End Class
