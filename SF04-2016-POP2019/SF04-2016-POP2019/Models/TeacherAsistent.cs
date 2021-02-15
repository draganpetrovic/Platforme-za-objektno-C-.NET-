using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF04_2016_POP2019.Models
{
    public class TeacherAsistent : User
    {
        private int _profesrorID;

        public int ProfesorID
        {
            get { return _profesrorID; }
            set { _profesrorID = value; }
        }

        public TeacherAsistent(string name, string username, string password, string email,bool active) : base(name, username, password, email, active)
        {
            
            TypeOfUser = TypeOfUser.TA;
        }

        public override User Clone()
        {
            TeacherAsistent teacherAsistent = new TeacherAsistent(Name, Username,Password, Email, Active);
            return teacherAsistent;
        }
    }
}
