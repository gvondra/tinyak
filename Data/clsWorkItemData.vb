Public Class clsWorkItemData

    Friend Const TABLE_NAME As String = "WorkItem"

    Private m_intId As Nullable(Of Integer)
    Private m_strTitle As String
    Private m_intState As Int16
    Private m_intAssignedTo As Nullable(Of Integer)
    Private m_intEffort As Nullable(Of Int16)
    Private m_strDescription As String
    Private m_strAcceptanceCriteria As String
    Private m_colTasks As List(Of clsTaskData)

    Public Property Id As Nullable(Of Integer)
        Get
            Return m_intId
        End Get
        Set
            m_intId = Value
        End Set
    End Property

    Public Property Title As String
        Get
            Return m_strTitle
        End Get
        Set
            m_strTitle = Value
        End Set
    End Property

    Public Property State As Int16
        Get
            Return m_intState
        End Get
        Set
            m_intState = Value
        End Set
    End Property

    Public Property AssignedTo As Nullable(Of Integer)
        Get
            Return m_intAssignedTo
        End Get
        Set
            m_intAssignedTo = Value
        End Set
    End Property

    Public Property Effort As Nullable(Of Int16)
        Get
            Return m_intEffort
        End Get
        Set
            m_intEffort = Value
        End Set
    End Property

    Public Property Description As String
        Get
            Return m_strDescription
        End Get
        Set
            m_strDescription = Value
        End Set
    End Property

    Public Property AcceptanceCriteria As String
        Get
            Return m_strAcceptanceCriteria
        End Get
        Set
            m_strAcceptanceCriteria = Value
        End Set
    End Property

    Public Property Tasks As List(Of clsTaskData)
        Get
            Return m_colTasks
        End Get
        Set
            m_colTasks = Value
        End Set
    End Property
End Class
