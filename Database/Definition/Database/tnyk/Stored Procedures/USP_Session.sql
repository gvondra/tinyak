CREATE PROCEDURE [tnyk].[USP_Session]
	@sessionGuid binary(16),
	@userId int,
	@expirationDate datetime
AS
BEGIN
	UPDATE [tnyk].[Session]
	SET [UserId] = @userId,
		[ExpirationDate] = @expirationDate
	WHERE SessionGuid = @sessionGuid
	;
END