<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Depurar.aspx.cs" Inherits="Sorteos.Web.Administracion.Depurar" %>

<asp:Content ID="depurarHeader" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="depurarContent" ContentPlaceHolderID="content" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-12">
            <h2>Depurar Compras Registradas</h2>
            <hr />
            <div class="form-group col-md-6">
                <label class="font-normal">Sorteo</label>
                <div class="input-group date">
                    <span class="input-group-addon"><i class="fa fa-file"></i></span>
                    <asp:DropDownList ID="cboSorteos" ClientIDMode="Static"  AutoPostBack="true" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row justify-content-center">
            <div class="col-xs-12 col-sm-12 col-md-12">
                <asp:Panel runat="server" ID="pnlSelectRaffle" CssClass="text-center">
                    <img src="/Content/images/choose-option.svg" style="height:300px;" />
                    <h3>Seleccione un sorteo</h3>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlNoPendingPurchases" CssClass="text-center">
                    <img src="/Content/images/all-done.svg" style="height:300px;" />
                    <h3>No hay compras pendientes</h3>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlDebug">
                    <div id="cardPurchase" class="card mx-auto" style="max-width: 400px;margin-bottom:100px;">
                        <img id="imgCompra" src="" class="card-img-top" alt="">
                        <div class="card-body">
                            <h3 id="txtUsuario" class="card-title text-center">Card title</h3>
                            <p id="txtFecha" class="card-text text-center"></p>
                        </div>
                        <ul class="list-group list-group-flush">
                            <li class="list-group-item d-flex"><b>Lote:</b><span id="txtLote" style="margin-left:auto"></span></li>
                            <li class="list-group-item d-flex"><b>Tipo Compra:</b><span id="txtTipoCompra" style="margin-left:auto"></span></li>
                            <li class="list-group-item d-flex"><b>Ciudad:</b><span id="txtCiudad" style="margin-left:auto"></span></li>
                            <li class="list-group-item d-flex"><b>Provincia:</b><span id="txtProvincia" style="margin-left:auto"></span></li>
                        </ul>
                        <div class="card-body">
                            <button class="btn btn-info" onclick="changePurchaseStatus(20);return false;"><i class="fa fa-thumbs-up"></i> Válido</button>
                            <button class="btn btn-danger" onclick="changePurchaseStatus(30);return false;"><i class="fa fa-thumbs-down"></i> No Válido</button>
                        </div>
                    </div>
                    <script>
                        $(document).ready(() => {
                            loadNextPurchase();
                        })


                        async function loadNextPurchase(){
                            try {
                                let { purchase } = await ajaxPOST('/Service.asmx/GetNextPendingPurchase',
                                    {
                                        raffleId: $('#cboSorteos').val()
                                    })
                                if (purchase) {
                                    const createdAt = new Date(purchase.CreatedAt);
                                    const ye = new Intl.DateTimeFormat('es', { year: 'numeric' }).format(createdAt);
                                    const mo = new Intl.DateTimeFormat('es', { month: 'long' }).format(createdAt);
                                    const da = new Intl.DateTimeFormat('es', { day: '2-digit' }).format(createdAt);

                                    $('#cardPurchase').data('Id', purchase.Id);
                                    $('#imgCompra').attr('src', `/Content/images/invoices/${purchase.InvoicePath}`);
                                    $('#txtUsuario').text(purchase.User.FullName)
                                    $('#txtFecha').text(`${da} de ${mo} del ${ye}`);
                                    $('#txtLote').text(purchase.Lote);
                                    $('#txtTipoCompra').text(purchase.Type);
                                    $('#txtCiudad').text(purchase.City);
                                    $('#txtProvincia').text(purchase.State);
                                } else {
                                    __doPostBack();
                                }
                            } catch (e) {
                                error(e.message);
                            }
                        }

                        async function changePurchaseStatus(status) {
                            try {
                                await ajaxPOST('/Service.asmx/ChangePurchaseStatus',
                                    {
                                        purchaseId: $('#cardPurchase').data('Id'),
                                        status
                                    })
                                if (status == 20)
                                    notifier.success('Compra marcada como válida correctamente');
                                else
                                    notifier.success('Compra marcada como inválida correctamente.');
                                loadNextPurchase();
                                
                            } catch (e) {
                                error(e.message);
                            }
                        }
                    </script>

                </asp:Panel>
               
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="depurarBody" ContentPlaceHolderID="body" runat="server">
</asp:Content>
