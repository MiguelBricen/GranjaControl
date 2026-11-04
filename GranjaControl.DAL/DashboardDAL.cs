using GranjaControl.DAL.Common;
using GranjaControl.Entitys.Dto;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GranjaControl.DAL
{
    public class DashboardDAL
    {
        public async Task<DashboardDto> ObtenerAsync(int cantidadVentas = 5)
        {
            try
            {
                DashboardDto resultado = new();

                using SqlConnection cn = Conexion.ObtenerConexion();

                await cn.OpenAsync();

                using (SqlCommand cmd = new("sp_DashboardResumen", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                    if (await dr.ReadAsync())
                        resultado.Resumen = CrearResumen(dr);
                }

                using (SqlCommand cmd = new("sp_DashboardLotesActivos", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                    while (await dr.ReadAsync())
                        resultado.LotesActivos.Add(CrearLoteActivo(dr));
                }

                using (SqlCommand cmd = new("sp_DashboardVentasRecientes", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@cantidad", cantidadVentas);

                    using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                    while (await dr.ReadAsync())
                        resultado.VentasRecientes.Add(CrearVentaReciente(dr));
                }

                return resultado;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        private static DashboardResumenDto CrearResumen(SqlDataReader dr)
        {
            return new DashboardResumenDto
            {
                LotesActivos = Convert.ToInt32(dr["LotesActivos"]),
                AvesIniciales = Convert.ToInt32(dr["AvesIniciales"]),
                AvesDisponibles = Convert.ToInt32(dr["AvesDisponibles"]),
                PollosVendidosMes = Convert.ToInt32(dr["PollosVendidosMes"]),
                VentasMes = Convert.ToInt32(dr["VentasMes"]),
                IngresosMes = Convert.ToDecimal(dr["IngresosMes"]),
                MortalidadAcumulada = Convert.ToInt32(dr["MortalidadAcumulada"]),
                ComprasMes = Convert.ToDecimal(dr["ComprasMes"]),
                ClientesActivos = Convert.ToInt32(dr["ClientesActivos"])
            };
        }

        private static DashboardLoteActivoDto CrearLoteActivo(SqlDataReader dr)
        {
            return new DashboardLoteActivoDto
            {
                Id = Convert.ToInt32(dr["id"]),
                CodigoLote = dr["codigoLote"].ToString() ?? string.Empty,
                NombreGalpon = dr["nombreGalpon"].ToString() ?? string.Empty,
                NombreProveedor = dr["nombreProveedor"].ToString() ?? string.Empty,
                FechaIngreso = Convert.ToDateTime(dr["fechaIngreso"]),
                Estado = dr["estado"].ToString() ?? string.Empty,
                CantidadInicial = Convert.ToInt32(dr["cantidadInicial"]),
                Mortalidad = Convert.ToInt32(dr["Mortalidad"]),
                Vendidos = Convert.ToInt32(dr["Vendidos"]),
                Disponibles = Convert.ToInt32(dr["Disponibles"]),
                PorcentajeMortalidad = dr["PorcentajeMortalidad"] == DBNull.Value
                    ? 0
                    : Convert.ToDecimal(dr["PorcentajeMortalidad"])
            };
        }

        private static DashboardVentaRecienteDto CrearVentaReciente(SqlDataReader dr)
        {
            return new DashboardVentaRecienteDto
            {
                Id = Convert.ToInt32(dr["id"]),
                NumeroVenta = dr["numeroVenta"].ToString() ?? string.Empty,
                NombreCliente = dr["nombreCliente"].ToString() ?? string.Empty,
                FechaVenta = Convert.ToDateTime(dr["fechaVenta"]),
                Estado = dr["estado"].ToString() ?? string.Empty,
                CantidadPollos = Convert.ToInt32(dr["CantidadPollos"]),
                PesoLibras = Convert.ToDecimal(dr["PesoLibras"]),
                Total = Convert.ToDecimal(dr["Total"])
            };
        }
    }
}
