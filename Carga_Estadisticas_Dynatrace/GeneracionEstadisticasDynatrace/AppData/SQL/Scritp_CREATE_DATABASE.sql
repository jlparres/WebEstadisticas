-- CREAR BASE DE DATOS DYNATRACE
CREATE DATABASE [DATA_DYNA]
GO

USE [DATA_DYNA]
GO

/****** Object:  Table [dbo].[WebRequestsDynatraceMes]    Script Date: 12/10/2018 18:30:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WebRequestsDynatraceMes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[URI] [nchar](150) NOT NULL,
	[Canal] [nchar](30) NOT NULL,
	[TasaFallo] [real] NOT NULL,
	[Numero] [real] NOT NULL,
	[Anyo] [int] NOT NULL,
	[Mes] [int] NOT NULL,
	[NumDiasActividad] [int] NOT NULL,
 CONSTRAINT [PK_WebRequestsDynatraceMes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [DATA_DYNA]
GO

/****** Object:  Table [dbo].[DatosDynatraceMes]    Script Date: 12/10/2018 18:27:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DatosDynatraceMes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Metrica] [nchar](50) NOT NULL,
	[Canal] [nchar](30) NOT NULL,
	[Excepciones] [real] NOT NULL,
	[Promedio] [real] NOT NULL,
	[Percentil95] [real] NOT NULL,
	[NumPromedio] [real] NOT NULL,
	[NumPercentil] [real] NOT NULL,
	[Anyo] [int] NOT NULL,
	[Mes] [int] NOT NULL,
	[NumDiasActividad] [int] NOT NULL,
 CONSTRAINT [PK_DatosDynatraceMes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [DATA_DYNA]
GO

/****** Object:  Table [dbo].[WebRequestsDynatraceSemana]    Script Date: 12/10/2018 18:32:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WebRequestsDynatraceSemana](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[URI] [nchar](150) NOT NULL,
	[Canal] [nchar](30) NOT NULL,
	[TasaFallo] [real] NOT NULL,
	[Numero] [real] NOT NULL,
	[Semana] [int] NOT NULL,
	[Anyo] [int] NOT NULL,
	[NumDiasActividad] [int] NOT NULL,
 CONSTRAINT [PK_WebRequestsDynatraceSem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [DATA_DYNA]
GO

/****** Object:  Table [dbo].[DatosDynatrace]    Script Date: 12/10/2018 18:28:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DatosDynatrace](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Metrica] [nchar](50) NOT NULL,
	[Canal] [nchar](30) NOT NULL,
	[Excepciones] [int] NOT NULL,
	[Promedio] [real] NOT NULL,
	[Percentil95] [real] NOT NULL,
	[NumPromedio] [int] NOT NULL,
	[NumPercentil] [int] NOT NULL,
	[Fecha_dato] [datetime] NOT NULL,
 CONSTRAINT [PK_DatosDynatrace] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [DATA_DYNA]
GO

/****** Object:  Table [dbo].[DatosDynatraceSemana]    Script Date: 12/10/2018 18:28:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DatosDynatraceSemana](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Metrica] [nchar](50) NOT NULL,
	[Canal] [nchar](30) NOT NULL,
	[Excepciones] [real] NOT NULL,
	[Promedio] [real] NOT NULL,
	[Percentil95] [real] NOT NULL,
	[NumPromedio] [real] NOT NULL,
	[NumPercentil] [real] NOT NULL,
	[NumDiasActividad] [int] NOT NULL,
	[Semana] [int] NOT NULL,
	[Anyo] [int] NOT NULL,
 CONSTRAINT [PK_DatosDynatraceSem] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [DATA_DYNA]
GO

/****** Object:  Table [dbo].[WebRequestsDynatrace]    Script Date: 12/10/2018 18:28:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WebRequestsDynatrace](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[URI] [nchar](150) NOT NULL,
	[Canal] [nchar](30) NOT NULL,
	[TasaFallo] [real] NOT NULL,
	[Numero] [int] NOT NULL,
	[Fecha_dato] [datetime] NOT NULL,
 CONSTRAINT [PK_WebRequestsDynatrace] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [DATA_DYNA]
GO

/****** Object:  Table [dbo].[WS-END]    Script Date: 12/10/2018 18:32:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WS-END](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[WS_ID] [nchar](10) NOT NULL,
	[WS_NOMBRE] [nchar](100) NOT NULL,
	[ENDPOINT] [nchar](100) NOT NULL,
	[ACCION] [nchar](50) NULL,
	[ESTADO] [nchar](50) NULL,
 CONSTRAINT [PK_WS-END] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO