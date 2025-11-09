using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_ManSys_LIB.Model
{
    internal class Medication
    {
        public int MedID { get; set; }
        public string Name { get; set; } = "";
        public string? Dosage { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
