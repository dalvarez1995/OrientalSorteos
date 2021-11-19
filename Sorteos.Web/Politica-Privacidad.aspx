<%@ Page Title="Politica de Privacidad" MetaKeywords="privacidad,datos,uso,politicas" MetaDescription ="El objetivo de esta Política de Privacidad es informarte sobre qué datos recogemos, por qué los recogemos" Language="C#" MasterPageFile="~/Auth.Master" AutoEventWireup="true" CodeBehind="Politica-Privacidad.aspx.cs" Inherits="Sorteos.Web.Politica_Privacidad" %>

<asp:Content ID="popHead" ContentPlaceHolderID="head" runat="server">
     <meta name="robots" content="index"/>
    <meta property="og:title" content="Políticas de Privacidad"/>
    <meta property="og:description" content="El objetivo de esta Política de Privacidad es informarte sobre qué datos recogemos, por qué los recogemos."/>
    <meta property="og:url" content="<%: $"{Sorteos.Services.AppSingleton.Instance.Sitio.BaseUrl}/Politica-Privacidad" %>"" />
</asp:Content>
<asp:Content ID="popMain" ContentPlaceHolderID="main" runat="server">
    <div class="col s12" >
        <div class="row center-align">
            <h4>Políticas de privacidad</h4>
        </div>
        <div id="popContent" class="row" runat="server"></div>
    </div>
</asp:Content>
