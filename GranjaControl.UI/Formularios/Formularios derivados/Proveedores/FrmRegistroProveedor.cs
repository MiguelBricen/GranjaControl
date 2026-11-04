using GranjaControl.BLL;
using GranjaControl.Entitys.Entidades;
using GranjaControl.UI.Helpers;
using GranjaControl.UI.Seguridad;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Proveedores
{
    public partial class FrmRegistroProveedor : Form
    {
        private readonly ProveedorBLL _proveedorBLL;
        private readonly int? _idProveedor;
        private readonly Mensajes mensajes = new();

        private ProveedorEntity? _proveedor;

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private bool EsEdicion => _idProveedor.HasValue;

        public FrmRegistroProveedor(ProveedorBLL proveedorBLL)
        {
            InitializeComponent();

            _proveedorBLL = proveedorBLL;
            _idProveedor = null;

            lblTitulo.Text = "Nuevo proveedor";
            lblDescripcion.Text = "Complete la información del proveedor.";
        }

        public FrmRegistroProveedor(
            ProveedorBLL proveedorBLL,
            int idProveedor)
        {
            InitializeComponent();

            _proveedorBLL = proveedorBLL;
            _idProveedor = idProveedor;

            lblTitulo.Text = "Editar proveedor";
            lblDescripcion.Text = "Modifique la información del proveedor.";
        }

        private void CargarTiposProveedor()
        {
            cboTipoProveedor.Items.Clear();
            cboTipoProveedor.Items.AddRange(
                ProveedorEntity.TiposPermitidos.ToArray());

            cboTipoProveedor.SelectedIndex = -1;
        }

        private async Task CargarDatosAsync()
        {
            try
            {
                if (!_idProveedor.HasValue)
                    return;

                _proveedor = await _proveedorBLL.ObtenerPorIdAsync(_idProveedor.Value);

                if (_proveedor is null)
                {
                    mensajes.MensajeError("No se encontró el proveedor especificado.");

                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                txtNombre.Text = _proveedor.NombreProveedor;
                cboTipoProveedor.SelectedItem = _proveedor.TipoProveedor;
                txtTelefono.Text = _proveedor.Telefono ?? string.Empty;
                txtCorreo.Text = _proveedor.CorreoElectronico ?? string.Empty;
                txtDireccion.Text = _proveedor.Direccion ?? string.Empty;
            }
            catch (Exception ex)
            {
                mensajes.MensajeError($"Error al cargar los datos: {ex.Message}");
            }
        }

        private bool ValidarControles()
        {
            errorIcon.Clear();

            bool valido = true;

            if (!ValidationHelper.Requerido(txtNombre, errorIcon,"Ingrese el nombre del proveedor."))
            {
                valido = false;
            }

            if (txtNombre.Text.Trim().Length > 100)
            {
                errorIcon.SetError(txtNombre,"El nombre no puede superar los 100 caracteres.");

                valido = false;
            }

            if (cboTipoProveedor.SelectedIndex < 0)
            {
                errorIcon.SetError(cboTipoProveedor,"Seleccione el tipo de proveedor.");

                valido = false;
            }

            if (txtTelefono.Text.Trim().Length > 25)
            {
                errorIcon.SetError(txtTelefono,"El teléfono no puede superar los 25 caracteres.");

                valido = false;
            }

            if (txtCorreo.Text.Trim().Length > 100)
            {
                errorIcon.SetError(txtCorreo,"El correo no puede superar los 100 caracteres.");

                valido = false;
            }
            else if (!string.IsNullOrWhiteSpace(txtCorreo.Text) && !ValidationHelper.EmailValido(txtCorreo, errorIcon))
            {
                valido = false;
            }

            if (txtDireccion.Text.Trim().Length > 200)
            {
                errorIcon.SetError(txtDireccion, "La dirección no puede superar los 200 caracteres.");

                valido = false;
            }

            return valido;
        }

        private async void FrmRegistroProveedor_Load(object sender,EventArgs e)
        {
            CargarTiposProveedor();

            if (EsEdicion)
                await CargarDatosAsync();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarControles())
                return;

            try
            {
                ProveedorEntity proveedor = new();

                proveedor.CambiarNombre(txtNombre.Text);
                proveedor.CambiarTipoProveedor(cboTipoProveedor.SelectedItem!.ToString()!);

                proveedor.CambiarTelefono(txtTelefono.Text);
                proveedor.CambiarCorreo(txtCorreo.Text);
                proveedor.CambiarDireccion(txtDireccion.Text);

                if (!EsEdicion)
                {
                    proveedor.CambiarActivo(true);

                    await _proveedorBLL.AgregarAsync(proveedor,UsuarioSesion.NombreUsuario);

                    mensajes.MensajeOk("Proveedor registrado correctamente.");
                }
                else
                {
                    proveedor.AsignarId(_idProveedor!.Value);
                    proveedor.CambiarActivo(_proveedor?.Activo ?? true);

                    await _proveedorBLL.ActualizarAsync(proveedor, UsuarioSesion.NombreUsuario);

                    mensajes.MensajeOk("Proveedor actualizado correctamente.");
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                mensajes.MensajeError($"No se pudo guardar el proveedor: {ex.Message}");
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

        private void FrmRegistroProveedor_Shown(object sender, EventArgs e)
        {
            RedondearControlHelper.RedondearControl(this, 20);
            RedondearControlHelper.RedondearControl(pnlContenido, 20);
            RedondearControlHelper.RedondearControl(pnlAcciones, 20);

            RedondearControlHelper.RedondearControl(btnGuardar, 12);
            RedondearControlHelper.RedondearControl(btnCancelar, 12);

            RedondearControlHelper.RedondearControl(pnlNombre, 12);
            RedondearControlHelper.RedondearControl(pnlTipo, 12);
            RedondearControlHelper.RedondearControl(pnlTelefono, 12);
            RedondearControlHelper.RedondearControl(pnlCorreo, 12);
            RedondearControlHelper.RedondearControl(pnlDireccion, 12);
        }

        private void pnlSuperior_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = Location;
        }

        private void pnlSuperior_MouseMove(object sender,MouseEventArgs e)
        {
            if (dragging)
            {
                Point diferencia = Point.Subtract( Cursor.Position, new Size(dragCursorPoint));

                Location = Point.Add(dragFormPoint, new Size(diferencia));
            }
        }

        private void pnlSuperior_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
    }
}
