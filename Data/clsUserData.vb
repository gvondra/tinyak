Public Class clsUserData

    Friend Const TABLE_NAME As String = "User"

    Private m_intId As Nullable(Of Integer)
    Private m_strName As String
    Private m_strEmailAddress As String
    Private m_blnIsAdministrator As Boolean

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
End Class
