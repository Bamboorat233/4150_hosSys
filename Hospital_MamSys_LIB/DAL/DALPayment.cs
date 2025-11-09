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
    }
}