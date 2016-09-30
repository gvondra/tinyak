CREATE PROCEDURE [tnyk].[SSP_Exception_By_MinTimestamp]
	@minTimestamp DATETIME
AS
BEGIN
	SELECT [ExceptionId], [ParentExceptionId], [TypeName], [Message], [Source], [Target], [StackTrace], [HResult], [Timestamp]
	FROM [tnyk].[Exception]
	WHERE [Timestamp] >= @minTimestamp
	ORDER BY [Timestamp] DESC
	;

	SELECT [ED].[ExceptionDataId], [ED].[ExceptionId], [ED].[Name], [ED].[Value]
	FROM [tnyk].[Exception] [E]
		INNER JOIN [tnyk].[ExceptionData] [ED] on [E].[ExceptionId] = [ED].[ExceptionId]
	WHERE [E].[Timestamp] >= @minTimestamp
	;
END
