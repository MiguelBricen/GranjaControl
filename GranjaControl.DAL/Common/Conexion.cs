using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace GranjaControl.DAL.Common
{
    public class Conexion
    {
        public static string CadenaConexion { get; set; } = string.Empty;

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(CadenaConexion);
        }
    }
}
