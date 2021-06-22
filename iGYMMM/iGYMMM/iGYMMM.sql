USE iGYMMM
GO

CREATE TABLE [dbo].[Gyms](
	[GymId] [int] IDENTITY(100000,1) NOT NULL PRIMARY KEY,
	[GymName] [nvarchar](200) NOT NULL,	
	[GymDescr] [nvarchar](500) NOT NULL,	
    [Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[ChangedBy] [int] NOT NULL,
	[ChangedAt] [bigint] NOT NULL)

CREATE TABLE [dbo].[Users](
	[UsrId] [int] IDENTITY(100000,1) NOT NULL PRIMARY KEY,
	[UsrInfId] [int] NOT NULL DEFAULT 0,
	[UsrName] [nvarchar](50) NOT NULL,
	[UsrPass] [nvarchar](50) NOT NULL,
	[IsAdmin] [bit] NOT NULL DEFAULT 0,
    [Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[ChangedBy] [int] NOT NULL,
	[ChangedAt] [bigint] NOT NULL)
create unique index i1 on [Users] ( [UsrInfId] )

CREATE TABLE [dbo].[UsersInfo](
	[UsrInfId] [int] NOT NULL PRIMARY KEY,	
	[FullName] [nvarchar](200) NULL,
	[AliaseName] [nvarchar](200) NULL,
	[Auth1] [nvarchar](200) NULL,	
	[ClntIDN] [nvarchar](100) NULL,
	[UsrType] [nvarchar](100) NULL,   -- משתמש / מאמן / לקוח  
	[GymId] [nvarchar](100) NULL,
	[ClntDesc] [nvarchar](300) NULL,
	[Status] [nvarchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)

CREATE TABLE [dbo].[Addresses](
	[AdrsId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ObjType] [nvarchar](100) NOT NULL,  -- UsersInfo  Id
	[ObjId] [int] NOT NULL DEFAULT 0,    -- UsersInfo  Id
	[Street] [nvarchar](100) NULL,
	[Street1] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[ZipCode] [nvarchar](100) NULL,
	[Address1] [nvarchar](100) NULL,
	[Address2] [nvarchar](100) NULL,
	[Address3] [nvarchar](100) NULL,
	[Address4] [nvarchar](100) NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [Addresses] ( [ObjType] )
create  index i2 on [Addresses] ( [ObjId] )
create  index i3 on [Addresses] ( [ObjType], [ObjId] )

CREATE TABLE [dbo].[Communications](
	[CommuId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ObjType] [nvarchar](100) NOT NULL,  -- UsersInfo  Id
	[ObjId] [int] NOT NULL DEFAULT 0,    -- UsersInfo  Id
	[CommuName] [nvarchar](100) NULL,
	[CommuType] [nvarchar](100) NULL,
	[CommuValue] [nvarchar](200) NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)	
create  index i1 on [Communications] ( [ObjType] )
create  index i2 on [Communications] ( [ObjId] )
create  index i3 on [Communications] ( [ObjType], [ObjId] )

CREATE TABLE [dbo].[Logs](
	[LgId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ObjType] [nvarchar](100) NOT NULL,  -- UsersInfo  Id
	[ObjId] [int] NOT NULL DEFAULT 0,    -- UsersInfo  Id
	[LgDate1] [int] NOT NULL DEFAULT 0,
	[LgType] [nvarchar](100) NULL,
	[LgText] [nvarchar](600) NULL,
	[LgDate2] [int] NOT NULL DEFAULT 0,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)	
create  index i1 on [Logs] ( [ObjType] )
create  index i2 on [Logs] ( [ObjId] )
create  index i3 on [Logs] ( [ObjType], [ObjId] )

CREATE TABLE [dbo].[TrainPlans](
    [TPId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UsrInfId] [int] NOT NULL DEFAULT 0,
	


	[FullName] [nvarchar](200) NULL,
	[AliaseName] [nvarchar](200) NULL,
	[Auth1] [nvarchar](200) NULL,	
	[ClntIDN] [nvarchar](100) NULL,
	[UsrType] [nvarchar](100) NULL,   -- משתמש / מאמן / לקוח  
	[GymId] [nvarchar](100) NULL,
	[ClntDesc] [nvarchar](300) NULL,
	[Status] [nvarchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)

CREATE TABLE [dbo].[Reminders](
	[RemId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ObjType] [nvarchar](100) NOT NULL,  -- UsersInfo  Id
	[ObjId] [int] NOT NULL DEFAULT 0,    -- UsersInfo  Id
	[PopUpAt_YYYYMMDDHHMM] [bigint] NOT NULL DEFAULT 0,
	[RemText] [nvarchar](300) NULL,
	[IsRecorsive] [bit] NOT NULL,
	[RecorsivType] [nvarchar](100) NULL,
	[RecorsivTime] [nvarchar](100) NULL,
	[RecorsivTime1] [nvarchar](100) NULL,
	[Frequence] [nvarchar](100) NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [Reminders] ( [ObjType] )
create  index i2 on [Reminders] ( [ObjId] )
create  index i3 on [Reminders] ( [ObjType], [ObjId] )






























CREATE TABLE [dbo].[Clients](
	[ClntId] [int] IDENTITY(1000001,1) NOT NULL PRIMARY KEY,
	[ClntIDN] [nvarchar](100) NULL,
	[CompCode] [nvarchar](100) NULL,
	[ClntType] [nvarchar](100) NULL,   -- לקוח מזדמן / לקוח קבוע / קבלן משנה   
	[FullName] [nvarchar](200) NOT NULL,
	[AliaseName] [nvarchar](200) NULL,
	[ClntDesc] [nvarchar](300) NULL,
	[Date1] [int] NOT NULL,  -- YYYYMMDD  
	[Date2] [int] NOT NULL,  -- YYYYMMDD  
	[Date3] [int] NOT NULL,  -- YYYYMMDD  
	[City] [nvarchar](100) NULL,
	[ZipCode] [nvarchar](100) NULL,
	[Address1] [nvarchar](200) NULL,
	[Address2] [nvarchar](200) NULL,
	[Address3] [nvarchar](200) NULL,
	[Address4] [nvarchar](200) NULL,
	[Worthy] [nvarchar](100) NOT NULL,
	[OwnerCode] [int] NOT NULL DEFAULT 0,   -- if he has vehicle ( vehicle owner )
	[Status] [nvarchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create unique index i1 on [Clients] ( [ClntId])

CREATE TABLE [dbo].[ClientDesc](
	[CDId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ClntId] [int] NOT NULL DEFAULT 0,
	[TName] [nvarchar](100) NULL,
	[TValue] [nvarchar](800) NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [ClientDesc] ( [ClntId])



CREATE TABLE [dbo].[Drivers](
	[DrvId] [int] IDENTITY(90001,1) NOT NULL PRIMARY KEY,
	[DrvIDN] [nvarchar](100) NULL,
	[CompCode] [nvarchar](100) NULL,
	[FullName] [nvarchar](200) NOT NULL,
	[AliaseName] [nvarchar](200) NULL,
	[DrvDesc] [nvarchar](200) NULL,
	[City] [nvarchar](100) NULL,
	[ZipCode] [nvarchar](100) NULL,
	[Address1] [nvarchar](100) NULL,
	[Address2] [nvarchar](100) NULL,
	[Address3] [nvarchar](100) NULL,
	[Worthy] [nvarchar](100) NOT NULL,
	[OwnerCode] [int] NOT NULL DEFAULT 0,   -- if he has vehicle ( vehicle owner )
	[PerHour] [decimal](12, 2) NOT NULL DEFAULT 0,
	[PerWaitHour] [decimal](12, 2) NOT NULL DEFAULT 0,
	[Status] [nvarchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create unique index i1 on [Drivers] ( [DrvId])

CREATE TABLE [dbo].[DriverDesc](
	[DId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[DrvId] [int] NOT NULL DEFAULT 0,
	[TName] [nvarchar](100) NULL,
	[TValue] [nvarchar](700) NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [DriverDesc] ( [DrvId])

CREATE TABLE [dbo].[DriverCommus](
	[DId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[DrvId] [int] NOT NULL DEFAULT 0,
	[PersonName] [nvarchar](100) NULL,
	[CommuType] [nvarchar](100) NULL,
	[CommuValue] [nvarchar](200) NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [DriverCommus] ( [DrvId])

CREATE TABLE [dbo].[DriverLincs](
	[DId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[DrvId] [int] NOT NULL DEFAULT 0,
	[LncName] [nvarchar](100) NULL,
	[LncText] [nvarchar](300) NULL,
	[LncExpireAt] [int] NOT NULL DEFAULT 0,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [DriverLincs] ( [DrvId])

CREATE TABLE [dbo].[Addresses](
	[AdrsId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ObjType] [nvarchar](100) NOT NULL,  -- Client Driver 
	[ObjId] [int] NOT NULL DEFAULT 0,    -- ClntId DrvId
	[Street] [nvarchar](100) NULL,
	[Street1] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[ZipCode] [nvarchar](100) NULL,
	[Address1] [nvarchar](100) NULL,
	[Address2] [nvarchar](100) NULL,
	[Address3] [nvarchar](100) NULL,
	[Address4] [nvarchar](100) NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [Addresses] ( [AdrsId])
create  index i2 on [Addresses] ( [ObjType])
create  index i3 on [Addresses] ( [ObjId])


CREATE TABLE [dbo].[Vehicles](
	[VehId] [int] IDENTITY(90001,1) NOT NULL PRIMARY KEY,
	[VehCode] [nvarchar](100) NULL,
	[VehType] [nvarchar](200) NOT NULL,	
	[NumPsngrs] [int] NOT NULL DEFAULT 0,
	[ManufYear] [int] NOT NULL,
	[Model] [nvarchar](200) NULL,
	[Type] [nvarchar](200) NULL,
	[Color] [nvarchar](200) NULL,
	[LockCode] [nvarchar](100) NULL,
	[DrvId] [int] NOT NULL DEFAULT 0,
	[CurrentKM] [int] NOT NULL DEFAULT 0,           -- current kelometers
	[NxtTimingAt] [int] NOT NULL,     -- kilometers
	[NxtTreatmntAt] [int] NOT NULL,   -- kilometers
	[NxtTreatmnt1At] [int] NOT NULL,   -- kilometers
	[NxtTreatmnt2At] [int] NOT NULL,   -- kilometers
	[NxtTreatmnt3At] [int] NOT NULL,   -- kilometers
	[NxtTestAt] [int] NOT NULL,       -- YYYYMMDD
	[NxtInsuranceAt] [int] NOT NULL,  -- YYYYMMDD
	[ActivatDate] [int] NOT NULL,     -- YYYYMMDD
    [BrakesDate] [int] NOT NULL,  -- YYYYMMDD  20210507
    [WinterDate] [int] NOT NULL,  -- YYYYMMDD
    [AdminDate] [int] NOT NULL ,  -- YYYYMMDD
    [AdminDate1] [int] NOT NULL,  -- YYYYMMDD
    [AdminDate2] [int] NOT NULL,  -- YYYYMMDD
    [AdminDate3] [int] NOT NULL,  -- YYYYMMDD
    [AdminDate4] [int] NOT NULL,  -- YYYYMMDD
    [Date1] [int] NOT NULL,  -- YYYYMMDD  buy date
    [Date2] [int] NOT NULL,  -- YYYYMMDD  in fleet 
    [Date3] [int] NOT NULL,  -- YYYYMMDD 
	[HasWIFI] [bit] NOT NULL,
	[Worthy] [nvarchar](100) NOT NULL,	
	[OwnerCode] [int] NOT NULL DEFAULT 0,   -- the vehicle owner  (if zero the vehicle is company ownered )
	[Status] [nvarchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create unique index i1 on [Vehicles] ( [VehId])

CREATE TABLE [dbo].[VehDesc](
	[VId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[VehId] [int] NOT NULL DEFAULT 0,
	[TName] [nvarchar](100) NULL,
	[TValue] [nvarchar](700) NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [VehDesc] ( [VehId])

CREATE TABLE [dbo].[VehicleCommus](
	[VId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[VehId] [int] NOT NULL DEFAULT 0,
	[PersonRole] [nvarchar](100) NULL,
	[PersonName] [nvarchar](100) NULL,
	[CommuType] [nvarchar](100) NULL,
	[CommuValue] [nvarchar](200) NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [VehicleCommus] ( [VehId] )

CREATE TABLE [dbo].[VehLincs](
	[VId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[VehId] [int] NOT NULL DEFAULT 0,
	[LncName] [nvarchar](100) NULL,
	[LncText] [nvarchar](300) NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [VehLincs] ( [VehId])


CREATE TABLE [dbo].[ClntOrders](
	[OrdId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[OrdNum] [int] NOT NULL DEFAULT 0,
	[OrdType] [nvarchar](100) NOT NULL,  -- איסוף  /  פיזור
	[OrdTime] [nvarchar](100) NOT NULL,  -- בוקר /  צהריים  / לילה
	[OrdPath] [nvarchar](100) NOT NULL,  -- ארוך /  קצר  / בינוני
	[ClntId] [int] NOT NULL DEFAULT 0,
	[DistanceKM] [int] NOT NULL DEFAULT 0,
	[LenghtMinutes] [int] NOT NULL DEFAULT 0,   -- זמן נסיעה בדקות
	[NumPsngrs] [int] NOT NULL DEFAULT 0,
	[FromLocation] [nvarchar](200) NOT NULL,
	[ToLocation] [nvarchar](200) NOT NULL,
	[StartHour] [int] NOT NULL DEFAULT 0,
	[AriveHour] [int] NOT NULL DEFAULT 0,
	[FreeAtHour] [int] NOT NULL DEFAULT 0,
	[TotalCost] [decimal](12, 2) NOT NULL DEFAULT 0,
	[DrvFee] [decimal](12, 2) NOT NULL DEFAULT 0,
	[VehFee] [decimal](12, 2) NOT NULL DEFAULT 0,
	[OthrCosts] [decimal](12, 2) NOT NULL DEFAULT 0,
	[NetProfit] [decimal](12, 2) NOT NULL DEFAULT 0,
	[CommitionFee] [decimal](12, 2) NOT NULL DEFAULT 0,
	[CommitionToId] [int] NOT NULL DEFAULT 0,
	[OrdIsActive] [bit] NOT NULL,
    [OrdIfExtraHours] [bit] NOT NULL,
	[IsSunday] [bit] NOT NULL,
	[IsMonday] [bit] NOT NULL,
	[IsTuesday] [bit] NOT NULL,
	[IsWednesday] [bit] NOT NULL,
	[IsThursday] [bit] NOT NULL,
	[IsFriday] [bit] NOT NULL,
	[IsSaturday] [bit] NOT NULL,
	[OrdDays] [nvarchar](100) NOT NULL,  -- Sunday Monday Tuesday Wednesday Thursday Friday Saturday
	[OrdDays1111111] [nvarchar](100) NOT NULL,  -- examples  (1001001 => Sunday Wednesday Saturday)   (1000000 => Sunday)   (0011000  Tuesday Wednesday)   (0000001 Saturday)
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [ClntOrders] ( [OrdId] )

CREATE TABLE [dbo].[Passengers](
	[PsngrId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[FullName] [nvarchar](200) NOT NULL,
	[Phone1] [nvarchar](100) NULL,
	[Phone2] [nvarchar](100) NULL,
	[Phone3] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[ZipCode] [nvarchar](100) NULL,
	[Address1] [nvarchar](100) NULL,
	[Address2] [nvarchar](100) NULL,
	[Address3] [nvarchar](100) NULL,
	[Status] [nvarchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create unique index i1 on [Passengers] ( [PsngrId])


CREATE TABLE [dbo].[PassengerCommus](
	[PCId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[PsngrId] [int] NOT NULL DEFAULT 0,
	[PersonRole] [nvarchar](100) NULL,  -- SENDER / Receiver
	[PersonName] [nvarchar](100) NULL,
	[CommuType] [nvarchar](100) NULL,
	[CommuValue] [nvarchar](200) NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [PassengerCommus] ( [PsngrId] )



CREATE TABLE [dbo].[DiaryColors](
	[DryCId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[DryCCondition] [nvarchar](100) NULL,
	[Color] [nvarchar](100) NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
	
CREATE TABLE [dbo].[DiaryShuttles](
	[DryId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[OrdId] [int] NOT NULL DEFAULT 0,
	[ShtlYear] [int] NOT NULL DEFAULT 0,         -- Shuttle year
	[ShtlMonth] [int] NOT NULL DEFAULT 0,        -- Shuttle month
	[ShtlDateYYYYMMDD] [int] NOT NULL DEFAULT 0, -- Shuttle date yyyymmdd
	[ShtlDay] [nvarchar](100) NOT NULL,  -- day of week
	[OrdType] [nvarchar](100) NOT NULL,  -- איסוף  /  פיזור
	[OrdTime] [nvarchar](100) NOT NULL,  -- בוקר /  צהריים  / לילה
	[OrdPath] [nvarchar](100) NOT NULL,  -- ארוך /  קצר  / בינוני
	[ClntId] [int] NOT NULL DEFAULT 0,
	[DistanceKM] [int] NOT NULL DEFAULT 0,
	[LenghtMinutes] [int] NOT NULL DEFAULT 0,   -- זמן נסיעה בדקות
	[NumPsngrs] [int] NOT NULL DEFAULT 0,
	[FromLocation] [nvarchar](200) NOT NULL,
	[ToLocation] [nvarchar](200) NOT NULL,
	[StartHour] [int] NOT NULL DEFAULT 0,
	[AriveHour] [int] NOT NULL DEFAULT 0,
	[FreeAtHour] [int] NOT NULL DEFAULT 0,
	[TotalCost] [decimal](12, 2) NOT NULL DEFAULT 0,
	[DrvId] [int] NOT NULL DEFAULT 0, 
	[VehId] [int] NOT NULL DEFAULT 0,
	[DrvFee] [decimal](12, 2) NOT NULL DEFAULT 0,
	[VehFee] [decimal](12, 2) NOT NULL DEFAULT 0,
	[OthrCosts] [decimal](12, 2) NOT NULL DEFAULT 0,
	[NetProfit] [decimal](12, 2) NOT NULL DEFAULT 0,
	[CommitionFee] [decimal](12, 2) NOT NULL DEFAULT 0,	
	[ClntAddishtionCredit] [decimal](12, 2) NOT NULL DEFAULT 0,
	[ClntAddishtionCharge] [decimal](12, 2) NOT NULL DEFAULT 0,
	[DrvAddishtionCredit] [decimal](12, 2) NOT NULL DEFAULT 0,
	[DrvAddishtionCharge] [decimal](12, 2) NOT NULL DEFAULT 0,
	[CommitionToId] [int] NOT NULL DEFAULT 0,
	[OrdIsActive] [bit] NOT NULL,
    [OrdIfExtraHours] [bit] NOT NULL,
	[PerHour] [decimal](12, 2) NOT NULL DEFAULT 0,
	[TotWaitMinutes] [int] NOT NULL DEFAULT 0,
	[PerWaitHour] [decimal](12, 2) NOT NULL DEFAULT 0,
	[IsChecked] [bit] NOT NULL,   -- with driver
	[IsApproved] [bit] NOT NULL,  -- with client
	[IsFinished] [bit] NOT NULL,  -- shuttle was done by driver
	[IsFeeChecked] [bit] NOT NULL,
	[IsMarkedOk] [bit] NOT NULL,
	[IsDoneOk] [bit] NOT NULL,
	[IsPayed] [bit] NOT NULL,
	[IsReadOnly] [bit] NOT NULL,
	[DryText] [nvarchar](300) NULL,  -- 
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)	
create index i1 on [DiaryShuttles] ( [ShtlYear])
create index i2 on [DiaryShuttles] ( [ShtlMonth])
create index i3 on [DiaryShuttles] ( [ShtlDateYYYYMMDD])
create index i4 on [DiaryShuttles] ( [ClntId])

CREATE TABLE [dbo].[DryShtlsPassengers](
	[DSPId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[DryId] [int] NOT NULL DEFAULT 0,
	[PsngrId] [int] NOT NULL DEFAULT 0,
	[FullName] [nvarchar](200) NOT NULL,
	[Phone1] [nvarchar](100) NULL,
	[Phone2] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[Address1] [nvarchar](100) NULL,
	[Address2] [nvarchar](100) NULL,
	[Status] [nvarchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create index i1 on [DryShtlsPassengers] ( [DryId])
create index i2 on [DryShtlsPassengers] ( [PsngrId])


CREATE TABLE [dbo].[Reminders](
	[RemId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ObjType] [nvarchar](100) NOT NULL,  -- Client Vehicle Driver 
	[ObjId] [int] NOT NULL DEFAULT 0,    -- ClntId VehId DrvId
	[PopUpAt_YYYYMMDDHHMM] [bigint] NOT NULL DEFAULT 0,
	[RemText] [nvarchar](300) NULL,
	[IsRecorsive] [bit] NOT NULL,
	[RecorsivType] [nvarchar](100) NULL,
	[Frequence] [nvarchar](100) NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)

	   	
CREATE TABLE [dbo].[Vacations](
	[VacId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ObjType] [nvarchar](100) NOT NULL,  -- Client Vehicle Driver 
	[ObjId] [int] NOT NULL DEFAULT 0,    -- ClntId VehId DrvId
	[StartAt_YYYYMMDDHHMM] [bigint] NOT NULL DEFAULT 0,
	[EndAt_YYYYMMDDHHMM] [bigint] NOT NULL DEFAULT 0,
	[VacText] [nvarchar](200) NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create index i1 on [Vacations] ( [ObjType])
create index i2 on [Vacations] ( [ObjId])
create index i3 on [Vacations] ( [StartAt_YYYYMMDDHHMM])
create index i4 on [Vacations] ( [EndAt_YYYYMMDDHHMM])


CREATE TABLE [dbo].[EventCategs](
	[ECId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ObjType] [nvarchar](100) NOT NULL,  -- Client Vehicle Driver 
	[MainCateg] [nvarchar](100) NOT NULL,
	[ScndCateg] [nvarchar](100) NOT NULL,
	[ThrdCateg] [nvarchar](100) NOT NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)


CREATE TABLE [dbo].[Events](
	[EvntId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ObjType] [nvarchar](100) NOT NULL,  -- Client Vehicle Driver 
	[ObjId] [int] NOT NULL DEFAULT 0,    -- ClntId VehId DrvId
	[EvntDate_YYYYMMDD] [int] NOT NULL DEFAULT 0,
	[IsReminder] [bit] NOT NULL,
	[PopUpAt_YYYYMMDDHHMM] [bigint] NOT NULL DEFAULT 0,
	[EvntText] [nvarchar](300) NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)


CREATE TABLE [dbo].[Docs](
	[DocId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[DocName] [nvarchar](100) NULL,
	[ObjType] [nvarchar](100) NOT NULL,  -- Client Vehicle Driver 
	[ObjId] [int] NOT NULL DEFAULT 0,    -- ClntId VehId DrvId
	[DocCtg] [nvarchar](100) NOT NULL,
	[DocText] [nvarchar](300) NULL,
	[DocPath] [nvarchar](300) NOT NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[ChangedBy] [int] NOT NULL,
	[ChangedAt] [bigint] NOT NULL)

CREATE TABLE [dbo].[Packages](
	[PkgId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[PkgText] [nvarchar](200) NOT NULL,
	[PkgWidth] [decimal](12, 2) NOT NULL DEFAULT 0,
	[PkgHeight] [decimal](12, 2) NOT NULL DEFAULT 0,
	[PkgWeight] [decimal](12, 2) NOT NULL DEFAULT 0,
	[PkgValue] [decimal](12, 2) NOT NULL DEFAULT 0,
	[ShippingPrice] [decimal](12, 2) NOT NULL DEFAULT 0,
	[ShippingDrvFee] [decimal](12, 2) NOT NULL DEFAULT 0,
	[ShippingVehFee] [decimal](12, 2) NOT NULL DEFAULT 0,
	[PickDate_YYYYMMDD] [int] NOT NULL DEFAULT 0,
	[PickAdrs][nvarchar](300) NOT NULL,
	[DlvrDate_YYYYMMDD] [int] NOT NULL DEFAULT 0,
	[DlvrAdrs][nvarchar](300) NOT NULL,
	[PkgOwner] [nvarchar](300) NULL,
	[PkgSender] [nvarchar](300) NULL,
	[PkgReceiver] [nvarchar](300) NULL,
	[PkgTrackCode] [nvarchar](100) NULL,
	[DryId] [int] NOT NULL DEFAULT 0,   -- On what tirp was sent
	[IsApproved] [bit] NOT NULL,  -- with client
	[IsChecked] [bit] NOT NULL,   -- with driver
	[IsPicked] [bit] NOT NULL,  -- shuttle was done by driver
	[IsDelivered] [bit] NOT NULL,
	[IsFinished] [bit] NOT NULL,
	[IsDoneOk] [bit] NOT NULL,
	[IsPayed] [bit] NOT NULL,
	[IsReadOnly] [bit] NOT NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[ChangedBy] [int] NOT NULL,
	[ChangedAt] [bigint] NOT NULL)
	
CREATE TABLE [dbo].[PackageCommus](
	[UPId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[PkgId] [int] NOT NULL DEFAULT 0,
	[PersonRole] [nvarchar](100) NULL,  -- SENDER / Receiver
	[PersonName] [nvarchar](100) NULL,
	[CommuType] [nvarchar](100) NULL,
	[CommuValue] [nvarchar](200) NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [PackageCommus] ( [PkgId] )


CREATE TABLE [dbo].[Users](
	[UsrId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UsrName] [nvarchar](50) NOT NULL,
	[UsrPass] [nvarchar](50) NOT NULL,
	[FullName] [nvarchar](200) NULL,
	[Email] [nvarchar](200) NULL,
	[Phone] [nvarchar](200) NULL,
	[Admin] [bit] NOT NULL,
    [Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[ChangedBy] [int] NOT NULL,
	[ChangedAt] [bigint] NOT NULL)
		

CREATE TABLE [dbo].[UserCommus](
	[UCId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UsrId] [int] NOT NULL DEFAULT 0,
	[PersonName] [nvarchar](100) NULL,
	[CommuType] [nvarchar](100) NULL,
	[CommuValue] [nvarchar](200) NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [UserCommus] ( [UsrId] )