using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminApplication.Model
{
    public class AccountTableItem
    {
        private int id;
        private string userType;
        private string email;
        private string pid;

        private string active;
        private string banned;

        public int Id { get => id; set => id = value; }
        public string UserType { get => userType; set => userType = value; }
        public string Email { get => email; set => email = value; }
        public string Active { get => active; set => active = value; }
        public string Banned { get => banned; set => banned = value; }
        public string Pid { get => pid; set => pid = value; }

        public AccountTableItem(Account agentAccount)
        {
            id = agentAccount.Id;
            userType = agentAccount.UserType;
            email = agentAccount.Email;
            pid = agentAccount.Pid;

           if(agentAccount.Active.Equals("true"))
            {
                active = "Yes";
            }else
            {
                active = "No";
            }
            if (agentAccount.Banned.Equals("true"))
            {
                banned = "Yes";
            }
            else
            {
                banned = "No";
            }
        }
    }
}
