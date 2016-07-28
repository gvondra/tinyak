Public Class clsUserData

    Friend Const TABLE_NAME As String = "User"

    Private m_intId As Nullable(Of Integer)
    Private m_strName As String
    Private m_strEmailAddress As String
    Private m_bytPasswordToken As Byte()
    Private m_blnIsAdminstrator As Boolean

    Public Property Id As Nullable(Of Integer)
        Get
            Return m_intId
        End Get
        Set
            m_intId = Value
        End Set
    End Property

    Public Property Name As String
        Get
            Return m_strName
        End Get
        Set
            m_strName = Value
        End Set
    End Property

    Public Property EmailAddress As String
        Get
            Return m_strEmailAddress
        End Get
        Set
            m_strEmailAddress = Value
        End Set
    End Property

    Public Property PasswordToken As Byte()
        Get
            Return m_bytPasswordToken
        End Get
        Set
            m_bytPasswordToken = Value
        End Set
    End Property

    Public Property IsAdministrator As Boolean
        Get
            Return m_blnIsAdminstrator
        End Get
        Set
            m_blnIsAdminstrator = Value
        End Set
    End Property
End Class
