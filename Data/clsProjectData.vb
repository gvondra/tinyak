Public Class clsProjectData

    Friend Const TABLE_NAME As String = "Project"

    Private m_intId As Nullable(Of Integer)
    Private m_strTitle As String
    Private m_intOwnerId As Integer
    Private m_colTeamMembers As List(Of String)

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

    Public Property OwnerId As Integer
        Get
            Return m_intOwnerId
        End Get
        Set
            m_intOwnerId = Value
        End Set
    End Property

    Public Property TeamMembers As List(Of String)
        Get
            Return m_colTeamMembers
        End Get
        Set
            m_colTeamMembers = Value
        End Set
    End Property
End Class
