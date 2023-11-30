using System.Threading.Tasks;
using WebAPI.Models;
namespace WebAPI.Repository
{
    public interface IUserRepository
    {
        Task<UserDTO> LoginAsync(string username , string password);
        Task AddUsersAsync(UserDTO userDTO);
        Task UpdateUsersAsync(Guid uId, string password);
    }
}
