using GranjaControl.DAL;
using GranjaControl.Entitys.Dto;
using GranjaControl.Entitys.Entidades;

namespace GranjaControl.BLL
{
    public class ReporteBLL
    {
        private readonly ReporteDAL _reporteDal;

        public ReporteBLL(ReporteDAL reporteDal)
        {
            _reporteDal = reporteDal;
        }

        public async Task<ReporteVentasDto> ObtenerVentasAsync(
            DateTime? fechaInicio,
            DateTime? fechaFin,
            int? idCliente,
            string? estado)
        {
            ValidarPeriodo(fechaInicio, fechaFin);
            ValidarId(idCliente, "El cliente seleccionado no es válido.");

            estado = NormalizarEstado(estado, VentaEntity.EstadosPermitidos);

            return await _reporteDal.ObtenerVentasAsync(
                fechaInicio,
                fechaFin,
                idCliente,
                estado);
        }

        public async Task<ReporteProduccionDto> ObtenerProduccionAsync(
            DateTime? fechaInicio,
            DateTime? fechaFin,
            string? estado,
            int? idGalpon)
        {
            ValidarPeriodo(fechaInicio, fechaFin);
            ValidarId(idGalpon, "El galpón seleccionado no es válido.");

            estado = NormalizarEstado(estado, LoteEntity.EstadosPermitidos);

            return await _reporteDal.ObtenerProduccionAsync(
                fechaInicio,
                fechaFin,
                estado,
                idGalpon);
        }

        public async Task<ReporteComprasDto> ObtenerComprasAsync(
            DateTime? fechaInicio,
            DateTime? fechaFin,
            int? idProveedor)
        {
            ValidarPeriodo(fechaInicio, fechaFin);
            ValidarId(idProveedor, "El proveedor seleccionado no es válido.");

            return await _reporteDal.ObtenerComprasAsync(
                fechaInicio,
                fechaFin,
                idProveedor);
        }

        private static void ValidarPeriodo(
            DateTime? fechaInicio,
            DateTime? fechaFin)
        {
            if (fechaInicio.HasValue &&
                fechaFin.HasValue &&
                fechaInicio.Value.Date > fechaFin.Value.Date)
            {
                throw new ArgumentException(
                    "La fecha inicial no puede ser posterior a la fecha final.");
            }
        }

        private static void ValidarId(int? id, string mensaje)
        {
            if (id.HasValue && id.Value <= 0)
                throw new ArgumentException(mensaje);
        }

        private static string? NormalizarEstado(
            string? estado,
            IReadOnlyList<string> estadosPermitidos)
        {
            if (string.IsNullOrWhiteSpace(estado))
                return null;

            string valor = estado.Trim();

            if (!estadosPermitidos.Contains(
                    valor,
                    StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    "El estado seleccionado no es válido.");
            }

            return estadosPermitidos.First(
                e => e.Equals(valor, StringComparison.OrdinalIgnoreCase));
        }
    }
}
