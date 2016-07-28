CREATE PROCEDURE [tnyk].[SSP_Session]
	@sessionGuid binary(16)
AS
BEGIN
	SELECT [SessionGuid], [UserId], [ExpirationDate]
	FROM [tnyk].[Session]
	WHERE SessionGuid = @sessionGuid
	;

	SELECT [SessionDataId], [SessionGuid], [Name], [Value]
	FROM [tnyk].[SessionData]
	WHERE SessionGuid = @sessionGuid
	;
END
