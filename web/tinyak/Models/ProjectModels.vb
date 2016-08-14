Public Class clsProjectListModel
    Public Property Items As List(Of clsProjectListItemModel)
End Class

Public Class clsProjectListItemModel
    Public Property Id As Integer
    Public Property Title As String
End Class

Public Class clsProjectCreateModel
    Public Property ProjectTitle As String
End Class

Public Class clsProjectUpdateModel
    Public Property ProjectTitle As String
End Class

