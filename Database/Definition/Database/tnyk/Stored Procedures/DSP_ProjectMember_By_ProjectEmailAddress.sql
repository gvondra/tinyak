CREATE PROCEDURE [tnyk].[DSP_ProjectMember_By_ProjectEmailAddress]
	@projectId int,
	@emailAddress nvarchar(250)
AS
BEGIN
	DELETE FROM [tnyk].[ProjectMember]
	WHERE [ProjectId] = @projectId
		AND [EmailAddress] = @emailAddress
	;
END