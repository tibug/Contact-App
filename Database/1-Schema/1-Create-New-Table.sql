USE [DBContacts]
GO

-- Drop tables if they exist
IF(OBJECT_ID('dbo.vw_ContactCounts', 'V')) IS NOT NULL DROP VIEW dbo.vw_ContactCounts
IF OBJECT_ID('dbo.Contacts', 'U') IS NOT NULL DROP TABLE dbo.Contacts;
IF OBJECT_ID('dbo.Companies', 'U') IS NOT NULL DROP TABLE dbo.Companies;
IF OBJECT_ID('dbo.AccuracyScores', 'U') IS NOT NULL DROP TABLE dbo.AccuracyScores;
IF OBJECT_ID('dbo.CompanyFunctions', 'U') IS NOT NULL DROP TABLE dbo.CompanyFunctions;
IF OBJECT_ID('dbo.CompanyIndustries', 'U') IS NOT NULL DROP TABLE dbo.CompanyIndustries;
IF OBJECT_ID('dbo.CompanySectors', 'U') IS NOT NULL DROP TABLE dbo.CompanySectors;
IF OBJECT_ID('dbo.CompanySizes', 'U') IS NOT NULL DROP TABLE dbo.CompanySizes;
IF OBJECT_ID('dbo.CompanyTypes', 'U') IS NOT NULL DROP TABLE dbo.CompanyTypes;
IF OBJECT_ID('dbo.DecisionMakingPowers', 'U') IS NOT NULL DROP TABLE dbo.DecisionMakingPowers;
IF OBJECT_ID('dbo.LeadDivisions', 'U') IS NOT NULL DROP TABLE dbo.LeadDivisions;
IF OBJECT_ID('dbo.LeadLocations', 'U') IS NOT NULL DROP TABLE dbo.LeadLocations;
IF OBJECT_ID('dbo.LeadTitles', 'U') IS NOT NULL DROP TABLE dbo.LeadTitles;
IF OBJECT_ID('dbo.RevenueSizes', 'U') IS NOT NULL DROP TABLE dbo.RevenueSizes;
IF OBJECT_ID('dbo.SeniorityLevels', 'U') IS NOT NULL DROP TABLE dbo.SeniorityLevels;

-- Create tables
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
SET ANSI_PADDING ON

CREATE TABLE [dbo].[AccuracyScores](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Score] [int] NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Companies](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UniqueId] [nchar](100) NULL,
	[CompanyName] [nvarchar](1000) NULL,
	[CompanyWebsite] [nvarchar](250) NULL,
	[CompanyPhoneNumbers] [nvarchar](50) NULL,
	[CompanyLocationText] [nvarchar](700) NULL,
	[CompanyTypeId] [int] NULL,
	[CompanyFunctionId] [int] NULL,
	[CompanySectorId] [int] NULL,
	[CompanyIndustryId] [int] NULL,
	[CompanyFoundedAt] [int] NULL,
	[CompanyFundingRange] [varchar](200) NULL,
	[RevenueRange] [varchar](250) NULL,
	[EBITDARange] [varchar](250) NULL,
	[CompanyFacebookPage] [varchar](500) NULL,
	[CompanyTwitterPage] [varchar](500) NULL,
	[CompanyLinkedinPage] [varchar](500) NULL,
	[CompanySICCode] [int] NULL,
	[CompanyNAICSCode] [int] NULL,
	[CompanyDescription] [nvarchar](max) NULL,
	[CompanySizeId] [int] NULL,
	[CompanyProfileImageUrl] [varchar](500) NULL,
	[CompanyProductsServices] [nvarchar](2500) NULL,
	[keywords] [nvarchar](max) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[CompanyFunctions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyFunction] [nvarchar](1000) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([CompanyFunction] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CompanyIndustries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyIndustry] [nvarchar](1000) NULL,
	[Heading] [nvarchar](50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([CompanyIndustry] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CompanySectors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanySector] [nvarchar](250) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([CompanySector] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CompanySizes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanySize] [nvarchar](100) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([CompanySize] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CompanyTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyType] [nvarchar](250) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([CompanyType] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Contacts](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UniqueId] [nchar](100) NULL,
	[CompanyId] [bigint] NULL,
	[Name] [nvarchar](500) NULL,
	[Email] [nvarchar](150) NULL,
	[EmailScore] [int] NULL,
	[Phone] [nchar](150) NULL,
	[WorkPhone] [nchar](150) NULL,
	[LeadLocationId] [int] NULL,
	[LeadDivisionId] [int] NULL,
	[LeadTitle] [nvarchar](700) NULL,
	[DecisionMakingPowerId] [int] NULL,
	[SeniorityLevelId] [int] NULL,
	[LinkedInUrl] [varchar](700) NULL,
	[Skills] [nvarchar](max) NULL,
	[PastCompanies] [nvarchar](1500) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[DecisionMakingPowers](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [DecisionMakingPower] [nvarchar](150) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([DecisionMakingPower] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[LeadDivisions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Division] [nvarchar](255) NULL,
	[Heading] [nvarchar](50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Division] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[LeadLocations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Location] [nvarchar](500) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Location] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[LeadTitles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](250) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Title] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RevenueSizes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RevenueRange] [varchar](150) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SeniorityLevels](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [SeniorityLevel] [nvarchar](200) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([SeniorityLevel] ASC)
) ON [PRIMARY]
GO

CREATE VIEW dbo.vw_ContactCounts
WITH SCHEMABINDING AS
SELECT COUNT_BIG(1) AS RecordsTotal
FROM dbo.Contacts;
Go

-- Add Foreign Key Constraints
ALTER TABLE [dbo].[Companies] WITH CHECK ADD CONSTRAINT [FK_Companies_CompanyTypes] FOREIGN KEY([CompanyTypeId])
REFERENCES [dbo].[CompanyTypes] ([Id])
GO

ALTER TABLE [dbo].[Companies] WITH CHECK ADD CONSTRAINT [FK_Companies_CompanyFunctions] FOREIGN KEY([CompanyFunctionId])
REFERENCES [dbo].[CompanyFunctions] ([Id])
GO

ALTER TABLE [dbo].[Companies] WITH CHECK ADD CONSTRAINT [FK_Companies_CompanySectors] FOREIGN KEY([CompanySectorId])
REFERENCES [dbo].[CompanySectors] ([Id])
GO

ALTER TABLE [dbo].[Companies] WITH CHECK ADD CONSTRAINT [FK_Companies_CompanyIndustries] FOREIGN KEY([CompanyIndustryId])
REFERENCES [dbo].[CompanyIndustries] ([Id])
GO

ALTER TABLE [dbo].[Companies] WITH CHECK ADD CONSTRAINT [FK_Companies_CompanySizes] FOREIGN KEY([CompanySizeId])
REFERENCES [dbo].[CompanySizes] ([Id])
GO

ALTER TABLE [dbo].[Contacts] WITH CHECK ADD CONSTRAINT [FK_Contacts_Companies] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
GO

ALTER TABLE [dbo].[Contacts] WITH CHECK ADD CONSTRAINT [FK_Contacts_DecisionMakingPowers] FOREIGN KEY([DecisionMakingPowerId])
REFERENCES [dbo].[DecisionMakingPowers] ([Id])
GO

ALTER TABLE [dbo].[Contacts] WITH CHECK ADD CONSTRAINT [FK_Contacts_LeadDivisions] FOREIGN KEY([LeadDivisionId])
REFERENCES [dbo].[LeadDivisions] ([Id])
GO

ALTER TABLE [dbo].[Contacts] WITH CHECK ADD CONSTRAINT [FK_Contacts_LeadLocations] FOREIGN KEY([LeadLocationId])
REFERENCES [dbo].[LeadLocations] ([Id])
GO

ALTER TABLE [dbo].[Contacts] WITH CHECK ADD CONSTRAINT [FK_Contacts_SeniorityLevels] FOREIGN KEY([SeniorityLevelId])
REFERENCES [dbo].[SeniorityLevels] ([Id])
GO
