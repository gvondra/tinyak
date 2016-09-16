CREATE TABLE [tnyk].[WebMetrics]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Url] NVARCHAR(300) NOT NULL, 
    [Controller] VARCHAR(50) NOT NULL, 
    [Action] VARCHAR(50) NOT NULL, 
    [Timestamp] DATETIME NOT NULL, 
    [Duration] FLOAT NULL, 
    [ContentEncoding] VARCHAR(50) NOT NULL, 
    [ContentLength] INT NULL, 
    [ContentType] VARCHAR(50) NOT NULL, 
    [Method] VARCHAR(10) NOT NULL, 
    [RequestType] VARCHAR(50) NOT NULL, 
    [TotalBytes] INT NULL, 
    [UrlReferrer] NVARCHAR(300) NOT NULL, 
    [UserAgent] NVARCHAR(300) NOT NULL, 
    [Parameters] NTEXT NULL
)
