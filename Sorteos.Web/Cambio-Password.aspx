<%@ Page Title="Regreso a Clases Milky - Cambio de Contraseña" Language="C#" MasterPageFile="~/Auth.Master" AutoEventWireup="true" CodeBehind="Cambio-Password.aspx.cs" Inherits="Sorteos.Web.Cambio_Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <div class="row center-align">
        <asp:Panel ID="passwordCambiada" runat="server">
            <img src="/Content/images/confirmed.svg" style="max-height: 150px; margin-top: 20px;" alt="Activa" />
            <h5>Alerta</h5>
            <h6>La contraseña ya ha sido modificada</h6>
            <p>Ir al <a href="/Login">Login</a></p>
        </asp:Panel>
        <asp:Panel ID="noValida" runat="server">
            <img src="/Content/images/alert.svg" style="max-height: 150px; margin-top: 20px;" alt="Activa" />
            <h5>Error</h5>
            <h6>La petición de recuperación de contraseña ya expiro o no es correcta.</h6>
            <p>Ir al <a href="/Login">Login</a></p>
        </asp:Panel>

        <asp:Panel ID="formulario" runat="server">
            <h5>Cambio Contraseña</h5>
            <div class="row">
                <div class="input-field col s12">
                    <i class="fas fa-key prefix"></i>
                    <asp:TextBox ID="password" ClientIDMode="Static" runat="server" type="password" AutoCompleteType="Cellular" minlength="4" CssClass="icon-prefix validate" required="true" />
                    <label for="password" class="icon-prefix">Contraseña</label>
                </div>
                <asp:RequiredFieldValidator ErrorMessage="Ingrese la contraseña" CssClass="error-message" SetFocusOnError="true" ControlToValidate="password" runat="server" />
                <br />
                <asp:RegularExpressionValidator ErrorMessage="La contraseña debe tener mínimo 4 caractéres" CssClass="error-message" ValidationExpression="(^.{4,}$)" ControlToValidate="password" runat="server" />
            </div>
            <div class="row">
                <div class="input-field placeholded col s12">
                    <i class="fas fa-key prefix"></i>
                    <asp:TextBox ID="repeteatedPassword" ClientIDMode="Static" runat="server" type="password" minlength="4" CssClass="icon-prefix validate" required="true" />
                    <label for="repeteatedPassword" class="icon-prefix">Repetir Contraseña</label>
                </div>
                <asp:RequiredFieldValidator ErrorMessage="Contraseñas no coinciden" CssClass="error-message" SetFocusOnError="true" ControlToValidate="repeteatedPassword" runat="server" />
                <br />
                <asp:CompareValidator ErrorMessage="Contraseñas no coinciden" CssClass="error-message" ControlToValidate="repeteatedPassword" ControlToCompare="password" Operator="Equal" runat="server" />
            </div>
            <div class="row">
                <div class="col s12 align-right">
                    <button type="submit" class="waves-effect waves-light btn" runat="server" onserverclick="CambiarClave">
                        <i class="fas fa-key left"></i>
                        Cambiar Contraseña
                    </button>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
