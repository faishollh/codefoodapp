using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFoodApp.DTO
{
    public class AuthObject
    {
        public string token { get; set; }
    }
    public class UserObject
    {
        public int id { get; set; }
        public string username { get; set; }
    }
}
