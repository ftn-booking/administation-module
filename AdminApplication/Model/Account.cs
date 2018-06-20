using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminApplication.Model
{
    public class Account
    {
        private int id;
        private string userType;
        private string email;
        // private int accountState;
        private string active;
        private string banned;
        private string pid;

        private Boolean isActive;
        private Boolean isBanned;

       /* public Account(int id, string userType, string email, int accountState)
        {
            this.id = id;
            this.userType = userType;
            this.email = email;
            this.accountState = accountState;
            if(accountState==1)
            {
                isActive = false;
                isBanned = false;

            }else if (accountState==2)
            {
                isActive = true;
                isBanned = true;
            }else
            {
                isActive = true;
                isBanned = false;
            }
        }*/


        public Account()
        {
        }

        public Account(int id, string userType, string email, string active, string banned, bool isActive, bool isBanned, string pid)
        {
            this.id = id;
            this.userType = userType;
            this.email = email;
            this.active = active;
            this.banned = banned;
            this.isActive = isActive;
            this.isBanned = isBanned;
            this.pid = pid;
        }

        public string UserType { get => userType; set => userType = value; }
        //public int AccountState { get => accountState; set => accountState = value; }
        
        public int Id { get => id; set => id = value; }
        public string Email { get => email; set => email = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public bool IsBanned { get => isBanned; set => isBanned = value; }
        public string Active { get => active; set => active = value; }
        public string Banned { get => banned; set => banned = value; }
        public string Pid { get => pid; set => pid = value; }

        public string ToJSONUpdate()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{");
            if (id != 0)
            {
                stringBuilder.Append("\"id\" : \"" + id + "\",");
            }
            

            stringBuilder.Append("\"active\" : \"" + active + "\",");
            stringBuilder.Append("\"banned\" : \"" + banned + "\"");




            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
