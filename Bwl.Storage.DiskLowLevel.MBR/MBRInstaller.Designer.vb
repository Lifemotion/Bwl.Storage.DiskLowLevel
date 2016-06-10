<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MBRInstaller
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
        Me.tDriveNumber = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbLoaders = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.bInstallMBR = New System.Windows.Forms.Button()
        Me.tbProgram = New System.Windows.Forms.TextBox()
        Me.bSelectProgram = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.bInstallProgram = New System.Windows.Forms.Button()
        Me.openFile = New System.Windows.Forms.OpenFileDialog()
        Me.lResult = New System.Windows.Forms.Label()
        CType(Me.tDriveNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tDriveNumber
        '
        Me.tDriveNumber.Location = New System.Drawing.Point(12, 12)
        Me.tDriveNumber.Name = "tDriveNumber"
        Me.tDriveNumber.Size = New System.Drawing.Size(71, 20)
        Me.tDriveNumber.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(88, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Физический диск"
        '
        'cbLoaders
        '
        Me.cbLoaders.FormattingEnabled = True
        Me.cbLoaders.Location = New System.Drawing.Point(12, 57)
        Me.cbLoaders.Name = "cbLoaders"
        Me.cbLoaders.Size = New System.Drawing.Size(335, 21)
        Me.cbLoaders.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Загрузчики MBR"
        '
        'bInstallMBR
        '
        Me.bInstallMBR.Location = New System.Drawing.Point(12, 84)
        Me.bInstallMBR.Name = "bInstallMBR"
        Me.bInstallMBR.Size = New System.Drawing.Size(104, 23)
        Me.bInstallMBR.TabIndex = 4
        Me.bInstallMBR.Text = "Установить"
        Me.bInstallMBR.UseVisualStyleBackColor = True
        '
        'tbProgram
        '
        Me.tbProgram.Location = New System.Drawing.Point(12, 132)
        Me.tbProgram.Name = "tbProgram"
        Me.tbProgram.Size = New System.Drawing.Size(335, 20)
        Me.tbProgram.TabIndex = 5
        '
        'bSelectProgram
        '
        Me.bSelectProgram.Location = New System.Drawing.Point(12, 158)
        Me.bSelectProgram.Name = "bSelectProgram"
        Me.bSelectProgram.Size = New System.Drawing.Size(104, 23)
        Me.bSelectProgram.TabIndex = 6
        Me.bSelectProgram.Text = "Выбрать"
        Me.bSelectProgram.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 113)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(178, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Программа размещаемая в MBR"
        '
        'bInstallProgram
        '
        Me.bInstallProgram.Location = New System.Drawing.Point(122, 158)
        Me.bInstallProgram.Name = "bInstallProgram"
        Me.bInstallProgram.Size = New System.Drawing.Size(104, 23)
        Me.bInstallProgram.TabIndex = 8
        Me.bInstallProgram.Text = "Установить"
        Me.bInstallProgram.UseVisualStyleBackColor = True
        '
        'lResult
        '
        Me.lResult.AutoSize = True
        Me.lResult.Location = New System.Drawing.Point(12, 197)
        Me.lResult.Name = "lResult"
        Me.lResult.Size = New System.Drawing.Size(0, 13)
        Me.lResult.TabIndex = 9
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(359, 219)
        Me.Controls.Add(Me.lResult)
        Me.Controls.Add(Me.bInstallProgram)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.bSelectProgram)
        Me.Controls.Add(Me.tbProgram)
        Me.Controls.Add(Me.bInstallMBR)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cbLoaders)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tDriveNumber)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.Text = "Установка загрузчика и программ в MBR"
        CType(Me.tDriveNumber, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tDriveNumber As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbLoaders As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents bInstallMBR As System.Windows.Forms.Button
    Friend WithEvents tbProgram As System.Windows.Forms.TextBox
    Friend WithEvents bSelectProgram As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents bInstallProgram As System.Windows.Forms.Button
    Friend WithEvents openFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lResult As System.Windows.Forms.Label

End Class
