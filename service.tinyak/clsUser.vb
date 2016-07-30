Imports tCore = tinyak.Core
Imports System.Runtime.Serialization
<DataContract(Name:="User", [Namespace]:="urn:service.tinyak.net/User/v1")>
Public Class clsUser
    <DataMember>
    Public Property Name As String

    <DataMember>
    Public Property EmailAddress As String

    Public Shared Function [Get](ByVal objUser As tCore.clsUser) As clsUser
        Dim objResult As clsUser

        objResult = New clsUser
        With objResult
            .Name = objUser.Name
            .EmailAddress = objUser.EmailAddress
        End With
        Return objResult
    End Function
End Class
