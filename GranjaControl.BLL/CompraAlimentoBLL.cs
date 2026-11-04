using GranjaControl.DAL;
using GranjaControl.Entitys.Entidades;

namespace GranjaControl.BLL
{
    public class CompraAlimentoBLL
    {
        private readonly CompraAlimentoDAL _compraDal;
        private readonly DetalleCompraAlimentoDAL _detalleDal;

        public CompraAlimentoBLL(
            CompraAlimentoDAL compraDal,
            DetalleCompraAlimentoDAL detalleDal)
        {
            _compraDal = compraDal;
            _detalleDal = detalleDal;
        }

        public async Task<PaginacionEntity<CompraAlimentoEntity>> MostrarAsync(
            int pagina,
            int tamPagina,
            string? buscar = null)
        {
            if (pagina < 1)
                pagina = 1;

            if (tamPagina < 1)
                tamPagina = 5;

            return await _compraDal.MostrarAsync(
                pagina,
                tamPagina,
                buscar);
        }

        public async Task<CompraAlimentoEntity?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException(
                    "El ID de la compra no es válido.");

            return await _compraDal.ObtenerPorIdAsync(id);
        }

        public async Task<List<DetalleCompraAlimentoEntity>> ObtenerDetallesAsync(
            int idCompra)
        {
            if (idCompra <= 0)
                throw new ArgumentException(
                    "El ID de la compra no es válido.");

            return await _detalleDal.MostrarPorCompraAsync(idCompra);
        }

        public async Task<int> GuardarAsync(
            CompraAlimentoEntity compra,
            IReadOnlyList<DetalleCompraAlimentoEntity> detalles,
            string usuarioApp)
        {
            ArgumentNullException.ThrowIfNull(compra);
            ArgumentNullException.ThrowIfNull(detalles);

            ValidarCompra(compra, detalles, usuarioApp);

            return await _compraDal.GuardarAsync(
                compra,
                detalles,
                usuarioApp);
        }

        public static decimal CalcularTotal(
            IEnumerable<DetalleCompraAlimentoEntity> detalles)
        {
            decimal total = 0;

            foreach (DetalleCompraAlimentoEntity detalle in detalles)
                total += detalle.Subtotal;

            return decimal.Round(
                total,
                2,
                MidpointRounding.AwayFromZero);
        }

        private static void ValidarCompra(
            CompraAlimentoEntity compra,
            IReadOnlyList<DetalleCompraAlimentoEntity> detalles,
            string usuarioApp)
        {
            if (string.IsNullOrWhiteSpace(compra.NumeroCompra))
                throw new ArgumentException(
                    "Debe ingresar el número de compra.");

            if (compra.NumeroCompra.Length > 25)
                throw new ArgumentException(
                    "El número de compra no puede superar los 25 caracteres.");

            if (compra.IdProveedor <= 0)
                throw new ArgumentException(
                    "Debe seleccionar un proveedor válido.");

            if (compra.FechaCompra == default)
                throw new ArgumentException(
                    "Debe seleccionar una fecha válida.");

            if (compra.Observacion?.Length > 250)
                throw new ArgumentException(
                    "La observación no puede superar los 250 caracteres.");

            if (detalles.Count == 0)
                throw new ArgumentException(
                    "Debe agregar al menos un alimento a la compra.");

            foreach (DetalleCompraAlimentoEntity detalle in detalles)
                ValidarDetalle(detalle, compra.Id);

            if (string.IsNullOrWhiteSpace(usuarioApp))
                throw new ArgumentException(
                    "No se pudo identificar el usuario de la sesión.");
        }

        private static void ValidarDetalle(
            DetalleCompraAlimentoEntity detalle,
            int idCompra)
        {
            if (detalle.IdAlimento <= 0)
                throw new ArgumentException(
                    "Debe seleccionar un alimento válido.");

            if (detalle.Cantidad <= 0)
                throw new ArgumentException(
                    "La cantidad de alimento debe ser mayor que cero.");

            if (detalle.Cantidad > 99999999.99m)
                throw new ArgumentException(
                    "La cantidad de alimento es demasiado alta.");

            if (detalle.CostoUnitario < 0)
                throw new ArgumentException(
                    "El costo unitario no puede ser negativo.");

            if (detalle.CostoUnitario > 99999999.99m)
                throw new ArgumentException(
                    "El costo unitario es demasiado alto.");

            if (detalle.EsPendienteRevision)
            {
                if (detalle.Id <= 0)
                    throw new ArgumentException(
                        "Los detalles nuevos deben registrarse por sacos con el peso de cada saco identificado.");

                if (detalle.IdCompra != idCompra)
                    throw new ArgumentException(
                        "Uno de los detalles no pertenece a la compra seleccionada.");

                if (!DetalleCompraAlimentoEntity.UnidadesPesoSacoPermitidas.Contains(
                        detalle.UnidadMedida,
                        StringComparer.OrdinalIgnoreCase) ||
                    detalle.PesoPorSaco.HasValue ||
                    detalle.UnidadPesoSaco is not null ||
                    detalle.PesoTotalLibras.HasValue ||
                    detalle.PesoTotalKilogramos.HasValue)
                {
                    throw new ArgumentException(
                        "Un detalle histórico solo puede editarse conservando una unidad de peso y sin asignarle valores de saco.");
                }

                return;
            }

            if (!detalle.UnidadMedida.Equals(
                    DetalleCompraAlimentoEntity.UnidadCompra,
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    "Las compras nuevas deben registrarse en sacos.");
            }

            if (!detalle.PesoPorSaco.HasValue ||
                detalle.PesoPorSaco.Value <= 0 ||
                string.IsNullOrWhiteSpace(detalle.UnidadPesoSaco))
            {
                throw new ArgumentException(
                    "Debe indicar el peso y la unidad de cada saco.");
            }

            if (idCompra <= 0 && detalle.Id > 0)
                throw new ArgumentException(
                    "No se pueden incluir detalles existentes en una compra nueva.");

            if (detalle.Id > 0 && detalle.IdCompra != idCompra)
                throw new ArgumentException(
                    "Uno de los detalles no pertenece a la compra seleccionada.");
        }
    }
}
