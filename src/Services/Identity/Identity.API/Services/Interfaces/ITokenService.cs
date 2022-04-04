using Identity.API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Services.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(AppUser user);
        public RefreshToken GenerateRefreshToken();
    }
}
