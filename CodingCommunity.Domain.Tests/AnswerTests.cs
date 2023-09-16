using CodingCommunity.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingCommunity.Domain.Tests
{
    public class AnswerTests
    {
        [Fact]
        public void CreateAnswer_Success()
        {

            Answer answer = new Answer
            {
                AnswerID = 1,
                QuestionID = 3,
                UserID = 4,
                Content = "This is an answer.",
                CreatedAt = new System.DateTime(2023, 8, 3),
                UpdatedAt = new System.DateTime(2023, 8, 4)
            };


            Assert.Equal(1, answer.AnswerID);
            Assert.Equal(3, answer.QuestionID);
            Assert.Equal(4, answer.UserID);
            Assert.Equal("This is an answer.", answer.Content);
            Assert.Equal(new System.DateTime(2023, 8, 3), answer.CreatedAt);
            Assert.Equal(new System.DateTime(2023, 8, 4), answer.UpdatedAt);
        }
    }
}
