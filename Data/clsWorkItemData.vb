Public Class clsWorkItemData
    Public Property Id As Nullable(Of Integer)
    Public Property ProjectId As Nullable(Of Integer)
    Public Property FeatureId As Nullable(Of Integer)
    Public Property Title As String
    Public Property State As Int16
    Public Property AssignedTo As Nullable(Of Integer)
    Public Property Effort As Nullable(Of Int16)
    Public Property Description As String
    Public Property AcceptanceCriteria As String

    Private Shared Sub Initialize(ByVal objReader As IDataReader, ByVal objWorkItem As clsWorkItemData)
        With objWorkItem
            .AcceptanceCriteria = objReader.GetString(objReader.GetOrdinal("AcceptanceCriteria"))
            If objReader.IsDBNull(objReader.GetOrdinal("AssignedTo")) = False Then
                .AssignedTo = objReader.GetInt32(objReader.GetOrdinal("AssignedTo"))
            Else
                .AssignedTo = Nothing
            End If
            .Description = objReader.GetString(objReader.GetOrdinal("Description"))
            If objReader.IsDBNull(objReader.GetOrdinal("")) = False Then
                .Effort = objReader.GetInt16(objReader.GetOrdinal("Effort"))
            Else
                .Effort = Nothing
            End If
            .FeatureId = objReader.GetInt32(objReader.GetOrdinal("FeatureId"))
            .Id = objReader.GetInt32(objReader.GetOrdinal("Id"))
            .ProjectId = objReader.GetInt32(objReader.GetOrdinal("ProjectId"))
            .State = objReader.GetInt16(objReader.GetOrdinal("State"))
            .Title = objReader.GetString(objReader.GetOrdinal("Title")).TrimEnd
        End With
    End Sub

    Public Shared Function GetByFeatureId(ByVal objSettings As ISettings, ByVal intFeatureId As Integer) As List(Of clsWorkItemData)
        Dim objConnection As IDbConnection
        Dim objCommand As IDbCommand
        Dim objParameter As IDataParameter
        Dim objReader As IDataReader
        Dim colResult As List(Of clsWorkItemData)
        Dim objWorkItem As clsWorkItemData

        objConnection = OpenConnection(objSettings)
        Try
            objCommand = objConnection.CreateCommand()
            objCommand.CommandText = "tnyk.SSP_WorkItem_By_Feature"
            objCommand.CommandType = CommandType.StoredProcedure

            objParameter = CreateParameter(objCommand, "featureId", DbType.Int32)
            objParameter.Value = intFeatureId
            objCommand.Parameters.Add(objParameter)

            objReader = objCommand.ExecuteReader
            colResult = New List(Of clsWorkItemData)
            While objReader.Read
                objWorkItem = New clsWorkItemData
                Initialize(objReader, objWorkItem)
                colResult.Add(objWorkItem)
            End While

            objConnection.Close()
        Finally
            objConnection.Dispose()
        End Try
        Return colResult
    End Function

    Public Sub Create(ByVal objSettings As ISettings)
        Dim objCommand As IDbCommand
        Dim objParameter As IDataParameter
        Dim objFeatureId As IDataParameter

        If objSettings.DatabaseConnection Is Nothing Then
            objSettings.DatabaseConnection = OpenConnection(objSettings)
        End If
        If objSettings.DatabaseTransaction Is Nothing Then
            objSettings.DatabaseTransaction = objSettings.DatabaseConnection.BeginTransaction
        End If
        objCommand = objSettings.DatabaseConnection.CreateCommand
        objCommand.CommandText = "tnyk.ISP_WorkItem"
        objCommand.CommandType = CommandType.StoredProcedure
        objCommand.Transaction = objSettings.DatabaseTransaction

        objFeatureId = CreateParameter(objCommand, "id", DbType.Int32)
        objFeatureId.Direction = ParameterDirection.Output
        objCommand.Parameters.Add(objFeatureId)

        objParameter = CreateParameter(objCommand, "projectId", DbType.Int32)
        objParameter.Value = ProjectId.Value
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "featureId", DbType.Int32)
        objParameter.Value = FeatureId.Value
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "title", DbType.String)
        objParameter.Value = Title
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "state", DbType.Int16)
        objParameter.Value = State
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "assignedTo", DbType.Int32)
        If AssignedTo.HasValue Then
            objParameter.Value = AssignedTo.Value
        Else
            objParameter.Value = DBNull.Value
        End If
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "effort", DbType.Int16)
        If Effort.HasValue Then
            objParameter.Value = Effort.Value
        Else
            objParameter.Value = DBNull.Value
        End If
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "description", DbType.String)
        objParameter.Value = Description
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "acceptanceCriteria", DbType.String)
        objParameter.Value = AcceptanceCriteria
        objCommand.Parameters.Add(objParameter)

        objCommand.ExecuteNonQuery()

        Id = CType(objFeatureId.Value, Int32)
    End Sub

    Public Sub Update(ByVal objSettings As ISettings)
        Dim objCommand As IDbCommand
        Dim objParameter As IDataParameter

        If objSettings.DatabaseConnection Is Nothing Then
            objSettings.DatabaseConnection = OpenConnection(objSettings)
        End If
        If objSettings.DatabaseTransaction Is Nothing Then
            objSettings.DatabaseTransaction = objSettings.DatabaseConnection.BeginTransaction
        End If
        objCommand = objSettings.DatabaseConnection.CreateCommand
        objCommand.CommandText = "tnyk.USP_WorkItem"
        objCommand.CommandType = CommandType.StoredProcedure
        objCommand.Transaction = objSettings.DatabaseTransaction

        objParameter = CreateParameter(objCommand, "id", DbType.Int32)
        objParameter.Direction = ParameterDirection.Output
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "featureId", DbType.Int32)
        objParameter.Value = FeatureId.Value
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "title", DbType.String)
        objParameter.Value = Title
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "state", DbType.Int16)
        objParameter.Value = State
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "assignedTo", DbType.Int32)
        If AssignedTo.HasValue Then
            objParameter.Value = AssignedTo.Value
        Else
            objParameter.Value = DBNull.Value
        End If
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "effort", DbType.Int16)
        If Effort.HasValue Then
            objParameter.Value = Effort.Value
        Else
            objParameter.Value = DBNull.Value
        End If
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "description", DbType.String)
        objParameter.Value = Description
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "acceptanceCriteria", DbType.String)
        objParameter.Value = AcceptanceCriteria
        objCommand.Parameters.Add(objParameter)

        objCommand.ExecuteNonQuery()
    End Sub

    Public Shared Function Delete(ByVal objSettings As ISettings, ByVal intId As Integer) As Integer
        Dim objCommand As IDbCommand
        Dim objParameter As IDataParameter

        If objSettings.DatabaseConnection Is Nothing Then
            objSettings.DatabaseConnection = OpenConnection(objSettings)
        End If
        If objSettings.DatabaseTransaction Is Nothing Then
            objSettings.DatabaseTransaction = objSettings.DatabaseConnection.BeginTransaction
        End If
        objCommand = objSettings.DatabaseConnection.CreateCommand
        objCommand.CommandText = "tnyk.DSP_WorkItem"
        objCommand.CommandType = CommandType.StoredProcedure
        objCommand.Transaction = objSettings.DatabaseTransaction

        objParameter = CreateParameter(objCommand, "id", DbType.Int32)
        objParameter.Value = intId
        objCommand.Parameters.Add(objParameter)

        Return objCommand.ExecuteNonQuery()
    End Function
End Class
