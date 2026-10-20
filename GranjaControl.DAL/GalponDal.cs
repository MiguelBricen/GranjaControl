using GranjaControl.DAL.Common;
using GranjaControl.Entitys;
using GranjaControl.Entitys.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GranjaControl.DAL
{
    public class GalponDAL
    {
        #region Métodos Privados

        // Crear objeto galpón
        public async Task<PaginacionEntity<GalponEntity>> MostrarAsync(
            int pagina,
            int tamPagina,
            string? buscar = null)
        {
            try
            {
                PaginacionEntity<GalponEntity> resultado = new();

                using SqlConnection cn = Conexion.ObtenerConexion();

                // Total de registros
                using SqlCommand cmdTotal = new("sp_ContarGalpones", cn);

                cmdTotal.CommandType = CommandType.StoredProcedure;
                cmdTotal.Parameters.AddWithValue(
                    "@buscar",
                    string.IsNullOrWhiteSpace(buscar)
                        ? DBNull.Value
                        : buscar.Trim());

                await cn.OpenAsync();

                resultado.TotalRegistros =
                    Convert.ToInt32(await cmdTotal.ExecuteScalarAsync());

                // Mostrar registros
                using SqlCommand cmd = new("sp_MostrarGalpones", cn);

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
                {
                    GalponEntity galpon = new();

                    galpon.AsignarId(Convert.ToInt32(dr["id"]));
                    galpon.CambiarNombre(dr["nombreGalpon"].ToString()!);
                    galpon.CambiarCapacidad(Convert.ToInt32(dr["capacidad"]));

                    galpon.CambiarDescripcion(
                        dr["descripcion"] == DBNull.Value
                            ? null
                            : dr["descripcion"].ToString());

                    galpon.CambiarActivo(Convert.ToBoolean(dr["activo"]));

                    resultado.Registros.Add(galpon);
                }

                resultado.PaginaActual = pagina;
                resultado.TamPagina = tamPagina;

                return resultado;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<List<GalponEntity>> MostrarGalponesActivosAsync()
        {
            try
            {
                List<GalponEntity> lista = [];

                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_MostrarGalponesActivos", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                while (await dr.ReadAsync())
                {
                    GalponEntity galpon = new();

                    galpon.AsignarId(Convert.ToInt32(dr["id"]));
                    galpon.CambiarNombre(dr["nombreGalpon"].ToString()!);
                    galpon.CambiarCapacidad(Convert.ToInt32(dr["capacidad"]));

                    galpon.CambiarDescripcion(
                        dr["descripcion"] == DBNull.Value
                            ? null
                            : dr["descripcion"].ToString());

                    lista.Add(galpon);
                }

                return lista;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<GalponEntity?> ObtenerPorIdAsync(int id)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_ObtenerGalponPorId", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                if (!await dr.ReadAsync())
                    return null;

                GalponEntity galpon = new();

                galpon.AsignarId(Convert.ToInt32(dr["id"]));
                galpon.CambiarNombre(dr["nombreGalpon"].ToString()!);
                galpon.CambiarCapacidad(Convert.ToInt32(dr["capacidad"]));

                galpon.CambiarDescripcion(
                    dr["descripcion"] == DBNull.Value
                        ? null
                        : dr["descripcion"].ToString());

                galpon.CambiarActivo(Convert.ToBoolean(dr["activo"]));

                return galpon;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task AgregarAsync(GalponEntity galpon, string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_AgregarGalpon", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@nombreGalpon", galpon.NombreGalpon);

                cmd.Parameters.AddWithValue(
                    "@capacidad", galpon.Capacidad);

                cmd.Parameters.AddWithValue(
                    "@descripcion",
                    string.IsNullOrWhiteSpace(galpon.Descripcion)
                        ? DBNull.Value
                        : galpon.Descripcion.Trim());

                cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

                await cn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task ActualizarAsync(GalponEntity galpon, string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_ActualizarGalpon", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", galpon.Id);

                cmd.Parameters.AddWithValue(
                    "@nombreGalpon", galpon.NombreGalpon);

                cmd.Parameters.AddWithValue(
                    "@capacidad", galpon.Capacidad);

                cmd.Parameters.AddWithValue(
                    "@descripcion",
                    string.IsNullOrWhiteSpace(galpon.Descripcion)
                        ? DBNull.Value
                        : galpon.Descripcion.Trim());

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
                using SqlCommand cmd = new("sp_CambiarEstadoGalpon", cn);

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

        #endregion
    }
}


