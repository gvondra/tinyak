Public Class clsUser
    Private m_objService As UserServiceReference.UserClient

    Public Sub New(ByVal objSettings As ISettings)
        Dim objBinding As WSHttpBinding
        Dim objEndpoint As EndpointAddress
        Dim objUriBuilder As UriBuilder

        objUriBuilder = New UriBuilder(objSettings.BaseAddress)
        If String.IsNullOrEmpty(objUriBuilder.Path) Then
            objUriBuilder.Path = "User.svc"
        ElseIf objUriBuilder.Path.EndsWith("/"c) Then
            objUriBuilder.Path &= "User.svc"
        Else
            objUriBuilder.Path &= "/User.svc"
        End If
        objEndpoint = New EndpointAddress(objUriBuilder.Uri)

        objBinding = New WSHttpBinding(SecurityMode.Message)
        objBinding.Security.Message.ClientCredentialType = MessageCredentialType.Windows

        m_objService = New UserServiceReference.UserClient(objBinding, objEndpoint)
    End Sub

    Public Function IsEmailAddressAvailable(ByVal strEmailAddress As String) As Boolean
        Return m_objService.IsEmailAddressAvailable(strEmailAddress)
    End Function
End Class
