using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskDBContext _dbContext;
        public UserRepository(TaskDBContext context)
        {
            _dbContext = context;
        }
        public async Task AddUsersAsync(UserDTO userDto)
        {
            var newusers = new UserModel
            {
                Id = Guid.NewGuid(),
                username = userDto.username,
                passwords = Encrypted.HashPassword(userDto.passwords)
            };
            _dbContext.Users.Add(newusers);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserDTO> LoginAsync(string username , string password)
        {

            var users = await _dbContext.Users.FirstOrDefaultAsync(u => u.username == username);
            bool success = Encrypted.VerifyPassword(password,users.passwords);
            if (!success)
            {
                return null;
            }

            return new UserDTO
            {
                username = users.username
            };
        }

        public async Task UpdateUsersAsync(Guid uId, string passwords)
        {
            var exisitingusers = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == uId);
            if(exisitingusers == null)
            {
                return;
            }
            exisitingusers.passwords = Encrypted.HashPassword(passwords);
            _dbContext.Users.Update(exisitingusers);
            await _dbContext.SaveChangesAsync();
        }
    }
}
