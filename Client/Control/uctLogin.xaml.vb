Public Class uctLogin
    Public Event LoggedIn(ByVal objSender As Control, ByVal objUser As clsUser)

    Private Function SessionId() As Guid
        Return winMain.SessionId
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

    Private Sub uctLogin_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        txtEmailAddress.Focus()
    End Sub
End Class
