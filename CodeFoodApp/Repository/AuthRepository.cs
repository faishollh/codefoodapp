using CodeFoodApp.Collection;
using CodeFoodApp.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodeFoodApp.Repository
{
    public class AuthRepository
    {

        private static string salt = "nc23e";
        #region db connection
        public string ConnectionString { get; set; }

        public AuthRepository(string connectionString)
        {

            this.ConnectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = ConnectionString;
            return myConnection;
        }
        #endregion
        #region helper
        private string md5Hash(string hash)
        {
          
            byte[] asciiBytes = ASCIIEncoding.ASCII.GetBytes(hash);
            byte[] hashedBytes = MD5CryptoServiceProvider.Create().ComputeHash(asciiBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        #endregion
        public List<UserObject> Login(LoginParameter param)
        {
            List<UserObject> list = new List<UserObject>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(QueryCollection.qGetUserData.Replace("@@username", param.username).Replace("@@password", param.password));

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new UserObject()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            username = Convert.ToString(reader["username"]),
                        });
                    }
                }
            }
            return list;
        }
        public List<UserObject> Register(LoginParameter param)
        {
            List<UserObject> list = new List<UserObject>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(QueryCollection.qGetUserData.Replace("@@username", param.username).Replace("@@password", param.password));

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new UserObject()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            username = Convert.ToString(reader["username"]),
                        });
                    }
                }
                if (list.Count() == 0)
                {
                    var encryptPass = md5Hash(param.password+salt);
                    cmd = new MySqlCommand(QueryCollection.qRegister.Replace("@@username",param.username).Replace("@@password", encryptPass));

                    using (var reader = cmd.ExecuteReader())
                    {
                        
                    }
                }
            }
            return list;
        }
    }
}
