<%@ Page Title="Registrarse" MetaKeywords="sorteo,concurso,regalo,premio,milky,oriental" Language="C#" MasterPageFile="~/Auth.Master" AutoEventWireup="true" CodeBehind="Registrarse.aspx.cs" Inherits="Sorteos.Web.Registrarse" Async="true" %>


<asp:Content ID="registrarseHead" ContentPlaceHolderID="head" runat="server">
     <meta name="robots" content="index"/>
</asp:Content>


<asp:Content ID="registrarseMain" ContentPlaceHolderID="main" runat="server">

    <div class="row center-align" style="margin-bottom: 10px;">
        <h5>Registrate</h5>
    </div>
    <div class="row">
        <div class="col s12">
            <asp:LinkButton
                runat="server"
                CausesValidation="false"
                OnClick="btnfacebookRegister_ServerClick"
                CssClass="facebook-button btn waves-effect waves-light">
                 <i class="fab fa-facebook-square left"></i>
                Registrarse con Facebook
            </asp:LinkButton>
        </div>
    </div>
    <div class="row">
        <h6 class="o-label"><span>o</span></h6>
    </div>
    <div class="row">
        <div class="input-field col s12">
            <i class="fas fa-signature prefix"></i>
            <asp:TextBox ID="firstName" type="text" ClientIDMode="Static" AutoCompleteType="FirstName" CssClass="icon-prefix validate" runat="server" required="true" />
            <label for="firstName" class="icon-prefix">Nombre</label>
        </div>
        <asp:RequiredFieldValidator ErrorMessage="Ingrese su nombre" CssClass="error-message" SetFocusOnError="true" ControlToValidate="firstName" runat="server" />
        <br />
        <asp:RegularExpressionValidator EnableClientScript="true" ErrorMessage="El nombre debe tener mínimo 2 caractéres" CssClass="error-message" ValidationExpression="(^.{2,}$)" ControlToValidate="firstName" runat="server" />
    </div>
    <div class="row">
        <div class="input-field col s12">
            <i class="fas fa-signature prefix"></i>
            <asp:TextBox ID="lastName" type="text" ClientIDMode="Static" AutoCompleteType="LastName" CssClass="icon-prefix validate" runat="server" required="true" />
            <label for="lastName" class="icon-prefix">Apellido</label>
        </div>
        <asp:RequiredFieldValidator ErrorMessage="Ingrese su apellido" CssClass="error-message" SetFocusOnError="true" ControlToValidate="lastName" runat="server" />
        <br />
        <asp:RegularExpressionValidator EnableClientScript="true" ErrorMessage="El apellido debe tener mínimo 2 caractéres" CssClass="error-message" ValidationExpression="(^.{2,}$)" ControlToValidate="lastName" runat="server" />
    </div>
    <div class="row">
        <div class="input-field col s12">
            <i class="fas fa-at prefix"></i>
            <asp:TextBox ID="email" type="email" ClientIDMode="Static" runat="server" AutoCompleteType="Email" CssClass="icon-prefix validate" required="true" />
            <label for="email" class="icon-prefix">Correo electrónico</label>
        </div>
        <asp:RequiredFieldValidator ErrorMessage="Ingrese su correo electrónico" CssClass="error-message" SetFocusOnError="true" ControlToValidate="email" runat="server" />
        <br />
        <asp:RegularExpressionValidator ErrorMessage="Correo electrónico no válido" CssClass="error-message" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$" ControlToValidate="email" runat="server" />

    </div>
    <div class="row">
        <div class="input-field col s12">
            <i class="fab fa-whatsapp prefix"></i>
            <asp:TextBox ID="cellNumber" ClientIDMode="Static" runat="server" AutoCompleteType="Cellular" CssClass="icon-prefix validate" required="true" />
            <label for="cellNumber" class="icon-prefix">Whatsapp</label>
        </div>
        <asp:RequiredFieldValidator ErrorMessage="Ingrese su teléfono" CssClass="error-message" SetFocusOnError="true" ControlToValidate="cellNumber" runat="server" />
        <br />
        <asp:RegularExpressionValidator ErrorMessage="Teléfono no válido (9-10 digitos permitidos)" CssClass="error-message" ValidationExpression="(^[0-9]{9,10}$)" ControlToValidate="cellNumber" runat="server" />
    </div>
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
        <div class="col s12">Se enviara un correo de verificación para poder activar su cuenta.</div>
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
        <div class="col s6"><a href="Login">Volver al login</a></div>
        <div class="col s6 align-right">
            <button type="submit" class="waves-effect waves-light btn" runat="server" onserverclick="btnRegistrarse_Click">
                <i class="fas fa-arrow-circle-right left"></i>
                Registrarse
            </button>
        </div>
    </div>
</asp:Content>
