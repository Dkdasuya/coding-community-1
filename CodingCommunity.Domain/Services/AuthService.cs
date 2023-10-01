using CodingCommunity.Infrastructure;
using CodingCommunity.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CodingCommunity.Domain.Services
{
    public class AuthService
    {
        private readonly CodingCommunityContext _context;

        public AuthService(CodingCommunityContext context)
        {
            _context = context;
        }
        public async Task<RegistrationResult> RegisterAsync(RegistrationRequest request)
        {
            // Validate the input data, e.g., check if the username is unique.
            // You can add custom validation logic here.

            // Check if the username is already in use.
            if (_context.UserTables.Any(u => u.Username == request.Username))
            {
                return new RegistrationResult(false, "Username is already in use.");
            }

            // Create a new user and save it to the database.
            var user = new UserTable
            {
                Username = request.Username,
                Password = request.Password
            };

            _context.UserTables.Add(user);
            await _context.SaveChangesAsync();

            return new RegistrationResult(true, "User registration successful.");
        }

        public async Task<string> GenerateToken(string username, string password)
        {
            // Validate the user's credentials here, e.g., by checking the database
            var user = await _context.UserTables.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                return null; // Authentication failed
            }

            // Generate a random 8-character alphanumeric token
            string token = GenerateRandomToken();

            // Save the token in the database (assuming you have a UserToken table)
            var userToken = new TokenTable
            {
                UserId = user.UserId,
                Token = token,
                ExpirationUtc = DateTime.UtcNow.AddMinutes(10) 
            };

            _context.TokenTables.Add(userToken);
            await _context.SaveChangesAsync();

            return token;
        }

        public bool TokenExists(string token)
        {
            // Check if the token exists in the database.
            return _context.TokenTables.Any(t => t.Token == token);
        }

        public void DeleteToken(string token)
        {
            // Delete the token from the database.
            var tokenToDelete = _context.TokenTables.SingleOrDefault(t => t.Token == token);

            if (tokenToDelete != null)
            {
                _context.TokenTables.Remove(tokenToDelete);
                _context.SaveChanges();
            }
        }

        private string GenerateRandomToken()
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var token = new StringBuilder(8);

            for (int i = 0; i < 8; i++)
            {
                token.Append(characters[random.Next(characters.Length)]);
            }

            return token.ToString();
        }
    }
}
