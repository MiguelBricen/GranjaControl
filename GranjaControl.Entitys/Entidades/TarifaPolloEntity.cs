namespace GranjaControl.Entitys.Entidades
{
    public class TarifaPolloEntity
    {
        public const decimal PrecioMaximo = 99999999.99m;

        public int Id { get; private set; }

        public decimal PrecioPorLibra { get; private set; }

        public DateTime FechaInicio { get; private set; } = DateTime.Today;

        public DateTime? FechaFin { get; private set; }

        public bool Activo { get; private set; }

        public string Estado => Activo ? "Activo" : "Inactivo";

        public TarifaPolloEntity() { }

        public TarifaPolloEntity(
            decimal precioPorLibra,
            DateTime fechaInicio,
            DateTime? fechaFin)
        {
            CambiarPrecio(precioPorLibra);
            CambiarFechaInicio(fechaInicio);
            CambiarFechaFin(fechaFin);
        }

        public void AsignarId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El ID no puede ser negativo.");

            Id = id;
        }

        public void CambiarPrecio(decimal precioPorLibra)
        {
            if (precioPorLibra <= 0)
                throw new ArgumentException(
                    "El precio por libra debe ser mayor que cero.");

            if (precioPorLibra > PrecioMaximo)
                throw new ArgumentException(
                    "El precio por libra no puede superar los 99,999,999.99.");

            PrecioPorLibra = precioPorLibra;
        }

        public void CambiarFechaInicio(DateTime fechaInicio)
        {
            if (fechaInicio == default)
                throw new ArgumentException(
                    "Debe seleccionar una fecha de inicio válida.");

            FechaInicio = fechaInicio.Date;
        }

        public void CambiarFechaFin(DateTime? fechaFin)
        {
            if (fechaFin.HasValue && fechaFin.Value.Date < FechaInicio)
                throw new ArgumentException(
                    "La fecha fin no puede ser anterior a la fecha inicio.");

            FechaFin = fechaFin?.Date;
        }

        public void CambiarActivo(bool activo)
        {
            Activo = activo;
        }
    }
}
