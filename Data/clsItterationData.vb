Public Class clsItterationData
    Public Property Id As Nullable(Of Integer)
    Public Property ProjectId As Nullable(Of Integer)
    Public Property Name As String
    Public Property StartDate As Nullable(Of Date)
    Public Property EndDate As Nullable(Of Date)
    Public Property IsActive As Boolean

    Private Shared Sub Initialize(ByVal objReader As IDataReader, ByVal objItteration As clsItterationData)
        With objItteration
            If objReader.IsDBNull(objReader.GetOrdinal("EndDate")) = False Then
                .EndDate = objReader.GetDateTime(objReader.GetOrdinal("EndDate"))
            Else
                .EndDate = Nothing
            End If
            .Id = objReader.GetInt32(objReader.GetOrdinal("ItterationId"))
            .IsActive = objReader.GetBoolean(objReader.GetOrdinal("IsActive"))
            .Name = objReader.GetString(objReader.GetOrdinal("Name")).Trim
            .ProjectId = objReader.GetInt32(objReader.GetOrdinal("ProjectId"))
            If objReader.IsDBNull(objReader.GetOrdinal("StartDate")) = False Then
                .StartDate = objReader.GetDateTime(objReader.GetOrdinal("StartDate"))
            Else
                .StartDate = Nothing
            End If
        End With
    End Sub

    Public Shared Function [Get](ByVal objSettings As ISettings, ByVal intId As Integer) As clsItterationData
        Dim objConnection As IDbConnection
        Dim objCommand As IDbCommand
        Dim objParameter As IDataParameter
        Dim objReader As IDataReader
        Dim objResult As clsItterationData

        objConnection = OpenConnection(objSettings)
        Try
            objCommand = objConnection.CreateCommand()
            objCommand.CommandText = "tnyk.SSP_Itteration"
            objCommand.CommandType = CommandType.StoredProcedure

            objParameter = CreateParameter(objCommand, "id", DbType.Int32)
            objParameter.Value = intId
            objCommand.Parameters.Add(objParameter)

            objReader = objCommand.ExecuteReader
            If objReader.Read Then
                objResult = New clsItterationData
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

    Public Shared Function GetByProjectId(ByVal objSettings As ISettings, ByVal intProjectId As Integer) As List(Of clsItterationData)
        Dim objConnection As IDbConnection
        Dim objCommand As IDbCommand
        Dim objParameter As IDataParameter
        Dim objReader As IDataReader
        Dim colResult As List(Of clsItterationData)
        Dim objWorkItem As clsItterationData

        objConnection = OpenConnection(objSettings)
        Try
            objCommand = objConnection.CreateCommand()
            objCommand.CommandText = "tnyk.SSP_Itteration_By_Project"
            objCommand.CommandType = CommandType.StoredProcedure

            objParameter = CreateParameter(objCommand, "projectId", DbType.Int32)
            objParameter.Value = intProjectId
            objCommand.Parameters.Add(objParameter)

            objReader = objCommand.ExecuteReader
            colResult = New List(Of clsItterationData)
            While objReader.Read
                objWorkItem = New clsItterationData
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
        objCommand.CommandText = "tnyk.ISP_Itteration"
        objCommand.CommandType = CommandType.StoredProcedure
        objCommand.Transaction = objSettings.DatabaseTransaction

        objFeatureId = CreateParameter(objCommand, "id", DbType.Int32)
        objFeatureId.Direction = ParameterDirection.Output
        objCommand.Parameters.Add(objFeatureId)

        objParameter = CreateParameter(objCommand, "projectId", DbType.Int32)
        objParameter.Value = ProjectId.Value
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "name", DbType.String)
        objParameter.Value = Name
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "startDate", DbType.Date)
        If StartDate.HasValue Then
            objParameter.Value = StartDate.Value
        Else
            objParameter.Value = DBNull.Value
        End If
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "endDate", DbType.Date)
        If EndDate.HasValue Then
            objParameter.Value = EndDate.Value
        Else
            objParameter.Value = DBNull.Value
        End If
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "isActive", DbType.Boolean)
        objParameter.Value = IsActive
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
        objCommand.CommandText = "tnyk.USP_Itteration"
        objCommand.CommandType = CommandType.StoredProcedure
        objCommand.Transaction = objSettings.DatabaseTransaction

        objParameter = CreateParameter(objCommand, "id", DbType.Int32)
        objParameter.Value = Id.Value
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "name", DbType.String)
        objParameter.Value = Name
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "startDate", DbType.Date)
        If StartDate.HasValue Then
            objParameter.Value = StartDate.Value
        Else
            objParameter.Value = DBNull.Value
        End If
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "endDate", DbType.Date)
        If EndDate.HasValue Then
            objParameter.Value = EndDate.Value
        Else
            objParameter.Value = DBNull.Value
        End If
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "isActive", DbType.Boolean)
        objParameter.Value = IsActive
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
        objCommand.CommandText = "tnyk.DSP_Itteration"
        objCommand.CommandType = CommandType.StoredProcedure
        objCommand.Transaction = objSettings.DatabaseTransaction

        objParameter = CreateParameter(objCommand, "id", DbType.Int32)
        objParameter.Value = intId
        objCommand.Parameters.Add(objParameter)

        Return objCommand.ExecuteNonQuery()
    End Function
End Class
