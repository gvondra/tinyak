CREATE TABLE [tnyk].[WorkItem]
(
	[WorkItemId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(500) NOT NULL, 
    [State] SMALLINT NOT NULL, 
    [AssignedTo] INT NULL, 
    [Effort] SMALLINT NULL, 
    [Description] NTEXT NOT NULL, 
    [AcceptanceCriteria] NTEXT NOT NULL
)
