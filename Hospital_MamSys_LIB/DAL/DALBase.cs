using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace Hospital_MamSys_LIB.DAL
{
    public class DALBase
    {
        protected string connStr = ConfigurationManager
            .ConnectionStrings["HsConn"].ConnectionString;
    }

}
