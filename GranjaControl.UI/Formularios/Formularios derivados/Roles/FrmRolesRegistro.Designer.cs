namespace GranjaControl.UI.Formularios.Formularios_derivados.Roles
{
    partial class FrmRolesRegistro
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
            panel1 = new Panel();
            pnlContenido = new TableLayoutPanel();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            btnGuardar = new FontAwesome.Sharp.IconButton();
            label4 = new Label();
            pnlNombre = new Panel();
            txtRol = new TextBox();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            btnIconCerrar = new Button();
            lblDescripcion = new Label();
            lblTitulo = new Label();
            iconIcono = new FontAwesome.Sharp.IconButton();
            errorIcon = new ErrorProvider(components);
            panel1.SuspendLayout();
            pnlContenido.SuspendLayout();
            pnlNombre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorIcon).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(pnlContenido);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(pnlNombre);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(472, 208);
            panel1.TabIndex = 0;
            // 
            // pnlContenido
            // 
            pnlContenido.BackColor = Color.White;
            pnlContenido.ColumnCount = 4;
            pnlContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 123F));
            pnlContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 123F));
            pnlContenido.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            pnlContenido.Controls.Add(btnCancelar, 2, 0);
            pnlContenido.Controls.Add(btnGuardar, 1, 0);
            pnlContenido.Dock = DockStyle.Bottom;
            pnlContenido.Location = new Point(0, 152);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.RowCount = 1;
            pnlContenido.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlContenido.Size = new Size(472, 56);
            pnlContenido.TabIndex = 10;
            // 
            // btnCancelar
            // 
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Cancel;
            btnCancelar.IconColor = Color.Red;
            btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelar.IconSize = 28;
            btnCancelar.Location = new Point(332, 3);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(117, 50);
            btnCancelar.TabIndex = 2;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar.IconChar = FontAwesome.Sharp.IconChar.Save;
            btnGuardar.IconColor = Color.Blue;
            btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnGuardar.IconSize = 28;
            btnGuardar.Location = new Point(209, 3);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(117, 50);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "Guardar";
            btnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Lavender;
            label4.Enabled = false;
            label4.FlatStyle = FlatStyle.Flat;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(16, 89);
            label4.Name = "label4";
            label4.Size = new Size(138, 23);
            label4.TabIndex = 9;
            label4.Text = "Nombre del rol:";
            // 
            // pnlNombre
            // 
            pnlNombre.BackColor = Color.White;
            pnlNombre.Controls.Add(txtRol);
            pnlNombre.Location = new Point(157, 89);
            pnlNombre.Name = "pnlNombre";
            pnlNombre.Size = new Size(303, 41);
            pnlNombre.TabIndex = 8;
            // 
            // txtRol
            // 
            txtRol.BackColor = Color.White;
            txtRol.BorderStyle = BorderStyle.None;
            txtRol.Location = new Point(3, 11);
            txtRol.Name = "txtRol";
            txtRol.Size = new Size(265, 20);
            txtRol.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Lavender;
            pictureBox1.Location = new Point(0, 60);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(472, 88);
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnIconCerrar);
            panel2.Controls.Add(lblDescripcion);
            panel2.Controls.Add(lblTitulo);
            panel2.Controls.Add(iconIcono);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(472, 64);
            panel2.TabIndex = 1;
            // 
            // btnIconCerrar
            // 
            btnIconCerrar.FlatAppearance.BorderSize = 0;
            btnIconCerrar.FlatStyle = FlatStyle.Flat;
            btnIconCerrar.Image = Properties.Resources._02_cerrar1;
            btnIconCerrar.Location = new Point(428, 9);
            btnIconCerrar.Name = "btnIconCerrar";
            btnIconCerrar.Size = new Size(32, 32);
            btnIconCerrar.TabIndex = 3;
            btnIconCerrar.UseVisualStyleBackColor = true;
            btnIconCerrar.Click += btnIconCerrar_Click;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDescripcion.ForeColor = SystemColors.ControlDarkDark;
            lblDescripcion.Location = new Point(68, 37);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(263, 20);
            lblDescripcion.TabIndex = 2;
            lblDescripcion.Text = "Complete la información del registro ";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(68, 6);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(174, 31);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Nuevo registro";
            // 
            // iconIcono
            // 
            iconIcono.IconChar = FontAwesome.Sharp.IconChar.Vcard;
            iconIcono.IconColor = Color.Blue;
            iconIcono.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconIcono.IconSize = 50;
            iconIcono.Location = new Point(12, 7);
            iconIcono.Name = "iconIcono";
            iconIcono.Size = new Size(50, 50);
            iconIcono.TabIndex = 0;
            iconIcono.UseVisualStyleBackColor = true;
            // 
            // errorIcon
            // 
            errorIcon.ContainerControl = this;
            // 
            // FrmRolesRegistro
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(472, 208);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmRolesRegistro";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmRolesRegistro";
            Load += FrmRolesRegistro_Load;
            Shown += FrmRolesRegistro_Shown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            pnlContenido.ResumeLayout(false);
            pnlNombre.ResumeLayout(false);
            pnlNombre.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorIcon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TableLayoutPanel pnlContenido;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private Label label4;
        private Panel pnlNombre;
        private TextBox txtRol;
        private PictureBox pictureBox1;
        private Panel panel2;
        private Button btnIconCerrar;
        private Label lblDescripcion;
        private Label lblTitulo;
        private FontAwesome.Sharp.IconButton iconIcono;
        private ErrorProvider errorIcon;
    }
}