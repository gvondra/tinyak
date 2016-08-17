CREATE PROCEDURE [tnyk].[SSP_Project_By_Owner]
	@ownerId int
AS
BEGIN
	SELECT [ProjectId], [OwnerId], [Title]
	FROM [tnyk].[Project]
	WHERE [OwnerId] = @ownerId
	;
	
	SELECT [ProjectMemberId], [ProjectId], [EmailAddress]
	FROM [tnyk].[ProjectMember]
	WHERE [ProjectId] IN (SELECT [ProjectId] FROM [tnyk].[Project] WHERE [OwnerId] = @ownerId)
	;
END