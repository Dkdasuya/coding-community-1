using CodingCommunity.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingCommunity.Domain.Tests
{
    public class QuestionTests
    {
        [Fact]
        public void CreateQuestion_Success()
        {

            Question question = new Question
            {
                QuestionID = 1,
                UserID = 2,
                Title = "Sample Question",
                Content = "This is a sample question.",
                CreatedAt = new System.DateTime(2023, 8, 1),
                UpdatedAt = new System.DateTime(2023, 8, 2)
            };

            Assert.Equal(1, question.QuestionID);
            Assert.Equal(2, question.UserID);
            Assert.Equal("Sample Question", question.Title);
            Assert.Equal("This is a sample question.", question.Content);
            Assert.Equal(new System.DateTime(2023, 8, 1), question.CreatedAt);
            Assert.Equal(new System.DateTime(2023, 8, 2), question.UpdatedAt);
        }
    }
}
