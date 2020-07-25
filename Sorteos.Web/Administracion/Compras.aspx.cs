using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using Newtonsoft.Json;
using Sorteos.Services;
using Sorteos.Services.Datatables;
using Sorteos.Services.Datatables.Core;

namespace Sorteos.Web.Administracion
{
    public partial class Compras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                StateService stateService = new StateService();
                BrandService brandService = new BrandService();
                RaffleService raffleService = new RaffleService();

                var states = stateService.GetAllStates();

                cboProvincias.Items.Add(new ListItem("Seleccione una provincia", "0"));
                states.ForEach(state =>
                {
                    cboProvincias.Items.Add(new ListItem(state.Name, state.Id.ToString()));
                });

                var brands = brandService.GetAllBrands();
                cboMarcas.Items.Add(new ListItem("Seleccione una marca", "0"));
                brands.ForEach(brand =>
                {
                    cboMarcas.Items.Add(new ListItem(brand.Description, brand.Id.ToString()));
                });

                var raffles = raffleService.GetAllRaffles();
                var currentRaffle = raffleService.findCurrentRaffle();
                cboSorteos.Items.Add(new ListItem("Seleccione un sorteo", "0"));
                raffles.ForEach(raffle =>
                {
                    
                    cboSorteos.Items.Add(new ListItem { 
                        Text = raffle.Description,
                        Value = raffle.Id.ToString(),
                        Selected = currentRaffle.Id == raffle.Id
                    });
                    
                });

                cboCiudades.Items.Add(new ListItem("Seleccione una provincia primero", "0"));

                cboTipoCompra.Items.Add(new ListItem("Seleccione un tipo", ""));
                cboTipoCompra.Items.Add(new ListItem("Supermercado", "Supermercado"));
                cboTipoCompra.Items.Add(new ListItem("Tienda", "Tienda"));
            }
        }

        protected void cboProvincias_SelectedIndexChanged(object sender, EventArgs e)
        {
            var provinciaId = Int32.Parse(cboProvincias.SelectedValue);
            if (provinciaId > 0)
            {
                CityService cityService = new CityService();
                var cities = cityService.GetCitiesByState(provinciaId);
                cboCiudades.Items.Clear();
                cboCiudades.Items.Add(new ListItem("Seleccione una ciudad", "0"));
                cities.ForEach(city =>
                {
                    cboCiudades.Items.Add(new ListItem(city.Name, city.Id.ToString()));
                });
            }
            else
            {
                cboCiudades.Items.Clear();
                cboCiudades.Items.Add(new ListItem("Seleccione una provincia primero", "0"));
            }
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