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
    public class Faculty : INotifyPropertyChanged
    {
        private int _facultyID;

        public int FacultyID
        {
            get { return _facultyID; }
            set { _facultyID = value; OnPropertyChanged("FacultyID"); }
        }

        private string _nameF;

        public string NameF
        {
            get { return _nameF; }
            set { _nameF = value; OnPropertyChanged("Name"); }
        }

        private string  _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged("Address"); }
        }


        private Boolean _active;
        public bool Active
        {
            get { return _active; }
            set { _active = value; OnPropertyChanged("Active"); }
        }



        public virtual Faculty Clone()
        {
            Faculty fakultet = new Faculty(NameF, Address);
            return fakultet;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Faculty(string nameF, string address)
        {
            this._nameF = nameF;
            this._address = address;
        }

        public Faculty(int id, string nameF, string address, bool active)
        {
            this._facultyID = id;
            this._nameF = nameF;
            this._address = address;
            this._active = active;
        }

        public Faculty()
        {
            Active = true;
        }

        public virtual void SaveFaculty()
        {

            using (SqlConnection conn = new SqlConnection(Data.CONNECCTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"insert into faculty(name, address, active) values (@NameF, @Address, @Active)";
                
                command.Parameters.Add(new SqlParameter("NameF", this.NameF));
                command.Parameters.Add(new SqlParameter("Address", this.Address));
                command.Parameters.Add(new SqlParameter("Active", this.Active));

                command.ExecuteNonQuery();

            }


        }


        public virtual void UpdateFaculty()
        {
            using (SqlConnection conn = new SqlConnection(Data.CONNECCTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update faculty set name=@name, address=@address
                where id=@id";

                command.Parameters.Add(new SqlParameter("Id", this.FacultyID));
                command.Parameters.Add(new SqlParameter("Name", this.NameF));
                command.Parameters.Add(new SqlParameter("Address", this.Address));

                command.ExecuteNonQuery();
            }
        }

        public virtual void DeleteFaculty()
        {
            using (SqlConnection conn = new SqlConnection(Data.CONNECCTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update faculty set active=@active
                where id=@id";

                command.Parameters.Add(new SqlParameter("active", this.Active));
                command.Parameters.Add(new SqlParameter("id", this.FacultyID));

                command.ExecuteNonQuery();
            }
        }

    }
}
