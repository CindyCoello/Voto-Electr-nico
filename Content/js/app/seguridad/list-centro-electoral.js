VotoElectronicoConfig.CensoElectoral = (function () {

    var obj = {};
    //url de dfonde sacare la data
    obj.configureTable = function (params) {
        $(function () {
            var exportOptions = { columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9], orthogonal: "export" };
            var table = $('#datatableCenso').DataTable({
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
                            title: "Añadir Nuevo Censo Electoral",
                            onclick: "CensoDDL('Nuevo')"
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
                        data: 'censo_Id'
                    },
                    {
                        targets: 1,
                        data: 'censo_Identidad'
                    },
                    {
                        targets: 2,
                        data: 'censo_PrimerNombre'
                    },
                    {
                        targets: 3,
                        data: 'censo_PrimerApellido'
                    },
                    {
                        targets: 4,
                        data: 'censo_CodigoSexo'
                    },
                    {
                        targets: 5,
                        data: 'estadoCivil'
                    },
                    {
                        targets: 6,
                        data: 'poblado'
                    },
                    {
                        targets: 7,
                        data: 'aldea'
                    },
                    {
                        targets: 8,
                        data: 'municipio'
                    },
                    {
                        targets: 9,
                        className: 'text-center',
                        width: 60,
                        render: function (data, type, row) {
                            botones = "";
                            if (type == "display") {
                                botones += '<button class="btn btn-secondary btn-sm" onclick="EditarCenso(' + row.censo_Id + ')"><i class="mdi mdi-file-edit-outline"></i></button>|';
                                botones += '<button class="btn btn-danger btn-sm" onclick="EliminarCenso(' + row.censo_Id + ')"><i class="mdi mdi-trash-can"></i></button>|';
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