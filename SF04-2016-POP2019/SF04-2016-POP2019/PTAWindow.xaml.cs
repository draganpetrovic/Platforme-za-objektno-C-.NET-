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
    /// Interaction logic for PTAWindow.xaml
    /// </summary>
    public partial class PTAWindow : Window
    {
        ICollectionView view;
        private User ulogovan;


        public PTAWindow(User user)
        {
            Data.TerminiPTA(user);
            InitializeComponent();
            if (user.GetType() == typeof(TeacherAsistent))
                btnAsistenti.Visibility = Visibility.Collapsed;
            InitalizeView();
            txtUsername.IsReadOnly = true;
            txtName.IsReadOnly = true;
            txtEmail.IsReadOnly = true;
            txtPassword.IsReadOnly = true;
            ulogovan = user;
            this.DataContext = user;
        }

        private void InitalizeView()
        {
            view = CollectionViewSource.GetDefaultView(Data.TerminiProfA);
            dgTerminiPTA.ItemsSource = view;
        }

        private void dgTerminiPTA_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Error") || e.PropertyName.Equals("Active") || e.PropertyName.Equals("TerminID"))
            {
                e.Column.Visibility = Visibility.Collapsed;
            }

        }

        private void btnDodajTermin_Click(object sender, RoutedEventArgs e)
        {
            TerminiPTAcrud t = new TerminiPTAcrud(ulogovan);
            t.ShowDialog();
        }

        private void btnAsistenti_Click(object sender, RoutedEventArgs e)
        {
            Profesor p = ulogovan as Profesor;
            AsistentiProfesoraLista a = new AsistentiProfesoraLista(p);
            a.ShowDialog();
        }
    }
}
