
using GranjaControl.UI.Formularios;
using System.Diagnostics;

namespace GranjaControl.UI.Utilidades
{
    public class Mensajes
    {
        // Mostrar mensaje de error.
        public void MensajeError(string mensaje)
        {
            FrmMensajes frm =
                new FrmMensajes(mensaje, "error");

            frm.Show();
        }

        public void MensajeError(
            Exception error,
            string mensajeGenerico)
        {
            Trace.TraceError(
                "Error en GranjaControl: {0}",
                error);

            bool mensajeSeguro =
                error.Data["MensajeUsuario"] is true ||
                (error is ArgumentException && error is not ArgumentNullException);

            MensajeError(
                mensajeSeguro
                    ? error.Message
                    : mensajeGenerico);
        }

        // Mostrar mensaje de éxito.
        public void MensajeOk(string mensaje)
        {
            FrmMensajes frm =
                new FrmMensajes(mensaje, "ok");

            frm.Show();
        }

        // Mostrar mensaje de validación.
        public void MensajeValidacion(string mensaje)
        {
            FrmMensajes frm =
                new FrmMensajes(mensaje, "warning");

            frm.Show();
        }

        // Mostrar mensaje informativo.
        public void MensajeInformacion(string mensaje)
        {
            FrmMensajes frm =
                new FrmMensajes(mensaje, "info");

            frm.Show();
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
