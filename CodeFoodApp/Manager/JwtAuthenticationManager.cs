using CodeFoodApp.Collection;
using CodeFoodApp.DTO;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CodeFoodApp.Manager
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        #region db connection
        public string ConnectionString { get; set; }

        public MySqlConnection GetConnection()
        {
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = ConnectionString;
            return myConnection;
        }
        private readonly string key;
        public JwtAuthenticationManager(string key, string connectionString)
        {
            this.key = key;
            this.ConnectionString = connectionString;
        }

        #endregion
        public string Authenticate(string username, string password)
        {
            List<UserObject> list = new List<UserObject>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(QueryCollection.qGetUserData.Replace("@@username", username).Replace("@@password", password));
                using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new UserObject()
                            {
                                id = reader.GetInt32("id"),
                                username = reader.GetString("username"),
                            });
                        }
                    
                }
            }

            if (list.Count()<=0)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                { new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                                        SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
