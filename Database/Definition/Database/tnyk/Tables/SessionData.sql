CREATE TABLE [tnyk].[SessionData]
(
	[SessionDataId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[SessionGuid] BINARY(16) NOT NULL,
    [Name] VARCHAR(50) NOT NULL, 
    [Value] NVARCHAR(500) NOT NULL, 
    CONSTRAINT [FK_SessionData_To_Session] FOREIGN KEY ([SessionGuid]) REFERENCES tnyk.[Session]([SessionGuid])
)
