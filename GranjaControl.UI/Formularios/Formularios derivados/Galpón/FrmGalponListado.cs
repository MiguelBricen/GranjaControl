using GranjaControl.BLL;
using GranjaControl.DAL;
using GranjaControl.UI.Formularios_base;
using GranjaControl.UI.Seguridad;
using GranjaControl.UI.Utilidades;

namespace GranjaControl.UI.Formularios_derivados
{
    public partial class FrmGalponListado : FrmBaseListado
    {
        private readonly GalponBLL _galponBLL;
        private readonly Mensajes mensajes = new();

        public FrmGalponListado()
        {
            InitializeComponent();

            _galponBLL = new GalponBLL(new GalponDAL());
        }

        protected override async Task MostrarDatosAsync(int pagina, int tamPagina, string? buscar = null)
        {
            try
            {
                var resultado = await _galponBLL.MostrarAsync(pagina, tamPagina, buscar);

                dgvListado.DataSource = null;
                dgvListado.DataSource = resultado.Registros;


                PaginaActual = resultado.PaginaActual;
                TamPagina = resultado.TamPagina;
                TotalRegistros = resultado.TotalRegistros;
                TotalPaginas = resultado.TotalPaginas;

                ActualizarPaginacion();
                ConfigurarGrid();
            }
            catch (Exception ex)
            {
                mensajes.MensajeError($"Error al mostrar los galpones: {ex.Message}");
             
            }
        }


        private void ConfigurarGrid()
        {
            // Configuracion general del GridView
            dgvListado.BorderStyle = BorderStyle.None;
            dgvListado.BackgroundColor = Color.White;
            dgvListado.GridColor = Color.FromArgb(220, 228, 222);

            dgvListado.EnableHeadersVisualStyles = false;
            dgvListado.RowHeadersVisible = false;

            dgvListado.AllowUserToAddRows = false;
            dgvListado.AllowUserToDeleteRows = false;
            dgvListado.ReadOnly = true;
            dgvListado.MultiSelect = false;
            dgvListado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvListado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dgvListado.ColumnHeadersHeight = 42;
            dgvListado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgvListado.RowTemplate.Height = 36;
            dgvListado.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

          
            // Encabezados         
            dgvListado.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 76, 56);

            dgvListado.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgvListado.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(32, 76, 56);

            dgvListado.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            dgvListado.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            dgvListado.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvListado.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // Filas       
            dgvListado.DefaultCellStyle.BackColor = Color.White;

            dgvListado.DefaultCellStyle.ForeColor = Color.Black;

            dgvListado.DefaultCellStyle.SelectionBackColor = Color.FromArgb(226, 239, 228);

            dgvListado.DefaultCellStyle.SelectionForeColor = Color.FromArgb(32, 76, 56);

            dgvListado.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            dgvListado.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvListado.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);

            dgvListado.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

         
            /*// FILAS ALTERNAS          
            dgvListado.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(248, 250, 248);

            dgvListado.AlternatingRowsDefaultCellStyle.ForeColor =
                Color.Black;

            dgvListado.AlternatingRowsDefaultCellStyle.SelectionBackColor =
                Color.FromArgb(226, 239, 228);

            dgvListado.AlternatingRowsDefaultCellStyle.SelectionForeColor =
                Color.FromArgb(32, 76, 56);

            dgvListado.AlternatingRowsDefaultCellStyle.Font =
                new Font("Segoe UI", 11, FontStyle.Bold);*/

            // VISIBILIDAD Y ENCABEZADOS
           
            dgvListado.Columns["Id"]!.Visible = false;
            dgvListado.Columns["Activo"]!.Visible = false;

            dgvListado.Columns["NombreGalpon"]!.HeaderText = "Nombre del galpón";

            dgvListado.Columns["Capacidad"]!.HeaderText = "Capacidad (aves)";

            dgvListado.Columns["Descripcion"]!.HeaderText = "Descripción";

            dgvListado.Columns["Estado"]!.HeaderText = "Estado";

            
            // Ancho de las columnas
            dgvListado.Columns["NombreGalpon"]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvListado.Columns["NombreGalpon"]!.FillWeight = 30;

            dgvListado.Columns["Capacidad"]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgvListado.Columns["Descripcion"]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvListado.Columns["Descripcion"]!.FillWeight = 55;

            dgvListado.Columns["Estado"]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            
            // Alineación del contenido
           
            // Nombre del galpón: centrado a la izquierda
            dgvListado.Columns["NombreGalpon"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            // Capacidad: centrada.
            dgvListado.Columns["Capacidad"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            // Descripción: alineada a la izquierda.
            dgvListado.Columns["Descripcion"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            // Agregar espacio interior a la descripción.
            dgvListado.Columns["Descripcion"]!.DefaultCellStyle.Padding =
                new Padding(12, 0, 8, 0);

            // Estado: centrado.
            dgvListado.Columns["Estado"]!.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            // Ajuste adicional de columnas existente en tu proyecto.
            AjustarColumnas("Estado");
        }



        protected override Form? CrearFormularioAccionPrincipal()
        {
            return new FrmGalponRegistro(_galponBLL);
        }

        protected override Form? CrearFormularioAccionSecundaria(int id)
        {
            return new FrmGalponRegistro(_galponBLL, id);
        }

        protected override int? ObtenerIdSeleccionado()
        {
            if (dgvListado.CurrentRow == null)
                return null;

            return Convert.ToInt32(dgvListado.CurrentRow.Cells["Id"].Value);
        }

        protected override async Task DesactivarAsync()
        {
            int? id = ObtenerIdSeleccionado();

            if (id == null)
            {
                mensajes.MensajeInformacion("Seleccione un galpón para desactivarlo.");

                return;
            }
            bool activo = Convert.ToBoolean(dgvListado.CurrentRow!.Cells["Activo"].Value);

            string accion = activo ? "desactivar" : "activar";

            DialogResult respuesta = mensajes.MensajeConfirmacion($"¿Desea {accion} este registro?");

            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                await _galponBLL.CambiarEstadoAsync(id.Value, !activo, UsuarioSesion.NombreUsuario);
                await MostrarDatosAsync(PaginaActual, TamPagina, txtBuscar.Text);
            }
            catch (Exception ex)
            {
       
                MessageBox.Show(ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

      
    }
}
