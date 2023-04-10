
function ComponentesDDL(accion, componente, idmodulo, modulo) {
    if (accion == 'Guardar') {
        $('#comp_Id').empty();
        $.getJSON("/Modulos/ComponenteList", function (data) {

            data = JSON.parse(data);
            $.each(data, function (key, value) {
                $('#comp_Id').append('<option value="' + value.comp_Id + '">' + value.comp_Nombre + '</option>');
            });
            $('#modalNuevo').modal();
        });


    }
    if (accion == 'Editar' && componente != 0 && idmodulo != 0 && modulo != 0) {
        $('#comp_IdEdit').empty();
        $.getJSON("/Modulos/ComponenteList", function (data) {

            data = JSON.parse(data);
            $.each(data, function (key, value) {
                if (value.comp_Id == componente) {


                    $('#comp_IdEdit').append('<option selected value="' + value.comp_Id + '">' + value.comp_Nombre + '</option>');
                    console.log(componente);

                }
                else {
                    $('#comp_IdEdit').append('<option value="' + value.comp_Id + '">' + value.comp_Nombre + '</option>');
                }
            });

           
        });
        elegir(modulo)
    }
}

function elegir(modulo) {
    $("#mod_NombreEdit option[value=" + modulo + "]").attr("selected", true);
    console.log(modulo);
}


function GuardarModulo(accion) {
    $('#errorMessage').empty();


    var mod_Id;
    var comp_Id;
    var mod_Nombre;



    if (accion == 'Guardar') {
        mod_Id = 0;
        comp_Id = $('#comp_Id').val();
        mod_Nombre = $('#mod_Nombre').val();

    }
    if (accion == 'Editar') {

        mod_Id = $('#mod_IdEdit').val();
        comp_Id = $('#comp_IdEdit').val();
        mod_Nombre = $('#mod_NombreEdit').val();
        


    }if (accion == 'Eliminar') {
       
        comp_Id = $('#mod_IdEliminar').val();
        mod_Nombre = $('#mod_NombreEliminar').val();
      

    }

    if (mod_Nombre == "") {
        $('#errorMessage').append("El campo es requerido");

    } else if (comp_Id == "") {
        $('#errorMessage').append("El campo es requerido");
    } 
    else {

        var data = {
            mod_Id: mod_Id,
            comp_Id: comp_Id,
            mod_Nombre: mod_Nombre,
            accion: accion
           
        };

        $.ajax({
            url: "/Modulos/GuardarAjax",
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





function EditarModulo(mod_Id) {
    var data = {
        id: mod_Id
    };
    $.ajax({
        url: "/Modulos/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {


        if (result != null) {
            //result = JSON.parse(result);
            var modu_Id = result.mod_Id;
            var compont_Id = result.comp_Id;
            var modul_Nombre = result.mod_Nombre;



            $('#mod_IdEdit').val(modu_Id);
            $('#comp_IdEdit').val(compont_Id);
            $('#mod_NombreEdit').val(modul_Nombre);

            ComponentesDDL('Editar', modu_Id, compont_Id,modul_Nombre);
          $('#modalEditar').modal();


        }


    });
}


function EliminarModulo(mod_Id) {
    var data = {
        id: mod_Id
    };
    $.ajax({
        url: "/Modulos/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {


        if (result != null) {
            $('#mod_IdEliminar').val(mod_Id);
            $('#mod_NombreEliminar').val(mod_Nombre);
            $('#ModalEliminar').modal();

        }


    });
}



