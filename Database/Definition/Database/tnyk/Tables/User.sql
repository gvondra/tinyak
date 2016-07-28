CREATE TABLE [tnyk].[User] (
    [Id]              INT            NOT NULL,
    [Name]            NVARCHAR (100) NOT NULL,
    [EmailAddress]    NVARCHAR (250) NOT NULL,
    [PasswordToken]   BINARY (64)    NULL,
    [IsAdministrator] BIT            DEFAULT (0) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

