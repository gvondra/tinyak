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

        objUserId = CreateParameter(objCommand, "userId", DbType.Int32)
        objUserId.Direction = ParameterDirection.Output
        objCommand.Parameters.Add(objUserId)

        objParameter = CreateParameter(objCommand, "title", DbType.String)
        objParameter.Value = Title
        objCommand.Parameters.Add(objParameter)

        objCommand.ExecuteNonQuery()

        m_intId = CType(objUserId.Value, Int32)
    End Sub
End Class
