CREATE PROCEDURE [tnyk].[ISP_WorkItem]
	@id int out,
	@projectId int,
	@featureId int,
	@title NVARCHAR(500),
	@state SMALLINT,
	@assignedTo NVARCHAR(250),
	@effort SMALLINT,
	@description NTEXT,
	@acceptanceCriteria NTEXT,
	@itterationId int
AS
BEGIN
	INSERT INTO [tnyk].[WorkItem] ([ProjectId], [FeatureId], [Title], [State], [AssignedTo], [Effort], [Description], [AcceptanceCriteria], [ItterationId])
	VALUES (@projectId, @featureId, @title, @state, @assignedTo, @effort, @description, @acceptanceCriteria, @itterationId)
	;
	SET @id = SCOPE_IDENTITY();
END