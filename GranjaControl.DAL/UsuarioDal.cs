using GranjaControl.DAL.Common;
using GranjaControl.Entitys.Entidades;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GranjaControl.DAL
{
    public class UsuarioDal
    {
        public async Task<PaginacionEntity<UsuarioEntity>> MostrarAsync(int pagina, int tamPagina, string? buscar = null)
        {
            try
            {
                PaginacionEntity<UsuarioEntity> resultado = new();

                using SqlConnection cn = Conexion.ObtenerConexion();

                // Total de registros
                using SqlCommand cmdTotal = new("sp_ContarUsuarios2", cn);

                cmdTotal.CommandType = CommandType.StoredProcedure;
                cmdTotal.Parameters.AddWithValue("@buscar", string.IsNullOrWhiteSpace(buscar) ? DBNull.Value : buscar.Trim());

                await cn.OpenAsync();

                resultado.TotalRegistros = Convert.ToInt32(await cmdTotal.ExecuteScalarAsync());

                // Mostrar registros
                using SqlCommand cmd = new("sp_MostrarUsuarios2", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@buscar", string.IsNullOrWhiteSpace(buscar) ? DBNull.Value : buscar.Trim());
                cmd.Parameters.AddWithValue("pagina", pagina);
                cmd.Parameters.AddWithValue("tamPagina", tamPagina);

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                while (await dr.ReadAsync())
                {
                    UsuarioEntity usuario = new();

                    usuario.AsignarId(Convert.ToInt32(dr["IdUsuario"]));
                    usuario.CambiarNombreCompleto(dr["nombreCompleto"].ToString()!);
                    usuario.CambiarNombreUsuario(dr["nombreUsuario"].ToString()!);
                    usuario.CambiarCorreo(dr["correoElectronico"].ToString()!);
                    usuario.CambiarRol(Convert.ToInt32(dr["idRol"].ToString()));
                    usuario.CambiarNombreRol(dr["nombreRol"].ToString()!);
                    usuario.CambiarEstado(Convert.ToBoolean(dr["activo"]));

                    resultado.Registros.Add(usuario);
                }

                resultado.PaginaActual = pagina;
                resultado.TamPagina = tamPagina;

                return resultado;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task<UsuarioEntity?> ObtenerPorIdAsync(int id)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_ObtenerUsuarioPorId2", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                await cn.OpenAsync();

                using SqlDataReader dr = await cmd.ExecuteReaderAsync();

                if (!await dr.ReadAsync())
                    return null;

                UsuarioEntity usuario = new();

                usuario.AsignarId(Convert.ToInt32(dr["Id"]));
                usuario.CambiarNombreCompleto(dr["nombreCompleto"].ToString()!);
                usuario.CambiarNombreUsuario(dr["nombreUsuario"].ToString()!);
                usuario.CambiarCorreo(dr["correoElectronico"].ToString()!);
                usuario.CambiarRol(Convert.ToInt32(dr["idRol"].ToString()));
                usuario.CambiarEstado(Convert.ToBoolean(dr["activo"]));

                return usuario;
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task AgregarAsync(UsuarioEntity usuario, string clave, string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_AgregarUsuario", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombreCompleto", usuario.NombreCompleto);
                cmd.Parameters.AddWithValue("@nombreUsuario", usuario.NombreUsuario);
                cmd.Parameters.AddWithValue("@correoElectronico", usuario.CorreoElectronico);
                cmd.Parameters.AddWithValue("@idRol", usuario.IdRol);
                cmd.Parameters.AddWithValue("@claveUsuario", clave);
                cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

                await cn.OpenAsync();
                await cmd.ExecuteReaderAsync();
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task ActualizarAsync(UsuarioEntity usuario, string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_ActualizarUsuario2", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUsuario", usuario.Id);
                cmd.Parameters.AddWithValue("@nombreCompleto", usuario.NombreCompleto);
                cmd.Parameters.AddWithValue("@correoElectronico", usuario.CorreoElectronico);
                cmd.Parameters.AddWithValue("@idRol", usuario.IdRol);
                cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

                await cn.OpenAsync();
                await cmd.ExecuteReaderAsync();
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }

        public async Task CambiarEstadoAsync(int id, bool activo, string usuarioApp)
        {
            try
            {
                using SqlConnection cn = Conexion.ObtenerConexion();
                using SqlCommand cmd = new("sp_CambiarEstadoUsuario2", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@activo", activo);
                cmd.Parameters.AddWithValue("@usuarioApp", usuarioApp);

                await cn.OpenAsync();
                await cmd.ExecuteReaderAsync();
            }
            catch (SqlException ex)
            {
                throw SqlErrorHelper.TraducirError(ex);
            }
        }
    }
}
