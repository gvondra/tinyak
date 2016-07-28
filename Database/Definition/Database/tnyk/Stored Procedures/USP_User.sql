CREATE PROCEDURE [tnyk].[USP_User]
	@userId int,
	@name nvarchar(100),
	@isAdministrator bit
AS
BEGIN
	UPDATE [tnyk].[User]
	SET [Name] = @name,
		[IsAdministrator] = @isAdministrator
	WHERE [UserId] = @userId
END