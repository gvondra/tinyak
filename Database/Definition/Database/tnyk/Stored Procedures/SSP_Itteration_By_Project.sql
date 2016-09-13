CREATE PROCEDURE [tnyk].[SSP_Itteration_By_Project]
	@projectId int
AS
BEGIN 
	SELECT [ItterationId], [ProjectId], [Name], [StartDate], [EndDate], [IsActive]
	FROM [tnyk].[Itteration]
	WHERE [ProjectId] = @projectId
	;
END
