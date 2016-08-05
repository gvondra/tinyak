Imports tCore = tinyak.Core

<DataContract(Name:="User", [Namespace]:="urn:service.tinyak.net/User/v1")>
Public Class clsUser
    <DataMember>
    Public Property Id As Nullable(Of Integer)

    <DataMember>
    Public Property Name As String

    <DataMember>
    Public Property EmailAddress As String

    Public Shared Function [Get](ByVal objUser As tCore.clsUser) As clsUser
        Dim objResult As clsUser

        objResult = New clsUser
        With objResult
            .Id = objUser.Id
            .Name = objUser.Name
            .EmailAddress = objUser.EmailAddress
        End With
        Return objResult
    End Function

    Public Sub Update(ByVal objUser As tCore.clsUser)
        With objUser
            .Name = Name
        End With
    End Sub
End Class
