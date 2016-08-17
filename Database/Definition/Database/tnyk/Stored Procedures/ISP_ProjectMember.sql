CREATE PROCEDURE [tnyk].[ISP_ProjectMember]
	@id int out,
	@projectId int,
	@emailAddress nvarchar(250)
AS
BEGIN
	INSERT INTO [tnyk].[ProjectMember] ([ProjectId], [EmailAddress])
	VALUES (@projectId, @emailAddress)
	;
	
	SET @id = SCOPE_IDENTITY();
END