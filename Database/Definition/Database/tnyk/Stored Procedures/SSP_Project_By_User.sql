CREATE PROCEDURE [tnyk].[SSP_Project_By_Owner]
	@ownerId int
AS
BEGIN
	SELECT [ProjectId], [OwnerId], [Title]
	FROM [tnyk].[Project]
	WHERE [OwnerId] = @ownerId
END