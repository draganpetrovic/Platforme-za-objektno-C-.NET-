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
    /// Interaction logic for AsistentiProfesoraLista.xaml
    /// </summary>
    public partial class AsistentiProfesoraLista : Window
    {
        ICollectionView view;
        private Profesor prof;


        public AsistentiProfesoraLista(Profesor profesor)
        {
            prof = profesor;
            InitializeComponent();
            
            InitalizeView();

        }

        private void InitalizeView()
        {

            view = CollectionViewSource.GetDefaultView(prof.Assistants);
            dgAsistentiProfesora.ItemsSource = view;

        }

        private void dgAsistentiProfesora_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Error") || e.PropertyName.Equals("Active"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void ButtonDodaj_Click(object sender, RoutedEventArgs e)
        {
            AsistentiProfesoraDodavanje a = new AsistentiProfesoraDodavanje(prof);
            a.ShowDialog();
        }

        private void ButtonUkloni_Click(object sender, RoutedEventArgs e)
        {
            TeacherAsistent ta = dgAsistentiProfesora.SelectedValue as TeacherAsistent;
            if(ta != null)
            {
                prof.Assistants.Remove(ta);
                ta.ProfesorID = 0;
                int id = Data.GetUserIDbyUsername(ta.Username);
                Data.ObrisiTA(id);
                Data.ReadTA_ProfesorID();
            }
            else
            {
                MessageBox.Show("Asistent ne postoji", "Warning", MessageBoxButton.OK);
            }
        }
    }
}
