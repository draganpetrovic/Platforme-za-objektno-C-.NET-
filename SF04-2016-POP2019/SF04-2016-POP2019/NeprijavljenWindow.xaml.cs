using SF04_2016_POP2019.Models;
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
    /// Interaction logic for NeprijavljenListaWindow.xaml
    /// </summary>
    public partial class NeprijavljenWindow : Window
    {
        ICollectionView view;
        String selectedFilter = "Active";

        public NeprijavljenWindow()
        {
            InitializeComponent();
            InitalizeView();
            view.Filter = CostumFilter;
        }

        private bool CostumFilter(object obj)
        {
            Faculty fak = obj as Faculty;
            return fak.Active;
        }

        private void InitalizeView()
        {
            view = CollectionViewSource.GetDefaultView(Data.Faculties);
            dgFakulteti.ItemsSource = view;
        }

        private void dgFaculty_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Error") || e.PropertyName.Equals("Active") || e.PropertyName.Equals("FacultyID"))
            {
                e.Column.Visibility = Visibility.Collapsed;

            }
        }

        private void btnPredavanjaVezbe_Click(object sender, RoutedEventArgs e)
        {
            Faculty fakultet = dgFakulteti.SelectedItem as Faculty;
            if (fakultet == null)
            {
                MessageBox.Show("Faculty  not selected", "Warning", MessageBoxButton.OK);
            }
            else
            {
                Data.GetTerminByFacultyID(fakultet);
                ScheduleAdminWindow saw = new ScheduleAdminWindow();
                saw.Show();
            }
        }

        private void btnProfesoriAsistenti_Click(object sender, RoutedEventArgs e)
        {
            Faculty fakultet = dgFakulteti.SelectedItem as Faculty;
            if (fakultet == null)
            {
                MessageBox.Show("Faculty  not selected", "Warning", MessageBoxButton.OK);
            }
            else
            {
                Data.UcitajZaposlen(fakultet);
                ZaposleniWindow zw = new ZaposleniWindow();
                zw.Show();
            }
        }
    }
}