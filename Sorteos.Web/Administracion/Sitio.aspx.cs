using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sorteos.Services;

namespace Sorteos.Web.Administracion
{
    public partial class Sitio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                SiteService siteService = new SiteService();

                var site = siteService.GetSiteById(AppSingleton.Instance.Sitio.Id);
                txtCondicionesServicio.Text = site.TOS;
                txtPoliticaPrivacidad.Text = site.POP;
            }
        }

        protected void btnGuardar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                SiteService siteService = new SiteService();
                siteService.UpdateSite(AppSingleton.Instance.Sitio.Id,txtCondicionesServicio.Text, txtPoliticaPrivacidad.Text);
                Session["ShowAlert"] = $"success('Sitio actualizado satisfactoriamente','Exito!');";
                Response.Redirect("/Administracion/Estadisticas", false);
                return;
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "notification", $"error('{ex.Message}');", true);
            }
        }
    }
}