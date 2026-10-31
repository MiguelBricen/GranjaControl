namespace GranjaControl.Entitys.Dto
{
    public class ReporteVentasDto
    {
        public ReporteVentasResumenDto Resumen { get; set; } = new();

        public List<ReporteVentaDto> Detalle { get; set; } = [];
    }

    public class ReporteVentasResumenDto
    {
        public int TotalVentas { get; set; }

        public int Registradas { get; set; }

        public int Anuladas { get; set; }

        public int PollosVendidos { get; set; }

        public decimal LibrasVendidas { get; set; }

        public decimal Ingresos { get; set; }
    }

    public class ReporteVentaDto
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

    public class ReporteProduccionDto
    {
        public ReporteProduccionResumenDto Resumen { get; set; } = new();

        public List<ReporteProduccionLoteDto> Detalle { get; set; } = [];
    }

    public class ReporteProduccionResumenDto
    {
        public int TotalLotes { get; set; }

        public int CantidadInicial { get; set; }

        public int Mortalidad { get; set; }

        public int Vendidos { get; set; }

        public int Disponibles { get; set; }

        public decimal PorcentajeMortalidad { get; set; }
    }

    public class ReporteProduccionLoteDto
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

    public class ReporteComprasDto
    {
        public ReporteComprasResumenDto Resumen { get; set; } = new();

        public List<ReporteCompraDto> Detalle { get; set; } = [];
    }

    public class ReporteComprasResumenDto
    {
        public int TotalCompras { get; set; }

        public decimal TotalLibras { get; set; }

        public decimal TotalKilogramos { get; set; }

        public decimal CostoTotal { get; set; }
    }

    public class ReporteCompraDto
    {
        public int Id { get; set; }

        public string NumeroCompra { get; set; } = string.Empty;

        public string NombreProveedor { get; set; } = string.Empty;

        public DateTime FechaCompra { get; set; }

        public decimal Libras { get; set; }

        public decimal Kilogramos { get; set; }

        public decimal CostoTotal { get; set; }

        public int CantidadItems { get; set; }
    }
}
