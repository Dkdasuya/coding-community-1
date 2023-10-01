using CodingCommunity.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingCommunity.Domain.Services
{
    public class TokenService
    {
        private readonly CodingCommunityContext _context;

        public TokenService(CodingCommunityContext context)
        {
            _context = context;
        }

        public async Task<int?> GetUserIdFromToken(string token)
        {
            var userToken = await _context.TokenTables.FirstOrDefaultAsync(t => t.Token == token);

            if (userToken != null && userToken.ExpirationUtc > DateTime.UtcNow)
            {
                // Token is valid and not expired
                return userToken.UserId;
            }

            // Token is not valid or has expired
            return null;
        }
    }
}
