using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using Sorteos.Services;
using Sorteos.Services.Models;

namespace Sorteos.Web
{
    public partial class CompletarRegistro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["FacebookUser"] == null)
            {
                Response.Redirect("/Login");
                return;
            }

            var imageUrl = ((FacebookUser)Session["FacebookUser"]).PictureUrl;
            if (!string.IsNullOrEmpty(imageUrl))
                userImage.Src = imageUrl;
        }

        protected async void btnCompletarRegistro_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();

                if (!aceptarCondiciones.Checked)
                    throw new Exception("Debe aceptar las condiciones de uso y haber leído las políticas de privacidad.");

                var sessionFacebookUSer = Session["FacebookUser"];

                if (sessionFacebookUSer == null)
                    Session["ShowAlert"] = "error('Tu sesión ha caducado, ingresa de nuevo por favor.', 'Se acabo el tiempo!')";
                 

                AuthService authService = new AuthService();
                UserService userService = new UserService();

                var facebookUser = (FacebookUser)sessionFacebookUSer;

                await authService.Registrar(facebookUser.FirstName, facebookUser.LastName, facebookUser.Email, cellNumber.Text, password.Value,"", false);

                Session["UserId"] = facebookUser.Email;

                Response.Redirect("/Cliente/Resumen", false);
                return;
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "notification", $"error('{ex.Message}');", true);
            }
        }
    }
}