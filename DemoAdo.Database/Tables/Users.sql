CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL IDENTITY, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(320) NOT NULL, 
    [Passwd] BINARY(64) NOT NULL, 
    [Active] BIT NOT NULL
        CONSTRAINT DF_Users_Active DEFAULT (1), 
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]) 
)

GO

CREATE TRIGGER [dbo].[DATR_OnDeleteUser]
    ON [dbo].[Users]
    INSTEAD OF DELETE
    AS
    BEGIN
        Update Users Set Active = 0 where Id in (select Id from deleted)
    END