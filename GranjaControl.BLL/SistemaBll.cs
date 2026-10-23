
using GranjaControl.DAL;

namespace GranjaControl.BLL
{
    public class SistemaBll
    {
        private readonly SistemaDal _sistemaDal;

        public SistemaBll(SistemaDal sistemaDal)
        {
            _sistemaDal = sistemaDal;
        }

        public async Task CrearAdministradorInicialAsync()
        {
            await _sistemaDal.CrearAdministradorInicialAsync();
        }
    }
}
