CREATE TABLE [dbo].[StationSongs]
(
	[Id] INT IDENTITY (1, 1) NOT NULL, 
    [SongId] INT NOT NULL, 
    [StationId] INT NOT NULL, 
    [SongUrl] NVARCHAR(512) NULL, 
    [FileName] NVARCHAR(512) NULL, 
    [Duration] INT NULL,
	[Position] INT NOT NULL, 
    CONSTRAINT [PK_StationSongs] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_StationSongs_Songs] FOREIGN KEY ([SongId]) REFERENCES [Songs]([Id]),
	CONSTRAINT [FK_StationSongs_Stations] FOREIGN KEY ([StationId]) REFERENCES [Stations]([Id])
)
