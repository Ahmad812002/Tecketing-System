USE [master]
GO
/****** Object:  Database [ticketing]    Script Date: 9/23/2024 4:53:50 PM ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'ticketing')
BEGIN
CREATE DATABASE [ticketing]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ticketing', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ticketing.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ticketing_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ticketing_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
END
GO
ALTER DATABASE [ticketing] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ticketing].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ticketing] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ticketing] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ticketing] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ticketing] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ticketing] SET ARITHABORT OFF 
GO
ALTER DATABASE [ticketing] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ticketing] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ticketing] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ticketing] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ticketing] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ticketing] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ticketing] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ticketing] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ticketing] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ticketing] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ticketing] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ticketing] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ticketing] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ticketing] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ticketing] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ticketing] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ticketing] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ticketing] SET RECOVERY FULL 
GO
ALTER DATABASE [ticketing] SET  MULTI_USER 
GO
ALTER DATABASE [ticketing] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ticketing] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ticketing] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ticketing] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ticketing] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ticketing] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ticketing', N'ON'
GO
ALTER DATABASE [ticketing] SET QUERY_STORE = ON
GO
ALTER DATABASE [ticketing] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ticketing]
GO
/****** Object:  Table [dbo].[departments]    Script Date: 9/23/2024 4:53:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[departments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[departments](
	[departmentId] [int] NOT NULL,
	[name] [char](10) NOT NULL,
 CONSTRAINT [PK_departments] PRIMARY KEY CLUSTERED 
(
	[departmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tickets]    Script Date: 9/23/2024 4:53:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tickets]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tickets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[EmployeeId] [int] NULL,
	[Description] [text] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ResolvedAt] [datetime] NULL,
	[Title] [nvarchar](50) NOT NULL,
	[departmentId] [int] NULL,
 CONSTRAINT [PK__tickets__3214EC079983AD93] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[users]    Script Date: 9/23/2024 4:53:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[Username] [varchar](255) NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[UserType] [int] NOT NULL,
	[departmentId] [int] NULL,
 CONSTRAINT [PK__users__3214EC075ED1BB10] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[departments] ([departmentId], [name]) VALUES (1, N'IT        ')
INSERT [dbo].[departments] ([departmentId], [name]) VALUES (2, N'HR        ')
INSERT [dbo].[departments] ([departmentId], [name]) VALUES (3, N'PSS       ')
INSERT [dbo].[departments] ([departmentId], [name]) VALUES (4, N'Leader    ')
GO
SET IDENTITY_INSERT [dbo].[tickets] ON 

INSERT [dbo].[tickets] ([Id], [UserId], [EmployeeId], [Description], [Status], [CreatedAt], [ResolvedAt], [Title], [departmentId]) VALUES (1, 3, 4, N'asdfadsf', N'Solved', CAST(N'2023-05-15T08:30:00.000' AS DateTime), CAST(N'2024-08-23T17:45:00.000' AS DateTime), N'tyrty', 1)
INSERT [dbo].[tickets] ([Id], [UserId], [EmployeeId], [Description], [Status], [CreatedAt], [ResolvedAt], [Title], [departmentId]) VALUES (4, 12, 4, N'asfasdf', N'Solved', CAST(N'2024-07-12T09:09:12.000' AS DateTime), CAST(N'2024-10-11T12:04:00.000' AS DateTime), N'yilui', 4)
INSERT [dbo].[tickets] ([Id], [UserId], [EmployeeId], [Description], [Status], [CreatedAt], [ResolvedAt], [Title], [departmentId]) VALUES (5, 12, 4, N'asd', N'InProgress', CAST(N'2024-09-07T22:00:12.827' AS DateTime), NULL, N'eged', 3)
INSERT [dbo].[tickets] ([Id], [UserId], [EmployeeId], [Description], [Status], [CreatedAt], [ResolvedAt], [Title], [departmentId]) VALUES (6, 3, 4, N'asd', N'InProgress', CAST(N'2024-09-07T22:00:32.317' AS DateTime), NULL, N'eged', 1)
INSERT [dbo].[tickets] ([Id], [UserId], [EmployeeId], [Description], [Status], [CreatedAt], [ResolvedAt], [Title], [departmentId]) VALUES (7, 3, 4, N'i have a problem', N'InProgress', CAST(N'2024-09-07T22:00:32.330' AS DateTime), NULL, N'problem', 2)
INSERT [dbo].[tickets] ([Id], [UserId], [EmployeeId], [Description], [Status], [CreatedAt], [ResolvedAt], [Title], [departmentId]) VALUES (8, 3, 4, N'asd', N'Solved', CAST(N'2024-09-07T22:06:33.263' AS DateTime), CAST(N'2024-09-15T11:42:25.043' AS DateTime), N'eged', 2)
INSERT [dbo].[tickets] ([Id], [UserId], [EmployeeId], [Description], [Status], [CreatedAt], [ResolvedAt], [Title], [departmentId]) VALUES (9, 12, 4, N'asd', N'InProgress', CAST(N'2024-09-07T22:24:29.907' AS DateTime), NULL, N'eged', 3)
INSERT [dbo].[tickets] ([Id], [UserId], [EmployeeId], [Description], [Status], [CreatedAt], [ResolvedAt], [Title], [departmentId]) VALUES (10, 3, 4, N'asd', N'InProgress', CAST(N'2024-09-07T22:26:03.940' AS DateTime), NULL, N'eged', 4)
INSERT [dbo].[tickets] ([Id], [UserId], [EmployeeId], [Description], [Status], [CreatedAt], [ResolvedAt], [Title], [departmentId]) VALUES (11, 12, 4, N'asd', N'Solved', CAST(N'2024-09-07T22:26:20.570' AS DateTime), NULL, N'eged', 3)
INSERT [dbo].[tickets] ([Id], [UserId], [EmployeeId], [Description], [Status], [CreatedAt], [ResolvedAt], [Title], [departmentId]) VALUES (12, 3, NULL, N'asd', N'Solved', CAST(N'2024-09-07T22:26:57.493' AS DateTime), NULL, N'eged', 1)
INSERT [dbo].[tickets] ([Id], [UserId], [EmployeeId], [Description], [Status], [CreatedAt], [ResolvedAt], [Title], [departmentId]) VALUES (13, 12, NULL, N'asd', N'Solved', CAST(N'2024-09-07T22:27:35.567' AS DateTime), NULL, N'eged', 4)
INSERT [dbo].[tickets] ([Id], [UserId], [EmployeeId], [Description], [Status], [CreatedAt], [ResolvedAt], [Title], [departmentId]) VALUES (14, 3, NULL, N'there is a problem', N'Open', CAST(N'2024-09-07T22:56:12.183' AS DateTime), NULL, N'loay', 2)
INSERT [dbo].[tickets] ([Id], [UserId], [EmployeeId], [Description], [Status], [CreatedAt], [ResolvedAt], [Title], [departmentId]) VALUES (15, 12, 4, N'there is a problem', N'Solved', CAST(N'2024-09-07T22:56:42.203' AS DateTime), CAST(N'2024-09-15T11:41:58.383' AS DateTime), N'loay', 1)
INSERT [dbo].[tickets] ([Id], [UserId], [EmployeeId], [Description], [Status], [CreatedAt], [ResolvedAt], [Title], [departmentId]) VALUES (16, 3, NULL, N'gfhfsdfga', N'Open', CAST(N'2024-09-07T22:56:52.930' AS DateTime), NULL, N'sdfsafd', 4)
INSERT [dbo].[tickets] ([Id], [UserId], [EmployeeId], [Description], [Status], [CreatedAt], [ResolvedAt], [Title], [departmentId]) VALUES (17, 3, NULL, N'rt54hgfvs', N'Open', CAST(N'2024-09-08T14:20:36.793' AS DateTime), NULL, N'vcxbkg', 3)
INSERT [dbo].[tickets] ([Id], [UserId], [EmployeeId], [Description], [Status], [CreatedAt], [ResolvedAt], [Title], [departmentId]) VALUES (18, 12, NULL, N'agdhrjtter', N'Open', CAST(N'2024-09-08T14:22:44.067' AS DateTime), NULL, N'dfasfdqthcxz', 2)
INSERT [dbo].[tickets] ([Id], [UserId], [EmployeeId], [Description], [Status], [CreatedAt], [ResolvedAt], [Title], [departmentId]) VALUES (20, 12, NULL, N'sdaf', N'Open', CAST(N'2024-09-08T15:31:27.680' AS DateTime), NULL, N'Asd', 3)
INSERT [dbo].[tickets] ([Id], [UserId], [EmployeeId], [Description], [Status], [CreatedAt], [ResolvedAt], [Title], [departmentId]) VALUES (21, 12, NULL, N';sadfj', N'Open', CAST(N'2024-09-08T15:32:27.337' AS DateTime), NULL, N'Las', 1)
INSERT [dbo].[tickets] ([Id], [UserId], [EmployeeId], [Description], [Status], [CreatedAt], [ResolvedAt], [Title], [departmentId]) VALUES (25, 3, 4, N'Qusay', N'InProgress', CAST(N'2024-09-15T14:27:16.387' AS DateTime), NULL, N'Qusay', 1)
INSERT [dbo].[tickets] ([Id], [UserId], [EmployeeId], [Description], [Status], [CreatedAt], [ResolvedAt], [Title], [departmentId]) VALUES (26, 3, NULL, N';okoiy8fyrdyxtc', N'Open', CAST(N'2024-09-17T13:29:16.317' AS DateTime), NULL, N'jfeyuihiit', 3)
INSERT [dbo].[tickets] ([Id], [UserId], [EmployeeId], [Description], [Status], [CreatedAt], [ResolvedAt], [Title], [departmentId]) VALUES (27, 3, NULL, N'dsagdahfds', N'Open', CAST(N'2024-09-23T15:55:14.060' AS DateTime), NULL, N'asdfsdf', 4)
SET IDENTITY_INSERT [dbo].[tickets] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([Id], [Email], [Username], [Password], [UserType], [departmentId]) VALUES (2, N'Admin@gmail.com', N'Admin', N'Admin', 0, 4)
INSERT [dbo].[users] ([Id], [Email], [Username], [Password], [UserType], [departmentId]) VALUES (3, N'Regular@gmail.com', N'Regular', N'Regular', 1, 3)
INSERT [dbo].[users] ([Id], [Email], [Username], [Password], [UserType], [departmentId]) VALUES (4, N'Employee@gmail.com', N'Employee', N'Employee', 2, 1)
INSERT [dbo].[users] ([Id], [Email], [Username], [Password], [UserType], [departmentId]) VALUES (9, N'Jordan@gmail.com', N'JOR', N'JOrdan@812002', 0, 4)
INSERT [dbo].[users] ([Id], [Email], [Username], [Password], [UserType], [departmentId]) VALUES (12, N'Hassan@gmail.com', N'Hassan', N'Hassan@812002', 1, 3)
INSERT [dbo].[users] ([Id], [Email], [Username], [Password], [UserType], [departmentId]) VALUES (14, N'Salsabiel@gmail.com', N'Salsabiel', N'Salsabiel@1234', 0, 4)
INSERT [dbo].[users] ([Id], [Email], [Username], [Password], [UserType], [departmentId]) VALUES (20, N'Mousa@gmail.com', N'mousa', N'Mousa@812002', 0, 4)
INSERT [dbo].[users] ([Id], [Email], [Username], [Password], [UserType], [departmentId]) VALUES (38, N'm@gmail.com', N'm', N'Manager@812002', 3, 4)
INSERT [dbo].[users] ([Id], [Email], [Username], [Password], [UserType], [departmentId]) VALUES (39, N'ahmad@gmial.com', N'ahmad', N'Ahmad@812002', 2, 1)
INSERT [dbo].[users] ([Id], [Email], [Username], [Password], [UserType], [departmentId]) VALUES (40, N'Sausan@gmail.com', N'sausan', N'Sausan@812002', 1, 3)
SET IDENTITY_INSERT [dbo].[users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__users__A9D10534E8F7DD1A]    Script Date: 9/23/2024 4:53:51 PM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[users]') AND name = N'UQ__users__A9D10534E8F7DD1A')
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [UQ__users__A9D10534E8F7DD1A] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__tickets__Created__440B1D61]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tickets] ADD  CONSTRAINT [DF__tickets__Created__440B1D61]  DEFAULT (getdate()) FOR [CreatedAt]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tickets_departments]') AND parent_object_id = OBJECT_ID(N'[dbo].[tickets]'))
ALTER TABLE [dbo].[tickets]  WITH CHECK ADD  CONSTRAINT [FK_tickets_departments] FOREIGN KEY([departmentId])
REFERENCES [dbo].[departments] ([departmentId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tickets_departments]') AND parent_object_id = OBJECT_ID(N'[dbo].[tickets]'))
ALTER TABLE [dbo].[tickets] CHECK CONSTRAINT [FK_tickets_departments]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_users_departments]') AND parent_object_id = OBJECT_ID(N'[dbo].[users]'))
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_departments] FOREIGN KEY([departmentId])
REFERENCES [dbo].[departments] ([departmentId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_users_departments]') AND parent_object_id = OBJECT_ID(N'[dbo].[users]'))
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_departments]
GO
USE [master]
GO
ALTER DATABASE [ticketing] SET  READ_WRITE 
GO
