using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDApp
{
    public static class ConnectionString
    {
        public static string MsSqlConnection => @"Server = DESKTOP-BQ6LNRN\SQLEXPRESS; TrustServerCertificate = true; Database = testing; Trusted_Connection=True";
    }
}
