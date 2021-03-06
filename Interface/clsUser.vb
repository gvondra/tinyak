﻿Imports tas = tinyak.API.Shared
Public Class clsUser
    Private Property m_objInnerUser As tas.clsUser

    Private Sub New(ByVal objUser As tas.clsUser)
        m_objInnerUser = objUser
    End Sub

    Public ReadOnly Property Name As String
        Get
            Return m_objInnerUser.Name
        End Get
    End Property

    Public ReadOnly Property IsAdministrator As Boolean
        Get
            Return m_objInnerUser.IsAdministrator
        End Get
    End Property

    Public Shared Function GetByEmailAddress(ByVal objSettings As ISettings, ByVal objSession As Guid, ByVal strEmailAddress As String, ByVal strPassword As String) As clsUser
        Dim objRequest As HttpWebRequest
        Dim objResponse As HttpWebResponse
        Dim objUri As Uri
        Dim objQuery As NameValueCollection
        Dim objSerializer As DataContractSerializer
        Dim objInnerUser As tas.clsUser

        objQuery = New NameValueCollection
        objQuery.Add("emailAddress", strEmailAddress)
        objQuery.Add("password", strPassword)
        objUri = GetUri(objSettings, "User", objQuery)
        objRequest = CreateGetRequest(objSession, objUri)

        objResponse = DirectCast(objRequest.GetResponse, HttpWebResponse)
        objSerializer = New DataContractSerializer(GetType(tas.clsUser))
        objInnerUser = CType(objSerializer.ReadObject(objResponse.GetResponseStream), tas.clsUser)
        Return New clsUser(objInnerUser)
    End Function
End Class
