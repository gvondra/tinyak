<DataContract(Name:="User", Namespace:="urn:tinyak.net/api/v1")>
Public Class clsUser
    <DataMember()>
    Public Property Name As String
    <DataMember()>
    Public Property IsAdministrator As Boolean
End Class
