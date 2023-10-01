using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CodingCommunity.Infrastructure
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            Tags = new HashSet<Tag>();
        }

        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual UserTable User { get; set; } = null!;
        public virtual ICollection<Answer> Answers { get; set; }
        [JsonIgnore]
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
