CREATE PROCEDURE [tnyk].[DSP_WorkItem]
	@id int
AS
BEGIN
	EXECUTE [tnyk].[DSP_Task_By_WorkItem] @id;

	DELETE FROM [tnyk].[WorkItem]
	WHERE [WorkItemId] = @id
	;
END
