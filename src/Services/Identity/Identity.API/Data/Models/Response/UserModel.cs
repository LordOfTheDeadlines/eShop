using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Data.Models.Response
{
    public class UserModel
    {
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
    }
}
