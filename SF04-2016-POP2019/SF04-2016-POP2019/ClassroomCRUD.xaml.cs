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
    /// Interaction logic for ClassroomCRUD.xaml
    /// </summary>
    public partial class ClassroomCRUD : Window
    {
        enum Status { ADD, EDIT }
        private Status _status;
        private Classroom selectedClassroom;
        


        public ClassroomCRUD(Classroom classroom)
        {
            
            InitializeComponent();
            foreach(Faculty f in Data.Faculties)
            {
                fID.Items.Add(f.FacultyID);
            }

            cmbTypeOfClassroom.ItemsSource = new List<TypeOfClassroom>() { TypeOfClassroom.LABORATORY, TypeOfClassroom.AMPHITHEATER, TypeOfClassroom.IT };

            if (classroom.ClassroomID == 0)
            {
                this._status = Status.ADD;
            }
            else
            {
                this._status = Status.EDIT;
            }
            selectedClassroom = classroom;
            this.DataContext = classroom;

        }


        //NESTO NIJE U REDU... objekat nece da primi unos iz polja, cak ni kada propertiju dodelim txt polja
        
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if(_status.Equals(Status.ADD))
            {

                Data.Classrooms.Add(selectedClassroom);
                selectedClassroom.SaveClassroom();
            }

            if(_status.Equals(Status.EDIT))
            {
                selectedClassroom.UpdateClassroom();
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
