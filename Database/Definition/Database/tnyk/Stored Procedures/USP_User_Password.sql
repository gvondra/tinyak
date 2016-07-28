CREATE PROCEDURE [tnyk].[USP_User_Password]
	@emailAddress nvarchar(255),
	@passwordToken binary(64),
	@newPasswordToken binary(64)
AS
BEGIN
	UPDATE [tnyk].[User]
	SET [PasswordToken] = @newPasswordToken
	WHERE [EmailAddress] = @emailAddress
		AND [PasswordToken] = @passwordToken
END