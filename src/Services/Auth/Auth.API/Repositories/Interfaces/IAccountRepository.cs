using Auth.API.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auth.API.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<AppUser>> GetUsers();
        Task<IEnumerable<AppRole>> GetRoles();
        Task<AppUser> GetUser(int id);
        Task<AppUser> GetRole(int id);
    }
}
