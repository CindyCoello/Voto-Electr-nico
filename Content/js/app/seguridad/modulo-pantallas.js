

function ModuloPantallaDDL(accion, modulo) {
    if (accion == 'Guardar') {
        $('#mod_Id').empty();
        $.getJSON("/ModuloPantallas/ModuloList", function (data) {

            data = JSON.parse(data);
            $.each(data, function (key, value) {
                $('#mod_Id').append('<option value="' + value.mod_Id + '">' + value.mod_Nombre + '</option>');
            });
            $('#modalNuevo').modal();
        });


    }
    if (accion == 'Editar' && modulo != 0) {
        $('#mod_IdEdit').empty();
        $.getJSON("/ModuloPantallas/ModuloList", function (data) {

            data = JSON.parse(data);
            $.each(data, function (key, value) {
                if (value.mod_Id == modulo) {

                   
                    $('#mod_IdEdit').append('<option selected value="' + value.mod_Id + '">' + value.mod_Nombre + '</option>');
                    console.log(modulo);

                }
                else {
                    $('#mod_IdEdit').append('<option value="' + value.mod_Id + '">' + value.mod_Nombre + '</option>');
                }
            });
           

        });

    }
}




function GuardarModuloPant(accion) {
    $('#errorMessage').empty();

    var modpan_Id = 0;
    var mod_Id;
    var modpan_Nombre;


    if (accion == 'Guardar') {
        mod_Id = $('#mod_Id').val();
        modpan_Nombre = $('#modpan_Nombre').val();
    }
    if (accion == 'Editar') {
        modpan_Id = $('#modpan_IdEdit').val();
        mod_Id = $('#mod_IdEdit').val();
        modpan_Nombre = $('#modpan_NombreEdit').val();
       

    } if (accion == 'Eliminar') {

        modpan_Id = $('#modpan_IdEliminar').val();
        mod_Id = $('#mod_IdEliminar').val();
        modpan_Nombre = $('#modpan_NombreEliminar').val();
        
    }

    if (modpan_Nombre == "") {
        $('#errorMessage').append("El campo es requerido");

    }

    else {
        var data = {
            modpan_Id: modpan_Id,
            mod_Id: mod_Id,
            modpan_Nombre: modpan_Nombre,
            accion: accion
        };

        $.ajax({
            url: "/ModuloPantallas/GuardarAjax",
            type: "POST",
            DataType: "json",
            data: data,
        }).done(function (result) {
            switch (result) {

                default:
                case "Ingresado":
                case 0:                                                          
                    VotoElectronicoConfig.alert("success", 'Registro ingresado correctamente');



                    window.setTimeout(function () {
                        $('#datatable').DataTable().ajax.reload();
                        $('#modalNuevo').modal('hide');

                    }, 2000);

                    break;

                case "Modificado":
                case 1:                                                                                 
                    VotoElectronicoConfig.alert("success", 'Registro actualizado correctamente');

                    window.setTimeout(function () {
                        $('#datatable').DataTable().ajax.reload();
                        $('#modalEditar').modal('hide');
                    }, 2000);
                    break;

                case "Eliminado":
                case 2:                                                                                   
                    VotoElectronicoConfig.alert("success", 'Registro eliminado correctamente');

                    window.setTimeout(function () {
                        $('#datatable').DataTable().ajax.reload();
                        $('#ModalEliminar').modal('hide');
                    }, 2000);
                    break;


            }


        });
    }
}





function EditarModuloPant(modpan_Id) {
    var data = {
        id: modpan_Id
    };
    $.ajax({
        url: "/ModuloPantallas/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {
        if (result != null) {

            //result = JSON.parse(result);


            var modpant_Id = result.modpan_Id;
            var modu_Id = result.mod_Id;
            var mod_Nombre = result.modpan_Nombre;
          

            $('#modpan_IdEdit').val(modpant_Id);
            $('#mod_IdEdit').val(modu_Id);
            $('#modpan_NombreEdit').val(mod_Nombre);
           
            ModuloPantallaDDL('Editar', modu_Id);
             $('#modalEditar').modal();

        }


    });
}


function EliminarModuloPant(modpan_Id) {
    //console.log("accion eliminar");
    var data = {
        id: modpan_Id
    };
    $.ajax({
        url: "/ModuloPantallas/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {
       

        if (result != null) {
           
            $('#modpan_IdEliminar').val(result.modpan_Id);
            $('#mod_IdEliminar').val(result.mod_Id);
            $('#modpan_NombreEliminar').val(result.modpan_Nombre);
            $('#ModalEliminar').modal();

        }


    });
}

