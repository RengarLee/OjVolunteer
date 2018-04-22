CREATE TABLE [dbo].[OrganizeInfo] (
    [OrganizeInfoID]       INT            IDENTITY (1, 1) NOT NULL,
    [OrganizeInfoLoginId]  NVARCHAR (20)  NOT NULL,
    [OrganizeInfoShowName] NVARCHAR (20)  NULL,
    [OrganizeInfoPwd]      NVARCHAR (50)  NOT NULL,
    [OrganizeInfoPeople]   NVARCHAR (20)  NULL,
    [OrganizeInfoPhone]    NVARCHAR (20)  NULL,
    [OrganizeInfoEmail]    NVARCHAR (20)  NULL,
    [OrganizeInfoManageId] INT            NULL,
    [OrganizeInfoLastTime] DATETIME       NULL,
    [CreateTime]           DATETIME       NULL,
    [ModfiedOn]            DATETIME       NULL,
    [Remark]               NVARCHAR (100) NULL,
    [Status]               SMALLINT       NOT NULL,
    [OrganizeInfoIcon]     NVARCHAR (200) NULL,
    [ActivityCount]        INT            NULL,
    CONSTRAINT [PK_OrganizeInfo] PRIMARY KEY CLUSTERED ([OrganizeInfoID] ASC)
);

