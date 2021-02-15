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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class UserMain : Window
    {
        ICollectionView view;
        String selectedFilter = "Active";
        List<User> sortirana = Data.Users;

        public UserMain()
        {
            //Data.AddUsers();   ovo je kada ne koristimo bazu
            //Data.ReadUsers();
            InitializeComponent();
            cmbType.ItemsSource = new List<TypeOfUser>() { TypeOfUser.ADMIN, TypeOfUser.PROFESOR, TypeOfUser.TA };
            dgUsers.IsReadOnly = true;
            InitalizeView();
            view.Filter = CostumFilter;
        }

        private bool CostumFilter(object obj)
        {
            User user = obj as User;
            if (selectedFilter.Equals("SEARCH") && cmbType.SelectedIndex >= 0)
                return ((user.Active && user.Username.ToLower().Contains(txtSearchUsername.Text.ToLower())) && user.Name.ToLower().Contains(txtSearchName.Text.ToLower()) &&
                        user.Email.ToLower().Contains(txtSearchEmail.Text.ToLower()) && user.TypeOfUser.ToString().ToLower().Contains(cmbType.SelectedItem.ToString().ToLower()));
            else if (selectedFilter.Equals("SEARCH"))
                return ((user.Active && user.Username.ToLower().Contains(txtSearchUsername.Text.ToLower())) && user.Name.ToLower().Contains(txtSearchName.Text.ToLower()) &&
                        user.Email.ToLower().Contains(txtSearchEmail.Text.ToLower()));
            else
                return user.Active;
        }




        private void InitalizeView()
        {

            view = CollectionViewSource.GetDefaultView(Data.Users);
            dgUsers.ItemsSource = view;
            
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Administrator administrator = new Administrator();
            UserCRUD userWindow = new UserCRUD(administrator);
            if (userWindow.ShowDialog() == true)
            {
                selectedFilter = "Active";
                view.Refresh();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            User user = dgUsers.SelectedValue as User;
            if (user != null)
            {
                User userDelete = Data.GetUserByUsername(user.Username);
                userDelete.Active = false;
                userDelete.DeleteUser();
                selectedFilter = "Active";
                view.Refresh();
            }
            else
            {
                MessageBox.Show("User does not exist", "Warning", MessageBoxButton.OK);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            User user = dgUsers.SelectedItem as User;

            if (user == null)
            {
                MessageBox.Show("User  not selected", "Warning", MessageBoxButton.OK);
            }
            else
            {
                User oldUser = user.Clone();
                UserCRUD userWindow = new UserCRUD(user);
                if (userWindow.ShowDialog() == false)
                
                {
                    int indeks = Data.Users.FindIndex(u => u.Username.Equals(oldUser.Username));
                    Data.Users[indeks] = oldUser;
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

        private void dgUsers_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Error") || e.PropertyName.Equals("Active"))
                e.Column.Visibility = Visibility.Collapsed;
        }

        private void rbName_Checked(object sender, RoutedEventArgs e)
        {
            List<User> sortirana = Data.Users.OrderBy(u => u.Name).ToList();
            view = CollectionViewSource.GetDefaultView(sortirana);
            dgUsers.ItemsSource = view;
            view.Filter = CostumFilter;
        }

        private void rbType_Checked(object sender, RoutedEventArgs e)
        {
            List<User> sortirana = Data.Users.OrderBy(u => u.TypeOfUser).ToList();
            view = CollectionViewSource.GetDefaultView(sortirana);
            dgUsers.ItemsSource = view;
            view.Filter = CostumFilter;
        }

        private void rbUsername_Checked(object sender, RoutedEventArgs e)
        {
            List<User> sortirana = Data.Users.OrderBy(u => u.Username).ToList();
            view = CollectionViewSource.GetDefaultView(sortirana);
            dgUsers.ItemsSource = view;
            view.Filter = CostumFilter;
        }

        private void rbEmail_Checked(object sender, RoutedEventArgs e)
        {
            List<User> sortirana = Data.Users.OrderBy(u => u.Email).ToList();
            view = CollectionViewSource.GetDefaultView(sortirana);
            dgUsers.ItemsSource = view;
            view.Filter = CostumFilter;
        }
    }
}
