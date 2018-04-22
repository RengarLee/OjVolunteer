CREATE TABLE [dbo].[Department] (
    [DepartmentID]   INT            IDENTITY (1, 1) NOT NULL,
    [DepartmentName] NVARCHAR (20)  NOT NULL,
    [CreateTime]     DATETIME       NULL,
    [ModfiedOn]      DATETIME       NULL,
    [Remark]         NVARCHAR (100) NULL,
    [Status]         SMALLINT       NOT NULL,
    CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED ([DepartmentID] ASC)
);

