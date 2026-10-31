namespace GranjaControl.Entitys.Entidades
{
    public class DetalleCompraAlimentoEntity
    {
        public static IReadOnlyList<string> UnidadesPermitidas { get; } =
            ["Libras", "Kilogramos"];

        public int Id { get; private set; }

        public int IdCompra { get; private set; }

        public string NumeroCompra { get; private set; } = string.Empty;

        public int IdAlimento { get; private set; }

        public string NombreAlimento { get; private set; } = string.Empty;

        public string TipoAlimento { get; private set; } = string.Empty;

        public decimal Cantidad { get; private set; }

        public string UnidadMedida { get; private set; } = string.Empty;

        public decimal CostoUnitario { get; private set; }

        public decimal Subtotal => decimal.Round(
            Cantidad * CostoUnitario,
            2,
            MidpointRounding.AwayFromZero);

        public DetalleCompraAlimentoEntity() { }

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
        }
    }
}
