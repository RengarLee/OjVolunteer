CREATE TABLE [dbo].[Major] (
    [MajorID]    INT            IDENTITY (1, 1) NOT NULL,
    [MajorName]  NVARCHAR (50)  NOT NULL,
    [CreateTime] DATETIME       NULL,
    [ModfiedOn]  DATETIME       NULL,
    [Remark]     NVARCHAR (100) NULL,
    [Status]     SMALLINT       NOT NULL,
    CONSTRAINT [PK_Major] PRIMARY KEY CLUSTERED ([MajorID] ASC)
);

