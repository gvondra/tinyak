Imports tCore = tinyak.Core
Imports System.ServiceModel
Imports service.tinyak

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

    Public Function GetUser(objSessionId As Guid, intUserId As Int32) As clsUser Implements IUserService.GetUser
        Dim objResult As clsUser
        Dim objSession As tCore.clsSession
        Dim objSettings As clsSettings
        Dim objUser As tCore.clsUser
        Try
            objSettings = New clsSettings
            objSession = tCore.clsSession.Get(objSettings, objSessionId)
            If objSession.UserId.HasValue _
                    AndAlso (objSession.UserId.Value = intUserId OrElse objSession.GetUser(objSettings).IsAdministrator) Then

                objUser = tCore.clsUser.Get(objSettings, intUserId)
                If objUser IsNot Nothing Then
                    objResult = clsUser.Get(objUser)
                Else
                    objResult = Nothing
                End If
            Else
                Throw New FaultException("Not authorized")
            End If
        Catch ex As FaultException
            Throw
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

    Public Function Login(objSessionId As Guid, strEmailAddress As String, strPassword As String) As Boolean Implements IUserService.Login
        Dim objUser As tCore.clsUser
        Dim objSession As tCore.clsSession
        Dim objSettings As clsSettings
        Dim blnResult As Boolean
        Try
            objSettings = New clsSettings
            objSession = tCore.clsSession.Get(objSettings, objSessionId)
            If objSession IsNot Nothing Then
                objUser = tCore.clsUser.GetByEmailAddress(objSettings, strEmailAddress, strPassword)
                If objUser IsNot Nothing Then
                    objSession.UserId = objUser.Id
                    objSession.Save(objSettings)
                    blnResult = True
                Else
                    blnResult = False
                End If
            Else
                blnResult = False
            End If
        Catch ex As Exception
            Throw New FaultException("Unexpected error has occured")
        End Try
        Return blnResult
    End Function

    Public Function SaveUser(objSessionId As Guid, objUser As clsUser) As clsUser Implements IUserService.SaveUser
        Dim objResult As clsUser
        Dim objSession As tCore.clsSession
        Dim objSettings As clsSettings
        Dim objInnerUser As tCore.clsUser
        Try
            objSettings = New clsSettings
            objSession = tCore.clsSession.Get(objSettings, objSessionId)
            If objSession.UserId.HasValue _
                    AndAlso objUser.Id.HasValue _
                    AndAlso (objSession.UserId.Value = objUser.Id.Value OrElse objSession.GetUser(objSettings).IsAdministrator) Then

                objInnerUser = tCore.clsUser.Get(objSettings, objUser.Id.Value)
                If objInnerUser IsNot Nothing Then
                    objUser.Update(objInnerUser)
                    objInnerUser.Update(objSettings)

                    objResult = clsUser.Get(objInnerUser)
                Else
                    objResult = Nothing
                End If
            Else
                Throw New FaultException("Not authorized")
            End If
        Catch ex As FaultException
            Throw
        Catch ex As Exception
            Throw New FaultException("Unexpected error has occured")
        End Try
        Return objResult
    End Function
End Class
