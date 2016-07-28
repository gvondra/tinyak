CREATE PROCEDURE [tnyk].[DSP_SessionData_By_SessionGuid]
	@sessionGuid binary(16)
AS
Begin
	DELETE FROM tnyk.[SessionData]
	WHERE SessionGuid = @sessionGuid
	;
End