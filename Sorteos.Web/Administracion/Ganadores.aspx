<%@ Page Title="Ganadores - Administración" Language="C#" MasterPageFile="~/Admin.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Ganadores.aspx.cs" Inherits="Sorteos.Web.Administracion.Ganadores" %>

<asp:Content ID="ganadoresHeader" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="ganadoresContent" ContentPlaceHolderID="content" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-12">
            <h2>Ganadores</h2>
            <hr />
            <div class="form-group col-md-6">
                <label class="font-normal">Sorteo</label>
                <div class="input-group date">
                    <span class="input-group-addon"><i class="fa fa-file"></i></span>
                    <asp:DropDownList ID="cboSorteos" ClientIDMode="Static" AutoPostBack="true" runat="server" class="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row justify-content-center">
            <div class="col-xs-12 col-sm-12 col-md-12">
                <asp:Panel runat="server" ID="pnlSelectRaffle" CssClass="text-center">
                    <img src="/Content/images/choose-option.svg" style="height: 300px;" />
                    <h1>Seleccione un sorteo</h1>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlFinishedRaffle" CssClass="text-center">
                    <img src="/Content/images/winner_.svg" style="height: 300px;" />
                    <h1>Sorteo Finalizado</h1>
                    <div class="row">
                        <div class="table-responsive">
                            <table id="tblGanadoresFinalizado" class="table table-bordered hidden" style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>NOMBRE COMPLETO</th>
                                        <th>EMAIL</th>
                                        <th>TELEFONO</th>
                                    </tr>
                                </thead>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlWinnerSelection" CssClass="text-center">
                    <div class="col-md-12">
                        <div class="ibox ">
                            <div class="ibox-title collapse-link">
                                <h5>Elección de Ganadores</h5>
                                <div class="ibox-tools">
                                    <a>
                                        <i class="fa fa-chevron-up"></i>
                                    </a>
                                </div>
                            </div>
                            <div id="winnerDrawContainer" class="ibox-content">
                                <div class="col-md-12">
                                    <div class="row topbox">
                                        <div class="col-md-12 rollbox">
                                            <div class="line"></div>
                                            <table>
                                                <tr id="loadout">
                                                </tr>
                                            </table>
                                        </div>
                                    </div>

                                    <div class="row mb-4">
                                        <div class="col-md-12">
                                            <button id="roll" class="btn btn-primary" onclick="drawWinner(); return false;">
                                                <i class="fa fa-star"></i>
                                                Sortear
                                            </button>
                                            <button id="btnFinalize" class="btn btn-primary" onclick="__doPostBack(); return false;" style="display:none">
                                                <i class="fa fa-step-forward"></i>
                                                Finalizar
                                            </button>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h3 style="text-align: start;">Participantes:</h3>
                                            <textarea id="txtParticipants" runat="server" rows="8" class="form-control inputbox" readonly></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="ibox ">
                            <div class="ibox-title collapse-link">
                                <h3>Ganadores</h3>
                                <div class="ibox-tools">
                                    <a>
                                        <i class="fa fa-chevron-up"></i>
                                    </a>
                                </div>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <div class="table-responsive">
                                        <table id="tblGanadores" class="table table-bordered hidden" style="width: 100%">
                                            <thead>
                                                <tr>
                                                    <th>VER COMPRAS</th>
                                                    <th>NOMBRE COMPLETO</th>
                                                    <th>EMAIL</th>
                                                    <th>TELEFONO</th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <%--Purchases Modal--%>
        <div id="modal-purchases" class="modal" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h3 class="modal-title"></h3>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-12">
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
                                                    <th>FECHA REGISTRO</th>
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
        </div>
    </div>
    <%--Image Modal--%>
    <div id="modal-image" class="modal">
        <span class="close" onclick="$('#modal-image').hide()">&times;</span>
        <img class="modal-content" id="img-modal">
        <div id="caption"></div>
    </div>
    <button id="btnExportar" class="btn btn-secondary" runat="server" tabindex="0" style="display: none" aria-controls="tblOrdenes" type="button" onclick="if (!dtExportExcelServerSide('tblCompras')){ return;}" onserverclick="exportarExcel">
        <i class="fas fa-file-excel m-r-sm"></i><span>Exportar a Excel</span></button>
</asp:Content>
<asp:Content ID="ganadoresBody" ContentPlaceHolderID="body" runat="server">


    <%: Scripts.Render("~/bundles/datatables") %>

    <div class="fireworks">
    </div>
    <div id="winner-modal" class="modal" style="z-index: 2003;">
        <div class="modal-content">
            <div class="message-wrapper text-center">
                <h1></h1>
                <p>Has sido seleccionado como ganador!</p>
                <button class="btn btn-primary" onclick="closeWinnerOverlay();return false;">Continuar</button>
            </div>

        </div>
    </div>
    <script src="/Scripts/jquery/jquery.fireworks.js"></script>
    <script>

        var fireworkInstance = {};

        function closeWinnerOverlay() {
            $('#winner-modal').hide();
            fireworkInstance.stop();
            $('.fireworks').html('');
        }

        async function showWinnerOverlay(winnerId, winnerFullName) {
            try {
                const { ok, next } = await ajaxPOST('/Service.asmx/AddWinnerToRaffle', {
                    raffleId: $('#cboSorteos').val(),
                    winnerId
                })

                if (!next) {
                    $('#roll').hide();
                    $('#btnFinalize').show();
                }

                if (ok) {
                    $('#tblGanadores').DataTable().ajax.reload();
                    fireworkInstance = $('.fireworks').fireworks({ sound: true, opacity: 0.9, width: '100%', height: $('#page-wrapper').innerHeight() });
                    $('#winner-modal h1').text(`Felicidades ${winnerFullName}!`)
                    $('#winner-modal').show();
                }

            } catch (e) {
                error(e);
            }
        }

        function drawWinner() {
            let users = [],
                shuffled = [],
                loadout = $("#loadout"),
                insert_times = 30,
                duration_time = 10000;
            $('body').addClass('mini-navbar');
            users = [];
            let lines = $('textarea').val().split('\n');
            if (lines.length < 2) {
                warning('No hay suficientes participantes.');
                return;
            }
            for (let i = 0; i < lines.length; i++) {
                if (lines[i].length > 0) {
                    let currentLine = lines[i];
                    let userFullName = currentLine.split('-')[0];
                    let chances = currentLine.split('-')[1];
                    let userId = currentLine.split('-')[2];
                    for (let j = 0; j < chances; j++) {
                        users.push(`${userFullName}-${userId}`);
                    }

                }
            }
            $("#roll").attr("disabled", true);
            let scrollsize = $('.rollbox').innerWidth(),
                diff = 0;
            $(loadout).html("");
            loadout.css("left", "100%");
            if (users.length < 10) {
                insert_times = 20;
                duration_time = 5000;
            } else {
                insert_times = 10;
                duration_time = 10000;
            }
            for (let times = 0; times < 1; times++) {
                shuffled = users;
                shuffled.shuffle();
                for (let i = 0; i < users.length; i++) {
                    let userFullName = shuffled[i].split('-')[0];
                    let userId = shuffled[i].split('-')[1];
                    loadout.append(`<td data-id="${userId}"><div class="roller"><div>${userFullName}</div></div></td>`);
                    scrollsize = scrollsize + 192;
                }
            }

            diff = Math.round(scrollsize / 2);
            diff = randomEx(diff - 300, diff + 300);
            $("#loadout").animate({
                left: "-=" + diff
            }, duration_time, function () {
                $("#roll").attr("disabled", false);
                let center = window.innerWidth / 2;
                $('#loadout').children('td').each(function () {
                    $('body').addClass('mini-navbar');
                    if ($(this).offset().left < center && $(this).offset().left + 185 > center) {
                        let winnerFullName = $(this).children().text();
                        let winnerId = $(this).data('id');
                        showWinnerOverlay(winnerId, winnerFullName);
                    }
                });
            });
        }

        function drawDtWinnersFinalized() {
            try {
                //target
                const tableID = 'tblGanadoresFinalizado';
                const dtImplName = 'WinnerDatatable';
                const tableOpts = {
                    rowId: "Id",
                    dom: 't',
                    columns: [
                        { data: "FullName", orderable: false, },
                        { data: "Email", orderable: false, },
                        { data: "Whatsapp", orderable: false, },
                    ]
                };

                generarDT(tableID, dtImplName, {
                }, '/Service.asmx/GetDT', tableOpts);

            } catch (e) {
                notifier.alert(e.message ? e.message : e)
            }
        };


        function drawDtWinners() {
            try {
                //target
                const tableID = 'tblGanadores';
                const dtImplName = 'WinnerDatatable';
                const tableOpts = {
                    rowId: "Id",
                    dom: 't',
                    columns: [
                        {
                            data: null,
                            render: function (data, type, row) {
                                return `<button class='btn btn-primary'  onclick="throwPurchasesModal(${data.Id},'${data.FullName}');return false;"><i  class="fa fa-edit"></i></button>`;
                            },
                            searchable: false,
                            orderable: false,
                            width: '100px',
                            className: 'text-center',
                        },
                        { data: "FullName", orderable: false, },
                        { data: "Email", orderable: false, },
                        { data: "Whatsapp", orderable: false, },
                    ]
                };

                generarDT(tableID, dtImplName, {
                }, '/Service.asmx/GetDT', tableOpts);

            } catch (e) {
                notifier.alert(e.message ? e.message : e)
            }
        };

        function throwPurchasesModal(userId, fullname) {
            $('#modal-purchases').data('id', userId);
            $('#modal-purchases h3').text(`Compras Registradas de ${fullname}`);
            drawDtPurchases();
            $('#modal-purchases').modal({show:true});
        }

        function drawDtPurchases() {
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
                            width: '60px',
                            className: 'text-center',
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
                                        </button>${ data.Estado == 'Invalido' ? restoreButton : ''}`
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


                generarDT(tableID, dtImplName, {
                    sorteoId: $('#cboSorteos').val(),
                    usuarioId: `${$('#modal-purchases').data('id')}`,
                    estado: '20'
                }, '/Service.asmx/GetDT', tableOpts, () => { dtInjectServerButton('content_btnExportar', 'tblCompras'); });

            } catch (e) {
                notifier.alert(e.message ? e.message : e)
            }
        };

    </script>
</asp:Content>
