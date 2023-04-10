function Modal() {
    $('#modalNuevo').modal();
    $('#cenvot_Id').empty();
    $.getJSON("/MesasElectorales/CentroVotacionList", function (data) {
        data = JSON.parse(data);

        $.each(data, function (key, value) {
            $('#cenvot_Id').append('<option value="' + value.cenvot_Id + '"> ' + value.cenvot_Nombre + '</option>');
            console.log('value', value);
        });
        $('#modalNuevo').modal();
    });
}



function GuardarMesasElectorales(accion) {
    $('#errorMessage').empty();

    var mesa_Id;
    var cenvot_Id;
    if (accion == 'Guardar') {
        mesa_Id = 0;
        cenvot_Id = $('#cenvot_Id').val();
    }
   else if (accion == 'Editar') {
        mesa_Id = $('#mesa_IdEdit').val();
        cenvot_Id = $('#cenvot_IdEdit').val();
      
    }
    else if (accion == 'Eliminar') {
        mesa_Id = $('#eliminarmesaElect').val();
        cenvot_Id = $('#eliminarId').val();
       
    }


    if (cenvot_Id == "") {
        $('#errorMessage').append("Este campo es requerido");

    } else {
        var data = {
            mesa_Id: mesa_Id,
            cenvot_Id: cenvot_Id,
            accion: accion
        };

        $.ajax({
            url: "/MesasElectorales/GuardarAjax",
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


function EditarMesaElectoral(mesa_Id) {
    var data = {
        id: mesa_Id
    };

    $.ajax({
        url: "/MesasElectorales/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {


        if (result != null) {

            $('#mesa_IdEdit').val(result.mesa_Id);
            $('#nombreCentroVotacionEdit').val(result.cenvot_Id);
            $('#modalEditar').modal();

        }



    });
}

function EliminarMesaElectoral(mesa_Id) {
    var data = {
        id: mesa_Id
    };

    $.ajax({
        url: "/MesasElectorales/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {


        if (result != null) {

            $('#eliminarId').val(result.mesa_Id);
            $('#eliminarmesaElect').val(result.cenvot_Id);
            $('#modalEditar').modal();

        }

        

    });
}



