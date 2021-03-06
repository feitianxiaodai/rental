USE [master]
GO
/****** Object:  Database [Rental]    Script Date: 07/17/2015 00:39:25 ******/
CREATE DATABASE [Rental] ON  PRIMARY 
( NAME = N'Rental', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\Rental.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Rental_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\Rental_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Rental] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Rental].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Rental] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [Rental] SET ANSI_NULLS OFF
GO
ALTER DATABASE [Rental] SET ANSI_PADDING OFF
GO
ALTER DATABASE [Rental] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [Rental] SET ARITHABORT OFF
GO
ALTER DATABASE [Rental] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [Rental] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [Rental] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [Rental] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [Rental] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [Rental] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [Rental] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [Rental] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [Rental] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [Rental] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [Rental] SET  DISABLE_BROKER
GO
ALTER DATABASE [Rental] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [Rental] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [Rental] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [Rental] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [Rental] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [Rental] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [Rental] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [Rental] SET  READ_WRITE
GO
ALTER DATABASE [Rental] SET RECOVERY FULL
GO
ALTER DATABASE [Rental] SET  MULTI_USER
GO
ALTER DATABASE [Rental] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [Rental] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'Rental', N'ON'
GO
USE [Rental]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 07/17/2015 00:39:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Room](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoomType] [int] NOT NULL,
	[IsBasement] [int] NOT NULL,
	[RoomSpace] [float] NOT NULL,
	[RoomShi] [int] NOT NULL,
	[RoomTing] [int] NULL,
	[RoomKitchen] [int] NULL,
	[RoomBalcony] [int] NULL,
	[PrivateBathroom] [int] NULL,
	[PublicBathroom] [int] NULL,
	[RoomCount] [int] NOT NULL,
	[BedType1] [int] NOT NULL,
	[BedType2] [int] NULL,
	[BedType3] [int] NULL,
	[BedType4] [int] NULL,
	[Fridge] [bit] NULL,
	[WashingMechine] [bit] NULL,
	[Water] [bit] NULL,
	[Intercom] [bit] NULL,
	[Computer] [bit] NULL,
	[Shower] [bit] NULL,
	[Bathtub] [bit] NULL,
	[Toiletries] [bit] NULL,
	[Towel] [bit] NULL,
	[Slippers] [bit] NULL,
	[HotWater] [bit] NULL,
	[Elevator] [bit] NULL,
	[Police] [bit] NULL,
	[Child] [bit] NULL,
	[Older] [bit] NULL,
	[AirportTransfer] [bit] NULL,
	[LeftLuggage] [bit] NULL,
	[Guide] [bit] NULL,
	[RoomNumber] [int] NOT NULL,
	[RoomCity] [int] NOT NULL,
	[RoomArea] [int] NOT NULL,
	[RoomDetailedAddress] [varchar](max) NOT NULL,
	[RoomLang] [varchar](64) NOT NULL,
	[RoomLong] [varchar](64) NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Area]    Script Date: 07/17/2015 00:39:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Area](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AreaName] [varchar](256) NOT NULL,
	[AreaParent] [int] NOT NULL,
	[AreaLong] [varchar](256) NOT NULL,
	[AreaLang] [varchar](256) NOT NULL,
 CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Area] ON
INSERT [dbo].[Area] ([Id], [AreaName], [AreaParent], [AreaLong], [AreaLang]) VALUES (1, N'新北市', 0, N'121.511073', N'25.063134')
INSERT [dbo].[Area] ([Id], [AreaName], [AreaParent], [AreaLong], [AreaLang]) VALUES (3, N'高雄市', 0, N'120.313562', N'22.622139 ')
INSERT [dbo].[Area] ([Id], [AreaName], [AreaParent], [AreaLong], [AreaLang]) VALUES (4, N'北投区', 1, N'121.511157 ', N'25.138666 ')
INSERT [dbo].[Area] ([Id], [AreaName], [AreaParent], [AreaLong], [AreaLang]) VALUES (5, N'万里区', 1, N'121.696855', N'25.186806')
INSERT [dbo].[Area] ([Id], [AreaName], [AreaParent], [AreaLong], [AreaLang]) VALUES (6, N'三民区', 3, N'120.311883', N'22.650656')
INSERT [dbo].[Area] ([Id], [AreaName], [AreaParent], [AreaLong], [AreaLang]) VALUES (7, N'鼓山区', 3, N'120.292336', N'22.640516')
SET IDENTITY_INSERT [dbo].[Area] OFF
/****** Object:  Table [dbo].[Slider]    Script Date: 07/17/2015 00:39:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Slider](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TitleCN] [varchar](128) NOT NULL,
	[ImgUrl] [varchar](256) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Remark] [varchar](256) NULL,
	[TitleTW] [varchar](128) NULL,
	[TitleEN] [varchar](128) NULL,
 CONSTRAINT [PK_Slider] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Slider] ON
INSERT [dbo].[Slider] ([Id], [TitleCN], [ImgUrl], [CreateTime], [Remark], [TitleTW], [TitleEN]) VALUES (6, N'香港大酒店', N'635726829301496894.jpg', CAST(0x0000A4D701744F19 AS DateTime), NULL, N'香港繁体', N'hongkong hotel')
SET IDENTITY_INSERT [dbo].[Slider] OFF
/****** Object:  Table [dbo].[RoomImageInfo]    Script Date: 07/17/2015 00:39:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RoomImageInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ImgUrl] [varchar](128) NOT NULL,
	[RoomId] [int] NOT NULL,
 CONSTRAINT [PK_RoomImageInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_RoomImageInfo_Room]    Script Date: 07/17/2015 00:39:26 ******/
ALTER TABLE [dbo].[RoomImageInfo]  WITH CHECK ADD  CONSTRAINT [FK_RoomImageInfo_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
ALTER TABLE [dbo].[RoomImageInfo] CHECK CONSTRAINT [FK_RoomImageInfo_Room]
GO
