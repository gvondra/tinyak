Public Class uctFeatureList
    Public Event FeatureDoubleClick(ByVal objSender As Object, ByVal objFeature As clsFeatureListItemVM)

    Private Sub uctAddFeature_OnHide(objSender As Control)
        DirectCast(DataContext, clsFeatureListVM).HideAddFeature()
    End Sub

    Private Sub uctAddFeature_AfterAddFeature(objSender As Control, objFeature As clsFeatureListItem)
        Dim objVm As clsFeatureListItemVM

        objVm = New clsFeatureListItemVM(objFeature)
        objVm.Project = DirectCast(DataContext, clsFeatureListVM).Project
        DirectCast(DataContext, clsFeatureListVM).FeatureListItems.Add(New clsFeatureListItemVM(objFeature))
    End Sub

    Private Sub uctFeatureListItem_FeatureDoubleClick(objSender As Object, objFeature As clsFeatureListItemVM)
        RaiseEvent FeatureDoubleClick(objSender, objFeature)
    End Sub
End Class
