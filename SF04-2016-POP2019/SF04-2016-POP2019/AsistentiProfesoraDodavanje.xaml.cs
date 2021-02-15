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
    /// Interaction logic for AsistentiProfesoraDodavanje.xaml
    /// </summary>
    public partial class AsistentiProfesoraDodavanje : Window
    {
        ICollectionView view;
        Profesor prof;

        public AsistentiProfesoraDodavanje(Profesor profesor)
        {
            InitializeComponent();
            prof = profesor;
            InitalizeView();
        }

        private void InitalizeView()
        {
            Data.ReadTA();
            view = CollectionViewSource.GetDefaultView(Data.TA);
            dgAsistenti.ItemsSource = view;
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            TeacherAsistent ta = dgAsistenti.SelectedValue as TeacherAsistent;
            if (prof.Assistants.Contains(ta))
            {
                MessageBox.Show("Izabrani asistent je vec dodeljen vama", "Warning", MessageBoxButton.OK);
            }
            else if(ta.ProfesorID != 0)
            {
                MessageBox.Show("Izabrani asistent ima dodeljenog profesora", "Warning", MessageBoxButton.OK);
            }
            else
            {
                prof.Assistants.Add(ta);
                ta.ProfesorID = Data.GetUserIDbyUsername(prof.Username);
                Data.SacuvajTA(prof.Username, ta.Username);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void dgAsistenti_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Error") || e.PropertyName.Equals("Active"))
                e.Column.Visibility = Visibility.Collapsed;
        }
    }
}
