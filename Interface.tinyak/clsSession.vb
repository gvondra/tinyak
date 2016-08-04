Imports System.Collections.Specialized
Public Class clsSession
    Private m_objSessionData As SessionService.Session

    Friend Sub New(ByVal objInnerSession As SessionService.Session)
        m_objSessionData = objInnerSession
    End Sub

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

    Public Property ExpirationDate As Date
        Get
            Return m_objSessionData.ExpirationDate
        End Get
        Set
            m_objSessionData.ExpirationDate = Value
        End Set
    End Property

    Public Property Data As NameValueCollection
        Get
            Return m_objSessionData.Data
        End Get
        Set
            m_objSessionData.Data = Value
        End Set
    End Property

    Friend ReadOnly Property InnerSession As SessionService.Session
        Get
            Return m_objSessionData
        End Get
    End Property
End Class
