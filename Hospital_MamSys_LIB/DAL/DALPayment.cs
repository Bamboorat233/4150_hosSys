using Hospital_MamSys_LIB.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_MamSys_LIB.DAL
{
    internal class DALPayment : DALBase
    {
        // CREATE
        public void AddPayment(Payment p)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO Payment (InvoiceID, PaymentDate, Amount, PaymentMethod) " +
                "VALUES (@i, @date, @amt, @method)", conn))
            {
                cmd.Parameters.AddWithValue("@i", p.InvoiceID);
                cmd.Parameters.AddWithValue("@date", p.PaymentDate);
                cmd.Parameters.AddWithValue("@amt", p.Amount);
                cmd.Parameters.AddWithValue("@method", p.PaymentMethod);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // READ - get one
        public Payment GetPaymentById(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT PaymentID, InvoiceID, PaymentDate, Amount, PaymentMethod " +
                "FROM Payment WHERE PaymentID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return new Payment
                        {
                            PaymentID = rdr.GetInt32(0),
                            InvoiceID = rdr.GetInt32(1),
                            PaymentDate = rdr.GetDateTime(2),
                            Amount = rdr.GetDecimal(3),
                            PaymentMethod = rdr.GetString(4)
                        };
                    }
                }
            }

            return null;
        }

        // READ
        public List<Payment> GetAllPayments()
        {
            List<Payment> list = new List<Payment>();

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT PaymentID, InvoiceID, PaymentDate, Amount, PaymentMethod FROM Payment", conn))
            {
                conn.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new Payment
                        {
                            PaymentID = rdr.GetInt32(0),
                            InvoiceID = rdr.GetInt32(1),
                            PaymentDate = rdr.GetDateTime(2),
                            Amount = rdr.GetDecimal(3),
                            PaymentMethod = rdr.GetString(4)
                        });
                    }
                }
            }

            return list;
        }

        // UPDATE
        public void UpdatePayment(Payment p)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE Payment SET " +
                "InvoiceID = @i, PaymentDate = @date, Amount = @amt, PaymentMethod = @method " +
                "WHERE PaymentID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@i", p.InvoiceID);
                cmd.Parameters.AddWithValue("@date", p.PaymentDate);
                cmd.Parameters.AddWithValue("@amt", p.Amount);
                cmd.Parameters.AddWithValue("@method", p.PaymentMethod);
                cmd.Parameters.AddWithValue("@id", p.PaymentID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // DELETE
        public void DeletePayment(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM Payment WHERE PaymentID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}