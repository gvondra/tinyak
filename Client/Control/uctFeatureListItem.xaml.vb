Public Class uctFeatureListItem
    Private Sub Expand_Click(sender As Object, e As RoutedEventArgs)
        CType(DataContext, clsFeatureListItemVM).ToggleIsExpanded()
    End Sub

    Private Sub Add_Click(sender As Object, e As RoutedEventArgs)
        CType(DataContext, clsFeatureListItemVM).ShowAddWorkItem()
    End Sub

    Private Sub ctlAddWorkItem_OnHide(objSender As Control) Handles ctlAddWorkItem.OnHide
        CType(DataContext, clsFeatureListItemVM).HideAddWorkItem()
    End Sub

    Private Sub uctWorkListItem_DeselectAllWorkItems(objSender As Control)
        CType(DataContext, clsFeatureListItemVM).DeleselectAllWorkItems()
    End Sub

    Private Sub ctlAddWorkItem_AfterAddWorkItem(objSender As Control, objWorkItem As clsWorkListItem)
        CType(DataContext, clsFeatureListItemVM).WorkListItems.Add(New clsWorkListItemVM(objWorkItem))
    End Sub
End Class
