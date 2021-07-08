using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iGYMMM1
{
    public class AssigningAdapter
    {

        public static List<DiaryHeader> CreateTraining(int dateFrom, int dateTo, int TrnTmId)
        {
            DateTime d1 = dateFrom.Int2Date();
            DateTime d2 = dateTo.Int2Date();
            TrainingTeam TrainingTeam = new TrainingTeam();
            List<DiaryTeam> LDiaryTeams = new List<DiaryTeam>();
            List<Instructor> LInstructors = new List<Instructor>();
            List<DiaryHeader> LDiaryHeaders = new List<DiaryHeader>();
            List<AssigningLog> LAssigningLog = new List<AssigningLog>();  // error log
            List<TrainingTeam> LTrainingTeams = new List<TrainingTeam>();
            List<DiaryHeader> LDiaryHeaders_new = new List<DiaryHeader>();  // new assignments

            #region SeletecFromDB
            using (var db = new Model1())
            {
                TrainingTeam = db.TrainingTeams.FirstOrDefault(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active" && tt.TrnTmId == TrnTmId);
                if (TrainingTeam == null)
                    return LDiaryHeaders_new;

                TrainingTeam.LTeamGroups = db.TeamGroups.Where(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active" && tt.TrnTmId == TrnTmId).ToList();
                if (TrainingTeam.LTeamGroups == null)
                    return LDiaryHeaders_new;

                var LClients = db.Clients.Where(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active" && tt.TrnTmId == TrnTmId).ToList();
                if (LClients == null)
                    return LDiaryHeaders_new;
                foreach (var tmgrp in TrainingTeam.LTeamGroups)
                    tmgrp.LClients = LClients.Where(tt => tt.TrnTmId == TrnTmId && tt.TmGrpId == tmgrp.TmGrpId).ToList();

                TrainingTeam.Package = db.Packages.FirstOrDefault(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active" && tt.TrnTmId == TrnTmId && tt.PkDateStart <= dateFrom && tt.PkDateEnd >= dateTo);
                if (TrainingTeam.Package == null)
                    return LDiaryHeaders_new;

                TrainingTeam.Package.LRequriments = db.PkgRequrmnts.Where(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active" && tt.PkgId == TrainingTeam.Package.PkgId).ToList();
                if (TrainingTeam.Package.LRequriments == null)
                    return LDiaryHeaders_new;

                LTrainingTeams.Add(TrainingTeam);

                var LInstrAtendcs = db.InstrsAttendances.Where(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active" && tt.IAShiftDate >= dateFrom && tt.IAShiftDate <= dateTo).ToList();
                if (LInstrAtendcs == null)
                    return LDiaryHeaders_new;
                LInstructors = db.Instructors.Where(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active").ToList()
                                             .Where(x => LInstrAtendcs.Any(y => y.InstrId == x.InstrId)).ToList().Distinct().ToList();
                if (LInstructors == null)
                    return LDiaryHeaders_new;
                foreach (var item in LInstructors)
                {
                    item.LInstrsAttendances = db.InstrsAttendances.Where(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active" && tt.IAShiftDate >= dateFrom && tt.IAShiftDate <= dateTo).ToList();
                }

                // get already assignments from database
                LDiaryHeaders = db.DiaryHeaders.Where(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active" && tt.TrnDate <= dateFrom && tt.TrnDate >= dateTo).ToList();
                if (LDiaryHeaders != null)
                {
                    var LDIs = db.DiaryItems.Where(tt => tt.GymId == AppGlobals.Instance.GymId && tt.Status == "Active" && tt.TrnDate <= dateFrom && tt.TrnDate >= dateTo).ToList();
                    if (LDIs != null)
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
            foreach (var trnTeam in LTrainingTeams)
            {
                for (DateTime dd = d1; dd < d2; dd = dd.AddDays(1))   // period days
                {
                    foreach (var PkgReq in TrainingTeam.Package.LRequriments)
                    {
                        if (dd.DayOfWeek.ToString().ToLower() == PkgReq.PkReqDOW.ToLower())
                            LDiaryHeaders_new.AddRange(GoAssignTrns4Day(dd, trnTeam, PkgReq, LInstructors, LDiaryHeaders));
                    }
                }
            }

            // check existing training with same properties at database
            // paint those 








            return LDiaryHeaders_new;
        }

        private static List<DiaryHeader> GoAssignTrns4Day(DateTime dd, TrainingTeam trnTeam, PkgRequrmnt pkgReq, List<Instructor> lInstructors, List<DiaryHeader> lDiaryHeaders)
        {
            List<DiaryHeader> LDH = new List<DiaryHeader>();
            foreach (var TeamGrp in trnTeam.LTeamGroups)
            {
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
                //using (var db = new Model1())
                //{
                //    db.DiaryHeaders.Add(dh);
                //    db.SaveChanges();
                //}

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
                    //using (var db = new Model1())
                    //{
                    //    db.DiaryItems.Add(di);
                    //    db.SaveChanges();
                    //}
                    dh.LDiaryItems.Add(di);
                }
                LDH.Add(dh);
            }
            return LDH;
        }

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


        #region codeXXX 

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