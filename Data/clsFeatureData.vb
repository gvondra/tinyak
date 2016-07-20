Public Class clsFeatureDAta

    Friend Const COLLECTION_NAME As String = "Feature"

    Private m_strId As String
    Private m_strTitle As String
    Private m_colWorkItems As List(Of String)

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

    Public Property WorkItems As List(Of String)
        Get
            Return m_colWorkItems
        End Get
        Set
            m_colWorkItems = Value
        End Set
    End Property
End Class
