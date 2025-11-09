using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_MamSys_LIB.Model
{
    public class Doctor
    {
        public int DoctorID { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public string Contact { get; set; }
        public int DepartmentID { get; set; }
    }
}
