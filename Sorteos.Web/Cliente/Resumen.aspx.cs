using System;
using Sorteos.Services;

namespace Sorteos.Web.Cliente
{
    public partial class Resumen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = $"Resumen - {AppSingleton.Instance.Sitio.PageTitle}";

            WebContext.ValidateSession();

            empecemos.Visible = false;
            noHaySorteos.Visible = false;
            empezarParticipar.Visible = false;
            comprasRegistradas.Visible = false;

            if (!WebContext.AnyRaffleActive()) {
                noHaySorteos.Visible = true;
                return;
            }

            empecemos.Visible = true;
            RaffleService raffleService = new RaffleService();
            PurchaseService purchaseService = new PurchaseService();

            var activeRaffle = raffleService.findCurrentRaffle();
            var numOfPurchases = purchaseService.GetCustomerPurchasesCount(WebContext.GetCurrentUser().Email, activeRaffle.Id);

            var flipTimerScript = $@"
                $('.fliptimer').flipTimer({{
                        date: '{activeRaffle.EndDate.ToString("yyyy/MM/dd HH:mm:ss")}',
                        bgColor: '#EA252A',
                        timeZone: -5,
                        onFinish: function() {{
                            $('#empecemos').html(`
                                <div class=""col s12 m12"">
                                    <div class=""card"">
                                        <div class=""card-content"">
                                            <img src = ""/Content/images/slot-machine.svg"" style=""height: 150px;"" />
                                            <h6>Se acabo el tiempo!</h6>
                                        </div>
                                        <div class=""card-action"">
                                            <p style = ""text-align: justify;"" >
                                                <span> Síguenos en nuestras redes sociales</span>
                                                <span>ya que anunciaremos a los ganadores por esos medios.</span>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            `);
                        }}
                }});
            ";
            ClientScript.RegisterStartupScript(GetType(), "countdown", flipTimerScript ,true);

            if (numOfPurchases == 0)
            {
                empezarParticipar.Visible = true;
                return;
            }
            comprasRegistradas.Visible = true;
            numeroCompras.InnerText = numOfPurchases.ToString();
        }
    }
}