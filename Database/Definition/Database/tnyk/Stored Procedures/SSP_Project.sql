CREATE PROCEDURE [tnyk].[SSP_Project]
	@id int
AS
BEGIN
	SELECT [ProjectId], [OwnerId], [Title]
	FROM [tnyk].[Project]
	WHERE [ProjectId] = @id
	;

	SELECT [ProjectMemberId], [ProjectId], [EmailAddress]
	FROM [tnyk].[ProjectMember]
	WHERE [ProjectId] = @id
	;
END