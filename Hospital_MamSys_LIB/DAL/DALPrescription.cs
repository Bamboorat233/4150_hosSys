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
        // CREATE
        public void AddPrescription(Prescription p)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO Prescription (RecordID, MedID, Quantity) " +
                "VALUES (@r, @m, @q)", conn))
            {
                cmd.Parameters.AddWithValue("@r", p.RecordID);
                cmd.Parameters.AddWithValue("@m", p.MedID);
                cmd.Parameters.AddWithValue("@q", p.Quantity);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // READ 
        public Prescription GetPrescription(int recordId, int medId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT RecordID, MedID, Quantity " +
                "FROM Prescription WHERE RecordID = @r AND MedID = @m", conn))
            {
                cmd.Parameters.AddWithValue("@r", recordId);
                cmd.Parameters.AddWithValue("@m", medId);

                conn.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return new Prescription
                        {
                            RecordID = rdr.GetInt32(0),
                            MedID = rdr.GetInt32(1),
                            Quantity = rdr.GetInt32(2)
                        };
                    }
                }
            }

            return null;
        }

        // READ 
        public List<Prescription> GetAllPrescriptions()
        {
            List<Prescription> list = new List<Prescription>();

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT RecordID, MedID, Quantity FROM Prescription", conn))
            {
                conn.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new Prescription
                        {
                            RecordID = rdr.GetInt32(0),
                            MedID = rdr.GetInt32(1),
                            Quantity = rdr.GetInt32(2)
                        });
                    }
                }
            }

            return list;
        }

        // UPDATE
        public void UpdatePrescription(Prescription p)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE Prescription SET Quantity = @q " +
                "WHERE RecordID = @r AND MedID = @m", conn))
            {
                cmd.Parameters.AddWithValue("@q", p.Quantity);
                cmd.Parameters.AddWithValue("@r", p.RecordID);
                cmd.Parameters.AddWithValue("@m", p.MedID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // DELETE 
        public void DeletePrescription(int recordId, int medId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM Prescription WHERE RecordID = @r AND MedID = @m", conn))
            {
                cmd.Parameters.AddWithValue("@r", recordId);
                cmd.Parameters.AddWithValue("@m", medId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}