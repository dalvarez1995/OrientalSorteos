using Sorteos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sorteos.Web
{
    public partial class Olvido_Password : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            email.Focus();
        }


        protected async void Solicitar(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();

                AuthService authService = new AuthService();
                await authService.SolicitarRecuperacionClave(email.Text);
                ClientScript.RegisterStartupScript(GetType(), "notification", $"success('Se ha enviado un correo electrónico al correo ingresado con las instrucciones para recuperar su contraseña.','Exito!','Si no visualizas el correo en tu bandeja principal, revisa en correos no deseados,spam o promociones(Gmail)');", true);
                email.Text = "";
                return;
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "notification", $"error('{ex.Message}');", true);
            }
        }
    }
}