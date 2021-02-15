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
    public abstract class User : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value; OnPropertyChanged("Name");
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged("Password"); }
        }


        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged("Username"); }
        }

        private Boolean _active;
        public bool Active
        {
            get { return _active; }
            set { _active = value; OnPropertyChanged("Active"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private TypeOfUser _typeOfUser;
        public TypeOfUser TypeOfUser
        {
            get { return _typeOfUser; }
            set { _typeOfUser = value; OnPropertyChanged("TypeOfUser"); }
        }

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
               {
                    //case "Name":
                        //if (Name != Null && Name.Equals(string.Empty))
                            //return "Ovo polje je obavezno!";
                        //break;
                        //samo za ono sto postoji sme da bude tj sta ta klasa ima
                }
                return null;

            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged("Email"); }
        }

        public User(string name, string username, string password, string email, bool active)
        {
            this._name = name;
            this._username = username;
            this._password = password;
            this._active = active;
            this.email = email;
        }

        public virtual User Clone()
        {
            return null;
        }

        public User()
        {
            Username = "";
            Active = true;
        }

        public virtual void SaveUser()
        {
            
            using (SqlConnection conn = new SqlConnection(Data.CONNECCTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"insert into users(name, username, password, typeofuser, email, active) output inserted.id values (@Name, @Username, @password, @TypeOfUser, @Email, @Active)";
                command.Parameters.Add(new SqlParameter("Name", this.Name));
                command.Parameters.Add(new SqlParameter("Username", this.Username));
                command.Parameters.Add(new SqlParameter("Password", this.Password));
                command.Parameters.Add(new SqlParameter("TypeOfUser", this.TypeOfUser.ToString()));
                command.Parameters.Add(new SqlParameter("Email", this.Email));
                command.Parameters.Add(new SqlParameter("Active", this.Active));

                command.ExecuteScalar(); 
            }
            
        }

        public virtual void UpdateUser()
        {
            using (SqlConnection conn = new SqlConnection(Data.CONNECCTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update users set name=@name, email=@email
                where username=@username";

                command.Parameters.Add(new SqlParameter("name", this.Name));
                command.Parameters.Add(new SqlParameter("email", this.Email));
                command.Parameters.Add(new SqlParameter("username", this.Username));

                command.ExecuteNonQuery();
            }
        }

        public virtual void DeleteUser()
        {
            using (SqlConnection conn = new SqlConnection(Data.CONNECCTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update users set active=@active
                where username=@username";

                command.Parameters.Add(new SqlParameter("active", this.Active));
                command.Parameters.Add(new SqlParameter("username", this.Username));

                command.ExecuteNonQuery();
            }
        }

    }
}
