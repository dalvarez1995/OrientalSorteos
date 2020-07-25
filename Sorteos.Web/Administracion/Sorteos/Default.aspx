<%@ Page Title="Sorteos Oriental - Sorteos Registrados" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Sorteos.Web.Administracion.Sorteos.Default" %>
<asp:Content ID="sorteosHeader" ContentPlaceHolderID="header" runat="server">
     <webopt:BundleReference runat="server" Path="~/Content/css/datatables" />
</asp:Content>
<asp:Content ID="sorteosContent" ContentPlaceHolderID="content" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-12">
            <h2>Administración de Sorteos</h2>
            <a href="/Administracion/Sorteos/Nuevo"  class="btn btn-primary" ><i class="fa fa-plus m-r-sm"></i>Nuevo Sorteo</a>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row justify-content-center">
            <div class="col-xs-12 col-sm-12 col-md-10">
                <div class="ibox ">
                    <div class="ibox-title collapse-link">
                        <h5>Listado de Sorteos</h5>
                        <div class="ibox-tools">
                            <a>
                                <i class="fa fa-chevron-up"></i>
                            </a>

                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="table-responsive">
                                <table id="tblSorteos" class="table table-bordered hidden" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>EDITAR</th>
                                            <th>DESCRIPCION</th>
                                            <th>FECHA INICIO</th>
                                            <th>FECHA FIN</th>
                                            <th>ACTIVO</th>
                                            <th>FECHA CREACION</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="sorteosBody" ContentPlaceHolderID="body" runat="server">
    <%: Scripts.Render("~/bundles/datatables") %>

    <script>  

        $(document).ready(() => {
            drawDtSorteos();
        });

        function drawDtSorteos() {
            try {
                //target
                const tableID = 'tblSorteos';
                const dtImplName = 'RaffleDatatable';
                const tableOpts = {
                    rowId: "Id",
                    columns: [
                        {
                            data: null,
                            render: function (data, type, row) {
                                return `<a class='btn btn-primary'  href="/Administracion/Sorteos/Editar?pid=${data.Id}"><i  class="fa fa-edit"></i></a>`;
                            },
                            searchable: false,
                            orderable: false,
                            className: 'text-center',
                        },
                        { data: "Description", orderable: false, },
                        { data: "BeginDate", orderable: false, },
                        { data: "EndDate", orderable: false, },
                        {
                            data: "Active",
                            orderable: false,
                            className: 'text-center',
                            render: function (data, type, row) {
                                return `<i  class="fa fa-2x fa-${data ? 'check-circle' : 'times-circle'}" style="color:${ data ? 'green':'red'};"></i></a>`;
                            },
                        },
                        { data: "CreatedAt", orderable: false, },
                    ]
                };

                const promise = generarDT(tableID, dtImplName, {
                }, '/Service.asmx/GetDT', tableOpts);

                notifier.asyncBlock(promise, null, null, 'Cargando');

            } catch (e) {
                notifier.alert(e.message ? e.message : e)
            }
        };
    </script>
</asp:Content>
