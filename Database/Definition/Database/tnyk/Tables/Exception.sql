CREATE TABLE [tnyk].[Exception]
(
	[ExceptionId] INT NOT NULL PRIMARY KEY, 
	[ParentExceptionId] INT NULL,
    [TypeName] VARCHAR(500) NOT NULL, 
    [Message] NTEXT NOT NULL, 
    [Source] VARCHAR(500) NOT NULL, 
    [Target] VARCHAR(500) NOT NULL, 
    [StackTrace] NTEXT NOT NULL, 
    [HResult] INT NULL
)
