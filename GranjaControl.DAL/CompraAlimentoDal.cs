using GranjaControl.DAL.Common;
using GranjaControl.Entitys.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GranjaControl.DAL
{
    public class CompraAlimentoDAL
    {
        public async Task<PaginacionEntity<CompraAlimentoEntity>> MostrarAsync(
            int pagina,
            int tamPagina,
            string? buscar = null)
        {
            try
            {
                PaginacionEntity<CompraAlimentoEntity> resultado = new();

                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmdTotal =
                    new("sp_ContarComprasAlimento", cn);

                cmdTotal.CommandType = CommandType.StoredProcedure;
                cmdTotal.Parameters.AddWithValue(
                    "@buscar",
                    string.IsNullOrWhiteSpace(buscar)
                        ? DBNull.Value
                        : buscar.Trim());

                await cn.OpenAsync();

                resultado.TotalRegistros =
                    Convert.ToInt32(await cmdTotal.ExecuteScalarAsync());

                using SqlCommand cmd =
                    new("sp_MostrarComprasAlimento", cn);

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
                    resultado.Registros.Add(CrearCompra(dr));

                resultado.PaginaActual = pagina;
                resultado.TamPagina = tamPagina;

                return resultado;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<CompraAlimentoEntity?> ObtenerPorIdAsync(int id)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmd =
                    new("sp_ObtenerCompraAlimentoPorId", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                if (!await dr.ReadAsync())
                    return null;

                return CrearCompra(dr);
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<int> GuardarAsync(
            CompraAlimentoEntity compra,
            IReadOnlyList<DetalleCompraAlimentoEntity> detalles,
            string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();

                await cn.OpenAsync();

                using SqlTransaction tx = cn.BeginTransaction();

                try
                {
                    int idCompra = compra.Id;

                    if (idCompra > 0)
                    {
                        await ActualizarCompraAsync(
                            cn,
                            tx,
                            compra,
                            usuarioApp);
                    }
                    else
                    {
                        await AgregarCompraAsync(
                            cn,
                            tx,
                            compra,
                            usuarioApp);

                        idCompra = await ObtenerIdCompraAsync(
                            cn,
                            tx,
                            compra.NumeroCompra);
                    }

                    foreach (DetalleCompraAlimentoEntity detalle in detalles)
                    {
                        if (detalle.Id > 0)
                        {
                            await ActualizarDetalleAsync(
                                cn,
                                tx,
                                idCompra,
                                detalle,
                                usuarioApp);
                        }
                        else
                        {
                            await AgregarDetalleAsync(
                                cn,
                                tx,
                                idCompra,
                                detalle,
                                usuarioApp);
                        }
                    }

                    await tx.CommitAsync();

                    return idCompra;
                }
                catch
                {
                    IntentarRollback(tx);
                    throw;
                }
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        private static async Task AgregarCompraAsync(
            SqlConnection cn,
            SqlTransaction tx,
            CompraAlimentoEntity compra,
            string usuarioApp)
        {
            using SqlCommand cmd =
                new("sp_AgregarCompraAlimento", cn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = tx;

            AgregarParametrosCompra(compra, cmd);
            cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

            await cmd.ExecuteNonQueryAsync();
        }

        private static async Task ActualizarCompraAsync(
            SqlConnection cn,
            SqlTransaction tx,
            CompraAlimentoEntity compra,
            string usuarioApp)
        {
            using SqlCommand cmd =
                new("sp_ActualizarCompraAlimento", cn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = tx;

            cmd.Parameters.AddWithValue("@Id", compra.Id);
            AgregarParametrosCompra(compra, cmd);
            cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

            await cmd.ExecuteNonQueryAsync();
        }

        private static async Task<int> ObtenerIdCompraAsync(
            SqlConnection cn,
            SqlTransaction tx,
            string numeroCompra)
        {
            using SqlCommand cmd =
                new("sp_MostrarComprasAlimento", cn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = tx;

            cmd.Parameters.AddWithValue("@buscar", numeroCompra);
            cmd.Parameters.AddWithValue("@pagina", 1);
            cmd.Parameters.AddWithValue("@tamPagina", 10000);

            using SqlDataReader dr = await cmd.ExecuteReaderAsync();

            while (await dr.ReadAsync())
            {
                string numero = dr["numeroCompra"].ToString() ?? string.Empty;

                if (numero.Equals(
                        numeroCompra,
                        StringComparison.OrdinalIgnoreCase))
                {
                    return Convert.ToInt32(dr["id"]);
                }
            }

            throw new InvalidOperationException(
                "No se pudo recuperar el ID de la compra registrada.");
        }

        private static async Task AgregarDetalleAsync(
            SqlConnection cn,
            SqlTransaction tx,
            int idCompra,
            DetalleCompraAlimentoEntity detalle,
            string usuarioApp)
        {
            using SqlCommand cmd =
                new("sp_AgregarDetalleCompraAlimento", cn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = tx;

            AgregarParametrosDetalle(idCompra, detalle, cmd);
            cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

            await cmd.ExecuteNonQueryAsync();
        }

        private static async Task ActualizarDetalleAsync(
            SqlConnection cn,
            SqlTransaction tx,
            int idCompra,
            DetalleCompraAlimentoEntity detalle,
            string usuarioApp)
        {
            using SqlCommand cmd =
                new("sp_ActualizarDetalleCompraAlimento", cn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = tx;

            cmd.Parameters.AddWithValue("@Id", detalle.Id);
            AgregarParametrosDetalle(idCompra, detalle, cmd);
            cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

            await cmd.ExecuteNonQueryAsync();
        }

        private static void AgregarParametrosCompra(
            CompraAlimentoEntity compra,
            SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue(
                "@numeroCompra",
                compra.NumeroCompra);

            cmd.Parameters.AddWithValue(
                "@idProveedor",
                compra.IdProveedor);

            cmd.Parameters.AddWithValue(
                "@fechaCompra",
                compra.FechaCompra.Date);

            cmd.Parameters.AddWithValue(
                "@observacion",
                compra.Observacion is null
                    ? DBNull.Value
                    : compra.Observacion);
        }

        private static void AgregarParametrosDetalle(
            int idCompra,
            DetalleCompraAlimentoEntity detalle,
            SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@idCompra", idCompra);
            cmd.Parameters.AddWithValue("@idAlimento", detalle.IdAlimento);
            cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
            cmd.Parameters.AddWithValue("@unidadMedida", detalle.UnidadMedida);
            cmd.Parameters.AddWithValue("@costoUnitario", detalle.CostoUnitario);
        }

        private static void IntentarRollback(SqlTransaction tx)
        {
            try
            {
                tx.Rollback();
            }
            catch (InvalidOperationException)
            {
            }
            catch (SqlException)
            {
            }
        }

        private static CompraAlimentoEntity CrearCompra(SqlDataReader dr)
        {
            CompraAlimentoEntity compra = new();

            compra.AsignarId(Convert.ToInt32(dr["id"]));

            compra.CambiarNumeroCompra(
                dr["numeroCompra"].ToString()!);

            compra.AsignarProveedor(
                Convert.ToInt32(dr["idProveedor"]),
                dr["nombreProveedor"].ToString());

            compra.CambiarFecha(
                Convert.ToDateTime(dr["fechaCompra"]));

            compra.CambiarObservacion(
                dr["observacion"] == DBNull.Value
                    ? null
                    : dr["observacion"].ToString());

            return compra;
        }
    }
}
