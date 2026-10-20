using GranjaControl.UI.Properties;
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

            tipoMensaje = tipo.ToLower();

            lblMensaje.Text = mensaje;
            lblMensaje.MaximumSize = new Size(350, 0);

            SoundPlayer sonido = null;

            switch (tipo.ToLower())
            {
                case "error":
                    sonido = new SoundPlayer(Properties.Resources.SonidoError);
                    picIcono.Image = Resources.IconError50;
                    this.BackColor = Color.FromArgb(220, 53, 69); // rojo
                    break;

                case "ok":
                    sonido = new SoundPlayer(Properties.Resources.SonidoOk);
                    picIcono.Image = Resources.IconOk50;
                    this.BackColor = Color.FromArgb(25, 135, 84); // verde
                    break;

                case "info":
                    sonido = new SoundPlayer(Properties.Resources.SonidoInfo);
                    picIcono.Image = Resources.IconInformacion50;
                    this.BackColor = Color.FromArgb(13, 110, 153); // azul
                    break;

                case "warning":
                    sonido = new SoundPlayer(Properties.Resources.SonidoWarning);
                    picIcono.Image = Resources.IconPeligro50;
                    this.BackColor = Color.FromArgb(255, 152, 0); // naranja
                    break;

                case "confirmar":
                    sonido = new SoundPlayer(Properties.Resources.SonidoConfirmar);
                    picIcono.Image = Resources.IconConfirmacion50;
                    this.BackColor = Color.FromArgb(108, 117, 125); // gris
                    break;
            }
        }

        #region Metodos

        // Reproducir sonido según el tipo de mensaje.
        private void ReproducirSonido()
        {

            SoundPlayer sonido;

            switch (tipoMensaje)
            {
                case "error":
                    sonido = new SoundPlayer(Properties.Resources.SonidoError);
                    break;

                case "ok":
                    sonido = new SoundPlayer(Properties.Resources.SonidoOk);
                    break;

                case "info":
                    sonido = new SoundPlayer(Properties.Resources.SonidoInfo);
                    break;

                case "warning":
                    sonido = new SoundPlayer(Properties.Resources.SonidoWarning);
                    break;

                case "confirmar":
                    sonido = new SoundPlayer(Properties.Resources.SonidoConfirmar);
                    break;

                default:
                    return;
            }

            sonido.Play();
        }

        private void centrarFormulario()
        {
            Rectangle screeanArea = Screen.PrimaryScreen.WorkingArea;

            // Posición final pegada al borde derecho.
            posicionFinalX = screeanArea.Right - this.Width;

            // Posición vertical centrada.
            int posY = screeanArea.Top + (screeanArea.Height - this.Height) / 2;

            // Comenzar solamente 10 píxeles fuera de la posición final.
            this.Location = new Point(posicionFinalX + 30, posY);
        }

        private void EfectoBoton(Button boton)
        {
            Point posicion = boton.Location;

            boton.Location = new Point(
                posicion.X + 2,
                posicion.Y + 2
            );

            Application.DoEvents();

            System.Threading.Thread.Sleep(80);

            boton.Location = posicion;
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
            EfectoBoton(btnCancelar);
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            EfectoBoton(btnAceptar);

            DialogResult = DialogResult.Yes;
            Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            EfectoBoton(btnCerrar);
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
