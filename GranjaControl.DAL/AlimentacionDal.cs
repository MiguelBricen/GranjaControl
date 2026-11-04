using GranjaControl.DAL.Common;
using GranjaControl.Entitys.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GranjaControl.DAL
{
    public class AlimentacionDAL
    {
        public async Task<PaginacionEntity<AlimentacionEntity>> MostrarAsync(
            int pagina,
            int tamPagina,
            int? idLote = null)
        {
            try
            {
                PaginacionEntity<AlimentacionEntity> resultado = new();

                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmdTotal =
                    new("sp_ContarAlimentaciones", cn);

                cmdTotal.CommandType = CommandType.StoredProcedure;
                cmdTotal.Parameters.AddWithValue(
                    "@idLote",
                    idLote.HasValue ? idLote.Value : DBNull.Value);

                await cn.OpenAsync();

                resultado.TotalRegistros =
                    Convert.ToInt32(await cmdTotal.ExecuteScalarAsync());

                using SqlCommand cmd =
                    new("sp_MostrarAlimentaciones", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(
                    "@idLote",
                    idLote.HasValue ? idLote.Value : DBNull.Value);

                cmd.Parameters.AddWithValue("@pagina", pagina);
                cmd.Parameters.AddWithValue("@tamPagina", tamPagina);

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                while (await dr.ReadAsync())
                    resultado.Registros.Add(CrearAlimentacion(dr));

                resultado.PaginaActual = pagina;
                resultado.TamPagina = tamPagina;

                return resultado;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<AlimentacionEntity?> ObtenerPorIdAsync(int id)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmd =
                    new("sp_ObtenerAlimentacionPorId", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                if (!await dr.ReadAsync())
                    return null;

                return CrearAlimentacion(dr);
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task AgregarAsync(
            AlimentacionEntity alimentacion,
            string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_AgregarAlimentacion", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                AgregarParametros(alimentacion, cmd);
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
            AlimentacionEntity alimentacion,
            string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd =
                    new("sp_ActualizarAlimentacion", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", alimentacion.Id);
                AgregarParametros(alimentacion, cmd);
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
            AlimentacionEntity alimentacion,
            SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue(
                "@idLote",
                alimentacion.IdLote);

            cmd.Parameters.AddWithValue(
                "@idAlimento",
                alimentacion.IdAlimento);

            cmd.Parameters.AddWithValue(
                "@fecha",
                alimentacion.Fecha.Date);

            cmd.Parameters.AddWithValue(
                "@cantidad",
                alimentacion.Cantidad);

            cmd.Parameters.AddWithValue(
                "@unidadMedida",
                alimentacion.UnidadMedida);

            cmd.Parameters.AddWithValue(
                "@observacion",
                string.IsNullOrWhiteSpace(alimentacion.Observacion)
                    ? DBNull.Value
                    : alimentacion.Observacion);
        }

        private static AlimentacionEntity CrearAlimentacion(
            SqlDataReader dr)
        {
            AlimentacionEntity alimentacion = new();

            alimentacion.AsignarId(Convert.ToInt32(dr["id"]));

            alimentacion.AsignarLote(
                Convert.ToInt32(dr["idLote"]),
                dr["codigoLote"].ToString());

            alimentacion.AsignarAlimento(
                Convert.ToInt32(dr["idAlimento"]),
                dr["nombreAlimento"].ToString(),
                dr["tipoAlimento"].ToString());

            alimentacion.CambiarFecha(
                Convert.ToDateTime(dr["fecha"]));

            alimentacion.CambiarCantidad(
                Convert.ToDecimal(dr["cantidad"]));

            alimentacion.CambiarUnidadMedida(
                dr["unidadMedida"].ToString()!);

            alimentacion.CambiarObservacion(
                dr["observacion"] == DBNull.Value
                    ? null
                    : dr["observacion"].ToString());

            return alimentacion;
        }
    }
}
