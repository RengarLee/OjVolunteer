CREATE TABLE [dbo].[Activity] (
    [ActivityID]              INT             IDENTITY (1, 1) NOT NULL,
    [ActivityName]            NVARCHAR (20)   NOT NULL,
    [ActivityIcon]            NVARCHAR (100)  NOT NULL,
    [ActivityTypeID]          INT             NOT NULL,
    [ActivityAddress]         NVARCHAR (100)  NOT NULL,
    [ActivityAddressX]        DECIMAL (10, 6) NOT NULL,
    [ActivityAddressY]        DECIMAL (10, 6) NOT NULL,
    [ActivityContent]         NVARCHAR (500)  NOT NULL,
    [ActivityPrediNum]        INT             NOT NULL,
    [ActivityManagerID]       INT             NOT NULL,
    [ActivityApplyUserInfoID] INT             NULL,
    [ActivityApplyOrganizeID] INT             NULL,
    [AuditOrganizeInfoID]     INT             NOT NULL,
    [ActivityClicks]          INT             NOT NULL,
    [ActivityEnrollEnd]       DATETIME        NOT NULL,
    [ActivityEnrollStart]     DATETIME        NOT NULL,
    [ActivityStart]           DATETIME        NOT NULL,
    [ActivityEnd]             DATETIME        NOT NULL,
    [ActivityPolitical]       NVARCHAR (50)   NOT NULL,
    [ActivityMajor]           NVARCHAR (50)   NOT NULL,
    [ActivityDepartment]      NVARCHAR (50)   NOT NULL,
    [CreateTime]              DATETIME        NULL,
    [ModfiedOn]               DATETIME        NULL,
    [Remark]                  NVARCHAR (100)  NULL,
    [Status]                  SMALLINT        NOT NULL,
    CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED ([ActivityID] ASC),
    CONSTRAINT [FK_ACTIVITY_REFERENCE_ORGANIZE] FOREIGN KEY ([ActivityApplyOrganizeID]) REFERENCES [dbo].[OrganizeInfo] ([OrganizeInfoID]),
    CONSTRAINT [FK_ACTIVITY_REFERENCE_USERINFO] FOREIGN KEY ([ActivityApplyUserInfoID]) REFERENCES [dbo].[UserInfo] ([UserInfoID]),
    CONSTRAINT [FK_ACTIVITYAUDIT_REFERENCE_ORGANIZE] FOREIGN KEY ([AuditOrganizeInfoID]) REFERENCES [dbo].[OrganizeInfo] ([OrganizeInfoID]),
    CONSTRAINT [FK_ACTIVITYMANAGER_REFERENCE_USERINFO] FOREIGN KEY ([ActivityManagerID]) REFERENCES [dbo].[UserInfo] ([UserInfoID]),
    CONSTRAINT [FK_ACTIVITYTYPE_REFERENCE_ACTIVITY] FOREIGN KEY ([ActivityTypeID]) REFERENCES [dbo].[ActivityType] ([ActivityTypeID])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ACTIVITY_REFERENCE_ORGANIZE]
    ON [dbo].[Activity]([ActivityApplyOrganizeID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ACTIVITY_REFERENCE_USERINFO]
    ON [dbo].[Activity]([ActivityApplyUserInfoID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ACTIVITYAUDIT_REFERENCE_ORGANIZE]
    ON [dbo].[Activity]([AuditOrganizeInfoID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ACTIVITYMANAGER_REFERENCE_USERINFO]
    ON [dbo].[Activity]([ActivityManagerID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_ACTIVITYTYPE_REFERENCE_ACTIVITY]
    ON [dbo].[Activity]([ActivityTypeID] ASC);

