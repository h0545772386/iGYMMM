using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iGYMMM1
{
    public partial class AppGlobals
    {
        private static AppGlobals _AppGlobalVars;
        public static AppGlobals Instance
        {
            get { return _AppGlobalVars ?? (_AppGlobalVars = new AppGlobals()); }
        }

        public int GymId { get; set; }
        public User LoggedUser { get; set; }


        public AppGlobals()
        {
            GymId = 1000;
            LoggedUser = new User();
        }



    }
}
