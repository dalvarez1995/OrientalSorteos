<%@ Page Title="Regreso a Clases Milky - Olvido Password" Language="C#" MasterPageFile="~/Auth.Master" AutoEventWireup="true" Async="true" CodeBehind="Olvido-Password.aspx.cs" Inherits="Sorteos.Web.Olvido_Password" %>
<asp:Content ID="olvidoPasswordHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="olvidoPasswordMain" ContentPlaceHolderID="main" runat="server">
    <div class="row center-align">
        <h4>Recuperar Contraseña</h4>
    </div>
    <div class="row">
        <p>
            A continuación ingrese la dirección de correo electrónico asociado a su cuenta.
            En caso de haberse registrado mediante facebook, ingrese su correo registrado en su cuenta de facebook.
        </p>
    </div>
    <div class="row form-row">
        <div class="input-field col s12">
            <i class="fas fa-at prefix"></i>
            <asp:TextBox id="email" runat="server" type="email" CssClass="icon-prefix validate" />  
            <label for="email" class="icon-prefix">Correo electrónico</label>
        </div>
        <asp:RequiredFieldValidator ErrorMessage="Ingrese su email" CssClass="error-message" Display="Dynamic" SetFocusOnError="true" ControlToValidate="email" runat="server" />
        <asp:RegularExpressionValidator ErrorMessage="Correo electrónico no válido" CssClass="error-message" Display="Dynamic" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$" ControlToValidate="email" runat="server" />
    </div>
    <div class="row">
        <p>
            Se enviara un correo electrónico con las instrucciones para recuperar su contraseña.
        </p>
    </div>
    <div class="row"></div>
    <div class="row">
        <div class="col s12">
            <button type="submit" class="waves-effect waves-light btn"  runat="server" onserverclick="Solicitar">
                <i class="fas fa-paper-plane left"></i>
                Solicitar
            </button>
        </div>
    </div>
    <div class="row"></div>
    <div class="row">
        <div class="col s12">
            <a class="btn waves-effect waves-light" runat="server" causesvalidation="false" href="/Login">
                <i class=" fas fa-sign-out-alt left"></i>
                VOLVER AL LOGIN
            </a>
        </div>
    </div>
</asp:Content>
