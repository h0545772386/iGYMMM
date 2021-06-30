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
    }
}
