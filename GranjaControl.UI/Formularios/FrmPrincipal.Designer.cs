namespace GranjaControl.UI.Formularios
{
    partial class FrmPrincipal
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
            pnlMovimiento = new Panel();
            btnMinimizar = new Button();
            btnCerrar = new Button();
            pnlSeguridad = new Panel();
            btnRoles = new FontAwesome.Sharp.IconButton();
            btnUsuarios = new FontAwesome.Sharp.IconButton();
            pnlSidebar = new Panel();
            picDif7 = new PictureBox();
            picDif6 = new PictureBox();
            picDif5 = new PictureBox();
            picDif4 = new PictureBox();
            picDif3 = new PictureBox();
            picDif2 = new PictureBox();
            picDif = new PictureBox();
            btnInicio = new FontAwesome.Sharp.IconButton();
            btnReportes = new FontAwesome.Sharp.IconButton();
            btnComercializacion = new FontAwesome.Sharp.IconButton();
            btnProduccion = new FontAwesome.Sharp.IconButton();
            btnCompras = new FontAwesome.Sharp.IconButton();
            btnSeguridad = new FontAwesome.Sharp.IconButton();
            btnCatalogos = new FontAwesome.Sharp.IconButton();
            panel2 = new Panel();
            label2 = new Label();
            label1 = new Label();
            btnIcon = new Button();
            panel1 = new Panel();
            btnCerrarSesion = new FontAwesome.Sharp.IconButton();
            statusStrip1 = new StatusStrip();
            toolUsuario = new ToolStripStatusLabel();
            toolRol = new ToolStripStatusLabel();
            pnlContenedor = new Panel();
            pnlMovimiento.SuspendLayout();
            pnlSeguridad.SuspendLayout();
            pnlSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picDif7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picDif6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picDif5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picDif4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picDif3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picDif2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picDif).BeginInit();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMovimiento
            // 
            pnlMovimiento.BackColor = Color.FromArgb(23, 61, 43);
            pnlMovimiento.Controls.Add(btnMinimizar);
            pnlMovimiento.Controls.Add(btnCerrar);
            pnlMovimiento.Dock = DockStyle.Top;
            pnlMovimiento.Location = new Point(0, 0);
            pnlMovimiento.Name = "pnlMovimiento";
            pnlMovimiento.Size = new Size(1280, 44);
            pnlMovimiento.TabIndex = 0;
            // 
            // btnMinimizar
            // 
            btnMinimizar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMinimizar.BackColor = Color.Transparent;
            btnMinimizar.Cursor = Cursors.Hand;
            btnMinimizar.FlatAppearance.BorderSize = 0;
            btnMinimizar.FlatStyle = FlatStyle.Flat;
            btnMinimizar.Image = Properties.Resources._01_minimizar;
            btnMinimizar.Location = new Point(1180, 4);
            btnMinimizar.Name = "btnMinimizar";
            btnMinimizar.Size = new Size(40, 40);
            btnMinimizar.TabIndex = 3;
            btnMinimizar.UseVisualStyleBackColor = false;
            btnMinimizar.Click += btnMinimizar_Click;
            // 
            // btnCerrar
            // 
            btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCerrar.BackColor = Color.Transparent;
            btnCerrar.Cursor = Cursors.Hand;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.Image = Properties.Resources._30_salir;
            btnCerrar.Location = new Point(1228, 4);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(40, 40);
            btnCerrar.TabIndex = 2;
            btnCerrar.UseVisualStyleBackColor = false;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // pnlSeguridad
            // 
            pnlSeguridad.Controls.Add(btnRoles);
            pnlSeguridad.Controls.Add(btnUsuarios);
            pnlSeguridad.Location = new Point(0, 278);
            pnlSeguridad.Name = "pnlSeguridad";
            pnlSeguridad.Size = new Size(226, 106);
            pnlSeguridad.TabIndex = 4;
            pnlSeguridad.Visible = false;
            // 
            // btnRoles
            // 
            btnRoles.BackColor = Color.FromArgb(23, 61, 43);
            btnRoles.Cursor = Cursors.Hand;
            btnRoles.FlatAppearance.BorderColor = Color.FromArgb(220, 225, 216);
            btnRoles.FlatAppearance.BorderSize = 0;
            btnRoles.FlatAppearance.MouseOverBackColor = Color.FromArgb(62, 94, 69);
            btnRoles.FlatStyle = FlatStyle.Flat;
            btnRoles.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnRoles.ForeColor = Color.White;
            btnRoles.IconChar = FontAwesome.Sharp.IconChar.ShieldBlank;
            btnRoles.IconColor = Color.White;
            btnRoles.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRoles.IconSize = 30;
            btnRoles.ImageAlign = ContentAlignment.MiddleLeft;
            btnRoles.Location = new Point(2, 53);
            btnRoles.Name = "btnRoles";
            btnRoles.Padding = new Padding(5, 0, 0, 0);
            btnRoles.Size = new Size(225, 50);
            btnRoles.TabIndex = 18;
            btnRoles.Text = "Roles";
            btnRoles.TextAlign = ContentAlignment.MiddleRight;
            btnRoles.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRoles.UseVisualStyleBackColor = false;
            btnRoles.Click += btnRoles_Click;
            // 
            // btnUsuarios
            // 
            btnUsuarios.BackColor = Color.FromArgb(23, 61, 43);
            btnUsuarios.Cursor = Cursors.Hand;
            btnUsuarios.FlatAppearance.BorderColor = Color.FromArgb(220, 225, 216);
            btnUsuarios.FlatAppearance.BorderSize = 0;
            btnUsuarios.FlatAppearance.MouseOverBackColor = Color.FromArgb(62, 94, 69);
            btnUsuarios.FlatStyle = FlatStyle.Flat;
            btnUsuarios.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnUsuarios.ForeColor = Color.White;
            btnUsuarios.IconChar = FontAwesome.Sharp.IconChar.Users;
            btnUsuarios.IconColor = Color.White;
            btnUsuarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnUsuarios.IconSize = 30;
            btnUsuarios.ImageAlign = ContentAlignment.MiddleLeft;
            btnUsuarios.Location = new Point(2, 3);
            btnUsuarios.Name = "btnUsuarios";
            btnUsuarios.Padding = new Padding(5, 0, 0, 0);
            btnUsuarios.Size = new Size(225, 50);
            btnUsuarios.TabIndex = 17;
            btnUsuarios.Text = "Usuarios";
            btnUsuarios.TextAlign = ContentAlignment.MiddleRight;
            btnUsuarios.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnUsuarios.UseVisualStyleBackColor = false;
            btnUsuarios.Click += btnUsuarios_Click;
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(23, 61, 43);
            pnlSidebar.Controls.Add(pnlSeguridad);
            pnlSidebar.Controls.Add(picDif7);
            pnlSidebar.Controls.Add(picDif6);
            pnlSidebar.Controls.Add(picDif5);
            pnlSidebar.Controls.Add(picDif4);
            pnlSidebar.Controls.Add(picDif3);
            pnlSidebar.Controls.Add(picDif2);
            pnlSidebar.Controls.Add(picDif);
            pnlSidebar.Controls.Add(btnInicio);
            pnlSidebar.Controls.Add(btnReportes);
            pnlSidebar.Controls.Add(btnComercializacion);
            pnlSidebar.Controls.Add(btnProduccion);
            pnlSidebar.Controls.Add(btnCompras);
            pnlSidebar.Controls.Add(btnSeguridad);
            pnlSidebar.Controls.Add(btnCatalogos);
            pnlSidebar.Controls.Add(panel2);
            pnlSidebar.Controls.Add(panel1);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Font = new Font("Microsoft Sans Serif", 8.25F);
            pnlSidebar.Location = new Point(0, 44);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(226, 676);
            pnlSidebar.TabIndex = 0;
            // 
            // picDif7
            // 
            picDif7.BackColor = Color.FromArgb(226, 239, 228);
            picDif7.Location = new Point(1, 502);
            picDif7.Name = "picDif7";
            picDif7.Size = new Size(1, 50);
            picDif7.TabIndex = 23;
            picDif7.TabStop = false;
            // 
            // picDif6
            // 
            picDif6.BackColor = Color.FromArgb(226, 239, 228);
            picDif6.Location = new Point(1, 446);
            picDif6.Name = "picDif6";
            picDif6.Size = new Size(1, 50);
            picDif6.TabIndex = 22;
            picDif6.TabStop = false;
            // 
            // picDif5
            // 
            picDif5.BackColor = Color.FromArgb(226, 239, 228);
            picDif5.Location = new Point(1, 390);
            picDif5.Name = "picDif5";
            picDif5.Size = new Size(1, 50);
            picDif5.TabIndex = 21;
            picDif5.TabStop = false;
            // 
            // picDif4
            // 
            picDif4.BackColor = Color.FromArgb(226, 239, 228);
            picDif4.Location = new Point(1, 334);
            picDif4.Name = "picDif4";
            picDif4.Size = new Size(1, 50);
            picDif4.TabIndex = 20;
            picDif4.TabStop = false;
            // 
            // picDif3
            // 
            picDif3.BackColor = Color.FromArgb(226, 239, 228);
            picDif3.Location = new Point(1, 278);
            picDif3.Name = "picDif3";
            picDif3.Size = new Size(1, 50);
            picDif3.TabIndex = 19;
            picDif3.TabStop = false;
            // 
            // picDif2
            // 
            picDif2.BackColor = Color.FromArgb(226, 239, 228);
            picDif2.Location = new Point(1, 222);
            picDif2.Name = "picDif2";
            picDif2.Size = new Size(1, 50);
            picDif2.TabIndex = 18;
            picDif2.TabStop = false;
            // 
            // picDif
            // 
            picDif.BackColor = Color.FromArgb(226, 239, 228);
            picDif.Location = new Point(1, 166);
            picDif.Name = "picDif";
            picDif.Size = new Size(1, 50);
            picDif.TabIndex = 17;
            picDif.TabStop = false;
            // 
            // btnInicio
            // 
            btnInicio.BackColor = Color.FromArgb(23, 61, 43);
            btnInicio.Cursor = Cursors.Hand;
            btnInicio.FlatAppearance.BorderColor = Color.FromArgb(220, 225, 216);
            btnInicio.FlatAppearance.BorderSize = 0;
            btnInicio.FlatAppearance.MouseOverBackColor = Color.FromArgb(62, 94, 69);
            btnInicio.FlatStyle = FlatStyle.Flat;
            btnInicio.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnInicio.ForeColor = Color.White;
            btnInicio.IconChar = FontAwesome.Sharp.IconChar.TachometerAltFast;
            btnInicio.IconColor = Color.White;
            btnInicio.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnInicio.IconSize = 30;
            btnInicio.ImageAlign = ContentAlignment.MiddleLeft;
            btnInicio.Location = new Point(2, 166);
            btnInicio.Name = "btnInicio";
            btnInicio.Padding = new Padding(5, 0, 0, 0);
            btnInicio.Size = new Size(224, 50);
            btnInicio.TabIndex = 16;
            btnInicio.Text = "Inicio";
            btnInicio.TextAlign = ContentAlignment.MiddleRight;
            btnInicio.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnInicio.UseVisualStyleBackColor = false;
            // 
            // btnReportes
            // 
            btnReportes.BackColor = Color.FromArgb(23, 61, 43);
            btnReportes.Cursor = Cursors.Hand;
            btnReportes.FlatAppearance.BorderColor = Color.FromArgb(220, 225, 216);
            btnReportes.FlatAppearance.BorderSize = 0;
            btnReportes.FlatAppearance.MouseOverBackColor = Color.FromArgb(62, 94, 69);
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnReportes.ForeColor = Color.White;
            btnReportes.IconChar = FontAwesome.Sharp.IconChar.ChartLine;
            btnReportes.IconColor = Color.White;
            btnReportes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnReportes.IconSize = 30;
            btnReportes.ImageAlign = ContentAlignment.MiddleLeft;
            btnReportes.Location = new Point(2, 502);
            btnReportes.Name = "btnReportes";
            btnReportes.Padding = new Padding(5, 0, 0, 0);
            btnReportes.Size = new Size(225, 50);
            btnReportes.TabIndex = 15;
            btnReportes.Text = "Reportes";
            btnReportes.TextAlign = ContentAlignment.MiddleRight;
            btnReportes.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnReportes.UseVisualStyleBackColor = false;
            // 
            // btnComercializacion
            // 
            btnComercializacion.BackColor = Color.FromArgb(23, 61, 43);
            btnComercializacion.Cursor = Cursors.Hand;
            btnComercializacion.FlatAppearance.BorderColor = Color.FromArgb(220, 225, 216);
            btnComercializacion.FlatAppearance.BorderSize = 0;
            btnComercializacion.FlatAppearance.MouseOverBackColor = Color.FromArgb(62, 94, 69);
            btnComercializacion.FlatStyle = FlatStyle.Flat;
            btnComercializacion.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnComercializacion.ForeColor = Color.White;
            btnComercializacion.IconChar = FontAwesome.Sharp.IconChar.SackDollar;
            btnComercializacion.IconColor = Color.White;
            btnComercializacion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnComercializacion.IconSize = 30;
            btnComercializacion.ImageAlign = ContentAlignment.MiddleLeft;
            btnComercializacion.Location = new Point(2, 446);
            btnComercializacion.Name = "btnComercializacion";
            btnComercializacion.Padding = new Padding(5, 0, 0, 0);
            btnComercializacion.Size = new Size(225, 50);
            btnComercializacion.TabIndex = 14;
            btnComercializacion.Text = "Comercialización";
            btnComercializacion.TextAlign = ContentAlignment.MiddleRight;
            btnComercializacion.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnComercializacion.UseVisualStyleBackColor = false;
            // 
            // btnProduccion
            // 
            btnProduccion.BackColor = Color.FromArgb(23, 61, 43);
            btnProduccion.Cursor = Cursors.Hand;
            btnProduccion.FlatAppearance.BorderColor = Color.FromArgb(220, 225, 216);
            btnProduccion.FlatAppearance.BorderSize = 0;
            btnProduccion.FlatAppearance.MouseOverBackColor = Color.FromArgb(62, 94, 69);
            btnProduccion.FlatStyle = FlatStyle.Flat;
            btnProduccion.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnProduccion.ForeColor = Color.White;
            btnProduccion.IconChar = FontAwesome.Sharp.IconChar.WheatAwn;
            btnProduccion.IconColor = Color.White;
            btnProduccion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnProduccion.IconSize = 30;
            btnProduccion.ImageAlign = ContentAlignment.MiddleLeft;
            btnProduccion.Location = new Point(2, 334);
            btnProduccion.Name = "btnProduccion";
            btnProduccion.Padding = new Padding(5, 0, 0, 0);
            btnProduccion.Size = new Size(225, 50);
            btnProduccion.TabIndex = 13;
            btnProduccion.Text = "Producción";
            btnProduccion.TextAlign = ContentAlignment.MiddleRight;
            btnProduccion.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnProduccion.UseVisualStyleBackColor = false;
            // 
            // btnCompras
            // 
            btnCompras.BackColor = Color.FromArgb(23, 61, 43);
            btnCompras.Cursor = Cursors.Hand;
            btnCompras.FlatAppearance.BorderColor = Color.FromArgb(220, 225, 216);
            btnCompras.FlatAppearance.BorderSize = 0;
            btnCompras.FlatAppearance.MouseOverBackColor = Color.FromArgb(62, 94, 69);
            btnCompras.FlatStyle = FlatStyle.Flat;
            btnCompras.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnCompras.ForeColor = Color.White;
            btnCompras.IconChar = FontAwesome.Sharp.IconChar.CartShopping;
            btnCompras.IconColor = Color.White;
            btnCompras.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCompras.IconSize = 30;
            btnCompras.ImageAlign = ContentAlignment.MiddleLeft;
            btnCompras.Location = new Point(2, 390);
            btnCompras.Name = "btnCompras";
            btnCompras.Padding = new Padding(5, 0, 0, 0);
            btnCompras.Size = new Size(225, 50);
            btnCompras.TabIndex = 12;
            btnCompras.Text = "Compras";
            btnCompras.TextAlign = ContentAlignment.MiddleRight;
            btnCompras.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCompras.UseVisualStyleBackColor = false;
            // 
            // btnSeguridad
            // 
            btnSeguridad.BackColor = Color.FromArgb(23, 61, 43);
            btnSeguridad.Cursor = Cursors.Hand;
            btnSeguridad.FlatAppearance.BorderColor = Color.FromArgb(220, 225, 216);
            btnSeguridad.FlatAppearance.BorderSize = 0;
            btnSeguridad.FlatAppearance.MouseOverBackColor = Color.FromArgb(62, 94, 69);
            btnSeguridad.FlatStyle = FlatStyle.Flat;
            btnSeguridad.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnSeguridad.ForeColor = Color.White;
            btnSeguridad.IconChar = FontAwesome.Sharp.IconChar.ShieldHalved;
            btnSeguridad.IconColor = Color.White;
            btnSeguridad.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSeguridad.IconSize = 30;
            btnSeguridad.ImageAlign = ContentAlignment.MiddleLeft;
            btnSeguridad.Location = new Point(2, 222);
            btnSeguridad.Name = "btnSeguridad";
            btnSeguridad.Padding = new Padding(5, 0, 0, 0);
            btnSeguridad.Size = new Size(225, 50);
            btnSeguridad.TabIndex = 11;
            btnSeguridad.Text = "Seguridad";
            btnSeguridad.TextAlign = ContentAlignment.MiddleRight;
            btnSeguridad.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSeguridad.UseVisualStyleBackColor = false;
            btnSeguridad.Click += btnSeguridad_Click;
            // 
            // btnCatalogos
            // 
            btnCatalogos.BackColor = Color.FromArgb(23, 61, 43);
            btnCatalogos.Cursor = Cursors.Hand;
            btnCatalogos.FlatAppearance.BorderColor = Color.FromArgb(220, 225, 216);
            btnCatalogos.FlatAppearance.BorderSize = 0;
            btnCatalogos.FlatAppearance.MouseOverBackColor = Color.FromArgb(62, 94, 69);
            btnCatalogos.FlatStyle = FlatStyle.Flat;
            btnCatalogos.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold);
            btnCatalogos.ForeColor = Color.White;
            btnCatalogos.IconChar = FontAwesome.Sharp.IconChar.LayerGroup;
            btnCatalogos.IconColor = Color.White;
            btnCatalogos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCatalogos.IconSize = 30;
            btnCatalogos.ImageAlign = ContentAlignment.MiddleLeft;
            btnCatalogos.Location = new Point(2, 278);
            btnCatalogos.Name = "btnCatalogos";
            btnCatalogos.Padding = new Padding(5, 0, 0, 0);
            btnCatalogos.Size = new Size(225, 50);
            btnCatalogos.TabIndex = 10;
            btnCatalogos.Text = "Catálogos";
            btnCatalogos.TextAlign = ContentAlignment.MiddleRight;
            btnCatalogos.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCatalogos.UseVisualStyleBackColor = false;
            btnCatalogos.Click += btnCatalogos_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(23, 61, 43);
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(btnIcon);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(226, 163);
            panel2.TabIndex = 9;
            // 
            // label2
            // 
            label2.BackColor = Color.FromArgb(23, 61, 43);
            label2.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(12, 124);
            label2.Name = "label2";
            label2.Size = new Size(199, 28);
            label2.TabIndex = 3;
            label2.Text = "Sistema de soporte";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.BackColor = Color.FromArgb(23, 61, 43);
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(12, 83);
            label1.Name = "label1";
            label1.Size = new Size(199, 51);
            label1.TabIndex = 2;
            label1.Text = "Avícola Briceño";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnIcon
            // 
            btnIcon.Cursor = Cursors.Hand;
            btnIcon.FlatAppearance.BorderSize = 0;
            btnIcon.FlatStyle = FlatStyle.Flat;
            btnIcon.Image = Properties.Resources._19_galpon;
            btnIcon.Location = new Point(75, 16);
            btnIcon.Name = "btnIcon";
            btnIcon.Size = new Size(64, 64);
            btnIcon.TabIndex = 0;
            btnIcon.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(btnCerrarSesion);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 592);
            panel1.Name = "panel1";
            panel1.Size = new Size(226, 84);
            panel1.TabIndex = 8;
            // 
            // btnCerrarSesion
            // 
            btnCerrarSesion.Cursor = Cursors.Hand;
            btnCerrarSesion.FlatAppearance.BorderColor = Color.FromArgb(220, 225, 216);
            btnCerrarSesion.FlatAppearance.BorderSize = 0;
            btnCerrarSesion.FlatAppearance.MouseOverBackColor = Color.FromArgb(62, 94, 69);
            btnCerrarSesion.FlatStyle = FlatStyle.Flat;
            btnCerrarSesion.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCerrarSesion.ForeColor = Color.White;
            btnCerrarSesion.IconChar = FontAwesome.Sharp.IconChar.SignOutAlt;
            btnCerrarSesion.IconColor = Color.White;
            btnCerrarSesion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCerrarSesion.IconSize = 30;
            btnCerrarSesion.ImageAlign = ContentAlignment.MiddleLeft;
            btnCerrarSesion.Location = new Point(4, 17);
            btnCerrarSesion.Name = "btnCerrarSesion";
            btnCerrarSesion.Padding = new Padding(5, 0, 0, 0);
            btnCerrarSesion.Size = new Size(218, 50);
            btnCerrarSesion.TabIndex = 8;
            btnCerrarSesion.Text = "Cerrar Sesión";
            btnCerrarSesion.TextAlign = ContentAlignment.MiddleRight;
            btnCerrarSesion.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCerrarSesion.UseVisualStyleBackColor = true;
            btnCerrarSesion.Click += btnCerrarSesion_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.AutoSize = false;
            statusStrip1.BackColor = Color.FromArgb(38, 85, 52);
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolUsuario, toolRol });
            statusStrip1.Location = new Point(226, 680);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1054, 40);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolUsuario
            // 
            toolUsuario.AutoSize = false;
            toolUsuario.BorderSides = ToolStripStatusLabelBorderSides.Left;
            toolUsuario.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            toolUsuario.ForeColor = Color.White;
            toolUsuario.Name = "toolUsuario";
            toolUsuario.Padding = new Padding(8, 6, 8, 6);
            toolUsuario.Size = new Size(154, 34);
            toolUsuario.Text = "Usuario:";
            // 
            // toolRol
            // 
            toolRol.AutoSize = false;
            toolRol.BorderSides = ToolStripStatusLabelBorderSides.Left;
            toolRol.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            toolRol.ForeColor = Color.White;
            toolRol.Name = "toolRol";
            toolRol.Padding = new Padding(8, 6, 8, 6);
            toolRol.Size = new Size(154, 34);
            toolRol.Text = "Rol:";
            // 
            // pnlContenedor
            // 
            pnlContenedor.Dock = DockStyle.Fill;
            pnlContenedor.Location = new Point(226, 44);
            pnlContenedor.Name = "pnlContenedor";
            pnlContenedor.Size = new Size(1054, 636);
            pnlContenedor.TabIndex = 4;
            // 
            // FrmPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            Controls.Add(pnlContenedor);
            Controls.Add(statusStrip1);
            Controls.Add(pnlSidebar);
            Controls.Add(pnlMovimiento);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(1280, 720);
            Name = "FrmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmPrincipal";
            WindowState = FormWindowState.Maximized;
            FormClosing += FrmPrincipal_FormClosing;
            pnlMovimiento.ResumeLayout(false);
            pnlSeguridad.ResumeLayout(false);
            pnlSidebar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picDif7).EndInit();
            ((System.ComponentModel.ISupportInitialize)picDif6).EndInit();
            ((System.ComponentModel.ISupportInitialize)picDif5).EndInit();
            ((System.ComponentModel.ISupportInitialize)picDif4).EndInit();
            ((System.ComponentModel.ISupportInitialize)picDif3).EndInit();
            ((System.ComponentModel.ISupportInitialize)picDif2).EndInit();
            ((System.ComponentModel.ISupportInitialize)picDif).EndInit();
            panel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMovimiento;
        private Panel pnlSidebar;
        private Panel panel1;
        private FontAwesome.Sharp.IconButton btnCerrarSesion;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolUsuario;
        private ToolStripStatusLabel toolRol;
        private Panel panel2;
        private Label label1;
        private Button btnIcon;
        private FontAwesome.Sharp.IconButton btnComercializacion;
        private FontAwesome.Sharp.IconButton btnProduccion;
        private FontAwesome.Sharp.IconButton btnCompras;
        private FontAwesome.Sharp.IconButton btnSeguridad;
        private FontAwesome.Sharp.IconButton btnCatalogos;
        private Label label2;
        private FontAwesome.Sharp.IconButton btnReportes;
        private FontAwesome.Sharp.IconButton btnInicio;
        private Panel pnlContenedor;
        private Button btnMinimizar;
        private Button btnCerrar;
        private Panel pnlSeguridad;
        private FontAwesome.Sharp.IconButton btnRoles;
        private FontAwesome.Sharp.IconButton btnUsuarios;
        private PictureBox picDif;
        private PictureBox picDif7;
        private PictureBox picDif6;
        private PictureBox picDif5;
        private PictureBox picDif4;
        private PictureBox picDif3;
        private PictureBox picDif2;
    }
}