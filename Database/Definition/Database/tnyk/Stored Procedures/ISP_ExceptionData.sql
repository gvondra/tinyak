CREATE PROCEDURE [tnyk].[ISP_ExceptionData]
	@id int out,
	@exceptionId INT, 
    @name NVARCHAR(150), 
    @value NTEXT
AS
BEGIN
	INSERT INTO [tnyk].[ExceptionData] ([ExceptionId], [Name], [Value])
	VALUES (@exceptionId, @name, @value)
	;
	SET @id = SCOPE_IDENTITY();
END
