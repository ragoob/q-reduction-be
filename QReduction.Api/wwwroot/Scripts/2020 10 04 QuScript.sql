USE [qreductiondb]
GO
/****** Object:  User [qr_sa]    Script Date: 10/4/2020 11:43:05 PM ******/
CREATE USER [qr_sa] FOR LOGIN [qr_sa] WITH DEFAULT_SCHEMA=[qr_sa]
GO
/****** Object:  User [selshahat]    Script Date: 10/4/2020 11:43:05 PM ******/
CREATE USER [selshahat] FOR LOGIN [selshahat] WITH DEFAULT_SCHEMA=[selshahat]
GO
ALTER ROLE [db_owner] ADD MEMBER [qr_sa]
GO
ALTER ROLE [db_owner] ADD MEMBER [selshahat]
GO
/****** Object:  Table [Acl].[PagePermissions]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Acl].[PagePermissions](
	[Id] [int] NOT NULL,
	[PermissionTermId] [int] NOT NULL,
	[PageId] [int] NOT NULL,
	[ACLName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PagePermissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Acl].[Pages]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Acl].[Pages](
	[Id] [int] NOT NULL,
	[NameAr] [nvarchar](50) NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Acl].[PermissionsTerms]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Acl].[PermissionsTerms](
	[Id] [int] NOT NULL,
	[NameAr] [nvarchar](50) NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PermissionsTerms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Acl].[RolePagePermissions]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Acl].[RolePagePermissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PagePermissionId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_RolePagePermissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Acl].[Roles]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Acl].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NameAr] [nvarchar](50) NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
	[ReadOnly] [bit] NOT NULL,
	[Code] [int] NOT NULL,
	[CreateBy] [int] NULL,
	[CreateAt] [datetime2](7) NULL,
	[UpdateBy] [int] NULL,
	[UpdateAt] [datetime2](7) NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedAt] [datetime2](7) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Acl].[UserRoles]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Acl].[UserRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Acl].[Users]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Acl].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserGuid] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](225) NULL,
	[UserName] [nvarchar](255) NULL,
	[LastLoginUtcDate] [datetime2](7) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[Password] [varbinary](max) NULL,
	[PasswordSalt] [varbinary](max) NULL,
	[PhotoPath] [nvarchar](max) NULL,
	[ForgotPasswordCode] [nvarchar](6) NULL,
	[ForgotPasswordExpiration] [datetime2](7) NULL,
	[LastUpdateDate] [datetime2](7) NULL,
	[RowGuid] [uniqueidentifier] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedAt] [datetime2](7) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UserTypeId] [int] NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[VerificationCode] [nvarchar](4) NULL,
	[IsVerified] [bit] NOT NULL,
	[VerificationCodeExpiration] [datetime2](7) NULL,
	[OrganizationId] [int] NULL,
	[UserDeviceId] [nvarchar](max) NULL,
	[BranchId] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Acl].[UserTypes]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Acl].[UserTypes](
	[Id] [int] NOT NULL,
	[NameAr] [nvarchar](225) NULL,
	[NameEn] [nvarchar](255) NULL,
 CONSTRAINT [PK_UserTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [qr_sa].[__EFMigrationsHistory]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [qr_sa].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [qr_sa].[About]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [qr_sa].[About](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LabelValueAr] [nvarchar](max) NULL,
	[LabelValueEn] [nvarchar](max) NULL,
	[CreateAt] [datetime2](7) NOT NULL,
	[CreateBy] [int] NULL,
	[DeletedAt] [datetime2](7) NULL,
	[DeletedBy] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdateAt] [datetime2](7) NULL,
	[UpdateBy] [int] NULL,
	[LabelTextAr] [nvarchar](max) NULL,
	[LabelTextEn] [nvarchar](max) NULL,
 CONSTRAINT [PK_About] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [qr_sa].[Branchs]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [qr_sa].[Branchs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [int] NOT NULL,
	[OrganizationId] [int] NULL,
	[QrCode] [nvarchar](max) NULL,
	[NameAr] [nvarchar](50) NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[BranchAddress] [nvarchar](500) NULL,
	[Note] [nvarchar](500) NULL,
	[CreateBy] [int] NULL,
	[CreateAt] [smalldatetime] NOT NULL,
	[UpdateBy] [int] NULL,
	[UpdateAt] [smalldatetime] NULL,
	[DeletedAt] [smalldatetime] NULL,
	[DeletedBy] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Branchs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [qr_sa].[BranchServices]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [qr_sa].[BranchServices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BranchId] [int] NOT NULL,
	[ServiceId] [int] NOT NULL,
 CONSTRAINT [PK_BranchServices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [qr_sa].[EvaluationQuestionAnswer]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [qr_sa].[EvaluationQuestionAnswer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EvaluationId] [int] NULL,
	[QuestionId] [int] NULL,
	[AnswerValue] [int] NULL,
 CONSTRAINT [PK_EvaluationQuestionAnswer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [qr_sa].[Evaluations]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [qr_sa].[Evaluations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Comment] [nvarchar](500) NULL,
	[ShiftQueueId] [int] NOT NULL,
	[CreateAt] [smalldatetime] NOT NULL,
	[DeletedAt] [smalldatetime] NULL,
	[DeletedBy] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Evaluations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [qr_sa].[HelpAndSupport]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [qr_sa].[HelpAndSupport](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MessageTitle] [nvarchar](max) NULL,
	[Message] [nvarchar](max) NULL,
	[UserId] [int] NULL,
	[CreateAt] [datetime2](7) NOT NULL,
	[CreateBy] [int] NULL,
	[DeletedAt] [datetime2](7) NULL,
	[DeletedBy] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdateAt] [datetime2](7) NULL,
	[UpdateBy] [int] NULL,
 CONSTRAINT [PK_HelpAndSupport] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [qr_sa].[Instructions]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [qr_sa].[Instructions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [int] NOT NULL,
	[OrganizationId] [int] NULL,
	[NameAr] [nvarchar](50) NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
	[CreateBy] [int] NULL,
	[CreateAt] [smalldatetime] NOT NULL,
	[UpdateBy] [int] NULL,
	[UpdateAt] [smalldatetime] NULL,
	[DeletedAt] [smalldatetime] NULL,
	[DeletedBy] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Instructions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [qr_sa].[LoginProviders]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [qr_sa].[LoginProviders](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ProviderType] [int] NOT NULL,
	[Providertoken] [nvarchar](max) NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_LoginProviders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [qr_sa].[Organization]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [qr_sa].[Organization](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [int] NOT NULL,
	[NameAr] [nvarchar](50) NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[Note] [nvarchar](500) NULL,
	[CreateBy] [int] NULL,
	[CreateAt] [smalldatetime] NOT NULL,
	[UpdateBy] [int] NULL,
	[UpdateAt] [smalldatetime] NULL,
	[DeletedAt] [smalldatetime] NULL,
	[DeletedBy] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Organization] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [qr_sa].[Questions]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [qr_sa].[Questions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuestionTextAr] [nvarchar](250) NOT NULL,
	[QuestionTextEn] [nvarchar](250) NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[Code] [int] NOT NULL,
	[CreateBy] [int] NULL,
	[CreateAt] [smalldatetime] NOT NULL,
	[DeletedAt] [smalldatetime] NULL,
	[DeletedBy] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdateBy] [int] NULL,
	[UpdateAt] [smalldatetime] NULL,
 CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [qr_sa].[Services]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [qr_sa].[Services](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [int] NOT NULL,
	[NameAr] [nvarchar](50) NOT NULL,
	[NameEn] [nvarchar](50) NOT NULL,
	[Note] [nvarchar](505) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateBy] [int] NULL,
	[CreateAt] [smalldatetime] NOT NULL,
	[UpdateBy] [int] NULL,
	[UpdateAt] [smalldatetime] NULL,
	[DeletedAt] [smalldatetime] NULL,
	[DeletedBy] [int] NULL,
	[OrganizationId] [int] NOT NULL,
	[NoteEN] [nvarchar](max) NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [qr_sa].[ShiftQueues]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [qr_sa].[ShiftQueues](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserTurn] [int] NOT NULL,
	[ShiftId] [int] NOT NULL,
	[IsServiceDone] [bit] NOT NULL,
	[UserIdMobile] [int] NOT NULL,
	[UserIdBy] [int] NULL,
	[ServiceId] [int] NOT NULL,
	[WindowNumber] [nvarchar](50) NULL,
	[PushId] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ShiftQueues] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [qr_sa].[Shifts]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [qr_sa].[Shifts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [int] NOT NULL,
	[StartAt] [smalldatetime] NOT NULL,
	[EndAt] [smalldatetime] NULL,
	[BranchId] [int] NOT NULL,
	[IsEnded] [bit] NOT NULL,
	[QRCode] [nvarchar](max) NULL,
	[UserIdSupport] [int] NULL,
	[UpdateAt] [smalldatetime] NULL,
	[CreateBy] [int] NULL,
	[CreateAt] [smalldatetime] NOT NULL,
	[UpdateBy] [int] NULL,
 CONSTRAINT [PK_Shifts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [qr_sa].[ShiftUsers]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [qr_sa].[ShiftUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ShiftId] [int] NOT NULL,
	[WindowNumber] [nvarchar](50) NULL,
	[ServiceId] [int] NULL,
 CONSTRAINT [PK_ShiftUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Settings].[SystemGridColumns]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Settings].[SystemGridColumns](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[VisibilityDefault] [bit] NOT NULL,
	[SystemGridId] [int] NOT NULL,
 CONSTRAINT [PK_SystemGridColumns] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Settings].[SystemGrids]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Settings].[SystemGrids](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SystemGrids] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Settings].[UserGridColumns]    Script Date: 10/4/2020 11:43:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Settings].[UserGridColumns](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsVisible] [bit] NOT NULL,
	[UserId] [int] NOT NULL,
	[SystemGridColumnId] [int] NOT NULL,
 CONSTRAINT [PK_UserGridColumns] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [Acl].[Roles] ON 
GO
INSERT [Acl].[Roles] ([Id], [NameAr], [NameEn], [ReadOnly], [Code], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [IsDeleted], [DeletedBy], [DeletedAt]) VALUES (1, N'Mobile', N'MobileUser', 0, 1, 11, CAST(N'2020-08-04T00:00:00.0000000' AS DateTime2), NULL, NULL, 0, NULL, NULL)
GO
SET IDENTITY_INSERT [Acl].[Roles] OFF
GO
SET IDENTITY_INSERT [Acl].[UserRoles] ON 
GO
INSERT [Acl].[UserRoles] ([Id], [RoleId], [UserId]) VALUES (1, 1, 15)
GO
INSERT [Acl].[UserRoles] ([Id], [RoleId], [UserId]) VALUES (2, 1, 14)
GO
SET IDENTITY_INSERT [Acl].[UserRoles] OFF
GO
SET IDENTITY_INSERT [Acl].[Users] ON 
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (11, N'dca82236-77df-4ed6-8655-9ccb7b48de76', N'selshahat@gmail.com', N'selshahat@gmail.com', CAST(N'2020-08-09T23:05:30.1644658' AS DateTime2), N'010563219431', 1, 0x3FAB1547E836059D2063F79C33A614349DF4823E4CE5AF036ADA3AB4812DA59E0A3E9E6685EB40B7867E29A02CA0BE660CDC7ECC18A9C185177F2C0C2C8B4C23, 0x99B7B01DA4142B01120D29E2BFB766BAF400A33CD5E259D76D83305A0325E085CC7B730719A2BF73A35235FAD608DBCC442B11D1B7509DCD174BDFD8595C9199C9D22ED1981B6B310C42E6AC55F9F8A4703954DC0EC349CF7D79B8E9CE0449302EBF7868D2806980DCC8B0FB4FCC8206C895087BBBDDFF4BB5262C7A14ECF68A, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (12, N'eab99859-4e34-43d9-8b85-23a5ea706e48', N'selshahat@hotmail.com', N'Super Admin', CAST(N'2020-10-01T21:25:30.2682673' AS DateTime2), N'010171846219431', 1, 0x65C9579EC57F5670ECFA38055FD14AA70C17A1AD1E81C861D857C4B5EF01746DD0F347F03DD6F14979D7324597F23E10B2AF294C396372066F0389C67588DE76, 0x3227ECBEFAAF44C68862E45A6B76C1D1B97E2144D42363A53AA448616C085B04081AEC130C52B44C4918399B53CBDCD2AC06C85F73ED9031FE6B77A7E02ECB966C9CA63F24993D1CD70CD1674D1006750A896D504BF4D943CD253DA4E54797F60AECD122F8F75C878CB9FF8BE3A0E34C5491E2A8E46C894049AF6380BD1545BF, N'UserUploads/eab99859-4e34-43d9-8b85-23a5ea706e48download (1).jpg', NULL, NULL, CAST(N'2020-08-19T20:31:40.2830470' AS DateTime2), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 12, CAST(N'2020-08-19T20:31:40.2830470' AS DateTime2), 1, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (14, N'2649946c-d963-4543-9255-ca2f5c0742f4', N'selshahat@arcom.com.sa', N'sherifIPhone', CAST(N'2020-08-08T20:16:37.4022074' AS DateTime2), N'966563219431', 1, 0xD31EB0BB8F3224A874C72414B217DCA22A933CE8E5DEA5DBB12B061E10256DC0AD9E7D1876D7D21FF7A6FD49FDE2530C1A7598A4202EFED2BE795E7BDA174C38, 0x65890766A1B06158267492FAD8605D6AB2F46E0100D8539E63DDF2C18784A159ADC42DD3B07881324AA5933DE4CF582C73B2EF8E6361147CFBA3AEC3EACC54810943B1185AEC0EF156834C0AAA2A97C1861C9624B50331B3E71AC4BDAF5A5937152A3193F431A2A6585FD28EB5D3508AAC030B2C9822132D7AE3BDE0C2EF5579, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 5, NULL, NULL, N'6762', 1, CAST(N'2020-08-06T01:05:09.3567653' AS DateTime2), NULL, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (15, N'71535da4-c4fd-45df-8aab-8b711275432d', N'm@h.com', N'Mario Hany', CAST(N'2020-10-02T10:24:57.3349823' AS DateTime2), N'123123456', 1, 0x9F85C2A5F8FF16CFA8CD18429CD0BC234F1F4288A1D1FFD1247CBEF2C5526D47F10E00FA4E6C54CB79151AF6B64B94693A88E7CDC7BBBF740A0C998CAFA49EA1, 0x63118D878068A14985B810F516D888742F5AF12CC2FE98521262D4E51530A320C4944CE8D46B61BE97BE354AD3853859DC40606CC6EBED1EB7121C4AB875779E765556D10B20A00998062C2DEBAC454B1D9C01F6B8279808AC1C476D5F27286CAF12935D45C816724C9924599A247D01F3385188B4066BBF5F7F7FB53C3D6B31, N'UserUploads/71535da4-c4fd-45df-8aab-8b711275432dIMG_20180524_130637.jpg', N'251231', CAST(N'2020-09-20T20:27:11.5506478' AS DateTime2), CAST(N'2020-08-26T10:06:28.1463021' AS DateTime2), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 15, CAST(N'2020-08-26T10:06:28.1463021' AS DateTime2), 5, NULL, NULL, NULL, 1, NULL, NULL, N'djGRNPd2T9qWJfMQR5uPXu:APA91bF_xdOtpfJWZZjjKjviI0qMO9tLA-yxz_IYHEzqOKJ-56_QjfCXYiQaqcNugJcA5CJf0vcmT9KWbqhNN2vfZIQ_aSBpQfjXnYAg9lqaGXYQ3j62xQbBA-TXJ7qVVIdPGWj1DJON', NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (16, N'dca82236-88df-4ed6-8655-9ccb7b48de76', N'salsayed.eo@dd.cc', N'alsayed Alex Admin', CAST(N'2020-08-05T23:20:01.9222260' AS DateTime2), N'010563219431', 1, NULL, NULL, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 2, NULL, NULL, NULL, 1, NULL, 5, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (17, N'dca82236-88df-4ed6-8655-9ccb7b48de76', N'alexSuper@dd.cc', N'alexSupervisor', CAST(N'2020-08-14T19:11:00.2143894' AS DateTime2), N'010563219431', 1, 0x14A5359DEB7FE3DDCCF2AA95191A5DFC7253F0A5DE86F46CFE96CC35227C34647592E05CB3A8BDE5910DC621D10BF0E9E3DFDDBE84E776B0EBF946BE709172A7, 0xA4352F57134C3FA19AAA4CE54A7C08955160756E8FAEB3E77F51190143F4AD05CA3BC54D4F5FEE15CC23B64CF4FA375379D630BAF2F6BFCEFFA5A5EAF04F645091A133670B0E6357A45DC5F914B6F6FE90ACE60FEFA3C875E64E4FCAB88A9411DECAA8DE5B4968039F5025C3CC8E4CAC0961A9BBBBA8B0B0EE03625F4FFCBCD6, NULL, NULL, NULL, CAST(N'2020-08-15T13:49:30.2200500' AS DateTime2), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 17, NULL, 3, NULL, NULL, NULL, 1, NULL, 5, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (18, N'dca82236-88df-4ed6-8655-9ccb7b48de76', N'alexTailor@dd.cc', N'alexTailor', CAST(N'2020-08-13T19:39:36.5329468' AS DateTime2), N'010563219431', 1, 0xD31EB0BB8F3224A874C72414B217DCA22A933CE8E5DEA5DBB12B061E10256DC0AD9E7D1876D7D21FF7A6FD49FDE2530C1A7598A4202EFED2BE795E7BDA174C38, 0x65890766A1B06158267492FAD8605D6AB2F46E0100D8539E63DDF2C18784A159ADC42DD3B07881324AA5933DE4CF582C73B2EF8E6361147CFBA3AEC3EACC54810943B1185AEC0EF156834C0AAA2A97C1861C9624B50331B3E71AC4BDAF5A5937152A3193F431A2A6585FD28EB5D3508AAC030B2C9822132D7AE3BDE0C2EF5579, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 4, NULL, NULL, NULL, 1, NULL, 5, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (19, N'dca82236-88df-4ed6-8655-9ccb7b48de76', N'alexTailor2@dd.cc', N'alexTailor2', CAST(N'2020-08-05T23:20:01.9222260' AS DateTime2), N'010563219431', 1, NULL, NULL, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 4, NULL, NULL, NULL, 1, NULL, 5, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (20, N'dca82236-88df-4ed6-8655-9ccb7b48de76', N'alexTailor3@dd.cc', N'alexTailor2', CAST(N'2020-08-05T23:20:01.9222260' AS DateTime2), N'010563219431', 1, NULL, NULL, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 4, NULL, NULL, NULL, 1, NULL, 5, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (22, N'dca82236-88df-4ed6-8655-9ccb7b48de76', N'alexMob1@dd.cc', N'alexMob1', CAST(N'2020-08-15T18:36:52.1174584' AS DateTime2), N'010563219431', 1, 0xD31EB0BB8F3224A874C72414B217DCA22A933CE8E5DEA5DBB12B061E10256DC0AD9E7D1876D7D21FF7A6FD49FDE2530C1A7598A4202EFED2BE795E7BDA174C38, 0x65890766A1B06158267492FAD8605D6AB2F46E0100D8539E63DDF2C18784A159ADC42DD3B07881324AA5933DE4CF582C73B2EF8E6361147CFBA3AEC3EACC54810943B1185AEC0EF156834C0AAA2A97C1861C9624B50331B3E71AC4BDAF5A5937152A3193F431A2A6585FD28EB5D3508AAC030B2C9822132D7AE3BDE0C2EF5579, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 5, NULL, NULL, NULL, 1, NULL, 5, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (23, N'dca82236-88df-4ed6-8655-9ccb7b48de76', N'alexMob152@dd.cc', N'RTRT', CAST(N'2020-08-08T20:22:25.4914413' AS DateTime2), N'255255', 0, NULL, NULL, N'D:\seham\QReduction\QReduction.Api\wwwroot\UserUploads\dca82236-88df-4ed6-8655-9ccb7b48de76dz_cropped.png', NULL, NULL, CAST(N'2020-08-14T15:52:35.8935156' AS DateTime2), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 0, CAST(N'2020-08-14T15:52:35.8935156' AS DateTime2), 5, NULL, NULL, NULL, 1, NULL, 5, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (24, N'dca82236-88df-4ed6-8655-9ccb7b48de76', N'alexMob3@dd.cc', N'alexMob3', CAST(N'2020-08-08T20:22:25.4914413' AS DateTime2), N'010563219431', 1, NULL, NULL, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 5, NULL, NULL, NULL, 1, NULL, 5, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (25, N'dca82236-88df-4ed6-8655-9ccb7b48de76', N'alexMob4@dd.cc', N'alexMob4', CAST(N'2020-08-08T20:22:25.4914413' AS DateTime2), N'010563219431', 1, NULL, NULL, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 5, NULL, NULL, NULL, 1, NULL, 5, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (26, N'dca82236-88df-4ed6-8655-9ccb7b48de76', N'alexMob5@dd.cc', N'alexMob5', CAST(N'2020-08-08T20:22:25.4914413' AS DateTime2), N'010563219431', 1, NULL, NULL, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 5, NULL, NULL, NULL, 1, NULL, 5, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (27, N'dca82236-88df-4ed6-8655-9ccb7b48de76', N'alexMob6@dd.cc', N'alexMob6', CAST(N'2020-08-08T20:22:25.4914413' AS DateTime2), N'010563219431', 1, NULL, NULL, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 5, NULL, NULL, NULL, 1, NULL, 5, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (28, N'8077b98e-038a-47ec-a730-daa817976076', N'hassan@h.com', N'Test', CAST(N'2020-08-09T14:19:53.9838420' AS DateTime2), N'01112223330', 0, 0xD671BF394E8730FCBA3723EFBDBB751663651918304F9F4985244F85C0456130B6FB74E15C2F56489014EF8C60CD0F8D1BA2342B006D2369D3B3C56A48729D75, 0x83F01FCEF2C5756C9F105404B46049E45230BC94AA11BFF0FE0BFDD49A127F0A0F440654C2EFCB286084DBF7B383669AC3FC48349B58970758F4EE3DDA97BCE5E0A52405D5D2625385F3E6F2F7CED03099F1A6BE61A9EADFDB72AA4C25C2EC6F9787C17271BCF968CF5095675B128910C1501F144173A76EF4F49A5E0BA3A9DE, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 5, NULL, NULL, N'1521', 0, CAST(N'2020-08-09T15:19:53.9841568' AS DateTime2), NULL, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (29, N'58e3b6d1-2339-4ada-9e5a-54bbe6bac0c3', N'hassan2@h.com', N'test', CAST(N'2020-08-09T14:24:18.3739096' AS DateTime2), N'01112223331', 0, 0xDDF84CFE134F699F65085F62AA08B3B5C6FD71EB18BA4F3E1BBFCCE9B11FC957D90AF48212B60D4AC1085D0032F15A761757C9974A45B0A8C2C2D994CF9F646D, 0x13D8938AA927D55B5BF8C43A5FDABD1E8AF703D51C13AEFDAA496F62B7E2B435CD9CDE519ECDFFFE70F5BE95EC9E244E70C6033E8863DB4273FEFF7637889B0125FF0DD15DC256161E42E75AE962B6CADB80364C0D8904C2CE90C62878B5EE6EFBC994BE7267F90067040E6CFAEFEC8EE92F5395EB5C8B109A73757F6050E4B9, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 5, NULL, NULL, N'4724', 0, CAST(N'2020-08-09T15:24:18.3739145' AS DateTime2), NULL, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (30, N'55ed013b-cc0a-48f9-9038-ebe19af26164', N'hassan3@h.com', N'Test', CAST(N'2020-08-09T14:25:41.0999101' AS DateTime2), N'01112223333', 1, 0xBC968EB615FDECC53DC824A0D8E67E3E9823A094129BE2989C7D1614DCF0CA2109050859C7B0A77DB6071505B80BBF45A1FCF510A6205DD15DB02F38147986B8, 0xD5E923A941DC519DB1FDA645D4A4904AAEA577934034D51815E45E003A316A71766CC87FE3C670AB2DC66A4BD7B3D71615C9C926B3028827209DFE422A450834BBFCD0E00B627C6B68874039FBD884DCA40B3C0CCD6AAF56DA0E46A5A5901189BE3C724AC0F7FE575E204CF81F141EDF10CBF45ABB5F2E1D24BF8D6ADBBB4D24, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 5, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (31, N'5d75d805-459e-4eb6-b06b-a622b63240f9', N'h1@m.com', N'Hassan', CAST(N'2020-08-09T15:00:06.2933648' AS DateTime2), N'01112223332', 0, 0xB038EF092211A432F6247C8BC60F490079C2EF07767BDFCFFCA25D5BCB3D5B4730101605E336639AF5E0A1B0A8A7C392917FE9F9712ED50823A536A35F5B0252, 0xE356A5A52F633547AC843981227AFCE4E993684DEEB6514BCFA68F97B8D171D993543415BE2EF93AA572EAB2C30B27DB9A8428271C90B2103699FB002F403987A62CF20872C098D53FB7EEDFDBEF4457BCD2EE2A0F8F25ADA9503288580EFD4E3EAED50798A90AAC2D838EB790D370F3060337AA64DD57059EBFCC8C1620B1D1, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 5, NULL, NULL, N'2166', 0, CAST(N'2020-08-09T16:00:06.2933654' AS DateTime2), NULL, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (32, N'cff0119c-dfe9-4b88-8fc1-34d5d580d3bf', N'h2@m.com', N'hassan', CAST(N'2020-08-09T15:01:44.3069828' AS DateTime2), N'0123456999', 0, 0xC7926D5B6B87CC45C12CE4587238497D2F599F75CB0E4987A47A494383969AEBD46054FF1B0A1BEE63535DC32E3431D8AAEF0DB55C283527D4D3DD92F0E8C8A4, 0x52A8D57123DC1B16DE1D80A07205D168FD088F4C6C8EA26E521181D4A83051E3F08AD0AFA561E580EBD4D4075965684089FE86C4F662B4E68D77EFD7D8FABD53D1C9F37C98776A92F12937387FA75D2D24A3E6A0AD65B79064A7C4B170E612959A72226ABEECA562A1E6F854A9F48777EDDF864D57C1240C5926643C9BEE5FF2, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 5, NULL, NULL, N'6386', 0, CAST(N'2020-08-09T16:01:44.3069838' AS DateTime2), NULL, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (33, N'a3f95360-940f-4fb3-9eca-b630283c1390', N'h3@m.com', N'samuel ', CAST(N'2020-08-25T17:00:43.7408674' AS DateTime2), N'01201842678', 1, 0x502C16E7E6CC598507F1EB830525CFE7B9FB7034772FFCDED51E9EFC6ABE5D6EF2260955E5F47D082B0D5EA3240876934EA6ED65D4C8180E52FE9DC20F0369D6, 0x736D3D2340475AA7104362AFE395AD07D5AC82611336865A4FD0F37F0359C0A622149129840CB4AB90BB8A2BB15EB6D815689B3CB5E74FD5DA3E3C453E1C0BF55CE13BC3180422E8BAD0431E7CF59ADA4953AFFB09864BB9C01B82CD099402AD6706ED11147DD2958B7CD32678DCE32A94CED7522F751C3BC29B46E3AC78C5C3, N'UserUploads/a3f95360-940f-4fb3-9eca-b630283c1390UserImage', NULL, NULL, CAST(N'2020-08-25T18:01:38.5101885' AS DateTime2), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 33, CAST(N'2020-08-25T18:01:38.5101885' AS DateTime2), 5, NULL, NULL, NULL, 1, NULL, NULL, N'dKQ6FaRvTheseQEQVGgVCe:APA91bHpUx18WWJrGbXQx3-IBILEdSwK7vLRZwyQF3CXlTPEb37HQDLk00w76M639gdowdETikGZfUpuZ6BXVj1GKf1U1AFTS03Ba_hA9yiL7BT46MUlNOv1tXyHhYMTKSfoqdaLD1Dy', NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (34, N'6bf9bc42-fca2-44d3-9c63-0a967b6da7f7', N'h4@m.com', N'Hassan', CAST(N'2020-09-06T17:02:19.1860059' AS DateTime2), N'0168952865', 1, 0x1D060CF4FD2991CBB92AD32CF2502BBC0FF600149920D410EF66BBF937C5B13053E171BA12BC63DCF037FBFD006F179BFEC626D3F2215AA6CFAD71806305AEC2, 0x62C3E9DFB0C25F926B1380360BED1A9B57F80061838E6B7EE249CE121B70CF93E7B7B023C9E8B1E62CE26903EBE3D1E9478DDBE87D9FDE980AF0F5289B2FEA6859FC3173696D6479BCDBBCF4AAAC4D094BDE2B088B2F3016F39475ACEB3EF0354E7B0BE44BD5E969A5283516F1210B11FFAA1592A637E7AD19FA259288BD9E00, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 5, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (36, N'60033565-7e7b-4f57-bfa8-1541fe4073ba', N'ss@hh.c', N'sehTest', NULL, N'1111', 0, 0xC252776946741F4ABE8A8BE668ADCD496BB030539C7ADB42772295C1ACF70C43A3505C0413BDF0BC01C73B0B5D32090E1B1B1A735F2CC6A2881726840D497287, 0xDD0AD1695A04871BB1B0629E7BBFF098EF43E478A5AC18D7A07712310E61EC52623670AD2CC0644C0B1EFD001A7831D71026ADFDA0C1B13D18388AC64144E02921764E3357C46EA322B8ABCA0DC42EA607633FDD9D576F621907840C1FCD0047365A158681ECDB4BD2B87DF4C073A41FF0043C9D9B7C99D218B3DC496E267C8B, NULL, NULL, NULL, CAST(N'2020-08-09T23:11:06.3033479' AS DateTime2), N'cd1fbf73-f186-4218-adfa-a358c4c9d13a', 11, CAST(N'2020-08-09T23:11:06.3032820' AS DateTime2), NULL, NULL, 2, NULL, NULL, NULL, 0, NULL, 6, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (39, N'42bfdf1d-6040-4ef0-ac89-627963056fcf', N'ss44@hh.c', N'sehTest', NULL, N'113311', 0, 0xB6599367F043E6F5B2FC8F2C4606F88A9DA0FC911F7EEF6AB0CE3D44153B644A903FA7DF4A49A309B6A124C7523423C79D83A4F1FE05246A7940C1FFE4EC90C6, 0x3650BE237785823FCDA97FEC97BA783ECC59B7784C7F957B79044B4FF5C78C0F7DC707788D6FA6F5911D2E3FA73F73218BC07B54BAD030CC0B755530403F79F55358226D17B03E2D75A26AF26E73FC2A4E7CE06CFB5475DCA2CC4E1E0260589264995A524D858BC78A5A176109B8EFF8307B837A430A44F83D834BD6E26DF6D2, NULL, NULL, NULL, CAST(N'2020-08-09T23:13:32.4415582' AS DateTime2), N'1084496c-d81b-4560-ab92-36d01c956396', 11, CAST(N'2020-08-09T23:13:32.3914712' AS DateTime2), NULL, NULL, 3, NULL, NULL, NULL, 0, NULL, 6, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (40, N'577ce7aa-8afd-4488-aa7b-966c24b8a465', N'ss4uoiuuuiuo4@hh.c', N'sehTest', NULL, N'1133888811', 0, 0xBB19142F0474D93873B6D2DFC605463D0F0FA02F5FF8577E67401C7B760B672924813148507DE9BB44A6D24A1704606D3AFADE468E424AA59321D3A00A939E55, 0x31637ACDE8BBED2F0E7066E0F3EF49D347D7EB55B308B09CC492E1ABDCE3E3DB88779DAED362EBF92E31D4580C24E40EA092A377CBF00B0B1B3D175F503C502EDAE3DE52D3BDF4F0BDB4D329C8EB0AACD060CFD1143F6DD75B676EF1785CA887B786BA0500736027BCC1DA3A43BB693A8B8B135F557BD2958773E90ED4DCCB0B, NULL, NULL, NULL, CAST(N'2020-08-09T23:53:05.8643036' AS DateTime2), N'abd0b192-e9f0-469c-a766-aeeb823f9420', 11, CAST(N'2020-08-09T23:53:05.8622548' AS DateTime2), NULL, NULL, 2, NULL, NULL, NULL, 0, NULL, 7, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (41, N'9ca3bc32-d530-470d-bd5b-8c9a5069f640', N'test@test.com', N'test', NULL, N'0120222', 0, 0xCDA9EDCBEAB6DA0F9A238624EEDF6F177914E03C24DFFACE899F678C5992FEAD849A0982A0504C565C3CA1826D262FF7C835ADC6DB5246D87CF4ACC5ABB2429E, 0x653BDBECC80DA36D261CA41ABE831BA5B2D63C5007DF239E3E2B369C11EB89BDECAAAF8025812579D8D3CAD5D35C24D51BF4C1E22BDEA078B19CBA02DB94FF88F204E5436AD0A88DE74A71ABAC71F6BFE674581B8943C118B965664613CE3B12335C48A6F08E6FFDFD0D5C1D4E384FEA499CCE03A63EC23595AEFA2D0C9ECC89, NULL, NULL, NULL, CAST(N'2020-08-10T00:01:38.9214114' AS DateTime2), N'8f852df9-96a3-4b46-bcb8-0b73ee5775c4', 12, CAST(N'2020-08-10T00:01:38.9193605' AS DateTime2), NULL, NULL, 2, NULL, NULL, NULL, 0, NULL, 7, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (42, N'74d7bb96-6227-4200-aa8e-3ee733093d1a', N'beboadmin@dmail.com', N'bebo admin', NULL, N'0100000000000', 0, 0x009D38300D8D1D659059F3BDBA3EE88C7B84A18A77E8202F1D4F86B7119E68540574BA9BF95D0A660566F2989F993AC1116A81B49495484EA815C8371828036B, 0x3733AB63520C425D1248E913DC3B2007FE55864A5B829859B27E8E4EADDA46068D0A64B33343ED5686386B6CF88441D7BB2F2C15BD1D02A1E3D7350E6951A2C526B78D63B99F619F1CD8D5517007BDBFFBEA398D48AC3393CF856EDB018BF876CE1A26042B7F6D956C5E5B93B29F41B5ED4F5B88C22C15BBD83982D6E79DEACA, NULL, NULL, NULL, CAST(N'2020-08-10T00:07:24.8583858' AS DateTime2), N'8e08342d-2a0d-4b8c-925b-d96919defe51', 12, CAST(N'2020-08-10T00:04:07.7783025' AS DateTime2), 12, CAST(N'2020-08-10T00:07:24.8583858' AS DateTime2), 2, NULL, NULL, NULL, 0, NULL, 6, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (43, N'055dc364-4d7b-439c-91f6-ef9f316e00c6', N'admingdeed@gmail.com', N'admin gdeed', CAST(N'2020-08-13T23:43:12.9006127' AS DateTime2), N'012222222', 1, 0xC46794002D084DE18B24D0FBF26EC68E19D12592C2FEBCE29D5F76BFC62886E7647EE549CAD987D230D982177870600D2A25D89F8B4C95F0C8C72AB8CE151839, 0x7BF7F26006E49B129C8E7F226F31B3F77A6A917D23E9A06FCFFDFBF56B1B6BEF93DDB818E7EC6DA89A167D00197DCBA5770BE8585D19F40BB237FE8FA3C96180E612164A231797015C0A496137B172FBB4C3E21B75B38DBDB9379C1AE9335FEEF9C685A5618002117C3E19B23202DECB32D37C4481EF08FE4ACF26A7E365B519, NULL, NULL, NULL, CAST(N'2020-08-10T00:20:00.0194485' AS DateTime2), N'784cf30c-63dc-4641-aef9-1b29157c4354', 12, CAST(N'2020-08-10T00:10:24.4586236' AS DateTime2), 12, CAST(N'2020-08-10T00:20:00.0194485' AS DateTime2), 2, NULL, NULL, NULL, 0, NULL, 6, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (44, N'0ac0c815-094f-40f0-b355-3d32fbfe90d7', N'teller@gmail.com', N'teller', NULL, N'01212222', 1, 0xE4610B384D743C4CC7EBF74CF0C5EF85935F116084AFF9F048920250ABF60A80DA6346C4CBC88E1CE87A63BA857884C7F5A550E6B2F19EC7EC0F256C44DE6DD5, 0x6FAB3401273697E818545B3D0174796BF3CBE11F0890F535EDF31391EF3981E221B2E6A6228832EE74F2001FAA05831782BE2D568AE19C47244F0FA891C4E3EAF114CAFA35CDC805DA8F7314A796A9D906361BE0290A0FC843D7BA12CE8387642072EAB90402D9324535AB771865E568D0E4ADD60BA0A5E547E0AAED4E127735, NULL, NULL, NULL, CAST(N'2020-08-10T00:23:36.8525987' AS DateTime2), N'b1ef6fe1-3139-4a45-8e01-6e566ef4a683', 43, CAST(N'2020-08-10T00:23:36.8523996' AS DateTime2), NULL, NULL, 4, NULL, NULL, NULL, 0, NULL, 6, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (45, N'086d83e0-674a-41a2-80ba-35c08e9199a3', N'supervisor@gmail.com', N'supervisor', CAST(N'2020-08-14T00:00:50.2642699' AS DateTime2), N'01000', 1, 0x33FD06ED3778AB0C414C5B6E31371FE3FFAFEC3E721A7223DEBC170E18CBA67ADCE40659CDD91D4FC589F8957417E70FFCBA9FFCBDBFDB7B6C9A1B5190E2562D, 0x9D1039EA30AFDA810F75A58749162CEDCC8746A50C255E8664EA94017A32DB9005B92829C08BBFCFE577D2B059F6144E0BF9454E7BCFD5560BCCD9C6B55DB8728581810BE3CFE219B055588E9DE0A11E2B8F0FB05A3BC4F00837B949B4D4A62B7477E3A018E452527BC0EE388D8C1FD56F866E133A3073BA3F588F665E436C91, NULL, NULL, NULL, CAST(N'2020-08-10T00:29:35.3766698' AS DateTime2), N'bd0cc967-fb6c-4188-8bef-9714cec1c897', 43, CAST(N'2020-08-10T00:28:15.3023819' AS DateTime2), 43, CAST(N'2020-08-10T00:29:35.3766698' AS DateTime2), 3, NULL, NULL, NULL, 0, NULL, 6, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (46, N'dca82236-88df-4ed6-8655-9ccb7b48de76', N'alexTailorEdaa@dd.cc', N'alexTailor Edaa', CAST(N'2020-08-11T14:02:08.4836305' AS DateTime2), N'010563219431', 1, NULL, NULL, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 4, NULL, NULL, NULL, 1, NULL, 5, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (47, N'dca82236-88df-4ed6-8655-9ccb7b48de76', N'alexTailorEdaa2@dd.cc', N'alexTailor Edaa2', CAST(N'2020-08-11T14:02:08.4836305' AS DateTime2), N'010563219431', 1, NULL, NULL, NULL, NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 4, NULL, NULL, NULL, 1, NULL, 5, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (48, N'0239dd1e-67af-4534-8d4b-f6f6983255aa', N'kady@gmai.com', N'El Kady', CAST(N'2020-08-14T15:21:40.0289720' AS DateTime2), N'0122222', 1, 0x3007AFC34CFCC3119158A3D6893E51DBF9E6655ACE2D24987159487D3FDDB52B43FFBA8405BAAA91272379E8AD971F451F07AD12E7FCF330F3C3F7843E63DCCD, 0xAD561BD91880E85CBE17F1F330693540E9878A15CCC9956EA83467FE993D4C389BD67322C3202F3361C8A038D2114F14080F27473A0EAA3FE5CA1D5E77D0073F95026E9F82F95E5F50D138B372533390F81F5D63B00A83C619E369D57EE2676FED7985102FF0BBF6BD254648E8A6F3990276D0A0036C02B60252C7355DA11679, NULL, NULL, NULL, CAST(N'2020-08-14T14:29:07.1146407' AS DateTime2), N'87f01658-a5d1-4148-849b-1d4491ea45db', 12, CAST(N'2020-08-14T14:29:07.1144248' AS DateTime2), NULL, NULL, 2, NULL, NULL, NULL, 0, NULL, 8, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (49, N'f2f2458c-7e69-45c3-868a-347ad4dfe655', N'moshref@kadaa.com', N'El moshref', CAST(N'2020-08-14T15:02:02.5963312' AS DateTime2), N'0120000', 1, 0x0697AC83FBB7EB49D7438C1743ED6E4D58E26D6C6890EEB8415409928E6947833122570F494E29988CE60BBE8DAE04406B9C89EBECA78F506277BD8424194676, 0x6F634EE5A2F6821C19A9A99365936B8EF2AE08CDA27D4DD9338EA9F345A15A382E4D749C4F8AEA1EC0711EE2078C50EEA8FC6A7926C58701F91B4AECF51E421620FE5A5A76725B0C72A7FD8E1130125954524C78066ECD8A23A5548F5008B71D1CB0840E2F77915B45B14420A2945F076FEC7D801E7707CB00A61F5710503EEF, NULL, NULL, NULL, CAST(N'2020-08-14T14:32:29.1825107' AS DateTime2), N'782dce14-b16c-4b38-8490-a7756bbdef97', 48, CAST(N'2020-08-14T14:32:29.1823068' AS DateTime2), NULL, NULL, 3, NULL, NULL, NULL, 0, NULL, 8, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (50, N'6b22b52b-a4ff-4baa-93a8-3eb0c6b8574d', N'cairosuper@gmail.com', N'Cairo Supervisor', CAST(N'2020-08-14T15:31:13.2255980' AS DateTime2), N'0222222', 1, 0x651DEB50D454A6B89E5936953CF0E5BB0EC48C0CF946AA38E9AEFD63C09CFB03DC186F3A4AC73B00C2F521D6C6067786C442B69C5A6F48A4445F94D4DEEBDFA6, 0x5D9DF34698E54EBEB47695322E3D8379FD7538D885DAC972A3F2A879F1E434F37A7BB310D37001411CC7F38E151E57414FD446D9FFAE4293732B13CE445F0DB3C7E11F821B1D7805A5EE986379294739C8A044D7127BE9E193BA64A2C92BD69AE0717EFA987D223F4D9C9013002C0F8285A40CA9C9B4BD24CF7221C88CD3425A, NULL, NULL, NULL, CAST(N'2020-08-14T15:24:58.3003645' AS DateTime2), N'ace5c3fa-6634-449c-80f1-9bb134feb0c7', 48, CAST(N'2020-08-14T15:24:58.2981877' AS DateTime2), NULL, NULL, 3, NULL, NULL, NULL, 0, NULL, 8, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (51, N'3e89839f-b64b-46de-8d74-ad54667d4b87', N'kadaa2admin@gmail.com', N'el kadaa admin ', CAST(N'2020-08-19T17:12:23.0240986' AS DateTime2), N'01000000', 1, 0x7902F8694E74C6A883408B9D1864F2262626D48AD9FEE9E367487DE05DF8CCF8DEF27437797B736CDBF6469065AD4C901E156E614E45677CFEDEB764CEF867E2, 0x73E71DFF1AC36BF1A764D79B55DA2773312A528EABD22C9959AB347504240AC07CC16835DC164E68DE26ACD261825A6DFA882031D7A19502CAFBEFE2499A66DF0627577F76C45BAE14380E98D5B4344639C62708CFC31AA933422572A8644600C30DCFBC17E2DF529D57290253328203D67006B6412471770CC3E81AF89AA6A9, N'UserUploads/3e89839f-b64b-46de-8d74-ad54667d4b87download.jpg', NULL, NULL, CAST(N'2020-08-16T21:37:03.4675525' AS DateTime2), N'6aac62cf-dbcb-41ad-95c9-b5d2e9dda525', 12, CAST(N'2020-08-14T16:32:47.8949950' AS DateTime2), 51, CAST(N'2020-08-16T21:37:03.4675525' AS DateTime2), 2, NULL, NULL, NULL, 0, NULL, 8, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (52, N'4273c56c-7cc5-4474-a6f7-204271092a08', N'alex@gmail.com', N'alex supervisor', NULL, N'01222222', 1, 0xB1672F122D85475DA5502D6505C3CD97E1D6BBA2D49B29455192FE14616791059CC977EEDF4455FBAD39BB266A1FE0402AE891E093420954FB6D4E603ECB449A, 0x03115CBD68E417644BE4DF7F71A089EBC9C38BEAAE4CDEA910B1AC22AD5D4D415A98D7B59BDDCA78F842715FC153856CEE6C932A9B8EE7F341BDE8E38893457DF14F04892FF694AC5FE168A9A1A64FCB2EED5D0FC07F3FA3D0E637499CBC86D6269AB6879B96B07FC87165B59FC2B458A52A993F993BF848D43432115EE68947, NULL, NULL, NULL, CAST(N'2020-08-14T16:48:42.2435426' AS DateTime2), N'28b1d806-f852-40c7-91af-01c863d2d182', 51, CAST(N'2020-08-14T16:48:42.2433270' AS DateTime2), NULL, NULL, 3, NULL, NULL, NULL, 0, NULL, 8, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (53, N'0d6c1845-507b-4b47-a7de-2fa2afa9df0f', N'alexsuperrr@gmail.com', N'alex supervisor', CAST(N'2020-08-19T17:16:09.3778248' AS DateTime2), N'012255454', 1, 0x1B02F82145B9B7D7861064277A79CCB039DEA58D3EE9A02E84645A09CE8FE0900B212B4D2207387829C6498814B2DA2D6EF89BA166E1F4C20FDB6E31445F1278, 0xEBA34B737B4F71B1DE2F354AB6D0BF24897FFBB9375FEDF0DBBBD1EC80E3D775729DB4AEDA9DBC1A3AF1DF7C4C0A702218D084FE709A3E9D03B0711FEE27D52B1B50D6BFD4AEECDCCFEC1445394D6ED091F9BD03D3B43EAB2348C9D99DBE496ABB6FB26458D18F508AEBEDB3B19F720F581EA785566BC4AC77D3542D495DDBC0, N'UserUploads/0d6c1845-507b-4b47-a7de-2fa2afa9df0fdownload (2).jpg', NULL, NULL, CAST(N'2020-08-16T21:50:11.1654633' AS DateTime2), N'8566b813-7b0c-4d57-8d23-e3061ebd6263', 51, CAST(N'2020-08-14T16:54:32.1558369' AS DateTime2), 53, CAST(N'2020-08-16T21:50:11.1654633' AS DateTime2), 3, NULL, NULL, NULL, 0, NULL, 8, NULL, 27)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (54, N'b5f67ad6-f9c7-4636-b044-98d1725f4964', N'tellerr@gmail.com', N'alex teller', NULL, N'012221212', 1, 0x89E774A6E93D9F8CFEC38BB1D52F950EC23FAD7D9DDDA058F0051DAE74C6EF6FF9A82F7D601DA8590DBE64CF2A01320BEE98357EEFE63DCDB7CC6BC28E4F5E6D, 0xF3B6283C652771C144FCDB3C71F64B646543FEDF948F4CB29EE781356A517AD244BBB265882423D28EDF0438F4B60CB31E13F8F7C76FED90C1B3EF56BBFF42557B973C0801E411518AF98B670FBCD52CF2C8C00505B4FD69EDD2E0FE2B2D55B290DB5B65EF509FAF21C5D48D582B647BA8098937147EDB20776C0D8C4E920B99, NULL, NULL, NULL, CAST(N'2020-08-17T21:13:33.6612392' AS DateTime2), N'28cb9fe6-5c6e-4c0c-879f-697df3c352df', 51, CAST(N'2020-08-17T21:13:33.6610386' AS DateTime2), NULL, NULL, 4, NULL, NULL, NULL, 0, NULL, 8, NULL, 27)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (55, N'b04f98dc-e168-4309-956c-ce410d0962ae', N'teller2@gmail.com', N'Teller 2', NULL, N'01256', 1, 0xF6F6EEC1637FD497B44744894F74942D10EC4DDFF43E924F157AC102F99450EDA14802DFFB8F046F114CD0EDB457D7CAA2BE69CF53D0CCC3F1D954F01B2E9DB7, 0xA0C752955E4256652CA2FEB39A95E14A9B0D837621E063389D80FF29B1176A56E7638AEDD8C92CBA85F16B11808099F1D4CCC7E757C8D5D0E22C079FD1A0FF8B9F3DCA3422DDCE80332C3D93A275118E047C1060EEF800074D109B6AABD62C9638DC4A86A7774010EE09061647D3A697A11E1BD297522210A6C7F70631263513, NULL, NULL, NULL, CAST(N'2020-08-19T00:03:25.6572265' AS DateTime2), N'1ae95ce0-a1fd-4fd0-b722-38dcacf5f167', 51, CAST(N'2020-08-19T00:03:25.6570254' AS DateTime2), NULL, NULL, 4, NULL, NULL, NULL, 0, NULL, 8, NULL, 27)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (56, N'e5776e1f-bc4e-4de7-a74e-d44205200ff3', N'teller4@gmail.com', N'Tellr 4', NULL, N'012548', 1, 0xEF9A3C713CC3E76786228C5D320286543E9193690D05B7618AABBB9448950DE9474947D1AEE2CA3858EA349AC12AAB92042C283EFCD54935CCCEABA57E1FF2EC, 0x489A4E6D0F0DA37BEA53BA20739EB2B20174D06A8920D418C414E6F60F21008EDC9DDF937F52D3C14E4E45F8C52ABC7CCD8E7E18D4BD9E383D5BFFE50BD956C477FE9AAA10CF3D9891F3F41CD195E742F6C755D25447C5304938A2A2FCD558B440B0F265692DFBFBCB5927F990E1F69BA9B4B863C9DFDFE35A4B6A35C3D0071B, NULL, NULL, NULL, CAST(N'2020-08-19T00:11:34.6777986' AS DateTime2), N'a3ce4966-8831-43d9-9319-f46e37bdc599', 51, CAST(N'2020-08-19T00:11:34.6775971' AS DateTime2), NULL, NULL, 4, NULL, NULL, NULL, 0, NULL, 8, NULL, 27)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (57, N'f0a5244b-1ee8-4b88-b3b0-6e57733bd32e', N's3ody@gmail.com', N'S3ody Admin', CAST(N'2020-08-20T21:22:45.7439260' AS DateTime2), N'01222220122', 1, 0x78E6895087C8E0D6DB34B0F1D8B5CC964F05F3C036D0DFD7EBD1C38831CBE9712F644D87A914518A2C5AA100E6AF9A41E94B7570CA81F28F828AA03DFCFF8161, 0x7DDF053C6B01824D99C33C0EC31F071F49A8FD9C6C0B815FC0E8598DF40DADC85533260725A3F0D5AAB476BBE21F8847B7C1988DFADF4B582DE5150B982CABDBDF870C3B6CEF05E010C0EA33E10910FBB5C751EB7059C1A1D5FFA4AC2D5A319286ADD9F45DE4A97A968A50E99DB80A7E157EBB6FD450400FB465EB691E7FC625, NULL, NULL, NULL, CAST(N'2020-08-19T20:28:15.4428731' AS DateTime2), N'58c40886-f8f3-480f-ad05-0bd2382fdd96', 12, CAST(N'2020-08-19T20:28:15.4426703' AS DateTime2), NULL, NULL, 2, NULL, NULL, NULL, 0, NULL, 9, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (58, N'0a540b2a-1259-4598-99a6-8cb1ca070a41', N'hyper@gmail.com', N'Hyper Market', NULL, N'0129522', 1, 0xC7FAD513468B14FC6736448CCB9C9A57AECF2F0DDCD27EBA5F77FE111D65EA2A6EA9F9048730711540A1ABB81422D5F7BD32EEA79D4CB70828895DA4DC4B40E1, 0x693DAB02FC293D7E88832593862EFD8250DCFA22EEB586D8CD2422AECD226C6BB157D06784BADE1389BCF815B371A7C6BE0C49D96C5F503D8316431987B0221339C7E6AC9BC7C74080DD07A2D30DE22125DA97B46DF7D5BFEEAECFF14D96B8880665AC8D277C793B6D1F55DF6408C5A168F0302C127DA91031F5DB2FC4A8BC70, NULL, NULL, NULL, CAST(N'2020-08-19T20:30:09.0612915' AS DateTime2), N'9927a6ac-34e2-42b8-a873-c394057a5945', 12, CAST(N'2020-08-19T20:30:09.0610950' AS DateTime2), NULL, NULL, 2, NULL, NULL, NULL, 0, NULL, 10, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (59, N'e7f33b94-7101-4a8d-9e61-d7138ef5e5b3', N's3odysupervisor@gmail.com', N's3ody supervisor', CAST(N'2020-08-20T21:39:14.3704955' AS DateTime2), N'1234545455', 1, 0x22AA9F7C46F9FF0DBEF2951D93A5F9D673DE90DCAF8803F92FE59B940BA98550EE516F2AC7F7B4984B4A2BBE1F3AA5DB6C6012DA2CFCCA46EE819D59FBA48CEB, 0x8187852C4C7E9515A3451DB7859BCB28225923193F4FE86AC7DFD37B33CFBCADB6D1CE1D859F72C1868F13220AF8589734BFD0DD555927EBC1BFB7641824C879E47002BD4E361E4F861BB91B94E36AE14086BF31699D257004F2F2753938ABFC6C153AFE323079354C2707938B8149A745CC164C77BFFA293C3227E4E20B880E, NULL, NULL, NULL, CAST(N'2020-08-19T20:39:23.2984428' AS DateTime2), N'2d8eace0-fb63-47db-abff-46f3fa3d29b3', 57, CAST(N'2020-08-19T20:39:23.2982328' AS DateTime2), NULL, NULL, 3, NULL, NULL, NULL, 0, NULL, 9, NULL, 28)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (60, N'7a80601d-64cc-48fa-8b22-481121b01bdf', N's3odyteller1@gmail.com', N'teller 1', CAST(N'2020-09-20T19:46:54.1440049' AS DateTime2), N'122545588', 1, 0xDD2F4185ACEA938670EB3DD25CFE1BBA1047EB65E1CABFE897E460B664712E413CD7254D930B33D9D5F4DE9AAA0D2BBEFB1A00C491AB166B24CD82CBEA94EFCE, 0xF8956BAACCE62191715B18D6CD489C8DD3737930FEA2858D493A990F0DD388924B8AFCA03D3B11E242BDEF88CB2915900CBD753B5A73EB870280A1D4BB307AB0C96C1F8ADC6FF871A30988343E2379CBF2E33161355DE6BAE71782A70FF746916A1B18D935B0071FEBFD2DF6EC443DE3E80F1D874556BA8310ACAFCD915273B7, NULL, NULL, NULL, CAST(N'2020-08-19T20:40:26.1895319' AS DateTime2), N'6b8f3e06-b297-4dc9-8fe3-354ab74a20a7', 57, CAST(N'2020-08-19T20:40:26.1893320' AS DateTime2), NULL, NULL, 4, NULL, NULL, NULL, 0, NULL, 9, NULL, 28)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (61, N'c2f91f11-bd64-4885-b756-8035b3865223', N'steller2@gmail.com', N'teller 2', CAST(N'2020-09-20T19:45:52.6040310' AS DateTime2), N'01221121215', 1, 0xED68734B23109B1598455C247F18D3AF614B640804B4FBD8BCFDF96FD8D85FEE97E52E36BF7DD5C2E11A9560F9D2890B15DCFB248E48A9556310930AC679CC26, 0xED3DCF5EFC5B5EEA77570130BE30B6CF9C4A01F97C64CFC72DDB828696D31F741F5B87375B525825DBAEF87748C3A6C441277A431C3297428870E7FDD0D703C571AEA8F59C86D5AAC6C9D68EDD2D8F0098CA060DC44BA3E92F12514306E52CE050EB3EFE6468318DE44163FF70F1D4C13AC1023C048D54A7B8531E39945D1F4C, N'UserUploads/c2f91f11-bd64-4885-b756-8035b3865223dz_cropped.png', NULL, NULL, CAST(N'2020-08-21T17:43:08.1019155' AS DateTime2), N'429008b4-36ae-4c0e-acdd-cdff4f2ab6e5', 57, CAST(N'2020-08-19T21:13:00.4265249' AS DateTime2), 61, CAST(N'2020-08-21T17:43:08.1019155' AS DateTime2), 4, NULL, NULL, NULL, 0, NULL, 9, NULL, 28)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (62, N'601468b8-7a2a-47fd-98ae-8d42d2f2d7da', N'm@m.com', N'Mario Hany', CAST(N'2020-08-24T11:16:33.6865569' AS DateTime2), N'123123123', 0, 0x0B8BF666C96197D2FEB86CA360AB7F45B6D8A031F91B84CEAA40908D49FB4E2792647CBA16FE6A48D81A99C4AE27F9BE689CA1BEC0E7962DD0A878D160938841, 0xF010A27AD305A538907E31F9A9B8E448DE8DCB1CB8FB2A8B40FEEA6BB3810475F6C75D02824F3648B4BB536804DD68B4FB4769B2110F4A1F4581B85034CF9E2DDD83B4963A59A505AF16C71507B5839FACF0EDFFC260D72EEAF3B0C39BDC425EF7378FE16409727BD284AFB0401A603F0F97DCF5EAE14077B031C07C36622221, N'UserUploads/601468b8-7a2a-47fd-98ae-8d42d2f2d7daWhatsApp Image 2020-08-06 at 2.22.30 AM.jpeg', NULL, NULL, CAST(N'2020-08-24T11:20:33.2553821' AS DateTime2), N'00000000-0000-0000-0000-000000000000', NULL, NULL, 15, CAST(N'2020-08-24T11:20:33.2553821' AS DateTime2), 5, NULL, NULL, NULL, 1, NULL, NULL, N'd-xwHqyyQFWr4MZ6AQtnSX:APA91bGafVuRHuXQ2oQjGaezttrSwufNCzvW860hwTFkxMIsMHLIPn7ZFB0NpjDtOuBvU-mSf5iBlVvmGbj63WvVzqjRvE5FZ5KJOJGL05onM6P5MdeWYnIsPhhV-qa97LyPI3G-SlFm', NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (63, N'1a5b45ea-2154-40b7-bdd2-6bf6ef58ecef', N'masl7aAdmin@gmail.com', N'Sa7eb el masl7a', CAST(N'2020-10-03T21:15:49.9303969' AS DateTime2), N'0122154455', 1, 0xA5BD9C0FEA3572684D77594B1135B241222887F77E73EAAAA79A43194DD8A8C7ABF758CAE8EF4DC92CF7ACFB5CBBED5A4960CE43B6DE4C662DB5FD1B11E5073D, 0x22ADE1C15857CCAA27E7CF857C7A5C453E3E8DF3105990318EF785DEAE2A0CEC9A731F606550C173E40A3BB18B404F4CB007F3829812475F56004F231E93E2F3AC633AB282E6565714B3799641415FEFF44FBEB58D2A926DF201CF8EF0969B63A929EBAE73F472B6FE415ADC400065030327D0F942FAD75B548BFFC03ACB3241, NULL, NULL, NULL, CAST(N'2020-08-25T18:26:43.8809102' AS DateTime2), N'deb7cae7-2eaa-4ff3-9e0e-9b02746d68c6', 12, CAST(N'2020-08-25T18:26:43.8807096' AS DateTime2), NULL, NULL, 2, NULL, NULL, NULL, 0, NULL, 11, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (64, N'6fb01a44-31c3-46b9-923c-a0ee3af8e7f3', N'salesSupportTeler1@gmail.com', N'Sales Support Teller', CAST(N'2020-10-02T10:22:00.8636658' AS DateTime2), N'012212545', 1, 0xC7E467F6603FA319E880998721724C72C56FEE7E3341164177166A5351AA649B197F90B25F43C8E35A645E7014B54CC49C7E94A5EB79B500E770CFA61961A27A, 0xBEF47CFD8C47A52CD73BC61986929E8A8ED5D61BB691E0935D52841DBAFAEF154748F7094CB5185F4C1861ECB5932878DEACF9920F5368F4EB4C595BD7013486693808A8E51353E3B5BB062A26C97D9B83D56778A7E1BC78C3A19C00F49CD5D98F3824F25F060008C704B4E2BAC6A973CEE226E74125A8953D43C89100B5EBE1, NULL, NULL, NULL, CAST(N'2020-08-25T18:31:11.9545110' AS DateTime2), N'bf51db1d-9d70-4287-ae18-f5823e133c58', 63, CAST(N'2020-08-25T18:31:11.9542985' AS DateTime2), NULL, NULL, 4, NULL, NULL, NULL, 0, NULL, 11, NULL, 30)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (65, N'0f4dc945-f044-40cd-b592-f1080885730c', N'salesSupportTeler2@gmail.com', N'Sales Support Teller 2', CAST(N'2020-08-25T22:14:49.9353938' AS DateTime2), N'01225455', 1, 0x7993E2FF10E7D82F3241918595AFAC459650F0F7F63AD6C9C568A6B01A8C4D2D4F08E60F9A0A1EE0E4EEBF8E6806C4E0608C3ED71D1D7B5DA9D41D5EBB8E0942, 0xFFAE548B4207B7E44EA97FF81A1DD52E087109A7497D5A9081BBE0C26229AE4C189F76F1C8A0F217F3947F85C44233859372536DF4EA8F04272557D10484A58CA25522BC74ADED0FF98FA292BCD027576DAA0E9C68D0FC1421C9F7F7F8D3A9C52E140AC529212F2DDC843B6FD77285BD9B307B51AF09D738CFB132A8F2E165F1, NULL, NULL, NULL, CAST(N'2020-08-25T18:31:56.7914589' AS DateTime2), N'0345d538-5808-46a9-98a3-e3cf45df8ad0', 63, CAST(N'2020-08-25T18:31:56.7912593' AS DateTime2), NULL, NULL, 4, NULL, NULL, NULL, 0, NULL, 11, NULL, 30)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (66, N'c9717e25-0a66-4b52-8c6c-d73c841cfb47', N'masl7aShiftSupervisorPart1@gmail.com', N'masl7a Shift Supervisor Part 1', CAST(N'2020-09-24T23:11:20.0191697' AS DateTime2), N'123456', 1, 0xDAED3D24E382514C4B1B2D587429D57ADB625F2D0EFC03C9CA42BF50D35F0FB1467AFD347DEE4F4CF0D726F55B0BA5DAF30008FDF6432350EA5C95D598E3BB8C, 0x9A40EE04197BAF1DE5F6183183EE64ACF3AF84CF6C970697BBCE57D3C0360B62F8DE20389F77D5FB5E79D4C79924EFC8B953A2217A7CA4DE75C0C70724DA39649A7AD61AE097E92F27F0945C11A6477401A49E198B3A8B0665F79D324630B064AFDF0FF18C0E1A3D2A908762C6FCFE70FE2851966EBFCCCB4A0BC857156C3E72, NULL, NULL, NULL, CAST(N'2020-08-25T18:33:15.8283239' AS DateTime2), N'ae0f5e41-0c23-40ed-8fae-dca9ca63f666', 63, CAST(N'2020-08-25T18:33:15.8281229' AS DateTime2), NULL, NULL, 3, NULL, NULL, NULL, 0, NULL, 11, NULL, 30)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (68, N'bec14083-e816-4e9e-9a0c-2cf0a1e3fa38', N'sg@h.com', N'shshs', CAST(N'2020-08-28T17:45:07.2675142' AS DateTime2), N'234234234', 1, NULL, NULL, N'UserUploads/bec14083-e816-4e9e-9a0c-2cf0a1e3fa38Capture.PNG', NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 5, NULL, NULL, N'8577', 1, CAST(N'2020-08-28T18:45:19.5928341' AS DateTime2), NULL, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (69, N'dcdf040c-8b10-48a6-a2c2-ecc3ddbc344f', N'sg2@h.com', N'shshs2', CAST(N'2020-08-28T17:46:37.1338193' AS DateTime2), N'234234234456', 1, NULL, NULL, N'UserUploads/dcdf040c-8b10-48a6-a2c2-ecc3ddbc344fCapture.PNG', NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 5, NULL, NULL, N'8712', 1, CAST(N'2020-08-28T18:46:41.4447457' AS DateTime2), NULL, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (73, N'99e1eb06-30ac-4883-bbed-df9cb3a058fe', N'mariohany9@gmail.com', N'mario hany', CAST(N'2020-09-25T12:01:43.0149464' AS DateTime2), N'1234567890', 1, NULL, NULL, N'https://lh3.googleusercontent.com/a-/AOh14GiWrgTO4A1T1gwpVK-zs0CTAZCXJyI2x44xDUyyhw=s96-c', NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 5, NULL, NULL, N'4335', 1, CAST(N'2020-09-11T09:15:45.4432277' AS DateTime2), NULL, N'eKna61tiRoGoGgZELrSDrI:APA91bEUrA4w5JMvhMSeal2-kYr4tMXPtgGcNl5a_cqxO6mIbIrbGm0xm48UmpPPQB-5GImdU-6Hr0YjLr0tk7mGVzWY90mc-8BdFfcZcLIptQoqEEZGpRkwktxgoNNAJyIlPb1ZO0iN', NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (75, N'edf35ecf-2c7a-4243-b54d-4b05883e3abc', N'mario_goodboy_2010@yahoo.com', N'Mario Hany', CAST(N'2020-10-02T10:26:03.0202312' AS DateTime2), N'01282318740', 1, NULL, NULL, N'http://graph.facebook.com/10218325505790595/picture?type=large', NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 5, NULL, NULL, N'6847', 1, CAST(N'2020-09-25T04:35:25.9104303' AS DateTime2), NULL, N'e2dYVQTPTCmH1puMA_8ho-:APA91bF7EO-JP3dPRBdeg_cMRlVYL0Uj5gAVVr5NMQm8oXBo_nYTxwUlnJqNTfrrTjGR25bsuCoqag8gg5uYb90-aJZD6LESBdsUNn9FWhUSif_zwUOCQ_DzK4r8KRjU3oghTxGeDY-M', NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (76, N'7d1ea1c1-ea30-4d1b-bb28-7cdb25e2ea41', N'qrAdmin@gmail.com', N'qrAdmin', CAST(N'2020-09-26T19:41:39.1020835' AS DateTime2), N'01212555545', 1, 0xA462A6AA85EA853FB30753029E08965009F9EDF7422D524BC50511188DF3FD24848EFCFDB928B4E8147E36A48A91A80EF3F88B91150647E4DEDAFC71EBD800A6, 0xA6AA3C0CDF8341207D8C3EF68DD29156B973187139165B02B6ED25204198DCCB3D48B08B396A95B4D61C8B180E14AF52E1EFF5088DDA1A68BA63A20D841D77DF702F7682A2270961281A2785DFDCE42F5DB34D406C292B9CD9CF2A46976AAE38B10AFD3AA680C3320569CBDF9D3C54864B669C38CEDEFB37D312F2B372233BB7, N'UserUploads/7d1ea1c1-ea30-4d1b-bb28-7cdb25e2ea41download (2).jpg', NULL, NULL, CAST(N'2020-09-26T19:16:08.1789804' AS DateTime2), N'72ecb2ad-ba34-4ead-abf8-e95751df1992', 12, CAST(N'2020-09-26T18:28:56.5135082' AS DateTime2), 76, CAST(N'2020-09-26T19:16:08.1789804' AS DateTime2), 2, NULL, NULL, NULL, 1, NULL, 12, NULL, NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (77, N'6b01debe-649f-4ff0-bb07-d7399cf8fbf1', N'qrSupervisor@gmail.com', N'QR Supervisor', CAST(N'2020-09-28T19:58:09.8900119' AS DateTime2), N'0484512115', 1, 0xDF858ECECA5762310C1E18615F88846067A5B868EB834821B5E4809B9C5D200A883480BBFE8D4A78519A908228F1D073D563C55DFCF8ED75CB75BB364B2529EE, 0x57404B8F23ED0ECC486DCC8AA1B613E84755C8F1D76EA04CF89582155070DBF18671359AD00AF87F45830D0D662F26660F5EF01FD380D4D547C275AB037B502137357D0139889FA555C52FDB81125A29ED01C87D3924F0ECD4E5FE9EE02DE9EC3A573DC276FC82F4DC6174703678DFC93AB1F2FB77630BAEBDCF78C45E3711E5, NULL, NULL, NULL, CAST(N'2020-09-26T18:36:31.0542684' AS DateTime2), N'2e6e09fc-4daf-46f6-af6a-d1c83b039c75', 76, CAST(N'2020-09-26T18:36:31.0541236' AS DateTime2), NULL, NULL, 3, NULL, NULL, NULL, 0, NULL, 12, NULL, 32)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (78, N'c5faa10d-b91e-438e-bb1c-31ee909175c4', N'QRTeller1@gmail.com', N'QR Teller1', CAST(N'2020-09-26T19:08:14.7204461' AS DateTime2), N'024644646', 1, 0xC9857A4388C8951072C75581BB34C1151B1BCC79F74A96A741C083B31F7AE60DA191C6A985194689C70857E2C57382D9E429B27F167424D0DD3BD2DD784ABECE, 0x94BD6B42412BCA32211E00FBA58DCD27DB381D405DB06085D19653A5B91D1D20829B0849FC0C61E8B3D2FCAED9591758E18EC7E3DBE37ED7087BEC05E88E093972833BD599B5D435A2DEA0EFA14140C5E8DFEF64C35DA0CD9E821532819DF0B7F4F7D33168338F50F36778648A977B8C36F93F4394F5E89BCE2830C0897F5B87, NULL, NULL, NULL, CAST(N'2020-09-26T18:37:12.8535188' AS DateTime2), N'0bd200eb-c1ca-44d2-b103-41313b22ef35', 76, CAST(N'2020-09-26T18:37:12.8533668' AS DateTime2), NULL, NULL, 4, NULL, NULL, NULL, 0, NULL, 12, NULL, 32)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (79, N'6d51b42b-4c95-4d0c-be0b-8a9d7d6937fa', N'QRTeller2@gmail.com', N'QRTeller2', CAST(N'2020-09-26T19:19:45.8291526' AS DateTime2), N'1215448494849', 1, 0x9C9AEED891BD0908C6B06F2FC57CC69C30FEAF74D1D5B10289C91F4D675544B59F5FA09FB04F678101E69C1EE8CF96738D1C47BB92B00743307E31CD17B6F7A2, 0xD5533743E43BF5B3B2DDD305F8C37253E4353CF8B319533E4A152A4CEB3BFD28E2485753B395C5157D308935295038C873CCF1D87786A07E17C0276FA97DE097CCF0DA5C02C5DBEC412CE93E961D084E4B8D0A60C8D08AC614985F983622DF89E1DC3B6ACFB00C4E41FC37F97A0FF2701D6A9D5BE1B1E940911C35DF34490B48, NULL, NULL, NULL, CAST(N'2020-09-26T18:38:46.2088546' AS DateTime2), N'd7dd9b6d-cf5b-4c52-ae85-4a6d809ac8d9', 76, CAST(N'2020-09-26T18:38:46.2086991' AS DateTime2), NULL, NULL, 4, NULL, NULL, NULL, 0, NULL, 12, NULL, 32)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (80, N'5d3c0b13-4c83-436c-9f7e-77cf30127de3', N'alaaabdullahabdullah@gmail.com', N'Alaa Abdullah Abdullah', CAST(N'2020-09-26T19:12:05.1649809' AS DateTime2), N'01550138024', 1, NULL, NULL, N'https://lh5.googleusercontent.com/-myKNOldAMzg/AAAAAAAAAAI/AAAAAAAAAAA/AMZuuclC6R7acK1zk6qA4lSKFl7BXddiBw/s96-c/photo.jpg', NULL, NULL, NULL, N'00000000-0000-0000-0000-000000000000', NULL, NULL, NULL, NULL, 5, NULL, NULL, N'7851', 1, CAST(N'2020-09-26T20:12:05.1660053' AS DateTime2), NULL, N'eN7krdLkToKJuapnZYM99l:APA91bET4_9xSRkU2PIXSkssJdm3Ehs3RjunSE1i77olFSJMcCoymYcwis4gkr0m4MCfcxuc-TFOI8E1EDi9fZn9Vwx4d77LEatUBYGrogZj3LUa42ikIStasNHPPnasNSp0bRrwxRqp', NULL)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (81, N'9035171a-a371-469e-b6d7-ebfac083014e', N'QRTeller3@gmail.com', N'QR Teller3', CAST(N'2020-09-26T19:41:06.5475072' AS DateTime2), N'0211544456', 1, 0x444D2B549A2C5417B81998758A0CCFB2D29B680DD81F6A77A3B22688F3A429A970ABF8FC6C690B0CA01DB4E2BBD137D11F40C6032CE3062C000700CD24BA5AB6, 0xCA879EDEE2E550F65A8D3D6078ECBA44923C37F2C790CFEAEBC71FCB33129664DBDE80664DEFDE3EE868DDF00217A25A1579BB137C1872D768BBE33595166EE1634E47E0AA0BA930B225EA99DFD84614327946B1D3137749ABC7466CD5F513931694B4D1D34793586F5C42ECFB03D8CE41DA19AA93F556900231E862BA0A1C2F, NULL, NULL, NULL, CAST(N'2020-09-26T19:37:57.9497759' AS DateTime2), N'dc108daf-74de-4efc-af26-758fae9962a3', 76, CAST(N'2020-09-26T19:37:57.9496082' AS DateTime2), NULL, NULL, 4, NULL, NULL, NULL, 0, NULL, 12, NULL, 32)
GO
INSERT [Acl].[Users] ([Id], [UserGuid], [Email], [UserName], [LastLoginUtcDate], [PhoneNumber], [IsActive], [Password], [PasswordSalt], [PhotoPath], [ForgotPasswordCode], [ForgotPasswordExpiration], [LastUpdateDate], [RowGuid], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [UserTypeId], [FirstName], [LastName], [VerificationCode], [IsVerified], [VerificationCodeExpiration], [OrganizationId], [UserDeviceId], [BranchId]) VALUES (82, N'8d1f6123-d753-4826-8365-238f6f69ecbf', N'y@dgg.com', N'test', NULL, N'12545454', 1, 0xCD708615E5463A22089A5950FE5999AD6E27F538C2AE86D66BBA360FD5CF72F87CA39CDE5EAA401F8FFDB5E5F7282B79811794CEAA9D90B204C827CAF97AD5EE, 0x11EBD0C63850A361F946856D96D118B9909BD6A1B230AD389145C1B14F6DF06EC41FA29754FABB3D2D6A6FBCE95F31E4325B81817B7ACC2992ABE9DE52893661F1BFD355AD2B309441683D6F90E5A72B17EBFB9E1CFDD2984003FF2958088E14070401A872ACC547CC8F2CFA01D6F58EF10A2F9293C33C1DADA763035A3652B3, NULL, NULL, NULL, CAST(N'2020-09-28T19:31:34.2503353' AS DateTime2), N'bf7506a1-d3c8-43f1-a234-af63389586da', 12, CAST(N'2020-09-28T19:31:34.2471783' AS DateTime2), NULL, NULL, 2, NULL, NULL, NULL, 0, NULL, 8, NULL, NULL)
GO
SET IDENTITY_INSERT [Acl].[Users] OFF
GO
INSERT [Acl].[UserTypes] ([Id], [NameAr], [NameEn]) VALUES (1, N'SuperAdmin', N'SuperAdmin')
GO
INSERT [Acl].[UserTypes] ([Id], [NameAr], [NameEn]) VALUES (2, N'OrganizationAdmin', N'OrganizationAdmin')
GO
INSERT [Acl].[UserTypes] ([Id], [NameAr], [NameEn]) VALUES (3, N'ShiftSupervisor', N'ShiftSupervisor')
GO
INSERT [Acl].[UserTypes] ([Id], [NameAr], [NameEn]) VALUES (4, N'Tailor', N'Tailor')
GO
INSERT [Acl].[UserTypes] ([Id], [NameAr], [NameEn]) VALUES (5, N'Mobile', N'Mobile')
GO
INSERT [qr_sa].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200805014845_create', N'2.2.6-servicing-10079')
GO
INSERT [qr_sa].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200805220344_LatestChanges', N'2.2.6-servicing-10079')
GO
INSERT [qr_sa].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200805220514_LatestChanges2', N'2.2.6-servicing-10079')
GO
INSERT [qr_sa].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200805220835_LatestChanges21', N'2.2.6-servicing-10079')
GO
INSERT [qr_sa].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200805224430_Latest', N'2.2.6-servicing-10079')
GO
INSERT [qr_sa].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200805225528_SetOrgIdNULL', N'2.2.6-servicing-10079')
GO
INSERT [qr_sa].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200806204045_AddUserDevice', N'2.2.6-servicing-10079')
GO
INSERT [qr_sa].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200807203416_editServ', N'2.2.6-servicing-10079')
GO
INSERT [qr_sa].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200808191055_AddInstr', N'2.2.6-servicing-10079')
GO
INSERT [qr_sa].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200813184717_editWindownumbwrNullable', N'2.2.6-servicing-10079')
GO
INSERT [qr_sa].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200814145705_AddBranchIdForSupervisor', N'2.2.6-servicing-10079')
GO
INSERT [qr_sa].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200814190846_AddQuestionTable', N'2.2.6-servicing-10079')
GO
INSERT [qr_sa].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200814191502_editQuestionTable', N'2.2.6-servicing-10079')
GO
INSERT [qr_sa].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200815183830_addPushIdToQueue', N'2.2.6-servicing-10079')
GO
INSERT [qr_sa].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200826212857_AddUserLoginAndAbout', N'2.2.6-servicing-10079')
GO
INSERT [qr_sa].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200826215055_AddUserLoginAndAbout2', N'2.2.6-servicing-10079')
GO
INSERT [qr_sa].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200827160009_QuestionAnswersMig', N'2.2.6-servicing-10079')
GO
INSERT [qr_sa].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200829143644_changeAbout', N'2.2.6-servicing-10079')
GO
INSERT [qr_sa].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200905192221_dateTime', N'2.2.6-servicing-10079')
GO
SET IDENTITY_INSERT [qr_sa].[About] ON 
GO
INSERT [qr_sa].[About] ([Id], [LabelValueAr], [LabelValueEn], [CreateAt], [CreateBy], [DeletedAt], [DeletedBy], [IsDeleted], [UpdateAt], [UpdateBy], [LabelTextAr], [LabelTextEn]) VALUES (1, N'123456', N'رقم الهاتف', CAST(N'2020-08-29T14:37:20.5019587' AS DateTime2), 0, NULL, NULL, 0, NULL, 0, N'phoneNumber', N'123456')
GO
INSERT [qr_sa].[About] ([Id], [LabelValueAr], [LabelValueEn], [CreateAt], [CreateBy], [DeletedAt], [DeletedBy], [IsDeleted], [UpdateAt], [UpdateBy], [LabelTextAr], [LabelTextEn]) VALUES (2, N'012222', N'0122222
', CAST(N'2020-08-29T14:49:17.9376941' AS DateTime2), 0, NULL, NULL, 0, CAST(N'2020-08-29T14:56:30.3869920' AS DateTime2), 12, N'Phone', N'Phone')
GO
INSERT [qr_sa].[About] ([Id], [LabelValueAr], [LabelValueEn], [CreateAt], [CreateBy], [DeletedAt], [DeletedBy], [IsDeleted], [UpdateAt], [UpdateBy], [LabelTextAr], [LabelTextEn]) VALUES (3, N'facebook page link', N'facebook page link', CAST(N'2020-08-29T14:55:55.1015141' AS DateTime2), 12, NULL, NULL, 0, NULL, NULL, N'facebook page', N'facebook page')
GO
SET IDENTITY_INSERT [qr_sa].[About] OFF
GO
SET IDENTITY_INSERT [qr_sa].[Branchs] ON 
GO
INSERT [qr_sa].[Branchs] ([Id], [Code], [OrganizationId], [QrCode], [NameAr], [NameEn], [Phone], [BranchAddress], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (2, 11, 1, NULL, N'الدور الأول/ شؤؤون الأفراد وقضايا الاسرة', N'الدور الأول/ شؤؤون الأفراد وقضايا الاسرة', N'966563219431', N'adresss', N'cxxc', 11, CAST(N'2020-08-05T23:36:00' AS SmallDateTime), 14, CAST(N'2020-08-08T01:56:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Branchs] ([Id], [Code], [OrganizationId], [QrCode], [NameAr], [NameEn], [Phone], [BranchAddress], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (3, 100, 1, N'7fa29d50-d951-498c-b085-2446e604c119', N'الدور الأرضي/ شؤؤون الأفراد وقضايا الاسرة', N'الدور الأرضي/ شؤؤون الأفراد وقضايا الاسرة', N'966563219431', NULL, NULL, 11, CAST(N'2020-08-05T23:42:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Branchs] ([Id], [Code], [OrganizationId], [QrCode], [NameAr], [NameEn], [Phone], [BranchAddress], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (9, 111, 5, N'11', N'الدور الثاني / خدمات وسترن', N'الدور الثاني / خدمات وسترن', N'87666666666666', NULL, NULL, 16, CAST(N'2020-08-03T23:33:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Branchs] ([Id], [Code], [OrganizationId], [QrCode], [NameAr], [NameEn], [Phone], [BranchAddress], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (10, 112, 5, N'11', N'فرع محي الدين', N'فرع محي الدين', N'87666666666666', NULL, NULL, 16, CAST(N'2020-08-03T23:33:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Branchs] ([Id], [Code], [OrganizationId], [QrCode], [NameAr], [NameEn], [Phone], [BranchAddress], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (18, 113, 5, NULL, N'new branch', N'new branch', N'0121212', N'adresss', N'notes
', 17, CAST(N'2020-08-08T19:08:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Branchs] ([Id], [Code], [OrganizationId], [QrCode], [NameAr], [NameEn], [Phone], [BranchAddress], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (23, 114, 5, NULL, N'Cairo branch', N'Cairo branch', N'012122222', N'Cairooooo', N'Cairo branch noess', 17, CAST(N'2020-08-08T20:23:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Branchs] ([Id], [Code], [OrganizationId], [QrCode], [NameAr], [NameEn], [Phone], [BranchAddress], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (24, 115, 5, NULL, N'Alex NadyEsid', N'Alex NadyEsid', N'01333131', N'Alex ', N'Alex branch notes', 17, CAST(N'2020-08-08T20:24:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Branchs] ([Id], [Code], [OrganizationId], [QrCode], [NameAr], [NameEn], [Phone], [BranchAddress], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (25, 115, 5, NULL, N'Alex Tahrer', N'Alex Tahrer', N'01333131', N'Alex ', N'Alex branch notes', 17, CAST(N'2020-08-08T20:24:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Branchs] ([Id], [Code], [OrganizationId], [QrCode], [NameAr], [NameEn], [Phone], [BranchAddress], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (26, 116, 8, NULL, N'Kadaa2 Cairo', N'Kadaa2 Cairo', N'012000', N'Cairo', NULL, 48, CAST(N'2020-08-14T14:30:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Branchs] ([Id], [Code], [OrganizationId], [QrCode], [NameAr], [NameEn], [Phone], [BranchAddress], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (27, 117, 8, NULL, N'Kadaa2 Alex', N'Kadaa2 Alex', N'012222', N'012222', N'Kadaa2 Alex', 48, CAST(N'2020-08-14T14:31:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Branchs] ([Id], [Code], [OrganizationId], [QrCode], [NameAr], [NameEn], [Phone], [BranchAddress], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (28, 118, 9, N'7fa29d50-d951-498c-b085-2446e604c118', N'El shai5 zayed', N'El shai5 zayed', N'01222236445', N'El shai5 zayed', N'El shai5 zayed', 57, CAST(N'2020-08-19T20:38:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Branchs] ([Id], [Code], [OrganizationId], [QrCode], [NameAr], [NameEn], [Phone], [BranchAddress], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (29, 119, 9, NULL, N'Naser City', N'Naser City', N'128454545', N'Naser City', NULL, 57, CAST(N'2020-08-19T20:45:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Branchs] ([Id], [Code], [OrganizationId], [QrCode], [NameAr], [NameEn], [Phone], [BranchAddress], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (30, 120, 11, N'771bd295-c1f6-42e2-98cc-6d40441f88ca', N'El masl7a part 1', N'El masl7a part 1', N'01212555', N'El masl7a part 1 adress', NULL, 63, CAST(N'2020-08-25T18:28:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Branchs] ([Id], [Code], [OrganizationId], [QrCode], [NameAr], [NameEn], [Phone], [BranchAddress], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (31, 121, 11, N'f722123d-3467-41fc-8896-197dc480199d', N'El masl7a part 2', N'El masl7a part 2', N'0121215555', N'El masl7a part 2 address', NULL, 63, CAST(N'2020-08-25T18:28:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Branchs] ([Id], [Code], [OrganizationId], [QrCode], [NameAr], [NameEn], [Phone], [BranchAddress], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (32, 122, 12, N'36fa3206-02e0-4ff3-90a4-062609f29b69', N'QR Cairo', N'QR Cairo', N'0211544545', N'Cairo', NULL, 76, CAST(N'2020-09-26T18:36:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Branchs] ([Id], [Code], [OrganizationId], [QrCode], [NameAr], [NameEn], [Phone], [BranchAddress], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (33, 123, 12, N'9c843da7-551e-46e8-bcd6-48244869313a', N'QR Alex', N'QR Alex', N'02154554545', N'Alex', NULL, 76, CAST(N'2020-09-26T18:36:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
SET IDENTITY_INSERT [qr_sa].[Branchs] OFF
GO
SET IDENTITY_INSERT [qr_sa].[BranchServices] ON 
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (12, 24, 5)
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (13, 24, 10)
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (14, 24, 11)
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (15, 25, 5)
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (16, 25, 6)
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (17, 25, 10)
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (18, 25, 11)
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (35, 3, 5)
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (36, 3, 10)
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (48, 26, 16)
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (49, 26, 13)
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (50, 26, 14)
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (54, 27, 15)
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (55, 27, 16)
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (57, 29, 17)
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (58, 29, 18)
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (59, 28, 18)
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (60, 28, 17)
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (61, 30, 19)
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (62, 30, 20)
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (63, 32, 21)
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (64, 32, 22)
GO
INSERT [qr_sa].[BranchServices] ([Id], [BranchId], [ServiceId]) VALUES (65, 33, 21)
GO
SET IDENTITY_INSERT [qr_sa].[BranchServices] OFF
GO
SET IDENTITY_INSERT [qr_sa].[EvaluationQuestionAnswer] ON 
GO
INSERT [qr_sa].[EvaluationQuestionAnswer] ([Id], [EvaluationId], [QuestionId], [AnswerValue]) VALUES (4, 38, 8, 3)
GO
INSERT [qr_sa].[EvaluationQuestionAnswer] ([Id], [EvaluationId], [QuestionId], [AnswerValue]) VALUES (5, 38, 9, 3)
GO
INSERT [qr_sa].[EvaluationQuestionAnswer] ([Id], [EvaluationId], [QuestionId], [AnswerValue]) VALUES (6, 51, 8, 3)
GO
INSERT [qr_sa].[EvaluationQuestionAnswer] ([Id], [EvaluationId], [QuestionId], [AnswerValue]) VALUES (7, 51, 9, 1)
GO
INSERT [qr_sa].[EvaluationQuestionAnswer] ([Id], [EvaluationId], [QuestionId], [AnswerValue]) VALUES (8, 52, 8, 1)
GO
INSERT [qr_sa].[EvaluationQuestionAnswer] ([Id], [EvaluationId], [QuestionId], [AnswerValue]) VALUES (9, 52, 9, 1)
GO
INSERT [qr_sa].[EvaluationQuestionAnswer] ([Id], [EvaluationId], [QuestionId], [AnswerValue]) VALUES (10, 55, 10, 1)
GO
INSERT [qr_sa].[EvaluationQuestionAnswer] ([Id], [EvaluationId], [QuestionId], [AnswerValue]) VALUES (11, 55, 11, 3)
GO
SET IDENTITY_INSERT [qr_sa].[EvaluationQuestionAnswer] OFF
GO
SET IDENTITY_INSERT [qr_sa].[Evaluations] ON 
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (35, NULL, 57, CAST(N'2020-08-25T22:54:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (36, NULL, 58, CAST(N'2020-08-25T22:54:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (37, NULL, 60, CAST(N'2020-08-26T19:42:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (38, N'الدنبا زي الفل ', 61, CAST(N'2020-08-26T21:36:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (39, NULL, 62, CAST(N'2020-08-26T23:44:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (40, NULL, 63, CAST(N'2020-08-26T23:47:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (41, NULL, 64, CAST(N'2020-08-26T23:52:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (42, NULL, 65, CAST(N'2020-08-26T23:56:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (43, NULL, 66, CAST(N'2020-08-27T00:04:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (44, NULL, 67, CAST(N'2020-08-27T00:12:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (45, NULL, 68, CAST(N'2020-08-27T00:21:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (46, NULL, 69, CAST(N'2020-08-27T00:23:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (47, NULL, 70, CAST(N'2020-08-27T00:27:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (48, NULL, 71, CAST(N'2020-08-27T16:39:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (49, NULL, 72, CAST(N'2020-08-27T16:43:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (50, NULL, 73, CAST(N'2020-08-27T20:08:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (51, N'test comment test', 74, CAST(N'2020-08-27T20:09:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (52, N'jfjskxjxbkdosodjdjxjsiwlle', 75, CAST(N'2020-08-27T20:34:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (53, N'kolo 7lo', 79, CAST(N'2020-09-26T19:18:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (54, NULL, 80, CAST(N'2020-09-26T19:28:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (55, N'good', 81, CAST(N'2020-09-26T19:30:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (56, NULL, 82, CAST(N'2020-09-26T19:55:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (57, NULL, 83, CAST(N'2020-09-26T19:57:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (58, NULL, 84, CAST(N'2020-10-02T10:09:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (59, NULL, 85, CAST(N'2020-10-02T10:10:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (60, NULL, 86, CAST(N'2020-10-02T10:17:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (61, NULL, 87, CAST(N'2020-10-02T10:22:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (62, NULL, 88, CAST(N'2020-10-02T10:27:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (63, NULL, 89, CAST(N'2020-10-02T10:27:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (64, NULL, 90, CAST(N'2020-10-02T10:36:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (65, NULL, 91, CAST(N'2020-10-02T10:42:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Evaluations] ([Id], [Comment], [ShiftQueueId], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (66, NULL, 92, CAST(N'2020-10-03T11:21:00' AS SmallDateTime), NULL, NULL, 0)
GO
SET IDENTITY_INSERT [qr_sa].[Evaluations] OFF
GO
SET IDENTITY_INSERT [qr_sa].[HelpAndSupport] ON 
GO
INSERT [qr_sa].[HelpAndSupport] ([Id], [MessageTitle], [Message], [UserId], [CreateAt], [CreateBy], [DeletedAt], [DeletedBy], [IsDeleted], [UpdateAt], [UpdateBy]) VALUES (1, N'ndndnckxkd f', N'fjfnfnfjviifjdndnf', 15, CAST(N'2020-08-29T17:56:36.0901797' AS DateTime2), 15, CAST(N'2020-08-29T19:52:29.1078627' AS DateTime2), 12, 1, NULL, NULL)
GO
INSERT [qr_sa].[HelpAndSupport] ([Id], [MessageTitle], [Message], [UserId], [CreateAt], [CreateBy], [DeletedAt], [DeletedBy], [IsDeleted], [UpdateAt], [UpdateBy]) VALUES (2, N'Test Message', N'test message body .......', 63, CAST(N'2020-08-29T19:33:53.8223905' AS DateTime2), 63, CAST(N'2020-08-29T21:22:31.3751061' AS DateTime2), 12, 1, NULL, NULL)
GO
INSERT [qr_sa].[HelpAndSupport] ([Id], [MessageTitle], [Message], [UserId], [CreateAt], [CreateBy], [DeletedAt], [DeletedBy], [IsDeleted], [UpdateAt], [UpdateBy]) VALUES (3, N'message 2', N'Message 2 body .....', 63, CAST(N'2020-08-29T19:37:12.7612408' AS DateTime2), 63, NULL, NULL, 0, NULL, NULL)
GO
INSERT [qr_sa].[HelpAndSupport] ([Id], [MessageTitle], [Message], [UserId], [CreateAt], [CreateBy], [DeletedAt], [DeletedBy], [IsDeleted], [UpdateAt], [UpdateBy]) VALUES (4, N'Ttttt', N'000000', 33, CAST(N'2020-08-30T21:42:49.1760021' AS DateTime2), 33, NULL, NULL, 0, NULL, NULL)
GO
INSERT [qr_sa].[HelpAndSupport] ([Id], [MessageTitle], [Message], [UserId], [CreateAt], [CreateBy], [DeletedAt], [DeletedBy], [IsDeleted], [UpdateAt], [UpdateBy]) VALUES (5, N'Kjjkjk', NULL, 33, CAST(N'2020-08-30T21:44:25.1906262' AS DateTime2), 33, NULL, NULL, 0, NULL, NULL)
GO
INSERT [qr_sa].[HelpAndSupport] ([Id], [MessageTitle], [Message], [UserId], [CreateAt], [CreateBy], [DeletedAt], [DeletedBy], [IsDeleted], [UpdateAt], [UpdateBy]) VALUES (6, N'Exec', N'Xsxsx', 33, CAST(N'2020-08-30T21:51:01.2396148' AS DateTime2), 33, NULL, NULL, 0, NULL, NULL)
GO
INSERT [qr_sa].[HelpAndSupport] ([Id], [MessageTitle], [Message], [UserId], [CreateAt], [CreateBy], [DeletedAt], [DeletedBy], [IsDeleted], [UpdateAt], [UpdateBy]) VALUES (7, N'Help me ', N'Help me  Message body', 77, CAST(N'2020-09-26T18:58:45.7979609' AS DateTime2), 77, NULL, NULL, 0, NULL, NULL)
GO
SET IDENTITY_INSERT [qr_sa].[HelpAndSupport] OFF
GO
SET IDENTITY_INSERT [qr_sa].[Instructions] ON 
GO
INSERT [qr_sa].[Instructions] ([Id], [Code], [OrganizationId], [NameAr], [NameEn], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (1, 1, 5, N'Elbs el kmama ya 3mm', N'Elbs el kmama ya 3mm', 17, CAST(N'2020-08-08T21:26:00' AS SmallDateTime), 17, CAST(N'2020-08-08T21:47:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Instructions] ([Id], [Code], [OrganizationId], [NameAr], [NameEn], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (2, 2, 5, N'esm3 el klam w elbs el kmama', N'esm3 el klam w elbs el kmama', 17, CAST(N'2020-08-08T21:27:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Instructions] ([Id], [Code], [OrganizationId], [NameAr], [NameEn], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (3, 3, 5, N'brglak el ymeen yaba', N'brglak el ymeen yaba', 17, CAST(N'2020-08-08T21:53:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Instructions] ([Id], [Code], [OrganizationId], [NameAr], [NameEn], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (7, 4, 11, N'Announcement one for el masl7a  ', N'Announcement one for el masl7a  ', 63, CAST(N'2020-08-25T19:26:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Instructions] ([Id], [Code], [OrganizationId], [NameAr], [NameEn], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (8, 5, 11, N'Announcement twofor el masl7a  ', N'Announcement twofor el masl7a  ', 63, CAST(N'2020-08-25T19:26:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
SET IDENTITY_INSERT [qr_sa].[Instructions] OFF
GO
SET IDENTITY_INSERT [qr_sa].[LoginProviders] ON 
GO
INSERT [qr_sa].[LoginProviders] ([Id], [ProviderType], [Providertoken], [UserId]) VALUES (7, 1, N'108786463338802374612', 73)
GO
INSERT [qr_sa].[LoginProviders] ([Id], [ProviderType], [Providertoken], [UserId]) VALUES (9, 2, N'10218325505790595', 75)
GO
INSERT [qr_sa].[LoginProviders] ([Id], [ProviderType], [Providertoken], [UserId]) VALUES (10, 1, N'100963420074471060281', 80)
GO
SET IDENTITY_INSERT [qr_sa].[LoginProviders] OFF
GO
SET IDENTITY_INSERT [qr_sa].[Organization] ON 
GO
INSERT [qr_sa].[Organization] ([Id], [Code], [NameAr], [NameEn], [Phone], [Address], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (1, 1, N'محكمة قضايا المنزاعات بجدة', N'محكمة قضايا المنزاعات بجدة', N'966563219431', N'SaudiArabia,Geddah', NULL, 11, CAST(N'2020-08-05T23:25:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Organization] ([Id], [Code], [NameAr], [NameEn], [Phone], [Address], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (5, 1, N'بنك الاسكندرية', N'بنك الكس', N'887', N'egypt', NULL, 11, CAST(N'2020-08-05T23:25:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Organization] ([Id], [Code], [NameAr], [NameEn], [Phone], [Address], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (6, 2, N'CIB', N'CIB', N'01212121', N'CIB elly hnak updated', N'CIB not here
 updated', 14, CAST(N'2020-08-08T00:06:00' AS SmallDateTime), 14, CAST(N'2020-08-08T00:09:00' AS SmallDateTime), NULL, NULL, 0)
GO
INSERT [qr_sa].[Organization] ([Id], [Code], [NameAr], [NameEn], [Phone], [Address], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (7, 3, N'QNB', N'QNB', N'12121', N'3la awl el share3', N'QNB bank', 14, CAST(N'2020-08-08T00:08:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Organization] ([Id], [Code], [NameAr], [NameEn], [Phone], [Address], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (8, 4, N'Dar El kadaa2', N'Dar El kadaa2', N'01222222', N'Dar El kadaa2 address', N'', 12, CAST(N'2020-08-14T14:27:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Organization] ([Id], [Code], [NameAr], [NameEn], [Phone], [Address], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (9, 5, N'S3ody Markt', N'S3ody Markt', N'012222222', N'S3ody Markt', N'S3ody Markt', 12, CAST(N'2020-08-19T20:26:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Organization] ([Id], [Code], [NameAr], [NameEn], [Phone], [Address], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (10, 6, N'Hyper Market', N'Hyper Market', N'012222', N'Hyper Market', N'Hyper Market', 12, CAST(N'2020-08-19T20:27:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Organization] ([Id], [Code], [NameAr], [NameEn], [Phone], [Address], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (11, 7, N'El masl7a', N'El masl7a', N'01232644444', N'El masl7a adress', NULL, 12, CAST(N'2020-08-25T18:25:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
INSERT [qr_sa].[Organization] ([Id], [Code], [NameAr], [NameEn], [Phone], [Address], [Note], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES (12, 8, N'QReduction ', N'QReduction ', N'02121222', N'Cairo', NULL, 12, CAST(N'2020-09-26T18:28:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 0)
GO
SET IDENTITY_INSERT [qr_sa].[Organization] OFF
GO
SET IDENTITY_INSERT [qr_sa].[Questions] ON 
GO
INSERT [qr_sa].[Questions] ([Id], [QuestionTextAr], [QuestionTextEn], [OrganizationId], [Code], [CreateBy], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted], [UpdateBy], [UpdateAt]) VALUES (1, N'ques 11 q223 ques 11 q223  ques 1 q223 ques 1 q223 AR 333', N'ques 11 q223 ques 11 q223  ques 1 q223 ques 1 q223 En 44', 5, 1234, 17, CAST(N'2020-08-14T19:17:00' AS SmallDateTime), NULL, NULL, 0, 17, CAST(N'2020-08-14T19:28:00' AS SmallDateTime))
GO
INSERT [qr_sa].[Questions] ([Id], [QuestionTextAr], [QuestionTextEn], [OrganizationId], [Code], [CreateBy], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted], [UpdateBy], [UpdateAt]) VALUES (2, N'ques 1 q223 ques 1 q223  ques 1 q223 ques 1 q223 AR 333', N'ques 1 q223 ques 1 q223  ques 1 q223 ques 1 q223 En 44', 5, 12345, 17, CAST(N'2020-08-14T19:20:00' AS SmallDateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [qr_sa].[Questions] ([Id], [QuestionTextAr], [QuestionTextEn], [OrganizationId], [Code], [CreateBy], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted], [UpdateBy], [UpdateAt]) VALUES (4, N'ques 1 q223 ques 1 q223  ques 1 q223 ques 1 q223 AR 333', N'ques 1 q223 ques 1 q223  ques 1 q223 ques 1 q223 En 44', 1, 11, 17, CAST(N'2020-08-14T19:20:00' AS SmallDateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [qr_sa].[Questions] ([Id], [QuestionTextAr], [QuestionTextEn], [OrganizationId], [Code], [CreateBy], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted], [UpdateBy], [UpdateAt]) VALUES (5, N'eh elly gabak hna ya mlwany ?????', N'eh elly gabak hna ya mlwany ????', 8, 12346, 51, CAST(N'2020-08-14T23:33:00' AS SmallDateTime), NULL, NULL, 0, 51, CAST(N'2020-08-14T23:37:00' AS SmallDateTime))
GO
INSERT [qr_sa].[Questions] ([Id], [QuestionTextAr], [QuestionTextEn], [OrganizationId], [Code], [CreateBy], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted], [UpdateBy], [UpdateAt]) VALUES (6, N'eh elly gabak hna tany ya mlwany ?', N'eh elly gabak hna tany ya mlwany ?', 8, 12347, 51, CAST(N'2020-08-14T23:34:00' AS SmallDateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [qr_sa].[Questions] ([Id], [QuestionTextAr], [QuestionTextEn], [OrganizationId], [Code], [CreateBy], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted], [UpdateBy], [UpdateAt]) VALUES (7, N'What''s your opinion?', N'What''s your opinion?', 9, 12348, 57, CAST(N'2020-08-19T21:00:00' AS SmallDateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [qr_sa].[Questions] ([Id], [QuestionTextAr], [QuestionTextEn], [OrganizationId], [Code], [CreateBy], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted], [UpdateBy], [UpdateAt]) VALUES (8, N'Eh elly gabak el masl7a yabny?', N'Eh elly gabak el masl7a yabny?', 11, 12349, 63, CAST(N'2020-08-25T18:38:00' AS SmallDateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [qr_sa].[Questions] ([Id], [QuestionTextAr], [QuestionTextEn], [OrganizationId], [Code], [CreateBy], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted], [UpdateBy], [UpdateAt]) VALUES (9, N'eh r2yak ?', N'eh r2yak ?', 11, 12350, 63, CAST(N'2020-08-25T18:38:00' AS SmallDateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [qr_sa].[Questions] ([Id], [QuestionTextAr], [QuestionTextEn], [OrganizationId], [Code], [CreateBy], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted], [UpdateBy], [UpdateAt]) VALUES (10, N'What''s your opinion???', N'What''s your opinion???', 12, 12351, 76, CAST(N'2020-09-26T19:19:00' AS SmallDateTime), NULL, NULL, 0, NULL, NULL)
GO
INSERT [qr_sa].[Questions] ([Id], [QuestionTextAr], [QuestionTextEn], [OrganizationId], [Code], [CreateBy], [CreateAt], [DeletedAt], [DeletedBy], [IsDeleted], [UpdateBy], [UpdateAt]) VALUES (11, N'What''s your eval??', N'What''s your eval??', 12, 12352, 76, CAST(N'2020-09-26T19:19:00' AS SmallDateTime), NULL, NULL, 0, NULL, NULL)
GO
SET IDENTITY_INSERT [qr_sa].[Questions] OFF
GO
SET IDENTITY_INSERT [qr_sa].[Services] ON 
GO
INSERT [qr_sa].[Services] ([Id], [Code], [NameAr], [NameEn], [Note], [IsDeleted], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [OrganizationId], [NoteEN]) VALUES (1, 100, N'فض المنازعات', N'فض المنازعات', NULL, 0, 11, CAST(N'2020-08-05T23:56:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 1, NULL)
GO
INSERT [qr_sa].[Services] ([Id], [Code], [NameAr], [NameEn], [Note], [IsDeleted], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [OrganizationId], [NoteEN]) VALUES (2, 102, N'قضايا الشركات', N'قضايا الشركات', NULL, 0, 11, CAST(N'2020-08-05T23:57:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 1, NULL)
GO
INSERT [qr_sa].[Services] ([Id], [Code], [NameAr], [NameEn], [Note], [IsDeleted], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [OrganizationId], [NoteEN]) VALUES (3, 103, N'قضايا الأسره', N'قضايا الأسره', NULL, 0, 11, CAST(N'2020-08-05T23:57:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 1, NULL)
GO
INSERT [qr_sa].[Services] ([Id], [Code], [NameAr], [NameEn], [Note], [IsDeleted], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [OrganizationId], [NoteEN]) VALUES (5, 111, N'وستر يونيون', N'وستر يونيون', N'updated again', 0, 16, CAST(N'2020-08-05T23:57:00' AS SmallDateTime), 17, CAST(N'2020-08-08T22:27:00' AS SmallDateTime), NULL, NULL, 5, NULL)
GO
INSERT [qr_sa].[Services] ([Id], [Code], [NameAr], [NameEn], [Note], [IsDeleted], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [OrganizationId], [NoteEN]) VALUES (6, 112, N'بطاقات الائئتمان', N'بطاقات الائئتمان', NULL, 0, 16, CAST(N'2020-08-05T23:57:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 5, NULL)
GO
INSERT [qr_sa].[Services] ([Id], [Code], [NameAr], [NameEn], [Note], [IsDeleted], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [OrganizationId], [NoteEN]) VALUES (10, 113, N'سحب', N'سحب', NULL, 0, 16, CAST(N'2020-08-05T23:57:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 5, NULL)
GO
INSERT [qr_sa].[Services] ([Id], [Code], [NameAr], [NameEn], [Note], [IsDeleted], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [OrganizationId], [NoteEN]) VALUES (11, 114, N'ايداع', N'ايداع', NULL, 0, 16, CAST(N'2020-08-05T23:57:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 5, NULL)
GO
INSERT [qr_sa].[Services] ([Id], [Code], [NameAr], [NameEn], [Note], [IsDeleted], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [OrganizationId], [NoteEN]) VALUES (13, 115, N'service 1', N'service 1', N'service 1
', 0, 51, CAST(N'2020-08-18T20:01:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 8, NULL)
GO
INSERT [qr_sa].[Services] ([Id], [Code], [NameAr], [NameEn], [Note], [IsDeleted], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [OrganizationId], [NoteEN]) VALUES (14, 116, N'Service 2', N'Service 2', N'Service 2
', 0, 51, CAST(N'2020-08-18T21:34:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 8, NULL)
GO
INSERT [qr_sa].[Services] ([Id], [Code], [NameAr], [NameEn], [Note], [IsDeleted], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [OrganizationId], [NoteEN]) VALUES (15, 117, N'Service 3', N'Service 3', NULL, 0, 51, CAST(N'2020-08-18T21:34:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 8, NULL)
GO
INSERT [qr_sa].[Services] ([Id], [Code], [NameAr], [NameEn], [Note], [IsDeleted], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [OrganizationId], [NoteEN]) VALUES (16, 118, N'Service 4', N'Service 4', NULL, 0, 51, CAST(N'2020-08-18T21:34:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 8, NULL)
GO
INSERT [qr_sa].[Services] ([Id], [Code], [NameAr], [NameEn], [Note], [IsDeleted], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [OrganizationId], [NoteEN]) VALUES (17, 119, N'خدمات السحب والإيداع', N'Cach Managment', NULL, 0, 57, CAST(N'2020-08-19T20:42:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 9, NULL)
GO
INSERT [qr_sa].[Services] ([Id], [Code], [NameAr], [NameEn], [Note], [IsDeleted], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [OrganizationId], [NoteEN]) VALUES (18, 120, N'المبيعات', N'Sales', NULL, 0, 57, CAST(N'2020-08-19T20:43:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 9, NULL)
GO
INSERT [qr_sa].[Services] ([Id], [Code], [NameAr], [NameEn], [Note], [IsDeleted], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [OrganizationId], [NoteEN]) VALUES (19, 121, N'Sales Support', N'Sales Support', NULL, 0, 63, CAST(N'2020-08-25T18:29:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 11, NULL)
GO
INSERT [qr_sa].[Services] ([Id], [Code], [NameAr], [NameEn], [Note], [IsDeleted], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [OrganizationId], [NoteEN]) VALUES (20, 122, N'Customer Support', N'Customer Support', NULL, 0, 63, CAST(N'2020-08-25T18:29:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 11, NULL)
GO
INSERT [qr_sa].[Services] ([Id], [Code], [NameAr], [NameEn], [Note], [IsDeleted], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [OrganizationId], [NoteEN]) VALUES (21, 123, N'QR Sales ', N'QR Sales ', NULL, 0, 76, CAST(N'2020-09-26T18:40:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 12, NULL)
GO
INSERT [qr_sa].[Services] ([Id], [Code], [NameAr], [NameEn], [Note], [IsDeleted], [CreateBy], [CreateAt], [UpdateBy], [UpdateAt], [DeletedAt], [DeletedBy], [OrganizationId], [NoteEN]) VALUES (22, 124, N'QR Customer support', N'QR Customer support', NULL, 0, 76, CAST(N'2020-09-26T18:40:00' AS SmallDateTime), NULL, NULL, NULL, NULL, 12, NULL)
GO
SET IDENTITY_INSERT [qr_sa].[Services] OFF
GO
SET IDENTITY_INSERT [qr_sa].[ShiftQueues] ON 
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (57, 1, 16, 1, 22, 64, 19, N'1', N'-MFbo_DnHaN4aeONSfQJ', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (58, 2, 16, 1, 22, 64, 19, N'1', N'-MFbokPU0vIu5R4jlXL1', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (59, 3, 16, 1, 22, 65, 19, N'2', N'-MFbpGSMtyNlhDLJom3C', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (60, 4, 16, 1, 22, 64, 19, N'1', N'-MFbpYS0dcAQdA_8MO7S', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (61, 5, 16, 1, 15, 64, 19, N'1', N'-MFgg7F6mP3WY8gUCBgQ', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (62, 6, 16, 1, 15, 64, 19, N'1', N'-MFh8iJU9TEX64GDRnyU', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (63, 7, 16, 1, 15, 64, 19, N'1', N'-MFh9aup1eBB4bj3dx9P', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (64, 8, 16, 1, 15, 64, 19, N'1', N'-MFhAZ2e9P4rvxaDg7gQ', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (65, 9, 16, 1, 15, 64, 19, N'1', N'-MFhBqLAUGeD1vztdbEy', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (66, 10, 16, 1, 15, 64, 19, N'1', N'-MFhDWdzhkRW1QYKc8nE', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (67, 11, 16, 1, 15, 64, 19, N'1', N'-MFhFHxfRTFx25Fzs6Pf', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (68, 12, 16, 1, 15, 64, 19, N'1', N'-MFhHMbI2srd-lE17t95', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (69, 13, 16, 1, 15, 64, 19, N'1', N'-MFhI-wL7xxBOmLgVflD', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (70, 14, 16, 1, 15, 64, 19, N'1', N'-MFhIk9lYFcOicQ_BAKs', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (71, 15, 16, 1, 22, 64, 19, N'1', N'-MFkm6O4nu5qFhZ-Hpjn', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (72, 16, 16, 1, 22, 64, 19, N'1', N'-MFkmdJcER4i5Q_zmoPh', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (73, 17, 16, 1, 22, 64, 19, N'1', N'-MFkosbzkYtZ9kkjxCiN', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (74, 18, 16, 1, 15, 64, 19, N'1', N'-MFlXBrwXTS0l4P1SZeJ', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (75, 19, 16, 1, 15, 64, 19, N'1', N'-MFlc5N7eKqsbVKnzHB7', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (76, 20, 16, 0, 15, NULL, 19, NULL, N'-MHanjiSFVayTLjl7I2n', CAST(N'2020-09-19T07:40:16.8695483' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (77, 21, 16, 0, 15, NULL, 19, NULL, N'-MHh2e4Rb0ILn5zwcZ_W', CAST(N'2020-09-20T12:47:31.3626315' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (78, 22, 16, 0, 15, NULL, 19, NULL, N'-MHh2oBzGtwYhRs-3mW2', CAST(N'2020-09-20T12:48:12.7609660' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (79, 1, 19, 1, 80, 78, 21, N'5', N'-MIApT5ewKVt9cXz-Xw2', CAST(N'2020-09-26T12:13:45.9713813' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (80, 2, 19, 1, 80, 78, 21, N'5', N'-MIAr2DGh1y9WQCnEckU', CAST(N'2020-09-26T12:20:40.1091872' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (81, 3, 19, 1, 80, 78, 21, N'5', N'-MIAsvIRT5B_EhLrnEhJ', CAST(N'2020-09-26T12:28:51.9597071' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (82, 4, 19, 1, 80, 79, 21, N'2', N'-MIAwYu8Fv6ttwfpt7cO', CAST(N'2020-09-26T12:44:44.7097081' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (83, 1, 19, 1, 80, 81, 22, N'6', N'-MIAyPBG9MQN6rr_enj6', CAST(N'2020-09-26T12:52:49.1974685' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (84, 1, 17, 1, 15, 64, 19, N'5', N'-MIckugjyn-iF8tMVTux', CAST(N'2020-10-02T03:02:51.6444214' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (85, 2, 17, 1, 75, 64, 19, N'5', N'-MIckvBy39t4bdWWlTIq', CAST(N'2020-10-02T03:02:53.6569843' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (86, 3, 17, 1, 15, 64, 19, N'5', N'-MIclRXyTO6jaSSeR-9f', CAST(N'2020-10-02T03:05:10.2331390' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (87, 4, 17, 1, 15, 64, 19, N'5', N'-MIclV4ClkySSFanLODR', CAST(N'2020-10-02T03:05:24.7126984' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (88, 5, 17, 1, 15, 64, 19, N'5', N'-MIcldRa7RgN55c_jYPN', CAST(N'2020-10-02T03:06:03.0735257' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (89, 6, 17, 1, 75, 64, 19, N'5', N'-MIcqLINVDUGQteI1Cc9', CAST(N'2020-10-02T03:26:35.3798016' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (90, 7, 17, 1, 15, 64, 19, N'5', N'-MIcqkcEeqe-BCaqJPEO', CAST(N'2020-10-02T03:28:23.2112252' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (91, 8, 17, 1, 15, 64, 19, N'5', N'-MIcsL25iGe56G8XTnui', CAST(N'2020-10-02T03:35:18.6256480' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (92, 9, 17, 1, 15, 64, 19, N'5', N'-MIctqnJTlDhfo25oivX', CAST(N'2020-10-02T03:41:54.9273239' AS DateTime2))
GO
INSERT [qr_sa].[ShiftQueues] ([Id], [UserTurn], [ShiftId], [IsServiceDone], [UserIdMobile], [UserIdBy], [ServiceId], [WindowNumber], [PushId], [CreatedDate]) VALUES (95, 12, 17, 0, 75, 64, 19, N'5', N'-MIiBKNTHIKZ1GgvjLI5', CAST(N'2020-10-03T04:20:44.8727891' AS DateTime2))
GO
SET IDENTITY_INSERT [qr_sa].[ShiftQueues] OFF
GO
SET IDENTITY_INSERT [qr_sa].[Shifts] ON 
GO
INSERT [qr_sa].[Shifts] ([Id], [Code], [StartAt], [EndAt], [BranchId], [IsEnded], [QRCode], [UserIdSupport], [UpdateAt], [CreateBy], [CreateAt], [UpdateBy]) VALUES (9, 222, CAST(N'2020-08-08T00:00:00' AS SmallDateTime), NULL, 24, 0, N'e506a251-b6d3-4ef1-b096-eafcc9656621', NULL, NULL, 53, CAST(N'2020-08-08T00:00:00' AS SmallDateTime), NULL)
GO
INSERT [qr_sa].[Shifts] ([Id], [Code], [StartAt], [EndAt], [BranchId], [IsEnded], [QRCode], [UserIdSupport], [UpdateAt], [CreateBy], [CreateAt], [UpdateBy]) VALUES (11, 223, CAST(N'2020-08-14T17:10:00' AS SmallDateTime), CAST(N'2020-08-14T18:12:00' AS SmallDateTime), 27, 1, N'e506a251-b6d3-4ef1-b096-eafcc9656621', NULL, CAST(N'2020-08-14T18:12:00' AS SmallDateTime), 53, CAST(N'2020-08-14T17:10:00' AS SmallDateTime), 53)
GO
INSERT [qr_sa].[Shifts] ([Id], [Code], [StartAt], [EndAt], [BranchId], [IsEnded], [QRCode], [UserIdSupport], [UpdateAt], [CreateBy], [CreateAt], [UpdateBy]) VALUES (12, 224, CAST(N'2020-08-14T18:13:00' AS SmallDateTime), CAST(N'2020-08-14T22:16:00' AS SmallDateTime), 27, 1, N'50bdc822-bd40-4374-8f8d-0cf1c44db2e0', NULL, CAST(N'2020-08-14T22:16:00' AS SmallDateTime), 53, CAST(N'2020-08-14T18:13:00' AS SmallDateTime), 53)
GO
INSERT [qr_sa].[Shifts] ([Id], [Code], [StartAt], [EndAt], [BranchId], [IsEnded], [QRCode], [UserIdSupport], [UpdateAt], [CreateBy], [CreateAt], [UpdateBy]) VALUES (13, 225, CAST(N'2020-08-14T22:17:00' AS SmallDateTime), NULL, 27, 0, N'3d3f9a0b-76dd-47e1-91fe-a0c98b36695c', NULL, NULL, 53, CAST(N'2020-08-14T22:17:00' AS SmallDateTime), NULL)
GO
INSERT [qr_sa].[Shifts] ([Id], [Code], [StartAt], [EndAt], [BranchId], [IsEnded], [QRCode], [UserIdSupport], [UpdateAt], [CreateBy], [CreateAt], [UpdateBy]) VALUES (14, 226, CAST(N'2020-08-19T21:06:00' AS SmallDateTime), CAST(N'2020-08-19T21:07:00' AS SmallDateTime), 28, 1, N'55b3a9f1-bd57-4253-966a-1fff8cf8f0fc', NULL, CAST(N'2020-08-19T21:07:00' AS SmallDateTime), 59, CAST(N'2020-08-19T21:06:00' AS SmallDateTime), 59)
GO
INSERT [qr_sa].[Shifts] ([Id], [Code], [StartAt], [EndAt], [BranchId], [IsEnded], [QRCode], [UserIdSupport], [UpdateAt], [CreateBy], [CreateAt], [UpdateBy]) VALUES (15, 227, CAST(N'2020-08-19T21:08:00' AS SmallDateTime), NULL, 28, 0, N'd43c9210-e82a-4390-aed0-4430ffc287ee', NULL, NULL, 59, CAST(N'2020-08-19T21:08:00' AS SmallDateTime), NULL)
GO
INSERT [qr_sa].[Shifts] ([Id], [Code], [StartAt], [EndAt], [BranchId], [IsEnded], [QRCode], [UserIdSupport], [UpdateAt], [CreateBy], [CreateAt], [UpdateBy]) VALUES (16, 228, CAST(N'2020-08-25T18:40:00' AS SmallDateTime), CAST(N'2020-09-24T23:13:00' AS SmallDateTime), 30, 1, N'7efd62a6-a9bb-4816-ab54-c611b95fc9a2', NULL, CAST(N'2020-09-24T23:13:00' AS SmallDateTime), 66, CAST(N'2020-08-25T18:40:00' AS SmallDateTime), 66)
GO
INSERT [qr_sa].[Shifts] ([Id], [Code], [StartAt], [EndAt], [BranchId], [IsEnded], [QRCode], [UserIdSupport], [UpdateAt], [CreateBy], [CreateAt], [UpdateBy]) VALUES (17, 229, CAST(N'2020-09-24T23:13:00' AS SmallDateTime), NULL, 30, 0, N'7b4e9c70-cd45-4ca6-b358-8e7e8642cd23', NULL, NULL, 66, CAST(N'2020-09-24T23:13:00' AS SmallDateTime), NULL)
GO
INSERT [qr_sa].[Shifts] ([Id], [Code], [StartAt], [EndAt], [BranchId], [IsEnded], [QRCode], [UserIdSupport], [UpdateAt], [CreateBy], [CreateAt], [UpdateBy]) VALUES (18, 230, CAST(N'2020-09-26T18:52:00' AS SmallDateTime), CAST(N'2020-09-26T18:52:00' AS SmallDateTime), 32, 1, N'f235d614-17f1-4489-8b64-5e53bbe4af7c', NULL, CAST(N'2020-09-26T18:52:00' AS SmallDateTime), 77, CAST(N'2020-09-26T18:52:00' AS SmallDateTime), 77)
GO
INSERT [qr_sa].[Shifts] ([Id], [Code], [StartAt], [EndAt], [BranchId], [IsEnded], [QRCode], [UserIdSupport], [UpdateAt], [CreateBy], [CreateAt], [UpdateBy]) VALUES (19, 231, CAST(N'2020-09-26T18:52:00' AS SmallDateTime), NULL, 32, 0, N'5b16a4df-0809-45a1-bb5d-46f132a86560', NULL, NULL, 77, CAST(N'2020-09-26T18:52:00' AS SmallDateTime), NULL)
GO
SET IDENTITY_INSERT [qr_sa].[Shifts] OFF
GO
SET IDENTITY_INSERT [qr_sa].[ShiftUsers] ON 
GO
INSERT [qr_sa].[ShiftUsers] ([Id], [UserId], [ShiftId], [WindowNumber], [ServiceId]) VALUES (9, 18, 9, N'1', 10)
GO
INSERT [qr_sa].[ShiftUsers] ([Id], [UserId], [ShiftId], [WindowNumber], [ServiceId]) VALUES (10, 19, 9, N'2', 10)
GO
INSERT [qr_sa].[ShiftUsers] ([Id], [UserId], [ShiftId], [WindowNumber], [ServiceId]) VALUES (11, 20, 9, N'3', 10)
GO
INSERT [qr_sa].[ShiftUsers] ([Id], [UserId], [ShiftId], [WindowNumber], [ServiceId]) VALUES (12, 46, 9, N'4', 11)
GO
INSERT [qr_sa].[ShiftUsers] ([Id], [UserId], [ShiftId], [WindowNumber], [ServiceId]) VALUES (13, 47, 9, N'5', 11)
GO
INSERT [qr_sa].[ShiftUsers] ([Id], [UserId], [ShiftId], [WindowNumber], [ServiceId]) VALUES (19, 54, 13, N'2', 13)
GO
INSERT [qr_sa].[ShiftUsers] ([Id], [UserId], [ShiftId], [WindowNumber], [ServiceId]) VALUES (20, 55, 13, N'20', 13)
GO
INSERT [qr_sa].[ShiftUsers] ([Id], [UserId], [ShiftId], [WindowNumber], [ServiceId]) VALUES (25, 61, 15, N'12', 18)
GO
INSERT [qr_sa].[ShiftUsers] ([Id], [UserId], [ShiftId], [WindowNumber], [ServiceId]) VALUES (26, 60, 15, N'2', 18)
GO
INSERT [qr_sa].[ShiftUsers] ([Id], [UserId], [ShiftId], [WindowNumber], [ServiceId]) VALUES (30, 64, 16, N'1', 19)
GO
INSERT [qr_sa].[ShiftUsers] ([Id], [UserId], [ShiftId], [WindowNumber], [ServiceId]) VALUES (31, 65, 16, N'2', 19)
GO
INSERT [qr_sa].[ShiftUsers] ([Id], [UserId], [ShiftId], [WindowNumber], [ServiceId]) VALUES (32, 64, 17, N'5', 19)
GO
INSERT [qr_sa].[ShiftUsers] ([Id], [UserId], [ShiftId], [WindowNumber], [ServiceId]) VALUES (33, 65, 17, N'10', 19)
GO
INSERT [qr_sa].[ShiftUsers] ([Id], [UserId], [ShiftId], [WindowNumber], [ServiceId]) VALUES (36, 78, 19, N'5', 21)
GO
INSERT [qr_sa].[ShiftUsers] ([Id], [UserId], [ShiftId], [WindowNumber], [ServiceId]) VALUES (37, 81, 19, N'6', 22)
GO
INSERT [qr_sa].[ShiftUsers] ([Id], [UserId], [ShiftId], [WindowNumber], [ServiceId]) VALUES (38, 79, 19, N'11', 21)
GO
SET IDENTITY_INSERT [qr_sa].[ShiftUsers] OFF
GO
/****** Object:  Index [IX_PagePermissions_PageId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_PagePermissions_PageId] ON [Acl].[PagePermissions]
(
	[PageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PagePermissions_PermissionTermId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_PagePermissions_PermissionTermId] ON [Acl].[PagePermissions]
(
	[PermissionTermId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RolePagePermissions_PagePermissionId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_RolePagePermissions_PagePermissionId] ON [Acl].[RolePagePermissions]
(
	[PagePermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RolePagePermissions_RoleId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_RolePagePermissions_RoleId] ON [Acl].[RolePagePermissions]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRoles_RoleId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_RoleId] ON [Acl].[UserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserRoles_UserId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_UserId] ON [Acl].[UserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_BranchId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_Users_BranchId] ON [Acl].[Users]
(
	[BranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_Email]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Email] ON [Acl].[Users]
(
	[Email] ASC
)
WHERE ([Email] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_OrganizationId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_Users_OrganizationId] ON [Acl].[Users]
(
	[OrganizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_UserName]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_Users_UserName] ON [Acl].[Users]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_UserTypeId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_Users_UserTypeId] ON [Acl].[Users]
(
	[UserTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Branchs_OrganizationId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_Branchs_OrganizationId] ON [qr_sa].[Branchs]
(
	[OrganizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BranchServices_BranchId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_BranchServices_BranchId] ON [qr_sa].[BranchServices]
(
	[BranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BranchServices_ServiceId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_BranchServices_ServiceId] ON [qr_sa].[BranchServices]
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EvaluationQuestionAnswer_EvaluationId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_EvaluationQuestionAnswer_EvaluationId] ON [qr_sa].[EvaluationQuestionAnswer]
(
	[EvaluationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EvaluationQuestionAnswer_QuestionId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_EvaluationQuestionAnswer_QuestionId] ON [qr_sa].[EvaluationQuestionAnswer]
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Evaluations_ShiftQueueId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_Evaluations_ShiftQueueId] ON [qr_sa].[Evaluations]
(
	[ShiftQueueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HelpAndSupport_UserId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_HelpAndSupport_UserId] ON [qr_sa].[HelpAndSupport]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Instructions_OrganizationId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_Instructions_OrganizationId] ON [qr_sa].[Instructions]
(
	[OrganizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LoginProviders_UserId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_LoginProviders_UserId] ON [qr_sa].[LoginProviders]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Questions_OrganizationId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_Questions_OrganizationId] ON [qr_sa].[Questions]
(
	[OrganizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Services_OrganizationId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_Services_OrganizationId] ON [qr_sa].[Services]
(
	[OrganizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ShiftQueues_ServiceId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_ShiftQueues_ServiceId] ON [qr_sa].[ShiftQueues]
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ShiftQueues_ShiftId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_ShiftQueues_ShiftId] ON [qr_sa].[ShiftQueues]
(
	[ShiftId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ShiftQueues_UserIdBy]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_ShiftQueues_UserIdBy] ON [qr_sa].[ShiftQueues]
(
	[UserIdBy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ShiftQueues_UserIdMobile]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_ShiftQueues_UserIdMobile] ON [qr_sa].[ShiftQueues]
(
	[UserIdMobile] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Shifts_BranchId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_Shifts_BranchId] ON [qr_sa].[Shifts]
(
	[BranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Shifts_UserIdSupport]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_Shifts_UserIdSupport] ON [qr_sa].[Shifts]
(
	[UserIdSupport] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ShiftUsers_ServiceId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_ShiftUsers_ServiceId] ON [qr_sa].[ShiftUsers]
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ShiftUsers_ShiftId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_ShiftUsers_ShiftId] ON [qr_sa].[ShiftUsers]
(
	[ShiftId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ShiftUsers_UserId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_ShiftUsers_UserId] ON [qr_sa].[ShiftUsers]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SystemGridColumns_SystemGridId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_SystemGridColumns_SystemGridId] ON [Settings].[SystemGridColumns]
(
	[SystemGridId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserGridColumns_SystemGridColumnId]    Script Date: 10/4/2020 11:43:25 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserGridColumns_SystemGridColumnId] ON [Settings].[UserGridColumns]
(
	[SystemGridColumnId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [Acl].[Users] ADD  DEFAULT ((0)) FOR [IsVerified]
GO
ALTER TABLE [qr_sa].[About] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreateAt]
GO
ALTER TABLE [qr_sa].[About] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [qr_sa].[HelpAndSupport] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreateAt]
GO
ALTER TABLE [qr_sa].[HelpAndSupport] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [qr_sa].[Services] ADD  DEFAULT ((0)) FOR [OrganizationId]
GO
ALTER TABLE [qr_sa].[ShiftQueues] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreatedDate]
GO
ALTER TABLE [Acl].[PagePermissions]  WITH CHECK ADD  CONSTRAINT [FK_PagePermissions_Pages_PageId] FOREIGN KEY([PageId])
REFERENCES [Acl].[Pages] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Acl].[PagePermissions] CHECK CONSTRAINT [FK_PagePermissions_Pages_PageId]
GO
ALTER TABLE [Acl].[PagePermissions]  WITH CHECK ADD  CONSTRAINT [FK_PagePermissions_PermissionsTerms_PermissionTermId] FOREIGN KEY([PermissionTermId])
REFERENCES [Acl].[PermissionsTerms] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Acl].[PagePermissions] CHECK CONSTRAINT [FK_PagePermissions_PermissionsTerms_PermissionTermId]
GO
ALTER TABLE [Acl].[RolePagePermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolePagePermissions_PagePermissions_PagePermissionId] FOREIGN KEY([PagePermissionId])
REFERENCES [Acl].[PagePermissions] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Acl].[RolePagePermissions] CHECK CONSTRAINT [FK_RolePagePermissions_PagePermissions_PagePermissionId]
GO
ALTER TABLE [Acl].[RolePagePermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolePagePermissions_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [Acl].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Acl].[RolePagePermissions] CHECK CONSTRAINT [FK_RolePagePermissions_Roles_RoleId]
GO
ALTER TABLE [Acl].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [Acl].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Acl].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles_RoleId]
GO
ALTER TABLE [Acl].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [Acl].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Acl].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users_UserId]
GO
ALTER TABLE [Acl].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Branchs_BranchId] FOREIGN KEY([BranchId])
REFERENCES [qr_sa].[Branchs] ([Id])
GO
ALTER TABLE [Acl].[Users] CHECK CONSTRAINT [FK_Users_Branchs_BranchId]
GO
ALTER TABLE [Acl].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Organization_OrganizationId] FOREIGN KEY([OrganizationId])
REFERENCES [qr_sa].[Organization] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Acl].[Users] CHECK CONSTRAINT [FK_Users_Organization_OrganizationId]
GO
ALTER TABLE [Acl].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserTypes_UserTypeId] FOREIGN KEY([UserTypeId])
REFERENCES [Acl].[UserTypes] ([Id])
GO
ALTER TABLE [Acl].[Users] CHECK CONSTRAINT [FK_Users_UserTypes_UserTypeId]
GO
ALTER TABLE [qr_sa].[Branchs]  WITH CHECK ADD  CONSTRAINT [FK_Branchs_Organization_OrganizationId] FOREIGN KEY([OrganizationId])
REFERENCES [qr_sa].[Organization] ([Id])
GO
ALTER TABLE [qr_sa].[Branchs] CHECK CONSTRAINT [FK_Branchs_Organization_OrganizationId]
GO
ALTER TABLE [qr_sa].[BranchServices]  WITH CHECK ADD  CONSTRAINT [FK_BranchServices_Branchs_BranchId] FOREIGN KEY([BranchId])
REFERENCES [qr_sa].[Branchs] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [qr_sa].[BranchServices] CHECK CONSTRAINT [FK_BranchServices_Branchs_BranchId]
GO
ALTER TABLE [qr_sa].[BranchServices]  WITH CHECK ADD  CONSTRAINT [FK_BranchServices_Services_ServiceId] FOREIGN KEY([ServiceId])
REFERENCES [qr_sa].[Services] ([Id])
GO
ALTER TABLE [qr_sa].[BranchServices] CHECK CONSTRAINT [FK_BranchServices_Services_ServiceId]
GO
ALTER TABLE [qr_sa].[EvaluationQuestionAnswer]  WITH CHECK ADD  CONSTRAINT [FK_EvaluationQuestionAnswer_Evaluations_EvaluationId] FOREIGN KEY([EvaluationId])
REFERENCES [qr_sa].[Evaluations] ([Id])
GO
ALTER TABLE [qr_sa].[EvaluationQuestionAnswer] CHECK CONSTRAINT [FK_EvaluationQuestionAnswer_Evaluations_EvaluationId]
GO
ALTER TABLE [qr_sa].[EvaluationQuestionAnswer]  WITH CHECK ADD  CONSTRAINT [FK_EvaluationQuestionAnswer_Questions_QuestionId] FOREIGN KEY([QuestionId])
REFERENCES [qr_sa].[Questions] ([Id])
GO
ALTER TABLE [qr_sa].[EvaluationQuestionAnswer] CHECK CONSTRAINT [FK_EvaluationQuestionAnswer_Questions_QuestionId]
GO
ALTER TABLE [qr_sa].[Evaluations]  WITH CHECK ADD  CONSTRAINT [FK_Evaluations_ShiftQueues_ShiftQueueId] FOREIGN KEY([ShiftQueueId])
REFERENCES [qr_sa].[ShiftQueues] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [qr_sa].[Evaluations] CHECK CONSTRAINT [FK_Evaluations_ShiftQueues_ShiftQueueId]
GO
ALTER TABLE [qr_sa].[HelpAndSupport]  WITH CHECK ADD  CONSTRAINT [FK_HelpAndSupport_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [Acl].[Users] ([Id])
GO
ALTER TABLE [qr_sa].[HelpAndSupport] CHECK CONSTRAINT [FK_HelpAndSupport_Users_UserId]
GO
ALTER TABLE [qr_sa].[Instructions]  WITH CHECK ADD  CONSTRAINT [FK_Instructions_Organization_OrganizationId] FOREIGN KEY([OrganizationId])
REFERENCES [qr_sa].[Organization] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [qr_sa].[Instructions] CHECK CONSTRAINT [FK_Instructions_Organization_OrganizationId]
GO
ALTER TABLE [qr_sa].[LoginProviders]  WITH CHECK ADD  CONSTRAINT [FK_LoginProviders_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [Acl].[Users] ([Id])
GO
ALTER TABLE [qr_sa].[LoginProviders] CHECK CONSTRAINT [FK_LoginProviders_Users_UserId]
GO
ALTER TABLE [qr_sa].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Organization_OrganizationId] FOREIGN KEY([OrganizationId])
REFERENCES [qr_sa].[Organization] ([Id])
GO
ALTER TABLE [qr_sa].[Questions] CHECK CONSTRAINT [FK_Questions_Organization_OrganizationId]
GO
ALTER TABLE [qr_sa].[Services]  WITH CHECK ADD  CONSTRAINT [FK_Services_Organization_OrganizationId] FOREIGN KEY([OrganizationId])
REFERENCES [qr_sa].[Organization] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [qr_sa].[Services] CHECK CONSTRAINT [FK_Services_Organization_OrganizationId]
GO
ALTER TABLE [qr_sa].[ShiftQueues]  WITH CHECK ADD  CONSTRAINT [FK_ShiftQueues_Services_ServiceId] FOREIGN KEY([ServiceId])
REFERENCES [qr_sa].[Services] ([Id])
GO
ALTER TABLE [qr_sa].[ShiftQueues] CHECK CONSTRAINT [FK_ShiftQueues_Services_ServiceId]
GO
ALTER TABLE [qr_sa].[ShiftQueues]  WITH CHECK ADD  CONSTRAINT [FK_ShiftQueues_Shifts_ShiftId] FOREIGN KEY([ShiftId])
REFERENCES [qr_sa].[Shifts] ([Id])
GO
ALTER TABLE [qr_sa].[ShiftQueues] CHECK CONSTRAINT [FK_ShiftQueues_Shifts_ShiftId]
GO
ALTER TABLE [qr_sa].[ShiftQueues]  WITH CHECK ADD  CONSTRAINT [FK_ShiftQueues_Users_UserIdBy] FOREIGN KEY([UserIdBy])
REFERENCES [Acl].[Users] ([Id])
GO
ALTER TABLE [qr_sa].[ShiftQueues] CHECK CONSTRAINT [FK_ShiftQueues_Users_UserIdBy]
GO
ALTER TABLE [qr_sa].[ShiftQueues]  WITH CHECK ADD  CONSTRAINT [FK_ShiftQueues_Users_UserIdMobile] FOREIGN KEY([UserIdMobile])
REFERENCES [Acl].[Users] ([Id])
GO
ALTER TABLE [qr_sa].[ShiftQueues] CHECK CONSTRAINT [FK_ShiftQueues_Users_UserIdMobile]
GO
ALTER TABLE [qr_sa].[Shifts]  WITH CHECK ADD  CONSTRAINT [FK_Shifts_Branchs_BranchId] FOREIGN KEY([BranchId])
REFERENCES [qr_sa].[Branchs] ([Id])
GO
ALTER TABLE [qr_sa].[Shifts] CHECK CONSTRAINT [FK_Shifts_Branchs_BranchId]
GO
ALTER TABLE [qr_sa].[Shifts]  WITH CHECK ADD  CONSTRAINT [FK_Shifts_Users_UserIdSupport] FOREIGN KEY([UserIdSupport])
REFERENCES [Acl].[Users] ([Id])
GO
ALTER TABLE [qr_sa].[Shifts] CHECK CONSTRAINT [FK_Shifts_Users_UserIdSupport]
GO
ALTER TABLE [qr_sa].[ShiftUsers]  WITH CHECK ADD  CONSTRAINT [FK_ShiftUsers_Services_ServiceId] FOREIGN KEY([ServiceId])
REFERENCES [qr_sa].[Services] ([Id])
GO
ALTER TABLE [qr_sa].[ShiftUsers] CHECK CONSTRAINT [FK_ShiftUsers_Services_ServiceId]
GO
ALTER TABLE [qr_sa].[ShiftUsers]  WITH CHECK ADD  CONSTRAINT [FK_ShiftUsers_Shifts_ShiftId] FOREIGN KEY([ShiftId])
REFERENCES [qr_sa].[Shifts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [qr_sa].[ShiftUsers] CHECK CONSTRAINT [FK_ShiftUsers_Shifts_ShiftId]
GO
ALTER TABLE [qr_sa].[ShiftUsers]  WITH CHECK ADD  CONSTRAINT [FK_ShiftUsers_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [Acl].[Users] ([Id])
GO
ALTER TABLE [qr_sa].[ShiftUsers] CHECK CONSTRAINT [FK_ShiftUsers_Users_UserId]
GO
ALTER TABLE [Settings].[SystemGridColumns]  WITH CHECK ADD  CONSTRAINT [FK_SystemGridColumns_SystemGrids_SystemGridId] FOREIGN KEY([SystemGridId])
REFERENCES [Settings].[SystemGrids] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Settings].[SystemGridColumns] CHECK CONSTRAINT [FK_SystemGridColumns_SystemGrids_SystemGridId]
GO
ALTER TABLE [Settings].[UserGridColumns]  WITH CHECK ADD  CONSTRAINT [FK_UserGridColumns_SystemGridColumns_SystemGridColumnId] FOREIGN KEY([SystemGridColumnId])
REFERENCES [Settings].[SystemGridColumns] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Settings].[UserGridColumns] CHECK CONSTRAINT [FK_UserGridColumns_SystemGridColumns_SystemGridColumnId]
GO
