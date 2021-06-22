<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnOpvragenStudenten = New System.Windows.Forms.Button()
        Me.btnGetToken = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.txtLog = New System.Windows.Forms.TextBox()
        Me.frmMainStatusStrip = New System.Windows.Forms.StatusStrip()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(41, 97)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(669, 314)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.btnOpvragenStudenten)
        Me.TabPage1.Controls.Add(Me.btnGetToken)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(661, 286)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Onderwijs Online"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(87, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Opvragen token" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(87, 94)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(115, 15)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Opvragen studenten"
        '
        'btnOpvragenStudenten
        '
        Me.btnOpvragenStudenten.Location = New System.Drawing.Point(27, 86)
        Me.btnOpvragenStudenten.Name = "btnOpvragenStudenten"
        Me.btnOpvragenStudenten.Size = New System.Drawing.Size(35, 23)
        Me.btnOpvragenStudenten.TabIndex = 1
        Me.btnOpvragenStudenten.UseVisualStyleBackColor = True
        '
        'btnGetToken
        '
        Me.btnGetToken.Location = New System.Drawing.Point(27, 30)
        Me.btnGetToken.Name = "btnGetToken"
        Me.btnGetToken.Size = New System.Drawing.Size(35, 23)
        Me.btnGetToken.TabIndex = 0
        Me.btnGetToken.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.txtLog)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(661, 286)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Logging"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'txtLog
        '
        Me.txtLog.Location = New System.Drawing.Point(28, 15)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtLog.Size = New System.Drawing.Size(600, 245)
        Me.txtLog.TabIndex = 0
        '
        'frmMainStatusStrip
        '
        Me.frmMainStatusStrip.Location = New System.Drawing.Point(0, 428)
        Me.frmMainStatusStrip.Name = "frmMainStatusStrip"
        Me.frmMainStatusStrip.Size = New System.Drawing.Size(800, 22)
        Me.frmMainStatusStrip.TabIndex = 1
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.frmMainStatusStrip)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frmMain"
        Me.Text = "Onderwijs Online Koppeling"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents txtLog As TextBox
    Friend WithEvents btnGetToken As Button
    Friend WithEvents frmMainStatusStrip As StatusStrip
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btnOpvragenStudenten As Button
End Class
