using GranjaControl.DAL.Common;
using GranjaControl.Entitys.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GranjaControl.DAL
{
    public class TarifaPolloDAL
    {
        public async Task<PaginacionEntity<TarifaPolloEntity>> MostrarAsync(
            int pagina,
            int tamPagina)
        {
            try
            {
                PaginacionEntity<TarifaPolloEntity> resultado = new();

                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmdTotal =
                    new("sp_ContarTarifasPollo", cn);

                cmdTotal.CommandType = CommandType.StoredProcedure;

                await cn.OpenAsync();

                resultado.TotalRegistros =
                    Convert.ToInt32(await cmdTotal.ExecuteScalarAsync());

                using SqlCommand cmd =
                    new("sp_MostrarTarifasPollo", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pagina", pagina);
                cmd.Parameters.AddWithValue("@tamPagina", tamPagina);

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                while (await dr.ReadAsync())
                    resultado.Registros.Add(CrearTarifa(dr));

                resultado.PaginaActual = pagina;
                resultado.TamPagina = tamPagina;

                return resultado;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<List<TarifaPolloEntity>> MostrarActivasAsync()
        {
            try
            {
                List<TarifaPolloEntity> lista = [];

                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmd =
                    new("sp_MostrarTarifasPolloActivas", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                while (await dr.ReadAsync())
                    lista.Add(CrearTarifa(dr));

                return lista;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<TarifaPolloEntity?> ObtenerPorIdAsync(int id)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmd =
                    new("sp_ObtenerTarifaPolloPorId", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                if (!await dr.ReadAsync())
                    return null;

                return CrearTarifa(dr);
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<int> AgregarAsync(
            TarifaPolloEntity tarifa,
            string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmd =
                    new("sp_AgregarTarifaPollo", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@precioPorLibra",
                    tarifa.PrecioPorLibra);

                cmd.Parameters.AddWithValue(
                    "@fechaInicio",
                    tarifa.FechaInicio);

                cmd.Parameters.AddWithValue(
                    "@fechaFin",
                    tarifa.FechaFin.HasValue
                        ? tarifa.FechaFin.Value
                        : DBNull.Value);

                cmd.Parameters.AddWithValue("@activo", tarifa.Activo);
                cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

                await cn.OpenAsync();

                object? resultado = await cmd.ExecuteScalarAsync();

                return resultado is null || resultado == DBNull.Value
                    ? 0
                    : Convert.ToInt32(resultado);
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task ActualizarAsync(
            TarifaPolloEntity tarifa,
            string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmd =
                    new("sp_ActualizarTarifaPollo", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", tarifa.Id);

                cmd.Parameters.AddWithValue(
                    "@precioPorLibra",
                    tarifa.PrecioPorLibra);

                cmd.Parameters.AddWithValue(
                    "@fechaInicio",
                    tarifa.FechaInicio);

                cmd.Parameters.AddWithValue(
                    "@fechaFin",
                    tarifa.FechaFin.HasValue
                        ? tarifa.FechaFin.Value
                        : DBNull.Value);

                cmd.Parameters.AddWithValue("@activo", tarifa.Activo);
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

                using SqlCommand cmd =
                    new("sp_CambiarEstadoTarifaPollo", cn);

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

        private static TarifaPolloEntity CrearTarifa(SqlDataReader dr)
        {
            TarifaPolloEntity tarifa = new();

            tarifa.AsignarId(Convert.ToInt32(dr["id"]));
            tarifa.CambiarPrecio(Convert.ToDecimal(dr["precioPorLibra"]));
            tarifa.CambiarFechaInicio(Convert.ToDateTime(dr["fechaInicio"]));

            tarifa.CambiarFechaFin(
                dr["fechaFin"] == DBNull.Value
                    ? null
                    : Convert.ToDateTime(dr["fechaFin"]));

            tarifa.CambiarActivo(Convert.ToBoolean(dr["activo"]));

            return tarifa;
        }
    }
}
