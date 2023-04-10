VotoElectronicoConfig.Poblados = (function () {

    var obj = {};
    
    obj.configureTable = function (params) {
        $(function () {
            var exportOptions = { columns: [0, 1, 2], orthogonal: "export" };
            var table = $('#datatable').DataTable({
                buttons: [
                    {
                        text: '<i class="mdi mdi-refresh"> Recargar</i>',
                        titleAttr: 'Recargar la tabla',
                        action: function (e, dt, node, config) {
                            dt.ajax.reload();
                        }
                    },
                    {
                        extend: 'collection',
                        text: '<i class="mdi mdi-refresh"> Exportar</i>',
                        titleAttr: 'Exportar esta tabla',
                        buttons: [
                            {
                                extend: "excelHtml5",
                                text: '<i class="mdi mdi-file-excel"> Excel</i>',
                                exportOptions: exportOptions
                            },
                            {
                                extend: "csvHtml5",
                                text: '<i class="mdi mdi-file-excel-outline"> CSV</i>',
                                exportOptions: exportOptions
                            },
                            {
                                extend: "pdfHtml5",
                                text: '<i class="mdi mdi-pdf-box"> PDF</i>',
                                exportOptions: exportOptions
                            },
                        ]
                    },
                    {
                        text: '<i class="mdi mdi-plus-thick"> Nuevo</i>',
                        attr: {
                            title: "Añadir Nuevo Poblado",
                            onclick: "PobladosDDL('Guardar')"
                        }
                    }
                ],
                ajax: function (data, callback, settings) {
                    $.ajax({
                        url: params.listUrl,
                        type: "GET",
                        dataType: "json",
                        success: function (response) {
                            callback(response);
                        },
                    });

                },
                columnDefs: [
                    {
                        targets: 0,
                        data: 'pobl_Id'
                    },
                    {
                        targets: 1,
                        data: 'pobl_Descripcion'
                    },
                    {
                        targets: 2,
                        data: 'aldea_Id'
                    },
                    {
                        targets: 3,
                        data: 'muni_Id'
                    },
                    {
                        targets: 4,
                        data: 'depto_Id'
                    },
                   
                    {
                        targets: 5,
                        className: 'text-center',
                        width: 60,
                        render: function (data, type, row) {
                            botones = "";
                            if (type == "display") {
                                botones += '<button class="btn btn-secondary btn-sm" onclick="EditarPoblados(' + row.pobl_Id + ')"><i class="mdi mdi-file-edit-outline"></i></button> |';
                                botones += '<button class="btn btn-danger btn-sm" onclick="EliminarPoblados(' + row.pobl_Id + ')"><i class="mdi mdi-delete-empty-outline"></i></button>';
                            }
                            return botones;
                        }
                    }
                ]



            });
        });

    };
    return obj;
}());