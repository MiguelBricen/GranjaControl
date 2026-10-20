using System.Drawing.Imaging;

namespace GranjaControl.UI.Helpers
{
    public static class NavegacionHelper
    {
        private static Button? _botonActivo;

        // Paleta de colores de GranjaControl
        private static readonly Color ColorNormal =
            Color.FromArgb(23, 61, 43);

        private static readonly Color ColorActivo =
            Color.FromArgb(226, 239, 228);

        private static readonly Color ColorTextoNormal =
            Color.FromArgb(242, 245, 239);

        private static readonly Color ColorTextoActivo =
            Color.Black;

        // Conserva las imágenes originales de los botones.
        private static readonly Dictionary<Button, Image?> ImagenesOriginales
            = new();

        /// <summary>
        /// Activa un botón y restaura el anterior.
        /// </summary>
        public static void ActivarBoton(Button boton)
        {
            if (boton == null || boton.IsDisposed)
                return;

            // Restaurar el botón previamente activo.
            if (_botonActivo != null &&
                !_botonActivo.IsDisposed &&
                _botonActivo != boton)
            {
                AplicarEstiloNormal(_botonActivo);
            }

            _botonActivo = boton;

            AplicarEstiloActivo(boton);
        }

        /// <summary>
        /// Aplica el estilo normal.
        /// </summary>
        private static void AplicarEstiloNormal(Button boton)
        {
            boton.BackColor = ColorNormal;
            boton.ForeColor = ColorTextoNormal;

            RestaurarImagenOriginal(boton);
        }

        /// <summary>
        /// Aplica el estilo activo y recolorea el icono en negro.
        /// </summary>
        private static void AplicarEstiloActivo(Button boton)
        {
            boton.BackColor = ColorActivo;
            boton.ForeColor = ColorTextoActivo;

            if (!ImagenesOriginales.ContainsKey(boton))
                ImagenesOriginales[boton] = boton.Image;

            if (ImagenesOriginales[boton] != null)
            {
                Image imagenNegra = RecolorearIcono(
                    ImagenesOriginales[boton]!,
                    Color.Black);

                boton.Image = imagenNegra;
            }
        }

        /// <summary>
        /// Restaura la imagen original del botón.
        /// </summary>
        private static void RestaurarImagenOriginal(Button boton)
        {
            if (ImagenesOriginales.TryGetValue(
                boton, out Image? imagenOriginal))
            {
                boton.Image = imagenOriginal;
            }
        }

        /// <summary>
        /// Convierte los píxeles visibles del icono a negro,
        /// conservando su transparencia.
        /// </summary>
        private static Bitmap RecolorearIcono(
            Image original,
            Color color)
        {
            Bitmap resultado = new Bitmap(
                original.Width,
                original.Height,
                PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(resultado))
            {
                g.Clear(Color.Transparent);

                using (ImageAttributes atributos = new ImageAttributes())
                {
                    // Conserva el canal alfa original y reemplaza
                    // el color RGB por el color solicitado.
                    ColorMatrix matriz = new ColorMatrix(
                        new float[][]
                        {
                            new float[] { 0, 0, 0, 0, 0 },
                            new float[] { 0, 0, 0, 0, 0 },
                            new float[] { 0, 0, 0, 0, 0 },
                            new float[] { 0, 0, 0, 1, 0 },
                            new float[] {
                                color.R / 255f,
                                color.G / 255f,
                                color.B / 255f,
                                0,
                                1
                            }
                        });

                    atributos.SetColorMatrix(matriz);

                    g.DrawImage(
                        original,
                        new Rectangle(0, 0,
                            original.Width, original.Height),
                        0, 0,
                        original.Width, original.Height,
                        GraphicsUnit.Pixel,
                        atributos);
                }
            }

            return resultado;
        }

        /// <summary>
        /// Desactiva el botón si es el que está activo.
        /// </summary>
        public static void DesactivarBoton(Button boton)
        {
            if (boton == null || boton.IsDisposed)
                return;

            if (_botonActivo == boton)
            {
                AplicarEstiloNormal(boton);
                _botonActivo = null;
            }
        }

        /// <summary>
        /// Abre un formulario dentro del panel contenedor
        /// o como ventana modal.
        /// </summary>
        public static void AbrirFormulario(
            Form formularioHijo,
            Panel panelContenedor,
            ref Form? formularioActivo,
            Button boton,
            bool esHijoDelPanelContenedor = true)
        {
            try
            {
                ActivarBoton(boton);

                if (esHijoDelPanelContenedor)
                {
                    if (formularioActivo != null)
                    {
                        formularioActivo.Close();
                        formularioActivo.Dispose();
                    }

                    formularioActivo = formularioHijo;

                    formularioHijo.TopLevel = false;
                    formularioHijo.FormBorderStyle =
                        FormBorderStyle.None;
                    formularioHijo.Dock = DockStyle.Fill;

                    panelContenedor.Controls.Clear();
                    panelContenedor.Controls.Add(formularioHijo);
                    panelContenedor.Tag = formularioHijo;

                    formularioHijo.Show();
                    formularioHijo.BringToFront();
                }
                else
                {
                    formularioHijo.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error inesperado: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Método para restablecer los btnPresionados
        public static void RestablecerBotonActivo()
        {
            if (_botonActivo != null && !_botonActivo.IsDisposed)
            {
                AplicarEstiloNormal(_botonActivo);
                _botonActivo = null;
            }
        }
    }
}
