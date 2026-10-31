using GranjaControl.DAL.Common;
using GranjaControl.Entitys.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GranjaControl.DAL
{
    public class VitaminaDAL
    {
        public async Task<PaginacionEntity<VitaminaEntity>> MostrarAsync(
            int pagina,
            int tamPagina,
            string? buscar = null)
        {
            try
            {
                PaginacionEntity<VitaminaEntity> resultado = new();

                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmdTotal = new("sp_ContarVitaminas", cn);

                cmdTotal.CommandType = CommandType.StoredProcedure;
                cmdTotal.Parameters.AddWithValue(
                    "@buscar",
                    string.IsNullOrWhiteSpace(buscar)
                        ? DBNull.Value
                        : buscar.Trim());

                await cn.OpenAsync();

                resultado.TotalRegistros =
                    Convert.ToInt32(await cmdTotal.ExecuteScalarAsync());

                using SqlCommand cmd = new("sp_MostrarVitaminas", cn);

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
                    resultado.Registros.Add(CrearVitamina(dr));

                resultado.PaginaActual = pagina;
                resultado.TamPagina = tamPagina;

                return resultado;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<List<VitaminaEntity>> MostrarVitaminasActivasAsync()
        {
            try
            {
                List<VitaminaEntity> lista = [];

                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_MostrarVitaminasActivas", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                while (await dr.ReadAsync())
                {
                    VitaminaEntity vitamina = CrearVitamina(dr, false);
                    vitamina.CambiarActivo(true);

                    lista.Add(vitamina);
                }

                return lista;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<VitaminaEntity?> ObtenerPorIdAsync(int id)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_ObtenerVitaminaPorId", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                if (!await dr.ReadAsync())
                    return null;

                return CrearVitamina(dr);
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task AgregarAsync(VitaminaEntity vitamina, string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_AgregarVitamina", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@nombreVitamina",
                    vitamina.NombreVitamina);

                cmd.Parameters.AddWithValue(
                    "@descripcion",
                    string.IsNullOrWhiteSpace(vitamina.Descripcion)
                        ? DBNull.Value
                        : vitamina.Descripcion);

                cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

                await cn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task ActualizarAsync(VitaminaEntity vitamina, string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_ActualizarVitamina", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", vitamina.Id);

                cmd.Parameters.AddWithValue(
                    "@nombreVitamina",
                    vitamina.NombreVitamina);

                cmd.Parameters.AddWithValue(
                    "@descripcion",
                    string.IsNullOrWhiteSpace(vitamina.Descripcion)
                        ? DBNull.Value
                        : vitamina.Descripcion);

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
                using SqlCommand cmd = new("sp_CambiarEstadoVitamina", cn);

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

        private static VitaminaEntity CrearVitamina(
            SqlDataReader dr,
            bool incluirActivo = true)
        {
            VitaminaEntity vitamina = new();

            vitamina.AsignarId(Convert.ToInt32(dr["id"]));
            vitamina.CambiarNombre(dr["nombreVitamina"].ToString()!);

            vitamina.CambiarDescripcion(
                dr["descripcion"] == DBNull.Value
                    ? null
                    : dr["descripcion"].ToString());

            if (incluirActivo)
                vitamina.CambiarActivo(Convert.ToBoolean(dr["activo"]));

            return vitamina;
        }
    }
}
