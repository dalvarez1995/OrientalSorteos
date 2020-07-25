using SendGrid;
using System.Configuration;
using System.Threading.Tasks;
using Sorteos.Services.Properties;
using System.Security.Cryptography.X509Certificates;

namespace Sorteos.Services
{
    public class EmailService
    {
        public EmailService()
        {

        }

        public static async Task sendActivationAccountEmail(string email,string otp)
        {
            var payload = new string[][] { new string[] { "userId", email } };
            var subject = "Activa tu cuenta";
            var templateId = "d-49a7742952c246a0be740c82443e701f";
            var company = Settings.Default.Company;
            var dynamicTemplateData = $@"
      	            'destination' : '{email}',
	                'company_name': '{company}',
	                'code_otp':'{otp}'
            ";

            var response = await Send(email,subject, templateId, dynamicTemplateData);
        }


        private static async Task<Response> Send(string to,string subject, string templateId, string dynamicTemplateData)
        {

            var apiKey = Settings.Default.SendGridApiKey;
            var client = new SendGridClient(apiKey);
            var company = Settings.Default.Company;
            var emailAccount = Settings.Default.EmailAccount;
            var facebook = Settings.Default.FacebookLink;
            var instagram = Settings.Default.InstagramLink;
            var whatsapp = Settings.Default.WhatsappLink;
            var logo = Settings.Default.LogoSrc;

            var body = $@"{{
                'subject': '{subject}',
                'personalizations': [
                {{
                    'to': [
                    {{
                        'email': '{to}'
                    }}
                    ],
                    'subject': '{subject}',
                    'dynamic_template_data': {{
                        'facebook_link':'{facebook}',
	                    'instagram_link':'{instagram}',
	                    'whatsapp_link':'{whatsapp}',
	                    'logo_src':'{logo}',
                        {dynamicTemplateData}
                    }}
                }}
                ],
                'from': {{
                    'email': '{emailAccount}',
                    'name': 'Notificaciones {company}'
                }},
                'template_id': '{templateId}'
            }}";

            return await client.RequestAsync(method: SendGridClient.Method.POST,
                                                     urlPath: "mail/send",
                                                     requestBody: body.Replace("'", "\""));
        }

        public static async Task sendWelcomeEmail(string email,string nombreCompleto)
        {
            var loginUrl = $"{Settings.Default.BaseUrl}/Login";
            var templateId = "d-dc029ea4bec34cb1b740adb7267ecb49";
            var subject = "Bienvenido";

            var dynamicTemplateData = $@"
                    'page_name': 'Regreso a Clases Milky',
      	            'login_url' : '{loginUrl}',
	                'full_name': '{nombreCompleto}'
            ";

            var response = await Send(email,subject, templateId, dynamicTemplateData);
        }


        public static async Task sendRecoverPasswordEmail(string email,string name)
        {
            var payload = new string[][] { new string[] { "userId", email } };
            var token = SecurityUtil.GenerateJwtToken(payload,30);
            var subject = "Cambio de Contraseña";
            var activationUrl = $"{Settings.Default.BaseUrl}/Cambio-Password?pid={token}";
            var templateId = "d-ca31fbb3e157433b96ae2c0d962d564e";
            var company = Settings.Default.Company;
            var dynamicTemplateData = $@"
                    'name': '{name}'
	                'company_name': '{company}',
	                'activation_url':'{activationUrl}'
            ";

            var response = await Send(email, subject, templateId, dynamicTemplateData);
        }

    }
}