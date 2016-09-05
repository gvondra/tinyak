Public Class clsFeaturesViewerVM
    Private m_objProject As clsProject
    Private m_objFeatureListVM As clsFeatureListVM

    Public Sub New()
        m_objFeatureListVM = New clsFeatureListVM
    End Sub

    Public Property Project As clsProject
        Get
            Return m_objProject
        End Get
        Set(value As clsProject)
            m_objProject = value
            m_objFeatureListVM.Project = value
        End Set
    End Property

    Public Property FeatureList As clsFeatureListVM
        Get
            Return m_objFeatureListVM
        End Get
        Set(value As clsFeatureListVM)
            m_objFeatureListVM = value
        End Set
    End Property

    Public Sub ShowAddFeature()
        m_objFeatureListVM.ShowAddFeature()
    End Sub
End Class
