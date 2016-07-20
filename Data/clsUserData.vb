Public Class clsUserData

    Friend Const COLLECTION_NAME As String = "tUser"

    Private m_strId As String
    Private m_strName As String
    Private m_strEmailAddress As String
    Private m_bytPasswordToken As Byte()

    Public Sub New()
        m_strId = ObjectId.GenerateNewId.ToString
    End Sub

    <BsonId(), BsonRepresentation(BsonType.ObjectId)>
    Public Property Id As String
        Get
            Return m_strId
        End Get
        Set(value As String)
            m_strId = value
        End Set
    End Property

    <BsonRequired()>
    Public Property Name As String
        Get
            Return m_strName
        End Get
        Set
            m_strName = Value
        End Set
    End Property

    <BsonRequired()>
    Public Property EmailAddress As String
        Get
            Return m_strEmailAddress
        End Get
        Set
            m_strEmailAddress = Value
        End Set
    End Property

    <BsonRequired()>
    Public Property PasswordToken As Byte()
        Get
            Return m_bytPasswordToken
        End Get
        Set
            m_bytPasswordToken = Value
        End Set
    End Property
End Class
