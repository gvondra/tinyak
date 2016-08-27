Public Class clsSessionData

    Private Const TABLE_NAME As String = "Session"

    Private m_blnIsNew As Boolean
    Private m_objId As Guid
    Private m_intUserId As Nullable(Of Integer)
    Private m_datExpiration As Nullable(Of Date)

    Public Sub New()
        m_objId = Guid.Empty
        Data = New Dictionary(Of String, String)
        m_blnIsNew = True
    End Sub

    Public Property Id As Guid
        Get
            Return m_objId
        End Get
        Set
            m_objId = Value
        End Set
    End Property

    Public ReadOnly Property IsNew As Boolean
        Get
            Return m_blnIsNew
        End Get
    End Property

    Public Property UserId As Nullable(Of Integer)
        Get
            Return m_intUserId
        End Get
        Set
            m_intUserId = Value
        End Set
    End Property

    Public Property ExpirationDate As Nullable(Of Date)
        Get
            Return m_datExpiration
        End Get
        Set
            m_datExpiration = Value
        End Set
    End Property

    Public Property Data As Dictionary(Of String, String)

    Private Shared Sub Initialize(ByVal objReader As IDataReader, ByVal objSession As clsSessionData)
        Dim bytValue() As Byte
        With objSession
            .m_blnIsNew = False
            bytValue = CType(objReader.GetValue(objReader.GetOrdinal("SessionGuid")), Byte())
            .m_objId = New Guid(bytValue)
            If objReader.IsDBNull(objReader.GetOrdinal("UserId")) = False Then
                .m_intUserId = objReader.GetInt32(objReader.GetOrdinal("UserId"))
            Else
                .m_intUserId = Nothing
            End If
            If objReader.IsDBNull(objReader.GetOrdinal("ExpirationDate")) = False Then
                .m_datExpiration = objReader.GetDateTime(objReader.GetOrdinal("ExpirationDate"))
            Else
                .m_datExpiration = Nothing
            End If
        End With
    End Sub

    Public Shared Function [Get](ByVal objProcessingData As IProcessingData, ByVal objId As Guid) As clsSessionData
        Dim objConnection As IDbConnection
        Dim objCommand As IDbCommand
        Dim objParameter As IDataParameter
        Dim objReader As IDataReader
        Dim objResult As clsSessionData

        objConnection = OpenConnection(objProcessingData)
        Try
            objCommand = objConnection.CreateCommand
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.CommandText = "tnyk.SSP_Session"

            objParameter = CreateParameter(objCommand, "sessionGuid", DbType.Binary)
            objParameter.Value = objId.ToByteArray
            objCommand.Parameters.Add(objParameter)

            objReader = objCommand.ExecuteReader

            If objReader.Read Then
                objResult = New clsSessionData
                Initialize(objReader, objResult)

                If objReader.NextResult Then
                    While objReader.Read
                        objResult.Data.Add(objReader.GetString(objReader.GetOrdinal("Name")).Trim, objReader.GetString(objReader.GetOrdinal("Value")))
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

    Public Sub Save(ByVal objProcessingData As IProcessingData)
        Dim objCommand As IDbCommand
        Dim objParameter As IDataParameter
        Dim objEnumerator As Dictionary(Of String, String).Enumerator

        If objProcessingData.DatabaseConnection Is Nothing Then
            objProcessingData.DatabaseConnection = OpenConnection(objProcessingData)
        End If
        If objProcessingData.DatabaseTransaction Is Nothing Then
            objProcessingData.DatabaseTransaction = objProcessingData.DatabaseConnection.BeginTransaction
        End If
        objCommand = objProcessingData.DatabaseConnection.CreateCommand
        objCommand.CommandType = CommandType.StoredProcedure
        objCommand.Transaction = objProcessingData.DatabaseTransaction

        If m_blnIsNew Then
            objCommand.CommandText = "tnyk.ISP_Session"
        Else
            objCommand.CommandText = "tnyk.USP_Session"
        End If

        objParameter = CreateParameter(objCommand, "sessionGuid", DbType.Binary)
        objParameter.Value = Id.ToByteArray
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "userId", DbType.Int32)
        If UserId.HasValue Then
            objParameter.Value = UserId.Value
        Else
            objParameter.Value = DBNull.Value
        End If
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "expirationDate", DbType.DateTime)
        objParameter.Value = ExpirationDate
        objCommand.Parameters.Add(objParameter)

        objCommand.ExecuteNonQuery()

        objCommand = objProcessingData.DatabaseConnection.CreateCommand
        objCommand.CommandType = CommandType.StoredProcedure
        objCommand.Transaction = objProcessingData.DatabaseTransaction
        objCommand.CommandText = "tnyk.DSP_SessionData_By_SessionGuid"

        objParameter = CreateParameter(objCommand, "sessionGuid", DbType.Binary)
        objParameter.Value = Id.ToByteArray
        objCommand.Parameters.Add(objParameter)

        objCommand.ExecuteNonQuery()

        If Data IsNot Nothing AndAlso Data.Count > 0 Then
            objEnumerator = Data.GetEnumerator
            While objEnumerator.MoveNext
                objCommand = objProcessingData.DatabaseConnection.CreateCommand
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.Transaction = objProcessingData.DatabaseTransaction
                objCommand.CommandText = "tnyk.ISP_SessionData"

                objParameter = CreateParameter(objCommand, "sessionGuid", DbType.Binary)
                objParameter.Value = Id.ToByteArray
                objCommand.Parameters.Add(objParameter)

                objParameter = CreateParameter(objCommand, "name", DbType.String)
                objParameter.Value = objEnumerator.Current.Key
                objCommand.Parameters.Add(objParameter)

                objParameter = CreateParameter(objCommand, "value", DbType.String)
                objParameter.Value = objEnumerator.Current.Value
                objCommand.Parameters.Add(objParameter)

                objCommand.ExecuteNonQuery()

            End While
        End If
    End Sub
End Class
