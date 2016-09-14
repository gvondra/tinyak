<DataContract(Name:="Itteration", Namespace:="urn:tinyak.net/api/v1")>
Public Class clsItteration
    <DataMember>
    Public Property Id As Nullable(Of Integer)
    <DataMember>
    Public Property Name As String
    <DataMember>
    Public Property StartDate As Nullable(Of Date)
    <DataMember>
    Public Property EndDate As Nullable(Of Date)
    <DataMember>
    Public Property IsActive As Boolean
End Class
