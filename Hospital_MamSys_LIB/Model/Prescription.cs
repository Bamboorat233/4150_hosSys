using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_MamSys_LIB.Model
{
    public class Prescription
    {
        public int RecordID { get; set; }
        public int MedID { get; set; }
        public int Quantity { get; set; }   // number (>0)
    }
}