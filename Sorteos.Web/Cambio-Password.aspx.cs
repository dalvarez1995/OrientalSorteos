using Sorteos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sorteos.Web
{
    public partial class Cambio_Password : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    passwordCambiada.Visible = false;
                    noValida.Visible = false;
                    formulario.Visible = false;

                    var token = Page.Request.Params["pid"];

                    if (string.IsNullOrEmpty(token))
                    {
                        noValida.Visible = true;
                        return;
                    }

                    var payload = SecurityUtil.ValidateJwtToken(token, new string[] { "userId" });
                    if (payload["error"] != null)
                    {
                        noValida.Visible = true;
                        return;
                    }

                    var email = payload["userId"].ToString();

                    AuthService authService = new AuthService();
                    if (!authService.ValidarCambioClave(email))
                    {
                        passwordCambiada.Visible = true;
                        return;
                    }

                    formulario.Visible = true;
                }

            }
            catch (Exception ex)
            {
                noValida.Visible = true;
                ClientScript.RegisterStartupScript(GetType(), "notification", $"error('{ex.Message}');", true);
            }
        }

        protected void CambiarClave(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();

                AuthService authService = new AuthService();

                var token = Page.Request.Params["pid"];
                var payload = SecurityUtil.ValidateJwtToken(token, new string[] { "userId" });
                if (payload["error"] != null)
                {
                    noValida.Visible = true;
                    return;
                }
                var email = payload["userId"].ToString();

                authService.ValidarCambioClave(email);
                authService.CambiarClave(email, password.Text);

                Session["ShowAlert"] = $"success('La contraseña ha sido modificada con exito.','Exito!');";
                Response.Redirect("/Login");
                return;
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "notification", $"error('{ex.Message}');", true);
            }
        }

    }
}