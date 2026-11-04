using GranjaControl.DAL;
using GranjaControl.Entitys.Entidades;

namespace GranjaControl.BLL
{
    public class AlimentoBLL
    {
        private readonly AlimentoDAL _alimentoDal;

        public AlimentoBLL(AlimentoDAL alimentoDal)
        {
            _alimentoDal = alimentoDal;
        }

        public async Task<PaginacionEntity<AlimentoEntity>> MostrarAsync(
            int pagina,
            int tamPagina,
            string? buscar = null)
        {
            if (pagina < 1)
                pagina = 1;

            if (tamPagina < 1)
                tamPagina = 5;

            return await _alimentoDal.MostrarAsync(
                pagina,
                tamPagina,
                buscar);
        }

        public async Task<List<AlimentoEntity>> MostrarAlimentosActivosAsync()
        {
            return await _alimentoDal.MostrarAlimentosActivosAsync();
        }

        public async Task<AlimentoEntity?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del alimento no es válido.");

            return await _alimentoDal.ObtenerPorIdAsync(id);
        }

        public async Task AgregarAsync(
            AlimentoEntity alimento,
            string usuarioApp)
        {
            Validar(alimento, usuarioApp);

            await _alimentoDal.AgregarAsync(alimento, usuarioApp);
        }

        public async Task ActualizarAsync(
            AlimentoEntity alimento,
            string usuarioApp)
        {
            if (alimento.Id <= 0)
                throw new ArgumentException("El ID del alimento no es válido.");

            Validar(alimento, usuarioApp);

            await _alimentoDal.ActualizarAsync(alimento, usuarioApp);
        }

        public async Task CambiarEstadoAsync(
            int id,
            bool activo,
            string usuarioApp)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del alimento no es válido.");

            if (string.IsNullOrWhiteSpace(usuarioApp))
                throw new ArgumentException(
                    "No se pudo identificar el usuario de la sesión.");

            await _alimentoDal.CambiarEstadoAsync(
                id,
                activo,
                usuarioApp);
        }

        private static void Validar(
            AlimentoEntity alimento,
            string usuarioApp)
        {
            if (string.IsNullOrWhiteSpace(alimento.NombreAlimento))
                throw new ArgumentException(
                    "Debe ingresar el nombre del alimento.");

            if (!AlimentoEntity.TiposPermitidos.Contains(
                    alimento.TipoAlimento,
                    StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    "El tipo de alimento no es válido.");
            }

            if (string.IsNullOrWhiteSpace(usuarioApp))
                throw new ArgumentException(
                    "No se pudo identificar el usuario de la sesión.");
        }
    }
}
