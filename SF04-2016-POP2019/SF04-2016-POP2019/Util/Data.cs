using SF04_2016_POP2019.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF04_2016_POP2019.Util
{
    public class Data
    {
        public static List<User> Users { get; set; }
        public static List<Faculty> Faculties { get; set; }
        public static List<Classroom> Classrooms { get; set; }
        public static List<Termin> Termini { get; set; }
        public static List<Classroom> UcioniceFakulteta { get; set; }
        public static List<Termin> TerminiAdmina { get; set; }
        public static List<User> Zaposleni { get; set; }
        public static List<Termin> TerminiProfA { get; set; }
        public static List<TeacherAsistent> TA { get; set; }


        public static string CONNECTION_STRING { get; internal set; }

        //POVEZIVANJE SA BAZOM
        public static string CONNECCTION_STRING = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";





        public static Termin GetTerminByFacultyID(Faculty f)
        {
            TerminiAdmina = new List<Termin>();
            Classroom ucionica = GetClassroomByFacultyID(f);
            foreach (Termin termin in Termini)
            {
                foreach (Classroom classroom in UcioniceFakulteta)
                {
                    if (termin.ClassroomId.Equals(classroom.ClassroomID))
                    {
                        TerminiAdmina.Add(termin);
                    }
                }
            }
            return null;
        }

        public static Classroom GetClassroomByFacultyID(Faculty fakultet)
        {
            UcioniceFakulteta = new List<Classroom>();
            foreach (Classroom classroom in Classrooms)
            {
                if (fakultet.FacultyID == classroom.Faculty_Id)
                {
                    UcioniceFakulteta.Add(classroom);
                }
            }
            return null;
        }


        //pronalazenje termina po korisnikovom id i ucitavanje u listu

        public static Termin TerminiPTA(User pta)
        {
            
            TerminiProfA = new List<Termin>();
            string usernamePTA = pta.Username;

            using (SqlConnection conn = new SqlConnection(CONNECCTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.Parameters.AddWithValue("@usernamePTA", usernamePTA);
                command.CommandText = @"select * from termin as t where User_Id = (select id from users where username = @usernamePTA)";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command;
                da.Fill(ds, "Termini");
                foreach (DataRow row in ds.Tables["Termini"].Rows)
                {
                    int id = (int)row["Id"];
                    string vreme1 = (string)row["Od"];
                    string vreme2 = (string)row["Do"];
                    string dayOfWeek = (string)row["Dan"];
                    Enum.TryParse(dayOfWeek, out Models.DayOfWeek dan);
                    string tipNastave = (string)row["TipNastave"];
                    Enum.TryParse(tipNastave, out TipNastave tip);
                    bool active = (bool)row["Active"];
                    int classroomId = (int)row["Classroom_Id"];
                    int userId = (int)row["User_Id"];

                    Termin termin = new Termin(id, vreme1, vreme2, dan, tip, active, classroomId, userId);
                    if (termin.Active == true)
                    {
                        TerminiProfA.Add(termin);
                    }
                }
            }
            return null;
        }


        //UCITAVANJE TERMINA

        public static void ReadTermini()
        {
            Termini = new List<Termin>();

            using (SqlConnection conn = new SqlConnection(CONNECCTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"select * from termin";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command;
                da.Fill(ds, "Termini");
                foreach (DataRow row in ds.Tables["Termini"].Rows)
                {
                    int id = (int)row["Id"];
                    string vreme1 = (string)row["Od"];
                    string vreme2 = (string)row["Do"];
                    string dayOfWeek = (string)row["Dan"];
                    Enum.TryParse(dayOfWeek, out Models.DayOfWeek dan);
                    string tipNastave = (string)row["TipNastave"];
                    Enum.TryParse(tipNastave, out TipNastave tip); // string koji ucitamo iz baze u enum
                    bool active = (bool)row["Active"];
                    int classroomId = (int)row["Classroom_Id"];
                    int userId = (int)row["User_Id"];

                    Termin termin = new Termin(id, vreme1, vreme2, dan, tip, active, classroomId, userId);
                    if (termin.Active == true)
                    {
                        Termini.Add(termin);
                    }
                }
            }

        }



        public static Termin GetTerminById(int id)
        {
            foreach (Termin termin in Termini)
            {
                if (termin.TerminID.Equals(id))
                {
                    return termin;
                }
            }
            return null;
        }





        //UCITAVANJE FAKULTETA U LISTU IZ BAZE
        public static void ReadFaculties()
        {
            Faculties = new List<Faculty>();

            using (SqlConnection conn = new SqlConnection(CONNECCTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"select * from faculty";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command;
                da.Fill(ds, "Faculties");
                foreach (DataRow row in ds.Tables["Faculties"].Rows)
                {
                    int id = (int)row["Id"];
                    string nameF = (string)row["Name"];
                    string address = (string)row["Address"];
                    bool active = (bool)row["Active"];


                    Faculty fakultet = new Faculty(id, nameF, address, active);
                    if (fakultet.Active == true)
                    {
                        Faculties.Add(fakultet);
                    }
                }
            }
        }

        //UCITAVANJE ASISTENATA PROFESORA




        // UCITAVANJE ZAPOSLENIH NA IZABRANOM FAKULTETU

        public static User UcitajZaposlen(Faculty f)
        {
            Zaposleni = new List<User>();
            int fid = f.FacultyID;
            using (SqlConnection conn = new SqlConnection(CONNECCTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.Parameters.AddWithValue("@fid", fid);
                command.CommandText = @"select Id, Username, Name, Password, TypeOfUser, Active, Email
                                        from users as u, zaposlen as z where u.Id = z.User_Id and z.Faculty_Id = @fid";
                DataSet zds = new DataSet();
                SqlDataAdapter zda = new SqlDataAdapter();
                zda.SelectCommand = command;
                zda.Fill(zds, "Zaposlen");
                foreach (DataRow row in zds.Tables["Zaposlen"].Rows)
                {
                    string username = (string)row["Username"];
                    string name = (string)row["Name"];
                    string password = (string)row["Password"];
                    bool active = (bool)row["Active"];
                    string email = (string)row["Email"];
                    if (row["TypeOfUser"].Equals(TypeOfUser.PROFESOR.ToString()))
                    {
                        Profesor prof = new Profesor(name, username, password, email, active);
                        Zaposleni.Add(prof);
                    }
                    else if (row["TypeOfUser"].Equals(TypeOfUser.TA.ToString()))
                    {
                        TeacherAsistent ta = new TeacherAsistent(name, username, password, email, active);
                        Zaposleni.Add(ta);
                    }

                }
            } return null;

        }



        // CITANJE KORISNIKA IZ BAZE

        public static void ReadUsers()
        {
            Users = new List<User>();

            using (SqlConnection conn = new SqlConnection(CONNECCTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"select * from users";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;
                da.Fill(ds, "Users"); //sve ucitano iz upita se upisuje u tabelu users, da je objekat sa podacima iz tabele
                foreach (DataRow row in ds.Tables["Users"].Rows)// pristupamo tabeli, pa iteriramo kroz redove
                {
                    string username = (string)row["Username"];
                    string name = (string)row["Name"];
                    string password = (string)row["Password"];
                    bool active = (bool)row["Active"];
                    string email = (string)row["Email"];

                    if (row["TypeOfUser"].Equals(TypeOfUser.ADMIN.ToString()))
                    {
                        Administrator admin = new Administrator(name, username, password, email, active);
                        Users.Add(admin); //proverimo kog je tipa objekat i onda ga dodamo u listu kolekciju bla bla
                    }
                    else if (row["TypeOfUser"].Equals(TypeOfUser.PROFESOR.ToString()))
                    {
                        Profesor prof = new Profesor(name, username, password, email, active);
                        Users.Add(prof);
                    }
                    else if (row["TypeOfUser"].Equals(TypeOfUser.TA.ToString()))
                    {
                        TeacherAsistent ta = new TeacherAsistent(name, username, password, email, active);
                        Users.Add(ta);
                    }
                }
            }
        }


        public static int GetUserIDbyUsername(string userName)
        {
            string u = userName;
            using (SqlConnection conn = new SqlConnection(CONNECCTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.Parameters.AddWithValue("@u", u);
                command.CommandText = @"select id from users where username = @u";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;
                da.Fill(ds, "Users"); //sve ucitano iz upita se upisuje u tabelu users, da je objekat sa podacima iz tabele
                foreach (DataRow row in ds.Tables["Users"].Rows)
                {
                    int id = (int)row["Id"];
                    return id;
                }
            }
            return 0;
        }

        public static Faculty GetFacultyById(int id)
        {
            foreach (Faculty fakultet in Faculties)
            {
                if (fakultet.FacultyID.Equals(id))
                {
                    return fakultet;
                }
            }
            return null;
        }

        public static Faculty GetFacultyByName(object obj)
        {
            foreach (Faculty fakultet in Faculties)
            {
                if (fakultet.NameF.Equals(obj))
                {
                    return fakultet;
                }
            }
            return null;
        }

        //UCITAVANJE UCIONICA


        public static void ReadClassroom()
        {
            Classrooms = new List<Classroom>();

            using (SqlConnection conn = new SqlConnection(CONNECCTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"select * from classroom";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command;
                da.Fill(ds, "Classrooms");
                foreach (DataRow row in ds.Tables["Classrooms"].Rows)
                {
                    int id = (int)row["Id"];
                    string nameC = (string)row["Name"];
                    int seatsC = (int)row["Seats"];
                    string typeOfClassroom = (string)row["TypeOfClassroom"];
                    Enum.TryParse(typeOfClassroom, out TypeOfClassroom tip); // string koji ucitamo iz baze u enum
                    bool active = (bool)row["Active"];
                    int faculty_Id = (int)row["Faculty_Id"];


                    Classroom ucionica = new Classroom(id, nameC, seatsC, active, tip, faculty_Id);

                    if (ucionica.Active == true)
                    {
                        Classrooms.Add(ucionica);
                    }
                }
            }
        }

        public static Classroom GetClassroomById(int id)
        {
            foreach (Classroom ucionica in Classrooms)
            {
                if (ucionica.ClassroomID.Equals(id))
                {
                    return ucionica;
                }
            }
            return null;
        }




        //PROMENI OVO DA PRETRAZUJES LISTU USERS!!!!!!!!!!!

        //public static void SearchByUsername(string username, string ime, string e)
        //{
        //    Users.Clear(); //ovo je pretraga pa brisemo listu jer nam treba ponovo

        //    using (SqlConnection conn = new SqlConnection(CONNECCTION_STRING))//parvimo konekciju
        //    {
        //        conn.Open(); // otvaramo konekciju
        //        SqlCommand command = conn.CreateCommand();
        //        command.Parameters.Add(new SqlParameter("@username", username));
        //        command.Parameters.Add(new SqlParameter("@name", ime));
        //        command.Parameters.Add(new SqlParameter("@email", e));
        //        command.CommandText = @"select * from users where (username, name, email) like ('%" + username + "%', '%" + ime + "%', '%" + e + "%'  ";

        //        DataSet ds = new DataSet();
        //        SqlDataAdapter da = new SqlDataAdapter();
        //        da.SelectCommand = command;
        //        da.Fill(ds, "Users");

        //        foreach (DataRow row in ds.Tables["Users"].Rows)
        //        {
        //            string user_name = (string)row["Username"];
        //            string name = (string)row["Name"];
        //            string password = (string)row["Password"];
        //            bool active = (bool)row["Active"];
        //            string email = (string)row["Email"];

        //            if (row["TypeOfUser"].Equals(TypeOfUser.ADMIN.ToString()))
        //            {
        //                Administrator admin = new Administrator(name, user_name, password, email, active);
        //                Users.Add(admin); //proverimo kog je tipa objekat i onda ga dodamo u listu kolekciju bla bla
        //            }
        //            else if (row["TypeOfUser"].Equals(TypeOfUser.PROFESOR.ToString()))
        //            {
        //                Profesor prof = new Profesor(name, username, password, email, active);
        //                Users.Add(prof);
        //            }
        //            else if (row["TypeOfUser"].Equals(TypeOfUser.TA.ToString()))
        //            {
        //                TeacherAsistent ta = new TeacherAsistent(name, username, password, email, active);
        //                Users.Add(ta);
        //            }
        //        }
        //    }
        //}

        public static User GetUserByUsername(string username)
        {
            foreach (User user in Users)
            {
                if (user.Username.Equals(username))
                {
                    return user;
                }
            }
            return null;
        }

        internal static User GetUserByUsername(object text)
        {
            throw new NotImplementedException();
        }

        // ucitavanje profesorovog id za asistente

        public static void ReadTA_ProfesorID()
        {

            using (SqlConnection conn = new SqlConnection(CONNECCTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"select u.id, u.username, ta.professor_id  from teacherassistants as ta, users as u where ta.id = u.id";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = command;
                da.Fill(ds, "TeacherAssistants"); //sve ucitano iz upita se upisuje u tabelu users, da je objekat sa podacima iz tabele
                foreach (DataRow row in ds.Tables["TeacherAssistants"].Rows)// pristupamo tabeli, pa iteriramo kroz redove
                {
                    int id = (int)row["Id"];
                    string username = (string)row["Username"];
                    int profesorId = (int)row["Professor_Id"];
                    foreach(var u in Users)
                    {
                        if(u.Username.Equals(username))
                        {
                            TeacherAsistent ta = u as TeacherAsistent;
                            ta.ProfesorID = profesorId;
                        }

                    }
                    
                }

            }

        }

        public static void ListOfTA()
        {

            using (SqlConnection conn = new SqlConnection(CONNECCTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = @"select u.username, u.id from users as u, teacherassistants as ta where ta.Professor_Id = u.id";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command;
                da.Fill(ds, "TeacherAssistants");
                foreach (DataRow row in ds.Tables["TeacherAssistants"].Rows)
                {
                    int profesorId = (int)row["Id"];
                    string username = (string)row["Username"];

                    foreach (var u in Users)
                    {
                        if (u.GetType() == typeof(TeacherAsistent))
                        {
                            TeacherAsistent ta = u as TeacherAsistent;
                            if (ta.ProfesorID.Equals(profesorId))
                            {
                                foreach (var prof in Users)
                                {
                                    if (prof.GetType() == typeof(Profesor))
                                    {
                                        Profesor p = prof as Profesor;
                                        if (p.Username.Equals(username))
                                        {
                                            p.Assistants.Add(ta);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }

        }

        public static void ReadTA()
        {
            TA = new List<TeacherAsistent>();
            foreach(User u in Users)
            {
                if(u.GetType() == typeof(TeacherAsistent))
                {
                    TeacherAsistent ta = u as TeacherAsistent;
                    TA.Add(ta);
                }
            }
        }

        public static void SacuvajTA(string pU, string taU)
        {
            int pid = GetUserIDbyUsername(pU);
            int taid = GetUserIDbyUsername(taU);

            using (SqlConnection conn = new SqlConnection(Data.CONNECCTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.Parameters.AddWithValue("@pid", pid);
                command.Parameters.AddWithValue("@taid", taid);
                command.CommandText = @"insert into teacherassistants(Id, Professor_Id) values (@taid, @pid)";
                command.ExecuteScalar();

            }
        }

        public static void ObrisiTA(int id)
        {
            using (SqlConnection conn = new SqlConnection(Data.CONNECCTION_STRING))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.Parameters.AddWithValue("@id", id);
                command.CommandText = @"delete from teacherassistants where Id = @id";
                command.ExecuteScalar();

            }
        }





    }

}


