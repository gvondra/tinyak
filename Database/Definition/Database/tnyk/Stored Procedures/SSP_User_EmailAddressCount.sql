CREATE PROCEDURE [tnyk].[SSP_User_EmailAddressCount]
	@emailAddress nvarchar(255)
AS
BEGIN
	SELECT COUNT(*)
	FROM [tnyk].[User]
	WHERE EmailAddress = @emailAddress
	;
END
