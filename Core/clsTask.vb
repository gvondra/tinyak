Imports tinyak.Data
Public Class clsTask
    Private m_objTaskData As clsTaskData

    Public Property Id As Nullable(Of Integer)
        Get
            Return m_objTaskData.Id
        End Get
        Set
            m_objTaskData.Id = Value
        End Set
    End Property

    Public Property Title As String
        Get
            Return m_objTaskData.Title
        End Get
        Set
            m_objTaskData.Title = Value
        End Set
    End Property

    Public Property Status As Int16
        Get
            Return m_objTaskData.Status
        End Get
        Set
            m_objTaskData.Status = Value
        End Set
    End Property

    Public Property AssignedTo As Nullable(Of Integer)
        Get
            Return m_objTaskData.AssignedTo
        End Get
        Set
            m_objTaskData.AssignedTo = Value
        End Set
    End Property

    Public Property Remaining As Nullable(Of Int16)
        Get
            Return m_objTaskData.Remaining
        End Get
        Set
            m_objTaskData.Remaining = Value
        End Set
    End Property

    Public Property Description As String
        Get
            Return m_objTaskData.Description
        End Get
        Set
            m_objTaskData.Description = Value
        End Set
    End Property
End Class
