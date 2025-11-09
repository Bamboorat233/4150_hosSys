using Hospital_ManSys_LIB.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_MamSys_LIB.DAL
{
    internal class DALPrescription : DALBase
    {

        public void AddPrescription(Prescription p)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO Prescription (RecordID, MedID, Quantity) VALUES (@r, @m, @q)", conn))
            {
                cmd.Parameters.AddWithValue("@r", p.RecordID);
                cmd.Parameters.AddWithValue("@m", p.MedID);
                cmd.Parameters.AddWithValue("@q", p.Quantity);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}