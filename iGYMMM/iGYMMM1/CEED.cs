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
                //ceed_GymsTimeTable(db);
                //ceed_Instructor(db);
                //ceed_InstrsAttendance(db);
                //ceed_TrainingTeam(db);
                //ceed_TeamGroup(db);
                //ceed_Clients(db);
                //ceed_Packages(db);
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

        private static void ceed_GymsTimeTable(Model1 db)
        {
            var DOWs = LEnumsClass.Enums.Where(x => x.Enum == "DOW").ToList().OrderBy(x => x.EnumId).ToList();
            var gyms = db.Gyms.Where(tt => tt.Status == "Active").ToList();
            foreach (var item in gyms)
            {
                foreach (var day_ in DOWs)
                {
                    GymsTimeTable gymtt = new GymsTimeTable()
                    {
                        GymTTId = 0,
                        GymId = item.GymId,
                        DOW = day_.EnumEng,
                        OpenAt = 6,
                        CloseAt = 23,
                        Status = "Active",
                        CreatedBy = 0,
                        CreatedAt = 0,
                        ChangedBy = 0,
                        ChangedAt = 0
                    };
                    db.GymsTimeTables.Add(gymtt);
                    db.SaveChanges();
                }
            }
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
            int FAVinstrId = 0;
            List<int> Linst_indx = new List<int>();
            var r = new Random();
            var r1 = new Random();
            var TrainingTeams = db.TrainingTeams.ToList();
            var Instructors = db.Instructors.ToList();
            foreach (var item in TrainingTeams)
            {
                int grps = r.Next(1, Instructors.Count() - 1);
                DateTime d = DateTime.Now;
                for (int i = 1; i <= grps; i++)
                {
                    do
                    {
                        FAVinstrId = r.Next(0, Instructors.Count() - 1);
                    }
                    while (Linst_indx.Contains(FAVinstrId));
                    Linst_indx.Add(FAVinstrId);
                    item.LTeamGroups.Add(new TeamGroup()
                    {
                        GymId = 1000,
                        TrnTmId = item.TrnTmId,
                        TmGrpName = (" צוות מס " + i.ToString()).Trim(),
                        TmGrpDescr = (" צוות מס " + i.ToString()).Trim(),
                        FavIntrId = Instructors[FAVinstrId].InstrId,
                        MustFavIntrId = (TrainingTeams.IndexOf(item) + i) % 6 == 0 ? true : false,
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
                Linst_indx.Clear();
            }
        }

        private static void ceed_Clients(Model1 db)
        {
            //var r = new Random();
            var r1 = new Random();
            var Instructors = db.Instructors.ToList();
            var TeamGroups = db.TeamGroups.ToList();
            int j = 0;
            for (int i = 0; i < 600; i++)
            {
                int ClntInGrp = r1.Next(1, 5);
                for (int grp1 = 1; grp1 <= ClntInGrp; grp1++)
                {
                    //int FAVinstrId = r.Next(0, Instructors.Count() - 1);
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
                        //FavIntrId = Instructors[FAVinstrId].InstrId,
                        //MustFavIntrId = i % 5 == 0 ? true : false,
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
                    if (j >= TeamGroups.Count())
                        break;
                    clnt.TrnTmId = TeamGroups[j].TrnTmId;
                    clnt.TmGrpId = TeamGroups[j].TmGrpId;
                    db.Clients.Add(clnt);
                    db.SaveChanges();
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
                        pkg.PkTrnAmountWeek = 2;
                        pkg.TotalFee1 = (decimal)1100.0;
                        break;
                    case 2:
                    case 4:
                    case 5:
                        pkg.PkgType = "3TrnsInWeek";
                        pkg.IsPeriodFixed = true;
                        pkg.PkTrnAmountWeek = 3;
                        pkg.TotalFee1 = (decimal)1650.0;
                        break;
                    case 6:
                    case 8:
                    case 9:
                        pkg.PkgType = "4TrnsInWeek";
                        pkg.IsPeriodFixed = true;
                        pkg.PkTrnAmountWeek = 4;
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
            var r = new Random();
            PkgRequrmnt pkg_requ = null;
            var days = LEnumsClass.Enums.Where(tt => tt.Enum == "DOW").ToList();
            var day_time = LEnumsClass.Enums.Where(tt => tt.Enum == "DayTime").ToList();

            var Packages = db.Packages.ToList(); //.Where(X => X.PkgId <= 100667).ToList();    //.Where(x => x.PkgId == 100663).ToList();
            foreach (var item in Packages)
            {
                int loop = 0;
                int days_indx = 0;
                int PkReqHour1 = 6;
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
                    PkReqHour1 = r.Next(6, 23);
                    pkg_requ = new PkgRequrmnt()
                    {
                        PkgId = item.PkgId,
                        GymId = 1000,
                        PkReqDOW = days[days_indx].EnumEng,
                        PkReqDayTime = "",
                        PkReqHour1 = PkReqHour1,
                        PkReqTrnTime = 1,
                        Status = "Active",
                        CreatedBy = 0,
                        CreatedAt = 0,
                        ChangedBy = 0,
                        ChangedAt = 0
                    };
                    days_indx += 2;
                    if (days_indx > 6)
                        days_indx = 0;
                    var d_t = day_time.FirstOrDefault(x => x.EnumValue1 <= PkReqHour1 && x.EnumValue2 >= PkReqHour1);
                    if (d_t != null)
                        pkg_requ.PkReqDayTime = d_t.EnumEng;
                    PkReqHour1 += 3;
                    if (PkReqHour1 >= 23)
                        PkReqHour1 = 6;
                    db.PkgRequrmnts.Add(pkg_requ);
                    db.SaveChanges();
                }
            }
        }
    }
}