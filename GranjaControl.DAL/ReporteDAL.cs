using GranjaControl.DAL.Common;
using GranjaControl.Entitys.Dto;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GranjaControl.DAL
{
    public class ReporteDAL
    {
        public async Task<ReporteVentasDto> ObtenerVentasAsync(
            DateTime? fechaInicio,
            DateTime? fechaFin,
            int? idCliente,
            string? estado)
        {
            try
            {
                ReporteVentasDto resultado = new();

                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_ReporteVentasPeriodo", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(
                    "@fechaInicio",
                    fechaInicio.HasValue ? fechaInicio.Value.Date : DBNull.Value);

                cmd.Parameters.AddWithValue(
                    "@fechaFin",
                    fechaFin.HasValue ? fechaFin.Value.Date : DBNull.Value);

                cmd.Parameters.AddWithValue(
                    "@idCliente",
                    idCliente.HasValue ? idCliente.Value : DBNull.Value);

                cmd.Parameters.AddWithValue(
                    "@estado",
                    string.IsNullOrWhiteSpace(estado)
                        ? DBNull.Value
                        : estado.Trim());

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                if (await dr.ReadAsync())
                    resultado.Resumen = CrearResumenVentas(dr);

                await dr.NextResultAsync();

                while (await dr.ReadAsync())
                    resultado.Detalle.Add(CrearVenta(dr));

                return resultado;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<ReporteProduccionDto> ObtenerProduccionAsync(
            DateTime? fechaInicio,
            DateTime? fechaFin,
            string? estado,
            int? idGalpon)
        {
            try
            {
                ReporteProduccionDto resultado = new();

                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_ReporteProduccionLotes", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(
                    "@fechaInicio",
                    fechaInicio.HasValue ? fechaInicio.Value.Date : DBNull.Value);

                cmd.Parameters.AddWithValue(
                    "@fechaFin",
                    fechaFin.HasValue ? fechaFin.Value.Date : DBNull.Value);

                cmd.Parameters.AddWithValue(
                    "@estado",
                    string.IsNullOrWhiteSpace(estado)
                        ? DBNull.Value
                        : estado.Trim());

                cmd.Parameters.AddWithValue(
                    "@idGalpon",
                    idGalpon.HasValue ? idGalpon.Value : DBNull.Value);

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                if (await dr.ReadAsync())
                    resultado.Resumen = CrearResumenProduccion(dr);

                await dr.NextResultAsync();

                while (await dr.ReadAsync())
                    resultado.Detalle.Add(CrearLoteProduccion(dr));

                return resultado;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<ReporteComprasDto> ObtenerComprasAsync(
            DateTime? fechaInicio,
            DateTime? fechaFin,
            int? idProveedor)
        {
            try
            {
                ReporteComprasDto resultado = new();

                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_ReporteComprasAlimento", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(
                    "@fechaInicio",
                    fechaInicio.HasValue ? fechaInicio.Value.Date : DBNull.Value);

                cmd.Parameters.AddWithValue(
                    "@fechaFin",
                    fechaFin.HasValue ? fechaFin.Value.Date : DBNull.Value);

                cmd.Parameters.AddWithValue(
                    "@idProveedor",
                    idProveedor.HasValue ? idProveedor.Value : DBNull.Value);

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                if (await dr.ReadAsync())
                    resultado.Resumen = CrearResumenCompras(dr);

                await dr.NextResultAsync();

                while (await dr.ReadAsync())
                    resultado.Detalle.Add(CrearCompra(dr));

                return resultado;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        private static ReporteVentasResumenDto CrearResumenVentas(SqlDataReader dr)
        {
            return new ReporteVentasResumenDto
            {
                TotalVentas = Convert.ToInt32(dr["TotalVentas"]),
                Registradas = Convert.ToInt32(dr["Registradas"]),
                Anuladas = Convert.ToInt32(dr["Anuladas"]),
                PollosVendidos = Convert.ToInt32(dr["PollosVendidos"]),
                LibrasVendidas = Convert.ToDecimal(dr["LibrasVendidas"]),
                Ingresos = Convert.ToDecimal(dr["Ingresos"])
            };
        }

        private static ReporteVentaDto CrearVenta(SqlDataReader dr)
        {
            return new ReporteVentaDto
            {
                Id = Convert.ToInt32(dr["Id"]),
                NumeroVenta = dr["NumeroVenta"].ToString() ?? string.Empty,
                NombreCliente = dr["NombreCliente"].ToString() ?? string.Empty,
                FechaVenta = Convert.ToDateTime(dr["FechaVenta"]),
                Estado = dr["Estado"].ToString() ?? string.Empty,
                CantidadPollos = Convert.ToInt32(dr["CantidadPollos"]),
                PesoLibras = Convert.ToDecimal(dr["PesoLibras"]),
                Total = Convert.ToDecimal(dr["Total"])
            };
        }

        private static ReporteProduccionResumenDto CrearResumenProduccion(
            SqlDataReader dr)
        {
            return new ReporteProduccionResumenDto
            {
                TotalLotes = Convert.ToInt32(dr["TotalLotes"]),
                CantidadInicial = Convert.ToInt32(dr["CantidadInicial"]),
                Mortalidad = Convert.ToInt32(dr["Mortalidad"]),
                Vendidos = Convert.ToInt32(dr["Vendidos"]),
                Disponibles = Convert.ToInt32(dr["Disponibles"]),
                PorcentajeMortalidad = dr["PorcentajeMortalidad"] == DBNull.Value
                    ? 0
                    : Convert.ToDecimal(dr["PorcentajeMortalidad"])
            };
        }

        private static ReporteProduccionLoteDto CrearLoteProduccion(
            SqlDataReader dr)
        {
            return new ReporteProduccionLoteDto
            {
                Id = Convert.ToInt32(dr["Id"]),
                CodigoLote = dr["CodigoLote"].ToString() ?? string.Empty,
                NombreGalpon = dr["NombreGalpon"].ToString() ?? string.Empty,
                NombreProveedor = dr["NombreProveedor"].ToString() ?? string.Empty,
                FechaIngreso = Convert.ToDateTime(dr["FechaIngreso"]),
                Estado = dr["Estado"].ToString() ?? string.Empty,
                CantidadInicial = Convert.ToInt32(dr["CantidadInicial"]),
                Mortalidad = Convert.ToInt32(dr["Mortalidad"]),
                Vendidos = Convert.ToInt32(dr["Vendidos"]),
                Disponibles = Convert.ToInt32(dr["Disponibles"]),
                PorcentajeMortalidad = dr["PorcentajeMortalidad"] == DBNull.Value
                    ? 0
                    : Convert.ToDecimal(dr["PorcentajeMortalidad"])
            };
        }

        private static ReporteComprasResumenDto CrearResumenCompras(
            SqlDataReader dr)
        {
            return new ReporteComprasResumenDto
            {
                TotalCompras = Convert.ToInt32(dr["TotalCompras"]),
                TotalLibras = Convert.ToDecimal(dr["TotalLibras"]),
                TotalKilogramos = Convert.ToDecimal(dr["TotalKilogramos"]),
                CostoTotal = Convert.ToDecimal(dr["CostoTotal"])
            };
        }

        private static ReporteCompraDto CrearCompra(SqlDataReader dr)
        {
            return new ReporteCompraDto
            {
                Id = Convert.ToInt32(dr["Id"]),
                NumeroCompra = dr["NumeroCompra"].ToString() ?? string.Empty,
                NombreProveedor = dr["NombreProveedor"].ToString() ?? string.Empty,
                FechaCompra = Convert.ToDateTime(dr["FechaCompra"]),
                Libras = Convert.ToDecimal(dr["Libras"]),
                Kilogramos = Convert.ToDecimal(dr["Kilogramos"]),
                CostoTotal = Convert.ToDecimal(dr["CostoTotal"]),
                CantidadItems = Convert.ToInt32(dr["CantidadItems"])
            };
        }
    }
}
