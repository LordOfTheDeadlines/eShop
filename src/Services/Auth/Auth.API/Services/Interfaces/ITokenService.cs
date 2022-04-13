using Auth.API.Data.Entities;

namespace Auth.API.Services.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(AppUser user);
        //public RefreshToken GenerateRefreshToken();
    }
}
