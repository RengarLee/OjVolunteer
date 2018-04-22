CREATE TABLE [dbo].[Integrals] (
    [IntegralID]    INT            IDENTITY (1, 1) NOT NULL,
    [UserInfoID]    INT            NOT NULL,
    [IntegralNum]   INT            NOT NULL,
    [IntegralIndex] DECIMAL (5, 2) NULL,
    [CreateTime]    DATETIME       NULL,
    [ModfiedOn]     DATETIME       NULL,
    [Remark]        NVARCHAR (100) NULL,
    [Status]        SMALLINT       NOT NULL,
    CONSTRAINT [PK_Integrals] PRIMARY KEY CLUSTERED ([IntegralID] ASC),
    CONSTRAINT [FK_INTEGRAL_REFERENCE_USERINFO] FOREIGN KEY ([UserInfoID]) REFERENCES [dbo].[UserInfo] ([UserInfoID])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_INTEGRAL_REFERENCE_USERINFO]
    ON [dbo].[Integrals]([UserInfoID] ASC);

