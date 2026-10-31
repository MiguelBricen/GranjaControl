using GranjaControl.DAL.Common;
using GranjaControl.Entitys.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GranjaControl.DAL
{
    public class AlimentoDAL
    {
        public async Task<PaginacionEntity<AlimentoEntity>> MostrarAsync(
            int pagina,
            int tamPagina,
            string? buscar = null)
        {
            try
            {
                PaginacionEntity<AlimentoEntity> resultado = new();

                using SqlConnection cn = Conexion.ObtenerConexion();

                using SqlCommand cmdTotal = new("sp_ContarAlimentos", cn);

                cmdTotal.CommandType = CommandType.StoredProcedure;
                cmdTotal.Parameters.AddWithValue(
                    "@buscar",
                    string.IsNullOrWhiteSpace(buscar)
                        ? DBNull.Value
                        : buscar.Trim());

                await cn.OpenAsync();

                resultado.TotalRegistros =
                    Convert.ToInt32(await cmdTotal.ExecuteScalarAsync());

                using SqlCommand cmd = new("sp_MostrarAlimentos", cn);

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
                    resultado.Registros.Add(CrearAlimento(dr));

                resultado.PaginaActual = pagina;
                resultado.TamPagina = tamPagina;

                return resultado;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<List<AlimentoEntity>> MostrarAlimentosActivosAsync()
        {
            try
            {
                List<AlimentoEntity> lista = [];

                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_MostrarAlimentosActivos", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                while (await dr.ReadAsync())
                {
                    AlimentoEntity alimento = CrearAlimento(dr, false);
                    alimento.CambiarActivo(true);

                    lista.Add(alimento);
                }

                return lista;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<AlimentoEntity?> ObtenerPorIdAsync(int id)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_ObtenerAlimentoPorId", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                if (!await dr.ReadAsync())
                    return null;

                return CrearAlimento(dr);
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task AgregarAsync(AlimentoEntity alimento, string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_AgregarAlimento", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue(
                    "@nombreAlimento",
                    alimento.NombreAlimento);

                cmd.Parameters.AddWithValue(
                    "@tipoAlimento",
                    alimento.TipoAlimento);

                cmd.Parameters.AddWithValue(
                    "@descripcion",
                    string.IsNullOrWhiteSpace(alimento.Descripcion)
                        ? DBNull.Value
                        : alimento.Descripcion);

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
            AlimentoEntity alimento,
            string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_ActualizarAlimento", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", alimento.Id);

                cmd.Parameters.AddWithValue(
                    "@nombreAlimento",
                    alimento.NombreAlimento);

                cmd.Parameters.AddWithValue(
                    "@tipoAlimento",
                    alimento.TipoAlimento);

                cmd.Parameters.AddWithValue(
                    "@descripcion",
                    string.IsNullOrWhiteSpace(alimento.Descripcion)
                        ? DBNull.Value
                        : alimento.Descripcion);

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
                using SqlCommand cmd = new("sp_CambiarEstadoAlimento", cn);

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

        private static AlimentoEntity CrearAlimento(
            SqlDataReader dr,
            bool incluirActivo = true)
        {
            AlimentoEntity alimento = new();

            alimento.AsignarId(Convert.ToInt32(dr["id"]));
            alimento.CambiarNombre(dr["nombreAlimento"].ToString()!);

            alimento.CambiarTipoAlimento(
                dr["tipoAlimento"].ToString()!);

            alimento.CambiarDescripcion(
                dr["descripcion"] == DBNull.Value
                    ? null
                    : dr["descripcion"].ToString());

            if (incluirActivo)
                alimento.CambiarActivo(Convert.ToBoolean(dr["activo"]));

            return alimento;
        }
    }
}
