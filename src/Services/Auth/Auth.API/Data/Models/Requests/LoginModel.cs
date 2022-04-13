using System.ComponentModel.DataAnnotations;

namespace Auth.API.Data.Models.Requests
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
