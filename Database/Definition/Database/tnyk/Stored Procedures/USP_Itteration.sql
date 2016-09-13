CREATE PROCEDURE [tnyk].[USP_Itteration]
	@id int,
	@name nvarchar(100),
	@startDate date,
	@endDate date,
	@isActive bit
AS
BEGIN
	UPDATE [tnyk].[Itteration]
	SET [Name] = @name, 
		[StartDate] = @startDate, 
		[EndDate] = @endDate, 
		[IsActive] = @isActive
	WHERE [ItterationId] = @id
	;
END
