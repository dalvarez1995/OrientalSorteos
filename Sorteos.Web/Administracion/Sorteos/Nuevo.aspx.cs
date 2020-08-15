using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sorteos.Services;
using Sorteos.Services.Models;
using System.Globalization;

namespace Sorteos.Web.Administracion.Sorteos
{
    public partial class Nuevo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SiteService siteService = new SiteService();
                var sites = siteService.GetAllSites();

                sites.ForEach(sitio =>
                {
                    cboSitios.Items.Add(new ListItem
                    {
                        Text = sitio.BaseUrl,
                        Value = sitio.Id.ToString(),
                        Selected = false
                    });

                });
            }
        }

        protected void btnIngresar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                RaffleService raffleService = new RaffleService();

                int siteId = 0;
                Int32.TryParse(cboSitios.SelectedValue, out siteId);

                raffleService.Insert(new RaffleModel { 
                    Description  = txtDescripcion.Text,
                    //HtmlCode = txtContenido.Text,
                    SiteId = siteId,
                    BeginDate = DateTime.ParseExact(txtFechaInicio.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                    EndDate = DateTime.ParseExact(txtFechaFin.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                    Active = chkActivo.Checked
                });

                Session["ShowAlert"] = $"success('Sorteo registrado satisfactoriamente','Exito!');";
                Response.Redirect("/Administracion/Sorteos", false);
                return;
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "notification", $"error('{ex.Message}');", true);
            }
        }
    }
}