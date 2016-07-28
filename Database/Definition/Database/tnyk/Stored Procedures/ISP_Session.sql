CREATE PROCEDURE [tnyk].[ISP_Session]
	@sessionGuid binary(16),
	@userId int,
	@expirationDate datetime
AS
BEGIN
	INSERT INTO [tnyk].[Session] ([SessionGuid], [UserId], [ExpirationDate])
	VALUES (@sessionGuid, @userId, @expirationDate)
	;
END
