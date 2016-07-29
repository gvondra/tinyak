Imports tinyak.Data
Public Class clsFeature
    Private m_objFeatureData As clsFeatureDAta

    Public Property Id As Nullable(Of Integer)
        Get
            Return m_objFeatureData.Id
        End Get
        Set
            m_objFeatureData.Id = Value
        End Set
    End Property

    Public Property Title As String
        Get
            Return m_objFeatureData.Title
        End Get
        Set
            m_objFeatureData.Title = Value
        End Set
    End Property
End Class
