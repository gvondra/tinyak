CREATE TABLE [tnyk].[ProjectMember]
(
	[ProjectMemberId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProjectId]       INT NOT NULL,
	[EmailAddress]    NVARCHAR (250) NOT NULL, 
    CONSTRAINT [FK_ProjectMember_To_Project] FOREIGN KEY ([ProjectId]) REFERENCES [tnyk].[Project]([ProjectId]),
)
