using GranjaControl.DAL.Common;
using GranjaControl.Entitys.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GranjaControl.DAL
{
    public class VitaminizacionDAL
    {
        public async Task<PaginacionEntity<VitaminizacionEntity>> MostrarAsync(
            int pagina,
            int tamPagina,
            int? idLote = null)
        {
            try
            {
                PaginacionEntity<VitaminizacionEntity> resultado = new();

                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmdTotal =
                    new("sp_ContarAplicacionesVitaminas", cn);

                cmdTotal.CommandType = CommandType.StoredProcedure;
                cmdTotal.Parameters.AddWithValue(
                    "@idLote",
                    idLote.HasValue ? idLote.Value : DBNull.Value);

                await cn.OpenAsync();

                resultado.TotalRegistros =
                    Convert.ToInt32(await cmdTotal.ExecuteScalarAsync());

                using SqlCommand cmd =
                    new("sp_MostrarAplicacionesVitaminas", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(
                    "@idLote",
                    idLote.HasValue ? idLote.Value : DBNull.Value);

                cmd.Parameters.AddWithValue("@pagina", pagina);
                cmd.Parameters.AddWithValue("@tamPagina", tamPagina);

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                while (await dr.ReadAsync())
                    resultado.Registros.Add(CrearVitaminizacion(dr));

                resultado.PaginaActual = pagina;
                resultado.TamPagina = tamPagina;

                return resultado;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<VitaminizacionEntity?> ObtenerPorIdAsync(int id)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmd =
                    new("sp_ObtenerAplicacionVitaminaPorId", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                if (!await dr.ReadAsync())
                    return null;

                return CrearVitaminizacion(dr);
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task AgregarAsync(
            VitaminizacionEntity vitaminizacion,
            string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd =
                    new("sp_AgregarAplicacionVitamina", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                AgregarParametros(vitaminizacion, cmd);
                cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

                await cn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task ActualizarAsync(
            VitaminizacionEntity vitaminizacion,
            string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd =
                    new("sp_ActualizarAplicacionVitamina", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@Id",
                    vitaminizacion.Id);

                AgregarParametros(vitaminizacion, cmd);
                cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

                await cn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        private static void AgregarParametros(
            VitaminizacionEntity vitaminizacion,
            SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue(
                "@idLote",
                vitaminizacion.IdLote);

            cmd.Parameters.AddWithValue(
                "@idVitamina",
                vitaminizacion.IdVitamina);

            cmd.Parameters.AddWithValue(
                "@fecha",
                vitaminizacion.Fecha.Date);

            cmd.Parameters.AddWithValue(
                "@cantidad",
                vitaminizacion.Cantidad);

            cmd.Parameters.AddWithValue(
                "@unidadMedida",
                vitaminizacion.UnidadMedida);

            cmd.Parameters.AddWithValue(
                "@observacion",
                string.IsNullOrWhiteSpace(vitaminizacion.Observacion)
                    ? DBNull.Value
                    : vitaminizacion.Observacion);
        }

        private static VitaminizacionEntity CrearVitaminizacion(
            SqlDataReader dr)
        {
            VitaminizacionEntity vitaminizacion = new();

            vitaminizacion.AsignarId(
                Convert.ToInt32(dr["id"]));

            vitaminizacion.AsignarLote(
                Convert.ToInt32(dr["idLote"]),
                dr["codigoLote"].ToString());

            vitaminizacion.AsignarVitamina(
                Convert.ToInt32(dr["idVitamina"]),
                dr["nombreVitamina"].ToString());

            vitaminizacion.CambiarFecha(
                Convert.ToDateTime(dr["fecha"]));

            vitaminizacion.CambiarCantidad(
                Convert.ToDecimal(dr["cantidad"]));

            vitaminizacion.CambiarUnidadMedida(
                dr["unidadMedida"].ToString()!);

            vitaminizacion.CambiarObservacion(
                dr["observacion"] == DBNull.Value
                    ? null
                    : dr["observacion"].ToString());

            return vitaminizacion;
        }
    }
}
