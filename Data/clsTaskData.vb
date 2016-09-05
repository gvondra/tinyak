Public Class clsTaskData
    Public Property Id As Nullable(Of Integer)
    Public Property ProjectId As Nullable(Of Integer)
    Public Property WorkItemId As Nullable(Of Integer)
    Public Property Title As String
    Public Property Status As Int16
    Public Property AssignedTo As Nullable(Of Integer)
    Public Property Remaining As Nullable(Of Int16)
    Public Property Description As String
End Class
