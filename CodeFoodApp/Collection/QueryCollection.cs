using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFoodApp.Collection
{
    public class QueryCollection
    {
        public static string qRegister = @"
USE CodeFood;
INSERT IGNORE INTO Users
    ( username, password, created_at)
VALUES
    ( @@username, @@password, NOW());
                ";
        public static string qGetUserData = @"
USE CodeFood;
SELECT id, username FROM Users
WHERE id=@@password AND username=@@username ;";

    }
}
