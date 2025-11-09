using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_ManSys_LIB.Model
{
    internal class Prescription
    {
        public int RecordID { get; set; }
        public int MedID { get; set; }
        public int Quantity { get; set; }   // number (>0)
    }
}