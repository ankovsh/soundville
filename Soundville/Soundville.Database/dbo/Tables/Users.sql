CREATE TABLE [dbo].[Users] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (50)	  NULL,
	[Email]		   NVARCHAR (50)  NOT NULL,
	[ImageFileName]	NVARCHAR(256) NULL,
    [PasswordHash] NVARCHAR (MAX) NOT NULL,
    [Token] NVARCHAR(256) NULL, 
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

