using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminApplication.Model
{
    public class NewAccountDTO
    {
        private string email;
        private string name;
        private string lastname;
        private string city;
        private string phone;
        private string pid;

        public NewAccountDTO()
        {
        }

        public NewAccountDTO(string email, string name, string lastname, string city, string phone, string pid)
        {
            this.email = email;
            this.name = name;
            this.lastname = lastname;
            this.city = city;
            this.phone = phone;
            this.pid = pid;
        }

        public string Email { get => email; set => email = value; }
        public string Name { get => name; set => name = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public string City { get => city; set => city = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Pid { get => pid; set => pid = value; }

        public string ToJSON()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{");

            stringBuilder.Append("\"email\" : \"" + email + "\",");
            stringBuilder.Append("\"name\" : \"" + name + "\",");
            stringBuilder.Append("\"lastname\" : \"" + lastname + "\",");
            stringBuilder.Append("\"city\" : \"" + city + "\",");
            stringBuilder.Append("\"phone\" : \"" + phone + "\",");
            stringBuilder.Append("\"pid\" : \"" + pid + "\"");




            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

    }
}
