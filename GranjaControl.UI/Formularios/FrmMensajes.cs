using GranjaControl.UI.Properties;
using System.Diagnostics;
using System.IO;
using System.Media;


namespace GranjaControl.UI.Formularios
{
    public partial class FrmMensajes : Form
    {
        private readonly string tipoMensaje;

        // Posición final del mensaje.
        private int posicionFinalX;

        public FrmMensajes(string mensaje, string tipo)
        {

            InitializeComponent();
            this.Opacity = 1;

            timer1.Interval = 150;
            timerEntrada.Interval = 15;

            tipoMensaje = tipo?.Trim().ToLowerInvariant() ?? "info";

            lblMensaje.Text = mensaje;
            lblMensaje.MaximumSize = new Size(350, 0);

            Color colorEncabezado;
            Color colorAcento;
            string titulo;

            switch (tipoMensaje)
            {
                case "error":
                    picIcono.Image = Resources.error;
                    colorEncabezado = Color.FromArgb(128, 68, 62);
                    colorAcento = Color.FromArgb(148, 83, 75);
                    titulo = "ERROR EN LA OPERACIÓN";
                    break;

                case "ok":
                    picIcono.Image = Resources.ok;
                    colorEncabezado = Color.FromArgb(66, 101, 70);
                    colorAcento = Color.FromArgb(79, 117, 83);
                    titulo = "OPERACIÓN COMPLETADA";
                    break;

                case "info":
                    picIcono.Image = Resources.informacion;
                    colorEncabezado = Color.FromArgb(69, 103, 101);
                    colorAcento = Color.FromArgb(80, 116, 113);
                    titulo = "INFORMACIÓN";
                    break;

                case "warning":
                    picIcono.Image = Resources.peligro;
                    colorEncabezado = Color.FromArgb(132, 105, 59);
                    colorAcento = Color.FromArgb(151, 121, 68);
                    titulo = "ATENCIÓN REQUERIDA";
                    break;

                case "confirmar":
                    picIcono.Image = Resources.confirmacion;
                    colorEncabezado = Color.FromArgb(55, 86, 66);
                    colorAcento = Color.FromArgb(68, 101, 78);
                    titulo = "CONFIRMAR OPERACIÓN";
                    break;

                default:
                    tipoMensaje = "info";
                    picIcono.Image = Resources.informacion;
                    colorEncabezado = Color.FromArgb(69, 103, 101);
                    colorAcento = Color.FromArgb(80, 116, 113);
                    titulo = "INFORMACIÓN";
                    break;
            }

            bool esConfirmacion = tipoMensaje == "confirmar";
            btnAceptar.Visible = esConfirmacion;
            btnCancelar.Visible = esConfirmacion;

            AplicarEstilo(colorEncabezado, colorAcento, titulo);
        }

        #region Metodos

        private void AplicarEstilo(Color colorEncabezado, Color colorAcento, string titulo)
        {
            BackColor = Color.FromArgb(248, 247, 241);
            ForeColor = Color.FromArgb(48, 56, 48);

            panel1.BackColor = colorEncabezado;
            label1.Text = titulo;
            label1.ForeColor = Color.FromArgb(250, 249, 244);
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);

            lblMensaje.ForeColor = Color.FromArgb(48, 56, 48);
            lblMensaje.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);

            btnAceptar.BackColor = Color.FromArgb(66, 101, 70);
            btnAceptar.ForeColor = Color.FromArgb(255, 255, 250);
            btnAceptar.FlatAppearance.BorderColor = Color.FromArgb(57, 87, 61);
            btnAceptar.FlatAppearance.BorderSize = 1;
            btnAceptar.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 117, 84);
            btnAceptar.FlatAppearance.MouseDownBackColor = Color.FromArgb(53, 82, 57);
            btnAceptar.UseVisualStyleBackColor = false;

            btnCancelar.BackColor = Color.FromArgb(231, 232, 222);
            btnCancelar.ForeColor = Color.FromArgb(55, 63, 53);
            btnCancelar.FlatAppearance.BorderColor = Color.FromArgb(207, 211, 198);
            btnCancelar.FlatAppearance.BorderSize = 1;
            btnCancelar.FlatAppearance.MouseOverBackColor = Color.FromArgb(218, 222, 208);
            btnCancelar.FlatAppearance.MouseDownBackColor = Color.FromArgb(203, 209, 191);
            btnCancelar.UseVisualStyleBackColor = false;

            btnCerrar.BackColor = colorEncabezado;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatAppearance.MouseOverBackColor = colorAcento;
            btnCerrar.FlatAppearance.MouseDownBackColor = Color.FromArgb(47, 70, 53);
            btnCerrar.UseVisualStyleBackColor = false;
        }

        private void centrarFormulario()
        {
            Rectangle screeanArea = Screen.PrimaryScreen?.WorkingArea ?? Screen.FromControl(this).WorkingArea;

            // Posición final pegada al borde derecho.
            posicionFinalX = screeanArea.Right - this.Width;

            // Posición vertical centrada.
            int posY = screeanArea.Top + (screeanArea.Height - this.Height) / 2;

            // Comenzar solamente 10 píxeles fuera de la posición final.
            this.Location = new Point(posicionFinalX + 30, posY);
        }

        private void ReproducirSonido()
        {
            Task.Run(() => ReproducirSonidoEnSegundoPlano(tipoMensaje));
        }

        private static void ReproducirSonidoEnSegundoPlano(string tipo)
        {
            try
            {
                using Stream? sonido = tipo switch
                {
                    "error" => Resources.SonidoError,
                    "ok" => Resources.SonidoOk,
                    "info" => Resources.SonidoInfo,
                    "warning" => Resources.SonidoWarning,
                    "confirmar" => Resources.SonidoConfirmar,
                    _ => null
                };

                if (sonido == null)
                    return;

                using SoundPlayer reproductor = new SoundPlayer(sonido);
                reproductor.PlaySync();
            }
            catch (Exception ex)
            {
                Trace.TraceError($"No se pudo reproducir el sonido del mensaje '{tipo}': {ex}");
            }
        }

        #endregion

        #region Evento del formulario 

        private void FrmMensajes_Load_1(object sender, EventArgs e)
        {
            // Calcular la posición final.
            centrarFormulario();

            // Iniciar la animación de entrada.
            timerEntrada.Start();

            ReproducirSonido();

            if (tipoMensaje != "confirmar")
            {
                timer1.Start();
            }

            if (!btnCancelar.Visible)
            {
                btnAceptar.Location = new Point(145, 140);
            }
            else
            {
                btnCancelar.Location = new Point(285, 140);
            }
        }
        #endregion

        #region Evento del timer 

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            this.Opacity -= 0.02;

            if (this.Opacity <= 0)
            {
                timer1.Stop();
                this.Close();
            }
        }

        #endregion

        #region Eventos de los botones
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        #endregion

        #region Evento de entrada
        private void timerEntrada_Tick_1(object sender, EventArgs e)
        {
            // Mover el formulario hacia la izquierda.
            int nuevaPosicionX = this.Location.X - 2;

            // Verificar si llegó a la posición final.
            if (nuevaPosicionX <= posicionFinalX)
            {
                nuevaPosicionX = posicionFinalX;

                timerEntrada.Stop();
            }

            // Aplicar la nueva posición.
            this.Location = new Point(
                nuevaPosicionX,
                this.Location.Y
            );
        }
        #endregion


    }
}
