using Hospital_MamSys_LIB.DAL;
using Hospital_ManSys_LIB.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Hospital_ManSys_LIB.DAL
{
    internal class DALMedicalRecord : DALBase
    {
        public MedicalRecord GetById(int id)
        {
            const string sql = @"SELECT RecordID, PatientID, DoctorID, Diagnosis, Treatment, VisitDate
                                 FROM dbo.MedicalRecord WHERE RecordID=@id";

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                using (var r = cmd.ExecuteReader())
                {
                    if (!r.Read())
                        throw new InvalidOperationException("Medical record not found.");

                    return new MedicalRecord
                    {
                        RecordID = r.GetInt32(0),
                        PatientID = r.GetInt32(1),
                        DoctorID = r.GetInt32(2),
                        Diagnosis = r.IsDBNull(3) ? "" : r.GetString(3),
                        Treatment = r.IsDBNull(4) ? "" : r.GetString(4),
                        VisitDate = r.GetDateTime(5)
                    };
                }
            }
        }

        public List<MedicalRecord> GetByPatient(int patientId)
        {
            const string sql = @"SELECT RecordID, PatientID, DoctorID, Diagnosis, Treatment, VisitDate
                                 FROM dbo.MedicalRecord
                                 WHERE PatientID=@pid
                                 ORDER BY VisitDate DESC";

            var list = new List<MedicalRecord>();

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@pid", patientId);
                conn.Open();

                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new MedicalRecord
                        {
                            RecordID = r.GetInt32(0),
                            PatientID = r.GetInt32(1),
                            DoctorID = r.GetInt32(2),
                            Diagnosis = r.IsDBNull(3) ? "" : r.GetString(3),
                            Treatment = r.IsDBNull(4) ? "" : r.GetString(4),
                            VisitDate = r.GetDateTime(5)
                        });
                    }
                }
            }

            return list;
        }

        public List<MedicalRecord> GetByDoctor(int doctorId)
        {
            const string sql = @"SELECT RecordID, PatientID, DoctorID, Diagnosis, Treatment, VisitDate
                                 FROM dbo.MedicalRecord
                                 WHERE DoctorID=@did
                                 ORDER BY VisitDate DESC";

            var list = new List<MedicalRecord>();

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@did", doctorId);
                conn.Open();

                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new MedicalRecord
                        {
                            RecordID = r.GetInt32(0),
                            PatientID = r.GetInt32(1),
                            DoctorID = r.GetInt32(2),
                            Diagnosis = r.IsDBNull(3) ? "" : r.GetString(3),
                            Treatment = r.IsDBNull(4) ? "" : r.GetString(4),
                            VisitDate = r.GetDateTime(5)
                        });
                    }
                }
            }

            return list;
        }

        public int Insert(MedicalRecord m)
        {
            const string sql = @"INSERT INTO dbo.MedicalRecord(PatientID, DoctorID, Diagnosis, Treatment, VisitDate)
                                 OUTPUT INSERTED.RecordID
                                 VALUES(@pid, @did, @diag, @treat, @visit)";

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@pid", m.PatientID);
                cmd.Parameters.AddWithValue("@did", m.DoctorID);
                cmd.Parameters.AddWithValue("@diag", m.Diagnosis);
                cmd.Parameters.AddWithValue("@treat", m.Treatment);
                cmd.Parameters.AddWithValue("@visit", m.VisitDate);

                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public int Update(MedicalRecord m)
        {
            const string sql = @"UPDATE dbo.MedicalRecord
                                 SET PatientID=@pid, DoctorID=@did, Diagnosis=@diag, Treatment=@treat, VisitDate=@visit
                                 WHERE RecordID=@id";

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@pid", m.PatientID);
                cmd.Parameters.AddWithValue("@did", m.DoctorID);
                cmd.Parameters.AddWithValue("@diag", m.Diagnosis);
                cmd.Parameters.AddWithValue("@treat", m.Treatment);
                cmd.Parameters.AddWithValue("@visit", m.VisitDate);
                cmd.Parameters.AddWithValue("@id", m.RecordID);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public int Delete(int id)
        {
            const string sql = @"DELETE FROM dbo.MedicalRecord WHERE RecordID=@id";

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
