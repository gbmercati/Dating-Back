
using App_Dating.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Azure.Core.HttpHeader;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace App_Dating.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userServices;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userServices, ILogger<UserController> logger)
        {
            _userServices = userServices;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] NewUser newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var validationError = await ValidateBusinessRules(newUser);
            if (validationError.Any())
            {
                return BadRequest(System.String.Join(", ", validationError.ToArray()));
            }

            try
            {
                await _userServices.AddUserAsync(
                    newUser.UserName,
                    newUser.Name,
                    newUser.LastName,
                    newUser.Age,
                    newUser.Gender,
                    newUser.City,
                    newUser.Email
                    );

                return Ok("User created successfully.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(statusCode: 500, "Error internal");
            }
        }

        private async Task<List<string>> ValidateBusinessRules(NewUser newUser)
        {
            var validationError = new List<string>();
            if (newUser.Age < 18)
            {
                validationError.Add("The user must be of legal age.");
            }
            if (await _userServices.IsEmailRegisteredAsync(newUser.Email))
            {
                validationError.Add("The email entered is already registered.");
            }

            return validationError;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateserAsync([FromBody] UpdateUser updateUser)
        {
            try
            {
                await _userServices.UpdateUserAsync(
                    updateUser.Id,
                    updateUser.Name,
                    updateUser.LastName,
                    updateUser.Age,
                    updateUser.Gender,
                    updateUser.City,
                    updateUser.Email
                    );

                return Ok("User updated successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(statusCode: 500, "Error internal");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync([FromRoute] long id)
        {
            try
            {
                var user = await _userServices.GetUserAsync(id);

                if (user == null)
                {
                    return NotFound(new { message = "User not found" });
                }

                return Ok(user);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return NotFound(new { message = "User not found" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {

            var users = await _userServices.GetUsersAsync();

            return Ok(users);
        }

    }

    public class NewUser
    {
        public string UserName { get; set; } = null!;
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        [MaxLength(1)]
        public string? Gender { get; set; }
        public string? City { get; set; }
        [Required, DataType(DataType.EmailAddress)]

        public string? Email { get; set; }
    }

    public class UpdateUser
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? City { get; set; }
        public string? Email { get; set; }
    }

    public class ResponseUser
    {
        public long Id { get; set; }
        public string UserName { get; set; } = null!;
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? City { get; set; }
        public string? Email { get; set; }
    }

    public class ValidationError
    {
        public string Message { get; set; }
    }
}
