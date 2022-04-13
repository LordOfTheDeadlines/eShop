using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Auth.API.Data.Models.Responses
{
    public class UserModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
