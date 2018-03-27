
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/13/2015 04:14:03
-- Generated from EDMX file: C:\Users\MERKATOR\Desktop\RequestService\RequestService.DataLayer\RequestModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Requests];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserRequest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RequestSet] DROP CONSTRAINT [FK_UserRequest];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UsersSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsersSet];
GO
IF OBJECT_ID(N'[dbo].[RequestSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RequestSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UsersSet'
CREATE TABLE [dbo].[UsersSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [SystemUserID] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'RequestSet'
CREATE TABLE [dbo].[RequestSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] int  NOT NULL,
    [CreationDate] datetime  NOT NULL,
    [Message] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL,
    [SystemRequestID] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UsersSet'
ALTER TABLE [dbo].[UsersSet]
ADD CONSTRAINT [PK_UsersSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RequestSet'
ALTER TABLE [dbo].[RequestSet]
ADD CONSTRAINT [PK_RequestSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'RequestSet'
ALTER TABLE [dbo].[RequestSet]
ADD CONSTRAINT [FK_UserRequest]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UsersSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRequest'
CREATE INDEX [IX_FK_UserRequest]
ON [dbo].[RequestSet]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------