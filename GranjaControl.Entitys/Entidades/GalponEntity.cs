
namespace GranjaControl.Entitys
{
    public class GalponEntity
    {
        #region Propiedades

        public int Id { get; private set; }

        public string NombreGalpon { get; private set; } = string.Empty;

        public int Capacidad { get; private set; }

        public string? Descripcion { get; private set; }

        public bool Activo { get; private set; } 

        public string Estado => Activo ? "Activo" : "Inactivo";

        #endregion

        #region Constructores

        public GalponEntity() { }

        public GalponEntity(string nombreGalpon, int capacidad, string? descripcion)
        {
            CambiarNombre(nombreGalpon);
            CambiarCapacidad(capacidad);
            CambiarDescripcion(descripcion);
        }

        #endregion

        #region Métodos

        public void AsignarId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El ID no puede ser negativo.");

            Id = id;
        }

        public void CambiarNombre(string nombreGalpon)
        {
            if (string.IsNullOrWhiteSpace(nombreGalpon))
                throw new ArgumentNullException(
                    "Debe ingresar el nombre del galpón.");

            NombreGalpon = nombreGalpon.Trim();
        }

        public void CambiarCapacidad(int capacidad)
        {
            if (capacidad <= 0)
                throw new ArgumentException(
                    "La capacidad debe ser mayor que cero.");

            Capacidad = capacidad;
        }

        public void CambiarDescripcion(string? descripcion)
        {
            Descripcion = string.IsNullOrWhiteSpace(descripcion)
                ? null
                : descripcion.Trim();
        }

        public void CambiarActivo(bool activo)
        {
            Activo = activo;
        }

        #endregion
    }
}

