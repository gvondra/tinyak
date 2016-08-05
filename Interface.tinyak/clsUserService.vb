﻿Public Class clsUserService
    Private m_objService As UserServiceReference.UserServiceClient

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

        m_objService = New UserServiceReference.UserServiceClient(objBinding, objEndpoint)
    End Sub

    Public Function IsEmailAddressAvailable(ByVal strEmailAddress As String) As Boolean
        Return m_objService.IsEmailAddressAvailable(strEmailAddress)
    End Function

    Public Function Create(ByVal objSessionId As Guid, ByVal strUser As String, ByVal strEmailAddress As String, ByVal strPassword As String) As clsUser
        Dim objUser As UserServiceReference.User

        objUser = m_objService.CreateUser(objSessionId, strUser, strEmailAddress, strPassword)
        Return New clsUser(objUser)
    End Function

    Public Function Login(ByVal objSessionId As Guid, ByVal strEmailAddress As String, ByVal strPassword As String) As Boolean
        Return m_objService.Login(objSessionId, strEmailAddress, strPassword)
    End Function
End Class
