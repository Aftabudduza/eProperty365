USE [master]
GO
/****** Object:  Database [EPropertyDB]    Script Date: 6/14/2019 8:02:04 PM ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'EPropertyDB')
BEGIN
CREATE DATABASE [EPropertyDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EPropertyDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER12\MSSQL\DATA\EPropertyDB.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'EPropertyDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER12\MSSQL\DATA\EPropertyDB_log.ldf' , SIZE = 20096KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
END

GO
ALTER DATABASE [EPropertyDB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EPropertyDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EPropertyDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EPropertyDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EPropertyDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EPropertyDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EPropertyDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [EPropertyDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EPropertyDB] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [EPropertyDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EPropertyDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EPropertyDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EPropertyDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EPropertyDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EPropertyDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EPropertyDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EPropertyDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EPropertyDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EPropertyDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EPropertyDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EPropertyDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EPropertyDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EPropertyDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EPropertyDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EPropertyDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EPropertyDB] SET RECOVERY FULL 
GO
ALTER DATABASE [EPropertyDB] SET  MULTI_USER 
GO
ALTER DATABASE [EPropertyDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EPropertyDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EPropertyDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EPropertyDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'EPropertyDB', N'ON'
GO
USE [EPropertyDB]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetID]    Script Date: 6/14/2019 8:02:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_GetID] 
	@ObjectID varchar(255), @ItemID varchar(255), @IDForYear int=NULL,
	@IDForMonth int=NULL, @IDForDate datetime=NULL, @NewID int=NULL output
AS
	SET NOCOUNT ON;

	--Generate Sequence daily
	IF @IDForDate IS NOT NULL
	  BEGIN
		--Current ID
		SELECT @NewID = CurrentID FROM GlobalID 
		WHERE ObjectID = @ObjectID AND ItemID = @ItemID AND 
		  IDForDate = @IDForDate AND IDForMonth IS NULL AND IDForYear IS NULL 

		--If not exist Set NewID=1 and Save in table
		IF @NewID IS NULL
	      BEGIN
			SET @NewID = 1
			INSERT INTO GlobalID(ObjectID, ItemID, IDForYear, IDForMonth, IDForDate, CurrentID)
			VALUES(@ObjectID, @ItemID, @IDForYear, @IDForMonth, @IDForDate, 2)
		  END
		ELSE
	      BEGIN
			UPDATE GlobalID SET CurrentID = CurrentID + 1
			WHERE ObjectID = @ObjectID AND ItemID = @ItemID AND 
			  IDForDate = @IDForDate AND IDForMonth IS NULL AND IDForYear IS NULL 
		  END

		SET NOCOUNT OFF;
		RETURN
	  END

	--Generate Squence Monthly
	IF @IDForMonth IS NOT NULL
	  BEGIN
		SELECT @NewID = CurrentID FROM GlobalID 
		WHERE ObjectID = @ObjectID AND ItemID = @ItemID AND 
		  IDForMonth = @IDForMonth AND IDForDate IS NULL AND IDForYear IS NULL 
		
		--If not exist Set NewID=1 and Save in table
		IF @NewID IS NULL
	      BEGIN
			SET @NewID = 1
			INSERT INTO GlobalID(ObjectID, ItemID, IDForYear, IDForMonth, IDForDate, CurrentID)
			VALUES(@ObjectID, @ItemID, @IDForYear, @IDForMonth, @IDForDate, 2)
		  END
		ELSE
	      BEGIN
			UPDATE GlobalID SET CurrentID = CurrentID + 1
			WHERE ObjectID = @ObjectID AND ItemID = @ItemID AND 
			  IDForMonth = @IDForMonth AND IDForDate IS NULL AND IDForYear IS NULL 
		  END

		SET NOCOUNT OFF;
		RETURN
	  END
	
	--Generate Squence Yearly
	declare @NewIdFromUnit int
	IF @IDForYear IS NOT NULL
	  BEGIN
	  
	 
	 
	--SELECT CONVERT(int, RIGHT (''1000000''+''5'', 6))
		SELECT @NewID = CurrentID FROM GlobalID WHERE ObjectID = @ObjectID AND ItemID = @ItemID 

		--If not exist Set NewID=1 and Save in table
		IF @NewID IS NULL
	      BEGIN
			SET @NewID = 1
			INSERT INTO GlobalID(ObjectID, ItemID, IDForYear, IDForMonth, IDForDate, CurrentID)
			VALUES(@ObjectID, @ItemID, @IDForYear, @IDForMonth, @IDForDate, 2)
		  END
		ELSE
	      BEGIN		  
		 
			 if(@ObjectID =''Account'')
				  begin	 
					  set @NewIdFromUnit=( select isnull(CONVERT(int, RIGHT (''100000000000''+CONVERT(varchar,max(Serial)), 11)),0)   from dbo.FinancialTransaction )
					  UPDATE GlobalID SET CurrentID = @NewIdFromUnit + 1
							WHERE ObjectID = @ObjectID AND ItemID = @ItemID 
							  set @NewID = @NewIdFromUnit + 1
				  end
			  else if(@ObjectID =''Payment'')
				  begin	 
					  set @NewIdFromUnit=( select isnull(CONVERT(int, RIGHT (''100000000000''+CONVERT(varchar,max(Serial)), 11)),0)   from dbo.PaymentHistory )
					  UPDATE GlobalID SET CurrentID = @NewIdFromUnit + 1
							WHERE ObjectID = @ObjectID AND ItemID = @ItemID 
							  set @NewID = @NewIdFromUnit + 1
				  end
			   else if(@ObjectID =''Owner'')
				  begin	 
					  set @NewIdFromUnit=( select isnull(CONVERT(int, RIGHT (''100000000000''+CONVERT(varchar,max(OwnerId)), 11)),0)   from dbo.userprofile )
					  UPDATE GlobalID SET CurrentID = @NewIdFromUnit + 1
							WHERE ObjectID = @ObjectID AND ItemID = @ItemID 
							  set @NewID = @NewIdFromUnit + 1
				  end
			   else if(@ObjectID =''AccountPackage'')
				  begin	 
					  set @NewIdFromUnit=( select isnull(CONVERT(int, RIGHT (''100000000000''+CONVERT(varchar,max(OwnerId)), 11)),0)   from dbo.userprofile )
					  UPDATE GlobalID SET CurrentID = @NewIdFromUnit + 1
							WHERE ObjectID = @ObjectID AND ItemID = @ItemID 
							  set @NewID = @NewIdFromUnit + 1
				  end
			   else if(@ObjectID =''SalesPartnerProfile'')
				  begin	 
					  set @NewIdFromUnit=( select isnull(CONVERT(int, RIGHT (''100000''+CONVERT(varchar,max(serialCode)), 5)),0)   from dbo.Dealer_SalesPartner )
					  UPDATE GlobalID SET CurrentID = @NewIdFromUnit + 1
							WHERE ObjectID = @ObjectID AND ItemID = @ItemID 
							  set @NewID = @NewIdFromUnit + 1
				  end
				else if(@ObjectID =''DealerProfile'')
				  begin	 
					  set @NewIdFromUnit=( select isnull(CONVERT(int, RIGHT (''100000''+CONVERT(varchar,max(serialCode)), 5)),0)   from dbo.Dealer_SalesPartner )
					  UPDATE GlobalID SET CurrentID = @NewIdFromUnit + 1
							WHERE ObjectID = @ObjectID AND ItemID = @ItemID 
							  set @NewID = @NewIdFromUnit + 1
				  end
			  else 
				  begin
				  UPDATE GlobalID SET CurrentID = CurrentID + 1
						WHERE ObjectID = @ObjectID AND ItemID = @ItemID  
				  end  		 
		 			
			END

		SET NOCOUNT OFF;
		RETURN
	  END
	
	--Generate Squence
	IF @IDForDate IS NULL AND @IDForMonth IS NULL AND @IDForYear IS NULL
	  BEGIN
		SELECT @NewID = CurrentID FROM GlobalID 
		WHERE ObjectID = @ObjectID AND ItemID = @ItemID 

		--If not exist Set NewID=1 and Save in table
		IF @NewID IS NULL
		  BEGIN
			SET @NewID = 1
			INSERT INTO GlobalID(ObjectID, ItemID, IDForYear, IDForMonth, IDForDate, CurrentID)
			VALUES(@ObjectID, @ItemID, @IDForYear, @IDForMonth, @IDForDate, 2)
		  END
		ELSE
		  BEGIN
			UPDATE GlobalID SET CurrentID = CurrentID + 1
			WHERE ObjectID = @ObjectID AND ItemID = @ItemID 
		  END

		SET NOCOUNT OFF;
		RETURN
	  END




' 
END
GO
/****** Object:  Table [dbo].[AccountChart]    Script Date: 6/14/2019 8:02:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountChart]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AccountChart](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[accountTypeId] [varchar](50) NULL CONSTRAINT [DF_AccountChart_accountTypeId]  DEFAULT ((0)),
	[accountCode] [varchar](10) NULL CONSTRAINT [DF_AccountChart_code]  DEFAULT (''),
	[accountName] [varchar](50) NULL CONSTRAINT [DF_AccountChart_name]  DEFAULT (''),
	[isActive] [bit] NULL CONSTRAINT [DF_AccountChart_isActive]  DEFAULT ((1)),
	[editAble] [bit] NULL CONSTRAINT [DF_AccountChart_editAble]  DEFAULT ((0)),
	[OwnerId] [varchar](20) NULL,
	[SortOrder] [int] NULL,
	[CreateDate] [datetime] NULL CONSTRAINT [DF_AccountChart_CreateDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_AccountChart] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AccountType]    Script Date: 6/14/2019 8:02:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AccountType](
	[id] [int] NOT NULL,
	[type] [varchar](10) NULL CONSTRAINT [DF_AccountType_type]  DEFAULT (''),
	[description] [varchar](50) NULL CONSTRAINT [DF_AccountType_description]  DEFAULT (''),
	[isActive] [bit] NULL CONSTRAINT [DF_AccountType_isActive]  DEFAULT ((1)),
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_AccountType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Communication]    Script Date: 6/14/2019 8:02:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Communication]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Communication](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Com_From] [nvarchar](50) NULL,
	[Com_To] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
	[Description] [nvarchar](200) NULL,
	[Status] [nvarchar](20) NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_Communication] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dealer_SalesPartner]    Script Date: 6/14/2019 8:02:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dealer_SalesPartner]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dealer_SalesPartner](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[serialCode] [varchar](20) NULL,
	[firstName] [varchar](50) NULL,
	[lastName] [varchar](50) NULL,
	[address1] [varchar](100) NULL,
	[address2] [varchar](100) NULL,
	[city] [varchar](30) NULL,
	[stateId] [varchar](20) NULL,
	[countryId] [varchar](20) NULL,
	[region] [varchar](30) NULL,
	[zipCode] [varchar](30) NULL,
	[primaryPhoneNo] [varchar](20) NULL,
	[mobilePhoneNo] [varchar](20) NULL,
	[routingNo] [varchar](25) NULL,
	[accountNo] [varchar](25) NULL,
	[email] [varchar](50) NULL,
	[userType] [int] NULL,
	[joinDate] [datetime] NULL,
	[commissionRate] [decimal](10, 2) NULL,
	[createDate] [datetime] NULL,
 CONSTRAINT [PK_DealerSalesPartnerProfile] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Dealer_SalesPartner_DetailsZipCodeCoverage]    Script Date: 6/14/2019 8:02:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dealer_SalesPartner_DetailsZipCodeCoverage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dealer_SalesPartner_DetailsZipCodeCoverage](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dealerSalesPartnerId] [varchar](20) NULL,
	[zipCode] [varchar](30) NULL,
	[commissionRate] [decimal](10, 2) NULL,
 CONSTRAINT [PK_Dealer_SalesPartner_DetailsZipCodeCoverage] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FinancialTransaction]    Script Date: 6/14/2019 8:02:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FinancialTransaction]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FinancialTransaction](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Serial] [varchar](20) NULL,
	[AccountType] [varchar](20) NULL,
	[LedgerCode] [varchar](20) NULL,
	[InvoiceNo] [varchar](20) NULL,
	[RefId] [varchar](20) NULL,
	[Amount] [money] NULL,
	[Debit] [money] NULL,
	[Credit] [money] NULL,
	[CreateDate] [datetime] NULL,
	[Remarks] [varchar](20) NULL,
	[EntryType] [varchar](10) NULL,
 CONSTRAINT [PK_FinancialTransaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GlobalID]    Script Date: 6/14/2019 8:02:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GlobalID]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GlobalID](
	[ObjectID] [varchar](255) NOT NULL,
	[ItemID] [varchar](255) NOT NULL,
	[IDForYear] [int] NULL,
	[IDForMonth] [int] NULL,
	[IDForDate] [datetime] NULL,
	[CurrentID] [int] NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OwnerProfile]    Script Date: 6/14/2019 8:02:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OwnerProfile]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OwnerProfile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Serial] [nvarchar](20) NULL,
	[SalesPartnerId] [varchar](20) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[CompanyName] [nvarchar](100) NULL,
	[Logo] [nvarchar](300) NULL,
	[FoundedOn] [datetime] NULL,
	[FoundedState] [nvarchar](30) NULL,
	[OrganizationType] [int] NULL,
	[Address] [nvarchar](150) NULL,
	[Address1] [nvarchar](150) NULL,
	[Region] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[State] [nvarchar](30) NULL,
	[City] [nvarchar](30) NULL,
	[Zip] [nvarchar](10) NULL,
	[TypeOfNumber] [int] NULL,
	[FedNumber] [nvarchar](10) NULL,
	[FedLast4Digit] [nvarchar](10) NULL,
	[IsDelete] [bit] NULL,
	[CreatedBy] [smallint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [smallint] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.OwnerProfile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PaymentHistory]    Script Date: 6/14/2019 8:02:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PaymentHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PaymentHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FromUser] [varchar](20) NULL,
	[ToUser] [varchar](20) NULL,
	[FromUserType] [int] NULL,
	[ToUserType] [int] NULL,
	[UnitNo] [varchar](20) NULL,
	[Amount] [money] NULL,
	[AccountName] [nvarchar](50) NULL,
	[Address] [nvarchar](100) NULL,
	[Address1] [nvarchar](100) NULL,
	[City] [nvarchar](25) NULL,
	[State] [nvarchar](20) NULL,
	[Zip] [nvarchar](10) NULL,
	[CardNumber] [nvarchar](25) NULL,
	[CVS] [nvarchar](5) NULL,
	[Month] [nvarchar](5) NULL,
	[Year] [nvarchar](5) NULL,
	[LastFourDigitCard] [nvarchar](5) NULL,
	[IsCheckingAccount] [bit] NULL,
	[RoutingNo] [nvarchar](25) NULL,
	[AccountNo] [nvarchar](25) NULL,
	[CheckNo] [nvarchar](25) NULL,
	[AccountType] [varchar](15) NULL,
	[CreateDate] [datetime] NULL,
	[AuthorizationCode] [varchar](20) NULL,
	[TransactionCode] [varchar](20) NULL,
	[TransactionDescription] [varchar](100) NULL,
	[Getway] [varchar](50) NULL,
	[DebitAmount] [money] NULL,
	[CreditAmount] [money] NULL,
	[Status] [varchar](20) NULL,
	[Serial] [varchar](20) NULL,
	[TransactionType] [varchar](50) NULL,
	[LedgerCode] [varchar](20) NULL,
	[Remarks] [varchar](20) NULL,
 CONSTRAINT [PK_PaymentHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PaymentInformation]    Script Date: 6/14/2019 8:02:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PaymentInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PaymentInformation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OwnerId] [nvarchar](20) NULL,
	[AccountName] [nvarchar](50) NULL,
	[Address] [nvarchar](100) NULL,
	[Address1] [nvarchar](100) NULL,
	[City] [nvarchar](25) NULL,
	[State] [nvarchar](20) NULL,
	[Zip] [nvarchar](10) NULL,
	[CardNumber] [nvarchar](25) NULL,
	[CVS] [nvarchar](5) NULL,
	[Month] [nvarchar](5) NULL,
	[Year] [nvarchar](5) NULL,
	[LastFourDigitCard] [nvarchar](5) NULL,
	[IsCheckingAccount] [bit] NULL,
	[RoutingNo] [nvarchar](25) NULL,
	[AccountNo] [nvarchar](25) NULL,
	[CheckNo] [nvarchar](25) NULL,
	[Username] [nvarchar](20) NULL,
 CONSTRAINT [PK_PaymentInformation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Report]    Script Date: 6/14/2019 8:02:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Report](
	[id] [int] NOT NULL,
	[name] [varchar](50) NULL CONSTRAINT [DF_Report_name]  DEFAULT (''),
	[isActive] [bit] NULL CONSTRAINT [DF_Report_isActive]  DEFAULT ((1)),
 CONSTRAINT [PK_Report] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SystemInformation]    Script Date: 6/14/2019 8:02:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SystemInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SystemInformation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OwnerId] [nvarchar](20) NULL,
	[Website] [nvarchar](40) NULL,
	[EmailServer1] [nvarchar](40) NULL,
	[EmailUser1] [nvarchar](40) NULL,
	[EmailPassword1] [nvarchar](15) NULL,
	[EmailServer2] [nvarchar](40) NULL,
	[EmailUser2] [nvarchar](40) NULL,
	[EmailPassword2] [nvarchar](15) NULL,
	[EmailServer3] [nvarchar](40) NULL,
	[EmailUser3] [nvarchar](40) NULL,
	[EmailPassword3] [nvarchar](15) NULL,
	[EmailServer4] [nvarchar](40) NULL,
	[EmailUser4] [nvarchar](40) NULL,
	[EmailPassword4] [nvarchar](15) NULL,
	[SecurityLink] [nvarchar](40) NULL,
	[SecurityKey] [nvarchar](40) NULL,
	[SecurityUser] [nvarchar](25) NULL,
	[SecurityPassword] [nvarchar](15) NULL,
	[CreditCardLink] [nvarchar](40) NULL,
	[CreditCardKey] [nvarchar](40) NULL,
	[CreditCardUser] [nvarchar](25) NULL,
	[CreditCardPassword] [nvarchar](15) NULL,
	[DocumentLink] [nvarchar](40) NULL,
	[ApplicationFee] [decimal](18, 4) NULL,
	[FeeType] [smallint] NULL,
	[FeePercentage] [decimal](18, 4) NULL,
	[FeeFlatAmount] [decimal](18, 4) NULL,
	[IncludeProcessFees] [bit] NULL,
	[TanentPayFees] [bit] NULL,
	[IncludeCondoProcessFees] [bit] NULL,
	[TanentPayCondoFees] [bit] NULL,
	[MonthlySoftwareCharge] [decimal](18, 4) NULL,
	[CreditCardProcessFees] [decimal](18, 4) NULL,
	[OneTimePay] [bit] NULL,
	[RecurringPay] [bit] NULL,
	[IsGlobalSystem] [bit] NULL,
	[Username] [nvarchar](20) NULL,
	[UnitPrice] [decimal](18, 4) NULL,
	[NoOfUnit] [int] NULL,
	[FeeTypeCheck] [smallint] NULL,
	[FeePercentageCheck] [decimal](18, 4) NULL,
	[FeeFlatAmountCheck] [decimal](18, 4) NULL,
	[ScreeningFee] [decimal](18, 4) NULL,
	[LateRentPercentage] [decimal](18, 4) NULL,
	[ChargeBackFee] [decimal](18, 4) NULL,
	[ComUsername1] [nvarchar](40) NULL,
	[ComEmailAddress1] [nvarchar](40) NULL,
	[ComUsername2] [nvarchar](40) NULL,
	[ComEmailAddress2] [nvarchar](40) NULL,
	[ComUsername3] [nvarchar](40) NULL,
	[ComEmailAddress3] [nvarchar](40) NULL,
	[ComUsername4] [nvarchar](40) NULL,
	[ComEmailAddress4] [nvarchar](40) NULL,
	[AccountPackageId] [nvarchar](20) NULL,
 CONSTRAINT [PK_SystemInformation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserCommission]    Script Date: 6/14/2019 8:02:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserCommission]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserCommission](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RefId] [varchar](20) NULL,
	[InvoiceNo] [varchar](20) NULL,
	[TransactionType] [varchar](50) NULL,
	[AccountType] [varchar](20) NULL,
	[LedgerCode] [varchar](20) NULL,
	[Amount] [money] NULL,
	[Debit] [money] NULL,
	[Credit] [money] NULL,
	[CreateDate] [datetime] NULL,
	[Remarks] [varchar](20) NULL,
	[Status] [varchar](20) NULL,
	[UserType] [varchar](10) NULL,
	[UnitId] [varchar](20) NULL,
	[OwnerId] [varchar](20) NULL,
	[Month] [varchar](5) NULL,
	[Year] [varchar](5) NULL,
 CONSTRAINT [PK_UserCommission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 6/14/2019 8:02:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserProfile]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserProfile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](30) NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Password] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](30) NULL,
	[SecurityLevel] [nvarchar](30) NULL,
	[Location] [nvarchar](50) NULL,
	[DatabaseName] [nvarchar](30) NULL,
	[DatabaseLocation] [nvarchar](200) NULL,
	[UserType] [nvarchar](30) NULL,
	[IsAdmin] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
	[CanLogin] [bit] NULL CONSTRAINT [DF_UserProfile_CanLogin]  DEFAULT ((0)),
	[CreatedBy] [smallint] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [smallint] NULL,
	[UpdatedDate] [datetime] NULL,
	[Remarks] [nvarchar](200) NULL,
	[OwnerId] [nvarchar](20) NULL,
	[LocationId] [nvarchar](20) NULL,
	[HasSystemInfo] [bit] NULL CONSTRAINT [DF_UserProfile_HasSystemInfo_1]  DEFAULT ((0)),
	[HasOwnerProfile] [bit] NULL CONSTRAINT [DF_UserProfile_HasOwnerProfile]  DEFAULT ((0)),
	[HasPropertyManagerProfile] [bit] NULL CONSTRAINT [DF_UserProfile_HasPropertyManagerProfile]  DEFAULT ((0)),
	[HasUserProfile] [bit] NULL CONSTRAINT [DF_UserProfile_HasUserProfile]  DEFAULT ((0)),
	[HasContactProfile] [bit] NULL CONSTRAINT [DF_UserProfile_HasContactProfile_1]  DEFAULT ((0)),
	[HasPropertyLocation] [bit] NULL CONSTRAINT [DF_UserProfile_HasPropertyLocation_1]  DEFAULT ((0)),
	[HasPropertyUnit] [bit] NULL CONSTRAINT [DF_UserProfile_HasPropertyUnit_1]  DEFAULT ((0)),
	[HasVendorProfile] [bit] NULL CONSTRAINT [DF_UserProfile_HasVendorProfile_1]  DEFAULT ((0)),
	[HasLedgerCode] [bit] NULL CONSTRAINT [DF_UserProfile_HasLedgerCode_1]  DEFAULT ((0)),
	[HasCompletedFullProfile] [bit] NULL CONSTRAINT [DF_UserProfile_HasLedgerCode1]  DEFAULT ((0)),
	[HasFinishedTenantImport] [bit] NULL CONSTRAINT [DF_UserProfile_HasFinishedTenantImport]  DEFAULT ((0)),
	[HasAccountSystem] [bit] NULL CONSTRAINT [DF_UserProfile_HasAccountSystem]  DEFAULT ((0)),
	[HasDocuments] [bit] NULL CONSTRAINT [DF_UserProfile_HasDocuments]  DEFAULT ((0)),
 CONSTRAINT [PK_UserProfile_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 6/14/2019 8:02:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserType] [varchar](50) NULL,
	[UserDefineId] [int] NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[AccountChart] ON 

GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (1, N'Asset', N'1010', N'Cash Main', 1, 0, N'Admin', 1, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (2, N'Asset', N'1020', N'Cash Payroll account', 1, 0, N'Admin', 2, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (3, N'Asset', N'1030', N'Pretty Cash', 1, 0, N'Admin', 3, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (4, N'Asset', N'1040', N'Security Deposits', 1, 0, N'Admin', 4, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (5, N'Asset', N'1050', N'Marketable Securites', 1, 0, N'Admin', 5, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (6, N'Asset', N'1060', N'Rent Exchange Holding Account ', 1, 0, N'Admin', 6, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (7, N'Asset', N'1210', N'Account Receivable', 1, 0, N'Admin', 7, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (8, N'Asset', N'1290', N'Allowance for Doubtful Accounts', 1, 0, N'Admin', 8, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (9, N'Asset', N'1310', N'Inventory ', 1, 0, N'Admin', 9, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (10, N'Asset', N'1410', N'Prepaid Expenses', 1, 0, N'Admin', 10, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (11, N'Asset', N'1510', N'Building', 1, 0, N'Admin', 11, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (12, N'Asset', N'1520', N'Equipment', 1, 0, N'Admin', 12, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (13, N'Asset', N'1530', N'Vehicles', 1, 0, N'Admin', 13, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (14, N'Asset', N'1540', N'Furniture & Fixtues', 1, 0, N'Admin', 14, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (15, N'Asset', N'1550', N'Leasehold Improvements', 1, 0, N'Admin', 15, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (16, N'Asset', N'1610', N'Accoumulated Depreciation', 1, 0, N'Admin', 16, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (17, N'Asset', N'1910', N'Other Assets', 1, 0, N'Admin', 17, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (18, N'Lib', N'2110', N'Account Payable
', 1, 0, N'Admin', 18, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (19, N'Lib', N'2120', N'Accured Liabilies', 1, 0, N'Admin', 20, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (20, N'Lib', N'2130', N'Notes Payable
', 1, 0, N'Admin', 21, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (21, N'Lib', N'2140', N'Rent Exchange Transfer
', 1, 0, N'Admin', 22, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (22, N'Lib', N'2210', N'Wages Payable', 1, 0, N'Admin', 23, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (23, N'Lib', N'2220', N'Commission', 1, 0, N'Admin', 24, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (24, N'Lib', N'2230', N'FICA', 1, 0, N'Admin', 25, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (25, N'Lib', N'2240', N'Unemployment', 1, 0, N'Admin', 26, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (26, N'Lib', N'2250', N'Workmen''s Comp', 1, 0, N'Admin', 27, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (27, N'Lib', N'2260', N'Medical Benefits', 1, 0, N'Admin', 28, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (28, N'Lib', N'2270', N'401 K Match', 1, 0, N'Admin', 29, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (29, N'Lib', N'2280', N'401 K Employee Contribution', 1, 0, N'Admin', 30, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (30, N'Lib', N'2510', N'Federal Income Taxes', 1, 0, N'Admin', 31, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (31, N'Lib', N'2520', N'State Incomes Taxes', 1, 0, N'Admin', 32, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (32, N'Lib', N'2530', N'Deffer - FIT Current', 1, 0, N'Admin', 33, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (33, N'Lib', N'2540', N'Deferred - state Income Taxes', 1, 0, N'Admin', 34, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (34, N'Lib', N'2710', N'Long Term Debt', 1, 0, N'Admin', 35, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (35, N'Lib', N'2720', N'Notes Payable', 1, 0, N'Admin', 36, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (36, N'Lib', N'2730', N'Mortage Payable', 1, 0, N'Admin', 37, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (37, N'Lib', N'2740', N'Installment Notes Payable', 1, 0, N'Admin', 38, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (38, N'Equ', N'3100', N'Common Stock', 1, 0, N'Admin', 40, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (39, N'Equ', N'3200', N'Preferred Stock', 1, 0, N'Admin', 41, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (40, N'Equ', N'3300', N'Pain in capital', 1, 0, N'Admin', 42, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (41, N'Equ', N'3400', N'Partners Capital', 1, 0, N'Admin', 43, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (42, N'Equ', N'3500', N'Member Contributions', 1, 0, N'Admin', 44, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (43, N'Equ', N'3900', N'Retained Earning', 1, 0, N'Admin', 45, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (44, N'Inc', N'4000', N'Unit Fee', 1, 0, N'Admin', 46, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (45, N'Inc', N'4010', N'Rental Income', 1, 0, N'Admin', 47, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (46, N'Inc', N'4012', N'Less rent returns and allowances', 1, 0, N'Admin', 48, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (47, N'Inc', N'4020', N'Exclusive Territoy License Fee', 1, 0, N'Admin', 49, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (48, N'Inc', N'4030', N'ET License Renewal Fee', 1, 0, N'Admin', 50, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (49, N'Inc', N'4040', N'Late Fee', 1, 0, N'Admin', 51, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (50, N'Inc', N'4050', N'Bad Check Fee', 1, 0, N'Admin', 52, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (51, N'Inc', N'4060', N'Application Fee', 1, 0, N'Admin', 53, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (52, N'Inc', N'4070', N'Credit Check Fee', 1, 0, N'Admin', 54, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (53, N'Inc', N'4080', N'Other Income', 1, 0, N'Admin', 55, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (54, N'Inc', N'4100', N'Processing Fees', 1, 0, N'Admin', 56, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (55, N'Inc', N'4200', N'Ad Revenue National', 1, 0, N'Admin', 57, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (56, N'Inc', N'4210', N'Ad Revenue Local', 1, 0, N'Admin', 58, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (57, N'Inc', N'4220', N'Commissions', 1, 0, N'Admin', 59, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (58, N'Inc', N'4600', N'Interest Income', 1, 0, N'Admin', 60, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (59, N'Inc', N'4950', N'Sales Discounts', 1, 0, N'Admin', 61, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (60, N'COG', N'5010', N'Sales Partner Commission', 1, 0, N'Admin', 62, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (61, N'COG', N'5020', N'Dealer Commission', 1, 0, N'Admin', 63, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (62, N'COG', N'5030', N'Dealer Broker Commission', 1, 0, N'Admin', 64, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (63, N'COG', N'5040', N'Bank Processing Fee', 1, 0, N'Admin', 65, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (64, N'Exp', N'6010', N'Advertising', 1, 0, N'Admin', 66, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (65, N'Exp', N'6020', N'Amortization', 1, 0, N'Admin', 67, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (66, N'Exp', N'6030', N'Auto', 1, 0, N'Admin', 68, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (67, N'Exp', N'6040', N'Bad Debt', 1, 0, N'Admin', 69, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (68, N'Exp', N'6050', N'Bank Fee', 1, 0, N'Admin', 70, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (69, N'Exp', N'6060', N'Commissions', 1, 0, N'Admin', 71, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (70, N'Exp', N'6070', N'Depreciation Expense or Depletion', 1, 0, N'Admin', 72, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (71, N'Exp', N'6080', N'Employee Benefits', 1, 0, N'Admin', 73, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (72, N'Exp', N'6090', N'Frieght', 1, 0, N'Admin', 74, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (73, N'Exp', N'6100', N'Gifts', 1, 0, N'Admin', 75, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (74, N'Exp', N'6110', N'Insurance', 1, 0, N'Admin', 76, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (75, N'Exp', N'6120', N'Interest', 1, 0, N'Admin', 77, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (76, N'Exp', N'6130', N'License', 1, 0, N'Admin', 78, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (77, N'Exp', N'6140', N'Maintenance', 1, 0, N'Admin', 79, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (78, N'Exp', N'6150', N'Meals & Entertain', 1, 0, N'Admin', 80, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (79, N'Exp', N'6160', N'Office Supplies', 1, 0, N'Admin', 81, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (80, N'Exp', N'6170', N'Other Expenses', 1, 0, N'Admin', 82, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (81, N'Exp', N'6180', N'Payroll Taxes', 1, 0, N'Admin', 83, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (82, N'Exp', N'6190', N'Printing', 1, 0, N'Admin', 84, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (83, N'Exp', N'6200', N'Postage', 1, 0, N'Admin', 85, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (84, N'Exp', N'6210', N'Processing Fees', 1, 0, N'Admin', 86, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (85, N'Exp', N'6220', N'Rent', 1, 0, N'Admin', 87, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (86, N'Exp', N'6230', N'ledger 6200', 1, 0, N'Admin', 88, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (87, N'Exp', N'6240', N'Salaries', 1, 0, N'Admin', 89, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (88, N'Exp', N'6250', N'Supplies', 1, 0, N'Admin', 90, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (89, N'Exp', N'6260', N'Taxes', 1, 0, N'Admin', 91, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (90, N'Exp', N'6270', N'Utilities', 1, 0, N'Admin', 92, CAST(N'2019-01-28 14:55:38.313' AS DateTime))
GO
INSERT [dbo].[AccountChart] ([id], [accountTypeId], [accountCode], [accountName], [isActive], [editAble], [OwnerId], [SortOrder], [CreateDate]) VALUES (91, N'Lib', N'6280', N'Electricity Bill', 1, 0, N'Admin', 1001, CAST(N'2019-02-06 22:53:41.963' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[AccountChart] OFF
GO
INSERT [dbo].[AccountType] ([id], [type], [description], [isActive], [SortOrder]) VALUES (1, N'Asset', N'Assets', 1, 1)
GO
INSERT [dbo].[AccountType] ([id], [type], [description], [isActive], [SortOrder]) VALUES (2, N'Lib', N'Liabilites', 1, 2)
GO
INSERT [dbo].[AccountType] ([id], [type], [description], [isActive], [SortOrder]) VALUES (3, N'Equ', N'Equity', 1, 3)
GO
INSERT [dbo].[AccountType] ([id], [type], [description], [isActive], [SortOrder]) VALUES (4, N'COG', N'Cost of Goods', 1, 4)
GO
INSERT [dbo].[AccountType] ([id], [type], [description], [isActive], [SortOrder]) VALUES (5, N'Inc', N'Revenue', 1, 5)
GO
INSERT [dbo].[AccountType] ([id], [type], [description], [isActive], [SortOrder]) VALUES (6, N'Chk', N'Check', 1, 6)
GO
INSERT [dbo].[AccountType] ([id], [type], [description], [isActive], [SortOrder]) VALUES (7, N'ACH', N'ACH', 1, 7)
GO
INSERT [dbo].[AccountType] ([id], [type], [description], [isActive], [SortOrder]) VALUES (8, N'Joun', N'Journal entry', 1, 8)
GO
INSERT [dbo].[AccountType] ([id], [type], [description], [isActive], [SortOrder]) VALUES (9, N'Open', N'Opening', 1, 9)
GO
INSERT [dbo].[AccountType] ([id], [type], [description], [isActive], [SortOrder]) VALUES (10, N'Close', N'Closing', 1, 10)
GO
INSERT [dbo].[AccountType] ([id], [type], [description], [isActive], [SortOrder]) VALUES (11, N'Cred', N'Credit memo', 1, 11)
GO
INSERT [dbo].[AccountType] ([id], [type], [description], [isActive], [SortOrder]) VALUES (12, N'Exp', N'Operating Expenses', 1, 12)
GO
INSERT [dbo].[Report] ([id], [name], [isActive]) VALUES (1, N'Dealer Sales', 1)
GO
INSERT [dbo].[Report] ([id], [name], [isActive]) VALUES (2, N'Balance Sheet', 1)
GO
SET IDENTITY_INSERT [dbo].[UserProfile] ON 

GO
INSERT [dbo].[UserProfile] ([Id], [Username], [Title], [Password], [Email], [Phone], [SecurityLevel], [Location], [DatabaseName], [DatabaseLocation], [UserType], [IsAdmin], [IsDeleted], [IsActive], [CanLogin], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [Remarks], [OwnerId], [LocationId], [HasSystemInfo], [HasOwnerProfile], [HasPropertyManagerProfile], [HasUserProfile], [HasContactProfile], [HasPropertyLocation], [HasPropertyUnit], [HasVendorProfile], [HasLedgerCode], [HasCompletedFullProfile], [HasFinishedTenantImport], [HasAccountSystem], [HasDocuments]) VALUES (1, N'sbutcher', N'sbutcher', N'ZXByb3BlcnR5MzY1MTIz', N'sbutcher@eproperty365.com', N'123456', N'2 and Up', N'1000001', N'', N'', N'1', 1, 0, 1, 1, NULL, NULL, NULL, NULL, N'', N'', N'1000001', 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[UserProfile] OFF
GO
SET IDENTITY_INSERT [dbo].[UserType] ON 

GO
INSERT [dbo].[UserType] ([Id], [UserType], [UserDefineId]) VALUES (1, N'Admin', 1)
GO
INSERT [dbo].[UserType] ([Id], [UserType], [UserDefineId]) VALUES (2, N'Owner', 2)
GO
INSERT [dbo].[UserType] ([Id], [UserType], [UserDefineId]) VALUES (3, N'Property Manager', 3)
GO
INSERT [dbo].[UserType] ([Id], [UserType], [UserDefineId]) VALUES (4, N'Maintenance Manager', 4)
GO
INSERT [dbo].[UserType] ([Id], [UserType], [UserDefineId]) VALUES (5, N'Resident Tenant', 5)
GO
INSERT [dbo].[UserType] ([Id], [UserType], [UserDefineId]) VALUES (6, N'Commercial Tenant', 6)
GO
INSERT [dbo].[UserType] ([Id], [UserType], [UserDefineId]) VALUES (7, N'Condo Tenant', 7)
GO
INSERT [dbo].[UserType] ([Id], [UserType], [UserDefineId]) VALUES (8, N'Normal', 8)
GO
INSERT [dbo].[UserType] ([Id], [UserType], [UserDefineId]) VALUES (9, N'Sales Partner', 9)
GO
INSERT [dbo].[UserType] ([Id], [UserType], [UserDefineId]) VALUES (10, N'Dealer', 10)
GO
SET IDENTITY_INSERT [dbo].[UserType] OFF
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_PaymentHistory_IsCheckingAccount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PaymentHistory] ADD  CONSTRAINT [DF_PaymentHistory_IsCheckingAccount]  DEFAULT ((0)) FOR [IsCheckingAccount]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_PaymentInformation_IsCheckingAccount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PaymentInformation] ADD  CONSTRAINT [DF_PaymentInformation_IsCheckingAccount]  DEFAULT ((0)) FOR [IsCheckingAccount]
END

GO
USE [master]
GO
ALTER DATABASE [EPropertyDB] SET  READ_WRITE 
GO
