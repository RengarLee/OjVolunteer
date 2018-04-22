CREATE TABLE [dbo].[Political] (
    [PoliticalID]   INT            IDENTITY (1, 1) NOT NULL,
    [PoliticalName] NVARCHAR (20)  NOT NULL,
    [CreateTime]    DATETIME       NULL,
    [ModfiedOn]     DATETIME       NULL,
    [Remark]        NVARCHAR (100) NULL,
    [Status]        SMALLINT       NOT NULL,
    CONSTRAINT [PK_Political] PRIMARY KEY CLUSTERED ([PoliticalID] ASC)
);

