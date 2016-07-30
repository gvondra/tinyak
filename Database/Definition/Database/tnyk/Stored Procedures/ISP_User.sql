CREATE PROCEDURE [tnyk].[ISP_User]
	@userId int out,
	@name nvarchar(100),
	@emailAddress nvarchar(255),
	@passwordToken binary(64),
	@salt bigint,
	@isAdministrator bit
AS
BEGIN
	INSERT INTO [tnyk].[User] ([Name], [EmailAddress], [PasswordToken], [IsAdministrator], [Salt])
	VALUES (@name, @emailAddress, @passwordToken, @isAdministrator, @salt)
	;
	SET @userId = SCOPE_IDENTITY();
END