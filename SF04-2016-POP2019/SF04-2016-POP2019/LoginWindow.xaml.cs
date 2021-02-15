using SF04_2016_POP2019.Models;
using SF04_2016_POP2019.Util;
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
using System.Windows.Shapes;

namespace SF04_2016_POP2019
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        

        public LoginWindow()
        {
            Data.ReadUsers();
            Data.ReadFaculties();
            Data.ReadClassroom();
            Data.ReadTermini();
            Data.ReadTA_ProfesorID();
            Data.ListOfTA();
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            
            if (!String.IsNullOrEmpty(txtIme.Text) && !String.IsNullOrEmpty(txtLozinka.Password.ToString()))
            {
                if (ProveriKorisnika(txtIme.Text, txtLozinka.Password.ToString()) == true)
                {
                    User korisnik = Data.GetUserByUsername(txtIme.Text);
                    if (korisnik.GetType() == typeof(Administrator))
                    {
                        //otvori prozor administratora
                        AdminWindow aP = new AdminWindow();
                        aP.ShowDialog();
                    }
                    else if (korisnik.GetType() == typeof(Profesor) || korisnik.GetType() == typeof(TeacherAsistent))
                    {
                        PTAWindow ptaW = new PTAWindow(korisnik);
                        ptaW.ShowDialog();
                    }
                }
            else if (String.IsNullOrEmpty(txtIme.Text) || String.IsNullOrEmpty(txtLozinka.Password.ToString()))
            {
                MessageBox.Show("Niste uneli Korisnicko ime ili lozinku.", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Uneti podaci nisu ispravni.", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            }
        }

        private void btnBezPrijave_Click(object sender, RoutedEventArgs e)
        {

            NeprijavljenWindow nw = new NeprijavljenWindow();
            nw.Show();
        }


        private bool ProveriKorisnika(string ime, string lozinka)
        {
            foreach (User korisnik in Data.Users)
            {
                if (korisnik.Username.Equals(ime) && korisnik.Password.Equals(lozinka))
                {
                    
                    return true;
                }
              
            }
            return false;
        }

     
    }
}
