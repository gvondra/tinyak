Imports tinyak.Data
Friend Class clsSettings
    Implements IProcessingData

    Private m_objInnerSettings As ISettings
    Private m_objDatabaseConnection As IDbConnection
    Private m_objDatabaseTransaction As IDbTransaction

    Friend Sub New(ByVal objSettings As ISettings)
        m_objInnerSettings = objSettings
    End Sub

    Public ReadOnly Property ConnectionString As String Implements IProcessingData.ConnectionString
        Get
            Return m_objInnerSettings.ConnectionString
        End Get
    End Property

    Public Property DatabaseConnection As IDbConnection Implements IProcessingData.DatabaseConnection
        Get
            Return m_objDatabaseConnection
        End Get
        Set(value As IDbConnection)
            m_objDatabaseConnection = value
        End Set
    End Property

    Public Property DatabaseTransaction As IDbTransaction Implements IProcessingData.DatabaseTransaction
        Get
            Return m_objDatabaseTransaction
        End Get
        Set(value As IDbTransaction)
            m_objDatabaseTransaction = value
        End Set
    End Property
End Class
