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
    Public Property Id As Integer
    Public Property ProjectTitle As String
    Public Property ProjectMembers As clsProjectMembersModel
End Class

Public Class clsProjectCreateProjectMemberModel
    Public Property ProjectId As Integer
    Public Property EmailAddress As String
End Class

Public Class clsProjectMembersModel
    Public Property ProjectId As Integer
    Public Property Members As List(Of String)
End Class