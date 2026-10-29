using System.ComponentModel;
using System.Drawing.Drawing2D;


namespace GranjaControl.UI.Utilidades
{
    public class BotonCircularImagen : Control
    {
        // Imagen que se mostrará dentro del círculo.

        // Imagen que se asignará desde el código.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image? Imagen { get; set; }

        [DefaultValue(PictureBoxSizeMode.Zoom)]
        public PictureBoxSizeMode ModoImagen { get; set; } = PictureBoxSizeMode.Zoom;

        // Color del borde circular.
        [DefaultValue(typeof(Color), "SaddleBrown")]
        public Color ColorBorde { get; set; } = Color.SaddleBrown;

        // Grosor del borde circular.
        [DefaultValue(2f)]
        public float GrosorBorde { get; set; } = 2f;

        public BotonCircularImagen()
        {
            Size = new Size(70, 70);
            BackColor = Color.White;
            Cursor = Cursors.Hand;
            TabStop = true;

            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor,
                true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Define el área circular.
            RectangleF area = new RectangleF(
                GrosorBorde,
                GrosorBorde,
                Width - GrosorBorde * 2,
                Height - GrosorBorde * 2);

            using GraphicsPath circulo = new GraphicsPath();
            circulo.AddEllipse(area);

            // Dibuja el fondo del botón.
            using SolidBrush fondo = new SolidBrush(BackColor);
            g.FillEllipse(fondo, area);

            // Recorta la imagen para que no sobresalga del círculo.
            if (Imagen != null)
            {
                GraphicsState estado = g.Save();
                g.SetClip(circulo);

                RectangleF destino = area;

                if (ModoImagen == PictureBoxSizeMode.Zoom)
                {
                    float escala = Math.Min(
                        area.Width / Imagen.Width,
                        area.Height / Imagen.Height);

                    float ancho = Imagen.Width * escala;
                    float alto = Imagen.Height * escala;

                    destino = new RectangleF(
                        area.X + (area.Width - ancho) / 2,
                        area.Y + (area.Height - alto) / 2,
                        ancho,
                        alto);
                }

                g.DrawImage(Imagen, destino);
                g.Restore(estado);
            }

            // Dibuja el borde circular.
            using Pen borde = new Pen(ColorBorde, GrosorBorde);
            g.DrawEllipse(borde, area);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // Hace circular la región real del control.
            using GraphicsPath circulo = new GraphicsPath();
            circulo.AddEllipse(0, 0, Width, Height);
            Region = new Region(circulo);

            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                OnClick(EventArgs.Empty);
                e.Handled = true;
            }
        }
    }
}
