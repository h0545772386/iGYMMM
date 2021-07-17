using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iGYMMM3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //CEED.ceed();
            //var l1 = AssigningAdapter.CreateTrainingAllTeams(20210815, 20210821);
            var l1 = AssigningAdapter.CreateTrainingAllTeams(20210815, 20210921);//, 100614);            
        }
    }
}
