using Sorteos.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Facebook;
using Sorteos.Services.Properties;
using Sorteos.Services.Models;
using System.Reflection.Emit;

namespace Sorteos.Services
{
    public class AuthService
    {
        public Boolean Login(string correo, string password){
            using (var context = new SorteosDbEntities())
            {
                var userFound = context.Usuario.Where(user => user.Email == correo.ToLower()).FirstOrDefault();
                if (userFound == null)
                    throw new Exception("Credenciales no válidas");

                if(!SecurityUtil.CompareHash(userFound.PasswordHash, password))
                    throw new Exception("Credenciales no válidas");

                if (!userFound.EmailConfirmado)
                    return false;

            }
            return true;
        }

        public Boolean ValidarCorreoExistente(string email) {
            using (var context = new SorteosDbEntities())
            {
                var emailFound = context.Usuario.Where(user => user.Email == email).FirstOrDefault();
                if (emailFound != null)
                    return false;
                return true;
            }
        }

        public void ResendOtpCode(string email,string otp)
        {
            using (var context = new SorteosDbEntities())
            {
                var userFound = context.Usuario.Where(user => user.Email == email).FirstOrDefault();
                if (userFound != null)
                    throw new Exception("El usuario que esta intentando activar ya no existe.");

                userFound.OtpCode = otp;
                context.SaveChanges();
                EmailService.sendActivationAccountEmail(userFound.Email, otp);
            }
        }

        public Boolean ValidarActivacionCuenta(string email,string otp) {
            using (var context = new SorteosDbEntities())
            {
                var userFound = context.Usuario.Where(user => user.Email == email).FirstOrDefault();
                if (userFound == null)
                    throw new Exception("El usuario que esta intentando activar ya no existe.");

                if (userFound.OtpCode != otp)
                    return false;
                return true;
            }
        }

        public async Task ActivarCuenta(string email)
        {
            using (var context = new SorteosDbEntities())
            {
                var userFound = context.Usuario.Where(user => user.Email == email.ToLower()).FirstOrDefault();
                if (userFound == null)
                    throw new Exception("El usuario que esta intentando activar ya no existe.");

                userFound.OtpCode = "";
                userFound.EmailConfirmado = true;
                context.SaveChanges();
                await EmailService.sendWelcomeEmail(userFound.Email,userFound.Nombre);
            }
        }

        public async Task SolicitarRecuperacionClave(string email) {
            using (var context = new SorteosDbEntities())
            {
                var userFound = context.Usuario.Where(user => user.Email == email).FirstOrDefault();
                if (userFound == null)
                    throw new Exception("No existe ninguna cuenta asociada a el correo electrónico ingresado");
                userFound.CambioPassword = true;
                context.SaveChanges();
                await EmailService.sendRecoverPasswordEmail(userFound.Email, userFound.Nombre);
            }
        }

        public Boolean ValidarCambioClave(string email) {
            using (var context = new SorteosDbEntities())
            {
                var userFound = context.Usuario.Where(user => user.Email == email).FirstOrDefault();
                if (userFound == null)
                    throw new Exception("El usuario del que esta intentando recuperar su contraseña ya no existe.");

                if (userFound.CambioPassword == false)
                    return false;
                return true;
            }
        }

        public void CambiarClave(string email,string newPassword) {
            using (var context = new SorteosDbEntities())
            {
                var userFound = context.Usuario.Where(user => user.Email == email).FirstOrDefault();
                if (userFound == null)
                    throw new Exception("El usuario del que esta intentando recuperar su contraseña ya no existe.");
                userFound.CambioPassword = false;
                userFound.PasswordHash = SecurityUtil.HashPassword(newPassword);
                context.SaveChanges();
            }
        }

        public async Task Registrar(string firstName, string lastName,string email,string cellphone,string password,string otp = "", bool pendingActivation = true) {


            var newUser = new Usuario
            {
                Nombre = firstName,
                Apellido = lastName,
                Email = email.ToLower(),
                Telefono = cellphone,
                PasswordHash = password
            };

            newUser.PasswordHash = SecurityUtil.HashPassword(newUser.PasswordHash);
            using (var context = new SorteosDbEntities())
            {
                newUser.FechaCreacion = DateTime.UtcNow.AddHours(-5);
                newUser.EmailConfirmado = false;
                newUser.PerfilId = context.Perfil.Where(perfil => perfil.Descripcion == "Cliente").FirstOrDefault().Id;
                newUser.EmailConfirmado = !pendingActivation;
                newUser.OtpCode = otp; 
                context.Usuario.Add(newUser);
                context.SaveChanges();
                if (pendingActivation)
                    await EmailService.sendActivationAccountEmail(newUser.Email,otp);
                else
                    await EmailService.sendWelcomeEmail(newUser.Email, $"{newUser.Nombre } { newUser.Apellido}");
            }
        }


        public string FacebookLogin() {
            FacebookClient fbc = new FacebookClient();

            var clientId = Settings.Default.FacebookClientId;
            var redirectUri = Settings.Default.FacebookRedirectUri;

            var loginUrl = fbc.GetLoginUrl(new
            {
                client_id = clientId,
                redirect_uri = redirectUri,
                response_type = "code",
                scope = "email"
            });

            return loginUrl.AbsoluteUri;
        }

        public string GetFacebookUserAccessToken(string code) {
            FacebookClient fbc = new FacebookClient();

            var clientId = Settings.Default.FacebookClientId;
            var clientSecretKey = Settings.Default.FacebookSecretKey;
            var redirectUri = Settings.Default.FacebookRedirectUri;

            dynamic result = fbc.Post("oauth/access_token", new
            {
                client_id = clientId,
                client_secret = clientSecretKey,
                redirect_uri = redirectUri,
                code = code
            });

            //try get extended access token
            var extendedToken = GetExtendedAccessToken(result.access_token);

            return extendedToken;
        }

        private string GetExtendedAccessToken(string ShortLivedToken)
        {
            FacebookClient client = new FacebookClient();
            var clientId = Settings.Default.FacebookClientId;
            var clientSecretKey = Settings.Default.FacebookSecretKey;
            string extendedToken = "";
            try
            {
                dynamic result = client.Get("/oauth/access_token", new
                {
                    grant_type = "fb_exchange_token",
                    client_id = clientId,
                    client_secret = clientSecretKey,
                    fb_exchange_token = ShortLivedToken
                });
                extendedToken = result.access_token;
            }
            catch
            {
                extendedToken = ShortLivedToken;
            }
            return extendedToken;
        }

        public FacebookUser GetFacebookUserData(string accessToken)
        {
            FacebookClient fbc = new FacebookClient();
            fbc.AccessToken = accessToken;
            dynamic user = fbc.Get("me?fields=first_name,last_name,id,email,picture");

            FacebookUser facebookUser = new FacebookUser
            {
                UserId = user.id,
                FirstName = user.first_name,
                LastName = user.last_name,
                Email = user.email,
                PictureUrl = user.picture.data.url
            };

            return facebookUser;
        }
    }
}
