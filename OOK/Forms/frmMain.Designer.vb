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
        Me.components = New System.ComponentModel.Container()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnInlogOsiris = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnInlogGegevensOpgeven = New System.Windows.Forms.Button()
        Me.btnDatabaseLogin = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnOsirisStudentenOpvragen = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnOpvragenStudenten = New System.Windows.Forms.Button()
        Me.btnGetToken = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.chkStudentenVandaagGechecktOverslaan = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnSyncOsiris2OO = New System.Windows.Forms.Button()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.btnCheckMutaties = New System.Windows.Forms.Button()
        Me.tvStudentData = New System.Windows.Forms.TreeView()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnStuurStudentNaarOO = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtStudentNummer = New System.Windows.Forms.TextBox()
        Me.txtLog = New System.Windows.Forms.TextBox()
        Me.frmMainStatusStrip = New System.Windows.Forms.StatusStrip()
        Me.tsConnectieOsiris = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsConnectieMiddleware = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsStudentNummer = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsCurrentState = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsLastCheckTime = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsAliveTime = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsAutoVerwerkenActie = New System.Windows.Forms.ToolStripStatusLabel()
        Me.pbMutaties = New System.Windows.Forms.ToolStripProgressBar()
        Me.btnCheckBeschikbareMutatiesOsiris = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkUitgebreideLogging = New System.Windows.Forms.CheckBox()
        Me.txtTriggerNiveau = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.timTimerCheck = New System.Windows.Forms.Timer(Me.components)
        Me.chkAutomatischChecken = New System.Windows.Forms.CheckBox()
        Me.timStoppen = New System.Windows.Forms.Timer(Me.components)
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.btnMedewerker2OO = New System.Windows.Forms.Button()
        Me.txtLetterCode = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.frmMainStatusStrip.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(12, 108)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(399, 317)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.btnInlogOsiris)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.btnInlogGegevensOpgeven)
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
        Me.TabPage1.Size = New System.Drawing.Size(391, 289)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Inlogdata"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.Color.Blue
        Me.Label11.Location = New System.Drawing.Point(73, 64)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(197, 15)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Inloggegevens Osiris en Middleware"
        '
        'btnInlogOsiris
        '
        Me.btnInlogOsiris.Location = New System.Drawing.Point(13, 59)
        Me.btnInlogOsiris.Name = "btnInlogOsiris"
        Me.btnInlogOsiris.Size = New System.Drawing.Size(50, 25)
        Me.btnInlogOsiris.TabIndex = 9
        Me.btnInlogOsiris.Text = "Inlog"
        Me.btnInlogOsiris.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.Blue
        Me.Label10.Location = New System.Drawing.Point(73, 22)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(178, 15)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Inloggegevens Onderwijs Online"
        '
        'btnInlogGegevensOpgeven
        '
        Me.btnInlogGegevensOpgeven.Location = New System.Drawing.Point(13, 17)
        Me.btnInlogGegevensOpgeven.Name = "btnInlogGegevensOpgeven"
        Me.btnInlogGegevensOpgeven.Size = New System.Drawing.Size(50, 25)
        Me.btnInlogGegevensOpgeven.TabIndex = 7
        Me.btnInlogGegevensOpgeven.Text = "Inlog"
        Me.btnInlogGegevensOpgeven.UseVisualStyleBackColor = True
        '
        'btnDatabaseLogin
        '
        Me.btnDatabaseLogin.Location = New System.Drawing.Point(176, 175)
        Me.btnDatabaseLogin.Name = "btnDatabaseLogin"
        Me.btnDatabaseLogin.Size = New System.Drawing.Size(75, 23)
        Me.btnDatabaseLogin.TabIndex = 6
        Me.btnDatabaseLogin.UseVisualStyleBackColor = True
        Me.btnDatabaseLogin.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(236, 124)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(152, 15)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Opvragen studenten OSIRIS"
        '
        'btnOsirisStudentenOpvragen
        '
        Me.btnOsirisStudentenOpvragen.Location = New System.Drawing.Point(176, 124)
        Me.btnOsirisStudentenOpvragen.Name = "btnOsirisStudentenOpvragen"
        Me.btnOsirisStudentenOpvragen.Size = New System.Drawing.Size(35, 21)
        Me.btnOsirisStudentenOpvragen.TabIndex = 4
        Me.btnOsirisStudentenOpvragen.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(236, 37)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 15)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Opvragen token" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.Label2.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(236, 79)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 15)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Opvragen studenten OO"
        Me.Label1.Visible = False
        '
        'btnOpvragenStudenten
        '
        Me.btnOpvragenStudenten.Location = New System.Drawing.Point(176, 75)
        Me.btnOpvragenStudenten.Name = "btnOpvragenStudenten"
        Me.btnOpvragenStudenten.Size = New System.Drawing.Size(35, 23)
        Me.btnOpvragenStudenten.TabIndex = 1
        Me.btnOpvragenStudenten.UseVisualStyleBackColor = True
        Me.btnOpvragenStudenten.Visible = False
        '
        'btnGetToken
        '
        Me.btnGetToken.Location = New System.Drawing.Point(176, 33)
        Me.btnGetToken.Name = "btnGetToken"
        Me.btnGetToken.Size = New System.Drawing.Size(35, 23)
        Me.btnGetToken.TabIndex = 0
        Me.btnGetToken.UseVisualStyleBackColor = True
        Me.btnGetToken.Visible = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.chkStudentenVandaagGechecktOverslaan)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.btnSyncOsiris2OO)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(391, 289)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Sync instellingen"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'chkStudentenVandaagGechecktOverslaan
        '
        Me.chkStudentenVandaagGechecktOverslaan.AutoSize = True
        Me.chkStudentenVandaagGechecktOverslaan.Location = New System.Drawing.Point(16, 54)
        Me.chkStudentenVandaagGechecktOverslaan.Name = "chkStudentenVandaagGechecktOverslaan"
        Me.chkStudentenVandaagGechecktOverslaan.Size = New System.Drawing.Size(289, 19)
        Me.chkStudentenVandaagGechecktOverslaan.TabIndex = 6
        Me.chkStudentenVandaagGechecktOverslaan.Text = "Studenten vandaag al eerder gechecked overslaan"
        Me.chkStudentenVandaagGechecktOverslaan.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.Red
        Me.Label8.Location = New System.Drawing.Point(65, 27)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(199, 15)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "OO bijwerken met alle data uit Osiris"
        '
        'btnSyncOsiris2OO
        '
        Me.btnSyncOsiris2OO.Location = New System.Drawing.Point(17, 25)
        Me.btnSyncOsiris2OO.Name = "btnSyncOsiris2OO"
        Me.btnSyncOsiris2OO.Size = New System.Drawing.Size(42, 19)
        Me.btnSyncOsiris2OO.TabIndex = 0
        Me.btnSyncOsiris2OO.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.btnCheckMutaties)
        Me.TabPage3.Controls.Add(Me.tvStudentData)
        Me.TabPage3.Controls.Add(Me.Label6)
        Me.TabPage3.Controls.Add(Me.Label5)
        Me.TabPage3.Controls.Add(Me.btnStuurStudentNaarOO)
        Me.TabPage3.Location = New System.Drawing.Point(4, 24)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(391, 289)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Osiris"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'btnCheckMutaties
        '
        Me.btnCheckMutaties.Location = New System.Drawing.Point(15, 38)
        Me.btnCheckMutaties.Name = "btnCheckMutaties"
        Me.btnCheckMutaties.Size = New System.Drawing.Size(42, 19)
        Me.btnCheckMutaties.TabIndex = 5
        Me.btnCheckMutaties.UseVisualStyleBackColor = True
        '
        'tvStudentData
        '
        Me.tvStudentData.Location = New System.Drawing.Point(6, 65)
        Me.tvStudentData.Name = "tvStudentData"
        Me.tvStudentData.Size = New System.Drawing.Size(369, 213)
        Me.tvStudentData.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(63, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(152, 15)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Student naar OO verzenden"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(63, 40)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(108, 15)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Student data tonen"
        '
        'btnStuurStudentNaarOO
        '
        Me.btnStuurStudentNaarOO.Location = New System.Drawing.Point(15, 13)
        Me.btnStuurStudentNaarOO.Name = "btnStuurStudentNaarOO"
        Me.btnStuurStudentNaarOO.Size = New System.Drawing.Size(42, 19)
        Me.btnStuurStudentNaarOO.TabIndex = 1
        Me.btnStuurStudentNaarOO.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(83, 67)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 15)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Studentnummer"
        '
        'txtStudentNummer
        '
        Me.txtStudentNummer.Location = New System.Drawing.Point(19, 64)
        Me.txtStudentNummer.Name = "txtStudentNummer"
        Me.txtStudentNummer.Size = New System.Drawing.Size(58, 23)
        Me.txtStudentNummer.TabIndex = 0
        '
        'txtLog
        '
        Me.txtLog.Location = New System.Drawing.Point(429, 12)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtLog.Size = New System.Drawing.Size(359, 413)
        Me.txtLog.TabIndex = 0
        '
        'frmMainStatusStrip
        '
        Me.frmMainStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsConnectieOsiris, Me.tsConnectieMiddleware, Me.tsStudentNummer, Me.tsCurrentState, Me.tsLastCheckTime, Me.tsAliveTime, Me.tsAutoVerwerkenActie, Me.pbMutaties})
        Me.frmMainStatusStrip.Location = New System.Drawing.Point(0, 428)
        Me.frmMainStatusStrip.Name = "frmMainStatusStrip"
        Me.frmMainStatusStrip.Size = New System.Drawing.Size(842, 22)
        Me.frmMainStatusStrip.TabIndex = 1
        '
        'tsConnectieOsiris
        '
        Me.tsConnectieOsiris.AutoSize = False
        Me.tsConnectieOsiris.Name = "tsConnectieOsiris"
        Me.tsConnectieOsiris.Size = New System.Drawing.Size(119, 17)
        Me.tsConnectieOsiris.Text = "Osiris niet verbonden"
        '
        'tsConnectieMiddleware
        '
        Me.tsConnectieMiddleware.AutoSize = False
        Me.tsConnectieMiddleware.Name = "tsConnectieMiddleware"
        Me.tsConnectieMiddleware.Size = New System.Drawing.Size(152, 17)
        Me.tsConnectieMiddleware.Text = "Middleware niet verbonden"
        '
        'tsStudentNummer
        '
        Me.tsStudentNummer.AutoSize = False
        Me.tsStudentNummer.Name = "tsStudentNummer"
        Me.tsStudentNummer.Size = New System.Drawing.Size(82, 17)
        Me.tsStudentNummer.Text = "Student nr"
        '
        'tsCurrentState
        '
        Me.tsCurrentState.AutoSize = False
        Me.tsCurrentState.Name = "tsCurrentState"
        Me.tsCurrentState.Size = New System.Drawing.Size(87, 17)
        Me.tsCurrentState.Text = "Status software"
        '
        'tsLastCheckTime
        '
        Me.tsLastCheckTime.AutoSize = False
        Me.tsLastCheckTime.Name = "tsLastCheckTime"
        Me.tsLastCheckTime.Size = New System.Drawing.Size(135, 17)
        Me.tsLastCheckTime.Text = "Last Check: not checked"
        '
        'tsAliveTime
        '
        Me.tsAliveTime.AutoSize = False
        Me.tsAliveTime.ForeColor = System.Drawing.Color.Blue
        Me.tsAliveTime.Name = "tsAliveTime"
        Me.tsAliveTime.Size = New System.Drawing.Size(34, 17)
        Me.tsAliveTime.Text = "00:00"
        '
        'tsAutoVerwerkenActie
        '
        Me.tsAutoVerwerkenActie.Name = "tsAutoVerwerkenActie"
        Me.tsAutoVerwerkenActie.Size = New System.Drawing.Size(17, 17)
        Me.tsAutoVerwerkenActie.Text = "--"
        '
        'pbMutaties
        '
        Me.pbMutaties.Name = "pbMutaties"
        Me.pbMutaties.Size = New System.Drawing.Size(100, 16)
        '
        'btnCheckBeschikbareMutatiesOsiris
        '
        Me.btnCheckBeschikbareMutatiesOsiris.Location = New System.Drawing.Point(19, 14)
        Me.btnCheckBeschikbareMutatiesOsiris.Name = "btnCheckBeschikbareMutatiesOsiris"
        Me.btnCheckBeschikbareMutatiesOsiris.Size = New System.Drawing.Size(41, 27)
        Me.btnCheckBeschikbareMutatiesOsiris.TabIndex = 2
        Me.btnCheckBeschikbareMutatiesOsiris.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.Red
        Me.Label7.Location = New System.Drawing.Point(83, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(121, 15)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Check Mutaties Osiris"
        '
        'chkUitgebreideLogging
        '
        Me.chkUitgebreideLogging.AutoSize = True
        Me.chkUitgebreideLogging.Location = New System.Drawing.Point(296, 20)
        Me.chkUitgebreideLogging.Name = "chkUitgebreideLogging"
        Me.chkUitgebreideLogging.Size = New System.Drawing.Size(131, 19)
        Me.chkUitgebreideLogging.TabIndex = 6
        Me.chkUitgebreideLogging.Text = "Uitgebreide logging"
        Me.chkUitgebreideLogging.UseVisualStyleBackColor = True
        Me.chkUitgebreideLogging.Visible = False
        '
        'txtTriggerNiveau
        '
        Me.txtTriggerNiveau.Location = New System.Drawing.Point(296, 45)
        Me.txtTriggerNiveau.Name = "txtTriggerNiveau"
        Me.txtTriggerNiveau.Size = New System.Drawing.Size(38, 23)
        Me.txtTriggerNiveau.TabIndex = 7
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(340, 48)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(77, 15)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "triggerniveau"
        '
        'timTimerCheck
        '
        '
        'chkAutomatischChecken
        '
        Me.chkAutomatischChecken.AutoSize = True
        Me.chkAutomatischChecken.Location = New System.Drawing.Point(83, 38)
        Me.chkAutomatischChecken.Name = "chkAutomatischChecken"
        Me.chkAutomatischChecken.Size = New System.Drawing.Size(139, 19)
        Me.chkAutomatischChecken.TabIndex = 9
        Me.chkAutomatischChecken.Text = "automatisch checken"
        Me.chkAutomatischChecken.UseVisualStyleBackColor = True
        '
        'timStoppen
        '
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Label12)
        Me.TabPage4.Controls.Add(Me.txtLetterCode)
        Me.TabPage4.Controls.Add(Me.btnMedewerker2OO)
        Me.TabPage4.Location = New System.Drawing.Point(4, 24)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(391, 289)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Medewerkers"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'btnMedewerker2OO
        '
        Me.btnMedewerker2OO.Location = New System.Drawing.Point(37, 31)
        Me.btnMedewerker2OO.Name = "btnMedewerker2OO"
        Me.btnMedewerker2OO.Size = New System.Drawing.Size(56, 24)
        Me.btnMedewerker2OO.TabIndex = 0
        Me.btnMedewerker2OO.UseVisualStyleBackColor = True
        '
        'txtLetterCode
        '
        Me.txtLetterCode.Location = New System.Drawing.Point(108, 31)
        Me.txtLetterCode.Name = "txtLetterCode"
        Me.txtLetterCode.Size = New System.Drawing.Size(80, 23)
        Me.txtLetterCode.TabIndex = 1
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(34, 59)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(155, 15)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "Medewerker naar OO sturen"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(842, 450)
        Me.Controls.Add(Me.chkAutomatischChecken)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtTriggerNiveau)
        Me.Controls.Add(Me.chkUitgebreideLogging)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnCheckBeschikbareMutatiesOsiris)
        Me.Controls.Add(Me.txtLog)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.frmMainStatusStrip)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.txtStudentNummer)
        Me.Name = "frmMain"
        Me.Text = "Onderwijs Online Koppeling"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.frmMainStatusStrip.ResumeLayout(False)
        Me.frmMainStatusStrip.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
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
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents btnDatabaseLogin As Button
    Friend WithEvents btnCheckMutaties As Button
    Friend WithEvents tvStudentData As TreeView
    Friend WithEvents Label4 As Label
    Friend WithEvents btnCheckBeschikbareMutatiesOsiris As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents chkUitgebreideLogging As CheckBox
    Friend WithEvents Label8 As Label
    Friend WithEvents btnSyncOsiris2OO As Button
    Friend WithEvents txtTriggerNiveau As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents chkStudentenVandaagGechecktOverslaan As CheckBox
    Friend WithEvents tsConnectieOsiris As ToolStripStatusLabel
    Friend WithEvents tsConnectieMiddleware As ToolStripStatusLabel
    Friend WithEvents tsStudentNummer As ToolStripStatusLabel
    Friend WithEvents tsCurrentState As ToolStripStatusLabel
    Friend WithEvents timTimerCheck As Timer
    Friend WithEvents chkAutomatischChecken As CheckBox
    Friend WithEvents tsLastCheckTime As ToolStripStatusLabel
    Friend WithEvents btnInlogGegevensOpgeven As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents btnInlogOsiris As Button
    Friend WithEvents timStoppen As Timer
    Friend WithEvents tsAliveTime As ToolStripStatusLabel
    Friend WithEvents tsAutoVerwerkenActie As ToolStripStatusLabel
    Friend WithEvents pbMutaties As ToolStripProgressBar
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents txtLetterCode As TextBox
    Friend WithEvents btnMedewerker2OO As Button
    Friend WithEvents Label12 As Label
End Class
