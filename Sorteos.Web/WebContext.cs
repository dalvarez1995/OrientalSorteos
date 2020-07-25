using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Sorteos.Services;
using Sorteos.Services.Models;

namespace Sorteos.Web
{
    public class WebContext
    {
        public static UserModel GetCurrentUser() 
        {
            var userId = HttpContext.Current.Session["UserId"];
            UserService userService = new UserService();
            return userService.GetUserByEmail(userId.ToString());
        }

        public static bool IsFacebookLogged() {
            return HttpContext.Current.Session["FacebookUser"] != null ? true : false;
        }

        public static FacebookUser GetFacebookUser()
        {
            return (FacebookUser)HttpContext.Current.Session["FacebookUser"];
        }

        public static bool IsAdmin()
        {
            var userId = HttpContext.Current.Session["UserId"];
            if (userId == null)
                return false;
            UserService userService = new UserService();
            var user = userService.GetUserByEmail(userId.ToString());
            return user.Role.Special;
        }

        public static void ValidateSession()
        {
            var userId = HttpContext.Current.Session["UserId"];
            if (userId == null)
            {
                HttpContext.Current.Session["ShowAlert"] = "warning('Su sesión ha caducado por favor, vuelva a ingresar','Sesion caducada!');";
                HttpContext.Current.Response.Redirect("/Login",true);
            }
        }

        public static void ValidateAdminArea() {
            ValidateSession();
            var userId = HttpContext.Current.Session["UserId"];
            UserService userService = new UserService();
            var user = userService.GetUserByEmail(userId.ToString());
            if (!user.Role.Special) {
                HttpContext.Current.Session["ShowAlert"] = "warning('No tiene permisos para acceder a esta página','Permisos Insuficientes!');";
                HttpContext.Current.Response.Redirect("/Cliente/Resumen", true);
            }
        }

        public static bool AnyRaffleActive() {
            RaffleService raffleService = new RaffleService();
            return raffleService.findCurrentRaffle() != null ? true : false;
        }
    }
}