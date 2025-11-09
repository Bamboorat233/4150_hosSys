using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_ManSys_LIB.Model
{
    internal class Staff
    {
        public int StaffID { get; set; }
        public string Name { get; set; } = "";
        public string Role { get; set; } = "";
        public string Contact { get; set; } = "";
        public int DepartmentID { get; set; }
    }
}
