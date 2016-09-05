CREATE PROCEDURE [tnyk].[SSP_Feature]
	@id int
AS
BEGIN
	SELECT [FeatureId], [ProjectId], [Title]
	FROM [tnyk].[Feature]
	WHERE [FeatureId] = @id
END
