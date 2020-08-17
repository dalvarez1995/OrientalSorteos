using System;
using System.Web.UI;

namespace Sorteos.Web
{
    public partial class Public : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Services.AppSingleton.Instance.LogoSrc))
                userImage.Src = Services.AppSingleton.Instance.LogoSrc;
            else
                userImage.Src = "/Content/images/oriental-logo.png";

            if (Session["ShowAlert"] != null)
            {
                var alert = Session["ShowAlert"].ToString();
                Page.ClientScript.RegisterStartupScript(GetType(), "notification", alert, true);
                Session["ShowAlert"] = null;

            }
            WebContext.ValidateSession();

            var currentUser = WebContext.GetCurrentUser();
            if (currentUser == null)
            {
                Response.Redirect("/Login", true);
                return;
            }

            fullName.InnerText = currentUser.FullName;
            if (WebContext.IsFacebookLogged()) {
                userImage.Src = WebContext.GetFacebookUser().PictureUrl;
            }
        }

        protected void Logout(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("/Login", false);
            return;
        }

    }
}