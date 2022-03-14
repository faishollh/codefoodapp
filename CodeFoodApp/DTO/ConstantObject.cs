using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFoodApp.DTO
{
    public class ConstantObject
    {
    }
    public class MessageWording
    {
        public const string REQUIRED_PASSWORD = "password is required";
        public const string REQUIRED_USERNAME = "username is required";
        public const string INVALID_USERNAME_PASSWORD = "Invalid username or Password";
        public const string PASSWORD_MINIMUM = "password minimum 6 characters";
        public const string SUCCESS = "Success";
        public const string ALREADY_REGISTERED = "username [username] already registered";
        public const string TOO_MANY_TRY = "Too many invalid login, please wait for 1 minute";
        


    }
    public class ErrorStatus
    {
        public const int REQUIRED_PASSWORD = 400;
        public const int REQUIRED_USERNAME = 400;
        public const int PASSWORD_MINIMUM = 400;
        public const int ALREADY_REGISTERED = 400;
        public const int INVALID_USERNAME_PASSWORD = 401;
        public const int SUCCESS = 200;
        public const int TOO_MANY_TRY = 403;


    }
}
