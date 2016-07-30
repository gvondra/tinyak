Public Class clsUserData

    Friend Const TABLE_NAME As String = "User"

    Private m_intId As Nullable(Of Integer)
    Private m_strName As String
    Private m_strEmailAddress As String
    Private m_blnIsAdministrator As Boolean
    Private m_lngSalt As Long

    Public Property Id As Nullable(Of Integer)
        Get
            Return m_intId
        End Get
        Set
            m_intId = Value
        End Set
    End Property

    Public Property Name As String
        Get
            Return m_strName
        End Get
        Set
            m_strName = Value
        End Set
    End Property

    Public Property EmailAddress As String
        Get
            Return m_strEmailAddress
        End Get
        Set
            m_strEmailAddress = Value
        End Set
    End Property

    Public Property IsAdministrator As Boolean
        Get
            Return m_blnIsAdministrator
        End Get
        Set
            m_blnIsAdministrator = Value
        End Set
    End Property

    Public Property Salt As Long
        Get
            Return m_lngSalt
        End Get
        Set
            m_lngSalt = Value
        End Set
    End Property

    Public Shared Function IsEmailAddressAvailable(ByVal objProcessingData As IProcessingData, ByVal strEmailAddress As String) As Boolean
        Dim objConnection As IDbConnection
        Dim objCommand As IDbCommand
        Dim objParameter As IDataParameter
        Dim objResult As Object

        objConnection = OpenConnection(objProcessingData)
        Try
            objCommand = objConnection.CreateCommand
            objCommand.CommandText = "tnyk.SSP_User_EmailAddressCount"
            objCommand.CommandType = CommandType.StoredProcedure

            objParameter = CreateParameter(objCommand, "emailAddress", DbType.String)
            objParameter.Value = strEmailAddress
            objCommand.Parameters.Add(objParameter)

            objResult = objCommand.ExecuteScalar()
            objCommand.Dispose()
            objConnection.Close()
        Finally
            objConnection.Dispose()
        End Try
        Return (CType(objResult, Integer) = 0)
    End Function

    Public Sub SaveNew(ByVal objSettings As IProcessingData, ByVal bytPasswordToken As Byte(), ByVal lngSalt As Long)
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
        objCommand.CommandText = "tnyk.ISP_User"
        objCommand.CommandType = CommandType.StoredProcedure
        objCommand.Transaction = objSettings.DatabaseTransaction

        objUserId = CreateParameter(objCommand, "userId", DbType.Int32)
        objUserId.Direction = ParameterDirection.Output
        objCommand.Parameters.Add(objUserId)

        objParameter = CreateParameter(objCommand, "name", DbType.String)
        objParameter.Value = Name
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "emailAddress", DbType.String)
        objParameter.Value = EmailAddress
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "passwordToken", DbType.Binary)
        objParameter.Value = bytPasswordToken
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "isAdministrator", DbType.Boolean)
        objParameter.Value = IsAdministrator
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "salt", DbType.Int64)
        objParameter.Value = lngSalt
        objCommand.Parameters.Add(objParameter)

        objCommand.ExecuteNonQuery()

        m_intId = CType(objUserId.Value, Int32)
    End Sub
End Class
