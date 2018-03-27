
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/26/2014 14:06:46
-- Generated from EDMX file: C:\Users\1\Desktop\ScramperWPF\ScramperWPF\DB\HhScramperModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [HhScramper];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Settings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Settings];
GO
IF OBJECT_ID(N'[dbo].[Vacancies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Vacancies];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Settings'
CREATE TABLE [dbo].[Settings] (
    [UrlTb] varchar(max)  NULL,
    [XPathTb] varchar(max)  NULL,
    [UseExpressionsCb] bit  NULL,
    [CollectDataRb] bit  NULL,
    [CollectLinksRb] bit  NULL,
    [SettingsID] int  NOT NULL,
    [AttributeTb] varchar(max)  NULL,
    [DelayDeviationTb] varchar(max)  NULL,
    [DelayPodiumTb] varchar(max)  NULL
);
GO

-- Creating table 'Vacancies'
CREATE TABLE [dbo].[Vacancies] (
    [VacancyID] int IDENTITY(1,1) NOT NULL,
    [VacancyPageUrl] varchar(max)  NOT NULL,
    [VacancyPageHtml] varchar(max)  NOT NULL,
    [ScrampingDateTime] varchar(max)  NOT NULL,
    [VacancyPublicationDate] varchar(max)  NOT NULL,
    [DesiredPosition] varchar(max)  NOT NULL,
    [Salary] varchar(max)  NOT NULL,
    [VacancyPhotoUrl] varchar(max)  NOT NULL,
    [FIO] varchar(max)  NOT NULL,
    [AboutMe] varchar(max)  NOT NULL,
    [WorkExperience] varchar(max)  NOT NULL,
    [Age] varchar(max)  NOT NULL,
    [Phone] varchar(max)  NOT NULL,
    [Gender] varchar(max)  NOT NULL,
    [Address] varchar(max)  NOT NULL,
    [FieldOfActivity] varchar(max)  NOT NULL,
    [MailDeliveryStatus] int  NULL,
    [ReadyToBusinessTrip] varchar(max)  NOT NULL,
    [EMail] varchar(max)  NOT NULL,
    [WorkHistory] varchar(max)  NOT NULL,
    [RelativeFilePath] varchar(max)  NOT NULL,
    [HhVacancyID] varchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [SettingsID] in table 'Settings'
ALTER TABLE [dbo].[Settings]
ADD CONSTRAINT [PK_Settings]
    PRIMARY KEY CLUSTERED ([SettingsID] ASC);
GO

-- Creating primary key on [VacancyID] in table 'Vacancies'
ALTER TABLE [dbo].[Vacancies]
ADD CONSTRAINT [PK_Vacancies]
    PRIMARY KEY CLUSTERED ([VacancyID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------