namespace GranjaControl.Entitys.Entidades
{
    public class VitaminizacionEntity
    {
        public static IReadOnlyList<string> UnidadesPermitidas { get; } =
            ["Mililitros", "Litros", "Gramos"];

        public int Id { get; private set; }

        public int IdLote { get; private set; }

        public string CodigoLote { get; private set; } = string.Empty;

        public int IdVitamina { get; private set; }

        public string NombreVitamina { get; private set; } = string.Empty;

        public DateTime Fecha { get; private set; } = DateTime.Today;

        public decimal Cantidad { get; private set; }

        public string UnidadMedida { get; private set; } = string.Empty;

        public string? Observacion { get; private set; }

        public VitaminizacionEntity() { }

        public VitaminizacionEntity(
            int idLote,
            int idVitamina,
            DateTime fecha,
            decimal cantidad,
            string unidadMedida,
            string? observacion)
        {
            AsignarLote(idLote);
            AsignarVitamina(idVitamina);
            CambiarFecha(fecha);
            CambiarCantidad(cantidad);
            CambiarUnidadMedida(unidadMedida);
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

        public void AsignarVitamina(
            int idVitamina,
            string? nombreVitamina = null)
        {
            if (idVitamina <= 0)
                throw new ArgumentException(
                    "Debe seleccionar una vitamina válida.");

            IdVitamina = idVitamina;

            if (!string.IsNullOrWhiteSpace(nombreVitamina))
                NombreVitamina = nombreVitamina.Trim();
        }

        public void CambiarFecha(DateTime fecha)
        {
            if (fecha == default)
                throw new ArgumentException(
                    "Debe seleccionar una fecha válida.");

            Fecha = fecha.Date;
        }

        public void CambiarCantidad(decimal cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException(
                    "La cantidad de vitamina debe ser mayor que cero.");

            if (cantidad > 99999999.99m)
                throw new ArgumentException(
                    "La cantidad de vitamina es demasiado alta.");

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
