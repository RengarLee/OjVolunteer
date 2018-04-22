CREATE TABLE [dbo].[UserInfo] (
    [UserInfoID]        INT            IDENTITY (1, 1) NOT NULL,
    [UserInfoLoginId]   NVARCHAR (50)  NOT NULL,
    [UserInfoPwd]       NVARCHAR (50)  NOT NULL,
    [UserInfoStuId]     NVARCHAR (20)  NULL,
    [UserInfoShowName]  NVARCHAR (20)  NULL,
    [UserInfoPhone]     NVARCHAR (20)  NULL,
    [UserInfoEmail]     NVARCHAR (20)  NULL,
    [MajorID]           INT            NULL,
    [DepartmentID]      INT            NULL,
    [PoliticalID]       INT            NULL,
    [UpdatePoliticalID] INT            NULL,
    [OrganizeInfoID]    INT            NULL,
    [UserInfoLastTime]  DATETIME       NULL,
    [CreateTime]        DATETIME       NULL,
    [ModfiedOn]         DATETIME       NULL,
    [Remark]            NVARCHAR (100) NULL,
    [Status]            SMALLINT       NOT NULL,
    [UserInfoIcon]      NVARCHAR (200) NULL,
    [UserInfoTalkCount] INT            CONSTRAINT [DF_UserInfo_UserInfoTalkCount] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED ([UserInfoID] ASC),
    CONSTRAINT [FK_USERINFO_REFERENCE_DEPARTME] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[Department] ([DepartmentID]),
    CONSTRAINT [FK_USERINFO_REFERENCE_MAJOR] FOREIGN KEY ([MajorID]) REFERENCES [dbo].[Major] ([MajorID]),
    CONSTRAINT [FK_USERINFO_REFERENCE_ORGANIZE] FOREIGN KEY ([OrganizeInfoID]) REFERENCES [dbo].[OrganizeInfo] ([OrganizeInfoID]),
    CONSTRAINT [FK_USERINFO_REFERENCE_POLITICA] FOREIGN KEY ([PoliticalID]) REFERENCES [dbo].[Political] ([PoliticalID]),
    CONSTRAINT [FK_UserInfo_UpdatePolitical] FOREIGN KEY ([UpdatePoliticalID]) REFERENCES [dbo].[Political] ([PoliticalID])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_USERINFO_REFERENCE_DEPARTME]
    ON [dbo].[UserInfo]([DepartmentID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_USERINFO_REFERENCE_MAJOR]
    ON [dbo].[UserInfo]([MajorID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_USERINFO_REFERENCE_ORGANIZE]
    ON [dbo].[UserInfo]([OrganizeInfoID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_USERINFO_REFERENCE_POLITICA]
    ON [dbo].[UserInfo]([PoliticalID] ASC);

