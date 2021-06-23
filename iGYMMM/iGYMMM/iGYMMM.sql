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

CREATE TABLE [dbo].[TrainGroups](
	[TrnGrpId] [int] IDENTITY(100000,1) NOT NULL PRIMARY KEY,
	[GymId] [int] NOT NULL DEFAULT 0,
	[TGName] [nvarchar](200) NOT NULL,
	[UsrInfIdCount] [int] NOT NULL DEFAULT 0,   -- מספר מאמנים בקבוצה
	[TGDescr] [nvarchar](500) NOT NULL,
	[OnePayer] [bit] NOT NULL DEFAULT 1,
    [Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[ChangedBy] [int] NOT NULL,
	[ChangedAt] [bigint] NOT NULL)

CREATE TABLE [dbo].[TrnGrpsUsers](
	[TrnGrpUsrId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[TrnGrpId] [int] NOT NULL DEFAULT 0,
	[GymId] [int] NOT NULL DEFAULT 0,
	[UsrInfId] [int] NOT NULL DEFAULT 0,
	[UsrType] [nvarchar](100) NULL,   --   מאמן    	
    [Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[ChangedBy] [int] NOT NULL,
	[ChangedAt] [bigint] NOT NULL)
create  index i1 on [TrnGrpsUsers] ( [TrnGrpId] )
create  index i2 on [TrnGrpsUsers] ( [UsrInfId] )
create  index i3 on [TrnGrpsUsers] ( [UsrInfId], [UsrType] )


CREATE TABLE [dbo].[Users](
	[UsrId] [int] IDENTITY(100000,1) NOT NULL PRIMARY KEY,
	[TrnGrpId] [int] NOT NULL DEFAULT 0,
	[GymId] [int] NOT NULL DEFAULT 0,
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
	[UsrType] [nvarchar](100) NULL,   -- משתמש / מאמן / לקוח  
	[FullName] [nvarchar](200) NULL,
	[AliaseName] [nvarchar](200) NULL,
	[FavUsrInfId] [int] NOT NULL DEFAULT 0,   -- מאמן מועדף
	[MustFavUsrInfId] [bit] NOT NULL DEFAULT 0,   -- רק מאמן מועדף
	[Auth1] [nvarchar](200) NULL,	
	[ClntIDN] [nvarchar](100) NULL,
	[ClntDesc] [nvarchar](300) NULL,
	[PerHour1] [decimal](10, 2) NOT NULL DEFAULT 0,    -- שכר לשעה
	[PerHour2] [decimal](10, 2) NOT NULL DEFAULT 0,    -- שכר לשעה
	[PerWaitHour] [decimal](10, 2) NOT NULL DEFAULT 0,
	[PerTrip1] [decimal](10, 2) NOT NULL DEFAULT 0,      -- נסיעות
	[PerTrip2] [decimal](10, 2) NOT NULL DEFAULT 0,      -- נסיעות
	[Status] [nvarchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)

CREATE TABLE [dbo].[Addresses](
	[AdrsId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ObjType] [nvarchar](100) NOT NULL,  -- UsersInfo  Id
	[ObjId] [int] NOT NULL DEFAULT 0,    -- UsersInfo  Id
	[GymId] [int] NOT NULL DEFAULT 0,
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
	[GymId] [int] NOT NULL DEFAULT 0,
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

CREATE TABLE [dbo].[Descriptions](
	[DiscrId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ObjType] [nvarchar](100) NOT NULL,  -- UsersInfo  Id
	[ObjId] [int] NOT NULL DEFAULT 0,    -- UsersInfo  Id
	[GymId] [int] NOT NULL DEFAULT 0,
	[DiscrTyp] [nvarchar](100) NULL,
	[DiscrText] [nvarchar](800) NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [Descriptions] ( [ObjType] )
create  index i2 on [Descriptions] ( [ObjId] )
create  index i3 on [Descriptions] ( [ObjType], [ObjId] )

CREATE TABLE [dbo].[Logs](
	[LgId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ObjType] [nvarchar](100) NOT NULL,  -- UsersInfo  Id
	[ObjId] [int] NOT NULL DEFAULT 0,    -- UsersInfo  Id
	[GymId] [int] NOT NULL DEFAULT 0,
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

CREATE TABLE [dbo].[Packages](
    [PkgId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[TrnGrpId] [int] NOT NULL DEFAULT 0,
	[GymId] [int] NOT NULL DEFAULT 0,	
	[PkDateStart] [int] NOT NULL DEFAULT 0,  -- YYYYMMDD  
	[PkDateEnd] [int] NOT NULL DEFAULT 0,    -- YYYYMMDD  
	[PkAmount] [int] NOT NULL DEFAULT 0,   -- number of trainings / treatments
	[PkDay] [nvarchar](200) NULL,
	[PkDayTime] [nvarchar](200) NULL, -- [Morning (06-11)] [Afternoon (12-17)] [Eveinig (18-23)]
	[PkHour1] [nvarchar](200) NULL,
	[PkTrainTime] [int] NOT NULL DEFAULT 1,    -- one hour for train
	[TotalFee1] [decimal](10, 2) NOT NULL,
	[TotalFee2] [decimal](10, 2) NOT NULL,
	[AllGrpPymntDone] [bit] NOT NULL DEFAULT 0,
	[Status] [nvarchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [Packages] ( [TrnGrpId] )
create  index i2 on [Packages] ( [PkDateStart] )

CREATE TABLE [dbo].[PkgRequrmnts](
    [PkgReqId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[PkgId] [int] NOT NULL DEFAULT 0,	
	[PkDay] [nvarchar](200) NULL,
	[PkDayTime] [nvarchar](200) NULL, -- [Morning (06-11)] [Afternoon (12-17)] [Eveinig (18-23)]
	[PkHour1] [nvarchar](200) NULL,
	[PkTrainTime] [int] NOT NULL DEFAULT 1,    -- one hour for train	
	[Status] [nvarchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [PkgRequrmnts] ( [PkgId] )

CREATE TABLE [dbo].[PackagesPymnts](
    [PkgPymntId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [PkgId] [int] NOT NULL DEFAULT 0,
	[GymId] [int] NOT NULL DEFAULT 0,
	[UsrInfId] [int] NOT NULL DEFAULT 0,
	[UsrType] [nvarchar](100) NULL,   -- משתמש / מאמן / לקוח  
	[PkgPymntDate] [int] NOT NULL DEFAULT 0,  -- YYYYMMDD  
	[TotalFee] [decimal](10, 2) NOT NULL,
	[PaymentDone] [bit] NOT NULL DEFAULT 0,
	[Status] [nvarchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [PackagesPymnts] ( [PkgId] )
create  index i2 on [PackagesPymnts] ( [UsrInfId], [UsrType] )
create  index i3 on [PackagesPymnts] ( [PkgId], [UsrInfId], [UsrType] )


CREATE TABLE [dbo].[TrainGrps](
    [TrnGrpId] [int] IDENTITY(1000001,1) NOT NULL PRIMARY KEY,
	[PkgId] [int] NOT NULL DEFAULT 0,
	[TrDate] [int] NOT NULL DEFAULT 0,     -- YYYYMMDD  
	[TrDay] [nvarchar](100) NOT NULL, 
	[TrHour1][int] NOT NULL DEFAULT 0,     -- train start
	[TrHour2][int] NOT NULL DEFAULT 0,     -- train end
	--[UsrInfId] [int] NOT NULL DEFAULT 0,  -- מאמן אישי  
	--[UsrType] [nvarchar](100) NULL,       --  מאמן  
	--[HourlyRate1] [decimal](10, 2) NOT NULL DEFAULT 0,    -- שכר לשעה
	--[HourlyRate2] [decimal](10, 2) NOT NULL DEFAULT 0,    -- שכר לשעה
	--[TripRate1] [decimal](10, 2) NOT NULL DEFAULT 0,      -- נסיעות
	--[TripRate2] [decimal](10, 2) NOT NULL DEFAULT 0,      -- נסיעות
	--[ChargeTot] [decimal](10, 2) NOT NULL DEFAULT 0,      -- חיוב למאמן
	--[CreditTot] [decimal](10, 2) NOT NULL DEFAULT 0,      -- בונוס למאמן
	--[TrStatus] [nvarchar](100) NOT NULL,  -- scheduled Done Finished
	[Deleted] [bit] NOT NULL DEFAULT 0,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)	
create  index i1 on [TrainGrps] ( [TrDate] )
create  index i2 on [TrainGrps] ( [UsrInfId] )
create  index i3 on [TrainGrps] ( [UsrInfId], [UsrType] )


CREATE TABLE [dbo].[TrainHdrs](
    [TrnHdrId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[TrnGrpId] [int] NOT NULL DEFAULT 0,
	[PkgId] [int] NOT NULL DEFAULT 0,
	[TrDate] [int] NOT NULL DEFAULT 0,     -- YYYYMMDD  	
	[UsrInfId] [int] NOT NULL DEFAULT 0,  -- מאמן אישי  
	[UsrType] [nvarchar](100) NULL,       --  מאמן  
	[HourlyRate1] [decimal](10, 2) NOT NULL DEFAULT 0,    -- שכר לשעה
	[HourlyRate2] [decimal](10, 2) NOT NULL DEFAULT 0,    -- שכר לשעה
	[TripRate1] [decimal](10, 2) NOT NULL DEFAULT 0,      -- נסיעות
	[TripRate2] [decimal](10, 2) NOT NULL DEFAULT 0,      -- נסיעות
	[ChargeTot] [decimal](10, 2) NOT NULL DEFAULT 0,      -- חיוב למאמן
	[CreditTot] [decimal](10, 2) NOT NULL DEFAULT 0,      -- בונוס למאמן
	[TrStatus] [nvarchar](100) NOT NULL,  -- scheduled Done Finished
	[Deleted] [bit] NOT NULL DEFAULT 0,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)	
create  index i1 on [TrainHdrs] ( [TrDate] )
create  index i2 on [TrainHdrs] ( [UsrInfId] )
create  index i3 on [TrainHdrs] ( [UsrInfId], [UsrType] )


CREATE TABLE [dbo].[TrainItms](
    [TrnItmId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[TrnGrpId] [int] NOT NULL DEFAULT 0,
	[PkgId] [int] NOT NULL DEFAULT 0,
	[TrnHdrId] [int] NOT NULL DEFAULT 0,
	[TrDate] [int] NOT NULL DEFAULT 0,     -- YYYYMMDD  
	[UsrInfId] [int] NOT NULL DEFAULT 0,   -- לקוח  
	[UsrType] [nvarchar](100) NULL,        --  לקוח      
	[TrStatus] [nvarchar](100) NOT NULL,  -- scheduled Done Finished
	[Deleted] [bit] NOT NULL DEFAULT 0,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)	
create  index i1 on [TrainItms] ( [TrDate] )
create  index i2 on [TrainItms] ( [UsrInfId] )
create  index i3 on [TrainItms] ( [UsrInfId], [UsrType] )

CREATE TABLE [dbo].[Reminders](
	[RemId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ObjType] [nvarchar](100) NOT NULL,  -- UsersInfo  Id
	[ObjId] [int] NOT NULL DEFAULT 0,    -- UsersInfo  Id
	[GymId] [int] NOT NULL DEFAULT 0,
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

CREATE TABLE [dbo].[Vacations](
	[VacId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ObjType] [nvarchar](100) NOT NULL,  -- UsersInfo  Id
	[ObjId] [int] NOT NULL DEFAULT 0,    -- UsersInfo  Id
	[GymId] [int] NOT NULL DEFAULT 0,
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
	[ObjType] [nvarchar](100) NOT NULL,  -- UsersInfo  Id
	[GymId] [int] NOT NULL DEFAULT 0,
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
	[ObjType] [nvarchar](100) NOT NULL,  -- UsersInfo  Id
	[ObjId] [int] NOT NULL DEFAULT 0,    -- UsersInfo  Id
	[GymId] [int] NOT NULL DEFAULT 0,
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
	[ObjType] [nvarchar](100) NOT NULL,  -- UsersInfo  Id
	[ObjId] [int] NOT NULL DEFAULT 0,    -- UsersInfo  Id
	[GymId] [int] NOT NULL DEFAULT 0,
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