CREATE TABLE [tnyk].[Task]
(
	[TaskId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ProjectId] INT NOT NULL, 
	[WorkItemId] INT NOT NULL, 
    [Title] NVARCHAR(500) NOT NULL, 
    [Status] SMALLINT NOT NULL, 
    [AssignedTo] INT NULL, 
    [Remaining] INT NULL, 
    [Description] NTEXT NOT NULL, 
    CONSTRAINT [FK_Task_To_Project] FOREIGN KEY ([ProjectId]) REFERENCES [tnyk].[Project]([ProjectId]), 
    CONSTRAINT [FK_Task_To_WorkItem] FOREIGN KEY ([WorkItemId]) REFERENCES [tnyk].[WorkItem]([WorkItemId])
)
