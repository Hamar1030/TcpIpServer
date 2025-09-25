<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmGetCommand
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
        Me.lsbData = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'lsbData
        '
        Me.lsbData.FormattingEnabled = True
        Me.lsbData.ItemHeight = 12
        Me.lsbData.Location = New System.Drawing.Point(12, 12)
        Me.lsbData.Name = "lsbData"
        Me.lsbData.Size = New System.Drawing.Size(310, 340)
        Me.lsbData.TabIndex = 0
        '
        'frmGetCommand
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(334, 361)
        Me.Controls.Add(Me.lsbData)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmGetCommand"
        Me.Text = "フォーマット異常データ"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lsbData As ListBox
End Class
