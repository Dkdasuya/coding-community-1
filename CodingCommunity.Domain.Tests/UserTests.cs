using CodingCommunity.Domain.Models;
using Xunit;

namespace CodingCommunity.Domain.Tests
{
    public class UserTests
    {
        [Fact]
        public void CreateUser_Success()
        {
            User user = new User
            {
                UserID = 1,
                Username = "testUserName",
                Password = "testPassword",
            };

            //Assertions
            Assert.NotNull(user);
            Assert.Equal(1, user.UserID);
            Assert.Equal("testUserName", user.Username);
            Assert.Equal("testPassword", user.Password);
        }
    }
}