using GranjaControl.BLL;
using GranjaControl.Entitys;
using GranjaControl.UI.Helpers;
using GranjaControl.UI.Seguridad;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI
{
    public partial class FrmGalponRegistro : Form
    {
        Mensajes mensajes = new Mensajes();
        // Variables para controlar el movimiento de la ventana.
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private readonly GalponBLL _galponBLL;
        private readonly int? _idGalpon;
        private GalponEntity? _galpon;

        private bool EsEdicion => _idGalpon.HasValue;

        #region Construtores
        // Constructor para crear un nuevo galpón
        public FrmGalponRegistro(GalponBLL galponBLL)
        {
            InitializeComponent();
            _galponBLL = galponBLL;
            _idGalpon = null;

            lblTitulo.Text = "Nuevo Rol";
            lblDescripcion.Text = "Complete la información del rol.";
        }

        // Constructor para editar un galpón existente
        public FrmGalponRegistro(GalponBLL galponBLL, int idGalpon)
        {
            InitializeComponent();
            _galponBLL = galponBLL;
            _idGalpon = idGalpon;

            lblTitulo.Text = "Editar Rol";
            lblDescripcion.Text = "Modifique la información del rol.";
        }
        #endregion

        #region Métodos privados

        // Cargar los datos del galpón para editar.
        private async Task CargarDatosAsync()
        {
            try
            {

                _galpon = await _galponBLL.ObtenerPorIdAsync(_idGalpon!.Value);


                if (_idGalpon is null)
                {
                    mensajes.MensajeError("No se encontró el galpón especificado.");

                    DialogResult = DialogResult.Cancel;

                    Close();
                    return;
                }

                txtNombre.Text = _galpon!.NombreGalpon;
                txtCapacidad.Text = _galpon.Capacidad.ToString();
                txtDescripcion.Text = _galpon.Descripcion;


            }
            catch (Exception ex)
            {
                mensajes.MensajeError($"Error al cargar los datos: {ex.Message}");
            }
        }

        // Validar los campos del formulario.

        private bool ValidarControles()
        {
            errorIcon.Clear();

            bool nombreValido = ValidationHelper.Requerido(txtNombre, errorIcon, "Ingrese el nombre del galpón.");

            bool capacidadValida = int.TryParse(txtCapacidad.Text, out int capacidad) && capacidad > 0;

            if (!capacidadValida)
            {
                errorIcon.SetError(txtCapacidad, "La capacidad debe ser un número entero mayor que cero.");
            }

            bool descripcionValida = true;

            if (txtDescripcion.Text.Trim().Length > 250)
            {
                errorIcon.SetError(txtDescripcion, "La descripción no puede superar los 250 caracteres.");

                descripcionValida = false;
            }

            if (txtNombre.Text.Trim().Length > 75)
            {
                errorIcon.SetError(
                    txtNombre,
                    "El nombre no puede superar los 75 caracteres.");

                nombreValido = false;
            }

            return nombreValido && capacidadValida && descripcionValida;
        }


        #endregion

        #region Botones de comando
        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarControles())
                return;

            EfectoBoton(btnGuardar);

            try
            {
                // Crear el objeto con los datos del formulario.
                GalponEntity galpon = new();

                galpon.CambiarNombre(txtNombre.Text.Trim());
                galpon.CambiarCapacidad(int.Parse(txtCapacidad.Text));
                galpon.CambiarDescripcion(txtDescripcion.Text.Trim());

                if (!EsEdicion)
                {
                    // Registrar un nuevo galpón.
                    galpon.CambiarActivo(true);

                    await _galponBLL.AgregarAsync(galpon, UsuarioSesion.NombreUsuario);

                    mensajes.MensajeInformacion("Galpón registrado correctamente");
                }
                else
                {
                    // Asignar el ID del galpón que se está editando.
                    galpon.AsignarId(_idGalpon!.Value);

                    // Consultar el registro existente para conservar su estado.
                    GalponEntity? existente =
                        await _galponBLL.ObtenerPorIdAsync(_idGalpon.Value);

                    if (existente is null)
                    {
                        mensajes.MensajeError("No se encontró el galpón que desea actualizar.");

                        return;
                    }

                    // Mantener el estado actual del galpón.
                    galpon.CambiarActivo(existente.Activo);

                    await _galponBLL.ActualizarAsync(galpon, UsuarioSesion.NombreUsuario);
                    mensajes.MensajeInformacion("Galpón actualizado correctamente.");

                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Exception errorReal = ex.GetBaseException();
                mensajes.MensajeError($"Ocurrió un error al guardar el galpón:\n\n{errorReal.Message}");
            }
        }

        private void Cancelar()
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            EfectoBoton(btnCancelar);
            Cancelar();
           
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }
        #endregion

        #region Movimiento de la ventana

        private void EfectoBoton(Button boton)
        {
            Point posicion = boton.Location;

            boton.Location = new Point(
                posicion.X + 2,
                posicion.Y + 2);

            Application.DoEvents();
            System.Threading.Thread.Sleep(80);

            boton.Location = posicion;
        }


        private void pnlSuperior_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void pnlSuperior_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(
                    Cursor.Position,
                    new Size(dragCursorPoint));

                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void pnlSuperior_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        #endregion

        // Cargar datos al abrir el formulario.
        private async void FrmGalpon_Load(object sender, EventArgs e)
        {

            if (EsEdicion)
                await CargarDatosAsync();
        }

        private void FrmGalpon_Shown(object sender, EventArgs e)
        {

            RedondearControlHelper.RedondearControl(this, 20);

            RedondearControlHelper.RedondearControl(pnlContenido, 20);


            RedondearControlHelper.RedondearControl(btnGuardar, 12);
            RedondearControlHelper.RedondearControl(btnCancelar, 12);

            RedondearControlHelper.RedondearControl(pnlNombre, 12);
            RedondearControlHelper.RedondearControl(pnlCapacidad, 12);
            RedondearControlHelper.RedondearControl(pnlDescripcion, 12);


        }

       
    }
}
