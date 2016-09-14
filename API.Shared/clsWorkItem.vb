<DataContract(Name:="WorkItem", Namespace:="urn:tinyak.net/api/v1")>
Public Class clsWorkItem
    <DataMember>
    Public Property Id As Nullable(Of Integer)
    <DataMember>
    Public Property Title As String
    <DataMember>
    Public Property State As enumWorkItemState
    <DataMember>
    Public Property AssignedTo As String
    <DataMember>
    Public Property Effort As Nullable(Of Int16)
    <DataMember>
    Public Property Description As String
    <DataMember>
    Public Property AcceptanceCriteria As String
    <DataMember>
    Public Property Itteration As clsItteration
End Class
