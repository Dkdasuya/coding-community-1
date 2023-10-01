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
    public class QuestionService
    {

        private readonly CodingCommunityContext _context;

        private readonly TokenService _tokenService;

        public QuestionService(CodingCommunityContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        /*public async Task<List<QuestionDto>> GetAllQuestionsAsync()
        {
            var questions = await _context.Questions
                .Select(q => new QuestionDto
                {
                    Id = q.QuestionId,
                    Title = q.Title,
                    Content = q.Content,
                    // Map other properties as needed
                })
                .ToListAsync();

            return questions;
        }*/

        public async Task<List<QuestionDto>> GetAllQuestionsAsync()
        {
            //var questions = _questionRepository.GetAllQuestions(); // Replace with your actual repository method to fetch questions.

            var questionDtos = await _context.Questions.Select(q => new QuestionDto
            {
                Id = q.QuestionId,
                Title = q.Title,
                Content = q.Content,
                Tags = q.Tags.Select(t => t.TagName).ToList(),
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt
            }).ToListAsync();

            return questionDtos;
        }


        public async Task<Question> CreateQuestionAsync(string title, string content, List<string> tagNames, string userToken)
        {
            int? userId = await _tokenService.GetUserIdFromToken(userToken);
            // Create a new question
            if(userId == null)
            {
                throw new InvalidOperationException("User token is not valid or has expired.");
            }
            var question = new Question
            {
                UserId = (int)userId,
                Title = title,
                Content = content,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Add tags to the question
            foreach (var tagName in tagNames)
            {
                var tag = await _context.Tags.FirstOrDefaultAsync(t => t.TagName == tagName);

                if (tag == null)
                {
                    tag = new Tag { TagName = tagName };
                    _context.Tags.Add(tag);
                }

                question.Tags.Add(tag);
            }

            // Add the question to the context and save changes
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            return question;
        }

        public async Task<Question> GetQuestionByIdAsync(int questionId)
        {
            return await _context.Questions
                .Include(q => q.Tags)
                .FirstOrDefaultAsync(q => q.QuestionId == questionId);
        }
    }
}
