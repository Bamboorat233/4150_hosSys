using Hospital_MamSys_LIB.DAL;
using Hospital_ManSys_LIB.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Hospital_ManSys_LIB.DAL
{
    internal class DALInvoice : DALBase
    {
        public Invoice GetById(int id)
        {
            const string sql = @"SELECT InvoiceID, PatientID, AppointmentID, Amount, DateIssued, Status
                                 FROM dbo.Invoice WHERE InvoiceID=@id";
            using var conn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            using var r = cmd.ExecuteReader();
            if (!r.Read()) throw new InvalidOperationException("Invoice not found.");
            return new Invoice
            {
                InvoiceID = r.GetInt32(0),
                PatientID = r.GetInt32(1),
                AppointmentID = r.GetInt32(2),
                Amount = r.GetDecimal(3),
                DateIssued = r.GetDateTime(4),
                Status = r.GetString(5)
            };
        }

        public Invoice GetByAppointment(int appointmentId)
        {
            const string sql = @"SELECT InvoiceID, PatientID, AppointmentID, Amount, DateIssued, Status
                                 FROM dbo.Invoice WHERE AppointmentID=@aid";
            using var conn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@aid", appointmentId);
            conn.Open();
            using var r = cmd.ExecuteReader();
            if (!r.Read()) throw new InvalidOperationException("Invoice not found for this appointment.");
            return new Invoice
            {
                InvoiceID = r.GetInt32(0),
                PatientID = r.GetInt32(1),
                AppointmentID = r.GetInt32(2),
                Amount = r.GetDecimal(3),
                DateIssued = r.GetDateTime(4),
                Status = r.GetString(5)
            };
        }

        public List<Invoice> GetByPatient(int patientId)
        {
            const string sql = @"SELECT InvoiceID, PatientID, AppointmentID, Amount, DateIssued, Status
                                 FROM dbo.Invoice
                                 WHERE PatientID=@pid
                                 ORDER BY DateIssued DESC";
            var list = new List<Invoice>();
            using var conn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@pid", patientId);
            conn.Open();
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new Invoice
                {
                    InvoiceID = r.GetInt32(0),
                    PatientID = r.GetInt32(1),
                    AppointmentID = r.GetInt32(2),
                    Amount = r.GetDecimal(3),
                    DateIssued = r.GetDateTime(4),
                    Status = r.GetString(5)
                });
            }
            return list;
        }

        public int Insert(Invoice i)
        {
            const string sql = @"INSERT INTO dbo.Invoice(PatientID, AppointmentID, Amount, DateIssued, Status)
                                 OUTPUT INSERTED.InvoiceID
                                 VALUES(@pid, @aid, @amt, @dateIssued, @status)";
            try
            {
                using var conn = new SqlConnection(ConnectionString);
                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@pid", i.PatientID);
                cmd.Parameters.AddWithValue("@aid", i.AppointmentID);
                cmd.Parameters.AddWithValue("@amt", i.Amount);
                cmd.Parameters.AddWithValue("@dateIssued", i.DateIssued);
                cmd.Parameters.AddWithValue("@status", i.Status);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new InvalidOperationException("An invoice already exists for this appointment.", ex);
            }
        }

        public int Update(Invoice i)
        {
            const string sql = @"UPDATE dbo.Invoice
                                 SET PatientID=@pid, AppointmentID=@aid, Amount=@amt, DateIssued=@dateIssued, Status=@status
                                 WHERE InvoiceID=@id";
            try
            {
                using var conn = new SqlConnection(ConnectionString);
                using var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@pid", i.PatientID);
                cmd.Parameters.AddWithValue("@aid", i.AppointmentID);
                cmd.Parameters.AddWithValue("@amt", i.Amount);
                cmd.Parameters.AddWithValue("@dateIssued", i.DateIssued);
                cmd.Parameters.AddWithValue("@status", i.Status);
                cmd.Parameters.AddWithValue("@id", i.InvoiceID);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new InvalidOperationException("Another invoice already uses this appointment.", ex);
            }
        }

        public int UpdateStatus(int invoiceId, string status)
        {
            const string sql = @"UPDATE dbo.Invoice SET Status=@status WHERE InvoiceID=@id";
            using var conn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@id", invoiceId);
            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        public int Delete(int id)
        {
            const string sql = @"DELETE FROM dbo.Invoice WHERE InvoiceID=@id";
            using var conn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            return cmd.ExecuteNonQuery();
        }
    }
}
