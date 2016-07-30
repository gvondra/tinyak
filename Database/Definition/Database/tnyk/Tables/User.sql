CREATE TABLE [tnyk].[User] (
    [UserId]          INT            NOT NULL PRIMARY KEY IDENTITY,
    [Name]            NVARCHAR (100) NOT NULL,
    [EmailAddress]    NVARCHAR (250) NOT NULL,
    [PasswordToken]   BINARY (64)    NULL,
    [IsAdministrator] BIT            DEFAULT (0) NOT NULL, 
    [Salt]            BIGINT         NOT NULL
);


GO

CREATE UNIQUE INDEX [IX_User_EmailAddress] ON [tnyk].[User] ([EmailAddress])
