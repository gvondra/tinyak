Public Class uctWorkListItem
    Public Event DeselectAllWorkItems(ByVal objSender As Control)
    Public Event WorkItemDoubleClick(ByVal objSender As Object, ByVal objWorkItem As clsWorkListItemVM)
    Public Event WorkItemDelete(ByVal objSender As Object, ByVal objWorkItem As clsWorkListItemVM)

    Private Sub lblTitle_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles lblTitle.MouseDown
        If e.ChangedButton = MouseButton.Left Then
            RaiseEvent DeselectAllWorkItems(Me)
            CType(DataContext, clsWorkListItemVM).Selected()
            If e.ClickCount = 2 Then
                RaiseEvent WorkItemDoubleClick(Me, DirectCast(DataContext, clsWorkListItemVM))
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub Delete_Click(sender As Object, e As RoutedEventArgs)
        Dim objWorkListItem As clsWorkListItemVM

        objWorkListItem = DirectCast(DataContext, clsWorkListItemVM)
        objWorkListItem.Delete()

        RaiseEvent WorkItemDelete(Me, objWorkListItem)
    End Sub
End Class
