Public Class uctLogin
    Public Event LoggedIn(ByVal objSender As Control, ByVal objUser As clsUser)

    Private Function SessionId() As Guid
        Return DirectCast(Window.GetWindow(Me), winMain).SessionId
    End Function

    Private Sub btnLogin_Click(sender As Object, e As RoutedEventArgs) Handles btnLogin.Click
        Dim objUser As clsUser
        Try
            objUser = clsUser.GetByEmailAddress(New clsSettings, SessionId, txtEmailAddress.Text.Trim, txtPassword.Password)
            RaiseEvent LoggedIn(Me, objUser)
        Catch ex As Exception
            winException.BeginProcessException(ex, Dispatcher)
        End Try
    End Sub
End Class
