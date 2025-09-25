Imports System.Collections.Concurrent
Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Reflection
Imports System.Text
Imports System.Threading

Public Class clsServer : Implements IDisposable

#Region "Variable"
    ''' <summary>メインスレッド</summary>
    Private m_thrMain As Thread

    ''' <summary>スレッド一時停止用ウェイトハンドル</summary>
    Private m_mtxWaitHandle As ManualResetEventSlim

    Private m_listener As TcpListener

    ''' <summary>サーバー終了フラグ</summary>
    Private m_isServerClose As Boolean = False

    ''' <summary>スレッド終了フラグ</summary>
    Private m_isThreadClose As Boolean = False

    Private m_client As TcpClient

    Private m_CommandList As Dictionary(Of String, String)
#End Region

#Region "Property"
    Public ReadOnly Property IpAddress As IPAddress
    Public ReadOnly Property Port As Integer

    Public ReadOnly Property ErrorMessage As String
#End Region

#Region "Event"
    Public Event DataReceived(ByVal message As String)
#End Region

#Region "Constructor/Destructor"
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
        m_CommandList = New Dictionary(Of String, String)

        AddOrUpdateCommandList("*IDN?", "Emulated Device")

        AddOrUpdateCommandList("MEAS?", "10.23456789")
        AddOrUpdateCommandList("MEAS:m?", "0.00123456789")
        AddOrUpdateCommandList("MEAS:k?", "1234.56789")

        AddOrUpdateCommandList("test?", "test message")
        AddOrUpdateCommandList("test?", "test message2")

        'スレッド生成
        m_thrMain = New Thread(AddressOf Worker)

        m_mtxWaitHandle = New ManualResetEventSlim()
        m_mtxWaitHandle.Reset()

        'バックグラウンド処理
        m_thrMain.IsBackground = True

        'スレッド開始
        m_thrMain.Start()
    End Sub


#Region "IDisposable Support"
    Private disposedValue As Boolean ' 重複する呼び出しを検出するには

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: マネージド状態を破棄します (マネージド オブジェクト)。
                m_isThreadClose = True
                Close()
                m_mtxWaitHandle.Set()
                m_thrMain.Join(3000)
            End If

            ' TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、下の Finalize() をオーバーライドします。
            ' TODO: 大きなフィールドを null に設定します。
        End If
        disposedValue = True
    End Sub

    ' TODO: 上の Dispose(disposing As Boolean) にアンマネージド リソースを解放するコードが含まれる場合にのみ Finalize() をオーバーライドします。
    'Protected Overrides Sub Finalize()
    '    ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(disposing As Boolean) に記述します。
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' このコードは、破棄可能なパターンを正しく実装できるように Visual Basic によって追加されました。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(disposing As Boolean) に記述します。
        Dispose(True)
        ' TODO: 上の Finalize() がオーバーライドされている場合は、次の行のコメントを解除してください。
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

#End Region

#Region "PublicMethod"
    Public Function Open(ByVal ipAddressServer As IPAddress, ByVal portServer As Integer) As Boolean

        If m_listener IsNot Nothing AndAlso
           m_listener.Server.Connected = True Then
            Return False
        End If

        _IpAddress = ipAddressServer
        _Port = portServer

        Try
            m_listener = New TcpListener(_IpAddress, _Port)
            m_listener.Start()

            m_mtxWaitHandle.Set()
            RaiseEvent DataReceived("サーバーを開きました。")
        Catch ex As Exception
            _ErrorMessage = ex.Message

            Try
                m_listener.Stop()
            Catch exin As Exception
                _ErrorMessage &= vbCrLf & ex.Message
            End Try

            Return False
        End Try

        m_isServerClose = False

        Return True
    End Function

    Public Function Close() As Boolean

        If m_listener Is Nothing Then
            Return True
        End If
        m_isServerClose = True
        If m_client IsNot Nothing AndAlso
           m_client.Connected = False Then
            m_listener.Stop()
        End If
        Return True
    End Function

    Private Function Send(ByVal client As NetworkStream, ByVal message As String) As Boolean
        Try
            Dim clientIp As IPEndPoint = m_client.Client.RemoteEndPoint
            Dim strClientAddr As String = clientIp.Address.ToString() & "(" & clientIp.Port & ")"

            message &= vbCrLf
            Dim messageByte As Byte() = Encoding.UTF8.GetBytes(message)

            client.Write(messageByte, 0, messageByte.Length)

            RaiseEvent DataReceived($"データ送信(to:{strClientAddr}):{message}")

        Catch ex As Exception
            RaiseEvent DataReceived("送信エラー:" & ex.Message)
            Return False
        End Try

        Return True
    End Function

#Region "CommandList"
    Public Function TryAddCommandList(ByVal received As String, ByVal responce As String) As Boolean
        Dim receivedUpper As String = received.Trim.ToUpper

        If m_CommandList.ContainsKey(receivedUpper) = False Then
            m_CommandList.Add(received.ToUpper, responce)
        Else
            Return False
        End If

        Return True
    End Function
    Public Sub AddOrUpdateCommandList(ByVal received As String, ByVal responce As String)
        Dim receivedUpper As String = received.Trim.ToUpper

        If m_CommandList.ContainsKey(receivedUpper) = False Then
            m_CommandList.Add(received.ToUpper, responce)
        Else
            m_CommandList(receivedUpper) = responce
        End If
    End Sub

    Public Sub DeleteCommandList(ByVal received As String)
        m_CommandList.Remove(received.Trim.ToUpper)
    End Sub

    Public Sub InitializeCommandList()
        m_CommandList.Clear()
    End Sub
    Public Sub InitializeCommandList(ByVal base As Dictionary(Of String, String))
        m_CommandList.Clear()

        For Each val As KeyValuePair(Of String, String) In base
            m_CommandList.Add(val.Key.Trim.ToUpper, val.Value)
        Next
    End Sub
#End Region

#End Region

#Region "Thread"
    Private Sub Worker()
        Try
            Do
                m_mtxWaitHandle.Wait()

                If m_isServerClose = True Then
                    Try
                        m_listener.Server.Dispose()
                        m_listener.Stop()
                    Catch ex As Exception
                        RaiseEvent DataReceived(ex.Message)
                    End Try

                    m_isServerClose = False
                    RaiseEvent DataReceived("サーバーを閉じました。")
                    m_mtxWaitHandle.Reset()
                    Continue Do
                End If

                If m_isThreadClose = True Then
                    Exit Do
                End If

                'クライアントの接続要求がなければ少し待って次のループへ
                If m_listener.Pending = False Then
                    Thread.Sleep(100)
                    Continue Do
                End If

                'クライアントからの接続を受け入れる
                Dim client As TcpClient = m_listener.AcceptTcpClient()
                Dim clientIp As IPEndPoint = client.Client.RemoteEndPoint
                RaiseEvent DataReceived("クライアントが接続しました。 IPAddress:" & clientIp.Address.ToString() & " Port:" & clientIp.Port)
                m_client = client
                'クライアントとの通信用のストリームを取得
                Dim stream As NetworkStream = client.GetStream()
                stream.ReadTimeout = 3000

                Dim sentenceRecieved As New StringBuilder

                Do
                    Try
                        If m_isServerClose = True Then
                            Exit Do
                        End If

                        'データの受信
                        Dim buffer(1024) As Byte
                        Dim bytesRead As Integer = stream.Read(buffer, 0, buffer.Length)
                        If bytesRead > 0 Then
                            ' 受信したデータを文字列に変換
                            Dim strRead As String = Encoding.ASCII.GetString(buffer, 0, bytesRead)
                            RaiseEvent DataReceived(strRead)


                            '応答？
                            Do
                                'デリミタがなければ追加のみ
                                If strRead.Contains(vbCrLf) = False Then
                                    sentenceRecieved.Append(strRead)
                                    Exit Do
                                End If

                                'デリミタまでの文字列切り出し
                                Dim index As Integer = strRead.IndexOf(vbCrLf)
                                sentenceRecieved.Append(strRead.Substring(0, index + 1).Trim)

                                '一文取得
                                Dim strSentence As String = sentenceRecieved.ToString()

                                RaiseEvent DataReceived("デリミタ検出 一文:" & strSentence)

                                If strSentence.Contains("?") Then
                                    If m_CommandList.ContainsKey(strSentence.ToUpper) = False Then
                                        Send(stream, "-999,Undefined Command")
                                    Else
                                        Send(stream, m_CommandList(strSentence.ToUpper))
                                    End If
                                End If


                                '一文バッファクリア
                                sentenceRecieved.Clear()

                                'デリミタ以降も有ればデリミタ以降で上書き
                                If strRead.Length > index + 1 Then
                                    strRead = strRead.Substring(index + 1).Trim
                                End If
                            Loop

                        Else
                            'クライアントが接続を閉じた場合は抜ける
                            RaiseEvent DataReceived("クライアントが接続を終了しました。 IPAddress:" & clientIp.Address.ToString() & " Port:" & clientIp.Port)
                            Exit Do
                        End If
                    Catch ex As Exception
                        If client.Connected = False Then
                            Exit Do
                        End If
                    End Try
                Loop

                RaiseEvent DataReceived("クライアントとの接続を終了します。 IPAddress:" & clientIp.Address.ToString() & " Port:" & clientIp.Port)
                client.Close()
            Loop

        Catch ex As Exception

        Finally
            m_mtxWaitHandle.Dispose()
        End Try
    End Sub

#End Region
End Class
