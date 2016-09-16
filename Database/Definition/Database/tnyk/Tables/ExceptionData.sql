CREATE TABLE [tnyk].[ExceptionData]
(
	[ExceptionDataId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ExceptionId] INT NOT NULL, 
    [Name] NVARCHAR(150) NOT NULL, 
    [Value] NTEXT NOT NULL, 
    CONSTRAINT [FK_ExceptionData_To_Exception] FOREIGN KEY ([ExceptionId]) REFERENCES [tnyk].[Exception]([ExceptionId])
)
