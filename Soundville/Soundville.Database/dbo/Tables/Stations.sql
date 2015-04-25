CREATE TABLE [dbo].[Stations]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR(50) NOT NULL, 
    [ImageFileName] NVARCHAR(256) NULL, 
    [UserId] INT NOT NULL, 
    CONSTRAINT [PK_Stations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Stations_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id])
)
