Public Class clsProjectData

    Friend Const TABLE_NAME As String = "Project"

    Private m_intId As Nullable(Of Integer)
    Private m_strTitle As String
    Private m_intOwnerId As Integer
    Private m_colTeamMembers As List(Of String)

    Public Property Id As Nullable(Of Integer)
        Get
            Return m_intId
        End Get
        Set
            m_intId = Value
        End Set
    End Property

    Public Property Title As String
        Get
            Return m_strTitle
        End Get
        Set
            m_strTitle = Value
        End Set
    End Property

    Public Property OwnerId As Integer
        Get
            Return m_intOwnerId
        End Get
        Set
            m_intOwnerId = Value
        End Set
    End Property

    Public Property ProjectMembers As List(Of String)
        Get
            Return m_colTeamMembers
        End Get
        Set
            m_colTeamMembers = Value
        End Set
    End Property

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
            While objReader.Read
                objProject = New clsProjectData
                Initialize(objReader, objProject)
                colResult.Add(objProject)
            End While
            objConnection.Close()
        Finally
            objConnection.Dispose()
        End Try
        Return colResult
    End Function

    Public Sub Create(ByVal objSettings As IProcessingData)
        Dim objCommand As IDbCommand
        Dim objParameter As IDataParameter
        Dim objUserId As IDataParameter

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


        objUserId = CreateParameter(objCommand, "id", DbType.Int32)
        objUserId.Direction = ParameterDirection.Output
        objCommand.Parameters.Add(objUserId)

        objParameter = CreateParameter(objCommand, "ownerId", DbType.Int32)
        objParameter.Value = OwnerId
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "title", DbType.String)
        objParameter.Value = Title
        objCommand.Parameters.Add(objParameter)

        objCommand.ExecuteNonQuery()

        m_intId = CType(objUserId.Value, Int32)
    End Sub
End Class
