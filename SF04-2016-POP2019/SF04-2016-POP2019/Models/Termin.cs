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
    public class Termin : INotifyPropertyChanged
    {
        private int _terminID;

        public int TerminID
        {
            get { return _terminID; }
            set { _terminID = value; OnPropertyChanged("ID"); }
        }

        private string _vreme1;

        public string Vreme1
        {
            get { return _vreme1; }
            set { _vreme1 = value; OnPropertyChanged("Vreme1"); }
        }

        private string _vreme2;

        public string Vreme2
        {
            get { return _vreme2; }
            set { _vreme2 = value; OnPropertyChanged("Vreme2"); }
        }

        private DayOfWeek _dayOfWeek;

        public DayOfWeek DayOfWeek
        {
            get { return _dayOfWeek; }
            set { _dayOfWeek = value; OnPropertyChanged("DayOfWeek"); }
        }

        private TipNastave _tipNastave;

        public TipNastave TipNastave
        {
            get { return _tipNastave; }
            set { _tipNastave = value; OnPropertyChanged("TipNastave"); }
        }

        private Boolean _active;
        public bool Active
        {
            get { return _active; }
            set { _active = value; OnPropertyChanged("Active"); }
        }

        private int _classroomId;

        public int ClassroomId
        {
            get { return _classroomId; }
            set { _classroomId = value; OnPropertyChanged("ClassroomId"); }
        }

     


        private int _userId;

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; OnPropertyChanged("UserId"); }
        }


        //private string _usernameKor;

        //public string UsernameKor
        // {
        //    get { return _usernameKor; }
        //     set { _usernameKor = value; OnPropertyChanged("UsernameKorisnika"); }
        // }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Termin()
        {
            Active = true;
        }

        public virtual Termin Clone()
        {
            Termin termin = new Termin( Vreme1, Vreme2, DayOfWeek, TipNastave , ClassroomId, UserId);
            return termin;
        }

        

        public Termin(int id,string vreme1,string vreme2,DayOfWeek dayOfWeek,TipNastave tipNastave,bool active,int classroomId,int userId )
        {
            this._terminID = id;
            this._vreme1 = vreme1;
            this._vreme2 = vreme2;
            this._dayOfWeek = dayOfWeek;
            this._tipNastave = tipNastave;
            this._active = active;
            this._classroomId = classroomId;
            this._userId = userId;
        }

        public Termin( string vreme1, string vreme2, DayOfWeek dayOfWeek, TipNastave tipNastave, int classroomId, int userId)
        {
 
            this._vreme1 = vreme1;
            this._vreme2 = vreme2;
            this._dayOfWeek = dayOfWeek;
            this._tipNastave = tipNastave;
            this._classroomId = classroomId;
            this._userId = userId;
        }

        public virtual void DeleteTermin()
        {
            using (SqlConnection conn = new SqlConnection(Data.CONNECCTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"update termin set active=@active
                where id=@id";

                command.Parameters.Add(new SqlParameter("active", this.Active));
                command.Parameters.Add(new SqlParameter("id", this.TerminID));

                command.ExecuteNonQuery();
            }
        }

        public virtual void SaveTermin()
        {

            using (SqlConnection conn = new SqlConnection(Data.CONNECCTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();

                command.CommandText = @"insert into termin(od, do, dan, tipNastave, active, classroom_Id, user_Id )
                    values (@od, @do, @dan, @tipNastave, @active, @classroom_Id, @user_Id)";

                command.Parameters.Add(new SqlParameter("od", this.Vreme1));
                command.Parameters.Add(new SqlParameter("do", this.Vreme2));
                command.Parameters.Add(new SqlParameter("dan", this.DayOfWeek));
                command.Parameters.Add(new SqlParameter("tipNastave", this.TipNastave));
                command.Parameters.Add(new SqlParameter("active", this.Active));
                command.Parameters.Add(new SqlParameter("classroom_Id", this.ClassroomId));
                command.Parameters.Add(new SqlParameter("user_Id", this.UserId));

            }
        }


            public virtual void UpdateTermin()
            {
                using (SqlConnection conn = new SqlConnection(Data.CONNECCTION_STRING))
                {
                    conn.Open();

                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = @"update termin set od=@od, do=@do, dan=@dan, tipNastave=@tipNastave, classroom_Id=@classroom_Id,
                    user_Id=@user_Id,  where id=@id";

                    command.Parameters.Add(new SqlParameter("id", this.TerminID));
                    command.Parameters.Add(new SqlParameter("od", this.Vreme1));
                    command.Parameters.Add(new SqlParameter("do", this.Vreme2));
                    command.Parameters.Add(new SqlParameter("dan", this.DayOfWeek));
                    command.Parameters.Add(new SqlParameter("tipNastave", this.TipNastave));
                    command.Parameters.Add(new SqlParameter("classroom_Id", this.ClassroomId));
                    command.Parameters.Add(new SqlParameter("user_Id", this.UserId));

                    command.ExecuteNonQuery();
                }
            }

        public static bool ProveraTermina(Termin t)
        {
            var t1 = TimeSpan.Parse(t.Vreme1);
            var t2 = TimeSpan.Parse(t.Vreme2);

            foreach(Termin ter in Data.Termini)
            {
                var t11 = TimeSpan.Parse(ter.Vreme1);
                var t22 = TimeSpan.Parse(ter.Vreme2);
                if (t.ClassroomId.Equals(ter.ClassroomId) && t.DayOfWeek.Equals(ter.DayOfWeek))
                {
                    if(t1 >= t11 || t1 <= t22)
                    {
                        return false;
                    }
                    else if(t2 >= t11 || t2 <= t22)
                    {
                        return false;
                    }
                    else if(t1 < t11 && t2 > t22)
                    {
                        return false;
                    }

                }
            }
            return true;

        }

    }

}






