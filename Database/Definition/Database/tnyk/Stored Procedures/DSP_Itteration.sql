CREATE PROCEDURE [tnyk].[DSP_Itteration]
	@id int
AS
BEGIN 
	DELETE FROM [tnyk].[Itteration]
	WHERE [ItterationId] = @id
	;
END
