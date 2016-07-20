Friend Class clsDatabase
    Private m_objClient As MongoClient
    Private m_objInnerDatabase As IMongoDatabase

    Friend Sub New(ByVal objProcessingData As IProcessingData)
        m_objClient = New MongoClient(objProcessingData.ConnectionString)
        m_objInnerDatabase = m_objClient.GetDatabase(objProcessingData.DatabaseName)
    End Sub

    Public Function GetCollection(Of T)(ByVal strName As String) As IMongoCollection(Of T)
        Return m_objInnerDatabase.GetCollection(Of T)(strName)
    End Function
End Class
