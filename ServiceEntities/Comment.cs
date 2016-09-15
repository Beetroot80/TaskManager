using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceEntities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int DomainTaskId { get; set; }
        public int UserId { get; set; }
    }
}
