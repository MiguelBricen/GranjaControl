using GranjaControl.DAL.Common;
using GranjaControl.Entitys.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GranjaControl.DAL
{
    public class MortalidadDAL
    {
        public async Task<PaginacionEntity<MortalidadEntity>> MostrarAsync(
            int pagina,
            int tamPagina,
            int? idLote = null)
        {
            try
            {
                PaginacionEntity<MortalidadEntity> resultado = new();

                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmdTotal =
                    new("sp_ContarMortalidades", cn);

                cmdTotal.CommandType = CommandType.StoredProcedure;
                cmdTotal.Parameters.AddWithValue(
                    "@idLote",
                    idLote.HasValue ? idLote.Value : DBNull.Value);

                await cn.OpenAsync();

                resultado.TotalRegistros =
                    Convert.ToInt32(await cmdTotal.ExecuteScalarAsync());

                using SqlCommand cmd =
                    new("sp_MostrarMortalidades", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(
                    "@idLote",
                    idLote.HasValue ? idLote.Value : DBNull.Value);

                cmd.Parameters.AddWithValue("@pagina", pagina);
                cmd.Parameters.AddWithValue("@tamPagina", tamPagina);

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                while (await dr.ReadAsync())
                    resultado.Registros.Add(CrearMortalidad(dr));

                resultado.PaginaActual = pagina;
                resultado.TamPagina = tamPagina;

                return resultado;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<MortalidadEntity?> ObtenerPorIdAsync(int id)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmd =
                    new("sp_ObtenerMortalidadPorId", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                if (!await dr.ReadAsync())
                    return null;

                return CrearMortalidad(dr);
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task AgregarAsync(
            MortalidadEntity mortalidad,
            string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_AgregarMortalidad", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                AgregarParametros(mortalidad, cmd);
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
            MortalidadEntity mortalidad,
            string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd =
                    new("sp_ActualizarMortalidad", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", mortalidad.Id);
                AgregarParametros(mortalidad, cmd);
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
            MortalidadEntity mortalidad,
            SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue(
                "@idLote",
                mortalidad.IdLote);

            cmd.Parameters.AddWithValue(
                "@fecha",
                mortalidad.Fecha.Date);

            cmd.Parameters.AddWithValue(
                "@cantidad",
                mortalidad.Cantidad);

            cmd.Parameters.AddWithValue(
                "@causa",
                mortalidad.Causa);

            cmd.Parameters.AddWithValue(
                "@observacion",
                string.IsNullOrWhiteSpace(mortalidad.Observacion)
                    ? DBNull.Value
                    : mortalidad.Observacion);
        }

        private static MortalidadEntity CrearMortalidad(
            SqlDataReader dr)
        {
            MortalidadEntity mortalidad = new();

            mortalidad.AsignarId(Convert.ToInt32(dr["id"]));

            mortalidad.AsignarLote(
                Convert.ToInt32(dr["idLote"]),
                dr["codigoLote"].ToString());

            mortalidad.CambiarFecha(
                Convert.ToDateTime(dr["fecha"]));

            mortalidad.CambiarCantidad(
                Convert.ToInt32(dr["cantidad"]));

            mortalidad.CambiarCausa(
                dr["causa"].ToString()!);

            mortalidad.CambiarObservacion(
                dr["observacion"] == DBNull.Value
                    ? null
                    : dr["observacion"].ToString());

            return mortalidad;
        }
    }
}
