CREATE PROCEDURE [tnyk].[SSP_Project_By_EmailAddress]
	@emailAddress NVARCHAR (250)
AS
BEGIN
	SELECT [ProjectId], [OwnerId], [Title]
	FROM [tnyk].[Project]
	WHERE [ProjectId] IN (SELECT [ProjectId] FROM [tnyk].[ProjectMember] WHERE [EmailAddress] = @emailAddress)
	;
END