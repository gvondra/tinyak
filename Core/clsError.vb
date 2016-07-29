Imports tinyak.Data
Public Class clsError
    Private m_objErrorData As clsErrorData

    Public Property Id As Nullable(Of Integer)
        Get
            Return m_objErrorData.Id
        End Get
        Set
            m_objErrorData.Id = Value
        End Set
    End Property

    Public Property MachineName As String
        Get
            Return m_objErrorData.MachineName
        End Get
        Set(value As String)
            m_objErrorData.MachineName = value
        End Set
    End Property

    Public Property AppDomainName As String
        Get
            Return m_objErrorData.AppDomainName
        End Get
        Set(value As String)
            m_objErrorData.AppDomainName = value
        End Set
    End Property

    Public Property ThreadIdentity As String
        Get
            Return m_objErrorData.ThreadIdentity
        End Get
        Set(value As String)
            m_objErrorData.ThreadIdentity = value
        End Set
    End Property

    Public Property WindowsIdentity As String
        Get
            Return m_objErrorData.WindowsIdentity
        End Get
        Set(value As String)
            m_objErrorData.WindowsIdentity = value
        End Set
    End Property

    Public Property CreateTimestamp As Date
        Get
            Return m_objErrorData.CreateTimestamp
        End Get
        Set(value As Date)
            m_objErrorData.CreateTimestamp = value
        End Set
    End Property
End Class
