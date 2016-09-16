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

        objCommand.ExecuteNonQuery()

        Id = CType(obWebMetricId.Value, Int32)
    End Sub
End Class
