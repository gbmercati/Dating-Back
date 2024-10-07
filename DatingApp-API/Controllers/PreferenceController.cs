using App_Dating.Services;
using Microsoft.AspNetCore.Mvc;

namespace App_Dating.Controllers
{
    public class PreferenceController : Controller
    {
        private readonly IPreferenceService _preferenceService;
        private readonly ILogger<PreferenceController> _logger;

        public PreferenceController(IPreferenceService preferenceService, ILogger<PreferenceController> logger)
        {
            _preferenceService = preferenceService;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> GetUserAsync()
        {
            try
            {
                var prefrences = await _preferenceService.GetAllPreferenceAsync();
                if (prefrences.Count == 0) {

                    return NotFound(new { message = "Preferences does not found" });
                }

                return Ok(prefrences);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                
                return StatusCode(statusCode: 500, "Error internal");
            }
        }
    }
}
