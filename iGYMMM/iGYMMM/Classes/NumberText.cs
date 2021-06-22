using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace iGYMMM
{
    class NumberText : TextBox
    {
        /// <summary>
        /// Get int value
        /// </summary>
        public int IntValue
        {
            get
            {
                try
                {
                    string txt = "";
                    Dispatcher.Invoke(() => {
                        txt = Text;
                    });
                    return IntVal(txt);
                }
                catch
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// Get demical value
        /// </summary>
        public decimal DemicalValue
        {
            get
            {
                try
                {
                    string txt = "";
                    Dispatcher.Invoke(() => {
                        txt = Text;
                    });
                    return Convert.ToDecimal(txt);
                }
                catch
                {
                    return 0;
                }
            }
        }
        protected override void OnInitialized(EventArgs e)
        {
            VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            base.OnInitialized(e);
        }
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9.-]+");

            e.Handled = re.IsMatch(e.Text);

            base.OnPreviewTextInput(e);
        }
        public static int IntVal(object o)
        {
            if (o == null || o.ToString() == "")
                return 0;
            //int val = 0;
            Int32.TryParse(o.ToString(), out int val);
            return val;
        }
    }

  
}
