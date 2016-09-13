CREATE PROCEDURE [tnyk].[SSP_Itteration]
	@id int
AS
BEGIN 
	SELECT [ItterationId], [ProjectId], [Name], [StartDate], [EndDate], [IsActive]
	FROM [tnyk].[Itteration]
	WHERE [ItterationId] = @id
	;
END

