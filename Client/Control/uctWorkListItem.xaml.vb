Public Class uctWorkListItem
    Public Event DeselectAllWorkItems(ByVal objSender As Control)
    Public Event WorkItemDoubleClick(ByVal objSender As Object, ByVal objWorkItem As clsWorkListItemVM)

    Private Sub lblTitle_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles lblTitle.MouseDown
        If e.ChangedButton = MouseButton.Left Then
            RaiseEvent DeselectAllWorkItems(Me)
            CType(DataContext, clsWorkListItemVM).Selected()
            If e.ClickCount = 2 Then
                RaiseEvent WorkItemDoubleClick(Me, DirectCast(DataContext, clsWorkListItemVM))
            End If
        End If
    End Sub
End Class
