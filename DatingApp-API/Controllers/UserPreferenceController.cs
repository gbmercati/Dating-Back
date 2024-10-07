using App_Dating.Services;
using Microsoft.AspNetCore.Mvc;

namespace App_Dating.Controllers
{
    public class UserPreferenceController : Controller
    {

        private readonly IUserPreferenceService _userPreferenceService;
        private readonly ILogger<UserPreferenceController> _logger;

        public UserPreferenceController(IUserPreferenceService userPreferenceService, ILogger<UserPreferenceController> logger)
        {
            _userPreferenceService = userPreferenceService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] NewUserPreference newUserPreference)
        {
            try
            {
                await _userPreferenceService.AddUsersPreferenceAsync(
                    newUserPreference.IdsPreferences,
                    newUserPreference.IdUser
                    );

                return Ok(new { message = "User updated successfully" });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(statusCode: 500, "Error internal");
            }
        }
    }

    public class NewUserPreference
    {
        public long IdUser { get; set; }
        public  List<long> IdsPreferences { get; set; } = new List<long>();
    }
}
