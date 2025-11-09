using Hospital_MamSys_LIB.DAL;
using Hospital_MamSys_LIB.Model;
using Hospital_ManSys_LIB.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_MamSys_LIB.DAL
{
    internal class DALAppointment : DALBase
    {
        // 1) CREATE
        public void AddAppointment(Appointment a)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO Appointment (PatientID, DoctorID, AppointmentDate, AppointmentTime, Status) " +
                "VALUES (@p, @d, @date, @time, @s)", conn))
            {
                cmd.Parameters.AddWithValue("@p", a.PatientID);
                cmd.Parameters.AddWithValue("@d", a.DoctorID);
                cmd.Parameters.AddWithValue("@date", a.AppointmentDate);
                cmd.Parameters.AddWithValue("@time", a.AppointmentTime);
                cmd.Parameters.AddWithValue("@s", a.Status);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // 2) READ - get one by ID
        public Appointment GetAppointmentById(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT AppointmentID, PatientID, DoctorID, AppointmentDate, AppointmentTime, Status " +
                "FROM Appointment WHERE AppointmentID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return new Appointment
                        {
                            AppointmentID = rdr.GetInt32(0),
                            PatientID = rdr.GetInt32(1),
                            DoctorID = rdr.GetInt32(2),
                            AppointmentDate = rdr.GetDateTime(3),
                            AppointmentTime = (TimeSpan)rdr["AppointmentTime"],
                            Status = rdr.GetString(5)
                        };
                    }
                }
            }

            return null;    
        }

        // 3) READ - get all
        public List<Appointment> GetAllAppointments()
        {
            List<Appointment> list = new List<Appointment>();

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT AppointmentID, PatientID, DoctorID, AppointmentDate, AppointmentTime, Status FROM Appointment",
                conn))
            {
                conn.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new Appointment
                        {
                            AppointmentID = rdr.GetInt32(0),
                            PatientID = rdr.GetInt32(1),
                            DoctorID = rdr.GetInt32(2),
                            AppointmentDate = rdr.GetDateTime(3),
                            AppointmentTime = (TimeSpan)rdr["AppointmentTime"],
                            Status = rdr.GetString(5)
                        });
                    }
                }
            }

            return list;
        }

        // 4) UPDATE
        public void UpdateAppointment(Appointment a)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE Appointment SET " +
                "PatientID = @p, DoctorID = @d, AppointmentDate = @date, AppointmentTime = @time, Status = @s " +
                "WHERE AppointmentID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@p", a.PatientID);
                cmd.Parameters.AddWithValue("@d", a.DoctorID);
                cmd.Parameters.AddWithValue("@date", a.AppointmentDate);
                cmd.Parameters.AddWithValue("@time", a.AppointmentTime);
                cmd.Parameters.AddWithValue("@s", a.Status);
                cmd.Parameters.AddWithValue("@id", a.AppointmentID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // 5) DELETE
        public void DeleteAppointment(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM Appointment WHERE AppointmentID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}