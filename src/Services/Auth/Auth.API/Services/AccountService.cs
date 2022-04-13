using Auth.API.Data;
using Auth.API.Data.Entities;
using Auth.API.Data.Models.Requests;
using Auth.API.Data.Models.Responses;
using Auth.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Auth.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly AuthContext _db;

        public AccountService(AuthContext appDbContext)
        {
            _db = appDbContext?? throw new ArgumentNullException(nameof(appDbContext));

            if (!_db.Roles.Any(r => r.Name == "Admin"))
            {
                var admin = new AppUser("admin", "admin");
                var role = new AppRole("Admin");
                admin.Roles = new();
                admin.Roles.Add(role);
                _db.Users.Add(admin);
                _db.SaveChanges();
            }
        }
        public ResultResponse<AppUser> Login(LoginModel loginModel)
        {
            var user = _db.Users
                .Include(user => user.Roles)
                .FirstOrDefault(u => u.Username == loginModel.Username);

            if (user == null || !user.ValidatePassword(loginModel.Password))
                return ResultResponse<AppUser>.CreateError("invalid username or password");

            return ResultResponse<AppUser>.CreateSuccess(user);
        }

        public ResultResponse<AppUser> Register(RegisterModel registerModel)
        {
            if (_db.Users.Any(u => u.Username == registerModel.Username))
                return ResultResponse<AppUser>.CreateError("user already exists");

            var user = new AppUser(
                registerModel.Username,
                registerModel.Password
            );

            var role = new AppRole("User");
            user.Roles = new();
            user.Roles.Add(role);

            _db.Users.Add(user);
            _db.SaveChanges();

            return ResultResponse<AppUser>.CreateSuccess(user);
        }

    }
}
