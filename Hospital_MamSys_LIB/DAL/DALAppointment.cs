using Hospital_MamSys_LIB.DAL;
using Hospital_MamSys_LIB.Model;
using Hospital_ManSys_LIB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_ManSys_LIB.DAL
{
    internal class DALAppointment : DALBase
    {
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
    }
}