namespace GranjaControl.UI.Formularios.Formularios_derivados.Reportes
{
    partial class FrmDashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlFondo = new Panel();
            tlpPrincipal = new TableLayoutPanel();
            pnlEncabezado = new Panel();
            tlpEncabezado = new TableLayoutPanel();
            pnlTituloPrincipal = new Panel();
            lblTitulo = new Label();
            lblSubtitulo = new Label();
            tlpAcciones = new TableLayoutPanel();
            lblActualizado = new Label();
            btnActualizar = new Button();
            tlpTarjetas = new TableLayoutPanel();
            pnlDisponibilidad = new Panel();
            tlpDisponibilidad = new TableLayoutPanel();
            lblDisponibilidadTitulo = new Label();
            prgDisponibilidad = new ProgressBar();
            lblDisponibilidadValor = new Label();
            tlpDetalle = new TableLayoutPanel();
            pnlLotes = new Panel();
            tlpLotes = new TableLayoutPanel();
            pnlEncabezadoLotes = new Panel();
            lblLotesTitulo = new Label();
            lblLotesSubtitulo = new Label();
            dgvLotes = new DataGridView();
            pnlVentas = new Panel();
            tlpVentas = new TableLayoutPanel();
            pnlEncabezadoVentas = new Panel();
            lblVentasTitulo = new Label();
            lblVentasSubtitulo = new Label();
            dgvVentas = new DataGridView();
            pnlFondo.SuspendLayout();
            tlpPrincipal.SuspendLayout();
            pnlEncabezado.SuspendLayout();
            tlpEncabezado.SuspendLayout();
            pnlTituloPrincipal.SuspendLayout();
            tlpAcciones.SuspendLayout();
            pnlDisponibilidad.SuspendLayout();
            tlpDisponibilidad.SuspendLayout();
            tlpDetalle.SuspendLayout();
            pnlLotes.SuspendLayout();
            tlpLotes.SuspendLayout();
            pnlEncabezadoLotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLotes).BeginInit();
            pnlVentas.SuspendLayout();
            tlpVentas.SuspendLayout();
            pnlEncabezadoVentas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVentas).BeginInit();
            SuspendLayout();
            // 
            // pnlFondo
            // 
            pnlFondo.BackColor = Color.FromArgb(243, 247, 244);
            pnlFondo.Controls.Add(tlpPrincipal);
            pnlFondo.Dock = DockStyle.Fill;
            pnlFondo.Location = new Point(0, 0);
            pnlFondo.Name = "pnlFondo";
            pnlFondo.Padding = new Padding(18);
            pnlFondo.Size = new Size(1000, 630);
            pnlFondo.TabIndex = 0;
            // 
            // tlpPrincipal
            // 
            tlpPrincipal.ColumnCount = 1;
            tlpPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPrincipal.Controls.Add(pnlEncabezado, 0, 0);
            tlpPrincipal.Controls.Add(tlpTarjetas, 0, 1);
            tlpPrincipal.Controls.Add(pnlDisponibilidad, 0, 2);
            tlpPrincipal.Controls.Add(tlpDetalle, 0, 3);
            tlpPrincipal.Dock = DockStyle.Fill;
            tlpPrincipal.Location = new Point(18, 18);
            tlpPrincipal.Name = "tlpPrincipal";
            tlpPrincipal.RowCount = 4;
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 204F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            tlpPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPrincipal.Size = new Size(964, 594);
            tlpPrincipal.TabIndex = 0;
            // 
            // pnlEncabezado
            // 
            pnlEncabezado.Controls.Add(tlpEncabezado);
            pnlEncabezado.Dock = DockStyle.Fill;
            pnlEncabezado.Location = new Point(0, 0);
            pnlEncabezado.Margin = new Padding(0, 0, 0, 8);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(964, 64);
            pnlEncabezado.TabIndex = 0;
            // 
            // tlpEncabezado
            // 
            tlpEncabezado.ColumnCount = 2;
            tlpEncabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68F));
            tlpEncabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            tlpEncabezado.Controls.Add(pnlTituloPrincipal, 0, 0);
            tlpEncabezado.Controls.Add(tlpAcciones, 1, 0);
            tlpEncabezado.Dock = DockStyle.Fill;
            tlpEncabezado.Location = new Point(0, 0);
            tlpEncabezado.Name = "tlpEncabezado";
            tlpEncabezado.RowCount = 1;
            tlpEncabezado.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpEncabezado.Size = new Size(964, 64);
            tlpEncabezado.TabIndex = 0;
            // 
            // pnlTituloPrincipal
            // 
            pnlTituloPrincipal.Controls.Add(lblTitulo);
            pnlTituloPrincipal.Controls.Add(lblSubtitulo);
            pnlTituloPrincipal.Dock = DockStyle.Fill;
            pnlTituloPrincipal.Location = new Point(0, 0);
            pnlTituloPrincipal.Margin = new Padding(0);
            pnlTituloPrincipal.Name = "pnlTituloPrincipal";
            pnlTituloPrincipal.Size = new Size(655, 64);
            pnlTituloPrincipal.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(23, 61, 43);
            lblTitulo.Location = new Point(0, -12);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(289, 47);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Panel de control";
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Segoe UI", 10F);
            lblSubtitulo.ForeColor = Color.FromArgb(90, 105, 95);
            lblSubtitulo.Location = new Point(2, 38);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(408, 23);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Resumen operativo y actividad reciente de la granja.";
            // 
            // tlpAcciones
            // 
            tlpAcciones.ColumnCount = 2;
            tlpAcciones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpAcciones.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 132F));
            tlpAcciones.Controls.Add(lblActualizado, 0, 0);
            tlpAcciones.Controls.Add(btnActualizar, 1, 0);
            tlpAcciones.Dock = DockStyle.Fill;
            tlpAcciones.Location = new Point(655, 0);
            tlpAcciones.Margin = new Padding(0);
            tlpAcciones.Name = "tlpAcciones";
            tlpAcciones.RowCount = 1;
            tlpAcciones.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpAcciones.Size = new Size(309, 64);
            tlpAcciones.TabIndex = 1;
            // 
            // lblActualizado
            // 
            lblActualizado.Dock = DockStyle.Fill;
            lblActualizado.Font = new Font("Segoe UI", 9F);
            lblActualizado.ForeColor = Color.FromArgb(100, 116, 108);
            lblActualizado.Location = new Point(0, 0);
            lblActualizado.Margin = new Padding(0);
            lblActualizado.Name = "lblActualizado";
            lblActualizado.Size = new Size(177, 64);
            lblActualizado.TabIndex = 0;
            lblActualizado.Text = "Actualizado: --";
            lblActualizado.TextAlign = ContentAlignment.BottomRight;
            // 
            // btnActualizar
            // 
            btnActualizar.Anchor = AnchorStyles.None;
            btnActualizar.BackColor = Color.FromArgb(23, 61, 43);
            btnActualizar.Cursor = Cursors.Hand;
            btnActualizar.FlatAppearance.BorderSize = 0;
            btnActualizar.FlatStyle = FlatStyle.Flat;
            btnActualizar.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            btnActualizar.ForeColor = Color.White;
            btnActualizar.Location = new Point(183, 15);
            btnActualizar.Name = "btnActualizar";
            btnActualizar.Size = new Size(120, 34);
            btnActualizar.TabIndex = 1;
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseVisualStyleBackColor = false;
            btnActualizar.Click += btnActualizar_Click;
            // 
            // tlpTarjetas
            // 
            tlpTarjetas.ColumnCount = 4;
            tlpTarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpTarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpTarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpTarjetas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpTarjetas.Dock = DockStyle.Fill;
            tlpTarjetas.Location = new Point(0, 72);
            tlpTarjetas.Margin = new Padding(0, 0, 0, 8);
            tlpTarjetas.Name = "tlpTarjetas";
            tlpTarjetas.RowCount = 2;
            tlpTarjetas.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpTarjetas.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpTarjetas.Size = new Size(964, 196);
            tlpTarjetas.TabIndex = 1;
            // 
            // pnlDisponibilidad
            // 
            pnlDisponibilidad.BackColor = Color.White;
            pnlDisponibilidad.Controls.Add(tlpDisponibilidad);
            pnlDisponibilidad.Dock = DockStyle.Fill;
            pnlDisponibilidad.Location = new Point(0, 276);
            pnlDisponibilidad.Margin = new Padding(0, 0, 0, 8);
            pnlDisponibilidad.Name = "pnlDisponibilidad";
            pnlDisponibilidad.Padding = new Padding(16, 10, 16, 10);
            pnlDisponibilidad.Size = new Size(964, 56);
            pnlDisponibilidad.TabIndex = 2;
            // 
            // tlpDisponibilidad
            // 
            tlpDisponibilidad.ColumnCount = 3;
            tlpDisponibilidad.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));
            tlpDisponibilidad.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpDisponibilidad.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            tlpDisponibilidad.Controls.Add(lblDisponibilidadTitulo, 0, 0);
            tlpDisponibilidad.Controls.Add(prgDisponibilidad, 1, 0);
            tlpDisponibilidad.Controls.Add(lblDisponibilidadValor, 2, 0);
            tlpDisponibilidad.Dock = DockStyle.Fill;
            tlpDisponibilidad.Location = new Point(16, 10);
            tlpDisponibilidad.Name = "tlpDisponibilidad";
            tlpDisponibilidad.RowCount = 1;
            tlpDisponibilidad.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpDisponibilidad.Size = new Size(932, 36);
            tlpDisponibilidad.TabIndex = 0;
            // 
            // lblDisponibilidadTitulo
            // 
            lblDisponibilidadTitulo.Dock = DockStyle.Fill;
            lblDisponibilidadTitulo.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            lblDisponibilidadTitulo.ForeColor = Color.FromArgb(35, 55, 45);
            lblDisponibilidadTitulo.Location = new Point(0, 0);
            lblDisponibilidadTitulo.Margin = new Padding(0);
            lblDisponibilidadTitulo.Name = "lblDisponibilidadTitulo";
            lblDisponibilidadTitulo.Size = new Size(300, 36);
            lblDisponibilidadTitulo.TabIndex = 0;
            lblDisponibilidadTitulo.Text = "Disponibilidad de aves en lotes activos";
            lblDisponibilidadTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // prgDisponibilidad
            // 
            prgDisponibilidad.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            prgDisponibilidad.Location = new Point(312, 7);
            prgDisponibilidad.Margin = new Padding(12, 0, 12, 0);
            prgDisponibilidad.Name = "prgDisponibilidad";
            prgDisponibilidad.Size = new Size(438, 22);
            prgDisponibilidad.Style = ProgressBarStyle.Continuous;
            prgDisponibilidad.TabIndex = 1;
            // 
            // lblDisponibilidadValor
            // 
            lblDisponibilidadValor.Dock = DockStyle.Fill;
            lblDisponibilidadValor.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            lblDisponibilidadValor.ForeColor = Color.FromArgb(23, 61, 43);
            lblDisponibilidadValor.Location = new Point(762, 0);
            lblDisponibilidadValor.Margin = new Padding(0);
            lblDisponibilidadValor.Name = "lblDisponibilidadValor";
            lblDisponibilidadValor.Size = new Size(170, 36);
            lblDisponibilidadValor.TabIndex = 2;
            lblDisponibilidadValor.Text = "-- de -- aves";
            lblDisponibilidadValor.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tlpDetalle
            // 
            tlpDetalle.ColumnCount = 2;
            tlpDetalle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            tlpDetalle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            tlpDetalle.Controls.Add(pnlLotes, 0, 0);
            tlpDetalle.Controls.Add(pnlVentas, 1, 0);
            tlpDetalle.Dock = DockStyle.Fill;
            tlpDetalle.Location = new Point(0, 340);
            tlpDetalle.Margin = new Padding(0);
            tlpDetalle.Name = "tlpDetalle";
            tlpDetalle.RowCount = 1;
            tlpDetalle.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpDetalle.Size = new Size(964, 254);
            tlpDetalle.TabIndex = 3;
            // 
            // pnlLotes
            // 
            pnlLotes.BackColor = Color.White;
            pnlLotes.Controls.Add(tlpLotes);
            pnlLotes.Dock = DockStyle.Fill;
            pnlLotes.Location = new Point(0, 0);
            pnlLotes.Margin = new Padding(0, 0, 8, 0);
            pnlLotes.Name = "pnlLotes";
            pnlLotes.Padding = new Padding(12);
            pnlLotes.Size = new Size(551, 254);
            pnlLotes.TabIndex = 0;
            // 
            // tlpLotes
            // 
            tlpLotes.ColumnCount = 1;
            tlpLotes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpLotes.Controls.Add(pnlEncabezadoLotes, 0, 0);
            tlpLotes.Controls.Add(dgvLotes, 0, 1);
            tlpLotes.Dock = DockStyle.Fill;
            tlpLotes.Location = new Point(12, 12);
            tlpLotes.Name = "tlpLotes";
            tlpLotes.RowCount = 2;
            tlpLotes.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            tlpLotes.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpLotes.Size = new Size(527, 230);
            tlpLotes.TabIndex = 0;
            // 
            // pnlEncabezadoLotes
            // 
            pnlEncabezadoLotes.Controls.Add(lblLotesTitulo);
            pnlEncabezadoLotes.Controls.Add(lblLotesSubtitulo);
            pnlEncabezadoLotes.Dock = DockStyle.Fill;
            pnlEncabezadoLotes.Location = new Point(0, 0);
            pnlEncabezadoLotes.Margin = new Padding(0);
            pnlEncabezadoLotes.Name = "pnlEncabezadoLotes";
            pnlEncabezadoLotes.Size = new Size(527, 46);
            pnlEncabezadoLotes.TabIndex = 0;
            // 
            // lblLotesTitulo
            // 
            lblLotesTitulo.AutoSize = true;
            lblLotesTitulo.Font = new Font("Segoe UI Semibold", 12.5F, FontStyle.Bold);
            lblLotesTitulo.ForeColor = Color.FromArgb(31, 55, 42);
            lblLotesTitulo.Location = new Point(0, 0);
            lblLotesTitulo.Name = "lblLotesTitulo";
            lblLotesTitulo.Size = new Size(137, 30);
            lblLotesTitulo.TabIndex = 0;
            lblLotesTitulo.Text = "Lotes activos";
            // 
            // lblLotesSubtitulo
            // 
            lblLotesSubtitulo.AutoSize = true;
            lblLotesSubtitulo.Font = new Font("Segoe UI", 8.5F);
            lblLotesSubtitulo.ForeColor = Color.FromArgb(110, 124, 115);
            lblLotesSubtitulo.Location = new Point(2, 26);
            lblLotesSubtitulo.Name = "lblLotesSubtitulo";
            lblLotesSubtitulo.Size = new Size(247, 20);
            lblLotesSubtitulo.TabIndex = 1;
            lblLotesSubtitulo.Text = "Producción disponible para la venta";
            // 
            // dgvLotes
            // 
            dgvLotes.AllowUserToAddRows = false;
            dgvLotes.AllowUserToDeleteRows = false;
            dgvLotes.BackgroundColor = Color.White;
            dgvLotes.BorderStyle = BorderStyle.None;
            dgvLotes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLotes.Dock = DockStyle.Fill;
            dgvLotes.Location = new Point(0, 46);
            dgvLotes.Margin = new Padding(0);
            dgvLotes.MultiSelect = false;
            dgvLotes.Name = "dgvLotes";
            dgvLotes.ReadOnly = true;
            dgvLotes.RowHeadersVisible = false;
            dgvLotes.RowHeadersWidth = 51;
            dgvLotes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLotes.Size = new Size(527, 184);
            dgvLotes.TabIndex = 1;
            // 
            // pnlVentas
            // 
            pnlVentas.BackColor = Color.White;
            pnlVentas.Controls.Add(tlpVentas);
            pnlVentas.Dock = DockStyle.Fill;
            pnlVentas.Location = new Point(559, 0);
            pnlVentas.Margin = new Padding(0);
            pnlVentas.Name = "pnlVentas";
            pnlVentas.Padding = new Padding(12);
            pnlVentas.Size = new Size(405, 254);
            pnlVentas.TabIndex = 1;
            // 
            // tlpVentas
            // 
            tlpVentas.ColumnCount = 1;
            tlpVentas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpVentas.Controls.Add(pnlEncabezadoVentas, 0, 0);
            tlpVentas.Controls.Add(dgvVentas, 0, 1);
            tlpVentas.Dock = DockStyle.Fill;
            tlpVentas.Location = new Point(12, 12);
            tlpVentas.Name = "tlpVentas";
            tlpVentas.RowCount = 2;
            tlpVentas.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            tlpVentas.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpVentas.Size = new Size(381, 230);
            tlpVentas.TabIndex = 0;
            // 
            // pnlEncabezadoVentas
            // 
            pnlEncabezadoVentas.Controls.Add(lblVentasTitulo);
            pnlEncabezadoVentas.Controls.Add(lblVentasSubtitulo);
            pnlEncabezadoVentas.Dock = DockStyle.Fill;
            pnlEncabezadoVentas.Location = new Point(0, 0);
            pnlEncabezadoVentas.Margin = new Padding(0);
            pnlEncabezadoVentas.Name = "pnlEncabezadoVentas";
            pnlEncabezadoVentas.Size = new Size(381, 46);
            pnlEncabezadoVentas.TabIndex = 0;
            // 
            // lblVentasTitulo
            // 
            lblVentasTitulo.AutoSize = true;
            lblVentasTitulo.Font = new Font("Segoe UI Semibold", 12.5F, FontStyle.Bold);
            lblVentasTitulo.ForeColor = Color.FromArgb(31, 55, 42);
            lblVentasTitulo.Location = new Point(0, 0);
            lblVentasTitulo.Name = "lblVentasTitulo";
            lblVentasTitulo.Size = new Size(167, 30);
            lblVentasTitulo.TabIndex = 0;
            lblVentasTitulo.Text = "Ventas recientes";
            // 
            // lblVentasSubtitulo
            // 
            lblVentasSubtitulo.AutoSize = true;
            lblVentasSubtitulo.Font = new Font("Segoe UI", 8.5F);
            lblVentasSubtitulo.ForeColor = Color.FromArgb(110, 124, 115);
            lblVentasSubtitulo.Location = new Point(2, 26);
            lblVentasSubtitulo.Name = "lblVentasSubtitulo";
            lblVentasSubtitulo.Size = new Size(221, 20);
            lblVentasSubtitulo.TabIndex = 1;
            lblVentasSubtitulo.Text = "Últimas operaciones registradas";
            // 
            // dgvVentas
            // 
            dgvVentas.AllowUserToAddRows = false;
            dgvVentas.AllowUserToDeleteRows = false;
            dgvVentas.BackgroundColor = Color.White;
            dgvVentas.BorderStyle = BorderStyle.None;
            dgvVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVentas.Dock = DockStyle.Fill;
            dgvVentas.Location = new Point(0, 46);
            dgvVentas.Margin = new Padding(0);
            dgvVentas.MultiSelect = false;
            dgvVentas.Name = "dgvVentas";
            dgvVentas.ReadOnly = true;
            dgvVentas.RowHeadersVisible = false;
            dgvVentas.RowHeadersWidth = 51;
            dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVentas.Size = new Size(381, 184);
            dgvVentas.TabIndex = 1;
            // 
            // FrmDashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 247, 244);
            ClientSize = new Size(1000, 630);
            Controls.Add(pnlFondo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmDashboard";
            Text = "FrmDashboard";
            Load += FrmDashboard_Load;
            Shown += FrmDashboard_Shown;
            pnlFondo.ResumeLayout(false);
            tlpPrincipal.ResumeLayout(false);
            pnlEncabezado.ResumeLayout(false);
            tlpEncabezado.ResumeLayout(false);
            pnlTituloPrincipal.ResumeLayout(false);
            pnlTituloPrincipal.PerformLayout();
            tlpAcciones.ResumeLayout(false);
            pnlDisponibilidad.ResumeLayout(false);
            tlpDisponibilidad.ResumeLayout(false);
            tlpDetalle.ResumeLayout(false);
            pnlLotes.ResumeLayout(false);
            tlpLotes.ResumeLayout(false);
            pnlEncabezadoLotes.ResumeLayout(false);
            pnlEncabezadoLotes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLotes).EndInit();
            pnlVentas.ResumeLayout(false);
            tlpVentas.ResumeLayout(false);
            pnlEncabezadoVentas.ResumeLayout(false);
            pnlEncabezadoVentas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVentas).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlFondo;
        private TableLayoutPanel tlpPrincipal;
        private Panel pnlEncabezado;
        private TableLayoutPanel tlpEncabezado;
        private Panel pnlTituloPrincipal;
        private Label lblTitulo;
        private Label lblSubtitulo;
        private TableLayoutPanel tlpAcciones;
        private Label lblActualizado;
        private Button btnActualizar;
        private TableLayoutPanel tlpTarjetas;
        private Panel pnlDisponibilidad;
        private TableLayoutPanel tlpDisponibilidad;
        private Label lblDisponibilidadTitulo;
        private ProgressBar prgDisponibilidad;
        private Label lblDisponibilidadValor;
        private TableLayoutPanel tlpDetalle;
        private Panel pnlLotes;
        private TableLayoutPanel tlpLotes;
        private Panel pnlEncabezadoLotes;
        private Label lblLotesTitulo;
        private Label lblLotesSubtitulo;
        private DataGridView dgvLotes;
        private Panel pnlVentas;
        private TableLayoutPanel tlpVentas;
        private Panel pnlEncabezadoVentas;
        private Label lblVentasTitulo;
        private Label lblVentasSubtitulo;
        private DataGridView dgvVentas;
    }
}
