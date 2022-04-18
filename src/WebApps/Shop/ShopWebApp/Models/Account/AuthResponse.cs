using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebApp.Models
{
    public class AuthResponse<T>
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
        public T Value { get; set; }
    }
}
