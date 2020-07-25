

var dtLanguageSpanish = `{
    "sProcessing": "<i class='fa fa-spinner fa-spin fa-3x fa-fw mx-auto'></i>",
    "sLengthMenu": "Mostrar _MENU_",
    "sZeroRecords": "No se encontraron registros",
    "sEmptyTable": "Ningún registros disponible en esta tabla",
    "sInfo": "Mostrando del _START_ al _END_ de un total de _TOTAL_ registros",
    "sInfoEmpty": "Mostrando del 0 al 0 de un total de 0 registros",
    "sInfoFiltered": "(filtrado de un total de _MAX_)",
    "sInfoPostFix": "",
    "sSearch": "Buscar:",
    "sUrl": "",
    "sInfoThousands": ",",
    "sLoadingRecords": "Cargando...",
    "oPaginate": {
        "sFirst": "Primero",
        "sLast": "Último",
        "sNext": "Siguiente",
        "sPrevious": "Anterior"
    },
    "oAria": {
        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
    },
    "buttons": {
        "selectAll": "Seleccionar todos",
        "selectNone": "Deseleccionar todos"
    }
}`;



function generarDT(tableID, dtImplName, params,url, tableOpts, callback) {
    return new Promise((resolve, reject) => {
        try {
            if ($('#content_btnExportar').length > 0)
                $('form').append($('#content_btnExportar').hide());

            const table = $(`#${tableID}`);
            // DESTRUIR INSTANCIA SI EXISTE
            if (table.DataTable()) {
                table.DataTable().destroy();
            }
            table.removeClass('d-none');
            table.removeClass('hidden');
            // OPCIONES BASE DT
            const dtOpts = {
                bProcessing: true,
                serverSide: true,
                dom: '<"row p-2"<B>><"row"<"col-md-6"<l>><"col-md-6"<f>>>rtip',
                buttons: [
                    {
                        text: '<i class="fas fa-redo m-r-sm"></i>Refrescar',
                        action: function (e, dt, node, config) {
                            dt.search('').draw();
                            dt.state.clear();
                        }
                    }
                ],
                language: JSON.parse(dtLanguageSpanish),
                ajax: {
                    contentType: "application/json; charset=utf-8",
                    url: url,
                    type: "POST",
                    data: function (dtParams) {
                        const paramsKeys = Object.keys(params);
                        const customParameters = [];
                        paramsKeys.forEach((key, index) => {
                            const formattedParam = { key, value: params[key] };
                            customParameters.push(formattedParam);
                        });

                        dtParams["custom"] = customParameters;
                        return JSON.stringify({ dtImplName, requestDt: JSON.stringify(dtParams) });
                    },
                    dataFilter: function (res) {
                        const { datatable } = JSON.parse(JSON.parse(res).d);
                        return datatable;

                    },
                    error: function (err) {
                        let data, message, expired;
                        if (err.responseJSON) {
                            data = err.responseJSON.d ? JSON.parse(err.responseJSON.d) : err.responseJSON;
                        } else {
                            if (!err.statusText)
                                data = JSON.parse(JSON.parse(err.responseText).d ? JSON.parse(err.responseText).d : JSON.parse(err.responseText));
                            else
                                data = { message: err.statusText };
                        }
                        message = data.message ? data.message : data.Message;
                        expired = data.expired ? data.expired : false;
                        if (expired) {
                            ModalMensajeExpired(message);
                            reject();
                        } else {
                            notifier.alert(message);
                        }
                        $(`#${tableID}_processing`).hide();
                        reject(message);
                    }
                },
                initComplete: function () {
                    const api = this.api();
                    const input = $(`#${tableID}_filter input`);
                    input.unbind();
                    const callback = function () {
                        deshabilitarControles(`#${tableID}_wrapper`);
                    };
                    setBusqueda(input, api, callback);
                    resolve();
                }
            };

            // COMBINAR LAS OPCIONES BASE CON LAS DE LA IMPLEMENTACION

            Object.assign(dtOpts, tableOpts);

            // INICIALIZAR EL DATATABLE

            let datatable = table.DataTable(dtOpts);

            if (callback)
                callback(datatable);

            // EXTENDER FUNCIONALIDADES DT
            datatable.on('buttons-action', function (e, settings, processing) {
                deshabilitarControles(`#${tableID}_wrapper`);
            });
            datatable.on('search', function (e, settings, processing) {
                deshabilitarControles(`#${tableID}_wrapper`);
            });
            datatable.on('page', function () {
                deshabilitarControles(`#${tableID}_wrapper`);
            });
            datatable.on('preInit', function () {
                deshabilitarControles(`#${tableID}_wrapper`);
            });
            datatable.on('length', function (e, settings, processing) {
                deshabilitarControles(`#${tableID}_wrapper`);
            });
            datatable.on('draw', function () {
                habilitarControles(`#${tableID}_wrapper`);
            });
        } catch (e) {
            reject(e);
        }
    });
}

function generarDTNSS(tableID, selection, callback, customSettings, override) {
    return new Promise((resolve, reject) => {
        try {
            let table = $(`#MainContent_${tableID}`);
            if (table.length === 0)
                table = $(`#${tableID}`);
            // DESTRUIR INSTANCIA SI EXISTE
            if (table.DataTable()) {
                table.DataTable().destroy();
            }
            // Datatable settings

            //default buttons
            let buttons = [
                'copyHtml5',
                'excelHtml5',
                'pdfHtml5',
                'colvis',
                'print'
            ];
            const buttonsSelection = [
                'selectAll',
                'selectNone',
            ];

            if (selection)
                $.extend(true, buttons, buttonsSelection);

            let defaultSettings = {
                dom: '<"pull-left p-h-sm p-2" l</br>B><"pull-right"f>t<"pull-left p-h-sm p-2" i>p',
                colReorder: true,
                paging: true,
                pagingType: "simple_numbers",
                language: JSON.parse(dtLanguageSpanish),
                buttons,
                initComplete: function () {
                    resolve(this);
                }
            };

            if (selection)
                $.extend(true, defaultSettings, {
                    select: {
                        style: 'multi'
                    }
                })

            if (customSettings) {
                if (!override)
                    customSettings = $.extend({}, defaultSettings, customSettings);
                else
                    $.extend(true, defaultSettings, customSettings);
            }



            if (defaultSettings.ajax)
                defaultSettings.ajax.error = function (err) { reject(err); };

            // BS3
            $(`#${tableID}`).removeClass('hidden');
            // BS4
            $(`#${tableID}`).removeClass('d-none');


            // OPCIONES BASE DT
            table = table.DataTable(defaultSettings);

            if (callback)
                callback(table);

        } catch (e) {
            reject(e);
        }
    });
}

// METODOS EXTENDIDOS

function setBusqueda(input, api, deshabilitar) {
    $(input).keypress(function (e) {
        if (e.keyCode === 13) {
            deshabilitar();
            api.search($(this).val()).draw();
            return false;
        }

    });
}

// METODOS DE MODIFICACION DE ESTADO DE CONTROLES

function deshabilitarControles(content, alternativos) {
    let selector = [
        `${content} button`,
        `${content} input`,
        `${content} select`
    ];
    if (alternativos) selector = selector.concat(alternativos);
    let controlesConciliado = $(selector.join(','));
    controlesConciliado.prop("disabled", true);
    controlesConciliado = $(`${content} a`);
    controlesConciliado.hide();

}

function habilitarControles(content, alternativos) {
    let selector = [
        `${content} button`,
        `${content} input`,
        `${content} select`
    ];
    if (alternativos) selector = selector.concat(alternativos);
    let controlesConciliado = $(selector.join(','));
    controlesConciliado.prop("disabled", false);
    controlesConciliado = $(`${content} a`);
    controlesConciliado.show();
    if (!$(".switch input").is(':checked') && $(".switch input").length !== 0) {
        $('.dt-button.buttons-select-all').hide();
        $('.dt-button.buttons-select-none').hide();
    }
}

// Excel Export

function dtInjectServerButton(buttonId, tableId) {
    $(`#${tableId}_wrapper .dt-buttons`).append($(`#${buttonId}`).show());
}


function dtExportExcelServerSide(tableId) {
    const dt = $(`#${tableId}`).DataTable();
    if (!dt) {
        notifier.alert('Datatable is not valid.');
        return false;
    }
    // add search input to form data
    $(`#${tableId}_filter input[type="search"]`).attr('name', 'search');

    let hiddenInput = $('input[name="dtParams"]');
    if (hiddenInput.length === 0) {
        hiddenInput = document.createElement('input');
        hiddenInput.type = "hidden";
        hiddenInput.name = "dtParams";
        $('form').append(hiddenInput);
    }
    if (dt.rows().count() === 0) {
        notifier.warning('Empty table.');
        return false;
    } 
    $(hiddenInput).val(JSON.parse(dt.ajax.params()).requestDt);
    return true;
}


