using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iGYMMM1
{
    public class AssigningAdapter
    {

        public static List<AssignmentsDate> CreateTraining(int dateFrom, int dateTo, int TrnTmId)
        {
            List<Instructor> LInstructors = new List<Instructor>();
            TrainingTeam TrainingTeam = new TrainingTeam();
            List<AssignmentsDate> LTrainingDates = new List<AssignmentsDate>();

            using (var db = new Model1())
            {
                TrainingTeam = db.TrainingTeams.FirstOrDefault(tt => tt.Status == "Active" && tt.TrnTmId == TrnTmId);
                if (TrainingTeam == null)
                    return LTrainingDates;

                TrainingTeam.Package = db.Packages.FirstOrDefault(tt => tt.Status == "Active" && tt.TrnTmId == TrnTmId && tt.PkDateStart <= dateFrom && tt.PkDateEnd >= dateTo);
                if (TrainingTeam.Package == null)
                    return LTrainingDates;

                var LInstrAtendcs = db.InstrsAttendances.Where(tt => tt.Status == "Active" && tt.IAShiftDate >= dateFrom && tt.IAShiftDate <= dateTo).ToList();
                if (LInstrAtendcs == null)
                    return LTrainingDates;
                LInstructors = db.Instructors.Where(tt => tt.Status == "Active").ToList().Where(x => x.Status == "Active" && LInstrAtendcs.Any(y => y.InstrId == x.InstrId)).ToList();
                if (LInstructors == null)
                    return LTrainingDates;
                foreach (var item in LInstructors)
                {
                    item.LInstrsAttendances = db.InstrsAttendances.Where(tt => tt.Status == "Active" && tt.IAShiftDate >= dateFrom && tt.IAShiftDate <= dateTo).ToList();
                }




            }

            return LTrainingDates;
        }


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
                             */
            return LTrainingDates;
        }
    }
}
