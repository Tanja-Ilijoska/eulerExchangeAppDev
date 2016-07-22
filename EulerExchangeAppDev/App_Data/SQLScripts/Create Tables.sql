CREATE TABLE [dbo].[Companies] (
    [Id]                    INT             IDENTITY (1, 1) NOT NULL,
    [ContactPersonName]     NVARCHAR (50)   NOT NULL,
    [ContactPersonLastName] NVARCHAR (50)   NOT NULL,
    [CompanyName]           NVARCHAR (250)  NOT NULL,
    [YearFounded]           INT             NOT NULL,
    [NumberOfEmployees]     INT             NULL,
    [YearlyRevenue]         DECIMAL (18, 4) NULL,
    [CompanyAddress]        NVARCHAR (50)   NOT NULL,
    [CompanyCountry]        NVARCHAR (50)   NOT NULL,
    [CompanyCity]           NVARCHAR (50)   NOT NULL,
    [CompanyPhone]          NVARCHAR (50)   NULL,
    [CompanyWebsite]        NVARCHAR (50)   NULL,
    [AdditionalEMails]      NVARCHAR (250)  NULL,
    [CompanyLocation]       NVARCHAR (50)   NULL,
    [UserId]                NVARCHAR (128)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[CompanyType] (
    [Id]   INT        IDENTITY (1, 1) NOT NULL,
    [Type] NCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[CompaniesCompanyType](
	[CompanyId] [int] NOT NULL,
	[CompanyTypeId] [int] NOT NULL,
 CONSTRAINT [PK_CompaniesCompanyType] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC,
	[CompanyTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CompaniesCompanyType]  WITH CHECK ADD  CONSTRAINT [FK_CompaniesCompanyType_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
GO

ALTER TABLE [dbo].[CompaniesCompanyType] CHECK CONSTRAINT [FK_CompaniesCompanyType_Company]
GO

ALTER TABLE [dbo].[CompaniesCompanyType]  WITH CHECK ADD  CONSTRAINT [FK_CompaniesCompanyType_CompanyTypeId] FOREIGN KEY([CompanyTypeId])
REFERENCES [dbo].[CompanyType] ([Id])
GO

ALTER TABLE [dbo].[CompaniesCompanyType] CHECK CONSTRAINT [FK_CompaniesCompanyType_CompanyTypeId]
GO



CREATE TABLE [dbo].[Rings] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [Number]        NVARCHAR (50)   NULL,
    [Decription]    NVARCHAR (150)  NULL,
    [Size]          NVARCHAR (10)   NULL,
    [Radius]        DECIMAL (18, 4) NULL,
    [Circumference] DECIMAL (18, 4) NULL,
	[Carat]			DECIMAL (18, 4) NULL,
	[Price]			DECIMAL (18, 4) NULL,
	[Weight]		DECIMAL (18, 4) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[ImageURL] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [ImageURL] NVARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[RingsImageURL] (
    [RingId]         INT NOT NULL,
    [ImageURLId] INT NOT NULL,
    CONSTRAINT [FK_RingsImageURL_Rings] FOREIGN KEY ([RingId]) REFERENCES [dbo].[Rings] ([Id]),
    CONSTRAINT [FK_RingsImageURL_ImageURL] FOREIGN KEY ([ImageURLId]) REFERENCES [dbo].[ImageURL] ([Id])
);

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EngagementRings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](50) NULL,
	[Decription] [nvarchar](150) NULL,
	[Size] [nvarchar](10) NULL,
	[Radius] [decimal](18, 4) NULL,
	[Circumference] [decimal](18, 4) NULL,
	[Carat] [decimal](18, 4) NULL,
	[Price] [decimal](18, 4) NULL,
	[Weight] [decimal](18, 4) NULL,
	[CompanyId] [int] NOT NULL,
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EngagementRings]  WITH CHECK ADD  CONSTRAINT [FK_EngagementRings_Companies] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([Id])
GO

ALTER TABLE [dbo].[EngagementRings] CHECK CONSTRAINT [FK_EngagementRings_Companies]
GO