using Auth.API.Data;
using Auth.API.Data.Entities;
using Auth.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auth.API.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AuthContext _context;

        public AccountRepository(AuthContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Task<AppUser> GetRole(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<AppRole>> GetRoles()
        {
            throw new System.NotImplementedException();
        }

        public Task<AppUser> GetUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<AppUser>> GetUsers()
        {
            throw new System.NotImplementedException();
        }
    }
}
