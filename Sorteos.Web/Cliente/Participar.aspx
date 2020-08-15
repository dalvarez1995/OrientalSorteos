<%@ Page Title="Participar" Language="C#" MasterPageFile="~/Public.Master" AutoEventWireup="true" CodeBehind="Participar.aspx.cs" Inherits="Sorteos.Web.Cliente.Participar" %>

<asp:Content ID="resumenHead" ContentPlaceHolderID="logo" runat="server">
</asp:Content>

<asp:Content ID="participarHead" ContentPlaceHolderID="head" runat="server">
    <webopt:BundleReference runat="server" Path="~/Content/css/jquery-steps" />
</asp:Content>
<asp:Content ID="participarMain" ContentPlaceHolderID="main" runat="server">
    <div class="row">
        <h6 class="o-label"><span>REGISTRO</span></h6>
    </div>
    <div class="row">
        <div id="registrar-compra">
            <h4>Publicidad</h4>
            <section id="tipoPublicidad">
                <div class="row">
                    <h3>Por que medio te enteraste de nuestro sorteo?</h3>
                </div>
                <div class="row" style="height: 350px; display: grid; grid-template-rows: repeat(2,1fr); grid-template-columns: auto;">
                    <div style="display: flex; align-items: center; grid-row: 1/2; cursor: pointer; border-radius: 15px;" onmouseover="this.style.backgroundColor = '#bbfbf5'" onmouseout="this.style.backgroundColor = 'transparent'" onclick="checkRadioButton(this,event)">
                        <div class="row">
                            <i class="fas fa-home fa-4x" style="color: #0094ff;"></i>
                            <label>
                                <input id="radioDigital" runat="server" name="publicidad" type="radio"  value="tienda" />
                                <span>Publicidad Digital (Redes sociales, sitios web,etc.)</span>
                            </label>
                        </div>
                    </div>
                    <div style="display: flex; align-items: center; grid-row: 2/2; cursor: pointer; border-radius: 15px;" onmouseover="this.style.backgroundColor = '#bbfbf5'" onmouseout="this.style.backgroundColor = 'transparent'" onclick="checkRadioButton(this,event)">
                        <div class="row">
                            <i class="fas fa-industry fa-4x" style="color: #0094ff;"></i>
                            <label>
                                <input id="radioImpresa" runat="server" name="publicidad" type="radio" value="super" />
                                <span>Publicidad Impresa (Tiendas,Supermercados, Prensa, etc.)</span>
                            </label>
                        </div>
                    </div>
                    <span id="error-publicidad" style="display: none" class="error-message">Seleccione una opción</span>
                </div>
            </section>
            <h4>Negocio</h4>
            <section id="tipoNegocio">
                <div class="row">
                    <h3>En que tipo de negocio compraste nuestro producto?</h3>
                </div>
                <div class="row" style="height: 350px; display: grid; grid-template-rows: repeat(2,1fr); grid-template-columns: auto;">
                    <div style="display: flex; align-items: center; grid-row: 1/2; cursor: pointer; border-radius: 15px;" onmouseover="this.style.backgroundColor = '#bbfbf5'" onmouseout="this.style.backgroundColor = 'transparent'" onclick="checkRadioButton(this,event)">
                        <div class="row">
                            <i class="fas fa-home fa-4x" style="color: #0094ff;"></i>
                            <label>
                                <input id="radioTienda" runat="server" name="tipoNegocio" type="radio" value="tienda" />
                                <span>Tienda</span>
                            </label>
                        </div>
                    </div>
                    <div style="display: flex; align-items: center; grid-row: 2/2; cursor: pointer; border-radius: 15px;" onmouseover="this.style.backgroundColor = '#bbfbf5'" onmouseout="this.style.backgroundColor = 'transparent'" onclick="checkRadioButton(this,event)">
                        <div class="row">
                            <i class="fas fa-industry fa-4x" style="color: #0094ff;"></i>
                            <label>
                                <input id="radioSupermercado" runat="server" name="tipoNegocio" type="radio" value="super" />
                                <span>Supermercado</span>
                            </label>
                        </div>
                    </div>
                    <span id="error-tipocompra" style="display: none" class="error-message">Seleccione una opción</span>
                </div>
            </section>
            <h4>Ubicación</h4>
            <section id="ubicacion">
                <div class="row">
                    <h3>En donde compraste nuestro producto?</h3>
                </div>
                <label>Provincia</label>
                <div class="row">
                    <div class="input-field col s12">
                        <asp:DropDownList ID="cboProvincias" onchange="getCities(this,$('#cboCiudades'));return false;" CssClass="browser-default" ClientIDMode="Static" runat="server">
                        </asp:DropDownList>
                    </div>
                    <span id="error-provincias" style="display: none" class="error-message">Seleccione la provincia donde adquirio su producto</span>
                </div>
                <label>Ciudad</label>
                <div class="row">
                    <div class="input-field col s12">
                        <select id="cboCiudades" name="ciudadId" class="browser-default">
                            <option value="0" selected>Seleccione una provincia primero</option>
                        </select>
                    </div>
                    <span id="error-ciudad" style="display: none" class="error-message">Seleccione la ciudad donde adquirio su producto</span>
                </div>
            </section>

            <h4>Producto</h4>
            <section>
                <div class="row">
                    <h3>Ya casi estamos</h3>
                    <p>Por último, selecciona la marca, ingresa el lote del producto y la cantidad de productos participantes que adquiriste, además, sube una fotografía <span id="fotografiaInfo">del producto </span> para validar la información que nos proporcionaste.</p>
                </div>
                <label>Marcas Participantes</label>
                <div class="row">
                    <div class="input-field col s12">
                        <asp:DropDownList ID="cboMarcas" CssClass="browser-default" ClientIDMode="Static" runat="server">
                        </asp:DropDownList>
                    </div>
                    <span id="error-marcas" style="display: none" class="error-message">Seleccione la marca del producto adquirido</span>
                </div>
                <div class="row">

                    <div class="input-field col s12">
                        <asp:TextBox ID="txtCantidad" ClientIDMode="Static" type="number" min="1" max="100" Style="padding: 0 .75rem !important; border: none; border-bottom: 1px solid #9e9e9e;" runat="server" Text="1" />
                        <label>Cantidad</label>
                    </div>
                    <span id="error-cantidad" style="display: none" class="error-message">Ingrese un número válido.</span>
                </div>
                <div class="row">
                    <div class="input-field col s12">
                        <asp:TextBox ID="txtNumeroLote" ClientIDMode="Static" Style="padding: 0 .75rem !important; border: none; border-bottom: 1px solid #9e9e9e;" runat="server" Text="" />
                        <label>Número de Lote</label>
                    </div>
                    <span id="error-lote" style="display: none" class="error-message">Ingrese un número de lote válido. Debe ser de 6 dígitos númericos.</span>
                </div>
                <div class="row" style="margin-bottom:35px;">
                    <label>Fotografía</label>
                    <div class="file-field input-field">
                        <div class="btn">
                            <span>SUBIR</span>
                            <asp:FileUpload ID="fuFoto" ClientIDMode="Static" acceptedFiles="image/png,image/jpg,image/jpeg" runat="server" />
                        </div>
                        <div class="file-path-wrapper">
                            <input class="file-path validate" style="border: none; border-bottom: 1px solid #9e9e9e;" type="text" placeholder="Toca aqui">
                        </div>
                    </div>
                    <span id="error-file-required" style="display: none" class="error-message">No ha subido la fotografía.</span>
                    <span id="error-file-format" style="display: none" class="error-message">Formato no válido. Solo se aceptan extensiones de tipo (jpg|jpeg|png).</span>
                </div>
                <asp:Button ID="btnFinalizar" ClientIDMode="Static" Style="display: none" OnClientClick="return true;" OnClick="btnFinalizar_Click" runat="server" />
            </section>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="footer" runat="server">
    <div class="row">
        <div class="col s12">
            <a class="btn waves-effect waves-light" runat="server" href="/Cliente/Resumen" causesvalidation="false">
                <i class=" fas fa-arrow-left left"></i>
                VOLVER
            </a>
        </div>
    </div>
</asp:Content>

<asp:Content ID="participarBody" ContentPlaceHolderID="body" runat="server">
    <%: Scripts.Render("~/bundles/jquery-steps") %>


    <script>

        $(document).ready(() => {
        });


        function validatePublicidad() {
            if ($('#registrar-compra-p-0 input[type="radio"]:checked').length == 0) {
                $('#error-publicidad').show();
                return false;
            }
            return true;
            return true;
        }

        function validateTipoCompra() {
            if ($('#registrar-compra-p-1 input[type="radio"]:checked').length == 0) {
                $('#error-tipocompra').show();
                return false;
            }
            return true;
        }

        function validateProvincias() {
            let selectedState = $('#cboProvincias');
            if (selectedState.val() <= 0) {
                $('#error-provincias').show();
                return false;
            }
            return true;
        }


        function validateProvincias() {
            let selectedState = $('#cboProvincias');
            if (selectedState.val() <= 0) {
                $('#error-provincias').show();
                return false;
            }
            return true;
        }

        function validateMarcas() {
            let selectedState = $('#cboMarcas');
            if (selectedState.val() <= 0) {
                $('#error-marcas').show();
                return false;
            }
            return true;
        }

        function validateCiudad() {
            let selectedCity = $('#cboCiudades');
            if (selectedCity.val() <= 0) {
                $('#error-ciudad').show();
                return false;
            }
            return true;
        }

        function validateCantidad() {
            let enteredQty = $('#txtCantidad');
            if (+enteredQty.val() <= 0) {
                $('#error-cantidad').show();
                return false;
            }
            return true;
        }

        function validateLote() {
            let enteredLote = $('#txtNumeroLote');
            if (!enteredLote.val() || /^[0-9]{6}$/.test(enteredLote.val()) == false) {
                enteredLote.focus();
                $('#error-lote').show();
                return false;
            }
            return true;
        }

        function validateFile() {
            let enteredFile = $('#fuFoto');
            if (!enteredFile.val()) {
                $('#error-file-required').show();
                return false;
            }
            if (/\.(jpg|gif|jpeg|png)$/.test(enteredFile.val().toLowerCase()) == false) {
                $('#error-file-format').show();
                return false;
            }
            return true;
        }


        $(function () {

            let form = $('form');
            let wizard = $("#registrar-compra");
            wizard.steps({
                headerTag: "h4",
                bodyTag: "section",
                transitionEffect: "slideLeft",
                labels: {
                    previous: 'Anterior',
                    next: 'Siguiente',
                    finish: 'Finalizar'
                },
                onStepChanging: function (event, currentIndex, newIndex) {
                    if (currentIndex > newIndex) {
                        return true;
                    }
                    let valid = false;
                    switch (currentIndex) {
                        case 0:
                            valid = validatePublicidad();
                            return valid;
                            break;
                        case 1:
                            valid = validateTipoCompra();

                            if (valid) {
                                if ($('#registrar-compra-p-1 input[type="radio"]:checked').val() == "super")
                                    $('#fotografiaInfo').text('de la factura');
                                else
                                    $('#fotografiaInfo').text('del producto');
                            }
                            return valid;
                            break;
                        case 2:

                            valid = true;

                            valid = validateProvincias();

                            if (!valid)
                                return false;
                            else
                                $('#error-provincias').hide()

                            valid = validateCiudad();


                            if (valid) {
                                $('#error-provincias').hide();
                                $('#error-ciudad').hide();
                            }

                            return valid;
                            break;
                        default:
                            return true;
                            break;

                    }
                },
                onFinishing: function (event, currentIndex) {
                    try {

                        valid = true;


                        valid = validateMarcas();
                        if (!valid)
                            return false;
                        else
                            $('#error-marcas').hide();

                        valid = validateCantidad();
                        if (!valid)
                            return false;
                        else
                            $('#error-cantidad').hide();

                        valid = validateLote();
                        if (!valid)
                            return false;
                        else
                            $('#error-lote').hide();


                        valid = validateFile();
                        if (!valid)
                            return false;

                        if (valid) {
                            $('#error-lote').hide();
                            $('#error-file-required').hide();
                            $('#error-file-format').hide();
                            $('#error-marcas').hide();
                            $('#error-cantidad').hide();
                        }

                        ajaxPOST('/Service.asmx/ValidateLote', {
                            lote: $('#txtNumeroLote').val()
                        }, (result ) => {
                            if (!result) {
                                error('El número de lote proporcionado no es válido o no aplica para este sorteo');
                                return;
                            }
                            $('#btnFinalizar').click(); 
                        });

                        return false;

                    } catch (e) {
                        error(e.message);
                        return false;
                    }
                },
                onFinished: function (event, currentIndex) {
                    $('#btnFinalizar').click();
                }
            });
        });

        function checkRadioButton(el, event) {
            $(el).find('input[type=radio]').prop('checked', true);
        }
    </script>
</asp:Content>
