Public Class clsControllerBase
    Inherits Controller

    Private m_objSettings As clsSettings
    Private m_objSession As clsSession

    Public Sub New(ByVal objSettings As clsSettings, ByVal objSession As clsSession)
        m_objSettings = objSettings
        m_objSession = objSession
    End Sub

    Protected ReadOnly Property Settings As clsSettings
        Get
            Return m_objSettings
        End Get
    End Property

    Protected Shadows ReadOnly Property Session As clsSession
        Get
            Return m_objSession
        End Get
    End Property
End Class
