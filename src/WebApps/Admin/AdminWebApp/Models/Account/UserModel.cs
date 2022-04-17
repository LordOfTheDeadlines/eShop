using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminWebApp.Models.Account
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
