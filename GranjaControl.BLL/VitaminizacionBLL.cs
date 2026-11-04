using GranjaControl.DAL;
using GranjaControl.Entitys.Entidades;

namespace GranjaControl.BLL
{
    public class VitaminizacionBLL
    {
        private readonly VitaminizacionDAL _vitaminizacionDal;

        public VitaminizacionBLL(VitaminizacionDAL vitaminizacionDal)
        {
            _vitaminizacionDal = vitaminizacionDal;
        }

        public async Task<PaginacionEntity<VitaminizacionEntity>> MostrarAsync(
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

            return await _vitaminizacionDal.MostrarAsync(
                pagina,
                tamPagina,
                idLote);
        }

        public async Task<VitaminizacionEntity?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException(
                    "El ID de la vitaminización no es válido.");

            return await _vitaminizacionDal.ObtenerPorIdAsync(id);
        }

        public async Task AgregarAsync(
            VitaminizacionEntity vitaminizacion,
            string usuarioApp)
        {
            Validar(vitaminizacion, usuarioApp);

            await _vitaminizacionDal.AgregarAsync(
                vitaminizacion,
                usuarioApp);
        }

        public async Task ActualizarAsync(
            VitaminizacionEntity vitaminizacion,
            string usuarioApp)
        {
            if (vitaminizacion.Id <= 0)
                throw new ArgumentException(
                    "El ID de la vitaminización no es válido.");

            Validar(vitaminizacion, usuarioApp);

            await _vitaminizacionDal.ActualizarAsync(
                vitaminizacion,
                usuarioApp);
        }

        private static void Validar(
            VitaminizacionEntity vitaminizacion,
            string usuarioApp)
        {
            if (vitaminizacion.IdLote <= 0)
                throw new ArgumentException(
                    "Debe seleccionar un lote válido.");

            if (vitaminizacion.IdVitamina <= 0)
                throw new ArgumentException(
                    "Debe seleccionar una vitamina válida.");

            if (vitaminizacion.Fecha == default)
                throw new ArgumentException(
                    "Debe seleccionar una fecha válida.");

            if (vitaminizacion.Cantidad <= 0)
                throw new ArgumentException(
                    "La cantidad de vitamina debe ser mayor que cero.");

            if (vitaminizacion.Cantidad > 99999999.99m)
                throw new ArgumentException(
                    "La cantidad de vitamina es demasiado alta.");

            if (!VitaminizacionEntity.UnidadesPermitidas.Contains(
                    vitaminizacion.UnidadMedida,
                    StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    "La unidad de medida no es válida.");
            }

            if (vitaminizacion.Observacion?.Length > 250)
                throw new ArgumentException(
                    "La observación no puede superar los 250 caracteres.");

            if (string.IsNullOrWhiteSpace(usuarioApp))
                throw new ArgumentException(
                    "No se pudo identificar el usuario de la sesión.");
        }
    }
}
