using Sorteos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sorteos.Web.Administracion
{
    public partial class Depurar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlDebug.Visible = false;
            pnlNoPendingPurchases.Visible = false;
            pnlSelectRaffle.Visible = false;

            PurchaseService purchaseService = new PurchaseService();
            RaffleService raffleService = new RaffleService();
            var currentRaffle = raffleService.findCurrentRaffle();

            if (!IsPostBack)
            {
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

            if (cboSorteos.SelectedIndex > 0)
            {
                var pendingPurchasesCount = purchaseService.GetPendingPurchasesCountByRaffle(Int32.Parse(cboSorteos.SelectedValue));
                if (pendingPurchasesCount > 0)
                    pnlDebug.Visible = true;
                else
                    pnlNoPendingPurchases.Visible = true;
                return;
            }
            pnlSelectRaffle.Visible = true;


        }
    }
}