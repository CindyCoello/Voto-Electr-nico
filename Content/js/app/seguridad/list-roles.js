VotoElectronicoConfig.Roles = (function () {
    var obj = {};

    obj.configureRol = function (params) {
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
                            title: "Añadir nuevo",
                            onclick: "/Role/AgregarRol"

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
                        data: 'rol_Id'
                    },
                    {
                        targets: 1,
                        data: 'rol_Nombre'
                    },
                    {
                        targets: 2,
                        data: 'rol_EsActivo'
                    },
                    
                    {
                        targets: 3,
                        className: "text-center",
                        width: 80,
                        render: function (data, type, row) {

                            botones = "";

                            if (type == "display") {
                                botones += '<button class="btn btn-secondary btn-sm" onclick="EditarRol(' + row.rol_Id + ')"><i class="mdi mdi-file-edit-outline"></i></button> | ';
                                botones += '<button class="btn btn-secondary btn-sm" onclick="DetalleRol(' + row.rol_Id + ')"><i class="mdi mdi-file-document-box-minus-outline"></i></button> | ';
                                botones += '<button class="btn btn-danger btn-sm" onclick="EliminarRol(' + row.rol_Id + ')"><i class="mdi mdi-delete-empty-outline"></i></button>';
                            }
                            return botones;
                        }
                    }


                ]


            });

        });

    };

    obj.configureRole = function () {
        $(function () {
            var $componentsTree = $("#components-tree"),
                $itemsInput = $("#ModuleItemsInput");

            $componentsTree.jstree({
                plugins: ["checkbox", "search"],
            }).on("ready.jstree", function () {
                if ($itemsInput.val()) {
                    var selected = $itemsInput.val().split(",");
                    for (var i = 0; i < selected.length; i++) {
                        $componentsTree.jstree(true).select_node("item-" + selected[i]);
                    }
                }
            }).on("changed.jstree", function () {
                var selected = $componentsTree.jstree("get_selected", true)
                    .map(function (x) { return x.data.id; })
                    .filter(function (y) { return y !== undefined; });
                $itemsInput.val(selected);
            });
        });
    }

    return obj;

}());