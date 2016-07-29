Imports tinyak.Data
Public Class clsException
    Private m_objExceptionData As clsExceptionData

    Public Property Id As Nullable(Of Integer)
        Get
            Return m_objExceptionData.Id
        End Get
        Set
            m_objExceptionData.Id = Value
        End Set
    End Property

    Public Property TypeName As String
        Get
            Return m_objExceptionData.TypeName
        End Get
        Set(value As String)
            m_objExceptionData.TypeName = value
        End Set
    End Property

    Public Property Message As String
        Get
            Return m_objExceptionData.Message
        End Get
        Set(value As String)
            m_objExceptionData.Message = value
        End Set
    End Property

    Public Property Source As String
        Get
            Return m_objExceptionData.Source
        End Get
        Set(value As String)
            m_objExceptionData.Source = value
        End Set
    End Property

    Public Property Target As String
        Get
            Return m_objExceptionData.Target
        End Get
        Set(value As String)
            m_objExceptionData.Target = value
        End Set
    End Property

    Public Property StackTrace As String
        Get
            Return m_objExceptionData.StackTrace
        End Get
        Set(value As String)
            m_objExceptionData.StackTrace = value
        End Set
    End Property

    Public Property HResult As Integer
        Get
            Return m_objExceptionData.HResult
        End Get
        Set(value As Integer)
            m_objExceptionData.HResult = value
        End Set
    End Property
End Class
