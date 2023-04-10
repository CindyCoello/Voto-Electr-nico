﻿VotoElectronicoConfig.Usuarios = (function () {
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
                            title: "Añadir nuevo Usuario",
                            onclick: "Modal()"
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
                        data: 'usu_Id'
                    },
                    {
                        targets: 1,
                        data: 'usu_Identidad'
                    },
                    {
                        targets: 2,
                        data: 'usu_PrimerNombre'
                    },
                    {
                        targets: 3,
                        data: 'usu_PrimerApellido'
                    },
                    {
                        targets: 4,
                        data: 'usu_SegundoNombre'
                    },
                    {
                        targets: 5,
                        data: 'usu_SegundoApellido'
                    },
                    {
                        targets: 6,
                        data: 'usu_Telefono'
                    },
                    
                    {
                        targets: 7,
                        data: 'rol_Id'
                    },
                    {
                        targets: 8,
                        data: 'usu_EsActivo'
                    },

                    {
                        targets: 9,
                        className: "text-center",
                        width: 80,
                        render: function (data, type, row) {

                            botones = "";

                            if (type == "display") {
                                botones += '<button class="btn btn-secondary btn-sm" onclick="EditarUsuarios(' + row.usu_Id + ')"><i class="mdi mdi-file-edit-outline"></i></button> | ';
                                botones += '<button class="btn btn-danger btn-sm" onclick="EliminarUsuario(' + row.usu_Id + ')"><i class="mdi mdi-delete-empty-outline"></i></button>';
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