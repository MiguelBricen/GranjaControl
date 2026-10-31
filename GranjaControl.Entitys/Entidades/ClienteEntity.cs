namespace GranjaControl.Entitys.Entidades
{
    public class ClienteEntity
    {
        public static IReadOnlyList<string> TiposPermitidos { get; } =
            ["Cliente específico", "Pulpería", "Negocio", "Otro"];

        public int Id { get; private set; }

        public string NombreCliente { get; private set; } = string.Empty;

        public string? Telefono { get; private set; }

        public string? Direccion { get; private set; }

        public string TipoCliente { get; private set; } = string.Empty;

        public bool Activo { get; private set; }

        public string Estado => Activo ? "Activo" : "Inactivo";

        public ClienteEntity() { }

        public ClienteEntity(
            string nombreCliente,
            string? telefono,
            string? direccion,
            string tipoCliente)
        {
            CambiarNombre(nombreCliente);
            CambiarTelefono(telefono);
            CambiarDireccion(direccion);
            CambiarTipoCliente(tipoCliente);
        }

        public void AsignarId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El ID no puede ser negativo.");

            Id = id;
        }

        public void CambiarNombre(string nombreCliente)
        {
            if (string.IsNullOrWhiteSpace(nombreCliente))
                throw new ArgumentNullException(
                    "Debe ingresar el nombre del cliente.");

            string nombre = nombreCliente.Trim();

            if (nombre.Length > 100)
                throw new ArgumentException(
                    "El nombre del cliente no puede superar los 100 caracteres.");

            NombreCliente = nombre;
        }

        public void CambiarTelefono(string? telefono)
        {
            string? valor = string.IsNullOrWhiteSpace(telefono)
                ? null
                : telefono.Trim();

            if (valor?.Length > 25)
                throw new ArgumentException(
                    "El teléfono no puede superar los 25 caracteres.");

            Telefono = valor;
        }

        public void CambiarDireccion(string? direccion)
        {
            string? valor = string.IsNullOrWhiteSpace(direccion)
                ? null
                : direccion.Trim();

            if (valor?.Length > 200)
                throw new ArgumentException(
                    "La dirección no puede superar los 200 caracteres.");

            Direccion = valor;
        }

        public void CambiarTipoCliente(string tipoCliente)
        {
            if (string.IsNullOrWhiteSpace(tipoCliente))
                throw new ArgumentNullException(
                    "Debe seleccionar el tipo de cliente.");

            string tipo = tipoCliente.Trim();

            if (!TiposPermitidos.Contains(tipo, StringComparer.OrdinalIgnoreCase))
                throw new ArgumentException(
                    "El tipo de cliente no es válido.");

            TipoCliente = TiposPermitidos.First(
                t => t.Equals(tipo, StringComparison.OrdinalIgnoreCase));
        }

        public void CambiarActivo(bool activo)
        {
            Activo = activo;
        }
    }
}
