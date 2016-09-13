CREATE PROCEDURE [tnyk].[ISP_Itteration]
	@id int out,
	@projectId int,
	@name nvarchar(100),
	@startDate date,
	@endDate date,
	@isActive bit
AS
BEGIN
	INSERT INTO [tnyk].[Itteration] ([ProjectId], [Name], [StartDate], [EndDate], [IsActive])
	VALUES (@projectId, @name, @startDate, @endDate, @isActive)
	;

	SET @id = SCOPE_IDENTITY();
END
