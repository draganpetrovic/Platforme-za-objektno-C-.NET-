using SF04_2016_POP2019.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace SF04_2016_POP2019
{
    /// <summary>
    /// Interaction logic for ScheduleAdminWindow.xaml
    /// </summary>
    public partial class ScheduleAdminWindow : Window
    {
        ICollectionView view;
        String selectedFilter = "Active";

        public ScheduleAdminWindow()
        {
            InitializeComponent();
            InitalizeView();
        }

        private void InitalizeView()
        {
            view = CollectionViewSource.GetDefaultView(Data.TerminiAdmina);
            dgScheduleAdmin.ItemsSource = view;
        }

        private void dgScheduleAdmin_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Active") || e.PropertyName.Equals("TerminID"))
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
        }

    }
}
