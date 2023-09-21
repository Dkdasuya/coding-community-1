using System;
using System.Collections.Generic;

namespace CodingCommunity.Infrastructure
{
    public partial class UserTable
    {
        public UserTable()
        {
            Answers = new HashSet<Answer>();
            Questions = new HashSet<Question>();
            TokenTables = new HashSet<TokenTable>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<TokenTable> TokenTables { get; set; }
    }
}
