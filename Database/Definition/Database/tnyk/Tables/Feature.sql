CREATE TABLE [tnyk].[Feature]
(
	[FeatureId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ProjectId] INT NOT NULL, 
    [Title] NVARCHAR(500) NOT NULL, 
    CONSTRAINT [FK_Feature_To_Project] FOREIGN KEY ([ProjectId]) REFERENCES [tnyk].[Project]([ProjectId])
)
