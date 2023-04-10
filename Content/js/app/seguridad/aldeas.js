
function AldeasDDL(accion, iddepto, idmun) {
    if (accion == 'Guardar') {
        $('#depto_Id').empty();
        $.getJSON("/Aldeas/DepartamentoList", function (data) {

            data = JSON.parse(data);
            $.each(data, function (key, value) {
                $('#depto_Id').append('<option value="' + value.depto_Id + '">' + value.depto_Descripcion + '</option>');
            });
            $('#modalNuevo').modal();
        });


    }
    if (accion == 'Editar' && iddepto != 0 && idmun != 0) {
        $('#idDepartamentoEditar').empty();
        $.getJSON("/Aldeas/DepartamentoList", function (data) {

            data = JSON.parse(data);
            $.each(data, function (key, value) {
                if (value.depto_Id == iddepto) {

                    $('#idDepartamentoEditar').append('<option selected value="' + value.depto_Id + '">' + value.depto_Descripcion + '</option>');
                }
                else {
                    $('#idDepartamentoEditar').append('<option value="' + value.depto_Id + '">' + value.depto_Descripcion + '</option>');
                }
            });

        });

        $('#idMunicipioEditar').empty();

        $.getJSON("/Aldeas/MunicipioList/" + iddepto, function (data) {
            data = JSON.parse(data);
            $.each(data, function (key, value) {
                if (value.muni_Id == idmun) {
                    $('#idMunicipioEditar').empty();
                    $('#idMunicipioEditar').append('<option selected value="' + value.muni_Id + '">' + value.muni_Descripcion + '</option>');
                }
                else {
                    $('#idMunicipioEditar').append('<option  value="' + value.muni_Id + '">' + value.muni_Descripcion + '</option>');
                }


            });

        });

        $('#modalEditar').modal();
    }
}


var idDepto
$('#depto_Id').change(function () {
    var depto_Id = $(this).val();
    $('#muni_Id').empty();

    $.getJSON("/Aldeas/MunicipioList/" + depto_Id, function (data) {
        data = JSON.parse(data);
        $.each(data, function (key, value) {

            $('#muni_Id').append('<option value="' + value.muni_Id + '">' + value.muni_Descripcion + '</option>');

        });
    });
    idDepto = $('#muni_Id').val();

})


$('#idDepartamentoEditar').change(function () {
    var depto_Id = $(this).val();
    $('#idMunicipioEditar').empty();

    $.getJSON("/Aldeas/MunicipioList/" + depto_Id, function (data) {
        data = JSON.parse(data);
        $.each(data, function (key, value) {

            $('#idMunicipioEditar').append('<option value="' + value.muni_Id + '">' + value.muni_Descripcion + '</option>');

        });

    });
    idDepto = $('#idDepartamentoEditar').val();
})


function GuardarAldeas(accion) {
    $('#errorMessage').empty();

    var aldea_Id=0;
    var aldea_Descripcion;
    var muni_Id;
    var depto_Id;
   

    if (accion == 'Guardar') {
        
        aldea_Descripcion = $('#nombreALdea').val();
        muni_Id = $('#muni_Id').val();
        depto_Id = $('#depto_Id').val();
        
    }
    if (accion == 'Editar') {

        aldea_Id = $('#idAldeasEdit').val();
        aldea_Descripcion = $('#nombreALdeaEditar').val();
        muni_Id = $('#idMunicipioEditar').val();
        depto_Id = $('#idDepartamentoEditar').val();

    } if (accion == 'Eliminar') {

        aldea_Id = $('#eliminarId').val();
        aldea_Descripcion = $('#eliminarNombreAldea').val();
        muni_Id = $('#eliminarMunicipio').val();
        depto_Id = $('#eliminarDepartamento').val();

    }

    if (aldea_Descripcion == "") {
        $('#errorMessage').append("El campo es requerido");

    } if (depto_Id == "") {
        $('#errorMessage').append("El campo es requerido");
    } if (depto_Id == "") {
        $('#errorMessage').append("El campo es requerido");
    }

    else {
        var data = {
            aldea_Id: aldea_Id,
            aldea_Descripcion: aldea_Descripcion,
            muni_Id: muni_Id,
            depto_Id: depto_Id,
            accion: accion
        };

        $.ajax({
            url: "/Aldeas/GuardarAjax",
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
                          LimpiarInputs();
                        $('#modalNuevo').modal('hide');
                        
                    }, 2000);
                    break;

                case "Modificado":
                case 1:
                    VotoElectronicoConfig.alert("success", 'Registro actualizado correctamente');

                    window.setTimeout(function () {
                        $('#datatable').DataTable().ajax.reload();
                        $('#modalEditar').modal('hide')
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


const LimpiarInputs = function () {
    $('#muni_Id').val('')
    $('#depto_Id').val('')
    $('#nombreALdea').val('')



function EditarAldeas(aldea_Id) {
    var data = {
        id: aldea_Id
    };
    $.ajax({
        url: "/Aldeas/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {


        if (result != null) {
           
            var idmuni = result.muni_Id;
            var depto_id = result.depto_Id;

            $('#idAldeasEdit').val(result.aldea_Id);
            $('#nombreALdeaEditar').val(result.aldea_Descripcion);
            AldeasDDL('Editar', depto_id, idmuni);
            $('#modalEditar').modal();


        }


    });
}


function EliminarAldeas(aldea_Id) {
    var data = {
        id: aldea_Id
    };
    $.ajax({
        url: "/Aldeas/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {


        if (result != null) {

            $('#eliminarId').val(result.aldea_Id);
            $('#eliminarNombreAldea').val(result.aldea_Descripcion);
            $('#eliminarMunicipio').val(result.muni_Id);
            $('#eliminarDepartamento').val(result.depto_Id);
            $('#ModalEliminar').modal();

        }


    });
}


