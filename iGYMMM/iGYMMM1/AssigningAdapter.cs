using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace iGYMMM1
{
    public class AssigningAdapter
    {
        static List<AssigningLog> LAssigningLogs = new List<AssigningLog>();
        static List<DiaryHeader> LDiaryHeaders2Save = new List<DiaryHeader>();

        public static List<DiaryHeader> CreateTrainingAllTeams(int dateFrom, int dateTo, int TrnTmId = -1)
        {
            LAssigningLogs.Clear();
            List<TrainingTeam> LTrainingTeams = new List<TrainingTeam>();
            List<DiaryHeader> LDiaryHeaders_new = new List<DiaryHeader>();  // new assignments
            using (var db = new Model1())
            {
                if (TrnTmId == -1)
                    LTrainingTeams = db.TrainingTeams.Where(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active").ToList();
                else
                    LTrainingTeams = db.TrainingTeams.Where(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active" && tt.TrnTmId == TrnTmId).ToList();
            }
            if (LTrainingTeams != null)
            {
                int porgess = 0;
                int total = LTrainingTeams.Count;
                // Proccess starts
                // Assign as requirements - no checks
                // שיבות לקוחות לכל מאמן לפי דרישות החבליה ללא כל בדיקה
                // לא בודקים שום תנאי
                // מבצעים שיבוץ קבוצות לכל מאמן מועדף
                foreach (var trnTeam in LTrainingTeams)
                {
                    LDiaryHeaders_new.AddRange(CreateTraining4Team(dateFrom, dateTo, trnTeam.TrnTmId));
                    Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss fff")} {++porgess}/{total}");
                }
            }
            //StringBuilder sb = new StringBuilder();
            //foreach(DiaryHeader dh in LDiaryHeaders_new)
            //{
            //    sb.AppendLine($"{dh.DryHdrId} PL-{dh.PlannedInstrId} AL-{dh.ActualInstrId} GP-{dh.TmGrpId} TR-{dh.TrnTmId}");
            //}
            //System.IO.File.WriteAllText($"{AppDomain.CurrentDomain.BaseDirectory}diary.txt",sb.ToString());

            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AssigningLog");
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            StringBuilder logs = new StringBuilder();
            int i = 0;
            foreach (AssigningLog log in LAssigningLogs)
            {
                logs.AppendLine($"{++i} {log.TrnTmId} {log.TrnTmName} {log.TmGrpId}  {log.PkgId} {log.PkgReqId} {log.InstrId} {log.ActionText} {log.AssignTime}  {log.ErrorText}");
            }
            System.IO.File.WriteAllText(Path.Combine(logPath, $"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.txt"), logs.ToString());
            // תחילת בדיקות
            // 1. בודקים התנגשויות
            // אם מאמן בשעה מסוימת יש לו יותר מקבוצה
            //    CheckOverLaps(dateFrom, dateTo, ref LDiaryHeaders_new);

            // check existing training with same properties at database
            // paint those 

            var overLaps = LDiaryHeaders_new.OrderBy(a => a.PlannedInstrId).ThenBy(b => b.ActualInstrId).ThenBy(c => c.TrnDate).ThenBy(d => d.TrnHour).ToList();

            return overLaps;
        }

        private static List<DiaryHeader> CreateTraining4Team(int dateFrom, int dateTo, int TrnTmId)
        {
            DateTime d1 = dateFrom.Int2Date();
            DateTime d2 = dateTo.Int2Date();
            TrainingTeam TrainingTeam = new TrainingTeam();
            List<DiaryTeam> LDiaryTeams = new List<DiaryTeam>();
            List<Instructor> LInstructors = new List<Instructor>();
            List<DiaryHeader> LDiaryHeaders = new List<DiaryHeader>();
            List<AssigningLog> LAssigningLog = new List<AssigningLog>();  // error log
                                                                          //   List<TrainingTeam> LTrainingTeams = new List<TrainingTeam>();
            List<DiaryHeader> LDiaryHeaders_new = new List<DiaryHeader>();  // new assignments

            #region SeletecFromDB
            using (var db = new Model1())
            {
                TrainingTeam = db.TrainingTeams.FirstOrDefault(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active" && tt.TrnTmId == TrnTmId);
                if (TrainingTeam == null)
                    return LDiaryHeaders_new;

                TrainingTeam.LTeamGroups = db.TeamGroups.Where(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active" && tt.TrnTmId == TrnTmId).ToList();
                if (TrainingTeam.LTeamGroups == null && TrainingTeam.LTeamGroups.Count > 0)
                    return LDiaryHeaders_new;

                var LClients = db.Clients.Where(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active" && tt.TrnTmId == TrnTmId).ToList();
                if (LClients == null && LClients.Count > 0)
                    return LDiaryHeaders_new;
                foreach (var tmgrp in TrainingTeam.LTeamGroups)
                    tmgrp.LClients = LClients.Where(tt => tt.TrnTmId == TrnTmId && tt.TmGrpId == tmgrp.TmGrpId).ToList();

                TrainingTeam.Package = db.Packages.FirstOrDefault(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active" && tt.IsPeriodFixed == true &&
                                                                        tt.TrnTmId == TrnTmId && dateFrom <= tt.PkDateEnd && dateTo >= tt.PkDateStart);
                //(
                //dateFrom <= tt.PkDateStart && dateTo >= tt.PkDateStart ||
                //dateFrom <= tt.PkDateEnd && dateTo >= tt.PkDateEnd ||
                //dateFrom <= tt.PkDateStart && dateTo >= tt.PkDateEnd ||
                //dateFrom >= tt.PkDateStart && dateTo <= tt.PkDateEnd 
                //));
                if (TrainingTeam.Package == null)
                    return LDiaryHeaders_new;

                TrainingTeam.Package.LRequriments = db.PkgRequrmnts.Where(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active" && tt.PkgId == TrainingTeam.Package.PkgId).ToList();
                if (TrainingTeam.Package.LRequriments == null && TrainingTeam.Package.LRequriments.Count > 0)
                    return LDiaryHeaders_new;

                //  LTrainingTeams.Add(TrainingTeam);

                var LInstrAtendcs = db.InstrsAttendances.Where(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active" && tt.IAShiftDate >= dateFrom && tt.IAShiftDate <= dateTo).ToList();
                if (LInstrAtendcs == null && LInstrAtendcs.Count > 0)
                    return LDiaryHeaders_new;
                LInstructors = db.Instructors.Where(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active").ToList()
                            .Where(x => LInstrAtendcs.Any(y => y.InstrId == x.InstrId)).ToList();//.Distinct().ToList();
                                                                                                 // LInstructors = LInstructors.Where(x=>LInstrAtendcs.Any(y=>y.InstrId==x.InstrId)).ToList();
                if (LInstructors == null && LInstructors.Count > 0)
                    return LDiaryHeaders_new;
                foreach (var item in LInstructors)
                {
                    item.LInstrsAttendances = db.InstrsAttendances.Where(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active" && tt.IAShiftDate >= dateFrom && tt.IAShiftDate <= dateTo).ToList();
                }


                // get already assignments from database
                LDiaryHeaders = db.DiaryHeaders.Where(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active" && tt.TrnDate >= dateFrom && tt.TrnDate <= dateTo).ToList();
                if (LDiaryHeaders != null)
                {
                    var LDIs = db.DiaryItems.Where(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active" && tt.TrnDate >= dateFrom && tt.TrnDate <= dateTo).ToList();
                    if (LDIs != null && LDIs.Count > 0)
                    {
                        foreach (var item in LDiaryHeaders)
                        {
                            item.LDiaryItems = LDIs.Where(tt => tt.DryHdrId == item.DryHdrId).ToList();
                        }
                    }
                }
            }
            #endregion SeletecFromDB

            // Proccess starts
            // Assign as requirements - no checks
            // שיבות לקוחות לכל מאמן לפי דרישות החבליה ללא כל בדיקה
            // לא בודקים שום תנאי
            // מבצעים שיבוץ קבוצות לכל מאמן מועדף
            //foreach (var trnTeam in LTrainingTeams)
            //{
            DateTime pkgEndDate = TrainingTeam.Package.PkDateEnd.Int2Date();
            DateTime pkgStartDate = TrainingTeam.Package.PkDateStart.Int2Date();
            for (DateTime dd = d1; dd <= d2; dd = dd.AddDays(1))   // period days
            {
                if (dd < pkgStartDate)
                    continue;
                if (dd > pkgEndDate)
                    break;
                foreach (var PkgReq in TrainingTeam.Package.LRequriments)
                {
                    if (dd.DayOfWeek.ToString().ToLower() == PkgReq.PkReqDOW.ToLower())
                        LDiaryHeaders_new.AddRange(GoAssignTrns4Day(dd, TrainingTeam, PkgReq, LInstructors, LDiaryHeaders));
                }
            }
            //}
            return LDiaryHeaders_new;
        }

        private static List<DiaryHeader> GoAssignTrns4Day(DateTime dd, TrainingTeam trnTeam, PkgRequrmnt pkgReq, List<Instructor> lInstructors, List<DiaryHeader> lDiaryHeaders)
        {
            List<DiaryHeader> LDH = new List<DiaryHeader>();
            using (var db = new Model1())
            {
                using (var transation = db.Database.BeginTransaction())
                {
                    foreach (var TeamGrp in trnTeam.LTeamGroups)
                    {
                        // What has been assigned is no longer assignment.
                        //In order to prevent the newly added group from not being assigned exactly to group.
                        //New clients added to group should copy the same information from his goup members when added
                        var diary = lDiaryHeaders.Where(c => c.TmGrpId == TeamGrp.TmGrpId && c.TrnDate == dd.Date2Int()).FirstOrDefault();
                        if (diary != null)
                        {
                            LDH.Add(diary);
                            continue;
                        }

                        DiaryHeader dh = new DiaryHeader()
                        {
                            DryHdrId = 0,
                            GymId = AppGlobals.Instance.GymId,
                            TrnTmId = trnTeam.TrnTmId,
                            TmGrpId = TeamGrp.TmGrpId,
                            PkgId = pkgReq.PkgId,
                            PkgReqId = pkgReq.PkgReqId,
                            TrnDate = dd.Date2Int(),
                            TrnHour = pkgReq.PkReqHour1,
                            PkReqDOW = pkgReq.PkReqDOW,
                            PkReqDayTime = pkgReq.PkReqDayTime,
                            PkReqHour1 = pkgReq.PkReqHour1,
                            PkReqHour2 = 0,
                            PkReqTrnTime = pkgReq.PkReqTrnTime,
                            ActualTrnDOW = pkgReq.PkReqDOW,
                            ActualTrnDayTime = pkgReq.PkReqDayTime,
                            ActualTrnHour1 = pkgReq.PkReqHour1,
                            ActualTrnHour2 = 0,
                            ActualTrnTrnTime = pkgReq.PkReqTrnTime,
                            PlannedInstrId = TeamGrp.FavIntrId,
                            ActualInstrId = TeamGrp.FavIntrId,
                            PerHour1 = 0,
                            PerHour2 = 0,
                            PerWaitHour = 0,
                            PerTrip1 = 0,
                            PerTrip2 = 0,
                            ChargeTot = 0,
                            CreditTot = 0,
                            ColorView = "",
                            TrStatus = "Scheduled",
                            Status = "Active",
                            CreatedBy = AppGlobals.Instance.LoggedUser.UId,
                            CreatedAt = DateTime.Now.Date2Long(),
                            ChangedBy = AppGlobals.Instance.LoggedUser.UId,
                            ChangedAt = DateTime.Now.Date2Long()

                        };
                        //Assign instructor
                        bool b = false;
                        int date = dd.Date2Int();
                        var instructor = lInstructors.Where(c => c.InstrId == TeamGrp.FavIntrId).FirstOrDefault();
                        if (instructor != null)
                        {
                            b = instructor.LInstrsAttendances.Any(c => c.IAShiftDate == date && pkgReq.PkReqHour1 * 100 >= c.IAShiftStart % 10000 && pkgReq.PkReqHour1 * 100 <= c.IAShiftEnd % 10000);
                        }
                        //if favirote instructor is not available or there is a same record change instructor or train time
                        if (!b || db.DiaryHeaders.Any(c => c.TrnDate == date && c.TrnHour == pkgReq.PkReqHour1 && c.ActualInstrId == TeamGrp.FavIntrId))
                        {
                            if (TeamGrp.MustFavIntrId)
                            {
                                if (instructor == null || !GetInstructor(pkgReq.PkReqDayTime, db, dd, pkgReq, TeamGrp, dh, new List<Instructor>() { instructor }))
                                {
                                    AssigningLog assigningLog = new AssigningLog()
                                    {
                                        Id = 0,
                                        AssignTime = dd,
                                        ErrorText = "No favorite coach access",
                                        ActionText = "Assigning",
                                        InstrId = TeamGrp.FavIntrId,
                                        PkgId = pkgReq.PkgId,
                                        PkgReqId = pkgReq.PkgReqId,
                                        TmGrpId = TeamGrp.TmGrpId,
                                        TrnTmId = TeamGrp.TrnTmId,
                                        TrnTmName = trnTeam.TrnTmName
                                    };
                                    LAssigningLogs.Add(assigningLog);
                                    continue;
                                }
                                #region
                                //int start = 6;
                                //switch (pkgReq.PkReqDayTime)
                                //{
                                //    case "Morning":
                                //        start = 6;
                                //            break;
                                //    case "Afternoon":
                                //        start = 12;
                                //        break;
                                //    case "Eveinig":
                                //        start = 18;
                                //        break;
                                //}
                                //int x = 0;
                                //int y = 0;
                                //bool bExist = true;
                                //for (int d = 0; bExist; d++)
                                //{
                                //    //Get open time from GymsTimeTables  if it is null(may be not a working day) ,move to next day
                                //    var working = db.GymsTimeTables.Where(c => c.Status == "Active" && c.DOW == dd.AddDays(d).DayOfWeek.ToString()).FirstOrDefault();
                                //    if (working == null)
                                //        continue;
                                //    //loop 24 hours for one open day during open hours whether there is a period to get this instructor
                                //    //selected PkReqDayTime first
                                //    for (int i = start; x * y < start; i++)
                                //    {
                                //        x = i / 23;
                                //        y = i % 23;
                                //        if (y < working.OpenAt || y > working.CloseAt)
                                //            continue;
                                //        bExist = db.DiaryHeaders.Any(c => c.TrnDate == dd.AddDays(d).Date2Int() && c.TrnHour == pkgReq.PkReqHour1 && c.ActualInstrId == TeamGrp.FavIntrId);
                                //        if (!bExist)
                                //        {
                                //            if (i > 5 && i < 12)
                                //            {
                                //                dh.ActualTrnDayTime = "Morning";
                                //            }
                                //            else if (i > 11 && i < 18)
                                //            {
                                //                dh.ActualTrnDayTime = "Afternoon";
                                //            }
                                //            else if (i > 17 && i < 24)
                                //            {
                                //                dh.ActualTrnDayTime = "Eveinig";
                                //            }
                                //            dh.ActualTrnDOW = dd.AddDays(d).DayOfWeek.ToString();
                                //            dh.TrnHour = i;
                                //            dh.TrnDate = dd.AddDays(d).Date2Int();
                                //            break;
                                //        }
                                //    }
                                //    //if move to check next day will start from eariest morning time
                                //    start = 6;
                                //}
                                #endregion
                            }
                            else
                            {
                                var intrs = db.DiaryHeaders.Where(c => c.TrnDate == date && c.TrnHour == pkgReq.PkReqHour1);
                                var freeIntrs = lInstructors.Where(x => !intrs.Any(y => y.ActualInstrId == x.InstrId)).ToList().Where(x => x.LInstrsAttendances.Any(c => c.IAShiftDate == date && pkgReq.PkReqHour1 * 100 >= c.IAShiftStart % 10000 && pkgReq.PkReqHour1 * 100 <= c.IAShiftEnd % 10000)).ToList();

                                if (freeIntrs == null || freeIntrs.Count < 1)
                                {
                                    if (lInstructors == null || !GetInstructor(pkgReq.PkReqDayTime, db, dd, pkgReq, TeamGrp, dh, lInstructors))
                                    {
                                        AssigningLog assigningLog = new AssigningLog()
                                        {
                                            Id = 0,
                                            AssignTime = dd,
                                            ErrorText = "No free coach access",
                                            ActionText = "Assigning",
                                            InstrId = TeamGrp.FavIntrId,
                                            PkgId = pkgReq.PkgId,
                                            PkgReqId = pkgReq.PkgReqId,
                                            TmGrpId = TeamGrp.TmGrpId,
                                            TrnTmId = TeamGrp.TrnTmId,
                                            TrnTmName = trnTeam.TrnTmName
                                        };
                                        LAssigningLogs.Add(assigningLog);
                                        continue;
                                    }
                                }
                                else
                                {
                                    Instructor instructor1 = freeIntrs[new Random(DateTime.Now.Millisecond).Next(0, freeIntrs.Count - 1)];
                                    dh.ActualInstrId = instructor1.InstrId;
                                }
                            }
                        }
                        db.DiaryHeaders.Add(dh);
                        db.SaveChanges();
                        foreach (var clnt in TeamGrp.LClients)
                        {
                            DiaryItem di = new DiaryItem()
                            {
                                DryItmId = 0,
                                DryHdrId = dh.DryHdrId,
                                GymId = AppGlobals.Instance.GymId,
                                TrnTmId = trnTeam.TrnTmId,
                                TmGrpId = TeamGrp.TmGrpId,
                                PkgId = pkgReq.PkgId,
                                PkgReqId = pkgReq.PkgReqId,
                                TrnDate = dd.Date2Int(),
                                TrnHour = pkgReq.PkReqHour1,
                                ClntId = clnt.ClntId,
                                PerHour1 = clnt.PerHour1,
                                PerHour2 = clnt.PerHour2,
                                PerTrip1 = clnt.PerTrip1,
                                PerTrip2 = clnt.PerTrip2,
                                ChargeTot = 0,
                                CreditTot = 0,
                                TrStatus = "Scheduled",
                                Status = "Active",
                                CreatedBy = AppGlobals.Instance.LoggedUser.UId,
                                CreatedAt = DateTime.Now.Date2Long(),
                                ChangedBy = AppGlobals.Instance.LoggedUser.UId,
                                ChangedAt = DateTime.Now.Date2Long()
                            };

                            db.DiaryItems.Add(di);
                            db.SaveChanges();
                            dh.LDiaryItems.Add(di);
                        }
                        LDH.Add(dh);
                        lDiaryHeaders.Add(dh);
                    }
                    transation.Commit();
                }
            }
            return LDH;
        }

        /// <summary>
        /// get instructors 
        /// check differet instructor or differet hour
        /// </summary>
        /// <param name="dayTime"></param>
        /// <param name="db"></param>
        /// <param name="dd"></param>
        /// <param name="pkgReq"></param>
        /// <param name="TeamGrp"></param>
        /// <param name="dh"></param>
        /// <param name="instructors"></param>
        /// <returns></returns>
        private static bool GetInstructor(string dayTime, Model1 db, DateTime dd, PkgRequrmnt pkgReq, TeamGroup TeamGrp, DiaryHeader dh, List<Instructor> instructors)
        {
            int start = 6;
            switch (dayTime)
            {
                case "Morning":
                    start = 6;
                    break;
                case "Afternoon":
                    start = 12;
                    break;
                case "Eveinig":
                    start = 18;
                    break;
            }
            int x = 0;
            int y = 0;
            bool bExist = true;
            // a week is a cycle
            for (int d = 0; bExist && d < 7; d++)
            {
                string weekDay = dd.AddDays(d).DayOfWeek.ToString();
                //Get open time from GymsTimeTables  if it is null(may be not a working day) ,move to next day
                var working = db.GymsTimeTables.Where(c => c.Status == "Active" && c.DOW == weekDay).FirstOrDefault();
                if (working == null)
                    continue;
                //loop 24 hours for one open day during open hours whether there is a period to get this instructor
                //selected PkReqDayTime first
                for (int i = start; x * y < start; i++)
                {
                    x = i / 23;
                    y = i % 23;
                    if (y < working.OpenAt || y > working.CloseAt)
                        continue;
                    int date = dd.AddDays(d).Date2Int();
                    foreach (Instructor ins in instructors)
                    {
                        //make sure the time is a instructors working time
                        var insAtt = ins.LInstrsAttendances.Where(c => c.IAShiftDate == date && y * 100 >= c.IAShiftStart % 10000 && y * 100 <= c.IAShiftEnd % 10000).FirstOrDefault();
                        if (insAtt == null)
                            continue;
                        //make sure there is no same time DiaryHeaders record
                        bExist = db.DiaryHeaders.Any(c => c.TrnDate == date && c.TrnHour == y && c.ActualInstrId == ins.InstrId);
                        if (!bExist)
                        {
                            if (y > 5 && y < 12)
                            {
                                dh.ActualTrnDayTime = "Morning";
                            }
                            else if (y > 11 && y < 18)
                            {
                                dh.ActualTrnDayTime = "Afternoon";
                            }
                            else if (y > 17 && y < 24)
                            {
                                dh.ActualTrnDayTime = "Eveinig";
                            }
                            dh.ActualTrnDOW = weekDay;//dd.AddDays(d).DayOfWeek.ToString();
                            dh.TrnHour = y;
                            dh.TrnDate = date;//dd.AddDays(d).Date2Int();
                            dh.ActualInstrId = ins.InstrId;
                            return !bExist;
                        }
                    }
                }
                //if move to check next day will start from eariest morning time
                start = 6;
                y = 0;
                x = 0;
            }
            return !bExist;
        }





        #region codeXXX 
        //private static void CheckOverLaps(int dateFrom, int dateTo, ref List<DiaryHeader> lDiaryHeaders_new)
        //{
        //    DateTime d1 = dateFrom.Int2Date();
        //    DateTime d2 = dateTo.Int2Date();
        //    List<GymsTimeTable> LGymTT = new List<GymsTimeTable>();
        //    using (var db = new Model1())
        //    {
        //        LGymTT = db.GymsTimeTables.Where(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active").ToList();
        //    }
        //    for (DateTime dd = d1; dd <= d2; dd = dd.AddDays(1))   // period days
        //    {
        //        foreach (var GymTT in LGymTT)
        //        {
        //            if (GymTT.DOW == dd.DayOfWeek.ToString())
        //            {
        //                for (int h = GymTT.OpenAt; h < GymTT.CloseAt; h++)
        //                {
        //                    var g1 = lDiaryHeaders_new.Where(tt => tt.TrnDate == dd.Date2Int() && tt.TrnHour == h).GroupBy(tt => tt.ActualInstrId).ToList();
        //                    if (lDiaryHeaders_new.Where(tt => tt.TrnDate == dd.Date2Int() && tt.TrnHour == h).GroupBy(tt => tt.ActualInstrId).ToList().Count > 1)
        //                    {

        //                    }
        //                }
        //            }
        //        }
        //    }

        //}
        /*
        private static AssigningLog AssignTrns4Day(DateTime dd, TrainingTeam trnTeam, TeamGroup teamGrp, PkgRequrmnt pkgReq, List<Instructor> lInstructors, List<DiaryHeader> lDiaryHeaders, ref List<DiaryHeader> lDiaryHeaders_new)
        {






            var r = new AssigningLog();

            var instr = lInstructors.FirstOrDefault(tt => tt.InstrId == teamGrp.FavIntrId);

            lDiaryHeaders_new.Add(new DiaryHeader() { DryHdrId = 7 });






            if (instr != null)
            {

            }
            else   // instr == null  מאמן לא עובד ביום זה
            {
                if (trnTeam.MustFavIntr4Grp)   // חייבים להעביר את האימון של כל הקבוצה ליום אחר
                {

                }
                else  // צריך למצוא שעה מאמן פנוי באותה שעה
                {

                }
            }

            return r;
        }


        private static void SetAssignment(List<DiaryHeader> lDiaryHeaders_new, TrainingTeam trainingTeam, TeamGroup tmGrp, PkgRequrmnt pkgReq, Instructor instr, DateTime dd)
        {
            DiaryHeader dh = new DiaryHeader()
            {
                DryHdrId = 0,
                GymId = AppGlobals.Instance.GymId,
                TrnTmId = trainingTeam.TrnTmId,
                TmGrpId = tmGrp.TmGrpId,
                PkgId = pkgReq.PkgId,
                PkgReqId = pkgReq.PkgReqId,
                TrnDate = dd.Date2Int(),
                TrnHour = 0,
                PkReqDOW = pkgReq.PkReqDOW,
                PkReqDayTime = pkgReq.PkReqDayTime,
                PkReqHour1 = pkgReq.PkReqHour1,
                PkReqHour2 = 0,
                PkReqTrnTime = pkgReq.PkReqTrnTime,
                ActualTrnDOW = pkgReq.PkReqDOW,
                ActualTrnDayTime = pkgReq.PkReqDayTime,
                ActualTrnHour1 = pkgReq.PkReqHour1,
                ActualTrnHour2 = 0,
                ActualTrnTrnTime = pkgReq.PkReqTrnTime,
                PlannedInstrId = tmGrp.FavIntrId,
                ActualInstrId = tmGrp.FavIntrId,
                PerHour1 = 0,
                PerHour2 = 0,
                PerWaitHour = 0,
                PerTrip1 = 0,
                PerTrip2 = 0,
                ChargeTot = 0,
                CreditTot = 0,
                ColorView = "",
                TrStatus = "Scheduled",
                Status = "Active",
                CreatedBy = AppGlobals.Instance.LoggedUser.UId,
                CreatedAt = DateTime.Now.Date2Long(),
                ChangedBy = AppGlobals.Instance.LoggedUser.UId,
                ChangedAt = DateTime.Now.Date2Long()
            };

        }

        */

        //private void a1()
        //{
        //  #region test




        //    //foreach (var TeamGrp in TrainingTeam.LTeamGroups)
        //    //{
        //    //    var trns = LDiaryHeaders.Where(tt => tt.Status == "Active" &&
        //    //                                         tt.TrnTmId == trnTeam.TrnTmId &&
        //    //                                         tt.TmGrpId == TeamGrp.TmGrpId &&
        //    //                                         tt.PkgId == PkgReq.PkgId &&
        //    //                                         tt.PkgReqId == PkgReq.PkgReqId).ToList();
        //    //    if (trns == null || trns.Count == 0)  // לא קיים אימון בבסיס הנתונים עבור אילוץ זה
        //    //    {
        //    //        AssigningLog result = AssignTrns4Day(dd, trnTeam, TeamGrp, PkgReq, LInstructors, LDiaryHeaders, ref LDiaryHeaders_new);
        //    //        if (!result.OkCode)  // לא נוצר
        //    //        {

        //    //        }
        //    //    }
        //    //}


        //    //var days_ = LEnumsClass.Enums.Where(x => x.Enum == "DOW").ToList().OrderBy(x => x.EnumId);
        //    int ass_log_id = 0;
        //    for (DateTime dd = d1; dd < d2; dd = dd.AddDays(1))   // period days
        //    {
        //        //AssignTrns4Day(dd, TrainingTeam, LInstructors, LDiaryHeaders);

        //        foreach (var PkgReq in TrainingTeam.Package.LRequriments)
        //        {
        //            foreach (var TmGrp in TrainingTeam.LTeamGroups)
        //            {
        //                if (PkgReq.PkReqDOW == dd.DayOfWeek.ToString())
        //                {
        //                    var instr = LInstructors.FirstOrDefault(tt => tt.InstrId == TmGrp.FavIntrId);
        //                    if (instr == null) // מאמן לא עובד ביום זה
        //                    {
        //                        LAssigningLog.Add(new AssigningLog()
        //                        {
        //                            Id = ass_log_id++,
        //                            AssignTime = DateTime.Now,
        //                            TrnTmId = TmGrp.TrnTmId,
        //                            TmGrpId = TmGrp.TmGrpId,
        //                            InstrId = TmGrp.FavIntrId,
        //                            InstrId1 = 0,
        //                            PkgId = PkgReq.PkgId,
        //                            PkgReqId = PkgReq.PkgReqId,
        //                            ErrorText = "מאמן לא עובד ביום זה",
        //                            ActionText = ""
        //                        });
        //                        if (TmGrp.MustFavIntrId)  // חובה רק מאמן זה
        //                        {
        //                            // להזיז את כל הקבוצה ליום אחר

        //                        }
        //                        else
        //                        {
        //                            // לשבץ את כל הקבוצה עם מאמן אחר
        //                        }
        //                    }
        //                    else
        //                    {






        //                    }
        //                    var DH = LDiaryHeaders.FirstOrDefault(tt => tt.GymId == TmGrp.GymId &&
        //                                                                tt.TrnTmId == TmGrp.TrnTmId &&
        //                                                                tt.TmGrpId == TmGrp.TmGrpId &&
        //                                                                tt.PkgId == PkgReq.PkgId &&
        //                                                                tt.PkgReqId == PkgReq.PkgReqId &&
        //                                                                tt.Status == "Active"
        //                                                              );
        //                    if (DH == null)
        //                    {
        //                        SetAssignment(LDiaryHeaders_new, TrainingTeam, TmGrp, PkgReq, instr, dd);
        //                    }
        //                    else
        //                    {

        //                    }

        //                }
        //            }
        //        }
        //    }
        // #endregion test
    }

    #region x1
    //private static AssigningLog AssignTrns4Day1(DateTime dd, TrainingTeam trnTeam, TeamGroup teamGrp, PkgRequrmnt pkgReq, List<Instructor> lInstructors, List<DiaryHeader> lDiaryHeaders, ref List<DiaryHeader> lDiaryHeaders_new)
    //{
    //    var r = new AssigningLog();

    //    var instr = lInstructors.FirstOrDefault(tt => tt.InstrId == teamGrp.FavIntrId);
    //    if (instr != null)
    //    {

    //    }
    //    else   // instr == null  מאמן לא עובד ביום זה
    //    {
    //        if (trnTeam.MustFavIntr4Grp)   // חייבים להעביר את האימון של כל הקבוצה ליום אחר
    //        {

    //        }
    //        else  // צריך למצוא שעה מאמן פנוי באותה שעה
    //        {

    //        }
    //    }

    //    return r;
    //}
    #endregion x1

    #region x2













    /*
    public static List<AssignmentsDate> CreateTraining4All(int dateFrom, int dateTo)
    {
        List<Client> LClients = new List<Client>();
        List<Package> LPackages = new List<Package>();
        List<Instructor> LInstructors = new List<Instructor>();
        List<TrainingTeam> LTrainingTeams = new List<TrainingTeam>();
        List<AssignmentsDate> LTrainingDates = new List<AssignmentsDate>();

        //using (var db = new Model1())
        //{
        //    LInstructors = db.Instructors.Where(tt => tt.Status == "Active").ToList();
        //    LInstructors = db.Instructors.Where(tt => tt.Status == "Active").ToList();
        //    if (LInstructors == null)
        //        return LTrainingDates;


        //    foreach (var item in LInstructors)
        //    {
        //        item.LInstrsAttendances = db.InstrsAttendances.Where(tt => tt.Status == "Active" && tt.IAShiftDate >= dateFrom && tt.IAShiftDate <= dateTo).ToList();
        //    }

        //    if (TrnTmId != 0)
        //    {
        //        var TrainingTeam = db.TrainingTeams.FirstOrDefault(tt => tt.Status == "Active" && tt.TrnTmId == TrnTmId);
        //        if(TrainingTeam!=null)
        //        LTrainingTeams.Add
        //    }
        //    else
        //    {

        //    }

        //        LPackages = db.Packages.Where(tt => tt.Status == "Active" && tt.PkDateStart >= dateFrom && tt.PkDateEnd <= dateTo).ToList();
        //    if (LPackages == null)
        //        return LTrainingDates;
        //    foreach (var item in LPackages)
        //    {
        //        item.LRequriments = db.PkgRequrmnts.Where(tt => tt.Status == "Active" && tt.PkgId >= item.PkgId).ToList();
        //    }
        //    LTrainingTeams = db.TrainingTeams.ToList().Where(x => x.Status == "Active" && LPackages.Any(y => y.TrnTmId == x.TrnTmId)).ToList();
        //    if (LTrainingTeams == null)
        //        return LTrainingDates;
        //    if (TrnTmId != 0)
        //    {
        //        LTrainingTeams = LTrainingTeams.Where(tt => tt.TrnTmId == TrnTmId).ToList();
        //        LPackages = LPackages.Where(x => LTrainingTeams.Any(y => y.TrnTmId == x.TrnTmId)).ToList();
        //    }
        //    foreach (var item in LTrainingTeams)
        //    {
        //        item.LTeamGroups = db.TeamGroups.Where(tt => tt.Status == "Active" && tt.TrnTmId == item.TrnTmId).ToList();
        //        if (item.LTeamGroups == null)
        //            if (item.LTeamGroups.Count > 0)
        //            {
        //                foreach (var TG in item.LTeamGroups)
        //                {
        //                    TG.LClients = db.Clients.Where(tt => tt.Status == "Active" && tt.TrnTmId == TG.TrnTmId && tt.TmGrpId == TG.TmGrpId).ToList();
        //                }
        //            }
        //    }

        //}


        //// how much instructor can train
        //int instructorTraining_by_atendc = 0;
        //foreach (var item in LInstructors)
        //{
        //    instructorTraining_by_atendc += (int)item.LInstrsAttendances.Sum(tt => tt.IAShiftEnd - tt.IAShiftStart);
        //}

        //// how much clients need trainings
        //int clients_need_trains = 0;
        //foreach (var item in LTrainingTeams)
        //{
        //    int days = dateTo - dateFrom;
        //    clients_need_trains += (int)item.LTeamGroups.Count() * days / item.Package.PkTrnAmountWeek;
        //}

        //if (clients_need_trains > instructorTraining_by_atendc)
        //{

        //}
        /*

                         LInstructors = db.Instructors.Where(tt => tt.Status == "Active").ToList();
                         if (LInstructors == null)
                             return LTrainingDates;


                         LInstructors = db.Instructors.Where(tt => tt.Status == "Active").ToList();
                         if (LInstructors == null)
                             return LTrainingDates;


                         foreach (var item in LInstructors)
                         {
                             item.LInstrsAttendances = db.InstrsAttendances.Where(tt => tt.Status == "Active" && tt.IAShiftDate >= dateFrom && tt.IAShiftDate <= dateTo).ToList();
                         }

                                 LTrainingTeams.Add
                         }
                         else
                         {

                         }


                         LPackages = db.Packages.Where(tt => tt.Status == "Active" && tt.PkDateStart >= dateFrom && tt.PkDateEnd <= dateTo).ToList();
                         if (LPackages == null)
                             return LTrainingDates;
                         foreach (var item in LPackages)
                         {
                             item.LRequriments = db.PkgRequrmnts.Where(tt => tt.Status == "Active" && tt.PkgId >= item.PkgId).ToList();
                         }
                         LTrainingTeams = db.TrainingTeams.ToList().Where(x => x.Status == "Active" && LPackages.Any(y => y.TrnTmId == x.TrnTmId)).ToList();
                         if (LTrainingTeams == null)
                             return LTrainingDates;
                         if (TrnTmId != 0)
                         {
                             LTrainingTeams = LTrainingTeams.Where(tt => tt.TrnTmId == TrnTmId).ToList();
                             LPackages = LPackages.Where(x => LTrainingTeams.Any(y => y.TrnTmId == x.TrnTmId)).ToList();
                         }
                         foreach (var item in LTrainingTeams)
                         {
                             item.LTeamGroups = db.TeamGroups.Where(tt => tt.Status == "Active" && tt.TrnTmId == item.TrnTmId).ToList();
                             if (item.LTeamGroups == null)
                                 if (item.LTeamGroups.Count > 0)
                                 {
                                     foreach (var TG in item.LTeamGroups)
                                     {
                                         TG.LClients = db.Clients.Where(tt => tt.Status == "Active" && tt.TrnTmId == TG.TrnTmId && tt.TmGrpId == TG.TmGrpId).ToList();
                                     }
                                 }
                         }

                     }


                     // how much instructor can train
                     int instructorTraining_by_atendc = 0;
                     foreach (var item in LInstructors)
                     {
                         instructorTraining_by_atendc += (int)item.LInstrsAttendances.Sum(tt => tt.IAShiftEnd - tt.IAShiftStart);
                     }

                     // how much clients need trainings
                     int clients_need_trains = 0;
                     foreach (var item in LTrainingTeams)
                     {
                         int days = dateTo - dateFrom;
                         clients_need_trains += (int)item.LTeamGroups.Count() * days / item.Package.PkTrnAmountWeek;
                     }

                     if (clients_need_trains > instructorTraining_by_atendc)
                     {

                     }                             
        return LTrainingDates;
    }
*/
    #endregion x2
    #endregion codeXXX
}