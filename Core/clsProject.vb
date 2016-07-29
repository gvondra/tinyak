Imports tinyak.Data
Public Class clsProject
    Private m_objProjectData As clsProjectData

    Public Property Id As Nullable(Of Integer)
        Get
            Return m_objProjectData.Id
        End Get
        Set
            m_objProjectData.Id = Value
        End Set
    End Property

    Public Property Title As String
        Get
            Return m_objProjectData.Title
        End Get
        Set
            m_objProjectData.Title = Value
        End Set
    End Property

    Public Property OwnerId As Integer
        Get
            Return m_objProjectData.OwnerId
        End Get
        Set
            m_objProjectData.OwnerId = Value
        End Set
    End Property

    Public Property TeamMembers As List(Of String)
        Get
            Return m_objProjectData.TeamMembers
        End Get
        Set
            m_objProjectData.TeamMembers = Value
        End Set
    End Property
End Class
