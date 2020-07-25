using System;
using System.Web.UI;
using Sorteos.Services;

namespace Sorteos.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    var code = Request.QueryString["code"];

                    if (code == null)
                    {
                        email.Focus();
                        return;
                    }

                    AuthService auth = new AuthService();
                    UserService userService = new UserService();

                    var accessToken = auth.GetFacebookUserAccessToken(code);
                    var facebookUserData = auth.GetFacebookUserData(accessToken);

                    var user = userService.GetUserByEmail(facebookUserData.Email);
                    

                    Session["FacebookUser"] = facebookUserData;

                    if (user == null)
                    {
                        Response.Redirect("/CompletarRegistro");
                        return;
                    }

                    user.FacebookAccessToken = accessToken;
                    userService.Update(user);
                    Session["UserId"] = facebookUserData.Email;
                    Response.Redirect("/Cliente/Resumen");


                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "notification", $"error('{ex.Message}');", true);
            }

        }

        public void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();

                AuthService authService = new AuthService();

                if (!authService.Login(email.Text, password.Text))
                {
                    //ClientScript.RegisterStartupScript(GetType(), "notification", $@"
                    //    warning('Revise su correo electrónico, en busca del email de confirmación de esta cuenta y siga las instrucciones.',
                    //            'Pendiente Activación',
                    //            'No recibio ningún correo? Comuniquese con nosotros <a href=""#"" style=""margin-left:5px"">Aqui</a>');", true);
                    //Session["ShowAlert"] = "warning('Su cuenta esta pendiente de activación, ingrese el código que le enviamos a su correo.');";
                    Session["UserActivationId"] = email.Text;
                    Response.Redirect("/ActivarCuenta", false);
                    return;
                }

                Session["UserId"] = email.Text;
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

        protected void btnfacebookLogin_ServerClick(object sender, EventArgs e)
        {
            AuthService auth = new AuthService();
            var redirectUri = auth.FacebookLogin();
            Response.Redirect(redirectUri);
            return;
        }

        protected void btnWhatsappLogin_ServerClick(object sender, EventArgs e)
        {

        }
    }
}