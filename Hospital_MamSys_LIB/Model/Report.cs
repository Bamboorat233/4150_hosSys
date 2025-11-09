using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_ManSys_LIB.Models
{
    public class Report
    {
        public int ReportID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime ReportDate { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
    }
}