CREATE PROCEDURE [tnyk].[DSP_Session_By_Expiration]
	@minExpirationDate datetime
AS
BEGIN
	DELETE FROM tnyk.[SessionData] 
	WHERE SessionGuid in (SELECT SessionGuid FROM tnyk.[Session] WHERE ExpirationDate <= @minExpirationDate)
	;

	DELETE FROM tnyk.[Session]
	WHERE ExpirationDate <= @minExpirationDate
	;
END