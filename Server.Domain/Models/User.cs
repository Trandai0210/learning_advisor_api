using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Domain.Models
{
    public partial class User
    {
        public User()
        {
            Messages = new HashSet<Message>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Gmail { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public DateTime? Dob { get; set; }
        public bool? Status { get; set; }
        public int? PermissionId { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
