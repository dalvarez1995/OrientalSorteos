using ClosedXML.Excel;
using Newtonsoft.Json;
using Sorteos.Services;
using Sorteos.Services.Datatables;
using Sorteos.Services.Datatables.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sorteos.Web.Administracion
{
    public partial class Ganadores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlNoFinalizedRaffle.Visible = false;
            pnlFinishedRaffle.Visible = false;
            pnlWinnerSelection.Visible = false;
            pnlSelectRaffle.Visible = false;

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
                var selectedRaffle = raffleService.GetRaffleById(Int32.Parse(cboSorteos.SelectedValue));
                if (selectedRaffle.Finished)
                {
                    pnlFinishedRaffle.Visible = true;
                    ClientScript.RegisterStartupScript(GetType(), "datatableInitialization", $"drawDtWinnersFinalized();", true);
                } else if (selectedRaffle.EndDate > DateTime.UtcNow.AddHours(-5)) {
                    pnlNoFinalizedRaffle.Visible = true;
                }
                else
                {
                    pnlWinnerSelection.Visible = true;
                    txtParticipants.Value = string.Join("\n", raffleService.GetParticipantsByRaffle(selectedRaffle.Id).Select(p => $"{p.FullName}-{p.ChancesNumber}-{p.UserId}"));
                    ClientScript.RegisterStartupScript(GetType(), "datatableInitialization", $"drawDtWinners();", true);
                }

                return;
            }
            pnlSelectRaffle.Visible = true;

        }

        protected void exportarExcel(object sender, EventArgs e)
        {
            var dtRequest = JsonConvert.DeserializeObject<DTRequest>(Request.Form.Get("dtParams"));
            var dtResponse = new PurchaseDatatable(true).GetDTResponse(dtRequest);
            PropertyDescriptorCollection properties;

            //get anonymous items type of dynamic list
            properties = TypeDescriptor.GetProperties(typeof(CompraDtModel));
            DataTable dt = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                dt.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (var item in dtResponse.data)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                dt.Rows.Add(row);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Orders");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", $"attachment;filename=ReporteComprasRegistradas-{DateTime.UtcNow.AddHours(-5).ToString("yyyyMMdd")}.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
    }
}