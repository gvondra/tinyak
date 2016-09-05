Public Class clsWorkItemData
    Public Property Id As Nullable(Of Integer)
    Public Property ProjectId As Nullable(Of Integer)
    Public Property FeatureId As Nullable(Of Integer)
    Public Property Title As String
    Public Property State As Int16
    Public Property AssignedTo As Nullable(Of Integer)
    Public Property Effort As Nullable(Of Int16)
    Public Property Description As String
    Public Property AcceptanceCriteria As String
    Public Property Tasks As List(Of clsTaskData)
End Class
