Public Class clsProjectMemberData
    Private Sub New()
    End Sub

    Public Shared Sub Create(ByVal objSettings As ISettings, ByVal intProjectId As Integer, ByVal strEmailAddress As String)
        Dim objCommand As IDbCommand
        Dim objParameter As IDataParameter
        Dim objId As IDataParameter

        If objSettings.DatabaseConnection Is Nothing Then
            objSettings.DatabaseConnection = OpenConnection(objSettings)
        End If
        If objSettings.DatabaseTransaction Is Nothing Then
            objSettings.DatabaseTransaction = objSettings.DatabaseConnection.BeginTransaction
        End If
        objCommand = objSettings.DatabaseConnection.CreateCommand
        objCommand.CommandText = "tnyk.ISP_ProjectMember"
        objCommand.CommandType = CommandType.StoredProcedure
        objCommand.Transaction = objSettings.DatabaseTransaction

        objId = CreateParameter(objCommand, "id", DbType.Int32)
        objId.Direction = ParameterDirection.Output
        objCommand.Parameters.Add(objId)

        objParameter = CreateParameter(objCommand, "projectId", DbType.Int32)
        objParameter.Value = intProjectId
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "emailAddress", DbType.String)
        objParameter.Value = strEmailAddress
        objCommand.Parameters.Add(objParameter)

        objCommand.ExecuteNonQuery()
    End Sub

    Public Shared Sub Remove(ByVal objSettings As ISettings, ByVal intProjectId As Integer, ByVal strEmailAddress As String)
        Dim objCommand As IDbCommand
        Dim objParameter As IDataParameter

        If objSettings.DatabaseConnection Is Nothing Then
            objSettings.DatabaseConnection = OpenConnection(objSettings)
        End If
        If objSettings.DatabaseTransaction Is Nothing Then
            objSettings.DatabaseTransaction = objSettings.DatabaseConnection.BeginTransaction
        End If
        objCommand = objSettings.DatabaseConnection.CreateCommand
        objCommand.CommandText = "tnyk.DSP_ProjectMember_By_ProjectEmailAddress"
        objCommand.CommandType = CommandType.StoredProcedure
        objCommand.Transaction = objSettings.DatabaseTransaction

        objParameter = CreateParameter(objCommand, "projectId", DbType.Int32)
        objParameter.Value = intProjectId
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "emailAddress", DbType.String)
        objParameter.Value = strEmailAddress
        objCommand.Parameters.Add(objParameter)

        objCommand.ExecuteNonQuery()
    End Sub
End Class
