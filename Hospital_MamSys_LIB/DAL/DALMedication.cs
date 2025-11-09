using Hospital_MamSys_LIB.DAL;
using Hospital_ManSys_LIB.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Hospital_ManSys_LIB.DAL
{
    internal class DALMedication : DALBase
    {
        public Medication? GetById(int id)
        {
            const string sql = @"SELECT MedID, Name, Dosage, Price, Quantity
                                 FROM dbo.Medication WHERE MedID=@id";
            using var conn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            using var r = cmd.ExecuteReader();
            if (!r.Read()) return null;
            return new Medication
            {
                MedID = r.GetInt32(0),
                Name = r.GetString(1),
                Dosage = r.IsDBNull(2) ? null : r.GetString(2),
                Price = r.GetDecimal(3),
                Quantity = r.GetInt32(4)
            };
        }

        public List<Medication> GetAll()
        {
            const string sql = @"SELECT MedID, Name, Dosage, Price, Quantity
                                 FROM dbo.Medication ORDER BY Name";
            var list = new List<Medication>();
            using var conn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(sql, conn);
            conn.Open();
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new Medication
                {
                    MedID = r.GetInt32(0),
                    Name = r.GetString(1),
                    Dosage = r.IsDBNull(2) ? null : r.GetString(2),
                    Price = r.GetDecimal(3),
                    Quantity = r.GetInt32(4)
                });
            }
            return list;
        }

        public int Insert(Medication m)
        {
            const string sql = @"INSERT INTO dbo.Medication(Name, Dosage, Price, Quantity)
                                 OUTPUT INSERTED.MedID
                                 VALUES(@name, @dosage, @price, @qty)";
            using var conn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", m.Name);
            cmd.Parameters.AddWithValue("@dosage", (object?)m.Dosage ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@price", m.Price);
            cmd.Parameters.AddWithValue("@qty", m.Quantity);
            conn.Open();
            return (int)cmd.ExecuteScalar()!;
        }

        public int Update(Medication m)
        {
            const string sql = @"UPDATE dbo.Medication
                                 SET Name=@name, Dosage=@dosage, Price=@price, Quantity=@qty
                                 WHERE MedID=@id";
            using var conn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", m.Name);
            cmd.Parameters.AddWithValue("@dosage", (object?)m.Dosage ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@price", m.Price);
            cmd.Parameters.AddWithValue("@qty", m.Quantity);
            cmd.Parameters.AddWithValue("@id", m.MedID);
            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        public int Delete(int id)
        {
            const string sql = @"DELETE FROM dbo.Medication WHERE MedID=@id";
            using var conn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            return cmd.ExecuteNonQuery();
        }
    }
}