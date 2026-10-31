using GranjaControl.BLL;
using GranjaControl.Entitys.Entidades;
using GranjaControl.UI.Helpers;
using GranjaControl.UI.Seguridad;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Clientes
{
    public partial class FrmRegistroCliente : Form
    {
        private readonly ClienteBLL _clienteBLL;
        private readonly int? _idCliente;
        private readonly Mensajes mensajes = new();

        private ClienteEntity? _cliente;

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private bool EsEdicion => _idCliente.HasValue;

        public FrmRegistroCliente(ClienteBLL clienteBLL)
        {
            InitializeComponent();

            _clienteBLL = clienteBLL;
            _idCliente = null;

            lblTitulo.Text = "Nuevo cliente";
            lblDescripcion.Text =
                "Complete la información del cliente.";
        }

        public FrmRegistroCliente(
            ClienteBLL clienteBLL,
            int idCliente)
        {
            InitializeComponent();

            _clienteBLL = clienteBLL;
            _idCliente = idCliente;

            lblTitulo.Text = "Editar cliente";
            lblDescripcion.Text =
                "Modifique la información del cliente.";
        }

        private void CargarTiposCliente()
        {
            cboTipoCliente.Items.Clear();
            cboTipoCliente.Items.AddRange(
                ClienteEntity.TiposPermitidos.ToArray());

            cboTipoCliente.SelectedIndex = -1;
        }

        private async Task CargarDatosAsync()
        {
            try
            {
                if (!_idCliente.HasValue)
                    return;

                _cliente = await _clienteBLL.ObtenerPorIdAsync(
                    _idCliente.Value);

                if (_cliente is null)
                {
                    mensajes.MensajeError(
                        "No se encontró el cliente especificado.");

                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                txtNombre.Text = _cliente.NombreCliente;
                cboTipoCliente.SelectedItem = _cliente.TipoCliente;
                txtTelefono.Text = _cliente.Telefono ?? string.Empty;
                txtDireccion.Text = _cliente.Direccion ?? string.Empty;
            }
            catch (Exception ex)
            {
                mensajes.MensajeError(
                    $"Error al cargar los datos: {ex.Message}");
            }
        }

        private bool ValidarControles()
        {
            errorIcon.Clear();

            bool valido = true;

            if (!ValidationHelper.Requerido(
                    txtNombre,
                    errorIcon,
                    "Ingrese el nombre del cliente."))
            {
                valido = false;
            }

            if (txtNombre.Text.Trim().Length > 100)
            {
                errorIcon.SetError(
                    txtNombre,
                    "El nombre no puede superar los 100 caracteres.");

                valido = false;
            }

            if (cboTipoCliente.SelectedIndex < 0)
            {
                errorIcon.SetError(
                    cboTipoCliente,
                    "Seleccione el tipo de cliente.");

                valido = false;
            }

            if (txtTelefono.Text.Trim().Length > 25)
            {
                errorIcon.SetError(
                    txtTelefono,
                    "El teléfono no puede superar los 25 caracteres.");

                valido = false;
            }

            if (txtDireccion.Text.Trim().Length > 200)
            {
                errorIcon.SetError(
                    txtDireccion,
                    "La dirección no puede superar los 200 caracteres.");

                valido = false;
            }

            return valido;
        }

        private ClienteEntity CrearCliente()
        {
            ClienteEntity cliente = new(
                txtNombre.Text,
                txtTelefono.Text,
                txtDireccion.Text,
                cboTipoCliente.SelectedItem!.ToString()!);

            if (EsEdicion)
            {
                cliente.AsignarId(_idCliente!.Value);
                cliente.CambiarActivo(_cliente?.Activo ?? true);
            }
            else
            {
                cliente.CambiarActivo(true);
            }

            return cliente;
        }

        private async void FrmRegistroCliente_Load(
            object sender,
            EventArgs e)
        {
            CargarTiposCliente();

            await CargarDatosAsync();
        }

        private async void btnGuardar_Click(
            object sender,
            EventArgs e)
        {
            if (!ValidarControles())
                return;

            try
            {
                btnGuardar.Enabled = false;

                ClienteEntity cliente = CrearCliente();

                if (!EsEdicion)
                {
                    await _clienteBLL.AgregarAsync(
                        cliente,
                        UsuarioSesion.NombreUsuario);

                    mensajes.MensajeOk(
                        "Cliente registrado correctamente.");
                }
                else
                {
                    await _clienteBLL.ActualizarAsync(
                        cliente,
                        UsuarioSesion.NombreUsuario);

                    mensajes.MensajeOk(
                        "Cliente actualizado correctamente.");
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                btnGuardar.Enabled = true;

                mensajes.MensajeError(
                    $"No se pudo guardar el cliente: {ex.Message}");
            }
        }

        private void Cancelar()
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void FrmRegistroCliente_Shown(
            object sender,
            EventArgs e)
        {
            RedondearControlHelper.RedondearControl(this, 20);
            RedondearControlHelper.RedondearControl(pnlContenido, 20);
            RedondearControlHelper.RedondearControl(pnlAcciones, 20);

            RedondearControlHelper.RedondearControl(btnGuardar, 12);
            RedondearControlHelper.RedondearControl(btnCancelar, 12);

            RedondearControlHelper.RedondearControl(pnlNombre, 12);
            RedondearControlHelper.RedondearControl(pnlTipo, 12);
            RedondearControlHelper.RedondearControl(pnlTelefono, 12);
            RedondearControlHelper.RedondearControl(pnlDireccion, 12);
        }

        private void pnlSuperior_MouseDown(
            object sender,
            MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = Location;
        }

        private void pnlSuperior_MouseMove(
            object sender,
            MouseEventArgs e)
        {
            if (dragging)
            {
                Point diferencia = Point.Subtract(
                    Cursor.Position,
                    new Size(dragCursorPoint));

                Location = Point.Add(
                    dragFormPoint,
                    new Size(diferencia));
            }
        }

        private void pnlSuperior_MouseUp(
            object sender,
            MouseEventArgs e)
        {
            dragging = false;
        }
    }
}
