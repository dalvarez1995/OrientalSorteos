<%@ Page Title="Sorteos Oriental - Editar Sorteos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Editar.aspx.cs" ValidateRequest="false" Inherits="Sorteos.Web.Administracion.Sorteos.Editar" %>

<asp:Content ID="sorteosEditHeader" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="sorteosEditContent" ContentPlaceHolderID="content" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-12">
            <h2>Editar Sorteo</h2>
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
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="font-normal">Descripcion</label>
                                    <div class="input-group date">
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        <asp:TextBox ID="txtDescripcion" ClientIDMode="Static" runat="server" class="form-control" type="text"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row text-center justify-content-center">
                                    <asp:RequiredFieldValidator ErrorMessage="Ingrese la descripcion" Style="color: red" SetFocusOnError="true" ControlToValidate="txtDescripcion" runat="server" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="font-normal">Fecha Inicio</label>
                                    <div class="input-group date">
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        <asp:TextBox ID="txtFechaInicio" ClientIDMode="Static" runat="server" class="form-control" type="date"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row text-center justify-content-center">
                                    <asp:RequiredFieldValidator ErrorMessage="Ingrese la fecha de inicio del sorteo" Style="color: red" SetFocusOnError="true" ControlToValidate="txtFechaInicio" runat="server" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="font-normal">Fecha Fin</label>
                                    <div class="input-group date">
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        <asp:TextBox ID="txtFechaFin" ClientIDMode="Static" runat="server" class="form-control" type="date"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row text-center justify-content-center">
                                    <asp:RequiredFieldValidator ErrorMessage="Ingrese la fecha de finalización del sorteo" Style="color: red" SetFocusOnError="true" ControlToValidate="txtFechaFin" runat="server" />
                                </div>
                            </div>
                            <div class="form-group col-md-12">
                                <label class="font-normal">Contenido</label>
                                <asp:TextBox ID="txtContenido" ClientIDMode="Static" runat="server" class="form-control" Rows="10" TextMode="MultiLine" type="text"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-12">
                                <label class="font-normal">Activar este sorteo (Solo puede existir un sorteo activo la vez.)</label>
                                <asp:CheckBox ID="chkActivo" CssClass="form-control" runat="server" />
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
<asp:Content ID="sorteosEditBody" ContentPlaceHolderID="body" runat="server">
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
