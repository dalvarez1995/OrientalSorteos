<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="Sitio.aspx.cs" Inherits="Sorteos.Web.Administracion.Sitio" %>
<asp:Content ID="sitioHeader" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="sitioContent" ContentPlaceHolderID="content" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-12">
            <h2>Editar Sitio</h2>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row justify-content-center">
            <div class="col-xs-12 col-sm-12 col-md-10">

                <div class="ibox ">
                    <div class="ibox-title collapse-link">
                        <h5>Formulario</h5>
                        <div class="ibox-tools">
                            <a>
                                <i class="fa fa-chevron-up"></i>
                            </a>

                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="form-group col-md-12">
                                <label class="font-normal">Política de privacidad</label>
                                <asp:TextBox ID="txtPoliticaPrivacidad" ClientIDMode="Static" runat="server" class="form-control" Rows="10" TextMode="MultiLine" type="text"></asp:TextBox>

                            </div>
                            <div class="form-group col-md-12">
                                <label class="font-normal">Condiciones del servicio </label>
                                <asp:TextBox ID="txtCondicionesServicio" ClientIDMode="Static" runat="server" class="form-control" Rows="10" TextMode="MultiLine" type="text"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-12 justify-content-center text-center">
                                <button id="btnGuardar" type="submit" onserverclick="btnGuardar_ServerClick" runat="server" class="btn btn-primary"  ><i class="fa fa-save m-r-sm"></i>Guardar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="sitioBody" ContentPlaceHolderID="body" runat="server">
    <script src="https://cdn.tiny.cloud/1/aftjeq0dcidzbidqqx1n1wzmbvyhci8f9wl5zrwcx20r3g9s/tinymce/5/tinymce.min.js" referrerpolicy="origin"></script>

    <script>
        tinymce.init({
            selector: 'textarea',
            plugins: 'a11ychecker advcode casechange formatpainter linkchecker autolink lists checklist media mediaembed pageembed permanentpen powerpaste table advtable tinycomments tinymcespellchecker',
            toolbar: 'a11ycheck addcomment showcomments casechange checklist code formatpainter pageembed permanentpen table',
            toolbar_mode: 'floating',
            tinycomments_mode: 'embedded',
            tinycomments_author: 'Author name',
        });
    </script>
</asp:Content>
