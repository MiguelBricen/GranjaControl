namespace GranjaControl.Entitys.Entidades
{
    public class DetalleVentaEntity
    {
        public const decimal PesoMaximo = 99999999.99m;

        public int Id { get; private set; }

        public int IdVenta { get; private set; }

        public string NumeroVenta { get; private set; } = string.Empty;

        public int IdLote { get; private set; }

        public string CodigoLote { get; private set; } = string.Empty;

        public int CantidadPollos { get; private set; }

        public decimal PesoLibras { get; private set; }

        public decimal PrecioPorLibra { get; private set; }

        public decimal Subtotal => decimal.Round(
            PesoLibras * PrecioPorLibra,
            2,
            MidpointRounding.AwayFromZero);

        public DetalleVentaEntity() { }

        public DetalleVentaEntity(
            int idVenta,
            int idLote,
            int cantidadPollos,
            decimal pesoLibras,
            decimal precioPorLibra)
        {
            AsignarVenta(idVenta);
            AsignarLote(idLote);
            CambiarCantidadPollos(cantidadPollos);
            CambiarPesoLibras(pesoLibras);
            CambiarPrecioPorLibra(precioPorLibra);
        }

        public void AsignarId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El ID no puede ser negativo.");

            Id = id;
        }

        public void AsignarVenta(
            int idVenta,
            string? numeroVenta = null)
        {
            if (idVenta < 0)
                throw new ArgumentException(
                    "El ID de la venta no puede ser negativo.");

            IdVenta = idVenta;

            if (!string.IsNullOrWhiteSpace(numeroVenta))
                NumeroVenta = numeroVenta.Trim();
        }

        public void AsignarLote(
            int idLote,
            string? codigoLote = null)
        {
            if (idLote <= 0)
                throw new ArgumentException(
                    "Debe seleccionar un lote válido.");

            IdLote = idLote;

            if (!string.IsNullOrWhiteSpace(codigoLote))
                CodigoLote = codigoLote.Trim();
        }

        public void CambiarCantidadPollos(int cantidadPollos)
        {
            if (cantidadPollos <= 0)
                throw new ArgumentException(
                    "La cantidad de pollos debe ser mayor que cero.");

            CantidadPollos = cantidadPollos;
        }

        public void CambiarPesoLibras(decimal pesoLibras)
        {
            if (pesoLibras <= 0)
                throw new ArgumentException(
                    "El peso total en libras debe ser mayor que cero.");

            if (pesoLibras > PesoMaximo)
                throw new ArgumentException(
                    "El peso total en libras es demasiado alto.");

            PesoLibras = decimal.Round(
                pesoLibras,
                2,
                MidpointRounding.AwayFromZero);
        }

        public void CambiarPrecioPorLibra(decimal precioPorLibra)
        {
            if (precioPorLibra <= 0)
                throw new ArgumentException(
                    "El precio por libra debe ser mayor que cero.");

            if (precioPorLibra > PesoMaximo)
                throw new ArgumentException(
                    "El precio por libra es demasiado alto.");

            PrecioPorLibra = decimal.Round(
                precioPorLibra,
                2,
                MidpointRounding.AwayFromZero);
        }
    }
}
