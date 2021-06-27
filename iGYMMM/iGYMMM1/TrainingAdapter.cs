using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iGYMMM1
{
    public class TrainingAdapter
    {

        public static List<TrainingDate> CreateTraining(int dateFrom, int dateTo, TrainingTeam trainingTeam = null)
        {
            List<Package> LPackages = new List<Package>();
            List<Instructor> LInstructors = new List<Instructor>();
            List<TrainingDate> LTrainingDate = new List<TrainingDate>();
            using (var db = new Model1())
            {
                LInstructors = db.Instructors.Where(tt => tt.Status == "Active").ToList();
                LInstructors = db.Instructors.Where(tt => tt.Status == "Active").ToList();
                if (LInstructors == null)
                    return LTrainingDate;

                foreach (var item in LInstructors)
                {
                    item.LInstrsAttendances = db.InstrsAttendances.Where(tt => tt.Status == "Active" && tt.IAShiftDate >= dateFrom && tt.IAShiftDate <= dateTo).ToList();
                }
                LPackages = db.Packages.Where(tt => tt.Status == "Active" && tt.PkDateStart >= dateFrom && tt.PkDateEnd <= dateTo).ToList();




                foreach (var item in LPackages)
                {
                    item.LRequriments = db.PkgRequrmnts.Where(tt => tt.Status == "Active" && tt.PkgId >= item.PkgId).ToList();
                }
            }

            if (trainingTeam == null)
            {

            }
            else
            {

            }
            return LTrainingDate;
        }
    }
}
