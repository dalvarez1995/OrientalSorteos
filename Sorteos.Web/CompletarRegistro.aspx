<%@ Page Title="Completar Registro" Language="C#" MasterPageFile="~/Auth.Master" AutoEventWireup="true" CodeBehind="CompletarRegistro.aspx.cs" Inherits="Sorteos.Web.CompletarRegistro" Async="true" %>
<asp:Content ID="completarRegistroHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="completarLogo" ContentPlaceHolderID="logo" runat="server">
    <img id="userImage" runat="server" src="/Content/images/regreso-clases.jpeg" class="z-depth-2" style="object-fit: contain; height: 75px; width: 75px; border-radius: 15px" />
</asp:Content>
<asp:Content ID="completarRegistroBody" ContentPlaceHolderID="main" runat="server">
    <div class="row center-align">
        <h5>Completar Registro</h5>
        <p>Necesitamos estos campos adicionales para culminar tu registro</p>
    </div>
    <div class="row">
        <div class="input-field col s12">
            <asp:TextBox ID="cellNumber" ClientIDMode="Static" runat="server" AutoCompleteType="Cellular" CssClass="validate" required="true" />
            <label for="cellNumber" >Whatsapp</label>
        </div>
        <asp:RequiredFieldValidator Display="Dynamic" ErrorMessage="Ingrese su número de whatsapp" CssClass="error-message" SetFocusOnError="true" ControlToValidate="cellNumber" runat="server" />
        <asp:RegularExpressionValidator Display="Dynamic" ErrorMessage="Whatsapp no válido (10 digitos permitidos)" CssClass="error-message" ValidationExpression="(^[0-9]{9,10}$)" ControlToValidate="cellNumber" runat="server" />
    </div>
    <div class="row">
        <div class="input-field col s12">
            <input id="password" type="password" name="password" runat="server" autocomplete="on" minlength="4" class="validate" required="required" />
            <label for="password" >Contraseña</label>
        </div>
        <asp:RequiredFieldValidator Display="Dynamic" ErrorMessage="Ingrese la contraseña" CssClass="error-message" SetFocusOnError="true" ControlToValidate="password" runat="server" />
        <asp:RegularExpressionValidator Display="Dynamic" ErrorMessage="La contraseña debe tener mínimo 4 caractéres" CssClass="error-message" ValidationExpression="(^.{4,}$)" ControlToValidate="password" runat="server" />
    </div>
    <div class="row">
        <div class="input-field placeholded col s12">
            <input id="repeteatedPassword" name="repeated-password" type="password" runat="server" class="validate"  required="required" />
            <label for="repeteatedPassword" >Repetir Contraseña</label>
        </div>
        <asp:CompareValidator Display="Dynamic" ErrorMessage="Contraseñas no coinciden" CssClass="error-message" ControlToValidate="repeteatedPassword" ControlToCompare="password" Type="String" Operator="Equal" runat="server" />
    </div>
    <div class="row">
        <p>
      <label>
        <input id="aceptarCondiciones" type="checkbox" runat="server" />
        <span>Acepto los <a href="/Condiciones-Uso">Condiciones de uso</a> y he leído las <a href="/Politicas-Privacidad">Políticas de Privacidad</a></span>
      </label>
    </p>
    </div>
    <div class="row"></div>
    <div class="row">
        <div class="col s12"><button type="submit" class="waves-effect waves-light btn" runat="server" onserverclick="btnCompletarRegistro_Click">
            <i class="fas fa-sign-in-alt left"></i>
            Completar Registro</button>
        </div>
    </div>
</asp:Content>
