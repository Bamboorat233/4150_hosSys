using Hospital_ManSys_LIB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_MamSys_LIB.DAL
{
    internal class DALReport : DALBase
    {
        // 插入一条新的报告记录
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
    }
}