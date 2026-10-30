namespace GranjaControl.Entitys.Entidades
{
    public class ProveedorEntity
    {
        public static IReadOnlyList<string> TiposPermitidos { get; } =
            ["Pollitos", "Alimento", "Ambos"];

        public int Id { get; private set; }

        public string NombreProveedor { get; private set; } = string.Empty;

        public string? Telefono { get; private set; }

        public string? CorreoElectronico { get; private set; }

        public string? Direccion { get; private set; }

        public string TipoProveedor { get; private set; } = string.Empty;

        public bool Activo { get; private set; }

        public string Estado => Activo ? "Activo" : "Inactivo";

        public ProveedorEntity() { }

        public ProveedorEntity(
            string nombreProveedor,
            string? telefono,
            string? correoElectronico,
            string? direccion,
            string tipoProveedor)
        {
            CambiarNombre(nombreProveedor);
            CambiarTelefono(telefono);
            CambiarCorreo(correoElectronico);
            CambiarDireccion(direccion);
            CambiarTipoProveedor(tipoProveedor);
        }

        public void AsignarId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El ID no puede ser negativo.");

            Id = id;
        }

        public void CambiarNombre(string nombreProveedor)
        {
            if (string.IsNullOrWhiteSpace(nombreProveedor))
                throw new ArgumentNullException(
                    "Debe ingresar el nombre del proveedor.");

            string nombre = nombreProveedor.Trim();

            if (nombre.Length > 100)
                throw new ArgumentException(
                    "El nombre del proveedor no puede superar los 100 caracteres.");

            NombreProveedor = nombre;
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

        public void CambiarCorreo(string? correoElectronico)
        {
            string? valor = string.IsNullOrWhiteSpace(correoElectronico)
                ? null
                : correoElectronico.Trim();

            if (valor?.Length > 100)
                throw new ArgumentException(
                    "El correo electrónico no puede superar los 100 caracteres.");

            CorreoElectronico = valor;
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

        public void CambiarTipoProveedor(string tipoProveedor)
        {
            if (string.IsNullOrWhiteSpace(tipoProveedor))
                throw new ArgumentNullException(
                    "Debe seleccionar el tipo de proveedor.");

            string tipo = tipoProveedor.Trim();

            if (!TiposPermitidos.Contains(tipo, StringComparer.OrdinalIgnoreCase))
                throw new ArgumentException(
                    "El tipo de proveedor no es válido.");

            TipoProveedor = TiposPermitidos.First(t =>
                t.Equals(tipo, StringComparison.OrdinalIgnoreCase));
        }

        public void CambiarActivo(bool activo)
        {
            Activo = activo;
        }
    }
}
