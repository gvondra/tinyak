CREATE PROCEDURE [tnyk].[USP_Feature]
	@id int,
	@title NVARCHAR(500)
AS
BEGIN
	UPDATE [tnyk].[Feature]
	SET [Title] = @title
	WHERE [FeatureId] = @id
	;
END