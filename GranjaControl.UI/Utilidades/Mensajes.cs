
using GranjaControl.UI.Formularios;

namespace GranjaControl.UI.Utilidades
{
    public class Mensajes
    {
        // Mostrar mensaje de error.
        public void MensajeError(string mensaje)
        {
            FrmMensajes frm =
                new FrmMensajes(mensaje, "error");

            frm.ShowDialog();
        }

        // Mostrar mensaje de éxito.
        public void MensajeOk(string mensaje)
        {
            FrmMensajes frm =
                new FrmMensajes(mensaje, "ok");

            frm.ShowDialog();
        }

        // Mostrar mensaje de validación.
        public void MensajeValidacion(string mensaje)
        {
            FrmMensajes frm =
                new FrmMensajes(mensaje, "warning");

            frm.ShowDialog();
        }

        // Mostrar mensaje informativo.
        public void MensajeInformacion(string mensaje)
        {
            FrmMensajes frm =
                new FrmMensajes(mensaje, "info");

            frm.ShowDialog();
        }

        // Mostrar mensaje de confirmación.
        public DialogResult MensajeConfirmacion(string mensaje)
        {
            FrmMensajes frm =
                new FrmMensajes(mensaje, "confirmar");

            return frm.ShowDialog();
        }
    }
}
