using System;
using System.Collections.Generic;

namespace CodingCommunity.Infrastructure
{
    public partial class Tag
    {
        public Tag()
        {
            Questions = new HashSet<Question>();
        }

        public int TagId { get; set; }
        public string TagName { get; set; } = null!;

        public virtual ICollection<Question> Questions { get; set; }
    }
}
