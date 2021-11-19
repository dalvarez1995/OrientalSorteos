<%@ Page Title="Resumen" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="Resumen.aspx.cs" Inherits="Sorteos.Web.Cliente.Resumen" %>

<asp:Content ID="resumenHead" ContentPlaceHolderID="head" runat="server">
    <webopt:BundleReference runat="server" Path="~/Content/css/fliptimer" />
</asp:Content>
<asp:Content ID="resumenMain" ContentPlaceHolderID="main" runat="server">
    <asp:Panel ID="noHaySorteos" class="col s12 m12 center-align" runat="server">
        <img src="/Content/images/empty-raffle.svg" style="max-height: 150px; margin-top: 20px;" alt="Activa" />
        <h5>Lo sentimos!</h5>
        <h6>Al momento no tenemos ningún sorteo activo, siguenos en nuestras redes sociales para estar atento a nuestro próximo sorteo</h6>
    </asp:Panel>
    <asp:Panel ID="empecemos" ClientIDMode="Static" runat="server" class="row center-align">
        <asp:Panel ID="comprasRegistradas" class="col s12 m12" runat="server">
            <div class="card">
                <div class="card-content">
                    <h6>Felicidades! Ya estas participando.</h6>
                    <span>Si quieres tener más oportunidades de ganar, adquiere más de nuestros productos participantes y registralos.</span>
                    <span id="numeroCompras" class="card-title" style="margin-top: 1rem;" runat="server">0</span>
                    <p>
                        Compras Registradas
                    </p>
                </div>
                <div class="card-action">
                    <a href="/Cliente/Participar" class="btn waves-effect pulse waves-light" style="background-color: #EA252A">
                        <i class="fas fa-shopping-bag left"></i>
                        REGISTRAR COMPRA
                    </a>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="empezarParticipar" class="col s12 m12" runat="server">
            <div class="card">
                <div class="card-content">
                    <img src="/Content/images/vote.svg" style="height: 150px;" />
                    <h6>Aún no estas participando!</h6>
                </div>
                <div class="card-action">
                    <a href="/Cliente/Participar" class="btn waves-effect pulse waves-light" style="background-color: #EA252A">
                        <i class="fas fa-shopping-bag left"></i>
                        EMPEZAR A PARTICIPAR
                    </a>
                </div>
            </div>
        </asp:Panel>
        <div class="col s12 m12">
            <div class="card">
                <div class="card-content">
                    <h6 style="font-weight:700;">Tiempo Restante</h6>
                </div>
                <div class="card-action">
                    <div class="fliptimer"></div>
                </div>
            </div>
        </div>
        <div class="col s12 m12">
            <p style="text-align: justify;">
                <span>Síguenos en nuestras redes sociales</span>
                <span>para estar atento a futuros sorteos.</span>
                <br />
                <span>Además anunciaremos los ganadores por esos medios.</span>
            </p>
        </div>
    </asp:Panel>
    <div class="col s12 social-buttons ">
        <div class="row">
            <div class="col s12">
                <a href="https://www.facebook.com/milkyoficial.ec" target="_blank" class="btn facebook-button waves-effect waves-light">
                    <i class="fab fa-facebook-square left"></i>
                    MILKY
                </a>
            </div>
        </div>
        <div class="row">
            <div class="col s12">
                <a href="https://www.facebook.com/aguamana.ec/" target="_blank" class="btn facebook-button waves-effect waves-light">
                    <i class="fab fa-facebook-square left"></i>
                    AGUA MANÁ
                </a>
            </div>
        </div>
        <div class="row">
            <div class="col s12">
                <a href="https://www.facebook.com/teverde.ec/" target="_blank" class="btn facebook-button waves-effect waves-light">
                    <i class="fab fa-facebook-square left"></i>
                    TÉ VERDE
                </a>
            </div>
        </div>
        <div class="row">
            <div class="col s12">
                <a href="https://www.facebook.com/applefit.ec/" target="_blank" class="btn facebook-button waves-effect waves-light">
                    <i class="fab fa-facebook-square left"></i>
                    APPLEFIT
                </a>
            </div>
        </div>
        <div class="row">
            <div class="col s12">
                <a href="https://www.facebook.com/TeChaOriental/" target="_blank" class="btn facebook-button waves-effect waves-light">
                    <i class="fab fa-facebook-square left"></i>
                    TÉ CHA
                </a>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="resumenBody" ContentPlaceHolderID="body" runat="server">
    <%: Scripts.Render("~/bundles/fliptimer") %>
</asp:Content>
