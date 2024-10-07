using App_Dating.Models;
using Microsoft.EntityFrameworkCore;

namespace App_Dating.Services
{
    public interface IPreferenceService
    {
        Task<List<Preference>> GetAllPreferenceAsync();

    }
    public class PreferenceService: IPreferenceService
    {
        private readonly MyDbContext _context;

        public PreferenceService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Preference>> GetAllPreferenceAsync()
        {
            return await _context.Preferences.ToListAsync();
        }
    }
}
