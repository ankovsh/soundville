CREATE TABLE [dbo].[Votes]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[UserId] INT NOT NULL,	
	[StationSongId] INT NOT NULL,
	[Value] INT NOT NULL,
	CONSTRAINT [PK_Votes] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Votes_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]),
    CONSTRAINT [FK_Votes_StationSongs] FOREIGN KEY ([StationSongId]) REFERENCES [StationSongs]([Id])
)
