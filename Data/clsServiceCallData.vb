Public Class clsServiceCallData

    Friend Const COLLECTION_NAME As String = "ServiceCall"

    Private m_strId As String
    Private m_strUser As String
    Private m_strEndpointName As String
    Private m_strMethodName As String
    Private m_dblDuration As Double
    Private m_datCreateTimestamp As Date

    Public Sub New()
        m_strId = ObjectId.GenerateNewId.ToString
        CreateTimestamp = Date.UtcNow
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

    Public Sub Insert(ByVal objProcessingData As IProcessingData)
        Dim objCollection As IMongoCollection(Of clsServiceCallData)
        Dim objDatabase As clsDatabase

        objDatabase = New clsDatabase(objProcessingData)
        objCollection = objDatabase.GetCollection(Of clsServiceCallData)(COLLECTION_NAME)
        objCollection.InsertOne(Me)
    End Sub

    Public Shared Function GetAll(ByVal objProcessingData As IProcessingData) As IEnumerable(Of clsServiceCallData)
        Dim objCollection As IMongoCollection(Of clsServiceCallData)
        Dim objDatabase As clsDatabase
        Dim objCursor As IAsyncCursor(Of clsServiceCallData)
        Dim objFilterBuilder As FilterDefinitionBuilder(Of clsServiceCallData)

        objDatabase = New clsDatabase(objProcessingData)
        objCollection = objDatabase.GetCollection(Of clsServiceCallData)(COLLECTION_NAME)
        objFilterBuilder = New FilterDefinitionBuilder(Of clsServiceCallData)
        objCursor = objCollection.FindSync(objFilterBuilder.Empty)
        Return objCursor.Current
    End Function
End Class
