Public Class clsSessionData

    Private Const TABLE_NAME As String = "Session"

    Private m_intId As Nullable(Of Integer)
    Private m_intUserId As Nullable(Of Integer)

    Public Property Id As Nullable(Of Integer)
        Get
            Return m_intId
        End Get
        Set
            m_intId = Value
        End Set
    End Property

    Public Property UserId As Nullable(Of Integer)
        Get
            Return m_intUserId
        End Get
        Set
            m_intUserId = Value
        End Set
    End Property

End Class
