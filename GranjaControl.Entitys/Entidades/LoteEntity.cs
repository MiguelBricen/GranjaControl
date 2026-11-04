namespace GranjaControl.Entitys.Entidades
{
    public class LoteEntity
    {
        public static IReadOnlyList<string> EstadosPermitidos { get; } =
            ["En producción", "Listo para venta", "Finalizado", "Cancelado"];

        public static string EstadoInicial => EstadosPermitidos[0];

        public int Id { get; private set; }

        public string CodigoLote { get; private set; } = string.Empty;

        public int IdGalpon { get; private set; }

        public string NombreGalpon { get; private set; } = string.Empty;

        public int IdProveedor { get; private set; }

        public string NombreProveedor { get; private set; } = string.Empty;

        public DateTime FechaIngreso { get; private set; } = DateTime.Today;

        public int CantidadInicial { get; private set; }

        public decimal CostoPorPollito { get; private set; }

        public string Estado { get; private set; } = EstadoInicial;

        public string? Observacion { get; private set; }

        public LoteEntity() { }

        public LoteEntity(
            string codigoLote,
            int idGalpon,
            int idProveedor,
            DateTime fechaIngreso,
            int cantidadInicial,
            decimal costoPorPollito,
            string? observacion)
        {
            CambiarCodigo(codigoLote);
            AsignarGalpon(idGalpon);
            AsignarProveedor(idProveedor);
            CambiarFechaIngreso(fechaIngreso);
            CambiarCantidadInicial(cantidadInicial);
            CambiarCostoPorPollito(costoPorPollito);
            CambiarObservacion(observacion);
        }

        public void AsignarId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El ID no puede ser negativo.");

            Id = id;
        }

        public void CambiarCodigo(string codigoLote)
        {
            if (string.IsNullOrWhiteSpace(codigoLote))
                throw new ArgumentNullException(
                    "Debe ingresar el código del lote.");

            string codigo = codigoLote.Trim();

            if (codigo.Length > 25)
                throw new ArgumentException(
                    "El código del lote no puede superar los 25 caracteres.");

            CodigoLote = codigo;
        }

        public void AsignarGalpon(int idGalpon, string? nombreGalpon = null)
        {
            if (idGalpon <= 0)
                throw new ArgumentException(
                    "Debe seleccionar un galpón válido.");

            IdGalpon = idGalpon;

            if (nombreGalpon is not null)
                NombreGalpon = nombreGalpon.Trim();
        }

        public void AsignarProveedor(int idProveedor, string? nombreProveedor = null)
        {
            if (idProveedor <= 0)
                throw new ArgumentException(
                    "Debe seleccionar un proveedor válido.");

            IdProveedor = idProveedor;

            if (nombreProveedor is not null)
                NombreProveedor = nombreProveedor.Trim();
        }

        public void CambiarFechaIngreso(DateTime fechaIngreso)
        {
            FechaIngreso = fechaIngreso.Date;
        }

        public void CambiarCantidadInicial(int cantidadInicial)
        {
            if (cantidadInicial <= 0)
                throw new ArgumentException(
                    "La cantidad inicial de pollitos debe ser mayor que cero.");

            CantidadInicial = cantidadInicial;
        }

        public void CambiarCostoPorPollito(decimal costoPorPollito)
        {
            if (costoPorPollito < 0)
                throw new ArgumentException(
                    "El costo por pollito no puede ser negativo.");

            CostoPorPollito = costoPorPollito;
        }

        public void CambiarEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                throw new ArgumentNullException(
                    "Debe seleccionar un estado para el lote.");

            string valor = estado.Trim();

            if (!EstadosPermitidos.Contains(valor, StringComparer.OrdinalIgnoreCase))
                throw new ArgumentException(
                    "El estado del lote no es válido.");

            Estado = EstadosPermitidos.First(
                e => e.Equals(valor, StringComparison.OrdinalIgnoreCase));
        }

        public void CambiarObservacion(string? observacion)
        {
            string? valor = string.IsNullOrWhiteSpace(observacion)
                ? null
                : observacion.Trim();

            if (valor?.Length > 250)
                throw new ArgumentException(
                    "La observación no puede superar los 250 caracteres.");

            Observacion = valor;
        }
    }
}
