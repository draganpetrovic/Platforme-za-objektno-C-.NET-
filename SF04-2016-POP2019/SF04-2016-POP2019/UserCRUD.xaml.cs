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
    /// Interaction logic for UserCRUD.xaml
    /// </summary>
    public partial class UserCRUD : Window
    {
        enum Status { ADD, EDIT }
        private Status _status;
        private User selectedUser;

        public UserCRUD(User user)
        {
            InitializeComponent();
            cmbType.ItemsSource = new List<TypeOfUser>() { TypeOfUser.ADMIN, TypeOfUser.PROFESOR, TypeOfUser.TA };
            if (user.Username.Equals(""))
            {
                this._status = Status.ADD;

            }
            else
            {
                this._status = Status.EDIT;

                txtUsername.IsReadOnly = true;
            }
            selectedUser = user;
            this.DataContext = user; //data binding
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            User user = Data.GetUserByUsername(txtUsername.Text);
            if (user != null && _status.Equals(Status.ADD))
            {
                MessageBox.Show($"User with username  {user.Username} exits",
                    "Warning", MessageBoxButton.OK);
                return;
            }
            if (_status.Equals(Status.ADD))
            {
                Data.Users.Add(selectedUser);
                selectedUser.SaveUser(); //cuva u bazi

            }
            //izmena podataka
            if (_status.Equals(Status.EDIT))
                selectedUser.UpdateUser();

            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
