using SendGrid;
using System.Configuration;
using System.Threading.Tasks;
using Sorteos.Services.Properties;
using System.Security.Cryptography.X509Certificates;
using System;

namespace Sorteos.Services
{
    public class EmailService
    {
        public EmailService()
        {

        }

        public static async Task sendActivationAccountEmail(string customerName,string email,string otp)
        {
            if (string.IsNullOrEmpty(AppSingleton.Instance.Sitio.EmailTemplates.ActivationTemplateId))
                throw new Exception("El identificador de la plantilla del email no se ha encontrado.");
            var subject = "Activa tu cuenta";
            var templateId = AppSingleton.Instance.Sitio.EmailTemplates.ActivationTemplateId;
            var loginUrl = $"{AppSingleton.Instance.Sitio.BaseUrl}/Login";
            var siteName = AppSingleton.Instance.Sitio.PageTitle;
            var supportUrl = AppSingleton.Instance.Sitio.SupportUrl;
            var brand = AppSingleton.Instance.Sitio.Company;
            var dynamicTemplateData = $@"
                    ""customer_name"":""{customerName}"",
                    ""code_otp"":""{otp}"",
                    ""login_url"":""{loginUrl}"",
                    ""site_name"":""{siteName}"",
                    ""support_url"":""{supportUrl}"",
                    ""brand"":""{brand}""
            ";

            var response = await Send(email,subject, templateId, dynamicTemplateData);
        }


        private static async Task<Response> Send(string to,string subject, string templateId, string dynamicTemplateData)
        {

            var apiKey = AppSingleton.Instance.Sitio.SendGridApiKey;
            var client = new SendGridClient(apiKey);
            var company = AppSingleton.Instance.Sitio.Company;
            var emailAccount = AppSingleton.Instance.Sitio.EmailAccount;
            var facebook = AppSingleton.Instance.Sitio.FacebookLink;
            var instagram = AppSingleton.Instance.Sitio.InstagramLink;
            var whatsapp = AppSingleton.Instance.Sitio.WhatsappLink;
            var logo = AppSingleton.Instance.Sitio.LogoSrc;

            var body = $@"{{
                ""subject"": ""{subject}"",
                ""personalizations"": [
                {{
                    ""to"": [
                    {{
                        ""email"": ""{to}""
                    }}
                    ],
                    ""subject"": ""{subject}"",
                    ""dynamic_template_data"": {{
                        ""facebook_link"":""{facebook}"",
	                    ""instagram_link"":""{instagram}"",
	                    ""whatsapp_link"":""{whatsapp}"",
	                    ""logo_src"":""{logo}"",
                        {dynamicTemplateData}
                    }}
                }}
                ],
                ""from"": {{
                    ""email"": ""{emailAccount}"",
                    ""name"": ""Notificaciones {company}""
                }},
                ""template_id"": ""{templateId}""
            }}";

            return await client.RequestAsync(method: SendGridClient.Method.POST,
                                                     urlPath: "mail/send",
                                                     requestBody: body);
        }

        public static async Task sendWelcomeEmail(string email,string customerName)
        {
            if (string.IsNullOrEmpty(AppSingleton.Instance.Sitio.EmailTemplates.WelcomeTemplateId))
                throw new Exception("El identificador de la plantilla del email no se ha encontrado.");

            var loginUrl = $"{AppSingleton.Instance.Sitio.BaseUrl}/Login";
            var templateId = AppSingleton.Instance.Sitio.EmailTemplates.WelcomeTemplateId;
            var subject = "Bienvenido";
            var siteName = AppSingleton.Instance.Sitio.PageTitle;
            var supportUrl = AppSingleton.Instance.Sitio.SupportUrl;
            var brand = AppSingleton.Instance.Sitio.Company;

            var dynamicTemplateData = $@"
                    ""customer_name"":""{customerName}"",
                    ""login_url"":""{loginUrl}"",
                    ""site_name"":""{siteName}"",
                    ""support_url"":""{supportUrl}"",
                    ""brand"":""{brand}""
            ";

            var response = await Send(email,subject, templateId, dynamicTemplateData);
        }


        public static async Task sendRecoverPasswordEmail(string email,string customerName)
        {
            if (string.IsNullOrEmpty(AppSingleton.Instance.Sitio.EmailTemplates.RecoverPasswordTemplateId))
                throw new Exception("El identificador de la plantilla del email no se ha encontrado.");
            var payload = new string[][] { new string[] { "userId", email } };
            var token = SecurityUtil.GenerateJwtToken(payload,30);
            var subject = "Cambio de Contraseña";
            var passwordRecoverUrl = $"{AppSingleton.Instance.Sitio.BaseUrl}/Cambio-Password?pid={token}";
            var templateId = AppSingleton.Instance.Sitio.EmailTemplates.RecoverPasswordTemplateId;
            var brand = AppSingleton.Instance.Sitio.Company;
            var supportUrl = AppSingleton.Instance.Sitio.SupportUrl;
            var dynamicTemplateData = $@"
                    ""customer_name"": ""{customerName}"",
	                ""brand"": ""{brand}"",
                    ""support_url"":""{supportUrl}"",
	                ""password_recover_url"":""{passwordRecoverUrl}""
            ";

            var response = await Send(email, subject, templateId, dynamicTemplateData);
        }

    }
}