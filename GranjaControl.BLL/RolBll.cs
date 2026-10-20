

using GranjaControl.DAL;
using GranjaControl.Entitys.Entidades;

namespace GranjaControl.BLL
{
    public class RolBll
    {
        private readonly RolDal _rolDal;

        public RolBll(RolDal rolDal)
        {
            _rolDal = rolDal;
        }

        public async Task<PaginacionEntity<RolEntity>> MostrarAsync(int pagina, int tamPagina, string? buscar = null)
        {
            return await _rolDal.MostrarAsync(pagina, tamPagina, buscar);
        }

        public async Task<List<RolEntity>> MostrarRolesActivosAsync()
        {
            return await _rolDal.MostrarRolesActivosAsync();
        }

        public async Task<RolEntity?> ObtenerPorIdAsync(int id)
        {
            return await _rolDal.ObtenerPorIdAsync(id);
        }

        public async Task AgregarAsync(RolEntity rol, string usuarioApp)
        {
            await _rolDal.AgregarAsync(rol, usuarioApp);
        }

        public async Task ActualizarAsync(RolEntity rol, string usuarioApp)
        {
            await _rolDal.ActualizarAsync(rol, usuarioApp);
        }

        public async Task CambiarEstadoAsync(int id, bool activo, string usuarioApp)
        {
            await _rolDal.CambiarEstadoAsync(id, activo, usuarioApp);
        }
    }
}
