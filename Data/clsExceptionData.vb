Public Class clsExceptionData
    Public Property Id As Nullable(Of Integer)
    Public Property TypeName As String
    Public Property Message As String
    Public Property Source As String
    Public Property Target As String
    Public Property StackTrace As String
    Public Property HResult As Integer
    Public Property Timestamp As Date
    Public Property Data As Dictionary(Of String, String)
    Public Property ParentExceptionId As Nullable(Of Integer)
    Public Property InnerException As clsExceptionData

    Private Shared Sub Initialize(ByVal objReader As IDataReader, objException As clsExceptionData)
        With objException
            .Id = objReader.GetInt32(objReader.GetOrdinal("ExceptionId"))
            If objReader.IsDBNull(objReader.GetOrdinal("ParentExceptionId")) = False Then
                .ParentExceptionId = objReader.GetInt32(objReader.GetOrdinal("ParentExceptionId"))
            Else
                .ParentExceptionId = Nothing
            End If
            .TypeName = objReader.GetString(objReader.GetOrdinal("TypeName"))
            .Message = objReader.GetString(objReader.GetOrdinal("Message"))
            .Source = objReader.GetString(objReader.GetOrdinal("Source"))
            .Target = objReader.GetString(objReader.GetOrdinal("Target"))
            .StackTrace = objReader.GetString(objReader.GetOrdinal("StackTrace"))
            .HResult = objReader.GetInt32(objReader.GetOrdinal("HResult"))
            .Timestamp = objReader.GetDateTime(objReader.GetOrdinal("Timestamp"))
        End With
    End Sub

    Public Shared Function GetByMinimumTimestamp(ByVal objSettings As ISettings, ByVal datMinimumTimestamp As Date) As List(Of clsExceptionData)
        Dim objConnection As IDbConnection
        Dim objCommand As IDbCommand
        Dim objParameter As IDataParameter
        Dim objReader As IDataReader
        Dim colResult As List(Of clsExceptionData)
        Dim objException As clsExceptionData
        Dim objDictionary As SortedDictionary(Of Integer, clsExceptionData)
        Dim intExceptionId As Integer
        Dim objEnumerator As SortedDictionary(Of Integer, clsExceptionData).Enumerator

        objConnection = OpenConnection(objSettings)
        Try
            objCommand = objConnection.CreateCommand()
            objCommand.CommandText = "tnyk.SSP_Exception_By_MinTimestamp"
            objCommand.CommandType = CommandType.StoredProcedure

            objParameter = CreateParameter(objCommand, "minTimestamp", DbType.DateTime)
            objParameter.Value = datMinimumTimestamp
            objCommand.Parameters.Add(objParameter)

            objReader = objCommand.ExecuteReader
            colResult = New List(Of clsExceptionData)
            objDictionary = New SortedDictionary(Of Integer, clsExceptionData)
            While objReader.Read
                objException = New clsExceptionData
                Initialize(objReader, objException)
                If objException.ParentExceptionId.HasValue = False Then
                    colResult.Add(objException)
                End If
                objDictionary.Add(objException.Id.Value, objException)
            End While

            If objReader.NextResult Then
                While objReader.Read
                    intExceptionId = objReader.GetInt32(objReader.GetOrdinal("ExceptionId"))
                    objException = objDictionary(intExceptionId)
                    If objException.Data Is Nothing Then
                        objException.Data = New Dictionary(Of String, String)
                    End If
                    objException.Data.Add(objReader.GetString(objReader.GetOrdinal("Name")).Trim, objReader.GetString(objReader.GetOrdinal("Value")))
                End While
            End If

            objEnumerator = objDictionary.GetEnumerator
            While objEnumerator.MoveNext
                If objEnumerator.Current.Value.ParentExceptionId.HasValue Then
                    objException = objDictionary(objEnumerator.Current.Value.ParentExceptionId.Value)
                    objException.InnerException = objEnumerator.Current.Value
                End If
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
        Dim objExceptionId As IDataParameter

        If objSettings.DatabaseConnection Is Nothing Then
            objSettings.DatabaseConnection = OpenConnection(objSettings)
        End If
        If objSettings.DatabaseTransaction Is Nothing Then
            objSettings.DatabaseTransaction = objSettings.DatabaseConnection.BeginTransaction
        End If
        objCommand = objSettings.DatabaseConnection.CreateCommand
        objCommand.CommandText = "tnyk.ISP_Exception"
        objCommand.CommandType = CommandType.StoredProcedure
        objCommand.Transaction = objSettings.DatabaseTransaction

        objExceptionId = CreateParameter(objCommand, "id", DbType.Int32)
        objExceptionId.Direction = ParameterDirection.Output
        objCommand.Parameters.Add(objExceptionId)
        objParameter = CreateParameter(objCommand, "parentExceptionId", DbType.Int32)
        If ParentExceptionId.HasValue Then
            objParameter.Value = ParentExceptionId.Value
        Else
            objParameter.Value = DBNull.Value
        End If
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "typeName", DbType.String)
        objParameter.Value = TypeName
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "message", DbType.String)
        objParameter.Value = Message
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "source", DbType.String)
        objParameter.Value = Source
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "target", DbType.String)
        objParameter.Value = Target
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "stackTrace", DbType.String)
        objParameter.Value = StackTrace
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "hResult", DbType.Int32)
        objParameter.Value = HResult
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "timestamp", DbType.DateTime)
        objParameter.Value = Timestamp
        objCommand.Parameters.Add(objParameter)

        objCommand.ExecuteNonQuery()

        Id = CType(objExceptionId.Value, Int32)

        If Data IsNot Nothing AndAlso Data.Count > 0 Then
            CreateData(objSettings)
        End If

        If InnerException IsNot Nothing Then
            InnerException.ParentExceptionId = Id.Value
            InnerException.Create(objSettings)
        End If
    End Sub

    Private Sub CreateData(ByVal objSettings As ISettings)
        Dim objCommand As IDbCommand
        Dim objParameter As IDataParameter
        Dim objEnumerator As Dictionary(Of String, String).Enumerator

        objEnumerator = Data.GetEnumerator
        While objEnumerator.MoveNext
            objCommand = objSettings.DatabaseConnection.CreateCommand
            objCommand.CommandText = "tnyk.ISP_ExceptionData"
            objCommand.CommandType = CommandType.StoredProcedure
            objCommand.Transaction = objSettings.DatabaseTransaction

            objParameter = CreateParameter(objCommand, "id", DbType.Int32)
            objParameter.Direction = ParameterDirection.Output
            objCommand.Parameters.Add(objParameter)

            objParameter = CreateParameter(objCommand, "exceptionId", DbType.Int32)
            objParameter.Value = Id.Value
            objCommand.Parameters.Add(objParameter)

            objParameter = CreateParameter(objCommand, "name", DbType.String)
            objParameter.Value = objEnumerator.Current.Key
            objCommand.Parameters.Add(objParameter)

            objParameter = CreateParameter(objCommand, "value", DbType.String)
            objParameter.Value = objEnumerator.Current.Value
            objCommand.Parameters.Add(objParameter)

            objCommand.ExecuteNonQuery()
        End While
    End Sub
End Class
