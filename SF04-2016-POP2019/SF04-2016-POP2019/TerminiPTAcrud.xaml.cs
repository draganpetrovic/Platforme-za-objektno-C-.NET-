using SF04_2016_POP2019.Models;
using SF04_2016_POP2019.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for TerminiPTAcrud.xaml
    /// </summary>
    public partial class TerminiPTAcrud : Window
    {
        private Termin selectedTermin;
        private User ulogovan;

        public TerminiPTAcrud(User user)
        {
            InitializeComponent();
            ulogovan = user;
            UcioniceFakultetaZaposlenog();
            cmbDan.ItemsSource = new List<Models.DayOfWeek>() { Models.DayOfWeek.PONEDELJAK, Models.DayOfWeek.UTORAK,
                Models.DayOfWeek.SREDA,  Models.DayOfWeek.CETVRTAK,  Models.DayOfWeek.PETAK,  Models.DayOfWeek.SUBOTA,  Models.DayOfWeek.NEDELJA};
            cmbTip.ItemsSource = new List<TipNastave>() { TipNastave.LABORATORIJA, TipNastave.VEZBE, TipNastave.PREDAVANJA };
            
            this.DataContext = selectedTermin;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Termin selectedTermin = new Termin();

            int cid = int.Parse(string.Format("{0}", lbClassrooms.SelectedItem));
            selectedTermin.ClassroomId = cid;
            int uid = int.Parse(string.Format("{0}", Data.GetUserIDbyUsername(ulogovan.Username)));
            selectedTermin.UserId = uid;
            selectedTermin.Vreme1 = txtVreme1.Text;
            selectedTermin.Vreme2 = txtVreme2.Text;

            if (Termin.ProveraTermina(selectedTermin))
            {
                Data.Termini.Add(selectedTermin);
                selectedTermin.SaveTermin();
            }
            else
            {
                MessageBox.Show("Zauzet termin!", "Warning", MessageBoxButton.OK);
            }

            this.DialogResult = true;
            this.Close();
            

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }




        private void UcioniceFakultetaZaposlenog()
        {
            string u = ulogovan.Username;
            using (SqlConnection conn = new SqlConnection(Data.CONNECCTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.Parameters.AddWithValue("@u", u);
                command.CommandText = @"select faculty_id from zaposlen where User_Id = (select id from users where username = @u)";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command;
                da.Fill(ds, "Tabela");
                lbClassrooms.Items.Clear();
                foreach (DataRow row in ds.Tables["Tabela"].Rows)
                {
                    int fid = (int)(row["Faculty_Id"]);
                    foreach (Classroom c in Data.Classrooms)
                    {
                        if (fid == c.Faculty_Id)
                        {
                            lbClassrooms.Items.Add(c.ClassroomID);
                        }
                    }
                    
                }
            }
        }
    }
}
