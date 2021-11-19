<%@ Page Title="Condiciones de Uso" MetaKeywords="condiciones,terminos,uso,reglamento" MetaDescription="Nuestros terminos y condiciones de uso estan expuestos aqui." Language="C#" MasterPageFile="~//Auth.Master" AutoEventWireup="true" CodeBehind="Condiciones-Uso.aspx.cs" Inherits="Sorteos.Web.Condiciones_Uso" %>
<asp:Content ID="tosHead" ContentPlaceHolderID="head" runat="server">
     <meta name="robots" content="index"/>
    <meta property="og:title" content="Condiciones de Uso"/>
    <meta property="og:description" content="Nuestros terminos y condiciones de uso estan expuestos aqui."/>
    <meta property="og:url" content="<%: $"{Sorteos.Services.AppSingleton.Instance.Sitio.BaseUrl}/Condiciones-Uso" %>" />
</asp:Content>
<asp:Content ID="tosMain" ContentPlaceHolderID="main" runat="server">
    <div class="col s12" >
        <div class="row">
            <h4>Condiciones de uso</h4>
        </div>
        <div id="tosContent" class="row" runat="server"></div>
    </div>
</asp:Content>