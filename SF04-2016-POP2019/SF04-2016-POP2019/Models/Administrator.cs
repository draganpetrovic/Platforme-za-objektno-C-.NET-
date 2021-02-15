using SF04_2016_POP2019.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF04_2016_POP2019.Models
{
    public class Administrator : User
    {
        public Administrator(string name, string username, string password, string email, bool active) : base(name, username, password, email, active)
        {
            TypeOfUser = TypeOfUser.ADMIN;
        }

        public override User Clone()
        {
            Administrator administrator = new Administrator(Name, Username, Password, Email, Active);
            return administrator;
        }

        public Administrator() : base()
        {

        }

        public override void SaveUser()
        {
            base.SaveUser();

            using (SqlConnection conn = new SqlConnection(Data.CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"Insert into Administrators(Id) values (@Id)";

                //command.Parameters.Add(new SqlParameter("Id", id));

                command.ExecuteNonQuery();

            }

                
        }
    }
}
