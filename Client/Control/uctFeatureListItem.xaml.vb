Public Class uctFeatureListItem
    Public Event FeatureDoubleClick(ByVal objSender As Object, ByVal objFeature As clsFeatureListItemVM)
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

    Private Sub lblTitle_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles lblTitle.MouseDown
        If e.ChangedButton = MouseButton.Left AndAlso e.ClickCount = 2 Then
            RaiseEvent FeatureDoubleClick(Me, CType(DataContext, clsFeatureListItemVM))
        End If
    End Sub
End Class
