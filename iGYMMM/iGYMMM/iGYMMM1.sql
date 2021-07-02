USE iGYMMM
GO

CREATE TABLE [dbo].[Gyms](
	[GymId] [int] IDENTITY(1000,1) NOT NULL PRIMARY KEY,
	[GymName] [nvarchar](200) NOT NULL,	
	[GymDescr] [nvarchar](500) NOT NULL,	
    [Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[ChangedBy] [int] NOT NULL,
	[ChangedAt] [bigint] NOT NULL)

CREATE TABLE [dbo].[Users](
	[UId] [int] IDENTITY(1000,1) NOT NULL PRIMARY KEY,
	[GymId] [int] NOT NULL DEFAULT 0,
	[UName] [nvarchar](50) NOT NULL,
	[UPass] [binary](100) NOT NULL,
	[UCode] [binary](100) NOT NULL,
	[UResetPass] [binary](100) NOT NULL,
	[U_GUID] [binary](100) NOT NULL,
	[OAuthLvl] [binary](100) NOT NULL,	
	[FullName] [nvarchar](200) NULL,
	[Admin] [bit] NOT NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[ChangedBy] [int] NOT NULL,
	[ChangedAt] [bigint] NOT NULL)	
create  index i1 on [Users] ( [GymId] )

CREATE TABLE [dbo].[Clients](
	[ClntId] [int] NOT NULL IDENTITY(100000,1) PRIMARY KEY,	
	[ClntNum] [nvarchar](200) NULL,
	[GymId] [int] NOT NULL DEFAULT 0,
	[FullName] [nvarchar](200) NULL,
	[AliaseName] [nvarchar](200) NULL,
	[TrnTmId] [int] NOT NULL DEFAULT 0, -- link to training team
	[TmGrpId] [int] NOT NULL DEFAULT 0, -- link to team group
	[FavIntrId] [int] NOT NULL DEFAULT 0,   -- מאמן מועדף
	[MustFavIntrId] [bit] NOT NULL DEFAULT 0,   -- רק מאמן מועדף
	[ClntIDN] [nvarchar](100) NULL,
	[PerHour1] [decimal](10, 2) NOT NULL DEFAULT 0,    -- מחיר לשעה
	[PerHour2] [decimal](10, 2) NOT NULL DEFAULT 0,    -- מחיר לשעה
	[PerTrip1] [decimal](10, 2) NOT NULL DEFAULT 0,      -- נסיעות
	[PerTrip2] [decimal](10, 2) NOT NULL DEFAULT 0,      -- נסיעות
	[ClntColor] [nvarchar](200) NULL,
	[CWorthy] [nvarchar](100) NOT NULL,
	[CRate] [nvarchar](100) NOT NULL,
	[Status] [nvarchar](100) NOT NULL,	
	[UName] [nvarchar](50) NOT NULL,
	[UPass] [binary](100) NOT NULL,
	[UCode] [binary](100) NOT NULL,
	[UResetPass] [binary](100) NOT NULL,
	[U_GUID] [binary](100) NOT NULL,
	[OAuthLvl] [binary](100) NOT NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [Clients] ( [GymId] )

CREATE TABLE [dbo].[Instructors](
	[InstrId] [int] NOT NULL IDENTITY(100000,1) PRIMARY KEY,	
	[InstrNum] [nvarchar](200) NULL,
	[GymId] [int] NOT NULL DEFAULT 0,
	[FullName] [nvarchar](200) NULL,
	[AliaseName] [nvarchar](200) NULL,
	[InstrIDN] [nvarchar](100) NULL,
	[PerHour1] [decimal](10, 2) NOT NULL DEFAULT 0,    -- מחיר לשעה
	[PerHour2] [decimal](10, 2) NOT NULL DEFAULT 0,    -- מחיר לשעה
	[PerWaitHour] [decimal](10, 2) NOT NULL DEFAULT 0,
	[PerTrip1] [decimal](10, 2) NOT NULL DEFAULT 0,      -- נסיעות
	[PerTrip2] [decimal](10, 2) NOT NULL DEFAULT 0,      -- נסיעות
	[InstrColor] [nvarchar](200) NULL,
	[CWorthy] [nvarchar](100) NOT NULL,
	[CRate] [nvarchar](100) NOT NULL,
	[Status] [nvarchar](100) NOT NULL,
	[UName] [nvarchar](50) NOT NULL,
	[UPass] [binary](100) NOT NULL,
	[UCode] [binary](100) NOT NULL,
	[UResetPass] [binary](100) NOT NULL,
	[U_GUID] [binary](100) NOT NULL,
	[OAuthLvl] [binary](100) NOT NULL,	
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [Instructors] ( [GymId] )

CREATE TABLE [dbo].[InstrsAttendance](
	[IAtnId] [int] NOT NULL IDENTITY(1,1) PRIMARY KEY,	
	[InstrId] [int] NOT NULL DEFAULT 0,
	[GymId] [int] NOT NULL DEFAULT 0,
	[IAShiftDate] [int] NOT NULL DEFAULT 0,      -- YYYYMMDD  
	[IAShiftStart] [bigint] NOT NULL DEFAULT 0,  -- YYYYMMDDHHMM hour that shift start   
	[IAShiftEnd] [bigint] NOT NULL DEFAULT 0,    -- YYYYMMDDHHMM hour that shift start 
	[IAShiftPrcntg] [int] NOT NULL DEFAULT 0,      -- shift persentage 
	[IAShiftCredit] [decimal](10, 2) NOT NULL DEFAULT 0,      -- bounos  
	[IAShiftCharge] [decimal](10, 2) NOT NULL DEFAULT 0,      -- chargs
	[Status] [nvarchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [InstrsAttendance] ( [GymId] )
create  index i2 on [InstrsAttendance] ( [InstrId] )

CREATE TABLE [dbo].[Addresses](
	[AdrsId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ObjType] [nvarchar](100) NOT NULL,  -- GYM   USER CLIENT INSTRUCTOR
	[ObjId] [int] NOT NULL DEFAULT 0,    -- GymId UId  ClntId InstrId
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
create  index i4 on [Addresses] ( [GymId] )

CREATE TABLE [dbo].[Communications](
	[CommuId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ObjType] [nvarchar](100) NOT NULL,  -- GYM   USER CLIENT INSTRUCTOR
	[ObjId] [int] NOT NULL DEFAULT 0,    -- GymId UId  ClntId InstrId
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
create  index i4 on [Communications] ( [GymId] )

CREATE TABLE [dbo].[Descriptions](
	[DiscrId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ObjType] [nvarchar](100) NOT NULL,  -- GYM   USER CLIENT INSTRUCTOR
	[ObjId] [int] NOT NULL DEFAULT 0,    -- GymId UId  ClntId InstrId
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
create  index i4 on [Descriptions] ( [GymId] )

CREATE TABLE [dbo].[Logs](
	[LgId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ObjType] [nvarchar](100) NOT NULL,  -- GYM   USER CLIENT INSTRUCTOR
	[ObjId] [int] NOT NULL DEFAULT 0,    -- GymId UId  ClntId InstrId
	[GymId] [int] NOT NULL DEFAULT 0,
	[LgDate1] [int] NOT NULL DEFAULT 0,
	[LgType] [nvarchar](100) NULL,
	[LgText] [nvarchar](600) NULL,
	[LgDate2] [int] NOT NULL DEFAULT 0,
	[LgColor] [nvarchar](200) NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)	
create  index i1 on [Logs] ( [ObjType] )
create  index i2 on [Logs] ( [ObjId] )
create  index i3 on [Logs] ( [ObjType], [ObjId] )
create  index i4 on [Logs] ( [GymId] )

CREATE TABLE [dbo].[Reminders](
	[RemId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ObjType] [nvarchar](100) NOT NULL,  -- GYM   USER CLIENT INSTRUCTOR
	[ObjId] [int] NOT NULL DEFAULT 0,    -- GymId UId  ClntId InstrId
	[GymId] [int] NOT NULL DEFAULT 0,
	[PopUpAt_YYYYMMDDHHMM] [bigint] NOT NULL DEFAULT 0,
	[RemText] [nvarchar](300) NULL,
	[IsRecorsive] [bit] NOT NULL,
	[RecorsivType] [nvarchar](100) NULL,
	[RecorsivTime] [nvarchar](100) NULL,
	[RecorsivTime1] [nvarchar](100) NULL,
	[Frequence] [nvarchar](100) NULL,
	[RemColor] [nvarchar](200) NULL,
	[Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [Reminders] ( [ObjType] )
create  index i2 on [Reminders] ( [ObjId] )
create  index i3 on [Reminders] ( [ObjType], [ObjId] )
create  index i4 on [Reminders] ( [GymId] )

CREATE TABLE [dbo].[TrainingTeams](
	[TrnTmId] [int] IDENTITY(100000,1) NOT NULL PRIMARY KEY,
	[GymId] [int] NOT NULL DEFAULT 0,
	[TrnTmName] [nvarchar](200) NOT NULL,
	[TrnTmDescr] [nvarchar](500) NOT NULL,
	[InstrIdCount] [int] NOT NULL DEFAULT 0,   -- מספר מאמנים בקבוצה
	[OnePayer] [bit] NOT NULL DEFAULT 0,
	[TrnTmColor] [nvarchar](200) NULL,	
    [Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[ChangedBy] [int] NOT NULL,
	[ChangedAt] [bigint] NOT NULL)
create  index i1 on [TrainingTeams] ( [GymId] )

CREATE TABLE [dbo].[TeamGroups](
	[TmGrpId] [int] IDENTITY(10000,1) NOT NULL PRIMARY KEY,
	[GymId] [int] NOT NULL DEFAULT 0,
	[TrnTmId] [int] NOT NULL DEFAULT 0,
	[TmGrpName] [nvarchar](200) NOT NULL,
	[TmGrpDescr] [nvarchar](500) NOT NULL,
	[FavIntrId] [int] NOT NULL DEFAULT 0,       -- מאמן מועדף
	[MustFavIntrId] [bit] NOT NULL DEFAULT 0,   -- רק מאמן מועדף
	[TotalGrpFee1] [decimal](10, 2) NOT NULL,
	[TotalGrpFee2] [decimal](10, 2) NOT NULL,
	[TotalGrpFee3] [decimal](10, 2) NOT NULL,
	[AllGrpPymntDone] [bit] NOT NULL DEFAULT 0,
	[TmGrpColor] [nvarchar](200) NULL,
    [Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[ChangedBy] [int] NOT NULL,
	[ChangedAt] [bigint] NOT NULL)
create  index i1 on [TeamGroups] ( [GymId] )
create  index i2 on [TeamGroups] ( [TrnTmId] )

	-- link with clients table via [TmGrpId] at table clients
CREATE TABLE [dbo].[TeamGroupsClients](
	[ClntGrpId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[GymId] [int] NOT NULL DEFAULT 0,
	[ClntId] [int] NOT NULL DEFAULT 0,
	[TmGrpId] [int] NOT NULL DEFAULT 0,
    [Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[ChangedBy] [int] NOT NULL,
	[ChangedAt] [bigint] NOT NULL)
create  index i1 on [TeamGroupsClients] ( [GymId] )
create  index i2 on [TeamGroupsClients] ( [ClntId] )
create  index i3 on [TeamGroupsClients] ( [TmGrpId] )

CREATE TABLE [dbo].[Packages](
    [PkgId] [int] IDENTITY(100000,1) NOT NULL PRIMARY KEY,
	[PkgName] [nvarchar](200) NOT NULL,
	[PkgType] [nvarchar](200) NOT NULL,
	[GymId] [int] NOT NULL DEFAULT 0,
	[TrnTmId] [int] NOT NULL DEFAULT 0,
	[PkDateStart] [int] NOT NULL DEFAULT 0,  -- YYYYMMDD  
	[PkDateStart1] [int] NOT NULL DEFAULT 0,  -- YYYYMMDD  
	[PkDateEnd] [int] NOT NULL DEFAULT 0,    -- YYYYMMDD  
	[IsPeriodFixed] [bit] NOT NULL DEFAULT 0,  -- לפי זמן
	[PkTrnAmount] [int] NOT NULL DEFAULT 0,   -- number of trainings / treatment	Total
	[PkTrnAmountWeek] [int] NOT NULL DEFAULT 0,   -- number of trainings / treatment in week	
	[TotalFee1] [decimal](10, 2) NOT NULL,
	[TotalFee2] [decimal](10, 2) NOT NULL,
	[TotalFee3] [decimal](10, 2) NOT NULL,
	[AllGrpPymntDone] [bit] NOT NULL DEFAULT 0,
	[PkgColor] [nvarchar](200) NULL,
	[Status] [nvarchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [Packages] ( [GymId] )
create  index i2 on [Packages] ( [TrnTmId] )
create  index i3 on [Packages] ( [PkDateStart] )

CREATE TABLE [dbo].[TrnTmPackages](
	[TrnTmPkgId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[GymId] [int] NOT NULL DEFAULT 0,
	[TrnTmId] [int] NOT NULL DEFAULT 0,
	[PkgId] [int] NOT NULL DEFAULT 0,
    [Status] [nvarchar](100) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [bigint] NOT NULL,
	[ChangedBy] [int] NOT NULL,
	[ChangedAt] [bigint] NOT NULL)
create  index i1 on [TrnTmPackages] ( [GymId] )
create  index i2 on [TrnTmPackages] ( [TrnTmId] )
create  index i3 on [TrnTmPackages] ( [PkgId] )
create  index i4 on [TrnTmPackages] ( [TrnTmId], [PkgId] )

CREATE TABLE [dbo].[PkgRequrmnts](
    [PkgReqId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[PkgId] [int] NOT NULL DEFAULT 0,	
	[GymId] [int] NOT NULL DEFAULT 0,	
	[PkReqDOW] [nvarchar](200) NULL, -- day of week
	[PkReqDayTime] [nvarchar](200) NULL, -- [Morning (06-11)] [Afternoon (12-17)] [Eveinig (18-23)]
	[PkReqHour1] [int] NOT NULL DEFAULT 0,
	[PkReqTrnTime] [int] NOT NULL DEFAULT 1,    -- one hour for train	
	[Status] [nvarchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [PkgRequrmnts] ( [PkgId] )
create  index i3 on [PkgRequrmnts] ( [GymId] )

CREATE TABLE [dbo].[PkgPayments](
    [PkgPymntId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [PkgId] [int] NOT NULL DEFAULT 0,
	[GymId] [int] NOT NULL DEFAULT 0,
	[ClntId] [int] NOT NULL DEFAULT 0,
	[PkgPymntDate] [int] NOT NULL DEFAULT 0,  -- YYYYMMDD  
	[TotalFee] [decimal](10, 2) NOT NULL,
	[PaymentDone] [bit] NOT NULL DEFAULT 0,
	[Status] [nvarchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [PkgPayments] ( [PkgId] )
create  index i2 on [PkgPayments] ( [GymId] )
create  index i3 on [PkgPayments] ( [ClntId] )

CREATE TABLE [dbo].[DiaryTeams](
    [DryTmId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[GymId] [int] NOT NULL DEFAULT 0,
	[TrnTmId] [int] NOT NULL DEFAULT 0,
	[PkgId] [int] NOT NULL DEFAULT 0,
	[PkgReqId] [int] NOT NULL DEFAULT 0,
	[TrnDate] [int] NOT NULL DEFAULT 0,     -- YYYYMMDD
	[TrnHour] [int] NOT NULL DEFAULT 0,     -- 0- 23 hours
	[PkReqDOW] [nvarchar](200) NULL, -- day of week
	[PkReqDayTime] [nvarchar](200) NULL, -- [Morning (06-11)] [Afternoon (12-17)] [Eveinig (18-23)]
	[PkReqHour1] [int] NOT NULL DEFAULT 0,
	[PkReqHour2] [int] NOT NULL DEFAULT 0,
	[PkReqTrnTime] [int] NOT NULL DEFAULT 1,        -- one hour for train
	[ActualTrnDOW] [nvarchar](200) NULL,            -- day of week
	[ActualTrnDayTime] [nvarchar](200) NULL,        -- [Morning (06-11)] [Afternoon (12-17)] [Eveinig (18-23)]
	[ActualTrnHour1] [int] NOT NULL DEFAULT 0,
	[ActualTrnHour2] [int] NOT NULL DEFAULT 0,
	[ActualTrnTrnTime] [int] NOT NULL DEFAULT 1,    -- one hour for train	
	[ColorView] [nvarchar](200) NULL,     --- alert view
	[Status] [nvarchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [DiaryTeams] ( [GymId] )
create  index i2 on [DiaryTeams] ( [TrnTmId] )
create  index i3 on [DiaryTeams] ( [PkgId] )
create  index i4 on [DiaryTeams] ( [PkgReqId] )
create  index i5 on [DiaryTeams] ( [TrnDate] )


CREATE TABLE [dbo].[DiaryInstrs](
    [DryInstrId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[GymId] [int] NOT NULL DEFAULT 0,
    [DryTmId] [int] NOT NULL DEFAULT 0,
	[PkgId] [int] NOT NULL DEFAULT 0,
	[TrnDate] [int] NOT NULL DEFAULT 0,     -- YYYYMMDD 
	[TrnHour] [int] NOT NULL DEFAULT 0,     -- 0 - 23 hours
	[PkReqDOW] [nvarchar](200) NULL, -- day of week
	[PkReqDayTime] [nvarchar](200) NULL, -- [Morning (06-11)] [Afternoon (12-17)] [Eveinig (18-23)]
	[PkReqHour1] [int] NOT NULL DEFAULT 0,
	[PkReqHour2] [int] NOT NULL DEFAULT 0,
	[PkReqTrnTime] [int] NOT NULL DEFAULT 1,        -- one hour for train
	[ActualTrnDOW] [nvarchar](200) NULL,            -- day of week
	[ActualTrnDayTime] [nvarchar](200) NULL,        -- [Morning (06-11)] [Afternoon (12-17)] [Eveinig (18-23)]
	[ActualTrnHour1] [int] NOT NULL DEFAULT 0,
	[ActualTrnHour2] [int] NOT NULL DEFAULT 0,
	[ActualTrnTrnTime] [int] NOT NULL DEFAULT 1,    -- one hour for train
	[PlannedInstrId] [int] NOT NULL DEFAULT 0, 
	[ActualInstrId] [int] NOT NULL DEFAULT 0, 
	[PerHour1] [decimal](10, 2) NOT NULL DEFAULT 0,    -- מחיר לשעה
	[PerHour2] [decimal](10, 2) NOT NULL DEFAULT 0,    -- מחיר לשעה
	[PerWaitHour] [decimal](10, 2) NOT NULL DEFAULT 0,
	[PerTrip1] [decimal](10, 2) NOT NULL DEFAULT 0,      -- נסיעות
	[PerTrip2] [decimal](10, 2) NOT NULL DEFAULT 0,      -- נסיעות
	[ChargeTot] [decimal](10, 2) NOT NULL DEFAULT 0,      -- חיוב למאמן
	[CreditTot] [decimal](10, 2) NOT NULL DEFAULT 0,      -- בונוס למאמן
	[TrStatus] [nvarchar](100) NOT NULL,  -- scheduled Done Finished
	[Status] [nvarchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [DiaryInstrs] ( [GymId] )
create  index i2 on [DiaryInstrs] ( [DryTmId] )
create  index i3 on [DiaryInstrs] ( [PkgId] )
create  index i4 on [DiaryInstrs] ( [TrnDate] )
create  index i5 on [DiaryInstrs] ( [PlannedInstrId] )
create  index i6 on [DiaryInstrs] ( [ActualInstrId] )

CREATE TABLE [dbo].[DiaryClnts](
    [DryClntId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[GymId] [int] NOT NULL DEFAULT 0,
    [DryTmId] [int] NOT NULL DEFAULT 0,
	[DryInstrId] [int] NOT NULL DEFAULT 0,
	[PkgId] [int] NOT NULL DEFAULT 0,
	[TrnDate] [int] NOT NULL DEFAULT 0,     -- YYYYMMDD
	[TrnHour] [int] NOT NULL DEFAULT 0,     -- 0- 23 hours
	[PkReqDOW] [nvarchar](200) NULL, -- day of week
	[PkReqDayTime] [nvarchar](200) NULL, -- [Morning (06-11)] [Afternoon (12-17)] [Eveinig (18-23)]
	[PkReqHour1] [int] NOT NULL DEFAULT 0,
	[PkReqHour2] [int] NOT NULL DEFAULT 0,
	[PkReqTrnTime] [int] NOT NULL DEFAULT 1,        -- one hour for train
	[ActualTrnDOW] [nvarchar](200) NULL,            -- day of week
	[ActualTrnDayTime] [nvarchar](200) NULL,        -- [Morning (06-11)] [Afternoon (12-17)] [Eveinig (18-23)]
	[ActualTrnHour1] [int] NOT NULL DEFAULT 0,
	[ActualTrnHour2] [int] NOT NULL DEFAULT 0,
	[ActualTrnTrnTime] [int] NOT NULL DEFAULT 1,    -- one hour for train
	[PlannedInstrId] [int] NOT NULL DEFAULT 0, 
	[ActualInstrId] [int] NOT NULL DEFAULT 0, 
	[ClntId] [int] NOT NULL DEFAULT 0,
	[PerHour1] [decimal](10, 2) NOT NULL DEFAULT 0,    -- מחיר לשעה
	[PerHour2] [decimal](10, 2) NOT NULL DEFAULT 0,    -- מחיר לשעה
	[PerTrip1] [decimal](10, 2) NOT NULL DEFAULT 0,      -- נסיעות
	[PerTrip2] [decimal](10, 2) NOT NULL DEFAULT 0,      -- נסיעות
    [ChargeTot] [decimal](10, 2) NOT NULL DEFAULT 0,      -- חיוב למאמן
	[CreditTot] [decimal](10, 2) NOT NULL DEFAULT 0,      -- בונוס למאמן
	[TrStatus] [nvarchar](100) NOT NULL,  -- scheduled Done Finished
	[Status] [nvarchar](100) NOT NULL,
	[CreatedBy] [int] NOT NULL DEFAULT 0,
	[CreatedAt] [bigint] NOT NULL DEFAULT 0,
	[ChangedBy] [int] NOT NULL DEFAULT 0,
	[ChangedAt] [bigint] NOT NULL DEFAULT 0)
create  index i1 on [DiaryClnts] ( [GymId] )
create  index i2 on [DiaryClnts] ( [DryTmId] )
create  index i3 on [DiaryClnts] ( [PkgId] )
create  index i4 on [DiaryClnts] ( [TrnDate] )
create  index i5 on [DiaryClnts] ( [PlannedInstrId] )
create  index i6 on [DiaryClnts] ( [ActualInstrId] )
create  index i7 on [DiaryClnts] ( [ClntId] )