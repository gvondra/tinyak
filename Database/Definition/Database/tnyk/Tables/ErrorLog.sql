CREATE TABLE [tnyk].[ErrorLog]
(
	[ErrorLogId] INT NOT NULL PRIMARY KEY, 
    [MachineName] VARCHAR(50) NOT NULL, 
    [AppDomainName] VARCHAR(50) NOT NULL, 
    [ThreadIdentity] VARCHAR(25) NOT NULL, 
    [WindowsIdentity] VARCHAR(25) NOT NULL, 
    [CreateTimestamp] DATETIME NOT NULL
)
