using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF04_2016_POP2019.Models
{
    public class Profesor : User
    {
        private ObservableCollection<User> assistants;
        

        public ObservableCollection<User> Assistants
        {
            get { return assistants; }
            set { assistants = value; }
        }

        public Profesor(string name, string username, string password, string email, bool active) : base(name, username,password, email, active)
        {
            this.assistants = new ObservableCollection<User>();
            TypeOfUser = TypeOfUser.PROFESOR;
        }

        public override User Clone()
        {
            Profesor profesor = new Profesor(Name, Username, Password, Email, Active);
            profesor.Assistants = Assistants;
            return profesor;
        }

        
    }
}
