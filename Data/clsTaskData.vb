Public Class clsTaskData
    Private m_intId As Nullable(Of Integer)
    Private m_strTitle As String
    Private m_intStatus As Int16
    Private m_intAssignedTo As Nullable(Of Integer)
    Private m_intRemaining As Nullable(Of Int16)
    Private m_strDescription As String

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

    Public Property Status As Int16
        Get
            Return m_intStatus
        End Get
        Set
            m_intStatus = Value
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

    Public Property Remaining As Nullable(Of Int16)
        Get
            Return m_intRemaining
        End Get
        Set
            m_intRemaining = Value
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
End Class
