CREATE PROCEDURE [tnyk].[SSP_WorkItem_By_Feature]
	@featureId int
AS
BEGIN
	SELECT [WorkItemId], [ProjectId], [FeatureId], [Title], [State], [AssignedTo], [Effort], [Description], [AcceptanceCriteria], [ItterationId]
	FROM [tnyk].[WorkItem]
	WHERE [FeatureId] = @featureId
	;
END
