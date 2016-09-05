CREATE PROCEDURE [tnyk].[ISP_Feature]
	@id int out,
	@projectId int,
	@title NVARCHAR(500)
AS
BEGIN
	INSERT INTO [tnyk].[Feature] ([ProjectId], [Title])
	VALUES (@projectId, @title)
	;
	SET @id = SCOPE_IDENTITY();
END
