using GranjaControl.DAL.Common;
using GranjaControl.Entitys.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GranjaControl.DAL
{
    public class LoteDAL
    {
        public async Task<PaginacionEntity<LoteEntity>> MostrarAsync(
            int pagina,
            int tamPagina,
            string? buscar = null)
        {
            try
            {
                PaginacionEntity<LoteEntity> resultado = new();

                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmdTotal = new("sp_ContarLotes", cn);

                cmdTotal.CommandType = CommandType.StoredProcedure;
                cmdTotal.Parameters.AddWithValue(
                    "@buscar",
                    string.IsNullOrWhiteSpace(buscar)
                        ? DBNull.Value
                        : buscar.Trim());

                await cn.OpenAsync();

                resultado.TotalRegistros =
                    Convert.ToInt32(await cmdTotal.ExecuteScalarAsync());

                using SqlCommand cmd = new("sp_MostrarLotes", cn);

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
                    resultado.Registros.Add(CrearLote(dr));

                resultado.PaginaActual = pagina;
                resultado.TamPagina = tamPagina;

                return resultado;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<List<LoteEntity>> MostrarLotesActivosAsync()
        {
            try
            {
                List<LoteEntity> lista = [];

                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_MostrarLotesActivos", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                while (await dr.ReadAsync())
                    lista.Add(CrearLote(dr));

                return lista;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<LoteEntity?> ObtenerPorIdAsync(int id)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_ObtenerLotePorId", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                if (!await dr.ReadAsync())
                    return null;

                return CrearLote(dr);
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task AgregarAsync(LoteEntity lote, string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_AgregarLote", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@codigoLote", lote.CodigoLote);
                cmd.Parameters.AddWithValue("@idGalpon", lote.IdGalpon);
                cmd.Parameters.AddWithValue("@idProveedor", lote.IdProveedor);
                cmd.Parameters.AddWithValue("@fechaIngreso", lote.FechaIngreso.Date);
                cmd.Parameters.AddWithValue("@cantidadInicial", lote.CantidadInicial);
                cmd.Parameters.AddWithValue("@costoPorPollito", lote.CostoPorPollito);

                cmd.Parameters.AddWithValue(
                    "@observacion",
                    string.IsNullOrWhiteSpace(lote.Observacion)
                        ? DBNull.Value
                        : lote.Observacion);

                cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

                await cn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task ActualizarAsync(LoteEntity lote, string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_ActualizarLote", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", lote.Id);
                cmd.Parameters.AddWithValue("@codigoLote", lote.CodigoLote);
                cmd.Parameters.AddWithValue("@idGalpon", lote.IdGalpon);
                cmd.Parameters.AddWithValue("@idProveedor", lote.IdProveedor);
                cmd.Parameters.AddWithValue("@fechaIngreso", lote.FechaIngreso.Date);
                cmd.Parameters.AddWithValue("@cantidadInicial", lote.CantidadInicial);
                cmd.Parameters.AddWithValue("@costoPorPollito", lote.CostoPorPollito);

                cmd.Parameters.AddWithValue(
                    "@observacion",
                    string.IsNullOrWhiteSpace(lote.Observacion)
                        ? DBNull.Value
                        : lote.Observacion);

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
            string estado,
            string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_CambiarEstadoLote", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@estado", estado);
                cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

                await cn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        private static LoteEntity CrearLote(SqlDataReader dr)
        {
            LoteEntity lote = new();

            lote.AsignarId(Convert.ToInt32(dr["id"]));
            lote.CambiarCodigo(dr["codigoLote"].ToString()!);
            lote.AsignarGalpon(
                Convert.ToInt32(dr["idGalpon"]),
                dr["nombreGalpon"].ToString());

            lote.AsignarProveedor(
                Convert.ToInt32(dr["idProveedor"]),
                dr["nombreProveedor"].ToString());

            lote.CambiarFechaIngreso(
                Convert.ToDateTime(dr["fechaIngreso"]));

            lote.CambiarCantidadInicial(
                Convert.ToInt32(dr["cantidadInicial"]));

            lote.CambiarCostoPorPollito(
                Convert.ToDecimal(dr["costoPorPollito"]));

            lote.CambiarEstado(dr["estado"].ToString()!);

            lote.CambiarObservacion(
                dr["observacion"] == DBNull.Value
                    ? null
                    : dr["observacion"].ToString());

            return lote;
        }
    }
}
