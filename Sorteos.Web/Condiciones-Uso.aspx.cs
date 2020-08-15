using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using Sorteos.Services;

namespace Sorteos.Web
{
    public partial class Condiciones_Uso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                SiteService siteService = new SiteService();
                var site = siteService.GetSiteById(AppSingleton.Instance.Sitio.Id);
                tosContent.InnerHtml = site.TOS;
            }
        }
    }
}