using App_Dating.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace App_Dating.Services
{
    public interface IUserService
    {
        Task AddUserAsync(string userName, string? name, string? lastName,
           int? age, string? gender, string? city, string? address);

        Task UpdateUserAsync(long id, string? name, string ?lastName,
            int? age, string? gender, string? city, string? address);

        Task<User?> GetUserAsync(long id);

        Task<bool> IsEmailRegisteredAsync(string email);

        Task<List<User>> GetUsersAsync();

    }
    public class UserService: IUserService
    {
        private readonly MyDbContext _context;

        public UserService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsEmailRegisteredAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task AddUserAsync(string userName, string? name, string? lastName,
            int? age, string? gender, string? city, string? address)
        {
          
            var user = new User
            {
                UserName = userName,
                Name = name,
                LastName = lastName,
                Age = age,
                Gender = gender,
                City = city,
                Email = address
            };          

            _context.Users.Add(user);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(long id, string? name, string? lastName,
            int? age, string? gender, string? city, string? address)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return ; 
            }

            user.Name = name;
            user.LastName = lastName;
            user.Age = age;
            user.Gender = gender;
            user.City = city;
            user.Email = address;           

            _context.Users.Add(user);

            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserAsync(long id)
        {
            var user = await _context.Users.FindAsync(id);          

            return user;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var users = await _context.Users.ToListAsync();

            return users;
        }

    }
}
