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

	INSERT INTO [tnyk].[ProjectMember] ([ProjectId], [EmailAddress])
	SELECT @id, [EmailAddress]
	FROM [tnyk].[User]
	WHERE [UserID] = @ownerId
	;
END
