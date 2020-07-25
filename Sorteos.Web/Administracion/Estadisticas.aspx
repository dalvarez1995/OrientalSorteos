<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Estadisticas.aspx.cs" Inherits="Sorteos.Web.Administracion.Estadisticas" %>
<asp:Content ID="estadisticasHeader" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="estadisticasContent" ContentPlaceHolderID="content" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-12">
            <h2>Estadísticas</h2>
        </div>
        <div class="col-lg 12">
            <button class="btn btn-primary" onclick="loadCharts(true); return false;">
                <i class="fas fa-sync left"></i>
                Actualizar</button>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row" >
            <div class="col-xs-12 col-sm-6 col-md-4">
                <div id="usersChartBox" class="ibox border-bottom">
                    <div class="ibox-title collapse-link">
                        <h5>Usuarios Registrados</h5>
                        <div class="ibox-tools">
                            <a>
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="row justify-content-center">
                            <div class="fa-2x">
                                <i class="fas fa-circle-notch fa-spin"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class=" col-xs-12 col-sm-6 col-md-4">
                <div id="purchsChartBox" class="ibox border-bottom">
                    <div class="ibox-title collapse-link">
                        <h5>Compras Registradas</h5>
                        <div class="ibox-tools">
                            <a>
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="row justify-content-center">
                            <div class="fa-2x">
                                <i class="fas fa-circle-notch fa-spin"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class=" col-xs-12 col-sm-6 col-md-4">
                <div id="statesChartBox" class="ibox border-bottom">
                    <div class="ibox-title collapse-link">
                        <h5>Compras por Provincia</h5>
                        <div class="ibox-tools">
                            <a>
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="row justify-content-center">
                            <div class="fa-2x">
                                <i class="fas fa-circle-notch fa-spin"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class=" col-xs-12 col-sm-6 col-md-4">
                <div id="typesChartBox" class="ibox border-bottom">
                    <div class="ibox-title collapse-link">
                        <h5>Compras por Tipo</h5>
                        <div class="ibox-tools">
                            <a>
                                <i class="fa fa-chevron-up"></i>
                            </a>
                        </div>
                    </div>
                    <div class="ibox-content">
                        <div class="row justify-content-center">
                            <div class="fa-2x">
                                <i class="fas fa-circle-notch fa-spin"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="estadisticasBody" ContentPlaceHolderID="body" runat="server">
    <%: Scripts.Render("~/bundles/chartjs") %>

    <script type="text/javascript">

        $(document).ready(() => {
            loadCharts();
        });

        function loadCharts(refresh) {
            drawCustomerChart();
            drawPurchasesChart();
            drawStatesChart();
            drawTypesChart();
            if (refresh)
                notifier.success("Estadísticas actualizadas.");
        }

        function drawCustomerChart() {

            ajaxPOST('/Service.asmx/GetCustomerChartData', {}, (res) => {
                try {
                    $('#usersChartBox .ibox-content .row').html(`
                    <div class="col-md-12">
                        <canvas id="chartUsers" class="mx-auto"></canvas>
                    </div>`);

                    let elChart = document.getElementById('chartUsers');

                    const labels = res.data.map(m => m.day);
                    const data = res.data.map(m => m.value);

                    crearLineChart(elChart, 'line', data, labels, 'Últimos 7 dias', 'Usuarios');

                } catch (e) {
                    notifier.alert(e.message);
                }
            });
        }

        function drawPurchasesChart() {

            ajaxPOST('/Service.asmx/GetPurchasesChartData', {}, (res) => {
                try {
                    $('#purchsChartBox .ibox-content .row').html(`
                    <div class="col-md-12">
                        <canvas id="chartPurchases" class="mx-auto"></canvas>
                    </div>`);

                    let elChart = document.getElementById('chartPurchases');

                    const labels = res.data.map(m => m.day);
                    const data = res.data.map(m => m.value);

                    crearLineChart(elChart, 'bar', data, labels, 'Últimos 7 dias', 'Compras Registradas');

                } catch (e) {
                    notifier.alert(e.message);
                }
            });
        }

        function drawStatesChart() {

            ajaxPOST('/Service.asmx/GetPurchasesByStateChartData', {}, (res) => {
                try {
                    $('#statesChartBox .ibox-content .row').html(`
                    <div class="col-md-12">
                        <canvas id="statesPurchases" class="mx-auto"></canvas>
                    </div>`);

                    let elChart = document.getElementById('statesPurchases');

                    const labels = res.data.map(m => m.state);
                    const data = res.data.map(m => m.value);

                    crearLineChart(elChart, 'pie', data, labels, 'Provincias del Ecuador', '',true);

                } catch (e) {
                    notifier.alert(e.message);
                }
            });
        }

        function drawTypesChart() {

            ajaxPOST('/Service.asmx/GetPurchasesByTypeChartData', {}, (res) => {
                try {
                    $('#typesChartBox .ibox-content .row').html(`
                    <div class="col-md-12">
                        <canvas id="typesPurchases" class="mx-auto"></canvas>
                    </div>`);

                    let elChart = document.getElementById('typesPurchases');

                    const labels = res.data.map(m => m.type);
                    const data = res.data.map(m => m.value);

                    crearLineChart(elChart, 'doughnut', data, labels, 'Tipos de Compra', '', true);

                } catch (e) {
                    notifier.alert(e.message);
                }
            });
        }

        
    </script>
</asp:Content>
