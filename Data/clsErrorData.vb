<BsonIgnoreExtraElements()> _
Public Class clsErrorData

    Friend Const COLLECTION_NAME As String = "ErrorLog"

    Private m_strId As String
    Private m_datCreateTimestamp As Date
    Private m_strMachineName As String
    Private m_strAppDomainName As String
    Private m_strThreadIdentity As String
    Private m_strWindowsIdentity As String
    Private m_objException As clsExceptionData

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

    <BsonIgnoreIfNull()> _
    Public Property Exception As clsExceptionData
        Get
            Return m_objException
        End Get
        Set(value As clsExceptionData)
            m_objException = value
        End Set
    End Property

    Public Sub Insert(ByVal objProcessingData As IProcessingData)
        Dim objCollection As IMongoCollection(Of clsErrorData)
        Dim objDatabase As clsDatabase

        objDatabase = New clsDatabase(objProcessingData)
        objCollection = objDatabase.GetCollection(Of clsErrorData)(COLLECTION_NAME)
        objCollection.InsertOne(Me)
    End Sub

    Shared Function GetAll(ByVal objProcessingData As IProcessingData) As IEnumerable(Of clsErrorData)
        Dim objCollection As IMongoCollection(Of clsErrorData)
        Dim objDatabase As clsDatabase
        Dim objCursor As IAsyncCursor(Of clsErrorData)
        Dim objFilter As FilterDefinitionBuilder(Of clsErrorData)

        objDatabase = New clsDatabase(objProcessingData)
        objCollection = objDatabase.GetCollection(Of clsErrorData)(COLLECTION_NAME)

        objFilter = New FilterDefinitionBuilder(Of clsErrorData)()

        objCursor = objCollection.FindSync(objFilter.Empty)
        Return objCursor.Current
    End Function
End Class
