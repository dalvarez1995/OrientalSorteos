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
        }

        protected void btnIngresar_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                RaffleService raffleService = new RaffleService();

                raffleService.Insert(new RaffleModel { 
                    Description  = txtDescripcion.Text,
                    //HtmlCode = txtContenido.Text,
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