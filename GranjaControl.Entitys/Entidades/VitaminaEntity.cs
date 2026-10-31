namespace GranjaControl.Entitys.Entidades
{
    public class VitaminaEntity
    {
        public int Id { get; private set; }

        public string NombreVitamina { get; private set; } = string.Empty;

        public string? Descripcion { get; private set; }

        public bool Activo { get; private set; }

        public string Estado => Activo ? "Activo" : "Inactivo";

        public VitaminaEntity() { }

        public VitaminaEntity(string nombreVitamina, string? descripcion)
        {
            CambiarNombre(nombreVitamina);
            CambiarDescripcion(descripcion);
        }

        public void AsignarId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El ID no puede ser negativo.");

            Id = id;
        }

        public void CambiarNombre(string nombreVitamina)
        {
            if (string.IsNullOrWhiteSpace(nombreVitamina))
                throw new ArgumentNullException(
                    "Debe ingresar el nombre de la vitamina.");

            string nombre = nombreVitamina.Trim();

            if (nombre.Length > 100)
                throw new ArgumentException(
                    "El nombre de la vitamina no puede superar los 100 caracteres.");

            NombreVitamina = nombre;
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
