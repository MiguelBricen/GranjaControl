namespace GranjaControl.Entitys.Entidades
{
    public class AlimentoEntity
    {
        public static IReadOnlyList<string> TiposPermitidos { get; } =
            ["Inicio", "Finalizador"];

        public int Id { get; private set; }

        public string NombreAlimento { get; private set; } = string.Empty;

        public string TipoAlimento { get; private set; } = string.Empty;

        public string? Descripcion { get; private set; }

        public bool Activo { get; private set; }

        public string Estado => Activo ? "Activo" : "Inactivo";

        public AlimentoEntity() { }

        public AlimentoEntity(
            string nombreAlimento,
            string tipoAlimento,
            string? descripcion)
        {
            CambiarNombre(nombreAlimento);
            CambiarTipoAlimento(tipoAlimento);
            CambiarDescripcion(descripcion);
        }

        public void AsignarId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El ID no puede ser negativo.");

            Id = id;
        }

        public void CambiarNombre(string nombreAlimento)
        {
            if (string.IsNullOrWhiteSpace(nombreAlimento))
                throw new ArgumentNullException(
                    "Debe ingresar el nombre del alimento.");

            string nombre = nombreAlimento.Trim();

            if (nombre.Length > 100)
                throw new ArgumentException(
                    "El nombre del alimento no puede superar los 100 caracteres.");

            NombreAlimento = nombre;
        }

        public void CambiarTipoAlimento(string tipoAlimento)
        {
            if (string.IsNullOrWhiteSpace(tipoAlimento))
                throw new ArgumentNullException(
                    "Debe seleccionar el tipo de alimento.");

            string tipo = tipoAlimento.Trim();

            if (!TiposPermitidos.Contains(tipo, StringComparer.OrdinalIgnoreCase))
                throw new ArgumentException(
                    "El tipo de alimento no es válido.");

            TipoAlimento = TiposPermitidos.First(
                t => t.Equals(tipo, StringComparison.OrdinalIgnoreCase));
        }

        public void CambiarDescripcion(string? descripcion)
        {
            string? valor = string.IsNullOrWhiteSpace(descripcion)
                ? null
                : descripcion.Trim();

            if (valor?.Length > 250)
                throw new ArgumentException(
                    "La descripción no puede superar los 250 caracteres.");

            Descripcion = valor;
        }

        public void CambiarActivo(bool activo)
        {
            Activo = activo;
        }
    }
}
