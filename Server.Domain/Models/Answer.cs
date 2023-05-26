using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Domain.Models
{
    public partial class Answer
    {
        public Answer()
        {
            Questions = new HashSet<Question>();
        }

        public int AnswerId { get; set; }
        public string Content { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
