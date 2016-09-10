CREATE PROCEDURE [tnyk].[ISP_WorkItem]
	@id int out,
	@projectId int,
	@featureId int,
	@title NVARCHAR(500),
	@state SMALLINT,
	@assignedTo NVARCHAR(250),
	@effort SMALLINT,
	@description NTEXT,
	@acceptanceCriteria NTEXT
AS
BEGIN
	INSERT INTO [tnyk].[WorkItem] ([ProjectId], [FeatureId], [Title], [State], [AssignedTo], [Effort], [Description], [AcceptanceCriteria])
	VALUES (@projectId, @featureId, @title, @state, @assignedTo, @effort, @description, @acceptanceCriteria)
	;
	SET @id = SCOPE_IDENTITY();
END