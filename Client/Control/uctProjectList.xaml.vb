Public Class uctProjectList
    Public Event OnProjectSelected(ByVal objSender As Control, ByVal objProject As clsProject)

    Private Sub Hyperlink_Click(sender As Object, e As RoutedEventArgs)
        RaiseEvent OnProjectSelected(Me, DirectCast(CType(e.Source, Hyperlink).DataContext, clsProject))
    End Sub
End Class
