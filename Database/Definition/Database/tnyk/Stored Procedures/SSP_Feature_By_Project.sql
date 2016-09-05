CREATE PROCEDURE [tnyk].[SSP_Feature_By_Project]
	@projectId int
AS
BEGIN
	SELECT [FeatureId], [ProjectId], [Title]
	FROM [tnyk].[Feature]
	WHERE [ProjectId] = @projectId
END
