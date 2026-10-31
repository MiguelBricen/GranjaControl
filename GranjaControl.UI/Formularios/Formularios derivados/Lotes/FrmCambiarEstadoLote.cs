using GranjaControl.BLL;
using GranjaControl.Entitys.Entidades;
using GranjaControl.UI.Helpers;
using GranjaControl.UI.Seguridad;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios.Formularios_derivados.Lotes
{
    public partial class FrmCambiarEstadoLote : Form
    {
        private readonly LoteBLL _loteBLL;
        private readonly int _idLote;
        private readonly string _estadoActual;
        private readonly Mensajes mensajes = new();

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public FrmCambiarEstadoLote(
            LoteBLL loteBLL,
            int idLote,
            string estadoActual)
        {
            InitializeComponent();

            _loteBLL = loteBLL;
            _idLote = idLote;
            _estadoActual = estadoActual;
        }

        private void CargarEstados()
        {
            cboEstado.Items.Clear();
            cboEstado.Items.AddRange(
                LoteEntity.EstadosPermitidos.ToArray());

            cboEstado.SelectedItem = _estadoActual;
        }

        private void FrmCambiarEstadoLote_Load(
            object sender,
            EventArgs e)
        {
            if (!RolesSistema.Autorizar(this, RolesSistema.PuedeGestionarLotes()))
                return;

            CargarEstados();

            lblEstadoActual.Text =
                $"Estado actual: {_estadoActual}";
        }

        private async void btnGuardar_Click(
            object sender,
            EventArgs e)
        {
            if (cboEstado.SelectedIndex < 0)
            {
                mensajes.MensajeValidacion(
                    "Seleccione el nuevo estado del lote.");

                return;
            }

            string estado = cboEstado.SelectedItem!.ToString()!;

            DialogResult respuesta = mensajes.MensajeConfirmacion(
                $"¿Desea cambiar el estado del lote a \"{estado}\"?");

            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                btnGuardar.Enabled = false;

                await _loteBLL.CambiarEstadoAsync(
                    _idLote,
                    estado,
                    UsuarioSesion.NombreUsuario);

                mensajes.MensajeOk(
                    "Estado del lote actualizado correctamente.");

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                btnGuardar.Enabled = true;

                mensajes.MensajeError(
                    $"No se pudo cambiar el estado del lote: {ex.Message}");
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

        private void FrmCambiarEstadoLote_Shown(
            object sender,
            EventArgs e)
        {
            RedondearControlHelper.RedondearControl(this, 20);
            RedondearControlHelper.RedondearControl(pnlContenido, 20);
            RedondearControlHelper.RedondearControl(pnlAcciones, 20);

            RedondearControlHelper.RedondearControl(btnGuardar, 12);
            RedondearControlHelper.RedondearControl(btnCancelar, 12);
            RedondearControlHelper.RedondearControl(pnlEstado, 12);
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
