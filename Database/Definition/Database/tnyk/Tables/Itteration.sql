CREATE TABLE [tnyk].[Itteration]
(
	[ItterationId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProjectId] INT NOT NULL, 
    [Name] NVARCHAR(100) NOT NULL, 
    [StartDate] DATE NULL, 
    [EndDate] DATE NULL, 
    [IsActive] BIT NOT NULL, 
    CONSTRAINT [FK_Itteration_To_Project] FOREIGN KEY ([ProjectId]) REFERENCES [tnyk].[Project]([ProjectId])
)
