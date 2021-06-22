using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iGYMMM
{
    public class DB_Commands
    {
        /*
        public static void Update_Balance4Clients(DB2 db)
        {
            string SqlCommand =
                              "UPDATE [myShopN1].[dbo].[Clients] set [ClntBalance] = " +
                                      "ISNULL( " +
                                      "ISNULL((SELECT SUM([Total]) FROM[myShopN1].[dbo].[Sales] WHERE [Deleted] = 0 AND [ClntNum] = [myShopN1].[dbo].[Clients].[ClntNum]) ,0) - " +
                                            "(( " +
                                            "ISNULL((SELECT SUM([TotalDepoOLD]) FROM[myShopN1].[dbo].[Sales] WHERE [Deleted] = 0 AND [ClntNum] = [myShopN1].[dbo].[Clients].[ClntNum]) ,0) + " +
                                            "ISNULL( (SELECT SUM([Total]) FROM [myShopN1].[dbo].[Deposits] WHERE [OLD_Software] = 0 AND [Deleted] = 0 AND [ClntNum] = [myShopN1].[dbo].[Clients].[ClntNum]) ,0) " +
                                            ")) " +
                                            ",0);";
                db.Database.ExecuteSqlCommand(SqlCommand);
        }

        public static void Update_LastBuyDate4Clients(DB2 db)
        {
            string SqlCommand = "UPDATE [myShopN1].[dbo].[Clients] SET [LastBuyDate] = " +
                                   "ISNULL((select max(Date1) from [myShopN1].[dbo].[Sales] WHERE [Deleted] = 0 AND [ClntNum] = [myShopN1].[dbo].[Clients].[ClntNum]) ,0);";
            db.Database.ExecuteSqlCommand(SqlCommand);
        }

        public static void Update_LastPayDate4Clients(DB2 db)
        {
            string SqlCommand = "UPDATE [myShopN1].[dbo].[Clients] SET [LastPayDate] = " +
                                   "ISNULL((select max(Date1) from [myShopN1].[dbo].[Deposits] WHERE [Deleted] = 0 AND [ClntNum] = [myShopN1].[dbo].[Clients].[ClntNum]) ,0);";
            db.Database.ExecuteSqlCommand(SqlCommand);
        }
        */
    }
}
