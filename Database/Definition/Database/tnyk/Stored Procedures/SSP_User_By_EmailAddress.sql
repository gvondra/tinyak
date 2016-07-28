CREATE PROCEDURE [tnyk].[SSP_User_By_EmailAddress]
	@emailAddress nvarchar(255),
	@passwordToken binary(64)
AS
BEGIN
	SELECT [UserId], [Name], [EmailAddress], [IsAdministrator]
	FROM [tnyk].[User]
	WHERE [EmailAddress] = @emailAddress
		AND [PasswordToken] = @passwordToken
END