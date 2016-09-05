<DataContract(Name:="CreateFeatureResponse", Namespace:="urn:tinyak.net/api/v1")>
Public Class clsCreateFeatureResponse
    <DataMember>
    Public Property ErrorMessage As String
    <DataMember>
    Public Property Feature As clsFeatureListItem
End Class
