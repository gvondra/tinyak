CREATE PROCEDURE [tnyk].[DSP_Task_By_WorkItem]
	@workItemId int
AS
BEGIN
	DELETE FROM [tnyk].[Task]
	WHERE [WorkItemId] = @workItemId
	;
END
