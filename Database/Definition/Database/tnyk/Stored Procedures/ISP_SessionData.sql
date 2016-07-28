CREATE PROCEDURE [tnyk].[ISP_SessionData]
	@sessionDataId INT OUT,
	@sessionGuid binary(16),
	@name varchar(50),
	@value nvarchar(500)
AS
BEGIN
	INSERT INTO [tnyk].[SessionData] ([SessionGuid], [Name], [Value])
	VALUES (@sessionGuid, @name, @value)
	;

	SET @sessionDataId = SCOPE_IDENTITY();
END