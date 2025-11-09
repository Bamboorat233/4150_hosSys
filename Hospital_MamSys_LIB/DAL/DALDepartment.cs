using Hospital_MamSys_LIB.DAL;
using Hospital_ManSys_LIB.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Hospital_ManSys_LIB.DAL
{
    internal class DALDepartment : DALBase
    {
        public Department GetById(int id)
        {
            const string sql = @"SELECT DepartmentID, Name, Location
                                 FROM dbo.Department WHERE DepartmentID=@id";
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (!r.Read()) throw new InvalidOperationException("Department not found.");
                    return new Department
                    {
                        DepartmentID = r.GetInt32(0),
                        Name = r.GetString(1),
                        Location = r.IsDBNull(2) ? "" : r.GetString(2)
                    };
                }
            }
        }

        public List<Department> GetAll()
        {
            const string sql = @"SELECT DepartmentID, Name, Location FROM dbo.Department ORDER BY Name";
            List<Department> list = new List<Department>();
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Department
                        {
                            DepartmentID = r.GetInt32(0),
                            Name = r.GetString(1),
                            Location = r.IsDBNull(2) ? "" : r.GetString(2)
                        });
                    }
                }
            }
            return list;
        }

        public int Insert(Department d)
        {
            const string sql = @"INSERT INTO dbo.Department(Name, Location)
                                 OUTPUT INSERTED.DepartmentID
                                 VALUES(@name, @loc)";
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@name", d.Name);
                cmd.Parameters.AddWithValue("@loc", d.Location);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public int Update(Department d)
        {
            const string sql = @"UPDATE dbo.Department
                                 SET Name=@name, Location=@loc
                                 WHERE DepartmentID=@id";
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@name", d.Name);
                cmd.Parameters.AddWithValue("@loc", d.Location);
                cmd.Parameters.AddWithValue("@id", d.DepartmentID);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int Delete(int id)
        {
            const string sql = @"DELETE FROM dbo.Department WHERE DepartmentID=@id";
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
