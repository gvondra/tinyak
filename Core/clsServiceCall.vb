Imports tinyak.Data
Public Class clsServiceCall
    Private m_objServiceCallData As clsServiceCallData

    Public Property Id As Nullable(Of Integer)
        Get
            Return m_objServiceCallData.Id
        End Get
        Set
            m_objServiceCallData.Id = Value
        End Set
    End Property

    Public Property User As String
        Get
            Return m_objServiceCallData.User
        End Get
        Set(value As String)
            m_objServiceCallData.User = value
        End Set
    End Property

    Public Property EndpointName As String
        Get
            Return m_objServiceCallData.EndpointName
        End Get
        Set(value As String)
            m_objServiceCallData.EndpointName = value
        End Set
    End Property

    Public Property MethodName As String
        Get
            Return m_objServiceCallData.MethodName
        End Get
        Set(value As String)
            m_objServiceCallData.MethodName = value
        End Set
    End Property

    Public Property Duration As Double
        Get
            Return m_objServiceCallData.Duration
        End Get
        Set(value As Double)
            m_objServiceCallData.Duration = value
        End Set
    End Property

    Public Property CreateTimestamp As Date
        Get
            Return m_objServiceCallData.CreateTimestamp
        End Get
        Set(value As Date)
            m_objServiceCallData.CreateTimestamp = value
        End Set
    End Property
End Class
