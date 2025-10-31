using System.Data;
using System.Data.SqlClient;

namespace Hospital_MamSys_LIB
{
    public class DALHospital
    {
        private readonly string _connStr;

        public DALHospital(string connStr)
        {
            _connStr = connStr;
        }

        public DataTable GetAllDepartments()
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                string query = "SELECT * FROM Department";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}
