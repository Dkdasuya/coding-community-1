using Microsoft.AspNetCore.Mvc;
using CodingCommunity.Domain.Services;
using static CodingCommunity.Shared.DTO.RegistrationRequest;
using CodingCommunity.Shared.DTO;

namespace CodingCommunity.API.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {

            try
            {
                // Call the registration service method to register the user.
                var result = await _authService.RegisterAsync(request);

                if (result.Success)
                {
                    // Return a 201 Created response if registration is successful.
                    return StatusCode(StatusCodes.Status201Created);
                }
                else
                {
                    // Return a 400 Bad Request response with an error message.
                    return BadRequest(new { result.Message });
                }
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors.
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred." });
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {

                // If login is successful, generate a token
                string token = await _authService.GenerateToken(loginRequest.Username, loginRequest.Password);

                if (token != null)
                {
                    // Return the token as a response
                    return Ok(new { token });
                }
                else
                {
                    // Authentication failed
                    return Unauthorized(new { message = "Invalid credentials" });
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an appropriate error response
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string token)
        {
            // Check if the token exists in the database.
            if (!_authService.TokenExists(token))
            {
                // Token not found in the database.
                return NotFound("Token not found.");
            }

            // Delete the token from the database.
            _authService.DeleteToken(token);

            // You can return any appropriate response here.
            return Ok("Logout Successful");
        }
    }

    
}
