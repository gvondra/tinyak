Public Class clsFeatureData

    Public Property Id As Nullable(Of Integer)
    Public Property ProjectId As Nullable(Of Integer)
    Public Property Title As String

    Private Shared Sub Initialize(ByVal objReader As IDataReader, ByVal objFeature As clsFeatureData)
        With objFeature
            .Id = objReader.GetInt32(objReader.GetOrdinal("FeatureId"))
            .ProjectId = objReader.GetInt32(objReader.GetOrdinal("ProjectId"))
            .Title = objReader.GetString(objReader.GetOrdinal("Title")).TrimEnd
        End With
    End Sub

    Public Shared Function [Get](ByVal objSettings As ISettings, ByVal intId As Integer) As clsFeatureData
        Dim objConnection As IDbConnection
        Dim objCommand As IDbCommand
        Dim objParameter As IDataParameter
        Dim objReader As IDataReader
        Dim objResult As clsFeatureData

        objConnection = OpenConnection(objSettings)
        Try
            objCommand = objConnection.CreateCommand()
            objCommand.CommandText = "tnyk.SSP_Feature"
            objCommand.CommandType = CommandType.StoredProcedure

            objParameter = CreateParameter(objCommand, "id", DbType.Int32)
            objParameter.Value = intId
            objCommand.Parameters.Add(objParameter)

            objReader = objCommand.ExecuteReader
            If objReader.Read Then
                objResult = New clsFeatureData
                Initialize(objReader, objResult)
            Else
                objResult = Nothing
            End If

            objConnection.Close()
        Finally
            objConnection.Dispose()
        End Try
        Return objResult
    End Function

    Public Shared Function GetByProjectId(ByVal objSettings As ISettings, ByVal intProjectId As Integer) As List(Of clsFeatureData)
        Dim objConnection As IDbConnection
        Dim objCommand As IDbCommand
        Dim objParameter As IDataParameter
        Dim objReader As IDataReader
        Dim colResult As List(Of clsFeatureData)
        Dim objFeature As clsFeatureData

        objConnection = OpenConnection(objSettings)
        Try
            objCommand = objConnection.CreateCommand()
            objCommand.CommandText = "tnyk.SSP_Feature_By_Project"
            objCommand.CommandType = CommandType.StoredProcedure

            objParameter = CreateParameter(objCommand, "projectId", DbType.Int32)
            objParameter.Value = intProjectId
            objCommand.Parameters.Add(objParameter)

            objReader = objCommand.ExecuteReader
            colResult = New List(Of clsFeatureData)
            While objReader.Read
                objFeature = New clsFeatureData
                Initialize(objReader, objFeature)
                colResult.Add(objFeature)
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
        objCommand.CommandText = "tnyk.ISP_Feature"
        objCommand.CommandType = CommandType.StoredProcedure
        objCommand.Transaction = objSettings.DatabaseTransaction

        objFeatureId = CreateParameter(objCommand, "id", DbType.Int32)
        objFeatureId.Direction = ParameterDirection.Output
        objCommand.Parameters.Add(objFeatureId)

        objParameter = CreateParameter(objCommand, "projectId", DbType.Int32)
        objParameter.Value = ProjectId.Value
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "title", DbType.String)
        objParameter.Value = Title
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
        objCommand.CommandText = "tnyk.USP_Feature"
        objCommand.CommandType = CommandType.StoredProcedure
        objCommand.Transaction = objSettings.DatabaseTransaction

        objParameter = CreateParameter(objCommand, "id", DbType.Int32)
        objParameter.Value = Id.Value
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "title", DbType.String)
        objParameter.Value = Title
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
        objCommand.CommandText = "tnyk.DSP_Feature"
        objCommand.CommandType = CommandType.StoredProcedure
        objCommand.Transaction = objSettings.DatabaseTransaction

        objParameter = CreateParameter(objCommand, "id", DbType.Int32)
        objParameter.Value = intId
        objCommand.Parameters.Add(objParameter)

        Return objCommand.ExecuteNonQuery()
    End Function
End Class
