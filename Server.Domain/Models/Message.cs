using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Domain.Models
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UserId { get; set; }
        public int? FacultyId { get; set; }

        public virtual Faculty Faculty { get; set; }
        public virtual User User { get; set; }
    }
}
