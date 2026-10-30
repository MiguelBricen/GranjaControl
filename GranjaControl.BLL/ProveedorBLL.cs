using GranjaControl.DAL;
using GranjaControl.Entitys.Entidades;
using System.Text.RegularExpressions;

namespace GranjaControl.BLL
{
    public class ProveedorBLL
    {
        private readonly ProveedorDAL _proveedorDal;

        public ProveedorBLL(ProveedorDAL proveedorDal)
        {
            _proveedorDal = proveedorDal;
        }

        public async Task<PaginacionEntity<ProveedorEntity>> MostrarAsync(
            int pagina,
            int tamPagina,
            string? buscar = null)
        {
            if (pagina < 1)
                pagina = 1;

            if (tamPagina < 1)
                tamPagina = 5;

            return await _proveedorDal.MostrarAsync(
                pagina,
                tamPagina,
                buscar);
        }

        public async Task<List<ProveedorEntity>> MostrarProveedoresActivosAsync()
        {
            return await _proveedorDal.MostrarProveedoresActivosAsync();
        }

        public async Task<ProveedorEntity?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del proveedor no es válido.");

            return await _proveedorDal.ObtenerPorIdAsync(id);
        }

        public async Task AgregarAsync(
            ProveedorEntity proveedor,
            string usuarioApp)
        {
            Validar(proveedor, usuarioApp);

            await _proveedorDal.AgregarAsync(proveedor, usuarioApp);
        }

        public async Task ActualizarAsync(
            ProveedorEntity proveedor,
            string usuarioApp)
        {
            if (proveedor.Id <= 0)
                throw new ArgumentException("El ID del proveedor no es válido.");

            Validar(proveedor, usuarioApp);

            await _proveedorDal.ActualizarAsync(proveedor, usuarioApp);
        }

        public async Task CambiarEstadoAsync(
            int id,
            bool activo,
            string usuarioApp)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del proveedor no es válido.");

            await _proveedorDal.CambiarEstadoAsync(
                id,
                activo,
                usuarioApp);
        }

        private static void Validar(
            ProveedorEntity proveedor,
            string usuarioApp)
        {
            if (string.IsNullOrWhiteSpace(proveedor.NombreProveedor))
                throw new ArgumentException(
                    "Debe ingresar el nombre del proveedor.");

            if (!ProveedorEntity.TiposPermitidos.Contains(
                    proveedor.TipoProveedor,
                    StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    "El tipo de proveedor no es válido.");
            }

            if (!string.IsNullOrWhiteSpace(proveedor.CorreoElectronico) &&
                !Regex.IsMatch(
                    proveedor.CorreoElectronico,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ArgumentException(
                    "Ingrese un correo electrónico válido.");
            }

            if (string.IsNullOrWhiteSpace(usuarioApp))
                throw new ArgumentException(
                    "No se pudo identificar el usuario de la sesión.");
        }
    }
}
