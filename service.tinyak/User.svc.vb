Imports tCore = tinyak.Core
Imports System.ServiceModel

Public Class User
    Implements IUserService

    Public Function CreateUser(strName As String, strEmailAddress As String, strPassword As String) As clsUser Implements IUserService.CreateUser
        Dim objUser As tCore.clsUser
        Dim objResult As clsUser
        Try
            objUser = tCore.clsUser.CreateNew(New clsSettings, strName, strEmailAddress, strPassword)
            objResult = clsUser.Get(objUser)
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
