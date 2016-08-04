Imports tCore = tinyak.Core
Imports System.ServiceModel

Public Class User
    Implements IUserService

    Public Function CreateUser(ByVal objSessionId As Guid, ByVal strName As String, strEmailAddress As String, strPassword As String) As clsUser Implements IUserService.CreateUser
        Dim objUser As tCore.clsUser
        Dim objResult As clsUser = Nothing
        Dim objSession As tCore.clsSession
        Dim objSettings As clsSettings
        Try
            objSettings = New clsSettings
            objSession = tCore.clsSession.Get(objSettings, objSessionId)
            If objSession IsNot Nothing Then
                objUser = tCore.clsUser.CreateNew(objSettings, strName, strEmailAddress, strPassword)
                objResult = clsUser.Get(objUser)

                objSession.UserId = objUser.Id.Value
                objSession.Save(objSettings)
            End If
        Catch ex As Exception
            Throw New FaultException("Unexpected error has occured")
        End Try
        Return objResult
    End Function

    Public Function IsEmailAddressAvailable(strEmailAddress As String) As Boolean Implements IUserService.IsEmailAddressAvailable
        Try
            strEmailAddress = strEmailAddress.Trim
            If tCore.clsValidation.IsValidEmailAddress(strEmailAddress) Then
                Return tCore.clsUser.IsEmailAddressAvailable(New clsSettings, strEmailAddress)
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New FaultException("Unexpected error has occured")
        End Try
    End Function
End Class
