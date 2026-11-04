namespace GranjaControl.Entitys.Entidades
{
    public class CompraAlimentoEntity
    {
        public int Id { get; private set; }

        public string NumeroCompra { get; private set; } = string.Empty;

        public int IdProveedor { get; private set; }

        public string NombreProveedor { get; private set; } = string.Empty;

        public DateTime FechaCompra { get; private set; } = DateTime.Today;

        public string? Observacion { get; private set; }

        public CompraAlimentoEntity() { }

        public CompraAlimentoEntity(
            string numeroCompra,
            int idProveedor,
            DateTime fechaCompra,
            string? observacion)
        {
            CambiarNumeroCompra(numeroCompra);
            AsignarProveedor(idProveedor);
            CambiarFecha(fechaCompra);
            CambiarObservacion(observacion);
        }

        public void AsignarId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El ID no puede ser negativo.");

            Id = id;
        }

        public void CambiarNumeroCompra(string numeroCompra)
        {
            if (string.IsNullOrWhiteSpace(numeroCompra))
                throw new ArgumentNullException(
                    "Debe ingresar el número de compra.");

            string numero = numeroCompra.Trim();

            if (numero.Length > 25)
                throw new ArgumentException(
                    "El número de compra no puede superar los 25 caracteres.");

            NumeroCompra = numero;
        }

        public void AsignarProveedor(
            int idProveedor,
            string? nombreProveedor = null)
        {
            if (idProveedor <= 0)
                throw new ArgumentException(
                    "Debe seleccionar un proveedor válido.");

            IdProveedor = idProveedor;

            if (!string.IsNullOrWhiteSpace(nombreProveedor))
                NombreProveedor = nombreProveedor.Trim();
        }

        public void CambiarFecha(DateTime fechaCompra)
        {
            if (fechaCompra == default)
                throw new ArgumentException(
                    "Debe seleccionar una fecha válida.");

            FechaCompra = fechaCompra.Date;
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
