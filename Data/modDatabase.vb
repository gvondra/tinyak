Friend Module modDatabase
    Public Function OpenConnection(ByVal objProcessingData As ISettings) As IDbConnection
        Dim objConnection As SqlClient.SqlConnection

        objConnection = New SqlClient.SqlConnection(objProcessingData.ConnectionString)
        objConnection.Open()
        Return objConnection
    End Function

    Public Function CreateParameter(ByVal objCommand As IDbCommand, ByVal strName As String, ByVal intType As DbType) As IDataParameter
        Dim objPramater As IDataParameter

        objPramater = objCommand.CreateParameter()
        objPramater.ParameterName = strName
        objPramater.DbType = intType
        Return objPramater
    End Function
End Module
