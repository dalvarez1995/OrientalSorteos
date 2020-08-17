using Sorteos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sorteos.Web.Administracion
{
    public partial class Estadisticas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RaffleService raffleService = new RaffleService();
                var currentRaffle = raffleService.findCurrentRaffle();

                var raffles = raffleService.GetAllRaffles();
                cboSorteos.Items.Add(new ListItem("Seleccione un sorteo", "0"));
                raffles.ForEach(raffle =>
                {

                    cboSorteos.Items.Add(new ListItem
                    {
                        Text = raffle.Description,
                        Value = raffle.Id.ToString(),
                        Selected = currentRaffle != null ? currentRaffle.Id == raffle.Id : false
                    });

                });
            }
        }
    }
}