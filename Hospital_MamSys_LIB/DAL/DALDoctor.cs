using Hospital_MamSys_LIB.Model;
using Hospital_MamSys_LIB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_MamSys_LIB.DAL
{
    internal class DALDoctor : DALBase
    {
        public void AddDoctor(Doctor d)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO Patient(Name, Specialization, Contact, DepartmentID) VALUES(@n, @d, @g, @c, @a)", conn))
            {
                cmd.Parameters.AddWithValue("@n", d.Name);
                cmd.Parameters.AddWithValue("@d", d.Specialization);
                cmd.Parameters.AddWithValue("@g", d.Contact);
                cmd.Parameters.AddWithValue("@c", d.DepartmentID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
