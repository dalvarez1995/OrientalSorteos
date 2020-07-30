<%@ Page Title="Sorteos - Compras" Language="C#" MasterPageFile="~/Admin.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="Sorteos.Web.Administracion.Compras" %>

<asp:Content ID="compraHeader" ContentPlaceHolderID="header" runat="server">
    <webopt:BundleReference runat="server" Path="~/Content/css/datatables" />
</asp:Content>
<asp:Content ID="compraContent" ContentPlaceHolderID="content" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-12">
            <h2>Compras Registradas</h2>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row justify-content-center">
            <div class="col-xs-12 col-sm-12 col-md-10">
                <div class="ibox ">
                    <div class="ibox-title collapse-link">
                        <h5>Filtros</h5>
                        <div class="ibox-tools">
                            <a>
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="form-group col-md-6">
                                <label class="font-normal">Fecha Desde</label>
                                <div class="input-group date">
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    <asp:TextBox ID="txtFechaDesde" ClientIDMode="Static" runat="server" class="form-control" type="date"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group col-md-6">
                                <label class="font-normal">Fecha Hasta</label>
                                <div class="input-group date">
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    <asp:TextBox ID="txtFechaHasta" ClientIDMode="Static" runat="server" class="form-control" type="date"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-6">
                                <label class="font-normal">Tipo</label>
                                <div class="input-group date">
                                    <span class="input-group-addon"><i class="fa fa-file"></i></span>
                                    <asp:DropDownList ID="cboTipoCompra" ClientIDMode="Static" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group col-md-6">
                                <label class="font-normal">Cliente (email)</label>
                                <div class="input-group date">
                                    <span class="input-group-addon"><i class="fa fa-at"></i></span>
                                    <asp:TextBox ID="txtCliente" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-6">
                                <label class="font-normal">Sorteo</label>
                                <div class="input-group date">
                                    <span class="input-group-addon"><i class="fa fa-file"></i></span>
                                    <asp:DropDownList ID="cboSorteos" ClientIDMode="Static" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group col-md-6">
                                <label class="font-normal">Marca</label>
                                <div class="input-group date">
                                    <span class="input-group-addon"><i class="fa fa-copyright"></i></span>
                                    <asp:DropDownList ID="cboMarcas" ClientIDMode="Static" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <label class="font-normal">Provincia</label>
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-map"></i></span>
                                            <asp:DropDownList ID="cboProvincias" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="cboProvincias_SelectedIndexChanged" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label class="font-normal">Ciudad</label>
                                        <div class="input-group date">
                                            <span class="input-group-addon"><i class="fa fa-map"></i></span>
                                            <asp:DropDownList ID="cboCiudades" ClientIDMode="Static" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="row">
                            <div class="form-group col-md-6">
                                <label class="font-normal">Estado</label>
                                <div class="input-group date">
                                    <span class="input-group-addon"><i class="fa fa-file"></i></span>
                                    <asp:DropDownList ID="cboEstados" ClientIDMode="Static" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row justify-content-center">
                            <button class="btn btn-primary" onclick="dtOrden();return false;"><i class="fa fa-search m-r-sm"></i>Buscar</button>
                        </div>
                    </div>
                </div>

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
                                <table id="tblCompras" class="table table-bordered hidden" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>ESTADO</th>
                                            <th>SORTEO</th>
                                            <th>NOMBRE CLIENTE</th>
                                            <th>TIPO</th>
                                            <th>MARCA</th>
                                            <th>LOTE</th>
                                            <th>CANTIDAD</th>
                                            <th>CIUDAD</th>
                                            <th>PROVINCIA</th>
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
    <button id="btnExportar" class="btn btn-secondary" runat="server" tabindex="0" style="display: none" aria-controls="tblOrdenes" type="button" onclick="if (!dtExportExcelServerSide('tblCompras')){ return;}" onserverclick="exportarExcel">
        <i class="fas fa-file-excel m-r-sm"></i><span>Exportar a Excel</span></button>

    <div id="modal-image" class="modal">
        <span class="close">&times;</span>
        <img class="modal-content" id="img-modal">
        <div id="caption"></div>
    </div>
</asp:Content>
<asp:Content ID="compraBody" ContentPlaceHolderID="body" runat="server">
    <%: Scripts.Render("~/bundles/datatables") %>

    <script>  

        $(document).ready(() => {
            drawDtCompras();
        });


        function throwImageModal(imgSrc, alt) {
            let modal = document.getElementById("modal-image");
            let modalImg = document.getElementById("img-modal");
            let captionText = document.getElementById("caption");
            modal.style.display = "block";
            modalImg.src = imgSrc;
            captionText.innerHTML = alt;
            let span = document.getElementsByClassName("close")[0];
            span.onclick = function () {
                modal.style.display = "none";
            }
        }

        function drawDtCompras() {
            try {
                //target
                const tableID = 'tblCompras';
                const dtImplName = 'PurchaseDatatable';
                const tableOpts = {
                    rowId: "Id",
                    order: [[10, 'asc']],
                    columns: [
                        {
                            data: null,
                            width:'60px',
                            className:'text-center',
                            render: function (data) {
                                const createdAt = new Date(data.FechaCreacion.toString());
                                const ye = new Intl.DateTimeFormat('es', { year: 'numeric' }).format(createdAt);
                                const mo = new Intl.DateTimeFormat('es', { month: 'long' }).format(createdAt);
                                const da = new Intl.DateTimeFormat('es', { day: '2-digit' }).format(createdAt);
                                let restoreButton = `</br><button title="Restaurar Compra" class="btn btn-primary mt-2" onclick="changePurchaseStatus(${data.Id},10);return false;" ><i class="fa fa-sync"></i></button>`;
                                return `<button class="btn btn-primary" 
                                                title="Ver Imagen"
                                                onclick="throwImageModal('/Content/images/invoices/${data.FacturaPath}','${data.Sorteo} - ${data.NombreCliente}, ${da} de ${mo} del ${ye}');return false;">
                                                <i  class="fa fa-eye">
                                                </i>
                                        </button>'${ data.Estado == 'Invalido' ? restoreButton : ''}`
                            },
                            searchable: false,
                            orderable: false,
                        },
                        {
                            data: "Estado",
                            className: 'text-center',
                            render: function (data) {
                                switch (data) {
                                    case 'Pendiente':
                                        return `<i  class="fa fa-2x fa-user-clock" title="Pendiente Depuración"></i>`;
                                        break;
                                    case 'Valido':
                                        return `<i  class="fa fa-2x fa-check-circle" style="color:green;" title="Válido"></i>`;
                                        break;
                                    case 'Invalido':
                                        return `<i  class="fa fa-2x fa-times-circle" style="color:red;" title="Ïnválido"></i>`;
                                        break;
                                }
                            }
                        },
                        { data: "Sorteo", orderable: false, },
                        { data: "NombreCliente", orderable: false, },
                        { data: "Tipo", orderable: false, },
                        { data: "Marca", orderable: false, },
                        { data: "Lote" },
                        { data: "Cantidad" },
                        { data: "Ciudad", orderable: false, },
                        { data: "Provincia", orderable: false, },
                        {
                            "data": "FechaCreacion",
                            render: function (data) {
                                const date = new Date(data.toString());
                                return date.format("dd/MM/yyyy HH:mm:ss");
                            }
                        },
                    ]
                };


                const promise = generarDT(tableID, dtImplName, {
                    fechaDesde: $('#txtFechaDesde').val(),
                    fechaHasta: $('#txtFechaHasta').val(),
                    sorteoId: $('#cboSorteos').val(),
                    marcaId: $('#cboMarcas').val(),
                    provinciaId: $('#cboProvincias').val(),
                    ciudadId: $('#cboCiudades').val(),
                    tipo: $('#cboTipoCompra').val(),
                    cliente: $('#txtCliente').val(),
                    estado: $('#cboEstados').val()

                }, '/Service.asmx/GetDT', tableOpts, () => { dtInjectServerButton('content_btnExportar', 'tblCompras'); });

            } catch (e) {
                notifier.alert(e.message ? e.message : e)
            }
        };

        async function changePurchaseStatus(purchaseId,status) {
            try {
                await ajaxPOST('/Service.asmx/ChangePurchaseStatus',
                    {
                        purchaseId,
                        status
                    })
                notifier.success('Compra restaurada correctamente');
                $('#tblCompras').DataTable().ajax.reload();

            } catch (e) {
                error(e.message);
            }
        }
    </script>
</asp:Content>
