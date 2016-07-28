CREATE TABLE [tnyk].[ServiceCall]
(
	[ServiceCallId] INT NOT NULL PRIMARY KEY, 
    [User] NVARCHAR(250) NOT NULL, 
    [EndpointName] VARCHAR(150) NOT NULL, 
    [MethodName] VARCHAR(150) NOT NULL, 
    [Duration] FLOAT NOT NULL, 
    [CreateTimestamp] DATETIME NOT NULL
)
