CREATE PROCEDURE [tnyk].[ISP_Exception]
	@id int out,
	@parentExceptionId INT,
    @typeName VARCHAR(500), 
    @message NTEXT, 
    @source VARCHAR(500), 
    @target VARCHAR(500), 
    @stackTrace NTEXT, 
    @hResult INT, 
    @timestamp DATETIME
AS
BEGIN
	INSERT INTO [tnyk].[Exception] ([ParentExceptionId], [TypeName], [Message], [Source], [Target], [StackTrace], [HResult], [Timestamp])
	VALUES (@parentExceptionId, @typeName, @message, @source, @target, @stackTrace, @hResult, @timestamp)
	;
	SET @id = SCOPE_IDENTITY();
END