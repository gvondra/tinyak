Public Class uctWorkListItem
    Public Event DeselectAllWorkItems(ByVal objSender As Control)

    Private Sub lblTitle_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles lblTitle.MouseDown
        RaiseEvent DeselectAllWorkItems(Me)
        CType(DataContext, clsWorkListItemVM).Selected()
    End Sub
End Class
