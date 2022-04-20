using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApp.Models.Account
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
