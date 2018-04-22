CREATE TABLE [dbo].[ActivityType] (
    [ActivityTypeID]   INT            IDENTITY (1, 1) NOT NULL,
    [ActivityTypeName] NVARCHAR (20)  NOT NULL,
    [CreateTime]       DATETIME       NULL,
    [ModfiedOn]        DATETIME       NULL,
    [Remark]           NVARCHAR (100) NULL,
    [Status]           SMALLINT       NOT NULL,
    CONSTRAINT [PK_ActivityType] PRIMARY KEY CLUSTERED ([ActivityTypeID] ASC)
);

