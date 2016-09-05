Public Class clsFeatureListItemVM
    Private m_objInnerFeatureListItem As clsFeatureListItem

    Public Sub New(ByVal objFeatureListItem As clsFeatureListItem)
        m_objInnerFeatureListItem = objFeatureListItem
    End Sub

    Public Property Title As String
        Get
            Return m_objInnerFeatureListItem.Title
        End Get
        Set(value As String)
            m_objInnerFeatureListItem.Title = value
        End Set
    End Property
End Class
