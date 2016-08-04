Public Class clsSessionService
    Private m_objService As SessionService.SessionServiceClient

    Public Sub New(ByVal objSettings As ISettings)
        Dim objBinding As WSHttpBinding
        Dim objEndpoint As EndpointAddress
        Dim objUriBuilder As UriBuilder

        objUriBuilder = New UriBuilder(objSettings.BaseAddress)
        If String.IsNullOrEmpty(objUriBuilder.Path) Then
            objUriBuilder.Path = "Session.svc"
        ElseIf objUriBuilder.Path.EndsWith("/"c) Then
            objUriBuilder.Path &= "Session.svc"
        Else
            objUriBuilder.Path &= "/Session.svc"
        End If
        objEndpoint = New EndpointAddress(objUriBuilder.Uri)

        objBinding = New WSHttpBinding(SecurityMode.Message)
        objBinding.Security.Message.ClientCredentialType = MessageCredentialType.Windows

        m_objService = New SessionService.SessionServiceClient(objBinding, objEndpoint)
    End Sub

    Public Function [Get](ByVal objId As Guid) As clsSession
        Dim objSession As SessionService.Session

        objSession = m_objService.Get(objId)
        Return New clsSession(objSession)
    End Function

    Public Function Create() As clsSession
        Dim objSession As SessionService.Session

        objSession = m_objService.Create
        Return New clsSession(objSession)
    End Function

    Public Sub Save(ByVal objSession As clsSession)
        m_objService.Save(objSession.InnerSession)
    End Sub
End Class
