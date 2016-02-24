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

namespace bindingDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // moduulitason muuttujat
        HockeyLeague smliiga;
        List<HockeyTeam> joukkueet;
        int clicked = 0;
        public MainWindow()
        {
            InitializeComponent();
            fillMyCombox();
            smliiga = new HockeyLeague();
            joukkueet = smliiga.GetTeams();
        }

        private void fillMyCombox()
        {
            cbCourses2.Items.Add("IIO12110 Ohjelmistotuotanto");
            cbCourses2.Items.Add("DED666 Työelämän ruåtsi");
            cbCourses2.Items.Add("J3EEE Kesäloma");
        }

        private void btnSetUser_Click(object sender, RoutedEventArgs e)
        {
            txtUserName.Text = "Helou " + bindingDemo.Properties.Settings.Default.UserName;
        }

        private void btnBind_Click(object sender, RoutedEventArgs e)
        {
            myGrid.DataContext = joukkueet;
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            clicked++;
            if (clicked > 3) clicked = 3;
            myGrid.DataContext = joukkueet[clicked];
        }

        private void btnBackward_Click(object sender, RoutedEventArgs e)
        {
            clicked--;
            if (clicked < 0) clicked = 0;
            myGrid.DataContext = joukkueet[clicked];
        }
    }
}
