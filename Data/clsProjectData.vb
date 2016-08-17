Public Class clsProjectData

    Friend Const TABLE_NAME As String = "Project"

    Public Property Id As Nullable(Of Integer)
    Public Property Title As String
    Public Property OwnerId As Integer
    Public Property ProjectMembers As List(Of String)

    Private Shared Sub Initialize(ByVal objReader As IDataReader, ByVal objProject As clsProjectData)
        With objProject
            .Id = objReader.GetInt32(objReader.GetOrdinal("ProjectId"))
            .OwnerId = objReader.GetInt32(objReader.GetOrdinal("OwnerId"))
            .Title = objReader.GetString(objReader.GetOrdinal("Title")).TrimEnd
        End With
    End Sub

    Public Shared Function [Get](ByVal objProcessingData As IProcessingData, ByVal intId As Integer) As clsProjectData
        Dim objConnection As IDbConnection
        Dim objCommand As IDbCommand
        Dim objParameter As IDataParameter
        Dim objReader As IDataReader
        Dim objResult As clsProjectData

        objConnection = OpenConnection(objProcessingData)
        Try
            objCommand = objConnection.CreateCommand
            objCommand.CommandText = "tnyk.SSP_Project"
            objCommand.CommandType = CommandType.StoredProcedure

            objParameter = CreateParameter(objCommand, "id", DbType.Int32)
            objParameter.Value = intId
            objCommand.Parameters.Add(objParameter)

            objReader = objCommand.ExecuteReader
            If objReader.Read Then
                objResult = New clsProjectData
                Initialize(objReader, objResult)

                If objReader.NextResult Then
                    objResult.ProjectMembers = New List(Of String)
                    While objReader.Read
                        objResult.ProjectMembers.Add(objReader.GetString(objReader.GetOrdinal("EmailAddress")).TrimEnd)
                    End While
                End If
            Else
                objResult = Nothing
            End If
            objConnection.Close()
        Finally
            objConnection.Dispose()
        End Try
        Return objResult
    End Function

    Public Shared Function GetByOwner(ByVal objProcessingData As IProcessingData, ByVal intOwnerId As Integer) As List(Of clsProjectData)
        Dim objConnection As IDbConnection
        Dim objCommand As IDbCommand
        Dim objParameter As IDataParameter
        Dim objReader As IDataReader
        Dim colResult As List(Of clsProjectData)
        Dim objProject As clsProjectData
        Dim objDictionary As Dictionary(Of Integer, clsProjectData)

        objConnection = OpenConnection(objProcessingData)
        Try
            objCommand = objConnection.CreateCommand
            objCommand.CommandText = "tnyk.SSP_Project_By_Owner"
            objCommand.CommandType = CommandType.StoredProcedure

            objParameter = CreateParameter(objCommand, "ownerId", DbType.Int32)
            objParameter.Value = intOwnerId
            objCommand.Parameters.Add(objParameter)

            objReader = objCommand.ExecuteReader
            colResult = New List(Of clsProjectData)
            objDictionary = New Dictionary(Of Integer, clsProjectData)
            While objReader.Read
                objProject = New clsProjectData
                Initialize(objReader, objProject)
                colResult.Add(objProject)
                objDictionary.Add(objProject.Id.Value, objProject)
            End While

            If objReader.NextResult Then
                While objReader.Read
                    objProject = objDictionary(objReader.GetInt32(objReader.GetOrdinal("ProjectId")))
                    If objProject IsNot Nothing Then
                        If objProject.ProjectMembers Is Nothing Then
                            objProject.ProjectMembers = New List(Of String)
                        End If
                        objProject.ProjectMembers.Add(objReader.GetString(objReader.GetOrdinal("EmailAddress")).TrimEnd)
                    End If
                End While
            End If
            objConnection.Close()
        Finally
            objConnection.Dispose()
        End Try
        Return colResult
    End Function

    Public Sub Create(ByVal objSettings As IProcessingData)
        Dim objCommand As IDbCommand
        Dim objParameter As IDataParameter
        Dim objProjectId As IDataParameter

        If objSettings.DatabaseConnection Is Nothing Then
            objSettings.DatabaseConnection = OpenConnection(objSettings)
        End If
        If objSettings.DatabaseTransaction Is Nothing Then
            objSettings.DatabaseTransaction = objSettings.DatabaseConnection.BeginTransaction
        End If
        objCommand = objSettings.DatabaseConnection.CreateCommand
        objCommand.CommandText = "tnyk.ISP_Project"
        objCommand.CommandType = CommandType.StoredProcedure
        objCommand.Transaction = objSettings.DatabaseTransaction


        objProjectId = CreateParameter(objCommand, "id", DbType.Int32)
        objProjectId.Direction = ParameterDirection.Output
        objCommand.Parameters.Add(objProjectId)

        objParameter = CreateParameter(objCommand, "ownerId", DbType.Int32)
        objParameter.Value = OwnerId
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "title", DbType.String)
        objParameter.Value = Title
        objCommand.Parameters.Add(objParameter)

        objCommand.ExecuteNonQuery()

        Id = CType(objProjectId.Value, Int32)
    End Sub

    Public Sub Update(ByVal objSettings As IProcessingData)
        Dim objCommand As IDbCommand
        Dim objParameter As IDataParameter

        If objSettings.DatabaseConnection Is Nothing Then
            objSettings.DatabaseConnection = OpenConnection(objSettings)
        End If
        If objSettings.DatabaseTransaction Is Nothing Then
            objSettings.DatabaseTransaction = objSettings.DatabaseConnection.BeginTransaction
        End If
        objCommand = objSettings.DatabaseConnection.CreateCommand
        objCommand.CommandText = "tnyk.USP_Project"
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
End Class
