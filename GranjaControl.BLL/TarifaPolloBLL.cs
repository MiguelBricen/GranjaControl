using GranjaControl.DAL;
using GranjaControl.Entitys.Entidades;

namespace GranjaControl.BLL
{
    public class TarifaPolloBLL
    {
        private readonly TarifaPolloDAL _tarifaDal;

        public TarifaPolloBLL(TarifaPolloDAL tarifaDal)
        {
            _tarifaDal = tarifaDal;
        }

        public async Task<PaginacionEntity<TarifaPolloEntity>> MostrarAsync(
            int pagina,
            int tamPagina)
        {
            if (pagina < 1)
                pagina = 1;

            if (tamPagina < 1)
                tamPagina = 5;

            return await _tarifaDal.MostrarAsync(pagina, tamPagina);
        }

        public async Task<List<TarifaPolloEntity>> MostrarActivasAsync()
        {
            return await _tarifaDal.MostrarActivasAsync();
        }

        public async Task<TarifaPolloEntity?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException(
                    "El ID de la tarifa no es válido.");

            return await _tarifaDal.ObtenerPorIdAsync(id);
        }

        public async Task<int> AgregarAsync(
            TarifaPolloEntity tarifa,
            string usuarioApp)
        {
            Validar(tarifa, usuarioApp);

            return await _tarifaDal.AgregarAsync(tarifa, usuarioApp);
        }

        public async Task ActualizarAsync(
            TarifaPolloEntity tarifa,
            string usuarioApp)
        {
            if (tarifa.Id <= 0)
                throw new ArgumentException(
                    "El ID de la tarifa no es válido.");

            Validar(tarifa, usuarioApp);

            await _tarifaDal.ActualizarAsync(tarifa, usuarioApp);
        }

        public async Task CambiarEstadoAsync(
            int id,
            bool activo,
            string usuarioApp)
        {
            if (id <= 0)
                throw new ArgumentException(
                    "El ID de la tarifa no es válido.");

            if (string.IsNullOrWhiteSpace(usuarioApp))
                throw new ArgumentException(
                    "No se pudo identificar el usuario de la sesión.");

            await _tarifaDal.CambiarEstadoAsync(
                id,
                activo,
                usuarioApp);
        }

        private static void Validar(
            TarifaPolloEntity tarifa,
            string usuarioApp)
        {
            if (tarifa.PrecioPorLibra <= 0)
                throw new ArgumentException(
                    "El precio por libra debe ser mayor que cero.");

            if (tarifa.PrecioPorLibra > TarifaPolloEntity.PrecioMaximo)
                throw new ArgumentException(
                    "El precio por libra no puede superar los 99,999,999.99.");

            if (tarifa.FechaInicio == default)
                throw new ArgumentException(
                    "Debe seleccionar una fecha de inicio válida.");

            if (tarifa.FechaFin.HasValue &&
                tarifa.FechaFin.Value < tarifa.FechaInicio)
            {
                throw new ArgumentException(
                    "La fecha fin no puede ser anterior a la fecha inicio.");
            }

            if (string.IsNullOrWhiteSpace(usuarioApp))
                throw new ArgumentException(
                    "No se pudo identificar el usuario de la sesión.");
        }
    }
}
