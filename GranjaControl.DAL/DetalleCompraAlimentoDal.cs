using GranjaControl.DAL.Common;
using GranjaControl.Entitys.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GranjaControl.DAL
{
    public class DetalleCompraAlimentoDAL
    {
        public async Task<List<DetalleCompraAlimentoEntity>> MostrarPorCompraAsync(
            int idCompra)
        {
            try
            {
                List<DetalleCompraAlimentoEntity> lista = [];

                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmd =
                    new("sp_MostrarDetalleComprasAlimento", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCompra", idCompra);

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

        public async Task<DetalleCompraAlimentoEntity?> ObtenerPorIdAsync(
            int id)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmd =
                    new("sp_ObtenerDetalleCompraAlimentoPorId", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                if (!await dr.ReadAsync())
                    return null;

                return CrearDetalle(dr);
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        private static DetalleCompraAlimentoEntity CrearDetalle(
            SqlDataReader dr)
        {
            DetalleCompraAlimentoEntity detalle = new();

            detalle.AsignarId(Convert.ToInt32(dr["id"]));

            detalle.AsignarCompra(
                Convert.ToInt32(dr["idCompra"]),
                dr["numeroCompra"].ToString());

            detalle.AsignarAlimento(
                Convert.ToInt32(dr["idAlimento"]),
                dr["nombreAlimento"].ToString(),
                dr["tipoAlimento"].ToString());

            detalle.CambiarCantidad(
                Convert.ToDecimal(dr["cantidad"]));

            detalle.CambiarUnidadMedida(
                dr["unidadMedida"].ToString()!);

            detalle.CambiarCostoUnitario(
                Convert.ToDecimal(dr["costoUnitario"]));

            return detalle;
        }
    }
}
