namespace GranjaControl.Entitys.Entidades
{
    public class MortalidadEntity
    {
        public int Id { get; private set; }

        public int IdLote { get; private set; }

        public string CodigoLote { get; private set; } = string.Empty;

        public DateTime Fecha { get; private set; } = DateTime.Today;

        public int Cantidad { get; private set; }

        public string Causa { get; private set; } = string.Empty;

        public string? Observacion { get; private set; }

        public MortalidadEntity() { }

        public MortalidadEntity(
            int idLote,
            DateTime fecha,
            int cantidad,
            string causa,
            string? observacion)
        {
            AsignarLote(idLote);
            CambiarFecha(fecha);
            CambiarCantidad(cantidad);
            CambiarCausa(causa);
            CambiarObservacion(observacion);
        }

        public void AsignarId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El ID no puede ser negativo.");

            Id = id;
        }

        public void AsignarLote(int idLote, string? codigoLote = null)
        {
            if (idLote <= 0)
                throw new ArgumentException(
                    "Debe seleccionar un lote válido.");

            IdLote = idLote;

            if (!string.IsNullOrWhiteSpace(codigoLote))
                CodigoLote = codigoLote.Trim();
        }

        public void CambiarFecha(DateTime fecha)
        {
            if (fecha == default)
                throw new ArgumentException(
                    "Debe seleccionar una fecha válida.");

            Fecha = fecha.Date;
        }

        public void CambiarCantidad(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException(
                    "La cantidad de mortalidad debe ser mayor que cero.");

            Cantidad = cantidad;
        }

        public void CambiarCausa(string causa)
        {
            if (string.IsNullOrWhiteSpace(causa))
                throw new ArgumentException(
                    "La causa de mortalidad es obligatoria.");

            string valor = causa.Trim();

            if (valor.Length > 150)
                throw new ArgumentException(
                    "La causa no puede superar los 150 caracteres.");

            Causa = valor;
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
