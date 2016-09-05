Public Class uctFeatureList
    Private Sub uctAddFeature_OnHide(objSender As Control)
        DirectCast(DataContext, clsFeatureListVM).HideAddFeature()
    End Sub

    Private Sub uctAddFeature_AfterAddFeature(objSender As Control, objFeature As clsFeatureListItem)
        DirectCast(DataContext, clsFeatureListVM).FeatureListItems.Add(New clsFeatureListItemVM(objFeature))
    End Sub
End Class
