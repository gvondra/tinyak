Public Interface ISettings

    ReadOnly Property ConnectionString() As String
    Property DatabaseConnection As IDbConnection
    Property DatabaseTransaction As IDbTransaction
End Interface
