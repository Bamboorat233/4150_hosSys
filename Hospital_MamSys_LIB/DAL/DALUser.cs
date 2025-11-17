using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using Hospital_MamSys_LIB.Model;

namespace Hospital_MamSys_LIB.DAL
{
    public class DALUser : DALBase
    {
        // 用户认证
        public User Authenticate(string username, string password)
        {
            const string sql = @"
SELECT UserID, Username, Password, Role, Email, CreatedDate, IsActive
FROM [User]
WHERE Username = @username AND Password = @password AND IsActive = 1;";

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@username", username ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@password", password ?? (object)DBNull.Value);

                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;

                    return new User
                    {
                        UserID = rd.GetInt32(rd.GetOrdinal("UserID")),
                        Username = rd["Username"] as string,
                        Password = rd["Password"] as string,
                        Role = rd["Role"] as string,
                        Email = rd["Email"] as string,
                        CreatedDate = rd.GetDateTime(rd.GetOrdinal("CreatedDate")),
                        IsActive = rd.GetBoolean(rd.GetOrdinal("IsActive"))
                    };
                }
            }
        }

        // 添加用户
        public void AddUser(User u)
        {
            const string sql = @"
INSERT INTO [User] (Username, Password, Role, Email, CreatedDate, IsActive)
VALUES (@username, @password, @role, @email, @created, @active);";

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@username", u.Username ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@password", u.Password ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@role", u.Role ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@email", u.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@created", u.CreatedDate);
                cmd.Parameters.AddWithValue("@active", u.IsActive);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // 获取所有用户
        public List<User> GetAllUsers()
        {
            const string sql = @"
SELECT UserID, Username, Password, Role, Email, CreatedDate, IsActive
FROM [User]
ORDER BY Username;";

            var users = new List<User>();
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        users.Add(new User
                        {
                            UserID = rd.GetInt32(rd.GetOrdinal("UserID")),
                            Username = rd["Username"] as string,
                            Password = rd["Password"] as string,
                            Role = rd["Role"] as string,
                            Email = rd["Email"] as string,
                            CreatedDate = rd.GetDateTime(rd.GetOrdinal("CreatedDate")),
                            IsActive = rd.GetBoolean(rd.GetOrdinal("IsActive"))
                        });
                    }
                }
            }
            return users;
        }

        // 更新用户
        public int UpdateUser(User u)
        {
            const string sql = @"
UPDATE [User] 
SET Username = @username, 
    Password = @password, 
    Role = @role, 
    Email = @email, 
    IsActive = @active
WHERE UserID = @id;";

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@username", u.Username ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@password", u.Password ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@role", u.Role ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@email", u.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@active", u.IsActive);
                cmd.Parameters.AddWithValue("@id", u.UserID);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // 删除用户
        public int DeleteById(int userId)
        {
            const string sql = "DELETE FROM [User] WHERE UserID = @id;";
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", userId);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
