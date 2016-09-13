CREATE PROCEDURE [tnyk].[SSP_WorkItem]
	@id int
AS
BEGIN
	SELECT [WorkItemId], [ProjectId], [FeatureId], [Title], [State], [AssignedTo], [Effort], [Description], [AcceptanceCriteria], [ItterationId]
	FROM [tnyk].[WorkItem]
	WHERE [WorkItemId] = @id
	;
END

