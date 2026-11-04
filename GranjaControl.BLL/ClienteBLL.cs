using GranjaControl.DAL;
using GranjaControl.Entitys.Entidades;

namespace GranjaControl.BLL
{
    public class ClienteBLL
    {
        private readonly ClienteDAL _clienteDal;

        public ClienteBLL(ClienteDAL clienteDal)
        {
            _clienteDal = clienteDal;
        }

        public async Task<PaginacionEntity<ClienteEntity>> MostrarAsync(
            int pagina,
            int tamPagina,
            string? buscar = null)
        {
            if (pagina < 1)
                pagina = 1;

            if (tamPagina < 1)
                tamPagina = 5;

            return await _clienteDal.MostrarAsync(
                pagina,
                tamPagina,
                buscar);
        }

        public async Task<List<ClienteEntity>> MostrarClientesActivosAsync()
        {
            return await _clienteDal.MostrarClientesActivosAsync();
        }

        public async Task<ClienteEntity?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del cliente no es válido.");

            return await _clienteDal.ObtenerPorIdAsync(id);
        }

        public async Task AgregarAsync(
            ClienteEntity cliente,
            string usuarioApp)
        {
            Validar(cliente, usuarioApp);

            await _clienteDal.AgregarAsync(cliente, usuarioApp);
        }

        public async Task ActualizarAsync(
            ClienteEntity cliente,
            string usuarioApp)
        {
            if (cliente.Id <= 0)
                throw new ArgumentException("El ID del cliente no es válido.");

            Validar(cliente, usuarioApp);

            await _clienteDal.ActualizarAsync(cliente, usuarioApp);
        }

        public async Task CambiarEstadoAsync(
            int id,
            bool activo,
            string usuarioApp)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del cliente no es válido.");

            if (string.IsNullOrWhiteSpace(usuarioApp))
                throw new ArgumentException(
                    "No se pudo identificar el usuario de la sesión.");

            await _clienteDal.CambiarEstadoAsync(
                id,
                activo,
                usuarioApp);
        }

        private static void Validar(
            ClienteEntity cliente,
            string usuarioApp)
        {
            if (string.IsNullOrWhiteSpace(cliente.NombreCliente))
                throw new ArgumentException(
                    "Debe ingresar el nombre del cliente.");

            if (cliente.NombreCliente.Length > 100)
                throw new ArgumentException(
                    "El nombre del cliente no puede superar los 100 caracteres.");

            if (cliente.Telefono?.Length > 25)
                throw new ArgumentException(
                    "El teléfono no puede superar los 25 caracteres.");

            if (cliente.Direccion?.Length > 200)
                throw new ArgumentException(
                    "La dirección no puede superar los 200 caracteres.");

            if (!ClienteEntity.TiposPermitidos.Contains(
                    cliente.TipoCliente,
                    StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    "El tipo de cliente no es válido.");
            }

            if (string.IsNullOrWhiteSpace(usuarioApp))
                throw new ArgumentException(
                    "No se pudo identificar el usuario de la sesión.");
        }
    }
}
