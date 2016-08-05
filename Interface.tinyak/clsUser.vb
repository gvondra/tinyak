Public Class clsUser
    Private m_objUser As tinyak.UserServiceReference.User
    Friend Sub New(ByVal objUser As tinyak.UserServiceReference.User)
        m_objUser = objUser
    End Sub

    Public Sub New()
        m_objUser = New tinyak.UserServiceReference.User
    End Sub

    Public Property Id As Nullable(Of Integer)
        Get
            Return m_objUser.Id
        End Get
        Set
            m_objUser.Id = Value
        End Set
    End Property

    Public Property Name As String
        Get
            Return m_objUser.Name
        End Get
        Set
            m_objUser.Name = Value
        End Set
    End Property

    Public Property EmailAddress As String
        Get
            Return m_objUser.EmailAddress
        End Get
        Set
            m_objUser.EmailAddress = Value
        End Set
    End Property

    Friend ReadOnly Property InnerUser As tinyak.UserServiceReference.User
        Get
            Return m_objUser
        End Get
    End Property
End Class
