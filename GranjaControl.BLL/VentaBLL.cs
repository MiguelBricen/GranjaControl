using GranjaControl.DAL;
using GranjaControl.Entitys.Entidades;

namespace GranjaControl.BLL
{
    public class VentaBLL
    {
        private readonly VentaDAL _ventaDal;

        public VentaBLL(VentaDAL ventaDal)
        {
            _ventaDal = ventaDal;
        }

        public async Task<PaginacionEntity<VentaEntity>> MostrarAsync(
            int pagina,
            int tamPagina,
            string? buscar = null)
        {
            if (pagina < 1)
                pagina = 1;

            if (tamPagina < 1)
                tamPagina = 5;

            return await _ventaDal.MostrarAsync(
                pagina,
                tamPagina,
                buscar);
        }

        public async Task<VentaEntity?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException(
                    "El ID de la venta no es válido.");

            return await _ventaDal.ObtenerPorIdAsync(id);
        }

        public async Task<List<DetalleVentaEntity>> ObtenerDetallesAsync(
            int idVenta)
        {
            if (idVenta <= 0)
                throw new ArgumentException(
                    "El ID de la venta no es válido.");

            return await _ventaDal.MostrarDetallesAsync(idVenta);
        }

        public async Task<int> GuardarAsync(
            VentaEntity venta,
            IReadOnlyList<DetalleVentaEntity> detalles,
            string usuarioApp)
        {
            ArgumentNullException.ThrowIfNull(venta);
            ArgumentNullException.ThrowIfNull(detalles);

            ValidarVenta(venta, detalles, usuarioApp);

            return await _ventaDal.GuardarAsync(
                venta,
                detalles,
                usuarioApp);
        }

        public async Task AnularAsync(int id, string usuarioApp)
        {
            if (id <= 0)
                throw new ArgumentException(
                    "El ID de la venta no es válido.");

            if (string.IsNullOrWhiteSpace(usuarioApp))
                throw new ArgumentException(
                    "No se pudo identificar el usuario de la sesión.");

            await _ventaDal.AnularAsync(id, usuarioApp);
        }

        public static int CalcularCantidadPollos(
            IEnumerable<DetalleVentaEntity> detalles)
        {
            return detalles.Sum(d => d.CantidadPollos);
        }

        public static decimal CalcularPesoTotal(
            IEnumerable<DetalleVentaEntity> detalles)
        {
            return decimal.Round(
                detalles.Sum(d => d.PesoLibras),
                2,
                MidpointRounding.AwayFromZero);
        }

        public static decimal CalcularTotal(
            IEnumerable<DetalleVentaEntity> detalles)
        {
            return decimal.Round(
                detalles.Sum(d => d.Subtotal),
                2,
                MidpointRounding.AwayFromZero);
        }

        private static void ValidarVenta(
            VentaEntity venta,
            IReadOnlyList<DetalleVentaEntity> detalles,
            string usuarioApp)
        {
            if (string.IsNullOrWhiteSpace(venta.NumeroVenta))
                throw new ArgumentException(
                    "Debe ingresar el número de venta.");

            if (venta.NumeroVenta.Length > 50)
                throw new ArgumentException(
                    "El número de venta no puede superar los 50 caracteres.");

            if (venta.IdCliente <= 0)
                throw new ArgumentException(
                    "Debe seleccionar un cliente válido.");

            if (venta.FechaVenta == default)
                throw new ArgumentException(
                    "Debe seleccionar una fecha válida.");

            if (venta.Observacion?.Length > 500)
                throw new ArgumentException(
                    "La observación no puede superar los 500 caracteres.");

            if (venta.Estado.Equals(
                    "Anulada",
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    "Una venta anulada no puede modificarse.");
            }

            if (detalles.Count == 0)
                throw new ArgumentException(
                    "Debe agregar al menos un lote a la venta.");

            HashSet<int> lotes = [];

            foreach (DetalleVentaEntity detalle in detalles)
            {
                ValidarDetalle(detalle, venta.Id);

                if (!lotes.Add(detalle.IdLote))
                {
                    throw new ArgumentException(
                        "No se puede repetir el mismo lote en la venta.");
                }
            }

            if (string.IsNullOrWhiteSpace(usuarioApp))
                throw new ArgumentException(
                    "No se pudo identificar el usuario de la sesión.");
        }

        private static void ValidarDetalle(
            DetalleVentaEntity detalle,
            int idVenta)
        {
            if (detalle.IdLote <= 0)
                throw new ArgumentException(
                    "Debe seleccionar un lote válido.");

            if (detalle.CantidadPollos <= 0)
                throw new ArgumentException(
                    "La cantidad de pollos debe ser mayor que cero.");

            if (detalle.PesoLibras <= 0)
                throw new ArgumentException(
                    "El peso total en libras debe ser mayor que cero.");

            if (detalle.PesoLibras > DetalleVentaEntity.PesoMaximo)
                throw new ArgumentException(
                    "El peso total en libras es demasiado alto.");

            if (detalle.PrecioPorLibra <= 0)
                throw new ArgumentException(
                    "No existe un precio por libra válido para la venta.");

            if (detalle.PrecioPorLibra > DetalleVentaEntity.PesoMaximo)
                throw new ArgumentException(
                    "El precio por libra es demasiado alto.");

            if (idVenta <= 0 && detalle.Id > 0)
            {
                throw new ArgumentException(
                    "No se pueden incluir detalles existentes en una venta nueva.");
            }

            if (detalle.Id > 0 && detalle.IdVenta != idVenta)
            {
                throw new ArgumentException(
                    "Uno de los detalles no pertenece a la venta seleccionada.");
            }
        }
    }
}
