using GranjaControl.DAL;
using GranjaControl.Entitys.Entidades;

namespace GranjaControl.BLL
{
    public class MortalidadBLL
    {
        private readonly MortalidadDAL _mortalidadDal;

        public MortalidadBLL(MortalidadDAL mortalidadDal)
        {
            _mortalidadDal = mortalidadDal;
        }

        public async Task<PaginacionEntity<MortalidadEntity>> MostrarAsync(
            int pagina,
            int tamPagina,
            int? idLote = null)
        {
            if (pagina < 1)
                pagina = 1;

            if (tamPagina < 1)
                tamPagina = 5;

            if (idLote <= 0)
                idLote = null;

            return await _mortalidadDal.MostrarAsync(
                pagina,
                tamPagina,
                idLote);
        }

        public async Task<MortalidadEntity?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException(
                    "El ID de la mortalidad no es válido.");

            return await _mortalidadDal.ObtenerPorIdAsync(id);
        }

        public async Task AgregarAsync(
            MortalidadEntity mortalidad,
            string usuarioApp)
        {
            Validar(mortalidad, usuarioApp);

            await _mortalidadDal.AgregarAsync(
                mortalidad,
                usuarioApp);
        }

        public async Task ActualizarAsync(
            MortalidadEntity mortalidad,
            string usuarioApp)
        {
            if (mortalidad.Id <= 0)
                throw new ArgumentException(
                    "El ID de la mortalidad no es válido.");

            Validar(mortalidad, usuarioApp);

            await _mortalidadDal.ActualizarAsync(
                mortalidad,
                usuarioApp);
        }

        private static void Validar(
            MortalidadEntity mortalidad,
            string usuarioApp)
        {
            if (mortalidad.IdLote <= 0)
                throw new ArgumentException(
                    "Debe seleccionar un lote válido.");

            if (mortalidad.Fecha == default)
                throw new ArgumentException(
                    "Debe seleccionar una fecha válida.");

            if (mortalidad.Cantidad <= 0)
                throw new ArgumentException(
                    "La cantidad de mortalidad debe ser mayor que cero.");

            if (string.IsNullOrWhiteSpace(mortalidad.Causa))
                throw new ArgumentException(
                    "La causa de mortalidad es obligatoria.");

            if (mortalidad.Causa.Length > 150)
                throw new ArgumentException(
                    "La causa no puede superar los 150 caracteres.");

            if (mortalidad.Observacion?.Length > 250)
                throw new ArgumentException(
                    "La observación no puede superar los 250 caracteres.");

            if (string.IsNullOrWhiteSpace(usuarioApp))
                throw new ArgumentException(
                    "No se pudo identificar el usuario de la sesión.");
        }
    }
}
