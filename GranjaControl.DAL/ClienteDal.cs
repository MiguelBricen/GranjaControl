using GranjaControl.DAL.Common;
using GranjaControl.Entitys.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GranjaControl.DAL
{
    public class ClienteDAL
    {
        public async Task<PaginacionEntity<ClienteEntity>> MostrarAsync(
            int pagina,
            int tamPagina,
            string? buscar = null)
        {
            try
            {
                PaginacionEntity<ClienteEntity> resultado = new();

                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmdTotal = new("sp_ContarClientes", cn);

                cmdTotal.CommandType = CommandType.StoredProcedure;
                cmdTotal.Parameters.AddWithValue(
                    "@buscar",
                    string.IsNullOrWhiteSpace(buscar)
                        ? DBNull.Value
                        : buscar.Trim());

                await cn.OpenAsync();

                resultado.TotalRegistros =
                    Convert.ToInt32(await cmdTotal.ExecuteScalarAsync());

                using SqlCommand cmd = new("sp_MostrarClientes", cn);

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
                    resultado.Registros.Add(CrearCliente(dr));

                resultado.PaginaActual = pagina;
                resultado.TamPagina = tamPagina;

                return resultado;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<List<ClienteEntity>> MostrarClientesActivosAsync()
        {
            try
            {
                List<ClienteEntity> lista = [];

                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_MostrarClientesActivos", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                while (await dr.ReadAsync())
                    lista.Add(CrearCliente(dr));

                return lista;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<ClienteEntity?> ObtenerPorIdAsync(int id)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_ObtenerClientePorId", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                if (!await dr.ReadAsync())
                    return null;

                return CrearCliente(dr);
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task AgregarAsync(ClienteEntity cliente, string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_AgregarCliente", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@nombreCliente",
                    cliente.NombreCliente);

                cmd.Parameters.AddWithValue(
                    "@telefono",
                    string.IsNullOrWhiteSpace(cliente.Telefono)
                        ? DBNull.Value
                        : cliente.Telefono);

                cmd.Parameters.AddWithValue(
                    "@direccion",
                    string.IsNullOrWhiteSpace(cliente.Direccion)
                        ? DBNull.Value
                        : cliente.Direccion);

                cmd.Parameters.AddWithValue(
                    "@tipoCliente",
                    cliente.TipoCliente);

                cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

                await cn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task ActualizarAsync(ClienteEntity cliente, string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_ActualizarCliente", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", cliente.Id);

                cmd.Parameters.AddWithValue(
                    "@nombreCliente",
                    cliente.NombreCliente);

                cmd.Parameters.AddWithValue(
                    "@telefono",
                    string.IsNullOrWhiteSpace(cliente.Telefono)
                        ? DBNull.Value
                        : cliente.Telefono);

                cmd.Parameters.AddWithValue(
                    "@direccion",
                    string.IsNullOrWhiteSpace(cliente.Direccion)
                        ? DBNull.Value
                        : cliente.Direccion);

                cmd.Parameters.AddWithValue(
                    "@tipoCliente",
                    cliente.TipoCliente);

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
                using SqlCommand cmd = new("sp_CambiarEstadoCliente", cn);

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

        private static ClienteEntity CrearCliente(SqlDataReader dr)
        {
            ClienteEntity cliente = new();

            cliente.AsignarId(Convert.ToInt32(dr["id"]));
            cliente.CambiarNombre(dr["nombreCliente"].ToString()!);

            cliente.CambiarTelefono(
                dr["telefono"] == DBNull.Value
                    ? null
                    : dr["telefono"].ToString());

            cliente.CambiarDireccion(
                dr["direccion"] == DBNull.Value
                    ? null
                    : dr["direccion"].ToString());

            cliente.CambiarTipoCliente(
                dr["tipoCliente"].ToString()!);

            cliente.CambiarActivo(Convert.ToBoolean(dr["activo"]));

            return cliente;
        }
    }
}
