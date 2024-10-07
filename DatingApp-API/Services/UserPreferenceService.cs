using App_Dating.Models;

namespace App_Dating.Services
{

    public interface IUserPreferenceService
    {
        Task AddUsersPreferenceAsync(List<long> idsPreferences, long idUser);

    }
    public class UserPreferenceService : IUserPreferenceService
    {
        private readonly MyDbContext _context;

        public UserPreferenceService(MyDbContext context)
        {
            _context = context;
          
        }
        public async Task AddUsersPreferenceAsync(List<long> idsPreferences, long idUser)
        {
            List<UserPreference> usersPreference = new List<UserPreference>();

            foreach (long id in idsPreferences)
            {
                usersPreference.Add(new UserPreference { Id = idUser, IdPreferences = id });
            }

            await  _context.UserPreferences.AddRangeAsync(usersPreference);

            await _context.SaveChangesAsync();
        }
    }
}
