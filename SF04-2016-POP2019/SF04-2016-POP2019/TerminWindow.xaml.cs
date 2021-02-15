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
using DayOfWeek = SF04_2016_POP2019.Models.DayOfWeek;

namespace SF04_2016_POP2019
{
    /// <summary>
    /// Interaction logic for TerminWindow.xaml
    /// </summary>
    public partial class TerminWindow : Window
    {
        ICollectionView view;
        String selectedFilter = "Active";

        public TerminWindow()
        {
            InitializeComponent();
            cmbDan.ItemsSource = new List<DayOfWeek>() { DayOfWeek.PONEDELJAK, DayOfWeek.UTORAK, DayOfWeek.SREDA, DayOfWeek.CETVRTAK, DayOfWeek.PETAK, DayOfWeek.SUBOTA, DayOfWeek.NEDELJA};
            cmbTip.ItemsSource = new List<TipNastave>() { TipNastave.PREDAVANJA, TipNastave.LABORATORIJA, TipNastave.VEZBE };
            dgTermin.IsReadOnly = true;
            InitalizeView();
            view.Filter = CostumFilter;
        }

        private bool CostumFilter(object obj)
        {
            Termin t = obj as Termin;
            if (selectedFilter.Equals("SEARCH") && (cmbTip.SelectedIndex >= 0) && (cmbDan.SelectedIndex >= 0))
            {
                return ((t.Active && t.Vreme1.ToLower().Contains(txtVreme1.Text.ToLower())) &&
                     t.Vreme2.ToLower().Contains(txtVreme2.Text.ToLower()) &&
                     t.ClassroomId.ToString().Contains(txtClassroomId.Text.ToString()) &&
                     t.UserId.ToString().Contains(txtUserId.Text.ToString())
                     && t.DayOfWeek.ToString().ToLower().Contains(cmbDan.SelectedItem.ToString().ToLower())
                     && t.TipNastave.ToString().ToLower().Contains(cmbTip.SelectedItem.ToString().ToLower()));
            }

            else if (selectedFilter.Equals("SEARCH") && cmbTip.SelectedIndex >= 0)
            {
                return ((t.Active && t.Vreme1.ToLower().Contains(txtVreme1.Text.ToLower())) &&
                     t.Vreme2.ToLower().Contains(txtVreme2.Text.ToLower()) &&
                     t.ClassroomId.ToString().Contains(txtClassroomId.Text.ToString()) &&
                     t.UserId.ToString().Contains(txtUserId.Text.ToString())
                      && t.TipNastave.ToString().ToLower().Contains(cmbTip.SelectedItem.ToString().ToLower()));
            }

            else if (selectedFilter.Equals("SEARCH") && cmbDan.SelectedIndex >= 0)
            {
                return (t.Active && t.Vreme1.ToLower().Contains(txtVreme1.Text.ToLower()) &&
                     t.Vreme2.ToLower().Contains(txtVreme2.Text.ToLower()) &&
                     t.ClassroomId.ToString().Contains(txtClassroomId.Text.ToString()) &&
                     t.UserId.ToString().Contains(txtUserId.Text.ToString())
                      && t.DayOfWeek.ToString().ToLower().Contains(cmbDan.SelectedItem.ToString().ToLower()));
            }

            else if (selectedFilter.Equals("SEARCH"))
            {
                return ((t.Active && t.Vreme1.ToLower().Contains(txtVreme1.Text.ToLower())) &&
                     t.Vreme2.ToLower().Contains(txtVreme2.Text.ToLower()) &&
                     t.ClassroomId.ToString().Contains(txtClassroomId.Text.ToString()) &&
                     t.UserId.ToString().Contains(txtUserId.Text.ToString()));
            }
            else
                return t.Active;
        }

        private void InitalizeView()
        {
            view = CollectionViewSource.GetDefaultView(Data.Termini);
            dgTermin.ItemsSource = view;
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = new Termin();
            TerminCRUD t = new TerminCRUD(termin);
            if (t.ShowDialog() == true)
            {
                selectedFilter = "Active";
                view.Refresh();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = dgTermin.SelectedValue as Termin;
            if (termin != null)
            {
                Termin terminDelete = Data.GetTerminById(termin.TerminID);
                terminDelete.Active = false;
                terminDelete.DeleteTermin();
                selectedFilter = "Active";
                view.Refresh();
            }
            else
            {
                MessageBox.Show("Termin ne postoji!", "Warning", MessageBoxButton.OK);
            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = dgTermin.SelectedItem as Termin;

            if (termin == null)
            {
                MessageBox.Show("Termin nije izabran.", "Warning", MessageBoxButton.OK);
            }
            else
            {
                Termin oldTermin = termin.Clone();
                TerminCRUD t = new TerminCRUD(termin);
                if (t.ShowDialog() == false)

                {
                    int indeks = Data.Termini.FindIndex(f => f.TerminID.Equals(oldTermin.TerminID));
                    Data.Termini[indeks] = oldTermin;
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

        private void dgTermin_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Error") || e.PropertyName.Equals("Active") || e.PropertyName.Equals("TerminID"))
            {
                e.Column.Visibility = Visibility.Collapsed;
                

            }
            
        }

    }
}
