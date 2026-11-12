namespace GranjaControl.UI.Formularios.Formularios_derivados.Reportes
{
    partial class FrmReportes
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
            tlpRaiz = new TableLayoutPanel();
            pnlEncabezado = new Panel();
            lblTitulo = new Label();
            lblSubtitulo = new Label();
            tabReportes = new TabControl();
            tabVentas = new TabPage();
            tabProduccion = new TabPage();
            tabCompras = new TabPage();
            pnlFondo.SuspendLayout();
            tlpRaiz.SuspendLayout();
            pnlEncabezado.SuspendLayout();
            tabReportes.SuspendLayout();
            SuspendLayout();
            // 
            // pnlFondo
            // 
            pnlFondo.BackColor = Color.FromArgb(243, 247, 244);
            pnlFondo.Controls.Add(tlpRaiz);
            pnlFondo.Dock = DockStyle.Fill;
            pnlFondo.Location = new Point(0, 0);
            pnlFondo.Name = "pnlFondo";
            pnlFondo.Padding = new Padding(18);
            pnlFondo.Size = new Size(1000, 630);
            pnlFondo.TabIndex = 0;
            // 
            // tlpRaiz
            // 
            tlpRaiz.ColumnCount = 1;
            tlpRaiz.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRaiz.Controls.Add(pnlEncabezado, 0, 0);
            tlpRaiz.Controls.Add(tabReportes, 0, 1);
            tlpRaiz.Dock = DockStyle.Fill;
            tlpRaiz.Location = new Point(18, 18);
            tlpRaiz.Name = "tlpRaiz";
            tlpRaiz.RowCount = 2;
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            tlpRaiz.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRaiz.Size = new Size(964, 594);
            tlpRaiz.TabIndex = 0;
            // 
            // pnlEncabezado
            // 
            pnlEncabezado.Controls.Add(lblTitulo);
            pnlEncabezado.Controls.Add(lblSubtitulo);
            pnlEncabezado.Dock = DockStyle.Fill;
            pnlEncabezado.Location = new Point(0, 0);
            pnlEncabezado.Margin = new Padding(0, 0, 0, 6);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(964, 66);
            pnlEncabezado.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(23, 61, 43);
            lblTitulo.Location = new Point(0, -12);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(167, 47);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Reportes";
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Segoe UI", 10F);
            lblSubtitulo.ForeColor = Color.FromArgb(90, 105, 95);
            lblSubtitulo.Location = new Point(2, 38);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(495, 23);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Consulte el comportamiento operativo y comercial de la granja.";
            // 
            // tabReportes
            // 
            tabReportes.Controls.Add(tabVentas);
            tabReportes.Controls.Add(tabProduccion);
            tabReportes.Controls.Add(tabCompras);
            tabReportes.Dock = DockStyle.Fill;
            tabReportes.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabReportes.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            tabReportes.ItemSize = new Size(210, 36);
            tabReportes.Location = new Point(0, 72);
            tabReportes.Margin = new Padding(0);
            tabReportes.Name = "tabReportes";
            tabReportes.Padding = new Point(12, 6);
            tabReportes.SelectedIndex = 0;
            tabReportes.Size = new Size(964, 522);
            tabReportes.SizeMode = TabSizeMode.Fixed;
            tabReportes.TabIndex = 1;
            tabReportes.DrawItem += tabReportes_DrawItem;
            tabReportes.SelectedIndexChanged += tabReportes_SelectedIndexChanged;
            // 
            // tabVentas
            // 
            tabVentas.BackColor = Color.FromArgb(243, 247, 244);
            tabVentas.Location = new Point(4, 40);
            tabVentas.Name = "tabVentas";
            tabVentas.Padding = new Padding(10);
            tabVentas.Size = new Size(956, 478);
            tabVentas.TabIndex = 0;
            tabVentas.Text = "Ventas por período";
            // 
            // tabProduccion
            // 
            tabProduccion.BackColor = Color.FromArgb(243, 247, 244);
            tabProduccion.Location = new Point(4, 40);
            tabProduccion.Name = "tabProduccion";
            tabProduccion.Padding = new Padding(10);
            tabProduccion.Size = new Size(956, 492);
            tabProduccion.TabIndex = 1;
            tabProduccion.Text = "Producción por lote";
            // 
            // tabCompras
            // 
            tabCompras.BackColor = Color.FromArgb(243, 247, 244);
            tabCompras.Location = new Point(4, 40);
            tabCompras.Name = "tabCompras";
            tabCompras.Padding = new Padding(10);
            tabCompras.Size = new Size(956, 492);
            tabCompras.TabIndex = 2;
            tabCompras.Text = "Compras de alimento";
            // 
            // FrmReportes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 247, 244);
            ClientSize = new Size(1000, 630);
            Controls.Add(pnlFondo);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmReportes";
            Text = "FrmReportes";
            Load += FrmReportes_Load;
            Shown += FrmReportes_Shown;
            pnlFondo.ResumeLayout(false);
            tlpRaiz.ResumeLayout(false);
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            tabReportes.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlFondo;
        private TableLayoutPanel tlpRaiz;
        private Panel pnlEncabezado;
        private Label lblTitulo;
        private Label lblSubtitulo;
        private TabControl tabReportes;
        private TabPage tabVentas;
        private TabPage tabProduccion;
        private TabPage tabCompras;
    }
}
