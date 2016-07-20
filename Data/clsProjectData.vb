Public Class clsProjectData

    Friend Const COLLECTION_NAME As String = "Project"

    Private m_strId As String
    Private m_strTitle As String
    Private m_strOwner As String
    Private m_colTeamMembers As List(Of String)
    Private m_colFeatureIds As List(Of ObjectId)

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

    <BsonRequired>
    Public Property Title As String
        Get
            Return m_strTitle
        End Get
        Set
            m_strTitle = Value
        End Set
    End Property

    <BsonRequired>
    Public Property Owner As String
        Get
            Return m_strOwner
        End Get
        Set
            m_strOwner = Value
        End Set
    End Property

    Public Property TeamMembers As List(Of String)
        Get
            Return m_colTeamMembers
        End Get
        Set
            m_colTeamMembers = Value
        End Set
    End Property

    Public Property FeatureIds As List(Of ObjectId)
        Get
            Return m_colFeatureIds
        End Get
        Set
            m_colFeatureIds = Value
        End Set
    End Property
End Class
