using System;
using System.Data.SqlClient;
using Hospital_MamSys_LIB.Model;

namespace Hospital_MamSys_LIB.DAL
{
    internal class DALPatient : DALBase
    {
        public void AddPatient(Patient p)
        {
            const string sql = @"
INSERT INTO Patient (Name, DOB, Gender, Contact, [Address])
VALUES (@n, @dob, @g, @c, @a);";

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@n", p.Name ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@dob", p.DOB); // DOB 为 NOT NULL
                cmd.Parameters.AddWithValue("@g", p.Gender ?? (object)DBNull.Value); // 需满足 'Male'/'Female'/'Other'
                cmd.Parameters.AddWithValue("@c", (object?)p.Contact ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@a", (object?)p.Address ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public Patient GetById(int patientId)
        {
            const string sql = @"
SELECT PatientID, Name, DOB, Gender, Contact, [Address]
FROM Patient
WHERE PatientID = @id;";

            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", patientId);
                conn.Open();

                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;

                    return new Patient
                    {
                        PatientID = rd.GetInt32(rd.GetOrdinal("PatientID")),
                        Name = rd["Name"] as string,
                        DOB = rd.GetDateTime(rd.GetOrdinal("DOB")),
                        Gender = rd["Gender"] as string,
                        Contact = rd["Contact"] as string,
                        Address = rd["Address"] as string
                    };
                }
            }
        }
        public int DeleteById(int patientId)
        {
            const string sql = "DELETE FROM Patient WHERE PatientID = @id;";
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", patientId);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
        public int UpdateContactById(int patientId, string newContact)
        {
            const string sql = "UPDATE Patient SET Contact = @contact WHERE PatientID = @id;";
            using (var conn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@contact", (object?)newContact ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@id", patientId);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
