CREATE TABLE [dbo].[ActivityDetail] (
    [ActivityDetailID]   INT             IDENTITY (1, 1) NOT NULL,
    [UserInfoId]         INT             NOT NULL,
    [ActivityID]         INT             NULL,
    [ActivityDetailTime] DECIMAL (10, 2) NOT NULL,
    [CreateTime]         DATETIME        NULL,
    [ModfiedOn]          DATETIME        NULL,
    [Remark]             NVARCHAR (100)  NULL,
    [Status]             SMALLINT        NOT NULL,
    CONSTRAINT [PK_ActivityDetail] PRIMARY KEY CLUSTERED ([ActivityDetailID] ASC),
    CONSTRAINT [FK_ACTIVITYDETAIL_REFERENCE_ACTIVITY] FOREIGN KEY ([ActivityID]) REFERENCES [dbo].[Activity] ([ActivityID]),
    CONSTRAINT [FK_ACTIVITYDETAIL_REFERENCE_USERINFO] FOREIGN KEY ([UserInfoId]) REFERENCES [dbo].[UserInfo] ([UserInfoID])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ACTIVITYDETAIL_REFERENCE_ACTIVITY]
    ON [dbo].[ActivityDetail]([ActivityID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ACTIVITYDETAIL_REFERENCE_USERINFO]
    ON [dbo].[ActivityDetail]([UserInfoId] ASC);

