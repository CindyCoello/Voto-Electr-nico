//$('#btnNuevo').click(function () {
//    $('#modalNuevo').modal();
//});

function Modal() {
    $('#modalNuevo').modal();
}


function Details() {
    $('#detalle').click(function () {
        $('#modaldetalles').modal();
    });

}

function GuardarEstadoCivil(accion) {
    $('#errorMessage1').empty();

    var nombreEstadoCivil;
    var idEstadoCivil;
    if (accion == 'Nuevo') {
        idEstadoCivil = 0;
        nombreEstadoCivil = $('#nombreEstadoCivil').val();
    }
    else {

        nombreEstadoCivil = $('#nombreEstadoCivilEdit').val();
        idEstadoCivil = $('#idEstadoCivilEdit').val();
    }

    if (nombreEstadoCivil == "") {
        $('#errorMessage1').append("Este campo es requerido");

    } else {
        var data = {
            idEstadoCivil: idEstadoCivil,
            nombreEstadoCivil: nombreEstadoCivil
        };

        $.ajax({
            url: "/EstadosCivils/GuardarAjax",
            type: "POST",
            DataType: "json",
            data: data,
        }).done(function (result) {
            if (result == "Ingresado") {
                VotoElectronicoConfig.alert('success', 'Registro ingresado correctamente');
                window.setTimeout(function () {
                    $('#datatable').DataTable().ajax.reload();
                    $('#modalNuevo').modal('hide');
                    LimpiarInputs();
                }, 2000);
            } else if (result == "Modificado") {
                VotoElectronicoConfig.alert('success', 'Registro actualizado correctamente');
                window.setTimeout(function () {

                    $('#datatable').DataTable().ajax.reload();
                    $('#modalEditar').modal('hide');

                }, 2000);
            }

        });
    }
}

const LimpiarInputs = function () {
    $('#nombreEstadoCivil').val('')
  

}




function EditarEstadoCivil(estCiv_Id) {
    var data = {
        id: estCiv_Id
    };
    $.ajax({
        url: "/EstadosCivils/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {


        if (result != null) {

            $('#idEstadoCivilEdit').val(result.estCiv_Id);
            $('#nombreEstadoCivilEdit').val(result.estCiv_Descripcion);
            $('#modalEditar').modal();

        }


    });
}




function DetalleEstadoCivil(estCiv_Id) {
    $('#modaldetalles').modal();
    var data = {
        id: estCiv_Id
    };
    $.ajax({
        url: "/EstadosCivils/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {

        console.log(result);
        if (result != null) {

            $('#idEstadoCivildetalle').text(result.estCiv_Id);
            $('#nombreEstadoCivildetalle').text(result.estCiv_Descripcion);
           

        }


    });
}


function EliminarEstadoCivil(estCiv_Id) {
    $('#ModalEliminar').modal();
    $('#btnSi').click(function () {
        var data = {
            id: estCiv_Id
        };
        $.ajax({
            url: "/EstadosCivils/DeleteAjax",
            type: "POST",
            DataType: "json",
            data: data,
        }).done(function (result) {
            //console.log(result);

            if (result != null) {

                $('#eliminarId').val(result.estCiv_Id);
                $('#eliminarEstadoCivil').val(result.estCiv_Descripcion);


            }
            if (result == "Eliminado") {
                VotoElectronicoConfig.alert('success', 'Registro Eliminado exitosamente');
                window.setTimeout(function () {

                    $('#datatable').DataTable().ajax.reload();
                    $('#ModalEliminar').modal('hide');
                }, 2000);

            }

        });
    });

}
