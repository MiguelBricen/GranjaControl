using GranjaControl.DAL;
using GranjaControl.Entitys.Entidades;

namespace GranjaControl.BLL
{
    public class VitaminaBLL
    {
        private readonly VitaminaDAL _vitaminaDal;

        public VitaminaBLL(VitaminaDAL vitaminaDal)
        {
            _vitaminaDal = vitaminaDal;
        }

        public async Task<PaginacionEntity<VitaminaEntity>> MostrarAsync(
            int pagina,
            int tamPagina,
            string? buscar = null)
        {
            if (pagina < 1)
                pagina = 1;

            if (tamPagina < 1)
                tamPagina = 5;

            return await _vitaminaDal.MostrarAsync(
                pagina,
                tamPagina,
                buscar);
        }

        public async Task<List<VitaminaEntity>> MostrarVitaminasActivasAsync()
        {
            return await _vitaminaDal.MostrarVitaminasActivasAsync();
        }

        public async Task<VitaminaEntity?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID de la vitamina no es válido.");

            return await _vitaminaDal.ObtenerPorIdAsync(id);
        }

        public async Task AgregarAsync(
            VitaminaEntity vitamina,
            string usuarioApp)
        {
            Validar(vitamina, usuarioApp);

            await _vitaminaDal.AgregarAsync(vitamina, usuarioApp);
        }

        public async Task ActualizarAsync(
            VitaminaEntity vitamina,
            string usuarioApp)
        {
            if (vitamina.Id <= 0)
                throw new ArgumentException("El ID de la vitamina no es válido.");

            Validar(vitamina, usuarioApp);

            await _vitaminaDal.ActualizarAsync(vitamina, usuarioApp);
        }

        public async Task CambiarEstadoAsync(
            int id,
            bool activo,
            string usuarioApp)
        {
            if (id <= 0)
                throw new ArgumentException("El ID de la vitamina no es válido.");

            if (string.IsNullOrWhiteSpace(usuarioApp))
                throw new ArgumentException(
                    "No se pudo identificar el usuario de la sesión.");

            await _vitaminaDal.CambiarEstadoAsync(
                id,
                activo,
                usuarioApp);
        }

        private static void Validar(
            VitaminaEntity vitamina,
            string usuarioApp)
        {
            if (string.IsNullOrWhiteSpace(vitamina.NombreVitamina))
                throw new ArgumentException(
                    "Debe ingresar el nombre de la vitamina.");

            if (string.IsNullOrWhiteSpace(usuarioApp))
                throw new ArgumentException(
                    "No se pudo identificar el usuario de la sesión.");
        }
    }
}
