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
    /// Interaction logic for ClassroomWindow.xaml
    /// </summary>
    public partial class ClassroomWindow : Window
    {
        ICollectionView view;
        String selectedFilter = "Active";
        List<Classroom> sortirana = Data.Classrooms;

        public ClassroomWindow()
        {
            InitializeComponent();
            cmbTypeOfClassroom.ItemsSource = new List<TypeOfClassroom>() { TypeOfClassroom.AMPHITHEATER, TypeOfClassroom.IT, TypeOfClassroom.LABORATORY };
            dgClassroom.IsReadOnly = true;

            InitalizeView();
            view.Filter = CostumFilter;
        }

        private bool CostumFilter(object obj)
        {
            Classroom cr = obj as Classroom;
            if (selectedFilter.Equals("SEARCH") && cmbTypeOfClassroom.SelectedIndex >= 0)
                return (cr.Active && cr.NameC.ToLower().Contains(txtSearchName.Text.ToLower()) &&
                    cr.SeatsC.ToString().Contains(txtSearchSeats.Text.ToString()) &&
                    cr.Faculty_Id.ToString().Contains(txtSearchFaculty.Text.ToString())
                    && cr.TypeOfClassroom.ToString().ToLower().Contains(cmbTypeOfClassroom.SelectedItem.ToString().ToLower()));
            else if (selectedFilter.Equals("SEARCH"))
                return (cr.Active && cr.NameC.ToLower().Contains(txtSearchName.Text.ToLower()) &&
                   cr.SeatsC.ToString().Contains(txtSearchSeats.Text.ToString()) &&
                   cr.Faculty_Id.ToString().Contains(txtSearchFaculty.Text.ToString()));
            else
                return cr.Active;
        }

        private void InitalizeView()
        {
            view = CollectionViewSource.GetDefaultView(Data.Classrooms);
            dgClassroom.ItemsSource = view;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Classroom ucionica = new Classroom();
            ClassroomCRUD c = new ClassroomCRUD(ucionica);
            if (c.ShowDialog() == true)
            {
                selectedFilter = "Active";
                view.Refresh();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Classroom ucionica = dgClassroom.SelectedValue as Classroom;
            if (ucionica != null)
            {
                Classroom ucionicaDelete = Data.GetClassroomById(ucionica.ClassroomID);
                ucionicaDelete.Active = false;
                ucionicaDelete.DeleteClassroom();
                selectedFilter = "Active";
                view.Refresh();
            }
            else
            {
                MessageBox.Show("Classroom does not exist", "Warning", MessageBoxButton.OK);
            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Classroom ucionica = dgClassroom.SelectedItem as Classroom;

            if (ucionica == null)
            {
                MessageBox.Show("Classroom  not selected", "Warning", MessageBoxButton.OK);
            }
            else
            {
                Classroom oldClassroom = ucionica.Clone();
                ClassroomCRUD cr = new ClassroomCRUD(ucionica);
                if (cr.ShowDialog() == false)
                {
                    int indeks = Data.Classrooms.FindIndex(c => c.ClassroomID.Equals(oldClassroom.ClassroomID));
                    Data.Classrooms[indeks] = oldClassroom;
                }
                selectedFilter = "Active";
                view.Refresh();
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            selectedFilter = "SEARCH";
            view.Refresh();
        }

        private void dgClassroom_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Error") || e.PropertyName.Equals("Active") || e.PropertyName.Equals("ClassroomID"))
            {
                e.Column.Visibility = Visibility.Collapsed;

            }
        }

        private void rbName_Checked(object sender, RoutedEventArgs e)
        {
            List<Classroom> sortirana = Data.Classrooms.OrderBy(u => u.NameC).ToList();
            view = CollectionViewSource.GetDefaultView(sortirana);
            dgClassroom.ItemsSource = view;
            view.Filter = CostumFilter;
        }

        private void rbSeats_Checked(object sender, RoutedEventArgs e)
        {
            List<Classroom> sortirana = Data.Classrooms.OrderBy(u => u.SeatsC).ToList();
            view = CollectionViewSource.GetDefaultView(sortirana);
            dgClassroom.ItemsSource = view;
            view.Filter = CostumFilter;
        }

        private void rbType_Checked(object sender, RoutedEventArgs e)
        {
            List<Classroom> sortirana = Data.Classrooms.OrderBy(u => u.TypeOfClassroom).ToList();
            view = CollectionViewSource.GetDefaultView(sortirana);
            dgClassroom.ItemsSource = view;
            view.Filter = CostumFilter;
        }

        private void rbFaculty_Checked(object sender, RoutedEventArgs e)
        {
            List<Classroom> sortirana = Data.Classrooms.OrderBy(u => u.Faculty_Id).ToList();
            view = CollectionViewSource.GetDefaultView(sortirana);
            dgClassroom.ItemsSource = view;
            view.Filter = CostumFilter;
        }

    }
}
