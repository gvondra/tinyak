Public Class clsTaskData
    Private m_strTitle As String
    Private m_intStatus As Int16
    Private m_strAssignedTo As String
    Private m_intRemaining As Nullable(Of Int16)
    Private m_strDescription As String

    <BsonRequired>
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

    Public Property AssignedTo As String
        Get
            Return m_strAssignedTo
        End Get
        Set
            m_strAssignedTo = Value
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
