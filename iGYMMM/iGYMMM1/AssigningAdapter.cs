using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iGYMMM1
{
    public class AssigningAdapter
    {
        
        public static List<AssignmentsDate> CreateTraining(int dateFrom, int dateTo, TrainingTeam trainingTeam = null)
        {
            List<Package> LPackages = new List<Package>();
            List<Instructor> LInstructors = new List<Instructor>();
            List<TrainingTeam> LTrainingTeams = new List<TrainingTeam>();
            List<AssignmentsDate> LTrainingDates = new List<AssignmentsDate>();

            using (var db = new Model1())
            {
                LInstructors = db.Instructors.Where(tt => tt.Status == "Active").ToList();
                LInstructors = db.Instructors.Where(tt => tt.Status == "Active").ToList();
                if (LInstructors == null)
                    return LTrainingDates;

                
                foreach (var item in LInstructors)
                {
                    item.LInstrsAttendances = db.InstrsAttendances.Where(tt => tt.Status == "Active" && tt.IAShiftDate >= dateFrom && tt.IAShiftDate <= dateTo).ToList();
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
                foreach (var item in LTrainingTeams)
                {
                    item.LTeamGroups = db.TeamGroups.Where(tt => tt.Status == "Active" && tt.TrnTmId == item.TrnTmId).ToList();
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

            if(clients_need_trains > instructorTraining_by_atendc)
            {

            }

            if (trainingTeam == null)
            {

            }
            else
            {

            }
            return LTrainingDates;
        }
    }
}
