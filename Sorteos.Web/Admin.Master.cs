using Sorteos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sorteos.Web
{
    public partial class Admin : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ShowAlert"] != null)
            {
                var alert = Session["ShowAlert"].ToString();
                Page.ClientScript.RegisterStartupScript(GetType(), "notification", alert, true);
                Session["ShowAlert"] = null;

            }
            WebContext.ValidateAdminArea();

            var currentUser = WebContext.GetCurrentUser();
            if (currentUser == null)
            {
                Response.Redirect("/Login", true);
                return;
            }

            lblNombreCompleto.InnerText = currentUser.FullName;
            lblPerfil.InnerText = currentUser.Role.Description;
        }

        protected void Logout(object sender, EventArgs e) {
            Session.RemoveAll();
            Response.Redirect("/Login",false);
            return;
        }
    }
}