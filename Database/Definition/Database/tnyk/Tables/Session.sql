CREATE TABLE [tnyk].[Session] (
    [SessionGuid] BINARY(16) NOT NULL, 
    [UserId] INT NULL,
    [ExpirationDate] DATETIME NOT NULL, 
    PRIMARY KEY CLUSTERED ([SessionGuid]), 
    CONSTRAINT [FK_Session_To_User] FOREIGN KEY ([UserId]) REFERENCES [tnyk].[User]([UserId])
);

