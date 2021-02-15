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
    /// Interaction logic for FacultyCRUD.xaml
    /// </summary>
    public partial class FacultyCRUD : Window
    {
        enum Status { ADD, EDIT }
        private Status _status;
        private Faculty selectedFaculty;

        public FacultyCRUD(Faculty fakultet)
        {
            InitializeComponent();
            if (fakultet.FacultyID == 0)
            {
                this._status = Status.ADD;
            }
            else
            {
                this._status = Status.EDIT;
            }
            selectedFaculty = fakultet;
            this.DataContext = fakultet;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

            if (_status.Equals(Status.ADD))
            {
                Data.Faculties.Add(selectedFaculty);
                selectedFaculty.SaveFaculty(); //cuva u bazi

            }
            //izmena podataka
            if (_status.Equals(Status.EDIT))
            {
                selectedFaculty.UpdateFaculty();
            }
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
