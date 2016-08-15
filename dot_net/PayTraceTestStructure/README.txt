Developer: Ben Esposito
Company: WTE Solutions
Date: 8/12/16

I've added multiple methods for creating a one-time payment with a credit card (oneTime()) and with a check, and for making recurring paymets (recurring()). Logging (logger()) can be enabled or disabled in web.config. Three tables in MS SQL have been created to store sReponses, sRequests, and identify the transaction types. It uses the username "demo123" and password "demo123" to create transactions. 





-----------------------------------------------
----   SQL CREATE SCRIPTS                  ----
-----------------------------------------------
Creating the Key Table:

CREATE TABLE [dbo].[TransactionTypeKey](
	[Type] [char](1) NOT NULL,
	[Description] [varchar](10) NOT NULL
) ON [PRIMARY]

GO

INSERT INTO [dbo].[TransactionTypeKey]([Type],[Description]) VALUES ('O','One-Time')
INSERT INTO [dbo].[TransactionTypeKey]([Type],[Description]) VALUES ('C','Check')
INSERT INTO [dbo].[TransactionTypeKey]([Type],[Description]) VALUES ('W','Weekly')
INSERT INTO [dbo].[TransactionTypeKey]([Type],[Description]) VALUES ('M','Monthly')
INSERT INTO [dbo].[TransactionTypeKey]([Type],[Description]) VALUES ('A','Annually')


Creating the sRequest table:

CREATE TABLE [dbo].[PaytraceRequest](
	[Request ID] [int] IDENTITY(1,1) NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[TransactionType] [char](1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Address] [varchar](50) NOT NULL,
	[Address2] [varchar](50) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[Zip] [varchar](12) NOT NULL,
	[PhoneNumber] [varchar](16) NOT NULL,
	[EmailAddress] [varchar](50) NOT NULL,
	[Amount] [varchar](15) NULL,
	[FirstPaymentDate] [smalldatetime] NULL,
	[NumOfTransactions] [varchar](5) NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PaytraceRequest] ADD  CONSTRAINT [DF_PaytraceRequest_Timestamp]  DEFAULT (getdate()) FOR [Timestamp]
GO

Creating the sResponse Table:

CREATE TABLE [dbo].[PaytraceResponse](
	[ResponseID] [int] IDENTITY(1,1) NOT NULL,
	[Timstamp] [datetime] NOT NULL,
	[TransactionType] [char](1) NULL,
	[TransactionID] [varchar](16) NULL,
	[Appcode] [varchar](6) NULL,
	[AVS] [varchar](255) NULL,
	[CSC] [varchar](255) NULL,
	[AppMsg] [varchar](255) NULL,
	[RecurID] [varchar](50) NULL,
	[CheckIdentifier] [varchar](16) NULL,
	[ACHCode] [char](6) NULL,
	[ACHMessage] [varchar](255) NULL,
	[Error] [varchar](255) NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PaytraceResponse] ADD  CONSTRAINT [DF_PaytraceResponse_Timstamp]  DEFAULT (getdate()) FOR [Timstamp]
GO