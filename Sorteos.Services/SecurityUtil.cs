using System;
using System.Linq;
using System.Text; 
using Newtonsoft.Json.Linq;
//using System.Security.Claims;
//using System.IdentityModel.Tokens.Jwt;
//using Microsoft.IdentityModel.Logging;
//using Microsoft.IdentityModel.Tokens;
using Sorteos.Services.Properties;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.IdentityModel.Logging;
using System.IdentityModel.Tokens.Jwt;

namespace Sorteos.Services
{
    public static class SecurityUtil
    {
        private const string _AUDIENCE_TOKEN = "customers-sorteos";
        private const string _ISSUER_TOKEN = "oriental";

        public static string GenerateJwtToken(string[][] parms,int? expireMinutes = null)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(Settings.Default.TokenSecret));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(parms.Select((parm, i) => new Claim(parm[0], parm[1])));

            //defining expiring date
            DateTime? expireDate = null;
            if (expireMinutes.HasValue)
                DateTime.UtcNow.AddMinutes(expireMinutes.Value);

            // create token to the user
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: _AUDIENCE_TOKEN,
                issuer: _ISSUER_TOKEN,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: expireDate,
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);

            return jwtTokenString;

        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[20]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[40];
            Array.Copy(salt, 0, hashBytes, 0, 20);
            Array.Copy(hash, 0, hashBytes, 20, 20);
            return Convert.ToBase64String(hashBytes);
        }

        public static Boolean CompareHash(string passwordHash, string enteredPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(passwordHash);
            byte[] salt = new byte[20];
            Array.Copy(hashBytes, 0, salt, 0, 20);
            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 20] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static JObject ValidateJwtToken(string jwtToken, string[] props)
        {
            JObject payload = new JObject();

            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _AUDIENCE_TOKEN;
            validationParameters.ValidIssuer = _ISSUER_TOKEN;
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.Default.TokenSecret));

            ClaimsPrincipal principal = new ClaimsPrincipal();
            try
            {
                principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);
            }
            catch (Exception)
            {
                payload["error"] = "Error al validar el token.";
                return payload;
            }
            foreach (var prop in props)
            {
                var propValue = "";

                var claim = principal.Claims.Where(cl => cl.Type == prop).FirstOrDefault();

                propValue = claim != null ? claim.Value : "";

                payload[prop] = propValue;
            }


            return payload;

        }

        public static string GenerateOTP(int digits)
        {
            var chars1 = "1234567890";
            var stringChars1 = new char[digits];
            var random1 = new Random();

            for (int i = 0; i < stringChars1.Length; i++)
            {
                stringChars1[i] = chars1[random1.Next(chars1.Length)];
            }

            var str = new String(stringChars1);

            return str;
        }
    }
}
