using GranjaControl.DAL;
using GranjaControl.Entitys.Entidades;

namespace GranjaControl.BLL
{
    public class LoteBLL
    {
        private readonly LoteDAL _loteDal;

        public LoteBLL(LoteDAL loteDal)
        {
            _loteDal = loteDal;
        }

        public async Task<PaginacionEntity<LoteEntity>> MostrarAsync(
            int pagina,
            int tamPagina,
            string? buscar = null)
        {
            if (pagina < 1)
                pagina = 1;

            if (tamPagina < 1)
                tamPagina = 5;

            return await _loteDal.MostrarAsync(
                pagina,
                tamPagina,
                buscar);
        }

        public async Task<List<LoteEntity>> MostrarLotesActivosAsync()
        {
            return await _loteDal.MostrarLotesActivosAsync();
        }

        public async Task<LoteEntity?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del lote no es válido.");

            return await _loteDal.ObtenerPorIdAsync(id);
        }

        public async Task AgregarAsync(
            LoteEntity lote,
            string usuarioApp)
        {
            Validar(lote, usuarioApp);

            await _loteDal.AgregarAsync(lote, usuarioApp);
        }

        public async Task ActualizarAsync(
            LoteEntity lote,
            string usuarioApp)
        {
            if (lote.Id <= 0)
                throw new ArgumentException("El ID del lote no es válido.");

            Validar(lote, usuarioApp);

            await _loteDal.ActualizarAsync(lote, usuarioApp);
        }

        public async Task CambiarEstadoAsync(
            int id,
            string estado,
            string usuarioApp)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del lote no es válido.");

            if (string.IsNullOrWhiteSpace(estado) ||
                !LoteEntity.EstadosPermitidos.Contains(
                    estado.Trim(),
                    StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    "El estado del lote no es válido.");
            }

            if (string.IsNullOrWhiteSpace(usuarioApp))
                throw new ArgumentException(
                    "No se pudo identificar el usuario de la sesión.");

            await _loteDal.CambiarEstadoAsync(
                id,
                estado.Trim(),
                usuarioApp);
        }

        private static void Validar(
            LoteEntity lote,
            string usuarioApp)
        {
            if (string.IsNullOrWhiteSpace(lote.CodigoLote))
                throw new ArgumentException(
                    "Debe ingresar el código del lote.");

            if (lote.IdGalpon <= 0)
                throw new ArgumentException(
                    "Debe seleccionar un galpón válido.");

            if (lote.IdProveedor <= 0)
                throw new ArgumentException(
                    "Debe seleccionar un proveedor válido.");

            if (lote.CantidadInicial <= 0)
                throw new ArgumentException(
                    "La cantidad inicial de pollitos debe ser mayor que cero.");

            if (lote.CostoPorPollito < 0)
                throw new ArgumentException(
                    "El costo por pollito no puede ser negativo.");

            if (string.IsNullOrWhiteSpace(usuarioApp))
                throw new ArgumentException(
                    "No se pudo identificar el usuario de la sesión.");
        }
    }
}
