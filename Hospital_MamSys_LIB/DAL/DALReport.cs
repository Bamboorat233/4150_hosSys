using Hospital_MamSys_LIB.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_MamSys_LIB.DAL
{
    public class DALReport : DALBase
    {
        // CREATE 
        public void AddReport(Report r)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO Report (PatientID, DoctorID, ReportDate, Diagnosis, Treatment) " +
                "VALUES (@p, @d, @date, @diag, @treat)", conn))
            {
                cmd.Parameters.AddWithValue("@p", r.PatientID);
                cmd.Parameters.AddWithValue("@d", r.DoctorID);
                cmd.Parameters.AddWithValue("@date", r.ReportDate);
                cmd.Parameters.AddWithValue("@diag", r.Diagnosis);
                cmd.Parameters.AddWithValue("@treat", (object)r.Treatment ?? DBNull.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // READ 
        public Report GetReportById(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT ReportID, PatientID, DoctorID, ReportDate, Diagnosis, Treatment " +
                "FROM Report WHERE ReportID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return new Report
                        {
                            ReportID = rdr.GetInt32(0),
                            PatientID = rdr.GetInt32(1),
                            DoctorID = rdr.GetInt32(2),
                            ReportDate = rdr.GetDateTime(3),
                            Diagnosis = rdr.GetString(4),
                            Treatment = rdr.IsDBNull(5) ? null : rdr.GetString(5)
                        };
                    }
                }
            }
            return null;
        }

        // READ 
        public List<Report> GetAllReports()
        {
            List<Report> list = new List<Report>();

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT ReportID, PatientID, DoctorID, ReportDate, Diagnosis, Treatment FROM Report",
                conn))
            {
                conn.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new Report
                        {
                            ReportID = rdr.GetInt32(0),
                            PatientID = rdr.GetInt32(1),
                            DoctorID = rdr.GetInt32(2),
                            ReportDate = rdr.GetDateTime(3),
                            Diagnosis = rdr.GetString(4),
                            Treatment = rdr.IsDBNull(5) ? null : rdr.GetString(5)
                        });
                    }
                }
            }

            return list;
        }

        // UPDATE 
        public void UpdateReport(Report r)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE Report SET " +
                "PatientID = @p, DoctorID = @d, ReportDate = @date, Diagnosis = @diag, Treatment = @treat " +
                "WHERE ReportID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@p", r.PatientID);
                cmd.Parameters.AddWithValue("@d", r.DoctorID);
                cmd.Parameters.AddWithValue("@date", r.ReportDate);
                cmd.Parameters.AddWithValue("@diag", r.Diagnosis);
                cmd.Parameters.AddWithValue("@treat", (object)r.Treatment ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@id", r.ReportID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // 5️⃣ DELETE 
        public void DeleteReport(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM Report WHERE ReportID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}