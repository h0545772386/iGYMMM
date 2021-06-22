using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace iGYMMM
{
    public class AppFontSize : INotifyPropertyChanged
    {
        double dg_fontSize { get; set; }
        double tb_fontSize { get; set; }

        public AppFontSize()
        {
            //dg_fontSize = Properties.Settings.Default.DG_FontSize;
            //tb_fontSize = Properties.Settings.Default.TB_FontSize;
        }
        public double DGFont_Size
        {
            get { return dg_fontSize; }
            set
            {
                dg_fontSize = value;
                OnPropertyChanged();
            }
        }
        public double TBFont_Size
        {
            get { return tb_fontSize; }
            set
            {
                tb_fontSize = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
