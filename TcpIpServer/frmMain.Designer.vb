<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btnOpenClose = New System.Windows.Forms.Button()
        Me.txtIpAddress1 = New System.Windows.Forms.TextBox()
        Me.txtIpAddress2 = New System.Windows.Forms.TextBox()
        Me.txtIpAddress3 = New System.Windows.Forms.TextBox()
        Me.txtIpAddress4 = New System.Windows.Forms.TextBox()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.txtMessageRecieved = New System.Windows.Forms.TextBox()
        Me.grbConnectionSetting = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnRegisterCommands = New System.Windows.Forms.Button()
        Me.grbConnectionSetting.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOpenClose
        '
        Me.btnOpenClose.Location = New System.Drawing.Point(343, 63)
        Me.btnOpenClose.Name = "btnOpenClose"
        Me.btnOpenClose.Size = New System.Drawing.Size(75, 23)
        Me.btnOpenClose.TabIndex = 0
        Me.btnOpenClose.Text = "Open"
        Me.btnOpenClose.UseVisualStyleBackColor = True
        '
        'txtIpAddress1
        '
        Me.txtIpAddress1.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtIpAddress1.Location = New System.Drawing.Point(79, 30)
        Me.txtIpAddress1.MaxLength = 3
        Me.txtIpAddress1.Name = "txtIpAddress1"
        Me.txtIpAddress1.Size = New System.Drawing.Size(55, 19)
        Me.txtIpAddress1.TabIndex = 1
        Me.txtIpAddress1.Text = "127"
        Me.txtIpAddress1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtIpAddress2
        '
        Me.txtIpAddress2.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtIpAddress2.Location = New System.Drawing.Point(140, 30)
        Me.txtIpAddress2.MaxLength = 3
        Me.txtIpAddress2.Name = "txtIpAddress2"
        Me.txtIpAddress2.Size = New System.Drawing.Size(55, 19)
        Me.txtIpAddress2.TabIndex = 1
        Me.txtIpAddress2.Text = "0"
        Me.txtIpAddress2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtIpAddress3
        '
        Me.txtIpAddress3.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtIpAddress3.Location = New System.Drawing.Point(201, 30)
        Me.txtIpAddress3.MaxLength = 3
        Me.txtIpAddress3.Name = "txtIpAddress3"
        Me.txtIpAddress3.Size = New System.Drawing.Size(55, 19)
        Me.txtIpAddress3.TabIndex = 1
        Me.txtIpAddress3.Text = "0"
        Me.txtIpAddress3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtIpAddress4
        '
        Me.txtIpAddress4.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtIpAddress4.Location = New System.Drawing.Point(262, 30)
        Me.txtIpAddress4.MaxLength = 3
        Me.txtIpAddress4.Name = "txtIpAddress4"
        Me.txtIpAddress4.Size = New System.Drawing.Size(55, 19)
        Me.txtIpAddress4.TabIndex = 1
        Me.txtIpAddress4.Text = "1"
        Me.txtIpAddress4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtPort
        '
        Me.txtPort.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtPort.Location = New System.Drawing.Point(79, 55)
        Me.txtPort.MaxLength = 5
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(55, 19)
        Me.txtPort.TabIndex = 1
        Me.txtPort.Text = "5000"
        Me.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtMessageRecieved
        '
        Me.txtMessageRecieved.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMessageRecieved.Location = New System.Drawing.Point(12, 115)
        Me.txtMessageRecieved.Multiline = True
        Me.txtMessageRecieved.Name = "txtMessageRecieved"
        Me.txtMessageRecieved.ReadOnly = True
        Me.txtMessageRecieved.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMessageRecieved.Size = New System.Drawing.Size(432, 323)
        Me.txtMessageRecieved.TabIndex = 2
        '
        'grbConnectionSetting
        '
        Me.grbConnectionSetting.Controls.Add(Me.Label2)
        Me.grbConnectionSetting.Controls.Add(Me.Label1)
        Me.grbConnectionSetting.Controls.Add(Me.txtIpAddress1)
        Me.grbConnectionSetting.Controls.Add(Me.txtPort)
        Me.grbConnectionSetting.Controls.Add(Me.txtIpAddress2)
        Me.grbConnectionSetting.Controls.Add(Me.txtIpAddress4)
        Me.grbConnectionSetting.Controls.Add(Me.txtIpAddress3)
        Me.grbConnectionSetting.Location = New System.Drawing.Point(12, 12)
        Me.grbConnectionSetting.Name = "grbConnectionSetting"
        Me.grbConnectionSetting.Size = New System.Drawing.Size(325, 83)
        Me.grbConnectionSetting.TabIndex = 3
        Me.grbConnectionSetting.TabStop = False
        Me.grbConnectionSetting.Text = "Connection Setting"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Port"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 12)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "IP Address"
        '
        'btnRegisterCommands
        '
        Me.btnRegisterCommands.Location = New System.Drawing.Point(343, 24)
        Me.btnRegisterCommands.Name = "btnRegisterCommands"
        Me.btnRegisterCommands.Size = New System.Drawing.Size(75, 33)
        Me.btnRegisterCommands.TabIndex = 4
        Me.btnRegisterCommands.Text = "Register Commands"
        Me.btnRegisterCommands.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(453, 450)
        Me.Controls.Add(Me.btnRegisterCommands)
        Me.Controls.Add(Me.txtMessageRecieved)
        Me.Controls.Add(Me.btnOpenClose)
        Me.Controls.Add(Me.grbConnectionSetting)
        Me.Name = "frmMain"
        Me.Text = "Form1"
        Me.grbConnectionSetting.ResumeLayout(False)
        Me.grbConnectionSetting.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnOpenClose As Button
    Friend WithEvents txtIpAddress1 As TextBox
    Friend WithEvents txtIpAddress2 As TextBox
    Friend WithEvents txtIpAddress3 As TextBox
    Friend WithEvents txtIpAddress4 As TextBox
    Friend WithEvents txtPort As TextBox
    Friend WithEvents txtMessageRecieved As TextBox
    Friend WithEvents grbConnectionSetting As GroupBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btnRegisterCommands As Button
End Class
