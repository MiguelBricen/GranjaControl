using GranjaControl.DAL.Common;
using GranjaControl.Entitys.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GranjaControl.DAL
{
    public class VentaDAL
    {
        public async Task<PaginacionEntity<VentaEntity>> MostrarAsync(
            int pagina,
            int tamPagina,
            string? buscar = null)
        {
            try
            {
                PaginacionEntity<VentaEntity> resultado = new();

                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmdTotal = new("sp_ContarVentas", cn);

                cmdTotal.CommandType = CommandType.StoredProcedure;
                cmdTotal.Parameters.AddWithValue(
                    "@buscar",
                    string.IsNullOrWhiteSpace(buscar)
                        ? DBNull.Value
                        : buscar.Trim());

                await cn.OpenAsync();

                resultado.TotalRegistros =
                    Convert.ToInt32(await cmdTotal.ExecuteScalarAsync());

                using SqlCommand cmd = new("sp_MostrarVentas", cn);

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
                    resultado.Registros.Add(CrearVenta(dr));

                resultado.PaginaActual = pagina;
                resultado.TamPagina = tamPagina;

                return resultado;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<VentaEntity?> ObtenerPorIdAsync(int id)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmd = new("sp_ObtenerVentaPorId", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                if (!await dr.ReadAsync())
                    return null;

                return CrearVenta(dr);
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<List<DetalleVentaEntity>> MostrarDetallesAsync(
            int idVenta)
        {
            try
            {
                List<DetalleVentaEntity> lista = [];

                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmd =
                    new("sp_MostrarDetalleVentas", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idVenta", idVenta);

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                while (await dr.ReadAsync())
                    lista.Add(CrearDetalle(dr));

                return lista;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<int> GuardarAsync(
            VentaEntity venta,
            IReadOnlyList<DetalleVentaEntity> detalles,
            string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();

                await cn.OpenAsync();

                using SqlTransaction tx = cn.BeginTransaction();

                try
                {
                    bool ventaNueva = venta.Id <= 0;
                    int idVenta = venta.Id;

                    if (!ventaNueva)
                    {
                        await ActualizarVentaAsync(
                            cn,
                            tx,
                            venta,
                            usuarioApp);
                    }
                    else
                    {
                        idVenta = await AgregarVentaAsync(
                            cn,
                            tx,
                            venta,
                            usuarioApp);
                    }

                    await EliminarDetallesRetiradosAsync(
                        cn,
                        tx,
                        idVenta,
                        detalles,
                        usuarioApp);

                    List<(DetalleVentaEntity Detalle, int Id)> detallesNuevos =
                        [];

                    foreach (DetalleVentaEntity detalle in detalles)
                    {
                        if (detalle.Id > 0)
                        {
                            await ActualizarDetalleAsync(
                                cn,
                                tx,
                                idVenta,
                                detalle,
                                usuarioApp);
                        }
                        else
                        {
                            int idDetalle = await AgregarDetalleAsync(
                                cn,
                                tx,
                                idVenta,
                                detalle,
                                usuarioApp);

                            detallesNuevos.Add((detalle, idDetalle));
                        }
                    }

                    await tx.CommitAsync();

                    if (ventaNueva)
                        venta.AsignarId(idVenta);

                    foreach (var nuevo in detallesNuevos)
                    {
                        nuevo.Detalle.AsignarId(nuevo.Id);
                        nuevo.Detalle.AsignarVenta(
                            idVenta,
                            venta.NumeroVenta);
                    }

                    return idVenta;
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

        public async Task AnularAsync(int id, string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmd = new("sp_AnularVenta", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

                await cn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        private static async Task<int> AgregarVentaAsync(
            SqlConnection cn,
            SqlTransaction tx,
            VentaEntity venta,
            string usuarioApp)
        {
            using SqlCommand cmd = new("sp_AgregarVenta", cn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = tx;

            AgregarParametrosVenta(venta, cmd);
            cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

            object? resultado = await cmd.ExecuteScalarAsync();

            if (resultado is null || resultado == DBNull.Value)
            {
                throw new InvalidOperationException(
                    "No se pudo recuperar el ID de la venta registrada.");
            }

            return Convert.ToInt32(resultado);
        }

        private static async Task ActualizarVentaAsync(
            SqlConnection cn,
            SqlTransaction tx,
            VentaEntity venta,
            string usuarioApp)
        {
            using SqlCommand cmd = new("sp_ActualizarVenta", cn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = tx;

            cmd.Parameters.AddWithValue("@Id", venta.Id);
            AgregarParametrosVenta(venta, cmd);
            cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

            await cmd.ExecuteNonQueryAsync();
        }

        private static async Task<int> AgregarDetalleAsync(
            SqlConnection cn,
            SqlTransaction tx,
            int idVenta,
            DetalleVentaEntity detalle,
            string usuarioApp)
        {
            using SqlCommand cmd = new("sp_AgregarDetalleVenta", cn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = tx;

            AgregarParametrosDetalle(idVenta, detalle, cmd);
            cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

            object? resultado = await cmd.ExecuteScalarAsync();

            if (resultado is null || resultado == DBNull.Value)
            {
                throw new InvalidOperationException(
                    "No se pudo recuperar el ID del detalle de venta.");
            }

            return Convert.ToInt32(resultado);
        }

        private static async Task ActualizarDetalleAsync(
            SqlConnection cn,
            SqlTransaction tx,
            int idVenta,
            DetalleVentaEntity detalle,
            string usuarioApp)
        {
            using SqlCommand cmd =
                new("sp_ActualizarDetalleVenta", cn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = tx;

            cmd.Parameters.AddWithValue("@Id", detalle.Id);
            AgregarParametrosDetalle(idVenta, detalle, cmd);
            cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

            await cmd.ExecuteNonQueryAsync();
        }

        private static async Task EliminarDetallesRetiradosAsync(
            SqlConnection cn,
            SqlTransaction tx,
            int idVenta,
            IReadOnlyList<DetalleVentaEntity> detalles,
            string usuarioApp)
        {
            List<int> idsActuales = await ObtenerIdsDetalleAsync(
                cn,
                tx,
                idVenta);

            HashSet<int> idsConservados = detalles
                .Where(d => d.Id > 0)
                .Select(d => d.Id)
                .ToHashSet();

            foreach (int idActual in idsActuales)
            {
                if (idsConservados.Contains(idActual))
                    continue;

                using SqlCommand cmd =
                    new("sp_EliminarDetalleVenta", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = tx;
                cmd.Parameters.AddWithValue("@Id", idActual);
                cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

                await cmd.ExecuteNonQueryAsync();
            }
        }

        private static async Task<List<int>> ObtenerIdsDetalleAsync(
            SqlConnection cn,
            SqlTransaction tx,
            int idVenta)
        {
            List<int> ids = [];

            using SqlCommand cmd =
                new("sp_MostrarDetalleVentas", cn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = tx;
            cmd.Parameters.AddWithValue("@idVenta", idVenta);

            using SqlDataReader dr = await cmd.ExecuteReaderAsync();

            while (await dr.ReadAsync())
                ids.Add(Convert.ToInt32(dr["id"]));

            return ids;
        }

        private static void AgregarParametrosVenta(
            VentaEntity venta,
            SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@numeroVenta", venta.NumeroVenta);
            cmd.Parameters.AddWithValue("@idCliente", venta.IdCliente);
            cmd.Parameters.AddWithValue("@fechaVenta", venta.FechaVenta);
            cmd.Parameters.AddWithValue(
                "@observacion",
                venta.Observacion is null
                    ? DBNull.Value
                    : venta.Observacion);
        }

        private static void AgregarParametrosDetalle(
            int idVenta,
            DetalleVentaEntity detalle,
            SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@idVenta", idVenta);
            cmd.Parameters.AddWithValue("@idLote", detalle.IdLote);
            cmd.Parameters.AddWithValue(
                "@cantidadPollos",
                detalle.CantidadPollos);

            cmd.Parameters.AddWithValue(
                "@pesoLibras",
                detalle.PesoLibras);
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

        private static VentaEntity CrearVenta(SqlDataReader dr)
        {
            VentaEntity venta = new();

            venta.AsignarId(Convert.ToInt32(dr["id"]));

            venta.CambiarNumeroVenta(
                dr["numeroVenta"].ToString()!);

            venta.AsignarCliente(
                Convert.ToInt32(dr["idCliente"]),
                dr["nombreCliente"].ToString());

            venta.CambiarFecha(
                Convert.ToDateTime(dr["fechaVenta"]));

            venta.CambiarObservacion(
                dr["observacion"] == DBNull.Value
                    ? null
                    : dr["observacion"].ToString());

            venta.CambiarEstado(dr["estado"].ToString()!);

            venta.EstablecerTotales(
                Convert.ToInt32(dr["cantidadPollos"]),
                Convert.ToDecimal(dr["pesoTotalLibras"]),
                Convert.ToDecimal(dr["totalVenta"]));

            return venta;
        }

        private static DetalleVentaEntity CrearDetalle(
            SqlDataReader dr)
        {
            DetalleVentaEntity detalle = new();

            detalle.AsignarId(Convert.ToInt32(dr["id"]));

            detalle.AsignarVenta(
                Convert.ToInt32(dr["idVenta"]),
                dr["numeroVenta"].ToString());

            detalle.AsignarLote(
                Convert.ToInt32(dr["idLote"]),
                dr["codigoLote"].ToString());

            detalle.CambiarCantidadPollos(
                Convert.ToInt32(dr["cantidadPollos"]));

            detalle.CambiarPesoLibras(
                Convert.ToDecimal(dr["pesoLibras"]));

            detalle.CambiarPrecioPorLibra(
                Convert.ToDecimal(dr["precioPorLibra"]));

            return detalle;
        }
    }
}
