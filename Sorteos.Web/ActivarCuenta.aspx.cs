using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Sorteos.Services;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace Sorteos.Web
{
    public partial class ActivarCuenta : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var activationId = Session["UserActivationId"];

                if (activationId == null)
                    Response.Redirect("/Login", true);




            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "notification", $"error('{ex.Message}');", true);
            }

        }

        protected async void Activar(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();

                var activationId = Session["UserActivationId"];
                if (activationId == null)
                {
                    Session["ShowAlert"] = "warning('Su sesión ha caducado por favor, vuelva a ingresar','Sesion caducada!');";
                    Response.Redirect("/Login", true);
                }

                var email = activationId.ToString();
                AuthService authService = new AuthService();
                if (!authService.ValidarActivacionCuenta(email, otp_code.Text)) {
                    otp_code.Text = "";
                    otp_code.Focus();
                    throw new Exception("Codigo incorrecto.");
                }
                
                await authService.ActivarCuenta(email);

                Session.RemoveAll();
                Session["UserId"] = email;
                if (WebContext.IsAdmin())
                    Response.Redirect("/Administracion/Estadisticas");
                else
                    Response.Redirect("/Cliente/Resumen");

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "notification", $"error('{ex.Message}');", true);
            }
        }

        protected void Reenviar(object sender, EventArgs e)
        {
            try
            {
                var activationId = Session["UserActivationId"];
                if (activationId == null)
                {
                    Session["ShowAlert"] = "warning('Su sesión ha caducado por favor, vuelva a ingresar','Sesion caducada!');";
                    Response.Redirect("/Login", true);
                }

                AuthService authService = new AuthService();
                authService.ResendOtpCode(activationId.ToString(),SecurityUtil.GenerateOTP(6));
                ClientScript.RegisterStartupScript(GetType(), "notification", $"success('Nuevo codigo generado, revise su correo electrónico.','Listo!','Si no visualizas el correo en tu bandeja principal, revisa en correos no deseados,spam o promociones(Gmail)');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "notification", $"error('{ex.Message}');", true);
            }
        }
    }
}