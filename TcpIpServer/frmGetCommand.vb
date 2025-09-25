Imports System.Runtime.CompilerServices
Imports System.Runtime.Remoting.Channels

Public Class frmGetCommand
    Private m_data As SortedDictionary(Of Integer, String)
    Public Sub New(ByVal data As SortedDictionary(Of Integer, String))

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        m_data = data
    End Sub

    Private Sub frmGetCommand_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        For Each keyvalue As KeyValuePair(Of Integer, String) In m_data
            lsbData.Items.Add($"L{keyvalue.Key.ToString}{vbTab} {keyvalue.Value}")
        Next
    End Sub
End Class