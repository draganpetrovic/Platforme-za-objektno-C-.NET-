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
    /// Interaction logic for FacultyWindow.xaml
    /// </summary>
    /// 

    public partial class FacultyWindow : Window
    {
        ICollectionView view;
        String selectedFilter = "Active";
        List<Faculty> sortirana = Data.Faculties;

        public FacultyWindow()
        {
            InitializeComponent();
            InitalizeView();
            view.Filter = CostumFilter;
            dgFaculty.IsReadOnly = true;
        }

        private bool CostumFilter(object obj)
        {
            Faculty fak = obj as Faculty;
            if (selectedFilter.Equals("SEARCH"))
            {
                return ((fak.Active && fak.Address.ToLower().Contains(txtAddress.Text.ToLower()))
                    && fak.NameF.ToLower().Contains(txtName.Text.ToLower()));
            }
            else
            {
                return fak.Active;
            }
        }

        private void InitalizeView()
        {
            view = CollectionViewSource.GetDefaultView(Data.Faculties);
            dgFaculty.ItemsSource = view;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Faculty fakultet = new Faculty();
            FacultyCRUD fcrud = new FacultyCRUD(fakultet);
            if(fcrud.ShowDialog() == true)
            {
                selectedFilter = "Active";
                view.Refresh();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Faculty fakultet = dgFaculty.SelectedValue as Faculty;
            if (fakultet != null)
            {
                Faculty fakultetDelete = Data.GetFacultyById(fakultet.FacultyID);
                fakultetDelete.Active = false;
                fakultetDelete.DeleteFaculty();
                selectedFilter = "Active";
                view.Refresh();
            }
            else
            {
                MessageBox.Show("Faculty does not exist", "Warning", MessageBoxButton.OK);
            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Faculty fakultet = dgFaculty.SelectedItem as Faculty;

            if (fakultet == null)
            {
                MessageBox.Show("Faculty  not selected", "Warning", MessageBoxButton.OK);
            }
            else
            {
                Faculty oldFaculty = fakultet.Clone();
                FacultyCRUD fcrud = new FacultyCRUD(fakultet);
                if (fcrud.ShowDialog() == false)

                {
                    int indeks = Data.Faculties.FindIndex(f => f.FacultyID.Equals(oldFaculty.FacultyID));
                    Data.Faculties[indeks] = oldFaculty;
                }
                selectedFilter = "Active";
                view.Refresh();
            }
        }

        private void btnRaspored_Click(object sender, RoutedEventArgs e)
        {
            Faculty fakultet = dgFaculty.SelectedItem as Faculty;
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            selectedFilter = "SEARCH";
            view.Refresh();
        }

        private void dgFaculty_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Error") || e.PropertyName.Equals("Active") || e.PropertyName.Equals("FacultyID"))
            {
                e.Column.Visibility = Visibility.Collapsed;
                
            }
        }

        private void rbName_Checked(object sender, RoutedEventArgs e)
        {
            List<Faculty> sortirana = Data.Faculties.OrderBy(u => u.NameF).ToList();
            view = CollectionViewSource.GetDefaultView(sortirana);
            dgFaculty.ItemsSource = view;
            view.Filter = CostumFilter;

        }

        private void rbAddress_Checked(object sender, RoutedEventArgs e)
        {
            List<Faculty> sortirana = Data.Faculties.OrderBy(u => u.Address).ToList();
            view = CollectionViewSource.GetDefaultView(sortirana);
            dgFaculty.ItemsSource = view;
            view.Filter = CostumFilter;

        }

    }
}
