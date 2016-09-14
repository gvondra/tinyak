<DataContract(Name:="WorkListItem", Namespace:="urn:tinyak.net/api/v1")>
Public Class clsWorkListItem
    <DataMember>
    Property Id As Nullable(Of Integer)
    <DataMember>
    Public Property Title As String
    <DataMember>
    Public Property State As enumWorkItemState
    <DataMember>
    Public Property AssignedTo As String
    <DataMember>
    Public Property Effort As Nullable(Of Int16)
    <DataMember>
    Public Property Itteration As clsItteration
End Class
