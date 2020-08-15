using System;
using Sorteos.Services;

namespace Sorteos.Web
{
    public partial class Registrarse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = $"Registrarse - {AppSingleton.Instance.Sitio.PageTitle}";
            if (Session["LoggedUser"] != null)
            {
                Response.Redirect("/");
            }

        }

        protected async void btnRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();

                if (!aceptarCondiciones.Checked)
                    throw new Exception("Debe aceptar las condiciones de uso y haber leído las políticas de privacidad.");

                AuthService auth = new AuthService();

                if (!auth.ValidarCorreoExistente(email.Text)) {
                    email.Focus();
                    throw new Exception("El correo electrónico ya se encuentra ingresado");
                }

                var otpCode = SecurityUtil.GenerateOTP(6);
                await auth.Registrar(firstName.Text, lastName.Text, email.Text, cellNumber.Text, password.Text, otpCode);

                

                Session["ShowAlert"] = 
                    "success('Tu cuenta se ha creado con éxito, ingresa el código que te enviamos a tu correo para activar tu cuenta.','Activación Pendiente','Si no visualizas el correo en tu bandeja principal, revisa en correos no deseados,spam o promociones(Gmail)');";

                Session["UserActivationId"] = email.Text;
                Response.Redirect("/ActivarCuenta",false);
                firstName.Text = "";
                lastName.Text = "";
                email.Text = "";
                cellNumber.Text = "";
                return;
            }
            catch (Exception ex)
            {
              ClientScript.RegisterStartupScript(GetType(), "notification",$"error('{ex.Message}');",true);
            }
        }

        protected void btnfacebookRegister_ServerClick(object sender, EventArgs e)
        {
            AuthService auth = new AuthService();
            var redirectUri = auth.FacebookLogin();
            Response.Redirect(redirectUri);
            return;
        }

        protected void btnWhatsappRegister_ServerClick(object sender, EventArgs e)
        {

        }
    }
}