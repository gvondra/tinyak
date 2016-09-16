CREATE PROCEDURE [tnyk].[ISP_WebMetrics]
	@id int OUT,
	@url NVARCHAR(300), 
    @controller VARCHAR(50), 
    @action VARCHAR(50), 
    @timestamp DATETIME, 
    @duration float, 
    @contentEncoding VARCHAR(50), 
    @contentLength INT, 
    @contentType VARCHAR(50), 
    @method VARCHAR(10), 
    @requestType VARCHAR(50), 
    @totalBytes INT, 
    @urlReferrer NVARCHAR(300), 
    @userAgent NVARCHAR(300), 
    @parameters NTEXT
AS
BEGIN
	INSERT INTO [tnyk].[WebMetrics] ([Url], [Controller], [Action], [Timestamp], [Duration], [ContentEncoding], [ContentLength], [ContentType], [Method], [RequestType], [TotalBytes], [UrlReferrer], [UserAgent], [Parameters])
	VALUES (@url, @controller, @action, @timestamp, @duration, @contentEncoding, @contentLength, @contentType, @method, @requestType, @totalBytes, @urlReferrer, @userAgent, @parameters)
	;
	SET @id = SCOPE_IDENTITY();
END