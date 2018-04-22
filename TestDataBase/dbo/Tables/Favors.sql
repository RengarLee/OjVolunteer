CREATE TABLE [dbo].[Favors] (
    [FavorID]    INT            IDENTITY (1, 1) NOT NULL,
    [TalkID]     INT            NOT NULL,
    [UserInfoID] INT            NOT NULL,
    [CreateTime] DATETIME       NULL,
    [ModfiedOn]  DATETIME       NULL,
    [Remark]     NVARCHAR (100) NULL,
    [Status]     SMALLINT       NOT NULL,
    CONSTRAINT [PK_Favors] PRIMARY KEY CLUSTERED ([FavorID] ASC),
    CONSTRAINT [FK_FAVORS_REFERENCE_TALKS] FOREIGN KEY ([TalkID]) REFERENCES [dbo].[Talks] ([TalkID]),
    CONSTRAINT [FK_FAVORS_REFERENCE_USERINFO] FOREIGN KEY ([UserInfoID]) REFERENCES [dbo].[UserInfo] ([UserInfoID])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_FAVORS_REFERENCE_TALKS]
    ON [dbo].[Favors]([TalkID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_FAVORS_REFERENCE_USERINFO]
    ON [dbo].[Favors]([UserInfoID] ASC);

