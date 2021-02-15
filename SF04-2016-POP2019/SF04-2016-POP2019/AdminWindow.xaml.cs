using SF4_2016_POP2019;
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

namespace SF04_2016_POP2019
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void BtnUser_Click(object sender, RoutedEventArgs e)
        {
            UserMain userMain = new UserMain();
            userMain.Show();
        }

        private void BtnFaculty_Click(object sender, RoutedEventArgs e)
        {
            FacultyWindow fw = new FacultyWindow();
            fw.Show();
        }

        private void BtnClassroom_Click(object sender, RoutedEventArgs e)
        {
            ClassroomWindow cw = new ClassroomWindow();
            cw.Show();
        }

        private void BtnTermin_Click(object sender, RoutedEventArgs e)
        {
            TerminWindow tw = new TerminWindow();
            tw.Show();
        }

    }
}
