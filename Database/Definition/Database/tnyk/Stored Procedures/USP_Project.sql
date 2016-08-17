CREATE PROCEDURE [tnyk].[USP_Project]
	@id int,
	@title NVARCHAR(500)
AS
BEGIN
	UPDATE [tnyk].[Project]
	SET [Title] = @title
	WHERE [ProjectId] = @id
	;
END
