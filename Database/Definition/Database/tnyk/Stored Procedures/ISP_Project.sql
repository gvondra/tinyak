CREATE PROCEDURE [tnyk].[ISP_Project]
	@id int out,
	@ownerId int,
	@title NVARCHAR(500)
AS
BEGIN
	INSERT INTO [tnyk].[Project] ([OwnerId], [Title])
	VALUES (@ownerId, @title)
	;
	SET @id = SCOPE_IDENTITY();
END
