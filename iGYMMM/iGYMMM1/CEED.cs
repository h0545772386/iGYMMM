using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iGYMMM1
{
    public partial class CEED
    {
        public static void ceed()
        {
            using (var db = new Model1())
            {
                //ceed_Gym(db);
                //ceed_Instructor(db);
                //ceed_InstrsAttendance(db);
                //ceed_TrainingTeam(db);
                // ceed_TeamGroup(db);
                //ceed_Clients(db);
                // ceed_Packages(db);
                //ceed_PkgRequrmnt(db);
            }
        }

        private static void ceed_Gym(Model1 db)
        {
            Gym gym = new Gym()
            {
                GymName = "Haifa",
                GymDescr = "Haifa gym Hermesh 53",
                Status = "Active",
                CreatedBy = 0,
                CreatedAt = 0,
                ChangedBy = 0,
                ChangedAt = 0
            };
            db.Gyms.Add(gym);
            db.SaveChanges();
        }
        private static void ceed_Instructor(Model1 db)
        {
            for (int i = 0; i < 6; i++)
            {
                byte[] ByteArray = new byte[1];
                ByteArray[0] = (byte)i;
                Instructor instr = new Instructor()
                {
                    InstrNum = i.ToString(),
                    GymId = 0,
                    FullName = (" מאמן " + i.ToString()).Trim(),
                    AliaseName = (" מאמן " + i.ToString()).Trim(),
                    InstrIDN = "000000000",
                    PerHour1 = 30 + i * 2,
                    PerHour2 = 30 + i * 3,
                    PerWaitHour = 30,
                    PerTrip1 = (decimal)24.0,
                    PerTrip2 = (decimal)28.40,
                    InstrColor = "color" + i.ToString(),
                    CWorthy = "0",
                    CRate = "0",
                    Status = "Active",
                    UName = "instr" + i.ToString(),
                    UPass = ByteArray,
                    UCode = ByteArray,
                    UResetPass = ByteArray,
                    U_GUID = ByteArray,
                    OAuthLvl = ByteArray,
                    CreatedBy = 0,
                    CreatedAt = 0,
                    ChangedBy = 0,
                    ChangedAt = 0
                };
                db.Instructors.Add(instr);
                db.SaveChanges();
            }
        }

        private static void ceed_InstrsAttendance(Model1 db)
        {
            int x = 2;
            var Instructors = db.Instructors.ToList();
            foreach (var item in Instructors)
            {
                DateTime d = DateTime.Now;
                for (int i = 1; i < 400; i++)
                {
                    if (i == x)
                    {
                        d = d.AddDays(1);
                        continue;
                    }
                    item.LInstrsAttendances.Add(new InstrsAttendance()
                    {
                        InstrId = item.InstrId,
                        GymId = 1000,
                        IAShiftDate = d.Date2Int(),
                        IAShiftStart = ((long.Parse(d.Date2String("YYYYMMDDHHMM"))) / 10000) * 10000 + 800,
                        IAShiftEnd = ((long.Parse(d.Date2String("YYYYMMDDHHMM"))) / 10000) * 10000 + 1700,
                        IAShiftPrcntg = 100,
                        IAShiftCredit = 0,
                        IAShiftCharge = 0,
                        Status = "Active",
                        CreatedBy = 0,
                        CreatedAt = 0,
                        ChangedBy = 0,
                        ChangedAt = 0
                    });
                    d = d.AddDays(1);
                }
                db.InstrsAttendances.AddRange(item.LInstrsAttendances);
                db.SaveChanges();
                x++;
            }
        }

        private static void ceed_TrainingTeam(Model1 db)
        {
            for (int i = 0; i < 164; i++)
            {
                TrainingTeam tt = new TrainingTeam()
                {

                    GymId = 1000,
                    TrnTmName = (" קבוצה מס " + (i + 1).ToString()).Trim(),
                    TrnTmDescr = (" קבוצה מס " + (i + 1).ToString()).Trim(),
                    InstrIdCount = 0,
                    OnePayer = true,
                    TrnTmColor = "",
                    Status = "Active",
                    CreatedBy = 0,
                    CreatedAt = 0,
                    ChangedBy = 0,
                    ChangedAt = 0
                };
                db.TrainingTeams.Add(tt);
                db.SaveChanges();
            }
        }

        private static void ceed_TeamGroup(Model1 db)
        {
            var r = new Random();
            var TrainingTeams = db.TrainingTeams.ToList();
            foreach (var item in TrainingTeams)
            {
                int grps = r.Next(1, 8);
                DateTime d = DateTime.Now;
                for (int i = 1; i <= grps; i++)
                {
                    item.LTeamGroups.Add(new TeamGroup()
                    {
                        GymId = 1000,
                        TrnTmId = item.TrnTmId,
                        TmGrpName = (" צוות מס " + i.ToString()).Trim(),
                        TmGrpDescr = (" צוות מס " + i.ToString()).Trim(),
                        FavIntrId = 0,
                        MustFavIntrId = false,
                        Status = "Active",
                        CreatedBy = 0,
                        CreatedAt = 0,
                        ChangedBy = 0,
                        ChangedAt = 0
                    });
                    d = d.AddDays(1);
                }
                db.TeamGroups.AddRange(item.LTeamGroups);
                db.SaveChanges();
            }
        }

        private static void ceed_Clients(Model1 db)
        {
            var r = new Random();
            var r1 = new Random();
            var Instructors = db.Instructors.ToList();
            var TeamGroups = db.TeamGroups.ToList();
            int j = 0;
            for (int i = 0; i < 600; i++)
            {
                int ClntInGrp = r1.Next(1, 5);
                for (int grp1 = 1; grp1 <= ClntInGrp; grp1++)
                {
                    int FAVinstrId = r.Next(0, Instructors.Count() - 1);
                    byte[] ByteArray = new byte[1];
                    ByteArray[0] = (byte)i;
                    Client clnt = new Client()
                    {
                        ClntNum = i.ToString(),
                        GymId = 1000,
                        FullName = (" לקוח " + (i + grp1).ToString()).Trim(),
                        AliaseName = (" לקוח " + (i + grp1).ToString()).Trim(),
                        TrnTmId = 0,
                        TmGrpId = 0,
                        FavIntrId = Instructors[FAVinstrId].InstrId,
                        MustFavIntrId = i % 5 == 0 ? true : false,
                        ClntIDN = "000000000",
                        PerHour1 = 0,
                        PerHour2 = 0,
                        PerTrip1 = 0,
                        PerTrip2 = 0,
                        ClntColor = "0",
                        CWorthy = "0",
                        CRate = "0",
                        Status = "Active",
                        UName = "clnt" + i.ToString(),
                        UPass = ByteArray,
                        UCode = ByteArray,
                        UResetPass = ByteArray,
                        U_GUID = ByteArray,
                        OAuthLvl = ByteArray,
                        CreatedBy = 0,
                        CreatedAt = 0,
                        ChangedBy = 0,
                        ChangedAt = 0
                    };
                    clnt.TrnTmId = TeamGroups[j].TrnTmId;
                    clnt.TmGrpId = TeamGroups[j].TmGrpId;
                    db.Clients.Add(clnt);
                    db.SaveChanges();
                    if (j >= TeamGroups.Count())
                        break;
                }
                j++;
            }
        }

        private static void ceed_Packages(Model1 db)
        {
            int i = 1;
            var TraningTeams = db.TrainingTeams.ToList();
            foreach (var item in TraningTeams)
            {
                DateTime d = DateTime.Now;
                d = d.AddDays(i / 3);
                Package pkg = new Package()
                {
                    PkgName = d.Date2String("YYYYMMDDHHMM") + "_" + i.ToString(),
                    PkgType = (i % 11).ToString(),
                    GymId = 1000,
                    TrnTmId = item.TrnTmId,
                    PkDateStart = d.Date2Int(),
                    PkDateStart1 = d.Date2Int(),
                    PkDateEnd = d.AddDays(7 * 12).Date2Int(),
                    IsPeriodFixed = false,
                    PkTrnAmount = 0,
                    PkTrnAmountWeek = 0,
                    TotalFee1 = 0,
                    TotalFee2 = 0,
                    TotalFee3 = 0,
                    AllGrpPymntDone = false,
                    Status = "Active",
                    CreatedBy = 0,
                    CreatedAt = 0,
                    ChangedBy = 0,
                    ChangedAt = 0
                };
                switch (i % 11)
                {
                    case 0:
                    case 1:
                    case 3:
                    case 7:
                        pkg.PkgType = "2TrnsInWeek";
                        pkg.IsPeriodFixed = true;
                        pkg.TotalFee1 = (decimal)1100.0;
                        break;
                    case 2:
                    case 4:
                    case 5:
                        pkg.PkgType = "3TrnsInWeek";
                        pkg.IsPeriodFixed = true;
                        pkg.TotalFee1 = (decimal)1650.0;
                        break;
                    case 6:
                    case 8:
                    case 9:
                        pkg.PkgType = "4TrnsInWeek";
                        pkg.IsPeriodFixed = true;
                        pkg.TotalFee1 = (decimal)2350.0;
                        break;
                    case 10:
                        pkg.PkgType = "BulkTrns";
                        pkg.IsPeriodFixed = false;
                        pkg.PkTrnAmount = 100;
                        pkg.TotalFee1 = (decimal)3350.0;
                        break;
                    default:
                        break;
                }
                db.Packages.Add(pkg);
                db.SaveChanges();
                i++;
            }
        }

        private static void ceed_PkgRequrmnt(Model1 db)
        {
            PkgRequrmnt pkg_requ = null;
            LEnumsClass DOWs = new LEnumsClass();
            var days = DOWs.Enums.Where(tt => tt.Enum == "DOW").ToList();
            var day_time = DOWs.Enums.Where(tt => tt.Enum == "DayTime").ToList();
            int PkReqHour1 = 0;
            int days_indx = 0;
            int day_time_indx = 0;
            int loop = 0;
            var Packages = db.Packages.ToList();
            foreach (var item in Packages)
            {
                switch (item.PkgType)
                {
                    case "2TrnsInWeek":
                        loop = 2;
                        break;
                    case "3TrnsInWeek":
                        loop = 3;
                        break;
                    case "4TrnsInWeek":
                        loop = 4;
                        break;
                        //case "BulkTrns":
                        //    break;
                }
                for (int i = 0; i < loop; i++)
                {
                    pkg_requ = new PkgRequrmnt()
                    {
                        PkgId = item.PkgId,
                        GymId = 1000,
                        PkReqDOW = days[days_indx += 2].EnumEng,
                        PkReqDayTime = day_time[day_time_indx].EnumEng,
                        PkReqHour1 = day_time[day_time_indx].EnumValue1 + PkReqHour1,
                        PkReqTrnTime = 1,
                        Status = "Active",
                        CreatedBy = 0,
                        CreatedAt = 0,
                        ChangedBy = 0,
                        ChangedAt = 0
                    };
                    PkReqHour1++;
                    if (days_indx > 4)
                        days_indx = 0;

                    if (PkReqHour1 > 5)
                        PkReqHour1 = 0;
                }
                db.PkgRequrmnts.Add(pkg_requ);
                db.SaveChanges();
                day_time_indx++;
                if (day_time_indx > 2)
                    day_time_indx = 0;
            }
        }

        private static void Do(Package item, int TrnsInWeekv)
        {
            throw new NotImplementedException();
        }
    }
}