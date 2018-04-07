
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/07/2018 14:41:54
-- Generated from EDMX file: E:\RengarLee\MyCode\C#\Source\OjVolunteer\OjVolunteer\OjVolunteer.Model\EFDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [OjVolunteer];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ACTIVITY_REFERENCE_ORGANIZE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Activity] DROP CONSTRAINT [FK_ACTIVITY_REFERENCE_ORGANIZE];
GO
IF OBJECT_ID(N'[dbo].[FK_ACTIVITY_REFERENCE_USERINFO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Activity] DROP CONSTRAINT [FK_ACTIVITY_REFERENCE_USERINFO];
GO
IF OBJECT_ID(N'[dbo].[FK_ACTIVITYAUDIT_REFERENCE_ORGANIZE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Activity] DROP CONSTRAINT [FK_ACTIVITYAUDIT_REFERENCE_ORGANIZE];
GO
IF OBJECT_ID(N'[dbo].[FK_ACTIVITYDETAIL_REFERENCE_ACTIVITY]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActivityDetail] DROP CONSTRAINT [FK_ACTIVITYDETAIL_REFERENCE_ACTIVITY];
GO
IF OBJECT_ID(N'[dbo].[FK_ACTIVITYDETAIL_REFERENCE_USERINFO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActivityDetail] DROP CONSTRAINT [FK_ACTIVITYDETAIL_REFERENCE_USERINFO];
GO
IF OBJECT_ID(N'[dbo].[FK_ACTIVITYMANAGER_REFERENCE_USERINFO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Activity] DROP CONSTRAINT [FK_ACTIVITYMANAGER_REFERENCE_USERINFO];
GO
IF OBJECT_ID(N'[dbo].[FK_ACTIVITYTYPE_REFERENCE_ACTIVITY]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Activity] DROP CONSTRAINT [FK_ACTIVITYTYPE_REFERENCE_ACTIVITY];
GO
IF OBJECT_ID(N'[dbo].[FK_FAVORS_REFERENCE_TALKS]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Favors] DROP CONSTRAINT [FK_FAVORS_REFERENCE_TALKS];
GO
IF OBJECT_ID(N'[dbo].[FK_FAVORS_REFERENCE_USERINFO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Favors] DROP CONSTRAINT [FK_FAVORS_REFERENCE_USERINFO];
GO
IF OBJECT_ID(N'[dbo].[FK_INTEGRAL_REFERENCE_USERINFO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Integrals] DROP CONSTRAINT [FK_INTEGRAL_REFERENCE_USERINFO];
GO
IF OBJECT_ID(N'[dbo].[FK_TALKS_REFERENCE_USERINFO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Talks] DROP CONSTRAINT [FK_TALKS_REFERENCE_USERINFO];
GO
IF OBJECT_ID(N'[dbo].[FK_USERDURA_REFERENCE_USERINFO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserDuration] DROP CONSTRAINT [FK_USERDURA_REFERENCE_USERINFO];
GO
IF OBJECT_ID(N'[dbo].[FK_USERENRO_REFERENCE_ACTIVITY]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserEnroll] DROP CONSTRAINT [FK_USERENRO_REFERENCE_ACTIVITY];
GO
IF OBJECT_ID(N'[dbo].[FK_USERENRO_REFERENCE_USERINFO]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserEnroll] DROP CONSTRAINT [FK_USERENRO_REFERENCE_USERINFO];
GO
IF OBJECT_ID(N'[dbo].[FK_USERINFO_REFERENCE_DEPARTME]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfo] DROP CONSTRAINT [FK_USERINFO_REFERENCE_DEPARTME];
GO
IF OBJECT_ID(N'[dbo].[FK_USERINFO_REFERENCE_MAJOR]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfo] DROP CONSTRAINT [FK_USERINFO_REFERENCE_MAJOR];
GO
IF OBJECT_ID(N'[dbo].[FK_USERINFO_REFERENCE_ORGANIZE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfo] DROP CONSTRAINT [FK_USERINFO_REFERENCE_ORGANIZE];
GO
IF OBJECT_ID(N'[dbo].[FK_USERINFO_REFERENCE_POLITICA]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfo] DROP CONSTRAINT [FK_USERINFO_REFERENCE_POLITICA];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Activity]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Activity];
GO
IF OBJECT_ID(N'[dbo].[ActivityDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActivityDetail];
GO
IF OBJECT_ID(N'[dbo].[ActivityType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActivityType];
GO
IF OBJECT_ID(N'[dbo].[Department]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Department];
GO
IF OBJECT_ID(N'[dbo].[Favors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Favors];
GO
IF OBJECT_ID(N'[dbo].[Integrals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Integrals];
GO
IF OBJECT_ID(N'[dbo].[Major]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Major];
GO
IF OBJECT_ID(N'[dbo].[OrganizeInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrganizeInfo];
GO
IF OBJECT_ID(N'[dbo].[Political]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Political];
GO
IF OBJECT_ID(N'[dbo].[Talks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Talks];
GO
IF OBJECT_ID(N'[dbo].[UserDuration]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserDuration];
GO
IF OBJECT_ID(N'[dbo].[UserEnroll]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserEnroll];
GO
IF OBJECT_ID(N'[dbo].[UserInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfo];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Activity'
CREATE TABLE [dbo].[Activity] (
    [ActivityID] int IDENTITY(1,1) NOT NULL,
    [ActivityName] nvarchar(20)  NOT NULL,
    [ActivityIcon] nvarchar(100)  NOT NULL,
    [ActivityTypeID] int  NOT NULL,
    [ActivityAddress] nvarchar(100)  NOT NULL,
    [ActivityAddressX] decimal(10,6)  NOT NULL,
    [ActivityAddressY] decimal(10,6)  NOT NULL,
    [ActivityContent] nvarchar(500)  NOT NULL,
    [ActivityPrediNum] int  NOT NULL,
    [ActivityManagerID] int  NOT NULL,
    [ActivityApplyUserInfoID] int  NULL,
    [ActivityApplyOrganizeID] int  NULL,
    [AuditOrganizeInfoID] int  NOT NULL,
    [ActivityClicks] int  NOT NULL,
    [ActivityEnrollEnd] datetime  NOT NULL,
    [ActivityEnrollStart] datetime  NOT NULL,
    [ActivityStart] datetime  NOT NULL,
    [ActivityEnd] datetime  NOT NULL,
    [ActivityPolitical] nvarchar(50)  NOT NULL,
    [ActivityMajor] nvarchar(50)  NOT NULL,
    [ActivityDepartment] nvarchar(50)  NOT NULL,
    [CreateTime] datetime  NULL,
    [ModfiedOn] datetime  NULL,
    [Remark] nvarchar(100)  NULL,
    [Status] smallint  NOT NULL
);
GO

-- Creating table 'ActivityDetail'
CREATE TABLE [dbo].[ActivityDetail] (
    [ActivityDetailID] int IDENTITY(1,1) NOT NULL,
    [UserInfoId] int  NOT NULL,
    [ActivityID] int  NULL,
    [ActivityDetailTime] decimal(10,2)  NOT NULL,
    [CreateTime] datetime  NULL,
    [ModfiedOn] datetime  NULL,
    [Remark] nvarchar(100)  NULL,
    [Status] smallint  NOT NULL
);
GO

-- Creating table 'ActivityType'
CREATE TABLE [dbo].[ActivityType] (
    [ActivityTypeID] int IDENTITY(1,1) NOT NULL,
    [ActivityTypeName] nvarchar(20)  NOT NULL,
    [CreateTime] datetime  NULL,
    [ModfiedOn] datetime  NULL,
    [Remark] nvarchar(100)  NULL,
    [Status] smallint  NOT NULL
);
GO

-- Creating table 'Department'
CREATE TABLE [dbo].[Department] (
    [department_id] int IDENTITY(1,1) NOT NULL,
    [department_name] nvarchar(20)  NOT NULL,
    [CreateTime] datetime  NULL,
    [ModfiedOn] datetime  NULL,
    [Remark] nvarchar(100)  NULL,
    [Status] smallint  NOT NULL
);
GO

-- Creating table 'Favors'
CREATE TABLE [dbo].[Favors] (
    [FavorID] int IDENTITY(1,1) NOT NULL,
    [TalkID] int  NOT NULL,
    [UserInfoID] int  NOT NULL,
    [CreateTime] datetime  NULL,
    [ModfiedOn] datetime  NULL,
    [Remark] nvarchar(100)  NULL,
    [Status] smallint  NOT NULL
);
GO

-- Creating table 'Integrals'
CREATE TABLE [dbo].[Integrals] (
    [IntegralID] int IDENTITY(1,1) NOT NULL,
    [UserInfoID] int  NOT NULL,
    [IntegralNum] int  NOT NULL,
    [IntegralIndex] decimal(5,2)  NULL,
    [CreateTime] datetime  NULL,
    [ModfiedOn] datetime  NULL,
    [Remark] nvarchar(100)  NULL,
    [Status] smallint  NOT NULL
);
GO

-- Creating table 'Major'
CREATE TABLE [dbo].[Major] (
    [MajorID] int IDENTITY(1,1) NOT NULL,
    [MajorName] nvarchar(50)  NOT NULL,
    [CreateTime] datetime  NULL,
    [ModfiedOn] datetime  NULL,
    [Remark] nvarchar(100)  NULL,
    [Status] smallint  NOT NULL
);
GO

-- Creating table 'OrganizeInfo'
CREATE TABLE [dbo].[OrganizeInfo] (
    [OrganizeInfoID] int IDENTITY(1,1) NOT NULL,
    [OrganizeInfLoginId] nvarchar(20)  NOT NULL,
    [OrganizeInfoShowName] nvarchar(20)  NOT NULL,
    [OrganizeInfoPwd] nvarchar(50)  NOT NULL,
    [OrganizeInfoPeople] nvarchar(20)  NOT NULL,
    [OrganizeInfoPhone] nvarchar(20)  NOT NULL,
    [OrganizeInfoEmail] nvarchar(20)  NOT NULL,
    [OrganizeInfoManageId] int  NULL,
    [OrganizeInfoLastTime] datetime  NOT NULL,
    [CreateTime] datetime  NULL,
    [ModfiedOn] datetime  NULL,
    [Remark] nvarchar(100)  NULL,
    [Status] smallint  NOT NULL
);
GO

-- Creating table 'Political'
CREATE TABLE [dbo].[Political] (
    [PoliticalID] int IDENTITY(1,1) NOT NULL,
    [PoliticalName] nvarchar(20)  NOT NULL,
    [CreateTime] datetime  NULL,
    [ModfiedOn] datetime  NULL,
    [Remark] nvarchar(100)  NULL,
    [Status] smallint  NOT NULL
);
GO

-- Creating table 'Talks'
CREATE TABLE [dbo].[Talks] (
    [TalkID] int IDENTITY(1,1) NOT NULL,
    [UserInfoID] int  NOT NULL,
    [TalkContent] nvarchar(500)  NOT NULL,
    [TalkFavorsNum] int  NOT NULL,
    [CreateTime] datetime  NULL,
    [ModfiedOn] datetime  NULL,
    [Remark] nvarchar(100)  NULL,
    [Status] smallint  NOT NULL
);
GO

-- Creating table 'UserDuration'
CREATE TABLE [dbo].[UserDuration] (
    [UserDurationID] int IDENTITY(1,1) NOT NULL,
    [UserInfoID] int  NOT NULL,
    [UserDurationTotal] decimal(10,2)  NOT NULL,
    [UserDurationPropartyTotal] decimal(10,2)  NOT NULL,
    [UserDurationPartyTotal] decimal(10,2)  NOT NULL,
    [UserDurationPropartyTime] datetime  NOT NULL,
    [UserDurationPartyTime] datetime  NOT NULL,
    [CreateTime] datetime  NULL,
    [ModfiedOn] datetime  NULL,
    [Remark] nvarchar(100)  NULL,
    [Status] smallint  NOT NULL
);
GO

-- Creating table 'UserEnroll'
CREATE TABLE [dbo].[UserEnroll] (
    [UserEnrollID] int IDENTITY(1,1) NOT NULL,
    [ActivityID] int  NOT NULL,
    [UserInfoID] int  NOT NULL,
    [UserEnrollStart] datetime  NULL,
    [UserEnrollActivityStart] datetime  NOT NULL,
    [UserEnrollActivityEnd] datetime  NOT NULL,
    [CreateTime] datetime  NULL,
    [ModfiedOn] datetime  NULL,
    [Remark] nvarchar(100)  NULL,
    [Status] smallint  NOT NULL
);
GO

-- Creating table 'UserInfo'
CREATE TABLE [dbo].[UserInfo] (
    [UserInfoID] int  NOT NULL,
    [UserInfoLoginId] nvarchar(50)  NOT NULL,
    [UserInfoPwd] nvarchar(50)  NOT NULL,
    [UserInfoStuId] nvarchar(20)  NOT NULL,
    [UserInfoShowName] nvarchar(20)  NOT NULL,
    [UserInfoPhone] nvarchar(20)  NOT NULL,
    [UserInfoEmail] nvarchar(20)  NOT NULL,
    [MajorID] int  NULL,
    [DepartmentID] int  NULL,
    [PoliticalID] int  NULL,
    [UpdateDepartmentID] int  NULL,
    [OrganizeinfoID] int  NULL,
    [UserInfoLastTime] datetime  NOT NULL,
    [CreateTime] datetime  NULL,
    [ModfiedOn] datetime  NULL,
    [Remark] nvarchar(100)  NULL,
    [Status] smallint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ActivityID] in table 'Activity'
ALTER TABLE [dbo].[Activity]
ADD CONSTRAINT [PK_Activity]
    PRIMARY KEY CLUSTERED ([ActivityID] ASC);
GO

-- Creating primary key on [ActivityDetailID] in table 'ActivityDetail'
ALTER TABLE [dbo].[ActivityDetail]
ADD CONSTRAINT [PK_ActivityDetail]
    PRIMARY KEY CLUSTERED ([ActivityDetailID] ASC);
GO

-- Creating primary key on [ActivityTypeID] in table 'ActivityType'
ALTER TABLE [dbo].[ActivityType]
ADD CONSTRAINT [PK_ActivityType]
    PRIMARY KEY CLUSTERED ([ActivityTypeID] ASC);
GO

-- Creating primary key on [department_id] in table 'Department'
ALTER TABLE [dbo].[Department]
ADD CONSTRAINT [PK_Department]
    PRIMARY KEY CLUSTERED ([department_id] ASC);
GO

-- Creating primary key on [FavorID] in table 'Favors'
ALTER TABLE [dbo].[Favors]
ADD CONSTRAINT [PK_Favors]
    PRIMARY KEY CLUSTERED ([FavorID] ASC);
GO

-- Creating primary key on [IntegralID] in table 'Integrals'
ALTER TABLE [dbo].[Integrals]
ADD CONSTRAINT [PK_Integrals]
    PRIMARY KEY CLUSTERED ([IntegralID] ASC);
GO

-- Creating primary key on [MajorID] in table 'Major'
ALTER TABLE [dbo].[Major]
ADD CONSTRAINT [PK_Major]
    PRIMARY KEY CLUSTERED ([MajorID] ASC);
GO

-- Creating primary key on [OrganizeInfoID] in table 'OrganizeInfo'
ALTER TABLE [dbo].[OrganizeInfo]
ADD CONSTRAINT [PK_OrganizeInfo]
    PRIMARY KEY CLUSTERED ([OrganizeInfoID] ASC);
GO

-- Creating primary key on [PoliticalID] in table 'Political'
ALTER TABLE [dbo].[Political]
ADD CONSTRAINT [PK_Political]
    PRIMARY KEY CLUSTERED ([PoliticalID] ASC);
GO

-- Creating primary key on [TalkID] in table 'Talks'
ALTER TABLE [dbo].[Talks]
ADD CONSTRAINT [PK_Talks]
    PRIMARY KEY CLUSTERED ([TalkID] ASC);
GO

-- Creating primary key on [UserDurationID] in table 'UserDuration'
ALTER TABLE [dbo].[UserDuration]
ADD CONSTRAINT [PK_UserDuration]
    PRIMARY KEY CLUSTERED ([UserDurationID] ASC);
GO

-- Creating primary key on [UserEnrollID] in table 'UserEnroll'
ALTER TABLE [dbo].[UserEnroll]
ADD CONSTRAINT [PK_UserEnroll]
    PRIMARY KEY CLUSTERED ([UserEnrollID] ASC);
GO

-- Creating primary key on [UserInfoID] in table 'UserInfo'
ALTER TABLE [dbo].[UserInfo]
ADD CONSTRAINT [PK_UserInfo]
    PRIMARY KEY CLUSTERED ([UserInfoID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ActivityApplyOrganizeID] in table 'Activity'
ALTER TABLE [dbo].[Activity]
ADD CONSTRAINT [FK_ACTIVITY_REFERENCE_ORGANIZE]
    FOREIGN KEY ([ActivityApplyOrganizeID])
    REFERENCES [dbo].[OrganizeInfo]
        ([OrganizeInfoID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ACTIVITY_REFERENCE_ORGANIZE'
CREATE INDEX [IX_FK_ACTIVITY_REFERENCE_ORGANIZE]
ON [dbo].[Activity]
    ([ActivityApplyOrganizeID]);
GO

-- Creating foreign key on [ActivityApplyUserInfoID] in table 'Activity'
ALTER TABLE [dbo].[Activity]
ADD CONSTRAINT [FK_ACTIVITY_REFERENCE_USERINFO]
    FOREIGN KEY ([ActivityApplyUserInfoID])
    REFERENCES [dbo].[UserInfo]
        ([UserInfoID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ACTIVITY_REFERENCE_USERINFO'
CREATE INDEX [IX_FK_ACTIVITY_REFERENCE_USERINFO]
ON [dbo].[Activity]
    ([ActivityApplyUserInfoID]);
GO

-- Creating foreign key on [AuditOrganizeInfoID] in table 'Activity'
ALTER TABLE [dbo].[Activity]
ADD CONSTRAINT [FK_ACTIVITYAUDIT_REFERENCE_ORGANIZE]
    FOREIGN KEY ([AuditOrganizeInfoID])
    REFERENCES [dbo].[OrganizeInfo]
        ([OrganizeInfoID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ACTIVITYAUDIT_REFERENCE_ORGANIZE'
CREATE INDEX [IX_FK_ACTIVITYAUDIT_REFERENCE_ORGANIZE]
ON [dbo].[Activity]
    ([AuditOrganizeInfoID]);
GO

-- Creating foreign key on [ActivityID] in table 'ActivityDetail'
ALTER TABLE [dbo].[ActivityDetail]
ADD CONSTRAINT [FK_ACTIVITYDETAIL_REFERENCE_ACTIVITY]
    FOREIGN KEY ([ActivityID])
    REFERENCES [dbo].[Activity]
        ([ActivityID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ACTIVITYDETAIL_REFERENCE_ACTIVITY'
CREATE INDEX [IX_FK_ACTIVITYDETAIL_REFERENCE_ACTIVITY]
ON [dbo].[ActivityDetail]
    ([ActivityID]);
GO

-- Creating foreign key on [ActivityManagerID] in table 'Activity'
ALTER TABLE [dbo].[Activity]
ADD CONSTRAINT [FK_ACTIVITYMANAGER_REFERENCE_USERINFO]
    FOREIGN KEY ([ActivityManagerID])
    REFERENCES [dbo].[UserInfo]
        ([UserInfoID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ACTIVITYMANAGER_REFERENCE_USERINFO'
CREATE INDEX [IX_FK_ACTIVITYMANAGER_REFERENCE_USERINFO]
ON [dbo].[Activity]
    ([ActivityManagerID]);
GO

-- Creating foreign key on [ActivityTypeID] in table 'Activity'
ALTER TABLE [dbo].[Activity]
ADD CONSTRAINT [FK_ACTIVITYTYPE_REFERENCE_ACTIVITY]
    FOREIGN KEY ([ActivityTypeID])
    REFERENCES [dbo].[ActivityType]
        ([ActivityTypeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ACTIVITYTYPE_REFERENCE_ACTIVITY'
CREATE INDEX [IX_FK_ACTIVITYTYPE_REFERENCE_ACTIVITY]
ON [dbo].[Activity]
    ([ActivityTypeID]);
GO

-- Creating foreign key on [ActivityID] in table 'UserEnroll'
ALTER TABLE [dbo].[UserEnroll]
ADD CONSTRAINT [FK_USERENRO_REFERENCE_ACTIVITY]
    FOREIGN KEY ([ActivityID])
    REFERENCES [dbo].[Activity]
        ([ActivityID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_USERENRO_REFERENCE_ACTIVITY'
CREATE INDEX [IX_FK_USERENRO_REFERENCE_ACTIVITY]
ON [dbo].[UserEnroll]
    ([ActivityID]);
GO

-- Creating foreign key on [UserInfoId] in table 'ActivityDetail'
ALTER TABLE [dbo].[ActivityDetail]
ADD CONSTRAINT [FK_ACTIVITYDETAIL_REFERENCE_USERINFO]
    FOREIGN KEY ([UserInfoId])
    REFERENCES [dbo].[UserInfo]
        ([UserInfoID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ACTIVITYDETAIL_REFERENCE_USERINFO'
CREATE INDEX [IX_FK_ACTIVITYDETAIL_REFERENCE_USERINFO]
ON [dbo].[ActivityDetail]
    ([UserInfoId]);
GO

-- Creating foreign key on [DepartmentID] in table 'UserInfo'
ALTER TABLE [dbo].[UserInfo]
ADD CONSTRAINT [FK_USERINFO_REFERENCE_DEPARTME]
    FOREIGN KEY ([DepartmentID])
    REFERENCES [dbo].[Department]
        ([department_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_USERINFO_REFERENCE_DEPARTME'
CREATE INDEX [IX_FK_USERINFO_REFERENCE_DEPARTME]
ON [dbo].[UserInfo]
    ([DepartmentID]);
GO

-- Creating foreign key on [TalkID] in table 'Favors'
ALTER TABLE [dbo].[Favors]
ADD CONSTRAINT [FK_FAVORS_REFERENCE_TALKS]
    FOREIGN KEY ([TalkID])
    REFERENCES [dbo].[Talks]
        ([TalkID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FAVORS_REFERENCE_TALKS'
CREATE INDEX [IX_FK_FAVORS_REFERENCE_TALKS]
ON [dbo].[Favors]
    ([TalkID]);
GO

-- Creating foreign key on [UserInfoID] in table 'Favors'
ALTER TABLE [dbo].[Favors]
ADD CONSTRAINT [FK_FAVORS_REFERENCE_USERINFO]
    FOREIGN KEY ([UserInfoID])
    REFERENCES [dbo].[UserInfo]
        ([UserInfoID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FAVORS_REFERENCE_USERINFO'
CREATE INDEX [IX_FK_FAVORS_REFERENCE_USERINFO]
ON [dbo].[Favors]
    ([UserInfoID]);
GO

-- Creating foreign key on [UserInfoID] in table 'Integrals'
ALTER TABLE [dbo].[Integrals]
ADD CONSTRAINT [FK_INTEGRAL_REFERENCE_USERINFO]
    FOREIGN KEY ([UserInfoID])
    REFERENCES [dbo].[UserInfo]
        ([UserInfoID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_INTEGRAL_REFERENCE_USERINFO'
CREATE INDEX [IX_FK_INTEGRAL_REFERENCE_USERINFO]
ON [dbo].[Integrals]
    ([UserInfoID]);
GO

-- Creating foreign key on [MajorID] in table 'UserInfo'
ALTER TABLE [dbo].[UserInfo]
ADD CONSTRAINT [FK_USERINFO_REFERENCE_MAJOR]
    FOREIGN KEY ([MajorID])
    REFERENCES [dbo].[Major]
        ([MajorID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_USERINFO_REFERENCE_MAJOR'
CREATE INDEX [IX_FK_USERINFO_REFERENCE_MAJOR]
ON [dbo].[UserInfo]
    ([MajorID]);
GO

-- Creating foreign key on [OrganizeinfoID] in table 'UserInfo'
ALTER TABLE [dbo].[UserInfo]
ADD CONSTRAINT [FK_USERINFO_REFERENCE_ORGANIZE]
    FOREIGN KEY ([OrganizeinfoID])
    REFERENCES [dbo].[OrganizeInfo]
        ([OrganizeInfoID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_USERINFO_REFERENCE_ORGANIZE'
CREATE INDEX [IX_FK_USERINFO_REFERENCE_ORGANIZE]
ON [dbo].[UserInfo]
    ([OrganizeinfoID]);
GO

-- Creating foreign key on [PoliticalID] in table 'UserInfo'
ALTER TABLE [dbo].[UserInfo]
ADD CONSTRAINT [FK_USERINFO_REFERENCE_POLITICA]
    FOREIGN KEY ([PoliticalID])
    REFERENCES [dbo].[Political]
        ([PoliticalID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_USERINFO_REFERENCE_POLITICA'
CREATE INDEX [IX_FK_USERINFO_REFERENCE_POLITICA]
ON [dbo].[UserInfo]
    ([PoliticalID]);
GO

-- Creating foreign key on [UserInfoID] in table 'Talks'
ALTER TABLE [dbo].[Talks]
ADD CONSTRAINT [FK_TALKS_REFERENCE_USERINFO]
    FOREIGN KEY ([UserInfoID])
    REFERENCES [dbo].[UserInfo]
        ([UserInfoID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TALKS_REFERENCE_USERINFO'
CREATE INDEX [IX_FK_TALKS_REFERENCE_USERINFO]
ON [dbo].[Talks]
    ([UserInfoID]);
GO

-- Creating foreign key on [UserInfoID] in table 'UserDuration'
ALTER TABLE [dbo].[UserDuration]
ADD CONSTRAINT [FK_USERDURA_REFERENCE_USERINFO]
    FOREIGN KEY ([UserInfoID])
    REFERENCES [dbo].[UserInfo]
        ([UserInfoID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_USERDURA_REFERENCE_USERINFO'
CREATE INDEX [IX_FK_USERDURA_REFERENCE_USERINFO]
ON [dbo].[UserDuration]
    ([UserInfoID]);
GO

-- Creating foreign key on [UserInfoID] in table 'UserEnroll'
ALTER TABLE [dbo].[UserEnroll]
ADD CONSTRAINT [FK_USERENRO_REFERENCE_USERINFO]
    FOREIGN KEY ([UserInfoID])
    REFERENCES [dbo].[UserInfo]
        ([UserInfoID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_USERENRO_REFERENCE_USERINFO'
CREATE INDEX [IX_FK_USERENRO_REFERENCE_USERINFO]
ON [dbo].[UserEnroll]
    ([UserInfoID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------