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
        ini = New cIniFile(Application.StartupPath & "\instellingen.ini")
        i = New cInstellingen
        l.LOGTHIS("****** SYSTEM STARTUP ******")

        If i.LoadDefaults = False Then
            l.LOGTHIS("Opvragen instellingen niet gelukt")
            End
        End If


    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged

    End Sub

    Private Sub btnGetToken_Click(sender As Object, e As EventArgs) Handles btnGetToken.Click
        l.LOGTHIS(i.GetToken)
    End Sub

    Private Sub btnOpvragenStudenten_Click(sender As Object, e As EventArgs) Handles btnOpvragenStudenten.Click

    End Sub
End Class
