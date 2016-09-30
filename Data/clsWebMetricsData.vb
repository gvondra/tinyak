Public Class clsWebMetricsData
    Public Property Id As Nullable(Of Integer)
    Public Property Url As String
    Public Property Controller As String
    Public Property Action As String
    Public Property Timestamp As Date
    Public Property Duration As Nullable(Of Double)
    Public Property ContentEncoding As String
    Public Property ContentLength As Nullable(Of Integer)
    Public Property ContentType As String
    Public Property Method As String
    Public Property RequestType As String
    Public Property TotalBytes As Nullable(Of Integer)
    Public Property UrlReferrer As String
    Public Property UserAgent As String
    Public Property Parameters As String
    Public Property StatusCode As Nullable(Of Integer)
    Public Property StatusDescription As String

    Private Shared Sub Initialize(ByVal objReader As IDataReader, ByVal objWebMetrics As clsWebMetricsData)
        With objWebMetrics
            .Id = objReader.GetInt32(objReader.GetOrdinal("WebMetricsId"))
            .Url = objReader.GetString(objReader.GetOrdinal("Url")).Trim
            .Controller = objReader.GetString(objReader.GetOrdinal("Controller")).Trim
            .Action = objReader.GetString(objReader.GetOrdinal("Action")).Trim
            .Timestamp = objReader.GetDateTime(objReader.GetOrdinal("Timestamp"))
            Date.SpecifyKind(.Timestamp, DateTimeKind.Utc)
            If objReader.IsDBNull(objReader.GetOrdinal("Duration")) = False Then
                .Duration = objReader.GetDouble(objReader.GetOrdinal("Duration"))
            Else
                .Duration = Nothing
            End If
            .ContentEncoding = objReader.GetString(objReader.GetOrdinal("ContentEncoding")).Trim
            If objReader.IsDBNull(objReader.GetOrdinal("ContentLength")) = False Then
                .ContentLength = objReader.GetInt32(objReader.GetOrdinal("ContentLength"))
            Else
                .ContentLength = Nothing
            End If
            .ContentType = objReader.GetString(objReader.GetOrdinal("ContentType")).Trim
            .Method = objReader.GetString(objReader.GetOrdinal("Method")).Trim
            .RequestType = objReader.GetString(objReader.GetOrdinal("RequestType")).Trim
            If objReader.IsDBNull(objReader.GetOrdinal("TotalBytes")) = False Then
                .TotalBytes = objReader.GetInt32(objReader.GetOrdinal("TotalBytes"))
            Else
                .TotalBytes = Nothing
            End If
            .UrlReferrer = objReader.GetString(objReader.GetOrdinal("UrlReferrer")).Trim
            .UserAgent = objReader.GetString(objReader.GetOrdinal("UserAgent")).Trim
            If objReader.IsDBNull(objReader.GetOrdinal("Parameters")) = False Then
                .Parameters = objReader.GetString(objReader.GetOrdinal("Parameters")).Trim
            Else
                .Parameters = Nothing
            End If
            If objReader.IsDBNull(objReader.GetOrdinal("StatusCode")) = False Then
                .StatusCode = objReader.GetInt32(objReader.GetOrdinal("StatusCode"))
            Else
                .StatusCode = Nothing
            End If
            .StatusDescription = objReader.GetString(objReader.GetOrdinal("StatusDescription")).Trim
        End With
    End Sub

    Public Shared Function GetByMinimumTimestamp(ByVal objSettings As ISettings, ByVal datMinimumTimestamp As Date) As List(Of clsWebMetricsData)
        Dim objConnection As IDbConnection
        Dim objCommand As IDbCommand
        Dim objParameter As IDataParameter
        Dim objReader As IDataReader
        Dim colResult As List(Of clsWebMetricsData)
        Dim objWebMetric As clsWebMetricsData

        objConnection = OpenConnection(objSettings)
        Try
            objCommand = objConnection.CreateCommand()
            objCommand.CommandText = "tnyk.SSP_WebMetrics_By_MinTimestamp"
            objCommand.CommandType = CommandType.StoredProcedure

            objParameter = CreateParameter(objCommand, "minTimestamp", DbType.DateTime)
            objParameter.Value = datMinimumTimestamp
            objCommand.Parameters.Add(objParameter)

            objReader = objCommand.ExecuteReader
            colResult = New List(Of clsWebMetricsData)
            While objReader.Read
                objWebMetric = New clsWebMetricsData
                Initialize(objReader, objWebMetric)
                colResult.Add(objWebMetric)
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
        Dim obWebMetricId As IDataParameter

        If objSettings.DatabaseConnection Is Nothing Then
            objSettings.DatabaseConnection = OpenConnection(objSettings)
        End If
        If objSettings.DatabaseTransaction Is Nothing Then
            objSettings.DatabaseTransaction = objSettings.DatabaseConnection.BeginTransaction
        End If
        objCommand = objSettings.DatabaseConnection.CreateCommand
        objCommand.CommandText = "tnyk.ISP_WebMetrics"
        objCommand.CommandType = CommandType.StoredProcedure
        objCommand.Transaction = objSettings.DatabaseTransaction

        obWebMetricId = CreateParameter(objCommand, "id", DbType.Int32)
        obWebMetricId.Direction = ParameterDirection.Output
        objCommand.Parameters.Add(obWebMetricId)

        objParameter = CreateParameter(objCommand, "url", DbType.String)
        objParameter.Value = Url
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "controller", DbType.String)
        objParameter.Value = Controller
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "action", DbType.String)
        objParameter.Value = Action
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "timestamp", DbType.DateTime)
        objParameter.Value = Timestamp
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "duration", DbType.Double)
        If Duration.HasValue Then
            objParameter.Value = Duration
        Else
            objParameter.Value = DBNull.Value
        End If
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "contentEncoding", DbType.String)
        objParameter.Value = ContentEncoding
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "contentLength", DbType.Int32)
        If ContentLength.HasValue Then
            objParameter.Value = ContentLength.Value
        Else
            objParameter.Value = DBNull.Value
        End If
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "contentType", DbType.String)
        objParameter.Value = ContentType
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "method", DbType.String)
        objParameter.Value = Method
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "requestType", DbType.String)
        objParameter.Value = RequestType
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "totalBytes", DbType.Int32)
        If TotalBytes.HasValue Then
            objParameter.Value = TotalBytes.Value
        Else
            objParameter.Value = DBNull.Value
        End If
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "urlReferrer", DbType.String)
        objParameter.Value = UrlReferrer
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "userAgent", DbType.String)
        objParameter.Value = UserAgent
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "parameters", DbType.String)
        If Parameters IsNot Nothing Then
            objParameter.Value = Parameters
        Else
            objParameter.Value = DBNull.Value
        End If
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "statusCode", DbType.Int32)
        If StatusCode.HasValue Then
            objParameter.Value = StatusCode.Value
        Else
            objParameter.Value = DBNull.Value
        End If
        objCommand.Parameters.Add(objParameter)

        objParameter = CreateParameter(objCommand, "statusDescription", DbType.String)
        If StatusDescription Is Nothing Then
            StatusDescription = String.Empty
        End If
        objParameter.Value = StatusDescription
        objCommand.Parameters.Add(objParameter)

        objCommand.ExecuteNonQuery()

        Id = CType(obWebMetricId.Value, Int32)
    End Sub
End Class
