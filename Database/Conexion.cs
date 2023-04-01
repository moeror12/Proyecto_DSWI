using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_DSWI.Database
{
    public static class Conexion
    {
        public static SqlConnection GetConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["BDCONEXION"].ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}
