using System;
using System.Collections.Generic;

namespace CodingCommunity.Infrastructure
{
    public partial class Answer
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Question Question { get; set; } = null!;
        public virtual UserTable User { get; set; } = null!;
    }
}
