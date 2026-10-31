namespace GranjaControl.Entitys.Dto
{
    public class DashboardDto
    {
        public DashboardResumenDto Resumen { get; set; } = new();

        public List<DashboardLoteActivoDto> LotesActivos { get; set; } = [];

        public List<DashboardVentaRecienteDto> VentasRecientes { get; set; } = [];
    }

    public class DashboardResumenDto
    {
        public int LotesActivos { get; set; }

        public int AvesIniciales { get; set; }

        public int AvesDisponibles { get; set; }

        public int PollosVendidosMes { get; set; }

        public int VentasMes { get; set; }

        public decimal IngresosMes { get; set; }

        public int MortalidadAcumulada { get; set; }

        public decimal ComprasMes { get; set; }

        public int ClientesActivos { get; set; }
    }

    public class DashboardLoteActivoDto
    {
        public int Id { get; set; }

        public string CodigoLote { get; set; } = string.Empty;

        public string NombreGalpon { get; set; } = string.Empty;

        public string NombreProveedor { get; set; } = string.Empty;

        public DateTime FechaIngreso { get; set; }

        public string Estado { get; set; } = string.Empty;

        public int CantidadInicial { get; set; }

        public int Mortalidad { get; set; }

        public int Vendidos { get; set; }

        public int Disponibles { get; set; }

        public decimal PorcentajeMortalidad { get; set; }
    }

    public class DashboardVentaRecienteDto
    {
        public int Id { get; set; }

        public string NumeroVenta { get; set; } = string.Empty;

        public string NombreCliente { get; set; } = string.Empty;

        public DateTime FechaVenta { get; set; }

        public string Estado { get; set; } = string.Empty;

        public int CantidadPollos { get; set; }

        public decimal PesoLibras { get; set; }

        public decimal Total { get; set; }
    }
}
