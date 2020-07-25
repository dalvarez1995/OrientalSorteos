<%@ Page Title="Login - Regreso a Clases Milky" MetaDescription="Inicia Sesión o registrate para empezar a participar" Language="C#" MasterPageFile="~/Auth.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Sorteos.Web.Login" %>


<asp:Content ID="loginHead" ContentPlaceHolderID="head" runat="server">
     <meta name="robots" content="index"/>
</asp:Content>


<asp:Content ID="loginMain" ContentPlaceHolderID="main" runat="server">
    <div class="row center-align">
        <h4>Ingresar</h4>
    </div>
    <div class="row form-row">
        <div class="input-field col s12">
            <i class="fas fa-at prefix"></i>
            <asp:TextBox id="email" runat="server" type="email" CssClass="icon-prefix validate" />  
            <label for="email" class="icon-prefix">Correo electrónico</label>
        </div>
        <asp:RequiredFieldValidator ErrorMessage="Ingrese su email" CssClass="error-message" SetFocusOnError="true" ControlToValidate="email" runat="server" />
        <br />
        <asp:RegularExpressionValidator ErrorMessage="Correo electrónico no válido" CssClass="error-message" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$" ControlToValidate="email" runat="server" />
    </div>
    <div class="row form-row">
        <div class="input-field col s12">
            <i class="fas fa-key prefix"></i>
            <asp:TextBox id="password" runat="server" type="password" CssClass="icon-prefix validate" /> 
            <label for="password" class="icon-prefix">Contraseña</label>
        </div>
        <asp:RequiredFieldValidator ErrorMessage="Ingrese su contraseña" CssClass="error-message" SetFocusOnError="true" ControlToValidate="password" runat="server" />
        <br />
    </div>
    <a href="/Olvido-Password" class="change-password-link left-align">Olvido su contraseña?</a>
    <div class="row"></div>
    <div class="row">
        <div class="col s12">
            <button type="submit" class="waves-effect waves-light btn"  runat="server" onserverclick="btnLogin_Click">
                <i class="fas fa-sign-in-alt left"></i>
                Ingresar
            </button>
        </div>
    </div>

    <div class="row">
        <h6 class="o-label"><span>o</span></h6>
    </div>
    <div class="row">
        <div class="col s12">
            <asp:LinkButton
                runat="server"
                CausesValidation="false"
                OnClientClick="notifier.asyncBlock(new Promise((resolve, reject) => { }), null, null, 'Conectando');"
                OnClick="btnfacebookLogin_ServerClick"
                CssClass="facebook-button btn waves-effect waves-light">
                 <i class="fab fa-facebook-square left"></i>
                Ingresar con Facebook
            </asp:LinkButton>
        </div>
    </div>
    <div class="row">
        <span>No tienes cuenta aún?</span><a href="/Registrarse"> Crear cuenta</a>
    </div>
</asp:Content>
