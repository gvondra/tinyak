Public Class uctFeatureListItem
    Public Event FeatureDoubleClick(ByVal objSender As Object, ByVal objFeature As clsFeatureListItemVM)
    Public Event WorkItemDoubleClick(ByVal objSender As Object, ByVal objWorkItem As clsWorkListItemVM)
    Private Sub Expand_Click(sender As Object, e As RoutedEventArgs)
        CType(DataContext, clsFeatureListItemVM).ToggleIsExpanded()
    End Sub

    Private Sub Add_Click(sender As Object, e As RoutedEventArgs)
        Dim objFeature As clsFeatureListItemVM

        objFeature = CType(DataContext, clsFeatureListItemVM)
        objFeature.ShowAddWorkItem()
        If objFeature.IsExpanded = False Then
            objFeature.ToggleIsExpanded()
        End If
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
            RaiseEvent FeatureDoubleClick(Me, DirectCast(DataContext, clsFeatureListItemVM))
        End If
    End Sub

    Private Sub uctWorkListItem_WorkItemDoubleClick(objSender As Object, objWorkItem As clsWorkListItemVM)
        RaiseEvent WorkItemDoubleClick(objSender, objWorkItem)
    End Sub
End Class
