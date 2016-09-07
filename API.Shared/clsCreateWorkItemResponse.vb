<DataContract(Name:="CreateWorkItemResponse", Namespace:="urn:tinyak.net/api/v1")>
Public Class clsCreateWorkItemResponse
    <DataMember>
    Public Property ErrorMessage As String
    <DataMember>
    Public Property WorkItem As clsWorkListItem
End Class
