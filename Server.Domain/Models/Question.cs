using System;
using System.Collections.Generic;

#nullable disable

namespace Server.Domain.Models
{
    public partial class Question
    {
        public int QuestionId { get; set; }
        public string Keyword { get; set; }
        public int? AnswerId { get; set; }

        public virtual Answer Answer { get; set; }
    }
}
