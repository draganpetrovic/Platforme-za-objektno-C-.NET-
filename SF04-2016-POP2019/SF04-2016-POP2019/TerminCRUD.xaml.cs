using SF04_2016_POP2019.Models;
using SF04_2016_POP2019.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SF04_2016_POP2019
{
    /// <summary>
    /// Interaction logic for TerminCRUD.xaml
    /// </summary>
    public partial class TerminCRUD : Window
    {
        enum Status { ADD, EDIT }
        private Status _status;
        private Termin selectedTermin;
        ICollectionView view;

        public TerminCRUD(Termin termin)
        {
            InitializeComponent();
            InitalizeView();
            cmbDan.ItemsSource = new List<Models.DayOfWeek>() { Models.DayOfWeek.PONEDELJAK, Models.DayOfWeek.UTORAK,
                Models.DayOfWeek.SREDA,  Models.DayOfWeek.CETVRTAK,  Models.DayOfWeek.PETAK,  Models.DayOfWeek.SUBOTA,  Models.DayOfWeek.NEDELJA};
            cmbTip.ItemsSource = new List<TipNastave>() { TipNastave.LABORATORIJA, TipNastave.VEZBE, TipNastave.PREDAVANJA };
            if (termin.TerminID == 0)
            {
                this._status = Status.ADD;
            }
            else
            {
                this._status = Status.EDIT;
            }
            selectedTermin = termin;
            this.DataContext = termin;
        }

        private void InitalizeView()
        {
            view = CollectionViewSource.GetDefaultView(Data.Faculties);
            dgFaculty.ItemsSource = view;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            int uid = int.Parse(string.Format("{0}", lbPTA.SelectedItem));
            selectedTermin.UserId = uid;
            int cid = int.Parse(string.Format("{0}", lbClassrooms.SelectedItem));
            selectedTermin.ClassroomId = cid;
            if (Termin.ProveraTermina(selectedTermin))
            {
                if (_status.Equals(Status.ADD))
                {
                    Data.Termini.Add(selectedTermin);
                    selectedTermin.SaveTermin(); //cuva u bazi

                }
                //izmena podataka
                if (_status.Equals(Status.EDIT))
                {
                    selectedTermin.UpdateTermin();

                }
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

        private void dgFaculty_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Equals("Error") || e.PropertyName.Equals("Active") || e.PropertyName.Equals("FacultyID"))
            {
                e.Column.Visibility = Visibility.Collapsed;
            }
            
        }

        //Na dupli klik misem, kada izaberemo fakultet, generisu nam se liste ucionica tog fakulteta i profesora i asistenata na fakultetu

        private void DgFaculty_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            {
                 
                Faculty fakultet = dgFaculty.SelectedItem as Faculty;
                int fID = fakultet.FacultyID;
                using (SqlConnection conn = new SqlConnection(Data.CONNECCTION_STRING))
                {
                    conn.Open();
                    SqlCommand command = conn.CreateCommand();
                    command.Parameters.AddWithValue("@fID", fID);
                    command.CommandText = @"select DISTINCT User_Id from zaposlen where Faculty_Id =  @fID";
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(ds, "Tabela");
                    lbPTA.Items.Clear();
                    lbClassrooms.Items.Clear();
                    foreach(Classroom c in Data.Classrooms)
                    {
                        if (fID == c.Faculty_Id)
                        {
                            lbClassrooms.Items.Add(c.ClassroomID);
                        }
                    }
                    foreach (DataRow row in ds.Tables["Tabela"].Rows)
                    {
                        
                        //lbClassrooms.Items.Add(row["ID"].ToString());
                        lbPTA.Items.Add(row["User_Id"].ToString());
                    }
                }
            }
        }
    }
}
