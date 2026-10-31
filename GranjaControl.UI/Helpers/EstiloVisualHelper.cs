namespace GranjaControl.UI.Helpers
{
    public static class EstiloVisualHelper
    {
        public static readonly Color Fondo = Color.FromArgb(243, 247, 244);
        public static readonly Color Superficie = Color.White;
        public static readonly Color VerdeOscuro = Color.FromArgb(23, 61, 43);
        public static readonly Color VerdeClaro = Color.FromArgb(231, 239, 233);
        public static readonly Color TextoPrincipal = Color.FromArgb(31, 41, 55);
        public static readonly Color TextoSecundario = Color.FromArgb(100, 116, 108);
        public static readonly Color Borde = Color.FromArgb(226, 234, 228);
        public static readonly Color EncabezadoTabla = Color.FromArgb(37, 99, 235);
        public static readonly Color FilaAlterna = Color.FromArgb(239, 246, 255);
        public static readonly Color Seleccion = Color.FromArgb(219, 234, 254);

        public static readonly Color Azul = Color.FromArgb(37, 99, 235);
        public static readonly Color Verde = Color.FromArgb(22, 163, 74);
        public static readonly Color Turquesa = Color.FromArgb(13, 148, 136);
        public static readonly Color Morado = Color.FromArgb(124, 58, 237);
        public static readonly Color Dorado = Color.FromArgb(202, 138, 4);
        public static readonly Color Rojo = Color.FromArgb(220, 38, 38);
        public static readonly Color Naranja = Color.FromArgb(234, 88, 12);
        public static readonly Color Indigo = Color.FromArgb(79, 70, 229);

        public static Panel CrearTarjeta(
            string titulo,
            Color acento,
            bool mostrarDetalle,
            float tamanoValor,
            out Label valor,
            out Label detalle)
        {
            Color fondo = MezclarConBlanco(acento, 0.12);
            Color borde = MezclarConBlanco(acento, 0.32);
            Color colorValor = Oscurecer(acento, 0.78);
            Color colorDetalle = Oscurecer(acento, 0.88);

            Panel tarjeta = new()
            {
                BackColor = fondo,
                Dock = DockStyle.Fill
            };

            tarjeta.Paint += (_, e) =>
            {
                using SolidBrush franja = new(acento);
                using Pen lapiz = new(borde);

                e.Graphics.FillRectangle(
                    franja,
                    0,
                    0,
                    5,
                    tarjeta.Height);

                e.Graphics.DrawRectangle(
                    lapiz,
                    0,
                    0,
                    Math.Max(0, tarjeta.Width - 1),
                    Math.Max(0, tarjeta.Height - 1));
            };

            TableLayoutPanel contenido = new()
            {
                BackColor = Color.Transparent,
                ColumnCount = 1,
                Dock = DockStyle.Fill,
                Padding = new Padding(18, 6, 12, 5),
                RowCount = mostrarDetalle ? 3 : 2
            };

            contenido.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100F));

            contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 21F));
            contenido.RowStyles.Add(
                new RowStyle(
                    mostrarDetalle
                        ? SizeType.Absolute
                        : SizeType.Percent,
                    mostrarDetalle ? 38F : 100F));

            Label etiqueta = new()
            {
                AutoEllipsis = true,
                BackColor = Color.Transparent,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                ForeColor = TextoSecundario,
                Margin = new Padding(0),
                Text = titulo,
                TextAlign = ContentAlignment.MiddleLeft
            };

            valor = new Label
            {
                AutoEllipsis = true,
                BackColor = Color.Transparent,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", tamanoValor, FontStyle.Bold),
                ForeColor = colorValor,
                Margin = new Padding(0),
                Text = "--",
                TextAlign = ContentAlignment.MiddleLeft
            };

            detalle = new Label
            {
                AutoEllipsis = true,
                BackColor = Color.Transparent,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold),
                ForeColor = colorDetalle,
                Margin = new Padding(0),
                Text = string.Empty,
                TextAlign = ContentAlignment.MiddleLeft
            };

            contenido.Controls.Add(etiqueta, 0, 0);
            contenido.Controls.Add(valor, 0, 1);

            if (mostrarDetalle)
            {
                contenido.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 100F));

                contenido.Controls.Add(detalle, 0, 2);
            }

            tarjeta.Controls.Add(contenido);

            RedondearControlHelper.RedondearControlAlCambiarTamano(
                tarjeta,
                12);

            return tarjeta;
        }

        public static Color FondoSuave(Color acento)
        {
            return MezclarConBlanco(acento, 0.12);
        }

        public static Color MezclarConBlanco(Color color, double porcentaje)
        {
            return Color.FromArgb(
                MezclarCanal(color.R, 255, porcentaje),
                MezclarCanal(color.G, 255, porcentaje),
                MezclarCanal(color.B, 255, porcentaje));
        }

        public static Color Oscurecer(Color color, double factor)
        {
            return Color.FromArgb(
                MezclarCanal(color.R, 0, factor),
                MezclarCanal(color.G, 0, factor),
                MezclarCanal(color.B, 0, factor));
        }

        private static int MezclarCanal(int canal, int destino, double porcentaje)
        {
            return Math.Clamp(
                (int)Math.Round(canal * porcentaje + destino * (1 - porcentaje)),
                0,
                255);
        }
    }
}
