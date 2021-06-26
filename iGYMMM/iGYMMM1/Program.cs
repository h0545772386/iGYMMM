using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iGYMMM1
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var db =new Model1())
            {
                var a = db.Gyms.ToList();
            }
           
        }
    }
}
