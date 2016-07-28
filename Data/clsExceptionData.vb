Public Class clsExceptionData
    Friend Const TABLE_NAME As String = "Exception"

    Private m_intId As Nullable(Of Integer)
    Private m_strTypeName As String
    Private m_strMessage As String
    Private m_strSource As String
    Private m_strTarget As String
    Private m_strStackTrace As String
    Private m_intHResult As Integer
    Private m_objData As Dictionary(Of String, String)
    Private m_objInnerException As clsExceptionData

    Public Property Id As Nullable(Of Integer)
        Get
            Return m_intId
        End Get
        Set
            m_intId = Value
        End Set
    End Property

    Public Property TypeName As String
        Get
            Return m_strTypeName
        End Get
        Set(value As String)
            m_strTypeName = value
        End Set
    End Property

    Public Property Message As String
        Get
            Return m_strMessage
        End Get
        Set(value As String)
            m_strMessage = value
        End Set
    End Property

    Public Property Source As String
        Get
            Return m_strSource
        End Get
        Set(value As String)
            m_strSource = value
        End Set
    End Property

    Public Property Target As String
        Get
            Return m_strTarget
        End Get
        Set(value As String)
            m_strTarget = value
        End Set
    End Property

    Public Property StackTrace As String
        Get
            Return m_strStackTrace
        End Get
        Set(value As String)
            m_strStackTrace = value
        End Set
    End Property

    Public Property HResult As Integer
        Get
            Return m_intHResult
        End Get
        Set(value As Integer)
            m_intHResult = value
        End Set
    End Property

    Public Property Data As Dictionary(Of String, String)
        Get
            Return m_objData
        End Get
        Set(value As Dictionary(Of String, String))
            m_objData = value
        End Set
    End Property

    Public Property InnerException As clsExceptionData
        Get
            Return m_objInnerException
        End Get
        Set(value As clsExceptionData)
            m_objInnerException = value
        End Set
    End Property
End Class
