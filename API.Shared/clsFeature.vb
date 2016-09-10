<DataContract(Name:="Feature", Namespace:="urn:tinyak.net/api/v1")>
Public Class clsFeature
    <DataMember>
    Public Property Id As Integer
    <DataMember>
    Public Property ProjectId As Integer
    <DataMember>
    Public Property Title As String
End Class
