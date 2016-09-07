<DataContract(Name:="CreateWorkItemRequest", Namespace:="urn:tinyak.net/api/v1")>
Public Class clsCreateWorkItemRequest
    <DataMember>
    Property FeatureId As Integer
    <DataMember>
    Property Title As String
End Class
