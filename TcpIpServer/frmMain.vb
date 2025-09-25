Imports System.Collections.Concurrent
Imports System.IO
Imports Microsoft.VisualBasic.FileIO
Public Class frmMain

    Private WithEvents m_clsServer As clsServer

    ''' <summary>受信データキュー</summary>
    Private m_queMessageReceived As ConcurrentQueue(Of String)
    ''' <summary>受信データ表示用タイマーイベント</summary>
    Private WithEvents m_timDispMessage As Timer

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        m_queMessageReceived = New ConcurrentQueue(Of String)

        m_clsServer = New clsServer
        m_timDispMessage = New Timer()

        m_timDispMessage.Interval = 100
        m_timDispMessage.Start()
    End Sub

    Private Sub btnOpenClose_Click(sender As Object, e As EventArgs) Handles btnOpenClose.Click
        If btnOpenClose.Text = "Open" Then
            If m_clsServer.Open(New Net.IPAddress(New Byte() {127, 0, 0, 1}), 5000) = False Then
                MessageBox.Show("Openに失敗しました。" & m_clsServer.ErrorMessage, "エラー", MessageBoxButtons.OK)
                Exit Sub
            End If

            btnOpenClose.Text = "Close"
            grbConnectionSetting.Enabled = False

        ElseIf btnOpenClose.Text = "Close" Then
            If m_clsServer.Close() = False Then
                MessageBox.Show("Closeに失敗しました。" & m_clsServer.ErrorMessage, "エラー", MessageBoxButtons.OK)
            End If
            btnOpenClose.Text = "Open"
            grbConnectionSetting.Enabled = True
        End If

    End Sub

    Private Sub m_timDispMessage_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles m_timDispMessage.Tick
        Dim message As String = ""
        'データがなければ終了
        If m_queMessageReceived.Count <= 0 OrElse
           m_queMessageReceived.TryDequeue(message) = False Then
            Exit Sub
        End If

        txtMessageRecieved.Text &= message & vbCrLf
    End Sub

    ''' <summary>
    ''' サーバーデータ受信イベント
    ''' </summary>
    ''' <param name="message"></param>
    Private Sub m_clsServer_DataRecieved(ByVal message As String) Handles m_clsServer.DataReceived
        m_queMessageReceived.Enqueue(message)
    End Sub

    Private Sub btnRegisterCommands_Click(sender As Object, e As EventArgs) Handles btnRegisterCommands.Click
        Using ofd As New OpenFileDialog()
            ofd.Multiselect = False
            ofd.CheckFileExists = True
            ofd.CheckPathExists = True
            ofd.Title = "コマンドリスト定義ファイルを選択してください。"

            If ofd.ShowDialog() = DialogResult.Cancel Then
                Exit Sub
            End If

            If RegisterCommands(ofd.FileName) = False Then
                Exit Sub
            End If
        End Using
    End Sub

    Private Function RegisterCommands(ByVal filepath As String) As Boolean
        Try
            If File.Exists(filepath) = False Then
                MessageBox.Show("ファイルが存在しません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            Dim lineError As New SortedDictionary(Of Integer, String)
            Dim lineCount As Integer = 1

            Dim commands As New Dictionary(Of String, String)

            Using sr As New StreamReader(filepath)

                While sr.EndOfStream = False
                    Dim line As String = sr.ReadLine
                    Dim fields As String() = line.Split(",")

                    If String.IsNullOrWhiteSpace(line) Then
                        lineCount += 1
                        Continue While
                    End If

                    If fields.Length < 2 Then
                        lineError.Add(lineCount, line)
                    Else
                        commands.Add(fields(0), String.Join(",", fields, 1, fields.Length - 1))
                    End If

                    lineCount += 1
                End While
            End Using

            If lineError.Count > 0 Then
                MessageBox.Show("異常データがあります。ファイルを確認してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Using frm As New frmGetCommand(lineError)
                    frm.ShowDialog()
                End Using
                Return False
            End If

            m_clsServer.InitializeCommandList(commands)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try

        Return True
    End Function
End Class
