using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminApplication.Model
{
    public class Comment
    {
        private long id;
        private string content;

        public Comment(long id, string content)
        {
            this.id = id;
            this.content = content;
        }

        public Comment()
        {
        }

        public long Id { get => id; set => id = value; }
        public string Content { get => content; set => content = value; }
    }
}
