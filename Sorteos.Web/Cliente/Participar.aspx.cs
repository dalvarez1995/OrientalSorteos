using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using Sorteos.Services;
using Sorteos.Services.Models;

namespace Sorteos.Web.Cliente
{
    public partial class Participar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = $"Participar - {AppSingleton.Instance.Sitio.PageTitle}";
            if (!WebContext.AnyRaffleActive())
            {
                Response.Redirect("/Cliente/Resumen",true);
                return;
            }

            try
            {
                if (!Page.IsPostBack) {
                    StateService stateService = new StateService();
                    BrandService brandService = new BrandService();

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
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "notification", $"error('{ex.Message.Replace("'","\"")}');", true);
            }
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (Page.IsPostBack)
                {
                    PurchaseService purchaseService = new PurchaseService();
                    RaffleService raffleService = new RaffleService();
                    var currentUser = WebContext.GetCurrentUser();
                    Regex extensionReg = new Regex(@"\.(png|jpg|jpeg)", RegexOptions.IgnoreCase);

                    var currentRaffle = raffleService.findCurrentRaffle();
                    if (currentRaffle == null)
                        throw new Exception("No exiten sorteos activos. Inténtelo más tarde");


                    var postedfile = fuFoto.PostedFile;
                    var imagesPath = Server.MapPath($"~/Content/images/{currentRaffle.Description.Replace(" ","").ToLower()}/invoices/");

                    if (!Directory.Exists(imagesPath))
                        Directory.CreateDirectory(imagesPath);

                    if (!extensionReg.IsMatch(postedfile.FileName))
                        throw new Exception("Extensión de archivo no permitida.");

                    string fileExtension = Path.GetExtension(postedfile.FileName);
                    var fileName = $"{postedfile.FileName.Replace(fileExtension, "")}-{DateTime.UtcNow.AddHours(-5).ToString("yyyyMMddhhmmss")}{fileExtension}";

                    postedfile.SaveAs(imagesPath + fileName);
                    var ciudadId = Request.Form["ciudadId"];
                    purchaseService.Insert(
                        txtNumeroLote.Text,
                        radioDigital.Checked ? "Digital" : "Impresa",
                        radioSupermercado.Checked ? "Supermercado": "Tienda",
                        Int32.Parse(txtCantidad.Text),
                        Int32.Parse(ciudadId),
                        Int32.Parse(cboMarcas.SelectedValue),
                        Int32.Parse(cboProvincias.SelectedValue),
                        currentRaffle.Id,
                        currentUser.Id,
                        fileName
                    );

                    Session["ShowAlert"] = $"success('Compra registrada exitosamente.', 'Exito!');";
                    Response.Redirect("/", false);
                    return;
                }
            }catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "notification", $"error('{ex.Message.Replace("'", "\"")}');", true);
            }
        }

    }
}