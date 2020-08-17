<%@ Page Title="Regreso a Clases Milky - Activar Cuenta" Language="C#" MasterPageFile="~/Auth.Master" AutoEventWireup="true" CodeBehind="ActivarCuenta.aspx.cs" Inherits="Sorteos.Web.ActivarCuenta" Async="true" %>

<asp:Content ID="activarCuentaHeader" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="activarCuentaBody" ContentPlaceHolderID="main" runat="server">
    <h5>Activar Cuenta</h5>
    <p>Su cuenta esta pendiente de activación, ingrese el código que le enviamos a su correo.</p>
    <div class="row center-align">
        <%--<asp:Panel ID="cuentaActivada" runat="server">
            <img src="/Content/images/confirmed.svg" style="max-height: 150px;margin-top:20px;" alt="Activa" />
            <h5>Exito</h5>
            <h6>Su cuenta ha sido validada exitosamente</h6>
            <p>Ir al <a href="/Login">Login</a></p>
        </asp:Panel>
        <asp:Panel ID="noValida" runat="server">
            <img src="/Content/images/alert.svg" style="max-height: 150px;margin-top:20px;" alt="Activa" />
            <h5>Error</h5>
            <h6>La petición de activación no es correcta</h6>
            <p>Ir al <a href="/Login">Login</a></p>
        </asp:Panel>--%>

        <div class="row">
            <div class="input-field col s12">
                <i class="fas fa-key prefix"></i>
                <asp:TextBox ID="otp_code" ClientIDMode="Static" runat="server" CssClass="icon-prefix validate" required="true" />
                <label for="otp_code" class="icon-prefix">Codigo OTP</label>
            </div>
            <asp:RequiredFieldValidator ErrorMessage="Ingrese el código enviado a su correo electrónico" Display="Dynamic" CssClass="error-message" SetFocusOnError="true" ControlToValidate="otp_code" runat="server" />
        </div>
        <div class="row"></div>
        <div class="row">
            <div class="col s12">
                <button type="submit" class="waves-effect waves-light btn" runat="server" onserverclick="Activar">
                    <i class="fas fa-check-circle left"></i>
                    Activar
                </button>
            </div>
        </div>
        <div class="row">
            <div class="col s12">
                <asp:LinkButton
                    runat="server"
                    CausesValidation="false"
                    OnClick="Reenviar"
                    CssClass="waves-effect waves-light btn">
                 <i class="fas fa-paper-plane left"></i>
                Reenviar
                </asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
