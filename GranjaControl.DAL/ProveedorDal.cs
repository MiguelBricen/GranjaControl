using GranjaControl.DAL.Common;
using GranjaControl.Entitys.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GranjaControl.DAL
{
    public class ProveedorDAL
    {
        public async Task<PaginacionEntity<ProveedorEntity>> MostrarAsync(
            int pagina,
            int tamPagina,
            string? buscar = null)
        {
            try
            {
                PaginacionEntity<ProveedorEntity> resultado = new();

                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmdTotal = new("sp_ContarProveedores", cn);

                cmdTotal.CommandType = CommandType.StoredProcedure;
                cmdTotal.Parameters.AddWithValue(
                    "@buscar",
                    string.IsNullOrWhiteSpace(buscar)
                        ? DBNull.Value
                        : buscar.Trim());

                await cn.OpenAsync();

                resultado.TotalRegistros =
                    Convert.ToInt32(await cmdTotal.ExecuteScalarAsync());

                using SqlCommand cmd = new("sp_MostrarProveedores", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(
                    "@buscar",
                    string.IsNullOrWhiteSpace(buscar)
                        ? DBNull.Value
                        : buscar.Trim());

                cmd.Parameters.AddWithValue("@pagina", pagina);
                cmd.Parameters.AddWithValue("@tamPagina", tamPagina);

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                while (await dr.ReadAsync())
                    resultado.Registros.Add(CrearProveedor(dr));

                resultado.PaginaActual = pagina;
                resultado.TamPagina = tamPagina;

                return resultado;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<List<ProveedorEntity>> MostrarProveedoresActivosAsync()
        {
            try
            {
                List<ProveedorEntity> lista = [];

                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_MostrarProveedoresActivos", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                while (await dr.ReadAsync())
                {
                    ProveedorEntity proveedor = CrearProveedor(dr, false);
                    proveedor.CambiarActivo(true);

                    lista.Add(proveedor);
                }

                return lista;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<ProveedorEntity?> ObtenerPorIdAsync(int id)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_ObtenerProveedorPorId", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                if (!await dr.ReadAsync())
                    return null;

                return CrearProveedor(dr);
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task AgregarAsync(ProveedorEntity proveedor, string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_AgregarProveedor", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@nombreProveedor", proveedor.NombreProveedor);

                cmd.Parameters.AddWithValue(
                    "@telefono",
                    string.IsNullOrWhiteSpace(proveedor.Telefono)
                        ? DBNull.Value
                        : proveedor.Telefono);

                cmd.Parameters.AddWithValue(
                    "@correoElectronico",
                    string.IsNullOrWhiteSpace(proveedor.CorreoElectronico)
                        ? DBNull.Value
                        : proveedor.CorreoElectronico);

                cmd.Parameters.AddWithValue(
                    "@direccion",
                    string.IsNullOrWhiteSpace(proveedor.Direccion)
                        ? DBNull.Value
                        : proveedor.Direccion);

                cmd.Parameters.AddWithValue(
                    "@tipoProveedor", proveedor.TipoProveedor);

                cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

                await cn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task ActualizarAsync(ProveedorEntity proveedor, string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_ActualizarProveedor", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", proveedor.Id);

                cmd.Parameters.AddWithValue(
                    "@nombreProveedor", proveedor.NombreProveedor);

                cmd.Parameters.AddWithValue(
                    "@telefono",
                    string.IsNullOrWhiteSpace(proveedor.Telefono)
                        ? DBNull.Value
                        : proveedor.Telefono);

                cmd.Parameters.AddWithValue(
                    "@correoElectronico",
                    string.IsNullOrWhiteSpace(proveedor.CorreoElectronico)
                        ? DBNull.Value
                        : proveedor.CorreoElectronico);

                cmd.Parameters.AddWithValue(
                    "@direccion",
                    string.IsNullOrWhiteSpace(proveedor.Direccion)
                        ? DBNull.Value
                        : proveedor.Direccion);

                cmd.Parameters.AddWithValue(
                    "@tipoProveedor", proveedor.TipoProveedor);

                cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

                await cn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task CambiarEstadoAsync(
            int id,
            bool activo,
            string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_CambiarEstadoProveedor", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@activo", activo);
                cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

                await cn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        private static ProveedorEntity CrearProveedor(
            SqlDataReader dr,
            bool incluirActivo = true)
        {
            ProveedorEntity proveedor = new();

            proveedor.AsignarId(Convert.ToInt32(dr["id"]));
            proveedor.CambiarNombre(dr["nombreProveedor"].ToString()!);

            proveedor.CambiarTelefono(
                dr["telefono"] == DBNull.Value
                    ? null
                    : dr["telefono"].ToString());

            proveedor.CambiarCorreo(
                dr["correoElectronico"] == DBNull.Value
                    ? null
                    : dr["correoElectronico"].ToString());

            proveedor.CambiarDireccion(
                dr["direccion"] == DBNull.Value
                    ? null
                    : dr["direccion"].ToString());

            proveedor.CambiarTipoProveedor(
                dr["tipoProveedor"].ToString()!);

            if (incluirActivo)
                proveedor.CambiarActivo(Convert.ToBoolean(dr["activo"]));

            return proveedor;
        }
    }
}
