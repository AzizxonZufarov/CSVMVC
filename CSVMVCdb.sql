USE [CSVMVCdb]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 18.08.2022 15:40:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Payroll_Number] [nvarchar](50) NULL,
	[Forenames] [nvarchar](50) NULL,
	[Surname] [nvarchar](50) NULL,
	[Date_of_Birth] [date] NULL,
	[Telephone] [int] NULL,
	[Mobile] [int] NULL,
	[Address] [varchar](max) NULL,
	[Address_2] [varchar](max) NULL,
	[Postcode] [varchar](50) NULL,
	[EMail_Home] [varchar](max) NULL,
	[Start_Date] [date] NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([ID], [Payroll_Number], [Forenames], [Surname], [Date_of_Birth], [Telephone], [Mobile], [Address], [Address_2], [Postcode], [EMail_Home], [Start_Date]) VALUES (15, N'COOP08', N'John ', N'William', CAST(N'1955-01-26' AS Date), 12345678, 987654231, N'12 Foreman road', N'London', N'GU12 6JW', N'nomadic20@hotmail.co.uk', CAST(N'2013-04-18' AS Date))
INSERT [dbo].[Employees] ([ID], [Payroll_Number], [Forenames], [Surname], [Date_of_Birth], [Telephone], [Mobile], [Address], [Address_2], [Postcode], [EMail_Home], [Start_Date]) VALUES (16, N'JACK13', N'Jerry', N'Jackson', CAST(N'1974-05-11' AS Date), 2050508, 6987457, N'115 Spinney Road', N'Luton', N'LU33DF', N'gerry.jackson@bt.com', CAST(N'2013-04-18' AS Date))
INSERT [dbo].[Employees] ([ID], [Payroll_Number], [Forenames], [Surname], [Date_of_Birth], [Telephone], [Mobile], [Address], [Address_2], [Postcode], [EMail_Home], [Start_Date]) VALUES (17, N'COOP08', N'John ', N'William', CAST(N'1955-01-26' AS Date), 12345678, 987654231, N'12 Foreman road', N'London', N'GU12 6JW', N'nomadic20@hotmail.co.uk', CAST(N'2013-04-18' AS Date))
INSERT [dbo].[Employees] ([ID], [Payroll_Number], [Forenames], [Surname], [Date_of_Birth], [Telephone], [Mobile], [Address], [Address_2], [Postcode], [EMail_Home], [Start_Date]) VALUES (18, N'JACK13', N'Jerry', N'Jackson', CAST(N'1974-05-11' AS Date), 2050508, 6987457, N'115 Spinney Road', N'Luton', N'LU33DF', N'gerry.jackson@bt.com', CAST(N'2013-04-18' AS Date))
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
