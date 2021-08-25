Imports System.Windows.Forms

Public Class frmInlog

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If Me.txtCLientId.Text = "" Then
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Close()
        End If

        If Me.txtClientSecret.Text = "" Then
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Close()
        End If

        If Me.txtBasisURL.Text = "" Then
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Close()
        End If

        ini.WriteString("Connect", "pfq@874", i.Encrypt(Me.txtCLientId.Text))
        i.ClientId = Me.txtCLientId.Text

        ini.WriteString("Connect", "3@4XX", i.Encrypt(Me.txtClientSecret.Text))
        i.ClientSecret = Me.txtClientSecret.Text

        ini.WriteString("Connect", "+()23Z", i.Encrypt(Me.txtBasisURL.Text))

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmInlog_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
