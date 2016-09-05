CREATE PROCEDURE [tnyk].[DSP_Feature]
	@id int
AS
BEGIN 
	DELETE FROM [tnyk].[Feature]
	WHERE [FeatureId] = @id
	;
END
