namespace GranjaControl.UI.Utilidades
{
    public static class MostrarModal
    {

        public static DialogResult MostrarConCapa(Form formularioPrincipal, Form formularioModal)
        {
            // Crear la capa semitransparente.
            Form capa = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.FromArgb(62, 125, 82),
                Opacity = 0.30,
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.Manual,
                Location = formularioPrincipal.PointToScreen(
                    Point.Empty),
                Size = formularioPrincipal.ClientSize,
                TopMost = false
            };

            try
            {
                // Mostrar la capa sobre el formulario principal.
                capa.Show(formularioPrincipal);

                // Mostrar el formulario modal encima de la capa.
                formularioModal.ShowInTaskbar = false;

                return formularioModal.ShowDialog(
                    formularioPrincipal);
            }
            finally
            {
                // Cerrar la capa al terminar el formulario modal.
                capa.Close();
                capa.Dispose();
            }
        }
    }
}
