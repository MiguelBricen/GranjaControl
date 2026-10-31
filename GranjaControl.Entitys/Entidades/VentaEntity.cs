namespace GranjaControl.Entitys.Entidades
{
    public class VentaEntity
    {
        public static IReadOnlyList<string> EstadosPermitidos { get; } =
            ["Registrada", "Anulada"];

        public static string EstadoInicial => EstadosPermitidos[0];

        public int Id { get; private set; }

        public string NumeroVenta { get; private set; } = string.Empty;

        public int IdCliente { get; private set; }

        public string NombreCliente { get; private set; } = string.Empty;

        public DateTime FechaVenta { get; private set; } = DateTime.Today;

        public string? Observacion { get; private set; }

        public string Estado { get; private set; } = EstadoInicial;

        public int CantidadPollos { get; private set; }

        public decimal PesoTotalLibras { get; private set; }

        public decimal TotalVenta { get; private set; }

        public VentaEntity() { }

        public VentaEntity(
            string numeroVenta,
            int idCliente,
            DateTime fechaVenta,
            string? observacion)
        {
            CambiarNumeroVenta(numeroVenta);
            AsignarCliente(idCliente);
            CambiarFecha(fechaVenta);
            CambiarObservacion(observacion);
        }

        public void AsignarId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El ID no puede ser negativo.");

            Id = id;
        }

        public void CambiarNumeroVenta(string numeroVenta)
        {
            if (string.IsNullOrWhiteSpace(numeroVenta))
                throw new ArgumentNullException(
                    "Debe ingresar el número de venta.");

            string numero = numeroVenta.Trim();

            if (numero.Length > 50)
                throw new ArgumentException(
                    "El número de venta no puede superar los 50 caracteres.");

            NumeroVenta = numero;
        }

        public void AsignarCliente(
            int idCliente,
            string? nombreCliente = null)
        {
            if (idCliente <= 0)
                throw new ArgumentException(
                    "Debe seleccionar un cliente válido.");

            IdCliente = idCliente;

            if (!string.IsNullOrWhiteSpace(nombreCliente))
                NombreCliente = nombreCliente.Trim();
        }

        public void CambiarFecha(DateTime fechaVenta)
        {
            if (fechaVenta == default)
                throw new ArgumentException(
                    "Debe seleccionar una fecha válida.");

            FechaVenta = fechaVenta.Date;
        }

        public void CambiarObservacion(string? observacion)
        {
            string? valor = string.IsNullOrWhiteSpace(observacion)
                ? null
                : observacion.Trim();

            if (valor?.Length > 500)
                throw new ArgumentException(
                    "La observación no puede superar los 500 caracteres.");

            Observacion = valor;
        }

        public void CambiarEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                throw new ArgumentNullException(
                    "Debe indicar el estado de la venta.");

            string valor = estado.Trim();

            if (!EstadosPermitidos.Contains(
                    valor,
                    StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    "El estado de la venta no es válido.");
            }

            Estado = EstadosPermitidos.First(
                e => e.Equals(valor, StringComparison.OrdinalIgnoreCase));
        }

        public void EstablecerTotales(
            int cantidadPollos,
            decimal pesoTotalLibras,
            decimal totalVenta)
        {
            if (cantidadPollos < 0)
                throw new ArgumentException(
                    "La cantidad de pollos no puede ser negativa.");

            if (pesoTotalLibras < 0)
                throw new ArgumentException(
                    "El peso total no puede ser negativo.");

            if (totalVenta < 0)
                throw new ArgumentException(
                    "El total de la venta no puede ser negativo.");

            CantidadPollos = cantidadPollos;
            PesoTotalLibras = pesoTotalLibras;
            TotalVenta = totalVenta;
        }
    }
}
