VotoElectronicoConfig.CentroVotacion = (function () {
    var obj = {};

    obj.configureTable = function (params) {
        $(function () {

            var exportOptions = { columns: [0, 1, 2], orthogonal: "export" };
            var table = $('#datatable').DataTable({

                buttons: [
                    {
                        text: '<i class="mdi mdi-refresh">Recargar</i>',
                        titleAttr: 'Recargar tabla',
                        action: function (e, dt, node, config) {


                        }

                    },

                    {
                        extend: "collection",
                        text: '<i class="mdi mdi-export">Exportar</i>',
                        titleAttr: 'Exportar esta tabla',
                        buttons: [
                            {
                                extend: "excelHtml5",
                                text: '<i class="mdi mdi-file-excel">Excel</i>',
                                exportOptions: exportOptions
                            },
                            {
                                extend: "csvHtml5",
                                text: '<i class="mdi mdi-file-excel-outline">CSV</i>',
                                exportOptions: exportOptions
                            },
                            {
                                extend: "pdfHtml5",
                                text: '<i class="mdi mdi-pdf-box">PDF</i>',
                                exportOptions: exportOptions
                            }
                        ]
                    },
                    {

                        text: '<i class="mdi mdi-plus-thick">Nuevo</i>',
                        attr: {
                            title: "Añadir nuevo centro votación",
                            onclick: "CentroVotacionDDL('Guardar')"
                        }
                    }

                ],
                ajax: function (data, callback, settings) {
                    $.ajax({
                        url: params.listUrl,
                        type: "GET",
                        DataType: "json",
                        success: function (response) {
                            callback(response);
                        },
                    });
                },
                columnDefs: [
                    {
                        targets: 0,
                        data: 'cenvot_Id'
                    },
                    {
                        targets: 1,
                        data: 'cenvot_Nombre'
                    },
                    {
                        targets: 2,
                        data: 'depto_Id'
                    },
                    {
                        targets: 3,
                        data: 'muni_Id'
                    },
                    {
                        targets: 4,
                        data: 'cenvot_CodigoArea'
                    },
                    {
                        targets: 5,
                        data: 'cenvot_CodigoSectorElectoral'
                    },
                    
                    {
                        targets: 6,
                        data: 'cenvot_Latitud'
                    },
                    {
                        targets: 7,
                        data: 'cenvot_Longitud'
                    },
                    {
                        targets: 8,
                        data: 'cenvot_TotalMesas'
                    },


                    {
                        targets: 9,
                        className: "text-center",
                        width: 80,
                        render: function (data, type, row) {

                            botones = "";

                            if (type == "display") {
                                botones += '<button class="btn btn-secondary btn-sm" onclick="EditarCentroVotacion(' + row.cenvot_Id + ')"><i class="mdi mdi-file-edit-outline"></i></button> | ';
                                botones += '<button class="btn btn-danger btn-sm" onclick="EliminarCentroVotacion(' + row.cenvot_Id + ')"><i class="mdi mdi-delete-empty-outline"></i></button>';
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