using SF04_2016_POP2019.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF04_2016_POP2019.Models
{
    public class Classroom : INotifyPropertyChanged
    {
        private int _classroomID;

        public int ClassroomID
        {
            get { return _classroomID; }
            set { _classroomID = value; OnPropertyChanged("ID"); }
        }

        private string _nameC;

        public string NameC
        {
            get { return _nameC; }
            set { string _nameC = value; OnPropertyChanged("Name"); }
        }

        private int _seatsC;

        public int SeatsC
        {
            get { return _seatsC; }
            set { _seatsC = value; OnPropertyChanged("Seats"); }
        }

        private TypeOfClassroom _typeOfClassroom;
        public TypeOfClassroom TypeOfClassroom
        {
            get { return _typeOfClassroom; }
            set { _typeOfClassroom = value; OnPropertyChanged("TypeOfClassroom"); }

        }

        private int _faculty_Id;

        public int Faculty_Id
        {
            get { return _faculty_Id; }
            set { _faculty_Id = value; OnPropertyChanged("FacultyID"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual Classroom Clone()
        {
            Classroom ucionica = new Classroom(ClassroomID ,NameC, SeatsC, Active, TypeOfClassroom, Faculty_Id);
            return ucionica;
        }

        private Boolean _active;
        public bool Active
        {
            get { return _active; }
            set { _active = value; OnPropertyChanged("Active"); }
        }

        public Classroom(string nameC, int seats, TypeOfClassroom typeOfClassroom, int faculty_Id)
        {
            this._nameC = nameC;
            this._seatsC = seats;
            this._typeOfClassroom = typeOfClassroom;
            this._faculty_Id = faculty_Id;

        }
        public Classroom()
        {
            Active = true;
        }

        public Classroom(int id, string nameC, int seats, bool active)
        {
            this._classroomID = id;
            this._nameC = nameC;
            this._seatsC = seats;
            this._active = active;

        }

        public Classroom(int id, string nameC, int seats, bool active, TypeOfClassroom typeOfClassroom, int faculty_Id)
        {
            this._classroomID = id;
            this._nameC = nameC;
            this._seatsC = seats;
            this._active = active;
            this._typeOfClassroom = typeOfClassroom;
            this._faculty_Id = faculty_Id;
            
        }

        public Classroom( string nameC, int seats, bool active, TypeOfClassroom typeOfClassroom, int faculty_Id)
        {

            this._nameC = nameC;
            this._seatsC = seats;
            this._active = active;
            this._typeOfClassroom = typeOfClassroom;
            this._faculty_Id = faculty_Id;

        }

        public Classroom(string nameC, int seats)
        {
            this._nameC = nameC;
            this._seatsC = seats;

        }
        
        public virtual void SaveClassroom()
        {

            using (SqlConnection conn = new SqlConnection(Data.CONNECCTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"insert into classroom(Name, Seats, TypeOfClassroom,Active, Faculty_Id ) values (@NameC, @SeatsC, @TypeOfClassroom, @Active, @Faculty_Id)";
                command.Parameters.Add(new SqlParameter("NameC", this.NameC));
                command.Parameters.Add(new SqlParameter("SeatsC", this.SeatsC));
                command.Parameters.Add(new SqlParameter("TypeOfClassroom", this.TypeOfClassroom.ToString()));
                command.Parameters.Add(new SqlParameter("Active", this.Active));
                command.Parameters.Add(new SqlParameter("Faculty_Id", this.Faculty_Id));

                command.ExecuteNonQuery();
            }

        }

        public virtual void SacuvajRucno(string name)
        {
            string test = name;
        }

        public virtual void DeleteClassroom()
        {
            using (SqlConnection conn = new SqlConnection(Data.CONNECCTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update classroom set active=@active
                where id=@id";

                command.Parameters.Add(new SqlParameter("active", this.Active));
                command.Parameters.Add(new SqlParameter("id", this.ClassroomID));

                command.ExecuteNonQuery();
            }
        }

        public virtual void UpdateClassroom()
        {
            using (SqlConnection conn = new SqlConnection(Data.CONNECCTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update classroom set name=@name, seats=@seats
                where id=@id";

                command.Parameters.Add(new SqlParameter("name", this.NameC));
                command.Parameters.Add(new SqlParameter("seats", this.SeatsC));
                command.Parameters.Add(new SqlParameter("id", this.ClassroomID));


                command.ExecuteNonQuery();
            }
        }



    }



}
