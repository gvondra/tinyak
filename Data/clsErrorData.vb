Public Class clsErrorData

    Friend Const TABLE_NAME As String = "ErrorLog"

    Private m_intId As Nullable(Of Integer)
    Private m_datCreateTimestamp As Date
    Private m_strMachineName As String
    Private m_strAppDomainName As String
    Private m_strThreadIdentity As String
    Private m_strWindowsIdentity As String
    Private m_objException As clsExceptionData

    Public Property Id As Nullable(Of Integer)
        Get
            Return m_intId
        End Get
        Set
            m_intId = Value
        End Set
    End Property

    Public Property MachineName As String
        Get
            Return m_strMachineName
        End Get
        Set(value As String)
            m_strMachineName = value
        End Set
    End Property

    Public Property AppDomainName As String
        Get
            Return m_strAppDomainName
        End Get
        Set(value As String)
            m_strAppDomainName = value
        End Set
    End Property

    Public Property ThreadIdentity As String
        Get
            Return m_strThreadIdentity
        End Get
        Set(value As String)
            m_strThreadIdentity = value
        End Set
    End Property

    Public Property WindowsIdentity As String
        Get
            Return m_strWindowsIdentity
        End Get
        Set(value As String)
            m_strWindowsIdentity = value
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

    Public Property Exception As clsExceptionData
        Get
            Return m_objException
        End Get
        Set(value As clsExceptionData)
            m_objException = value
        End Set
    End Property
End Class
