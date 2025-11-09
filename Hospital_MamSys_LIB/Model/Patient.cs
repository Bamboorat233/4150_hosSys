using System;

namespace Hospital_MamSys_LIB.Model
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
    }
}
