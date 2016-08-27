Public Class winMain
    Public Property SessionId As Guid

    Private Sub mnuExit_Click(sender As Object, e As RoutedEventArgs) Handles mnuExit.Click
        Me.Close()
    End Sub

    Private Sub LoadSessionId()
        Try
            SessionId = clsSession.GetSessionId(New clsSettings)
        Catch ex As Exception
            winException.BeginProcessException(ex, Dispatcher)
        End Try
    End Sub

    Private Sub winMain_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        Dim objLoadSessionId As Action

        objLoadSessionId = New Action(AddressOf LoadSessionId)
        objLoadSessionId.BeginInvoke(Nothing, objLoadSessionId)
    End Sub

    Friend Sub OnLoggedOn(ByVal objSender As Control, ByVal objUser As clsUser)
        lblUser.DataContext = New clsUserVM(objUser)
        pnlControl.Children.Clear()
    End Sub
End Class
