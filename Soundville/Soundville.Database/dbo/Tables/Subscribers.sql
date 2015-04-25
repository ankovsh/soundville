CREATE TABLE [dbo].[Subscribers]
(
    [UserId] INT NOT NULL, 
    [StationId] INT NOT NULL,
    CONSTRAINT [PK_Subscribers] PRIMARY KEY ([StationId], [UserId]),
    CONSTRAINT [FK_Subscribers_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]),
	CONSTRAINT [FK_Subscribers_Stations] FOREIGN KEY ([StationId]) REFERENCES [Stations]([Id])
)
