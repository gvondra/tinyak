CREATE PROCEDURE [tnyk].[USP_WorkItem]
	@id int,
	@featureId int,
	@title NVARCHAR(500),
	@state SMALLINT,
	@assignedTo NVARCHAR(250),
	@effort SMALLINT,
	@description NTEXT,
	@acceptanceCriteria NTEXT,
	@itterationId INT
AS
BEGIN
	UPDATE [tnyk].[WorkItem]
	SET [FeatureId] = @featureId, 
		[Title] = @title, 
		[State] = @state, 
		[AssignedTo] = @assignedTo, 
		[Effort] = @effort, 
		[Description] = @description, 
		[AcceptanceCriteria] = @acceptanceCriteria,
		[ItterationId] = @itterationId
	WHERE [WorkItemId] = @id
	;
END
