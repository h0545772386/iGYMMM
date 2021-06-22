using System.Collections.Generic;

namespace iGYMMM
{
    public class ConstVar
    {
        public string EngConst { get; set; }
        public string HebConst { get; set; }
    }

    public class DictConstsVars
    {
        public List<ConstVar> l_ConstVar { get; set; }

        public DictConstsVars()
        {
            l_ConstVar = new List<ConstVar>()
            {
               new ConstVar() { EngConst="Order",HebConst="הזמנה" },
               new ConstVar() { EngConst="Cash",HebConst="מזומן" },
               new ConstVar() { EngConst="Transfer",HebConst="העברה" },
               new ConstVar() { EngConst="Check",HebConst="צ'קים" },
               new ConstVar() { EngConst="Cheek",HebConst="צ'קים" },
               new ConstVar() { EngConst="Letter",HebConst="מכתב" },
               new ConstVar() { EngConst="SMS",HebConst="הודעת טקסט" },
               new ConstVar() { EngConst="Credit",HebConst="אשראי" },
               new ConstVar() { EngConst="ChkRet",HebConst="החזרת צ'ק" },
               new ConstVar() { EngConst="ChkRetC",HebConst="עמלת החזרת צ'ק" }

            };

        }

    }


}
