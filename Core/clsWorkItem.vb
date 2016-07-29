Imports tinyak.Data
Public Class clsWorkItem
    Private m_objWorkItemData As clsWorkItemData

    Public Property Id As Nullable(Of Integer)
        Get
            Return m_objWorkItemData.Id
        End Get
        Set
            m_objWorkItemData.Id = Value
        End Set
    End Property

    Public Property Title As String
        Get
            Return m_objWorkItemData.Title
        End Get
        Set
            m_objWorkItemData.Title = Value
        End Set
    End Property

    Public Property State As Int16
        Get
            Return m_objWorkItemData.State
        End Get
        Set
            m_objWorkItemData.State = Value
        End Set
    End Property

    Public Property AssignedTo As Nullable(Of Integer)
        Get
            Return m_objWorkItemData.AssignedTo
        End Get
        Set
            m_objWorkItemData.AssignedTo = Value
        End Set
    End Property

    Public Property Effort As Nullable(Of Int16)
        Get
            Return m_objWorkItemData.Effort
        End Get
        Set
            m_objWorkItemData.Effort = Value
        End Set
    End Property

    Public Property Description As String
        Get
            Return m_objWorkItemData.Description
        End Get
        Set
            m_objWorkItemData.Description = Value
        End Set
    End Property

    Public Property AcceptanceCriteria As String
        Get
            Return m_objWorkItemData.AcceptanceCriteria
        End Get
        Set
            m_objWorkItemData.AcceptanceCriteria = Value
        End Set
    End Property
End Class
