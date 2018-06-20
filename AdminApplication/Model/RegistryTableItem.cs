using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminApplication.Model
{
    public class RegistryTableItem
    {
        private long id;
        private string name;
        private string active;

        public long Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Active { get => active; set => active = value; }


        public RegistryTableItem(RegistryTableItem registryTableItem)
        {
            id = registryTableItem.Id;
            name = registryTableItem.Name;
            if(registryTableItem.Active.Equals("Yes"))
            {
                active = "false";
            }else if (registryTableItem.Active.Equals("true"))
            {
                active = "Yes";
            }else if (registryTableItem.Active.Equals("No"))
            {
                active = "true";
            }
            else 
            {
                active = "No";
            }
        }

        public RegistryTableItem(long id, string name, string active)
        {
            this.Id = id;
            this.Name = name;
            this.Active = active;
        }

        public RegistryTableItem()
        {
        }

        public string ToJSON()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{");
            if (id!=0)
            {
                stringBuilder.Append("\"id\" : \""+id+"\",");
            }
            if(name!=null)
            stringBuilder.Append("\"name\" : \"" + name + "\",");

            stringBuilder.Append("\"active\" : \"" + active + "\"");

           

            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }
    }
}
