CREATE PROCEDURE [tnyk].[SSP_Project]
	@id int
AS
BEGIN
	SELECT [ProjectId], [OwnerId], [Title]
	FROM [tnyk].[Project]
	WHERE [ProjectId] = @id
	;
END