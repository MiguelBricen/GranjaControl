using GranjaControl.DAL;
using GranjaControl.Entitys.Entidades;

namespace GranjaControl.BLL
{
    public class UsuarioBll
    {
        private readonly UsuarioDal _usuarioDal;

        public UsuarioBll(UsuarioDal usuarioDal)
        {
            _usuarioDal = usuarioDal;
        }

        public async Task<PaginacionEntity<UsuarioEntity>> MostrarAsync(int pagina, int tamPagina, string? buscar = null)
        {
            return await _usuarioDal.MostrarAsync(pagina, tamPagina, buscar);
        }

        public async Task<UsuarioEntity?> ObtenerPorIdAsync(int id)
        {
            return await _usuarioDal.ObtenerPorIdAsync(id);
        }

        public async Task AgregarAsync(UsuarioEntity usuario, string clave, string usuarioApp)
        {
            await _usuarioDal.AgregarAsync(usuario, clave, usuarioApp);
        }

        public async Task ActualizarAsync(UsuarioEntity usuario, string usuarioApp)
        {
            await _usuarioDal.ActualizarAsync(usuario, usuarioApp);
        }

        public async Task CambiarEstadoAsync(int id, bool activo, string usuarioApp)
        {
            await _usuarioDal.CambiarEstadoAsync(id, activo, usuarioApp);
        }
    }
}
