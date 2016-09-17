<DataContract(Name:="WebMetrics", Namespace:="urn:tinyak.net/api/v1")>
Public Class clsWebMetrics
    <DataMember>
    Public Property Url As String
    <DataMember>
    Public Property Controller As String
    <DataMember>
    Public Property Action As String
    <DataMember>
    Public Property Timestamp As Date
    <DataMember>
    Public Property Duration As Nullable(Of Double)
    <DataMember>
    Public Property ContentEncoding As String
    <DataMember>
    Public Property ContentLength As Nullable(Of Integer)
    <DataMember>
    Public Property ContentType As String
    <DataMember>
    Public Property Method As String
    <DataMember>
    Public Property RequestType As String
    <DataMember>
    Public Property TotalBytes As Nullable(Of Integer)
    <DataMember>
    Public Property UrlReferrer As String
    <DataMember>
    Public Property UserAgent As String
    <DataMember>
    Public Property Parameters As String
End Class
