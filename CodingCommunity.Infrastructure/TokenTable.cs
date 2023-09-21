using System;
using System.Collections.Generic;

namespace CodingCommunity.Infrastructure
{
    public partial class TokenTable
    {
        public int TokenId { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; } = null!;
        public DateTime ExpirationUtc { get; set; }

        public virtual UserTable User { get; set; } = null!;
    }
}
