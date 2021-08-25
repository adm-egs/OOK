Imports System.Windows.Forms

Public Class frmInlogOsiris

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If Me.txtOsirisConnectString.Text = "" Then
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Close()
        End If



        If Me.txtMiddlewareConnectString.Text = "" Then
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Close()
        End If

        'opslaan data
        ini.WriteString("connect", "&^2345", i.Encrypt(Me.txtOsirisConnectString.Text))
        ini.WriteString("connect", "562-0", i.Encrypt(Me.txtMiddlewareConnectString.Text))


        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmInlogOsiris_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
