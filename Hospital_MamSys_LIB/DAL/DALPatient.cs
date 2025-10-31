using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Hospital_MamSys_LIB.Models;

namespace Hospital_MamSys_LIB.DAL
{
    internal class DALPatient : DALBase
    {
        public void AddPatient(Patient p)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO Patient(Name, DOB, Gender, Contact, Address) VALUES(@n, @d, @g, @c, @a)", conn))
            {
                cmd.Parameters.AddWithValue("@n", p.Name);
                cmd.Parameters.AddWithValue("@d", p.DOB);
                cmd.Parameters.AddWithValue("@g", p.Gender);
                cmd.Parameters.AddWithValue("@c", p.Contact);
                cmd.Parameters.AddWithValue("@a", p.Address);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
