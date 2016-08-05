﻿Imports System.ServiceModel

<ServiceContract([Namespace]:="urn:service.tinyak.net/User/v1", Name:="UserService")>
Public Interface IUserService

    <OperationContract()>
    Function Login(ByVal objSessionId As Guid, ByVal strEmailAddress As String, ByVal strPassword As String) As Boolean

    <OperationContract()>
    Function IsEmailAddressAvailable(ByVal strEmailAddress As String) As Boolean

    <OperationContract()>
    Function CreateUser(ByVal objSessionId As Guid, ByVal strName As String, ByVal strEmailAddress As String, ByVal strPassword As String) As clsUser

    <OperationContract()>
    Function GetUser(ByVal objSessionId As Guid, ByVal intUserId As Integer) As clsUser

    <OperationContract()>
    Function SaveUser(ByVal objSessionId As Guid, ByVal objUser As clsUser) As clsUser
End Interface
