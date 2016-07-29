Imports tinyak.Data
Public Class clsSession
    Private m_objSessionData As clsSessionData

    Public Property Id As Guid
        Get
            Return m_objSessionData.Id
        End Get
        Set
            m_objSessionData.Id = Value
        End Set
    End Property

    Public Property UserId As Nullable(Of Integer)
        Get
            Return m_objSessionData.UserId
        End Get
        Set
            m_objSessionData.UserId = Value
        End Set
    End Property
End Class
