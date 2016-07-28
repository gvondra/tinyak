CREATE TABLE [tnyk].[Task]
(
	[TaskId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(500) NOT NULL, 
    [Status] SMALLINT NOT NULL, 
    [AssignedTo] INT NULL, 
    [Remaining] INT NULL, 
    [Description] NTEXT NOT NULL
)
