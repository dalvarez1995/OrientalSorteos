<%@ Page Title="Resumen" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="Resumen.aspx.cs" Inherits="Sorteos.Web.Cliente.Resumen" %>

<asp:Content ID="resumenHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="resumenMain" ContentPlaceHolderID="main" runat="server">
    <div class="row">
        <h6 class="o-label"><span>SORTEO</span></h6>
    </div>
    <asp:Panel ID="noHaySorteos" class="row center-align" runat="server">
        <img src="/Content/images/empty-raffle.svg" style="max-height: 150px; margin-top: 20px;" alt="Activa" />
        <h5>Lo sentimos!</h5>
        <h6>Al momento no tenemos ningún sorteo activo, siguenos en nuestras redes sociales para estar atento a nuestro próximo sorteo</h6>
    </asp:Panel>
    <asp:Panel ID="empecemos" runat="server" class="row center-align">
        <h4 id="nombreSorteo" runat="server"></h4>
        <div class="row">
            <asp:Panel ID="comprasRegistradas" class="col s12 m12" runat="server">
                <div class="card">
                    <div class="card-content">
                        <h6>Felicidades! Ya estas participando.</h6>
                        <span>Si quieres tener más oportunidades de ganar, adquiere más de nuestros productos participantes y registralos.</span>
                        <span id="numeroCompras" class="card-title" style="margin-top:1rem;" runat="server">0</span>
                        <p>
                            Compras Registradas
                        </p>
                    </div>
                    <div class="card-action">
                        <a href="/Cliente/Participar" class="btn waves-effect waves-light" style="background-color: #EA252A">
                            <i class="fas fa-shopping-bag left"></i>
                            REGISTRAR COMPRA
                        </a>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="empezarParticipar" class="col s12 m12" runat="server">
                <div class="card">
                    <div class="card-content">
                        <h5>Aún no estas participando!</h5>
                    </div>
                    <div class="card-action">
                        <a href="/Cliente/Participar" class="btn waves-effect waves-light" style="background-color: #EA252A">
                            <i class="fas fa-shopping-bag left"></i>
                            EMPEZAR A PARTICIPAR
                        </a>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div class="row">
            Síguenos en nuestras redes sociales, para estar atento a futuros sorteos.
            Además anunciaremos el ganador por esos medios.
        </div>
    </asp:Panel>
    <div class="row">
        <h6 class="o-label"><span>REDES SOCIALES</span></h6>
    </div>
    <div class="col s12 social-buttons ">
        <div class="row">
            <div class="col s12">
                <a href="https://www.facebook.com/milkyoficial.ec" target="_blank" class="btn pulse waves-effect waves-light" style="background-color: #DBA088">
                    <i class="fab fa-facebook-square left"></i>
                    MILKY
                </a>
            </div>
        </div>
        <div class="row">
            <div class="col s12">
                <a href="https://www.facebook.com/AguaLMana/" target="_blank" class="pulse  btn waves-effect waves-light" style="background-color: #132399">
                    <i class="fab fa-facebook-square left"></i>
                    AGUA L'MANÁ
                </a>
            </div>
        </div>
        <div class="row">
            <div class="col s12">
                <a href="https://www.facebook.com/teverde.ec/" target="_blank" class="btn pulse waves-effect waves-light" style="background-color: #729437">
                    <i class="fab fa-facebook-square left"></i>
                    TÉ VERDE
                </a>
            </div>
        </div>
        <div class="row">
            <div class="col s12">
                <a href="https://www.facebook.com/TeChaOriental/" target="_blank" class="btn pulse waves-effect waves-light" style="background-color: #01D4DC">
                    <i class="fab fa-facebook-square left"></i>
                    TÉ CHA
                </a>
            </div>
        </div>
    </div>
</asp:Content>
