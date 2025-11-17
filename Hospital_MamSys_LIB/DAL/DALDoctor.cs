using Hospital_MamSys_LIB.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Hospital_MamSys_LIB.DAL
{
    public class DALDoctor : DALBase
    {
        public void AddDoctor(Doctor d)
        {
            const string sql = @"
INSERT INTO Doctor (Name, Specialization, Contact, DepartmentID)
VALUES (@n, @s, @c, @dept);";

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@n", d.Name ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@s", d.Specialization ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@c", d.Contact ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@dept", d.DepartmentID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Doctor GetById(int doctorId)
        {
            const string sql = @"
SELECT DoctorID, Name, Specialization, Contact, DepartmentID
FROM Doctor
WHERE DoctorID = @id;";

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", doctorId);
                conn.Open();

                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;

                    return new Doctor
                    {
                        DoctorID = rd.GetInt32(rd.GetOrdinal("DoctorID")),
                        Name = rd["Name"] as string,
                        Specialization = rd["Specialization"] as string,
                        Contact = rd["Contact"] as string,
                        DepartmentID = rd.GetInt32(rd.GetOrdinal("DepartmentID"))
                    };
                }
            }
        }

        public int DeleteById(int doctorId)
        {
            const string sql = "DELETE FROM Doctor WHERE DoctorID = @id;";
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", doctorId);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        public int UpdateContactById(int doctorId, string newContact)
        {
            const string sql = "UPDATE Doctor SET Contact = @contact WHERE DoctorID = @id;";
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@contact", (object)newContact ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@id", doctorId);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int UpdateDoctor(Doctor d)
        {
            const string sql = @"
UPDATE Doctor 
SET Name = @n, Specialization = @s, Contact = @c, DepartmentID = @dept
WHERE DoctorID = @id;";

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@n", d.Name ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@s", d.Specialization ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@c", d.Contact ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@dept", d.DepartmentID);
                cmd.Parameters.AddWithValue("@id", d.DoctorID);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<Doctor> GetAllDoctors()
        {
            const string sql = @"
SELECT DoctorID, Name, Specialization, Contact, DepartmentID
FROM Doctor
ORDER BY Name;";

            var doctors = new List<Doctor>();
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        doctors.Add(new Doctor
                        {
                            DoctorID = rd.GetInt32(rd.GetOrdinal("DoctorID")),
                            Name = rd["Name"] as string,
                            Specialization = rd["Specialization"] as string,
                            Contact = rd["Contact"] as string,
                            DepartmentID = rd.GetInt32(rd.GetOrdinal("DepartmentID"))
                        });
                    }
                }
            }
            return doctors;
        }
    }
}
