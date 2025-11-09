using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_ManSys_LIB.Model
{
    internal class Department
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; } = "";
        public string? Location { get; set; }
    }
}
