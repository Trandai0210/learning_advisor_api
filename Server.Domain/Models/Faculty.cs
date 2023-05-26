using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Domain.Models
{
    public partial class Faculty
    {
        public Faculty()
        {
            Messages = new HashSet<Message>();
        }

        public int FacultyId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
