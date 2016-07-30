Public Class clsUser
    Friend Sub New(ByVal objUser As tinyak.UserServiceReference.User)
        Name = objUser.Name
        EmailAddress = objUser.EmailAddress
    End Sub
    Public Property Name As String
    Public Property EmailAddress As String
End Class
