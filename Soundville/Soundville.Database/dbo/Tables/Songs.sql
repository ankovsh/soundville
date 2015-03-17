﻿CREATE TABLE [dbo].[Songs]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
    [Title] NVARCHAR(256) NOT NULL, 
    [Artist] NVARCHAR(256) NULL, 
    CONSTRAINT [PK_Songs] PRIMARY KEY ([Id])
)
