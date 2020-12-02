# PumoxGmbh
Repozytorium zawierajacego rozwiązanie zadania rekrutacyjnego dla firmy PumoxGmbh

connection string w aplikacji znajduje się w klasie Startup

Aplikacja nasłuchuje na porcie 5000

autoryzacja odbywa się przy pomocy JWT

by uzyskać BearerToken należy wywołać http://localhost:5000/users/Login i w body(json) podać
{
    "username": "test",
    "password": "test"
}

skyrpt sql do utworzenia bazy danych

USE [RecruitTaskDatabase]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 02.12.2020 00:18:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[EstablishmentYear] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employer]    Script Date: 02.12.2020 00:18:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](81) NOT NULL,
	[LastName] [nvarchar](255) NOT NULL,
	[DateOfBirth] [datetime2](7) NOT NULL,
	[JobTitle] [nvarchar](255) NULL,
	[CompanyId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employer]  WITH CHECK ADD  CONSTRAINT [FK_676F7E46] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([Id])
GO
ALTER TABLE [dbo].[Employer] CHECK CONSTRAINT [FK_676F7E46]
GO