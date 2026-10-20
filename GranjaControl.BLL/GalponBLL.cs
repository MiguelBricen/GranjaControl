using GranjaControl.DAL;
using GranjaControl.Entitys;
using GranjaControl.Entitys.Entidades;

namespace GranjaControl.BLL
{
    public class GalponBLL
    {
        private readonly GalponDAL _galponDal;

        public GalponBLL(GalponDAL galponDAL)
        {
            _galponDal = galponDAL;
        }

     
        public async Task<PaginacionEntity<GalponEntity>> MostrarAsync(
            int pagina,
            int tamPagina,
            string? buscar = null)
        {
            return await _galponDal.MostrarAsync(
                pagina,
                tamPagina,
                buscar);
        }

        public async Task<List<GalponEntity>> MostrarGalponesActivosAsync()
        {
            return await _galponDal.MostrarGalponesActivosAsync();
        }

        public async Task<GalponEntity?> ObtenerPorIdAsync(int id)
        {
            return await _galponDal.ObtenerPorIdAsync(id);
        }

        public async Task AgregarAsync(
            GalponEntity galpon,
            string usuarioApp)
        {
            await _galponDal.AgregarAsync(galpon, usuarioApp);
        }

        public async Task ActualizarAsync(
            GalponEntity galpon,
            string usuarioApp)
        {
            await _galponDal.ActualizarAsync(galpon, usuarioApp);
        }

        public async Task CambiarEstadoAsync(
            int id,
            bool activo,
            string usuarioApp)
        {
            await _galponDal.CambiarEstadoAsync(
                id,
                activo,
                usuarioApp);
        }
    }
}
