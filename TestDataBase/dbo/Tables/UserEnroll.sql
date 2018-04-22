CREATE TABLE [dbo].[UserEnroll] (
    [UserEnrollID]            INT            IDENTITY (1, 1) NOT NULL,
    [ActivityID]              INT            NOT NULL,
    [UserInfoID]              INT            NOT NULL,
    [UserEnrollStart]         DATETIME       NULL,
    [UserEnrollActivityStart] DATETIME       NOT NULL,
    [UserEnrollActivityEnd]   DATETIME       NOT NULL,
    [CreateTime]              DATETIME       NULL,
    [ModfiedOn]               DATETIME       NULL,
    [Remark]                  NVARCHAR (100) NULL,
    [Status]                  SMALLINT       NOT NULL,
    CONSTRAINT [PK_UserEnroll] PRIMARY KEY CLUSTERED ([UserEnrollID] ASC),
    CONSTRAINT [FK_USERENRO_REFERENCE_ACTIVITY] FOREIGN KEY ([ActivityID]) REFERENCES [dbo].[Activity] ([ActivityID]),
    CONSTRAINT [FK_USERENRO_REFERENCE_USERINFO] FOREIGN KEY ([UserInfoID]) REFERENCES [dbo].[UserInfo] ([UserInfoID])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_USERENRO_REFERENCE_ACTIVITY]
    ON [dbo].[UserEnroll]([ActivityID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_USERENRO_REFERENCE_USERINFO]
    ON [dbo].[UserEnroll]([UserInfoID] ASC);

