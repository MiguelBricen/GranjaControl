namespace GranjaControl.UI
{
    partial class FrmGalponRegistro
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGalponRegistro));
            pnlSuperior = new Panel();
            lblDescripcion = new Label();
            btnCerrar = new Button();
            lblTitulo = new Label();
            btnIcon = new Button();
            pnlContenido = new Panel();
            pnlDescripcion = new Panel();
            txtDescripcion = new TextBox();
            pnlCapacidad = new Panel();
            txtCapacidad = new TextBox();
            pnlNombre = new Panel();
            txtNombre = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            errorIcon = new ErrorProvider(components);
            btnGuardar = new Button();
            btnCancelar = new Button();
            panel1 = new Panel();
            pnlSuperior.SuspendLayout();
            pnlContenido.SuspendLayout();
            pnlDescripcion.SuspendLayout();
            pnlCapacidad.SuspendLayout();
            pnlNombre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorIcon).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSuperior
            // 
            pnlSuperior.BackColor = Color.GhostWhite;
            pnlSuperior.Controls.Add(lblDescripcion);
            pnlSuperior.Controls.Add(btnCerrar);
            pnlSuperior.Controls.Add(lblTitulo);
            pnlSuperior.Controls.Add(btnIcon);
            pnlSuperior.Dock = DockStyle.Top;
            pnlSuperior.Location = new Point(0, 0);
            pnlSuperior.Name = "pnlSuperior";
            pnlSuperior.Size = new Size(468, 64);
            pnlSuperior.TabIndex = 0;
            pnlSuperior.MouseDown += pnlSuperior_MouseDown;
            pnlSuperior.MouseMove += pnlSuperior_MouseMove;
            pnlSuperior.MouseUp += pnlSuperior_MouseUp;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDescripcion.ForeColor = SystemColors.ControlDarkDark;
            lblDescripcion.Location = new Point(86, 38);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(290, 23);
            lblDescripcion.TabIndex = 3;
            lblDescripcion.Text = "Complete la informacion del registro";
            // 
            // btnCerrar
            // 
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Image = Properties.Resources._02_cerrar1;
            btnCerrar.Location = new Point(424, 12);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(32, 32);
            btnCerrar.TabIndex = 2;
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.Black;
            lblTitulo.Location = new Point(87, 6);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(219, 31);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "AGREGAR GALPON";
            // 
            // btnIcon
            // 
            btnIcon.Cursor = Cursors.Hand;
            btnIcon.FlatAppearance.BorderSize = 0;
            btnIcon.FlatStyle = FlatStyle.Flat;
            btnIcon.Image = Properties.Resources._17_configuracion;
            btnIcon.Location = new Point(12, 0);
            btnIcon.Name = "btnIcon";
            btnIcon.Size = new Size(69, 79);
            btnIcon.TabIndex = 1;
            btnIcon.TextImageRelation = TextImageRelation.ImageAboveText;
            btnIcon.UseVisualStyleBackColor = true;
            // 
            // pnlContenido
            // 
            pnlContenido.BackColor = Color.FromArgb(226, 239, 228);
            pnlContenido.Controls.Add(pnlDescripcion);
            pnlContenido.Controls.Add(pnlCapacidad);
            pnlContenido.Controls.Add(pnlNombre);
            pnlContenido.Controls.Add(label4);
            pnlContenido.Controls.Add(label3);
            pnlContenido.Controls.Add(label2);
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(0, 64);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Size = new Size(468, 259);
            pnlContenido.TabIndex = 1;
            // 
            // pnlDescripcion
            // 
            pnlDescripcion.BackColor = Color.White;
            pnlDescripcion.Controls.Add(txtDescripcion);
            pnlDescripcion.Location = new Point(135, 142);
            pnlDescripcion.Name = "pnlDescripcion";
            pnlDescripcion.Size = new Size(298, 41);
            pnlDescripcion.TabIndex = 8;
            // 
            // txtDescripcion
            // 
            txtDescripcion.BackColor = Color.White;
            txtDescripcion.BorderStyle = BorderStyle.None;
            txtDescripcion.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtDescripcion.ForeColor = Color.FromArgb(42, 52, 45);
            txtDescripcion.Location = new Point(3, 10);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(257, 24);
            txtDescripcion.TabIndex = 5;
            // 
            // pnlCapacidad
            // 
            pnlCapacidad.BackColor = Color.White;
            pnlCapacidad.Controls.Add(txtCapacidad);
            pnlCapacidad.Location = new Point(135, 80);
            pnlCapacidad.Name = "pnlCapacidad";
            pnlCapacidad.Size = new Size(298, 41);
            pnlCapacidad.TabIndex = 7;
            // 
            // txtCapacidad
            // 
            txtCapacidad.BackColor = Color.White;
            txtCapacidad.BorderStyle = BorderStyle.None;
            txtCapacidad.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtCapacidad.ForeColor = Color.FromArgb(42, 52, 45);
            txtCapacidad.Location = new Point(3, 7);
            txtCapacidad.Name = "txtCapacidad";
            txtCapacidad.Size = new Size(257, 24);
            txtCapacidad.TabIndex = 4;
            // 
            // pnlNombre
            // 
            pnlNombre.BackColor = Color.White;
            pnlNombre.Controls.Add(txtNombre);
            pnlNombre.Location = new Point(116, 18);
            pnlNombre.Name = "pnlNombre";
            pnlNombre.Size = new Size(317, 41);
            pnlNombre.TabIndex = 6;
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.White;
            txtNombre.BorderStyle = BorderStyle.None;
            txtNombre.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtNombre.ForeColor = Color.FromArgb(42, 52, 45);
            txtNombre.Location = new Point(3, 7);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(276, 24);
            txtNombre.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(42, 52, 45);
            label4.Location = new Point(24, 80);
            label4.Name = "label4";
            label4.Size = new Size(105, 25);
            label4.TabIndex = 2;
            label4.Text = "Capacidad:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(42, 52, 45);
            label3.Location = new Point(24, 142);
            label3.Name = "label3";
            label3.Size = new Size(116, 25);
            label3.TabIndex = 1;
            label3.Text = "Descripción:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(42, 52, 45);
            label2.Location = new Point(24, 18);
            label2.Name = "label2";
            label2.Size = new Size(86, 25);
            label2.TabIndex = 0;
            label2.Text = "Nombre:";
            // 
            // errorIcon
            // 
            errorIcon.ContainerControl = this;
            errorIcon.Icon = (Icon)resources.GetObject("errorIcon.Icon");
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.Green;
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Image = Properties.Resources._03_guardar;
            btnGuardar.Location = new Point(86, 0);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Padding = new Padding(10, 5, 5, 10);
            btnGuardar.Size = new Size(149, 43);
            btnGuardar.TabIndex = 4;
            btnGuardar.Text = "Guardar";
            btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(192, 0, 0);
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Image = Properties.Resources._04_cancelar;
            btnCancelar.Location = new Point(246, 0);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Padding = new Padding(10, 5, 5, 10);
            btnCancelar.Size = new Size(149, 43);
            btnCancelar.TabIndex = 6;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(226, 239, 228);
            panel1.Controls.Add(btnCancelar);
            panel1.Controls.Add(btnGuardar);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 268);
            panel1.Name = "panel1";
            panel1.Size = new Size(468, 55);
            panel1.TabIndex = 2;
            // 
            // FrmGalponRegistro
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(226, 239, 228);
            ClientSize = new Size(468, 323);
            Controls.Add(panel1);
            Controls.Add(pnlContenido);
            Controls.Add(pnlSuperior);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmGalponRegistro";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmGalpon";
            Load += FrmGalpon_Load;
            Shown += FrmGalpon_Shown;
            pnlSuperior.ResumeLayout(false);
            pnlSuperior.PerformLayout();
            pnlContenido.ResumeLayout(false);
            pnlContenido.PerformLayout();
            pnlDescripcion.ResumeLayout(false);
            pnlDescripcion.PerformLayout();
            pnlCapacidad.ResumeLayout(false);
            pnlCapacidad.PerformLayout();
            pnlNombre.ResumeLayout(false);
            pnlNombre.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorIcon).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSuperior;
        private Button btnCerrar;
        private Button btnMinimizar;
        private Label lblTitulo;
        private Panel pnlContenido;
        private Label label4;
        private Label label3;
        private Label label2;
        private TextBox txtNombre;
        private TextBox txtDescripcion;
        private TextBox txtCapacidad;
        private ErrorProvider errorIcon;
      
        private Button btnIcon;
        private Label lblDescripcion;
        private Panel pnlNombre;
        private Panel pnlDescripcion;
        private Panel pnlCapacidad;
        private Panel panel1;
        private Button btnGuardar;
        private Button btnCancelar;
    }
}