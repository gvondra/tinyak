<DataContract(Name:="WorkItem", Namespace:="urn:tinyak.net/api/v1")>
Public Class clsWorkItem
    <DataMember>
    Property Id As Nullable(Of Integer)
    <DataMember>
    Public Property Title As String
    <DataMember>
    Public Property State As enumWorkItemState
    <DataMember>
    Public Property AssignedTo As Nullable(Of Integer)
    <DataMember>
    Public Property Effort As Nullable(Of Int16)
    <DataMember>
    Public Property Description As String
    <DataMember>
    Public Property AcceptanceCriteria As String
End Class
