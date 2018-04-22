CREATE TABLE [dbo].[UserDuration] (
    [UserDurationID]            INT             NOT NULL,
    [UserDurationTotal]         DECIMAL (10, 2) NULL,
    [UserDurationPropartyTotal] DECIMAL (10, 2) NULL,
    [UserDurationPartyTotal]    DECIMAL (10, 2) NULL,
    [UserDurationPropartyTime]  DATETIME        NULL,
    [UserDurationPartyTime]     DATETIME        NULL,
    [CreateTime]                DATETIME        NULL,
    [ModfiedOn]                 DATETIME        NULL,
    [Remark]                    NVARCHAR (100)  NULL,
    [Status]                    SMALLINT        NOT NULL,
    [UserDurationNormalTotal]   DECIMAL (10, 2) NULL,
    CONSTRAINT [PK_UserDuration] PRIMARY KEY CLUSTERED ([UserDurationID] ASC),
    CONSTRAINT [FK_UserDuration_UserInfo] FOREIGN KEY ([UserDurationID]) REFERENCES [dbo].[UserInfo] ([UserInfoID])
);

