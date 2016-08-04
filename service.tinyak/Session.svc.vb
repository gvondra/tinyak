Imports tCore = tinyak.Core
Imports System.ServiceModel

Public Class Session
    Implements ISessionService

    Public Sub Save(objSession As clsSession) Implements ISessionService.Save
        Dim objInnerSession As tCore.clsSession
        Dim objSettings As clsSettings
        Try
            If objSession.Id.Equals(Guid.Empty) = False Then
                objSettings = New clsSettings
                objInnerSession = tCore.clsSession.Get(objSettings, objSession.Id)
                If objInnerSession IsNot Nothing Then
                    objSession.Update(objInnerSession)
                    objInnerSession.Save(objSettings)
                End If
            End If
        Catch ex As Exception
            Throw New FaultException("Unexpected error has occured")
        End Try
    End Sub

    Public Function Create() As clsSession Implements ISessionService.Create
        Dim objSession As tCore.clsSession
        Dim objResult As clsSession
        Try
            objSession = tCore.clsSession.CreateNew(New clsSettings)
            objResult = clsSession.Get(objSession)
        Catch ex As Exception
            Throw New FaultException("Unexpected error has occured")
        End Try
        Return objResult
    End Function

    Public Function [Get](objId As Guid) As clsSession Implements ISessionService.Get
        Dim objSession As clsSession
        Dim objInnerSession As tCore.clsSession
        Try
            If objId.Equals(Guid.Empty) = False Then
                objInnerSession = tCore.clsSession.Get(New clsSettings, objId)
                If objInnerSession IsNot Nothing Then
                    objSession = clsSession.Get(objInnerSession)
                Else
                    objSession = Nothing
                End If
            Else
                objSession = Nothing
            End If
        Catch ex As Exception
            Throw New FaultException("Unexpected error has occured")
        End Try
        Return objSession
    End Function
End Class
