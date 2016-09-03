CREATE TABLE [tnyk].[WorkItem]
(
	[WorkItemId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ProjectId] INT NOT NULL, 
	[FeatureId] INT NOT NULL, 
    [Title] NVARCHAR(500) NOT NULL, 
    [State] SMALLINT NOT NULL, 
    [AssignedTo] INT NULL, 
    [Effort] SMALLINT NULL, 
    [Description] NTEXT NOT NULL, 
    [AcceptanceCriteria] NTEXT NOT NULL, 
    CONSTRAINT [FK_WorkItem_To_Feature] FOREIGN KEY ([FeatureId]) REFERENCES [tnyk].[Feature]([FeatureId]), 
    CONSTRAINT [FK_WorkItem_To_Project] FOREIGN KEY ([ProjectId]) REFERENCES [tnyk].[Project]([ProjectId])
)
