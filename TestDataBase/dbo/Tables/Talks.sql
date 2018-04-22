CREATE TABLE [dbo].[Talks] (
    [TalkID]         INT            IDENTITY (1, 1) NOT NULL,
    [UserInfoID]     INT            NULL,
    [TalkContent]    NVARCHAR (500) NOT NULL,
    [TalkFavorsNum]  INT            NULL,
    [CreateTime]     DATETIME       NULL,
    [ModfiedOn]      DATETIME       NULL,
    [Remark]         NVARCHAR (100) NULL,
    [Status]         SMALLINT       NOT NULL,
    [OrganizeInfoID] INT            NULL,
    [TalkImagePath]  NVARCHAR (200) NULL,
    CONSTRAINT [PK_Talks] PRIMARY KEY CLUSTERED ([TalkID] ASC),
    CONSTRAINT [FK_Talks_OrganizeInfo] FOREIGN KEY ([OrganizeInfoID]) REFERENCES [dbo].[OrganizeInfo] ([OrganizeInfoID]),
    CONSTRAINT [FK_Talks_UserInfo] FOREIGN KEY ([UserInfoID]) REFERENCES [dbo].[UserInfo] ([UserInfoID])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_TALKS_REFERENCE_USERINFO]
    ON [dbo].[Talks]([UserInfoID] ASC);

