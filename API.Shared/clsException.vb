<DataContract(Name:="Exception", Namespace:="urn:tinyak.net/api/v1")>
Public Class clsException
    <DataMember>
    Public Property TypeName As String
    <DataMember>
    Public Property Message As String
    <DataMember>
    Public Property Source As String
    <DataMember>
    Public Property Target As String
    <DataMember>
    Public Property StackTrace As String
    <DataMember>
    Public Property HResult As Integer
    <DataMember>
    Public Property Timestamp As Date
    <DataMember>
    Public Property Data As Dictionary(Of String, String)
    <DataMember>
    Public Property InnerException As clsException
End Class
