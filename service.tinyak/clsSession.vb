﻿Imports tCore = tinyak.Core

<DataContract(Name:="Session", [Namespace]:="urn:service.tinyak.net/Session/v1")>
Public Class clsSession
    <DataMember>
    Public Property Id As Guid

    <DataMember>
    Public Property UserId As Nullable(Of Integer)

    <DataMember>
    Public Property ExpirationDate As Date

    <DataMember>
    Public Property Data As Dictionary(Of String, String)

    Friend Shared Function [Get](ByVal objSession As tCore.clsSession) As clsSession
        Dim objResult As clsSession

        objResult = New clsSession
        With objResult
            .Data = objSession.Data
            .Id = objSession.Id
            .UserId = objSession.UserId
        End With
        Return objResult
    End Function

    Friend Sub Update(ByVal objSession As tCore.clsSession)
        With objSession
            Data = .Data
            UserId = .UserId
        End With
    End Sub
End Class