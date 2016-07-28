Public Class clsServiceCallData

    Friend Const TABLE_NAME As String = "ServiceCall"

    Private m_intId As Nullable(Of Integer)
    Private m_strUser As String
    Private m_strEndpointName As String
    Private m_strMethodName As String
    Private m_dblDuration As Double
    Private m_datCreateTimestamp As Date

    Public Sub New()
        CreateTimestamp = Date.UtcNow
    End Sub

    Public Property Id As Nullable(Of Integer)
        Get
            Return m_intId
        End Get
        Set
            m_intId = Value
        End Set
    End Property

    Public Property User As String
        Get
            Return m_strUser
        End Get
        Set(value As String)
            m_strUser = value
        End Set
    End Property

    Public Property EndpointName As String
        Get
            Return m_strEndpointName
        End Get
        Set(value As String)
            m_strEndpointName = value
        End Set
    End Property

    Public Property MethodName As String
        Get
            Return m_strMethodName
        End Get
        Set(value As String)
            m_strMethodName = value
        End Set
    End Property

    Public Property Duration As Double
        Get
            Return m_dblDuration
        End Get
        Set(value As Double)
            m_dblDuration = value
        End Set
    End Property

    Public Property CreateTimestamp As Date
        Get
            Return m_datCreateTimestamp
        End Get
        Set(value As Date)
            m_datCreateTimestamp = value
        End Set
    End Property
End Class
