namespace GranjaControl.Entitys.Entidades
{
    public class DetalleCompraAlimentoEntity
    {
        public const string UnidadCompra = "Sacos";

        public const decimal KilogramosPorLibra = 0.453592m;

        public static IReadOnlyList<string> UnidadesPermitidas { get; } =
            ["Sacos", "Libras", "Kilogramos"];

        public static IReadOnlyList<string> UnidadesPesoSacoPermitidas { get; } =
            ["Libras", "Kilogramos"];

        public int Id { get; private set; }

        public int IdCompra { get; private set; }

        public string NumeroCompra { get; private set; } = string.Empty;

        public int IdAlimento { get; private set; }

        public string NombreAlimento { get; private set; } = string.Empty;

        public string TipoAlimento { get; private set; } = string.Empty;

        public decimal Cantidad { get; private set; }

        public string UnidadMedida { get; private set; } = string.Empty;

        public decimal? PesoPorSaco { get; private set; }

        public string? UnidadPesoSaco { get; private set; }

        public decimal? PesoTotalLibras { get; private set; }

        public decimal? PesoTotalKilogramos { get; private set; }

        public decimal CostoUnitario { get; private set; }

        public decimal Subtotal { get; private set; }

        public bool EsPendienteRevision =>
            !UnidadMedida.Equals(UnidadCompra, StringComparison.OrdinalIgnoreCase) ||
            !PesoPorSaco.HasValue ||
            string.IsNullOrWhiteSpace(UnidadPesoSaco) ||
            !PesoTotalLibras.HasValue ||
            !PesoTotalKilogramos.HasValue;

        public string EstadoRevision => EsPendienteRevision
            ? "Pendiente de revisión"
            : "Validada";

        public DetalleCompraAlimentoEntity() { }

        public DetalleCompraAlimentoEntity(
            int idCompra,
            int idAlimento,
            decimal cantidad,
            decimal pesoPorSaco,
            string unidadPesoSaco,
            decimal costoUnitario)
        {
            AsignarCompra(idCompra);
            AsignarAlimento(idAlimento);
            CambiarCantidad(cantidad);
            CambiarUnidadMedida(UnidadCompra);
            CambiarPesoPorSaco(pesoPorSaco, unidadPesoSaco);
            CambiarCostoUnitario(costoUnitario);
        }

        public DetalleCompraAlimentoEntity(
            int idCompra,
            int idAlimento,
            decimal cantidad,
            string unidadMedida,
            decimal costoUnitario)
        {
            AsignarCompra(idCompra);
            AsignarAlimento(idAlimento);
            CambiarCantidad(cantidad);
            CambiarUnidadMedida(unidadMedida);
            CambiarPesoPorSaco(null, null);
            CambiarCostoUnitario(costoUnitario);
        }

        public void AsignarId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El ID no puede ser negativo.");

            Id = id;
        }

        public void AsignarCompra(
            int idCompra,
            string? numeroCompra = null)
        {
            if (idCompra < 0)
                throw new ArgumentException(
                    "El ID de la compra no puede ser negativo.");

            IdCompra = idCompra;

            if (!string.IsNullOrWhiteSpace(numeroCompra))
                NumeroCompra = numeroCompra.Trim();
        }

        public void AsignarAlimento(
            int idAlimento,
            string? nombreAlimento = null,
            string? tipoAlimento = null)
        {
            if (idAlimento <= 0)
                throw new ArgumentException(
                    "Debe seleccionar un alimento válido.");

            IdAlimento = idAlimento;

            if (!string.IsNullOrWhiteSpace(nombreAlimento))
                NombreAlimento = nombreAlimento.Trim();

            if (!string.IsNullOrWhiteSpace(tipoAlimento))
                TipoAlimento = tipoAlimento.Trim();
        }

        public void CambiarCantidad(decimal cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException(
                    "La cantidad de alimento debe ser mayor que cero.");

            if (cantidad > 99999999.99m)
                throw new ArgumentException(
                    "La cantidad de alimento es demasiado alta.");

            Cantidad = decimal.Round(
                cantidad,
                2,
                MidpointRounding.AwayFromZero);

            RecalcularValoresDerivados();
        }

        public void CambiarUnidadMedida(string unidadMedida)
        {
            if (string.IsNullOrWhiteSpace(unidadMedida))
                throw new ArgumentNullException(
                    "Debe seleccionar la unidad de medida.");

            string unidad = unidadMedida.Trim();

            if (!UnidadesPermitidas.Contains(
                    unidad,
                    StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    "La unidad de medida no es válida.");
            }

            UnidadMedida = UnidadesPermitidas.First(
                u => u.Equals(unidad, StringComparison.OrdinalIgnoreCase));

            RecalcularValoresDerivados();
        }

        public void CambiarPesoPorSaco(
            decimal? pesoPorSaco,
            string? unidadPesoSaco)
        {
            if (pesoPorSaco is null && string.IsNullOrWhiteSpace(unidadPesoSaco))
            {
                PesoPorSaco = null;
                UnidadPesoSaco = null;
                RecalcularValoresDerivados();
                return;
            }

            if (!pesoPorSaco.HasValue || pesoPorSaco.Value <= 0)
                throw new ArgumentException("El peso por saco debe ser mayor que cero.");

            if (pesoPorSaco.Value > 999999.9999m)
                throw new ArgumentException("El peso por saco es demasiado alto.");

            if (string.IsNullOrWhiteSpace(unidadPesoSaco))
                throw new ArgumentNullException("Debe seleccionar la unidad del peso por saco.");

            string unidad = unidadPesoSaco.Trim();

            if (!UnidadesPesoSacoPermitidas.Contains(
                    unidad,
                    StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException("La unidad del peso por saco no es válida.");
            }

            PesoPorSaco = decimal.Round(
                pesoPorSaco.Value,
                4,
                MidpointRounding.AwayFromZero);

            UnidadPesoSaco = UnidadesPesoSacoPermitidas.First(
                u => u.Equals(unidad, StringComparison.OrdinalIgnoreCase));

            RecalcularValoresDerivados();
        }

        public void CambiarCostoUnitario(decimal costoUnitario)
        {
            if (costoUnitario < 0)
                throw new ArgumentException(
                    "El costo unitario no puede ser negativo.");

            if (costoUnitario > 99999999.99m)
                throw new ArgumentException(
                    "El costo unitario es demasiado alto.");

            CostoUnitario = decimal.Round(
                costoUnitario,
                2,
                MidpointRounding.AwayFromZero);

            RecalcularValoresDerivados();
        }

        public void AsignarValoresDerivadosPersistidos(
            decimal? pesoTotalLibras,
            decimal? pesoTotalKilogramos,
            decimal? subtotal)
        {
            if (pesoTotalLibras is null &&
                pesoTotalKilogramos is null &&
                subtotal is null)
            {
                PesoTotalLibras = null;
                PesoTotalKilogramos = null;

                return;
            }

            PesoTotalLibras = pesoTotalLibras;
            PesoTotalKilogramos = pesoTotalKilogramos;

            if (subtotal.HasValue)
                Subtotal = decimal.Round(
                    subtotal.Value,
                    2,
                    MidpointRounding.AwayFromZero);
        }

        private void RecalcularValoresDerivados()
        {
            Subtotal = decimal.Round(
                Cantidad * CostoUnitario,
                2,
                MidpointRounding.AwayFromZero);

            if (!UnidadMedida.Equals(UnidadCompra, StringComparison.OrdinalIgnoreCase) ||
                !PesoPorSaco.HasValue ||
                string.IsNullOrWhiteSpace(UnidadPesoSaco))
            {
                PesoTotalLibras = null;
                PesoTotalKilogramos = null;
                return;
            }

            decimal pesoBase = Cantidad * PesoPorSaco.Value;

            if (UnidadPesoSaco.Equals("Libras", StringComparison.OrdinalIgnoreCase))
            {
                PesoTotalLibras = decimal.Round(
                    pesoBase,
                    2,
                    MidpointRounding.AwayFromZero);

                PesoTotalKilogramos = decimal.Round(
                    pesoBase * KilogramosPorLibra,
                    2,
                    MidpointRounding.AwayFromZero);
            }
            else
            {
                PesoTotalKilogramos = decimal.Round(
                    pesoBase,
                    2,
                    MidpointRounding.AwayFromZero);

                PesoTotalLibras = decimal.Round(
                    pesoBase / KilogramosPorLibra,
                    2,
                    MidpointRounding.AwayFromZero);
            }
        }
    }
}
