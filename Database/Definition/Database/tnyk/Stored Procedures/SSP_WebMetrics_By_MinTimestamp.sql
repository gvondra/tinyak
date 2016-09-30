CREATE PROCEDURE [tnyk].[SSP_WebMetrics_By_MinTimestamp]
	@minTimestamp DATETIME
AS
SELECT [WebMetricsId], [Url], [Controller], [Action], [Timestamp], [Duration], [ContentEncoding], [ContentLength], [ContentType], [Method], [RequestType], [TotalBytes], [UrlReferrer], [UserAgent], [Parameters], [StatusCode], [StatusDescription]
FROM [tnyk].[WebMetrics]
WHERE [Timestamp] >= @minTimestamp
ORDER BY [Timestamp] DESC
