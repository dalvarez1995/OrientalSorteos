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
    public partial class Editar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                var pid = Request.QueryString["pid"];

                int sorteoId = 0;
                Int32.TryParse(pid, out sorteoId);

                if (sorteoId > 0)
                {
                    RaffleService raffleService = new RaffleService();
                    var sorteo = raffleService.GetRaffleById(sorteoId);

                    if (sorteo != null)
                    {
                        txtDescripcion.Text = sorteo.Description;
                        txtFechaInicio.Text = sorteo.BeginDate.ToString("yyyy-MM-dd");
                        txtFechaFin.Text = sorteo.EndDate.ToString("yyyy-MM-dd");
                        chkActivo.Checked = sorteo.Active;
                        return;
                    }
                }

                Session["ShowAlert"] = $"success('Sorteo registrado satisfactoriamente','Exito!');";
                Response.Redirect("/Administracion/Sorteos", false);
                return;
            }
        }

        protected void btnGuardar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                RaffleService raffleService = new RaffleService();

                var pid = Request.QueryString["pid"];

                int sorteoId = 0;
                Int32.TryParse(pid, out sorteoId);

                raffleService.Update(new RaffleModel
                {
                    Id = sorteoId,
                    Description = txtDescripcion.Text,
                    HtmlCode = txtContenido.Text,
                    BeginDate = DateTime.ParseExact(txtFechaInicio.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                    EndDate = DateTime.ParseExact(txtFechaFin.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None),
                    Active = chkActivo.Checked
                });

                Session["ShowAlert"] = $"success('Sorteo actualizado satisfactoriamente','Exito!');";
                Response.Redirect("/Administracion/Sorteos", false);
            } catch (Exception ex) {
                ClientScript.RegisterStartupScript(GetType(), "notification", $"error('{ex.Message}');", true);
            }
        }
    }
}