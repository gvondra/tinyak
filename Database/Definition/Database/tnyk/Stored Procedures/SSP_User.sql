CREATE PROCEDURE [tnyk].[SSP_User]
	@userId int
AS
BEGIN
	SELECT [UserId], [Name], [EmailAddress], [IsAdministrator], [Salt]
	FROM [tnyk].[User]
	WHERE [UserId] = @userId
END