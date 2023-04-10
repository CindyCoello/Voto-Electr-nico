//$('#btnNuevo').click(function () {
//    $('#modalNuevo').modal();
//});

function Modal() {
    $('#modalNuevo').modal();
}


function GuardarTipoCandidato(accion) {
    $('#errorMessage').empty();

    var nombreTipoCandidato;
    var idtipCandidato;
    if (accion == 'Nuevo') {
        idtipCandidato = 0;
        nombreTipoCandidato = $('#nombreTipoCandidato').val();
    }
    else {

        nombreTipoCandidato = $('#nombreTipoCandidatoEdit').val();
        idtipCandidato = $('#idTipoCandidatoEdit').val();
    }

    if (nombreTipoCandidato == "") {
        $('#errorMessage').append("El campo  Tipo Candidato es requerido");

    } else {
        var data = {
            idtipCandidato: idtipCandidato,
            nombreTipoCandidato: nombreTipoCandidato
        };

        $.ajax({
            url: "/TipoCandidato/GuardarAjax",
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
    $('#nombreTipoCandidato').val('')
    

}


function EditarTipoCandidato(tipcan_Id) {
    var data = {
        id: tipcan_Id
    };
    $.ajax({
        url: "/TipoCandidato/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {


        if (result != null) {

            $('#idTipoCandidatoEdit').val(result.tipcan_Id);
            $('#nombreTipoCandidatoEdit').val(result.tipcan_Descripcion);
            $('#modalEditar').modal();

        }


    });
}





function EliminarTipoCandidato(tipcan_Id) {
    $('#ModalEliminar').modal();
    $('#btnSi').click(function () {
        var data = {
            id: tipcan_Id
        };
        $.ajax({
            url: "/TipoCandidato/DeleteAjax",
            type: "POST",
            DataType: "json",
            data: data,
        }).done(function (result) {
            //console.log(result);

            if (result != null) {

                $('#eliminarId').val(result.tipcan_Id);
                $('#eliminarTipoCandidato').val(result.tipcan_Descripcion);


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