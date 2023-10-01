using CodingCommunity.Domain.Services;
using CodingCommunity.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodingCommunity.API.Controllers
{
    [ApiController]
    [Route("api/v1/questions")]
    public class QuestionController: ControllerBase
    {
        private readonly QuestionService _questionService;

        private readonly TokenService _tokenService;

        public QuestionController(QuestionService questionService, TokenService tokenService)
        {
            _questionService = questionService;
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            try
            {
                // Call the service method to fetch all questions
                var questions = await _questionService.GetAllQuestionsAsync();

                // Return the list of questions as a JSON response
                return Ok(questions);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an error response if needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (HttpContext.Request.Headers.TryGetValue("user-token", out var userToken))
            {
                
            }
            else
            {
                return Unauthorized("User token is missing.");
            }

            try
            {
                var question = await _questionService.CreateQuestionAsync(request.Title, request.Content, request.Tags, userToken);

                return CreatedAtAction(nameof(GetQuestion), new { id = question.QuestionId }, question);
            } catch (InvalidOperationException ex) {
                return StatusCode(403, "Invalid user token or token has expired.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion(int id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

    }
}
