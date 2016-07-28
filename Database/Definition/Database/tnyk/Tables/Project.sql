CREATE TABLE [tnyk].[Project]
(
	[ProjectId] INT NOT NULL PRIMARY KEY IDENTITY,  
    [OwnerId] INT NOT NULL,
    [Title] NVARCHAR(500) NOT NULL
)
