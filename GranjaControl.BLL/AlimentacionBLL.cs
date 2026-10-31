using GranjaControl.DAL;
using GranjaControl.Entitys.Entidades;

namespace GranjaControl.BLL
{
    public class AlimentacionBLL
    {
        private readonly AlimentacionDAL _alimentacionDal;

        public AlimentacionBLL(AlimentacionDAL alimentacionDal)
        {
            _alimentacionDal = alimentacionDal;
        }

        public async Task<PaginacionEntity<AlimentacionEntity>> MostrarAsync(
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

            return await _alimentacionDal.MostrarAsync(
                pagina,
                tamPagina,
                idLote);
        }

        public async Task<AlimentacionEntity?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException(
                    "El ID de la alimentación no es válido.");

            return await _alimentacionDal.ObtenerPorIdAsync(id);
        }

        public async Task AgregarAsync(
            AlimentacionEntity alimentacion,
            string usuarioApp)
        {
            Validar(alimentacion, usuarioApp);

            await _alimentacionDal.AgregarAsync(
                alimentacion,
                usuarioApp);
        }

        public async Task ActualizarAsync(
            AlimentacionEntity alimentacion,
            string usuarioApp)
        {
            if (alimentacion.Id <= 0)
                throw new ArgumentException(
                    "El ID de la alimentación no es válido.");

            Validar(alimentacion, usuarioApp);

            await _alimentacionDal.ActualizarAsync(
                alimentacion,
                usuarioApp);
        }

        private static void Validar(
            AlimentacionEntity alimentacion,
            string usuarioApp)
        {
            if (alimentacion.IdLote <= 0)
                throw new ArgumentException(
                    "Debe seleccionar un lote válido.");

            if (alimentacion.IdAlimento <= 0)
                throw new ArgumentException(
                    "Debe seleccionar un alimento válido.");

            if (alimentacion.Fecha == default)
                throw new ArgumentException(
                    "Debe seleccionar una fecha válida.");

            if (alimentacion.Cantidad <= 0)
                throw new ArgumentException(
                    "La cantidad de alimento debe ser mayor que cero.");

            if (!AlimentacionEntity.UnidadesPermitidas.Contains(
                    alimentacion.UnidadMedida,
                    StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    "La unidad de medida no es válida.");
            }

            if (alimentacion.Observacion?.Length > 250)
                throw new ArgumentException(
                    "La observación no puede superar los 250 caracteres.");

            if (string.IsNullOrWhiteSpace(usuarioApp))
                throw new ArgumentException(
                    "No se pudo identificar el usuario de la sesión.");
        }
    }
}
