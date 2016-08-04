Imports System.Collections.Specialized
Public Class clsSessionData

    Private Const TABLE_NAME As String = "Session"

    Private m_blnIsNew As Boolean
    Private m_objId As Guid
    Private m_intUserId As Nullable(Of Integer)
    Private m_datExpiration As Nullable(Of Date)
    Private m_objData As NameValueCollection

    Public Sub New()
        m_objId = Guid.Empty
        m_objData = New NameValueCollection
        m_blnIsNew=True
    End Sub

    Public Property Id As Guid
        Get
            Return m_objId
        End Get
        Set
            m_objId = Value
        End Set
    End Property

    Public readonly property IsNew As Boolean 
        Get 
            Return m_blnIsNew
        end get 
    end property

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

    Public Property Data As NameValueCollection
        Get
            Return m_objData
        End Get
        Set
            m_objData = Value
        End Set
    End Property

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
                        objResult.Data.Add(objReader.GetString(objReader.GetOrdinal("Name")).trim, objReader.GetString(objReader.GetOrdinal("Value")))
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
        Dim i As Integer

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
            For i = 0 To Data.Count - 1
                objCommand = objProcessingData.DatabaseConnection.CreateCommand
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.Transaction = objProcessingData.DatabaseTransaction
                objCommand.CommandText = "tnyk.ISP_SessionData"

                objParameter = CreateParameter(objCommand, "sessionGuid", DbType.Binary)
                objParameter.Value = Id.ToByteArray
                objCommand.Parameters.Add(objParameter)

                objParameter = CreateParameter(objCommand, "name", DbType.String)
                objParameter.Value = Data.Keys(i)
                objCommand.Parameters.Add(objParameter)

                objParameter = CreateParameter(objCommand, "value", DbType.String)
                objParameter.Value = Data(Data.Keys(i))
                objCommand.Parameters.Add(objParameter)

                objCommand.ExecuteNonQuery()

            Next i
        End If
    End Sub
End Class
