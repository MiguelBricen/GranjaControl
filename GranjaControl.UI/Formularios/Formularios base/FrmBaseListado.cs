using GranjaControl.UI.Helpers;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios_base
{
    public partial class FrmBaseListado : Form
    {
        protected int PaginaActual = 1;
        protected int TamPagina = 5;
        protected int TotalRegistros = 0;
        protected int TotalPaginas = 0;

        // Variables para controlar el movimiento de la ventana.
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        // Clase para mostrar mensajes personalizados.
        private readonly Mensajes mensajes = new();

        public FrmBaseListado()
        {
            InitializeComponent();
        }

        #region Métodos virtuales

        // Controlar la visibilidad de los botones.
        protected virtual bool MostrarBotonPrincipal => true;
        protected virtual bool MostrarBotonSecundario => true;
        protected virtual bool MostrarBotonTerciario => true;

        // Cargar los datos del listado.
        protected virtual Task MostrarDatosAsync(int pagina, int tamPagina, string? buscar = null)
        {
            return Task.CompletedTask;
        }

        protected void ActualizarBotonesPaginacion()
        {
            // Sin registros, deshabilitar todos los botones    
            if (TotalRegistros <= 0)
            {
                btnPrimero.Enabled = false;
                btnAnterior.Enabled = false;
                btnSiguiente.Enabled = false;
                btnUltimo.Enabled = false;

                return;
            }

            // Primera página
            btnPrimero.Enabled = PaginaActual > 1;
            btnAnterior.Enabled = PaginaActual > 1;

            // Última página
            btnSiguiente.Enabled = PaginaActual < TotalPaginas;
            btnUltimo.Enabled = PaginaActual < TotalPaginas;
        }

        protected void ActualizarPaginacion()
        {
            if (TotalRegistros == 0)
            {
                lblPagina.Text = "Página 0 de 0";
                lblTotalregistro.Text = $"Total de registros: 0";

                ActualizarBotonesPaginacion();
                return;
            }
            lblPagina.Text = $"Página {PaginaActual} de {TotalPaginas}";
            lblTotalregistro.Text = $"Total de registros: {TotalRegistros}";
            ActualizarBotonesPaginacion();
        }

        // Crear el formulario para agregar un registro.
        protected virtual Form? CrearFormularioAccionPrincipal()
        {
            return null;
        }

        // Crear el formulario para editar un registro.
        protected virtual Form? CrearFormularioAccionSecundaria(int id)
        {
            return null;
        }

        // Obtener el ID del registro seleccionado.
        protected virtual int? ObtenerIdSeleccionado()
        {
            return null;
        }

        // Desactivar el registro seleccionado.
        protected virtual Task DesactivarAsync()
        {
            return Task.CompletedTask;
        }

        // Personalizar el formato de las celdas.
        protected virtual void FormatearCelda(DataGridViewCellFormattingEventArgs e)
        {
            if (dgvListado.Columns[e.ColumnIndex].Name != "Estado")
                return;

            GridColorHelper.FormatearActivoInactivo(e);
        }

        protected void AjustarColumnas(params string[] columnas)
        {
            foreach (string columna in columnas)
            {
                if (!dgvListado.Columns.Contains(columna))
                    continue;

                dgvListado.Columns[columna]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
        #endregion

        #region Al cargar el formulario
        protected void MostrarEstadoVacio(bool tieneDatos)
        {
            dgvListado.Visible = tieneDatos;
            // picEmpty.Visible = !tieneDatos;
        }
        private async void FrmBaseListadoGalpon_Load(object sender, EventArgs e)
        {
            btnNuevo.Visible = MostrarBotonPrincipal;
            btnEditar.Visible = MostrarBotonSecundario;
            btnEliminar.Visible = MostrarBotonTerciario;

            GridDashboardHelper.Personalizar(dgvListado);

            await MostrarDatosAsync(PaginaActual, TamPagina, txtBuscar.Text);
        }
        #endregion

        #region Botones de comando
        private async void btnNuevo_Click(object sender, EventArgs e)
        {


            EfectoBoton((Button)sender);

            Form? frm = CrearFormularioAccionPrincipal();

            if (frm == null)
                return;

            using (frm)
            {
                DialogResult resultado = MostrarModal.MostrarConCapa(this, frm);

                if (resultado == DialogResult.OK)
                    await MostrarDatosAsync(PaginaActual, TamPagina, txtBuscar.Text);
            }
            /*if (frm.ShowDialog() == DialogResult.OK)
                await MostrarDatosAsync();*/
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            EfectoBoton((Button)sender);

            int? id = ObtenerIdSeleccionado();

            if (id is null)
            {
                mensajes.MensajeInformacion("Debe seleccionar un registro.");
                return;
            }

            Form? frm = CrearFormularioAccionSecundaria(id.Value);

            if (frm == null)
                return;


            using (frm)
            {
                DialogResult resultado = MostrarModal.MostrarConCapa(this, frm);

                if (resultado == DialogResult.OK)
                    await MostrarDatosAsync(PaginaActual, TamPagina, txtBuscar.Text);
            }

            /*if (frm.ShowDialog() == DialogResult.OK)
                await MostrarDatosAsync();*/
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            EfectoBoton((Button)sender);

            await DesactivarAsync();

        }
        private void btnVolver_Click(object sender, EventArgs e)
        {
            EfectoBoton(btnVolver);

            NavegacionHelper.RestablecerBotonActivo();

            this.Close();
        }
        #endregion

        #region Movimiento del panel superior y efectos de botones
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

        private void FrmBaseListadoGalpon_Shown(object sender, EventArgs e)
        {

            RedondearControlHelper.RedondearControl(pnlContenido, 20);
            RedondearControlHelper.RedondearControl(pnlPaginacion, 20);

            RedondearControlHelper.RedondearControl(btnNuevo, 8);
            RedondearControlHelper.RedondearControl(btnEditar, 8);
            RedondearControlHelper.RedondearControl(btnEliminar, 8);

            RedondearControlHelper.RedondearControl(dgvListado, 15);
            //RedondearControlHelper.RedondearControl(picEmpty, 15);
            RedondearControlHelper.RedondearControl(pnlBuscar, 15);
        }

        private async void txtBuscar_TextChanged(object sender, EventArgs e)
        {

            PaginaActual = 1;

            await MostrarDatosAsync(PaginaActual, TamPagina, txtBuscar.Text);
        }

        private void dgvGalpones_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            FormatearCelda(e);
        }
        #region Botones del pnl Paginación

        private async void btnPrimero_Click(object sender, EventArgs e)
        {
            PaginaActual = 1;

            await MostrarDatosAsync(PaginaActual, TamPagina, txtBuscar.Text);
        }

        private async void btnAnterior_Click(object sender, EventArgs e)
        {

            if (PaginaActual <= 1)
                return;

            PaginaActual--;

            await MostrarDatosAsync(PaginaActual, TamPagina, txtBuscar.Text);
        }

        private async void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (PaginaActual >= TotalPaginas)
                return;

            PaginaActual++;

            await MostrarDatosAsync(PaginaActual, TamPagina, txtBuscar.Text);
        }

        private async void btnUltimo_Click(object sender, EventArgs e)
        {
            PaginaActual = TotalPaginas;

            await MostrarDatosAsync(PaginaActual, TamPagina, txtBuscar.Text);
        }
        #endregion

        
    }
}
