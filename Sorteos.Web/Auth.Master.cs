using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sorteos.Web
{
    public partial class Auth : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Services.AppSingleton.Instance.LogoSrc))
                mainLogo.Src = Services.AppSingleton.Instance.LogoSrc;
            else
                mainLogo.Src = "/Content/images/oriental-logo.png";

                if (Session["ShowAlert"] != null)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "notification", Session["ShowAlert"].ToString(), true);
                Session["ShowAlert"] = null;
            }
            if (Session["UserId"] != null)
            {
                if (Request.Url.AbsolutePath == "/Login") {
                    if (WebContext.IsAdmin())
                        Response.Redirect("/Administracion/Estadisticas");
                    else
                        Response.Redirect("/Cliente/Resumen");

                }
            }
        }
    }
}