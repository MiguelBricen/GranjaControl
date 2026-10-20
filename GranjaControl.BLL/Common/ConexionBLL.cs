using Microsoft.Data.SqlClient;

namespace GranjaControl.BLL.Common
{
    public class ConexionBLL
    {
        public async Task<bool> ProbarConexion(string servidor, string baseDatos, string usuario, string password)
        {
            string cadena =
                $"Server={servidor};" +
                $"Database={baseDatos};" +
                $"User Id={usuario};" +
                $"Password={password};" +
                $"TrustServerCertificate=True;";

            try
            {
                using SqlConnection cn = new(cadena);

                await cn.OpenAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
