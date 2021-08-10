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
        Me.btnDatabaseLogin = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnOsirisStudentenOpvragen = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnOpvragenStudenten = New System.Windows.Forms.Button()
        Me.btnGetToken = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.txtLog = New System.Windows.Forms.TextBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.btnCheckMutaties = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnStuurStudentNaarOO = New System.Windows.Forms.Button()
        Me.txtStudentNummer = New System.Windows.Forms.TextBox()
        Me.tvStudentData = New System.Windows.Forms.TreeView()
        Me.frmMainStatusStrip = New System.Windows.Forms.StatusStrip()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(12, 93)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(399, 314)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.btnDatabaseLogin)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.btnOsirisStudentenOpvragen)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.btnOpvragenStudenten)
        Me.TabPage1.Controls.Add(Me.btnGetToken)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(391, 286)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Onderwijs Online"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'btnDatabaseLogin
        '
        Me.btnDatabaseLogin.Location = New System.Drawing.Point(27, 172)
        Me.btnDatabaseLogin.Name = "btnDatabaseLogin"
        Me.btnDatabaseLogin.Size = New System.Drawing.Size(75, 23)
        Me.btnDatabaseLogin.TabIndex = 6
        Me.btnDatabaseLogin.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(87, 121)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(152, 15)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Opvragen studenten OSIRIS"
        '
        'btnOsirisStudentenOpvragen
        '
        Me.btnOsirisStudentenOpvragen.Location = New System.Drawing.Point(27, 121)
        Me.btnOsirisStudentenOpvragen.Name = "btnOsirisStudentenOpvragen"
        Me.btnOsirisStudentenOpvragen.Size = New System.Drawing.Size(35, 21)
        Me.btnOsirisStudentenOpvragen.TabIndex = 4
        Me.btnOsirisStudentenOpvragen.UseVisualStyleBackColor = True
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
        Me.Label1.Location = New System.Drawing.Point(87, 76)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 15)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Opvragen studenten OO"
        '
        'btnOpvragenStudenten
        '
        Me.btnOpvragenStudenten.Location = New System.Drawing.Point(27, 72)
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
        Me.TabPage2.Size = New System.Drawing.Size(391, 286)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Log"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'txtLog
        '
        Me.txtLog.Location = New System.Drawing.Point(18, 15)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtLog.Size = New System.Drawing.Size(359, 245)
        Me.txtLog.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.btnCheckMutaties)
        Me.TabPage3.Controls.Add(Me.Label6)
        Me.TabPage3.Controls.Add(Me.Label5)
        Me.TabPage3.Controls.Add(Me.Label4)
        Me.TabPage3.Controls.Add(Me.btnStuurStudentNaarOO)
        Me.TabPage3.Controls.Add(Me.txtStudentNummer)
        Me.TabPage3.Location = New System.Drawing.Point(4, 24)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(391, 286)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Osiris"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'btnCheckMutaties
        '
        Me.btnCheckMutaties.Location = New System.Drawing.Point(49, 136)
        Me.btnCheckMutaties.Name = "btnCheckMutaties"
        Me.btnCheckMutaties.Size = New System.Drawing.Size(42, 23)
        Me.btnCheckMutaties.TabIndex = 5
        Me.btnCheckMutaties.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(49, 66)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(152, 15)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Student naar OO verzenden"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(107, 140)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 15)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Student data"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(216, 35)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 15)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Studentnummer"
        '
        'btnStuurStudentNaarOO
        '
        Me.btnStuurStudentNaarOO.Location = New System.Drawing.Point(49, 27)
        Me.btnStuurStudentNaarOO.Name = "btnStuurStudentNaarOO"
        Me.btnStuurStudentNaarOO.Size = New System.Drawing.Size(42, 23)
        Me.btnStuurStudentNaarOO.TabIndex = 1
        Me.btnStuurStudentNaarOO.UseVisualStyleBackColor = True
        '
        'txtStudentNummer
        '
        Me.txtStudentNummer.Location = New System.Drawing.Point(109, 28)
        Me.txtStudentNummer.Name = "txtStudentNummer"
        Me.txtStudentNummer.Size = New System.Drawing.Size(101, 23)
        Me.txtStudentNummer.TabIndex = 0
        '
        'tvStudentData
        '
        Me.tvStudentData.Location = New System.Drawing.Point(429, 32)
        Me.tvStudentData.Name = "tvStudentData"
        Me.tvStudentData.Size = New System.Drawing.Size(371, 371)
        Me.tvStudentData.TabIndex = 7
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
        Me.Controls.Add(Me.tvStudentData)
        Me.Controls.Add(Me.frmMainStatusStrip)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frmMain"
        Me.Text = "Onderwijs Online Koppeling"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
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
    Friend WithEvents Label3 As Label
    Friend WithEvents btnOsirisStudentenOpvragen As Button
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents btnStuurStudentNaarOO As Button
    Friend WithEvents txtStudentNummer As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents btnDatabaseLogin As Button
    Friend WithEvents btnCheckMutaties As Button
    Friend WithEvents tvStudentData As TreeView
End Class
