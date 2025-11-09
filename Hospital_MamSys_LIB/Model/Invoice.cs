using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital_ManSys_LIB.Model
{
    internal class Invoice
    {
        public int InvoiceID { get; set; }
        public int PatientID { get; set; }
        public int AppointmentID { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateIssued { get; set; }  
        public string Status { get; set; } = "Unpaid";
    }
}
