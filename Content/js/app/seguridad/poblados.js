
function PobladosDDL(accion, iddepto, idmun, idaldea) {
    if (accion == 'Guardar') {
        $('#depto_IdNuevo').empty();
        $.getJSON("/Poblados/DeptoList", function (data) {

            data = JSON.parse(data);
            $.each(data, function (key, value) {
                $('#depto_IdNuevo').append('<option value="' + value.depto_Id + '">' + value.depto_Descripcion + '</option>');
            });
            $('#modalNuevo').modal();
        });


    }
    if (accion == 'Editar' && iddepto != 0 && idmun != 0 && idaldea != 0) {
        $('#depto_IdEdit').empty();
        $.getJSON("/Poblados/DeptoList", function (data) {

            data = JSON.parse(data);
            $.each(data, function (key, value) {
                if (value.depto_Id == iddepto) {

                    $('#depto_IdEdit').append('<option selected value="' + value.depto_Id + '">' + value.depto_Descripcion + '</option>');
                }
                else {
                    $('#depto_IdEdit').append('<option value="' + value.depto_Id + '">' + value.depto_Descripcion + '</option>');
                }
            });

        });

        $('#muni_IdEdit').empty();

        $.getJSON("/Poblados/MunicipiosList/" + iddepto, function (data) {
            data = JSON.parse(data);
            $.each(data, function (key, value) {
                if (value.muni_Id == idmun) {
                    $('#muni_IdEdit').empty();
                    $('#muni_IdEdit').append('<option selected value="' + value.muni_Id + '">' + value.muni_Descripcion + '</option>');
                }
                else {
                    $('#muni_IdEdit').append('<option  value="' + value.muni_Id + '">' + value.muni_Descripcion + '</option>');
                }


            });

        });

        $('#aldea_IdEdit').empty();
        $.getJSON("/Poblados/AldeasList/" + iddepto, { muni_Id: idmun }, function (data) {
            data = JSON.parse(data);
            $.each(data, function (key, value) {
                if (value.aldea_Id == idaldea) {
                    $('#aldea_IdEdit').empty();
                    $('#aldea_IdEdit').append('<option selected value="' + value.aldea_Id + '">' + value.aldea_Descripcion + '</option>');
                }
                else {
                    $('#aldea_IdEdit').append('<option  value="' + value.aldea_Id + '">' + value.aldea_Descripcion + '</option>');
                }

            });

        });
        $('#modalEditar').modal();
    }
}



//capturar el valor del departamento
var idDepto
$('#depto_IdNuevo').change(function () {
    var depto_Id = $(this).val();
    $('#muni_IdNuevo').empty();

    $.getJSON("/Poblados/MunicipiosList/" + depto_Id, function (data) {
        data = JSON.parse(data);
        $.each(data, function (key, value) {

            $('#muni_IdNuevo').append('<option value="' + value.muni_Id + '">' + value.muni_Descripcion + '</option>');

        });
    });
    idDepto = $('#depto_IdNuevo').val();
  
})

$('#depto_IdEdit').change(function () {
    var depto_Id = $(this).val();
    $('#muni_IdEdit').empty();

    $.getJSON("/Poblados/MunicipiosList/" + depto_Id, function (data) {
        data = JSON.parse(data);
        $.each(data, function (key, value) {

            $('#muni_IdEdit').append('<option value="' + value.muni_Id + '">' + value.muni_Descripcion + '</option>');

        });

    });
    idDepto = $('#depto_IdEdit').val();
})

//Capturar el valor del municipio
var idMun
$('#muni_IdNuevo').change(function () {
    var depto_Id = $('#depto_IdNuevo').val();
    var muni_Id = $(this).val();
    $('#aldea_IdNuevo').empty();
    $.getJSON("/Poblados/AldeasList/" + depto_Id, { muni_Id: muni_Id }, function (data) {
        data = JSON.parse(data);
      
        $.each(data, function (key, value) {

            $('#aldea_IdNuevo').append('<option value="' + value.aldea_Id + '">' + value.aldea_Descripcion + '</option>');

        });

    });

    idMun = $('#muni_IdNuevo').val();
    //console.log(idMun);
});

$('#muni_IdEdit').change(function () {
    var depto_Id = $('#depto_IdEdit').val();
    var muni_Id = $(this).val();
    $('#aldea_IdEdit').empty();
    $.getJSON("/Poblados/AldeasList/" + depto_Id, { muni_Id: muni_Id }, function (data) {
      
        $.each(data, function (key, value) {

            $('#aldea_IdEdit').append('<option value="' + value.aldea_Id + '">' + value.aldea_Descripcion + '</option>');

        });

    });
    idMun = $('#muni_IdEdit').val();
})


//Capturar el valor de la aldea
var idAldea;
$('#aldea_IdNuevo').change(function () {
    idAldea = $('#aldea_IdNuevo').val();
})

$('#aldea_IdEdit').change(function () {
    idAldea = $('#aldea_IdEdit').val();
})



function GuardarPoblados(accion) {
    $('#errorMessage').empty();
    var idPobla;
    var nombrePobla;


    if (accion == 'Guardar') {

        idPobla = 0;
        nombrePobla = $('#nombrePoblado').val();
        idAldea = $('#aldea_IdNuevo').val();
        idMun = $('#muni_IdNuevo').val();
        idDepto = $('#depto_IdNuevo').val();

        //console.log(idAldea, idMun, idDepto);

    }
    if (accion == 'Editar') {

        idPobla = $('#idPobladoEdit').val();
        nombrePobla = $('#nombrePobladoEdit').val();
        idAldea = $('#aldea_IdEdit').val();
        idMun = $('#muni_IdEdit').val();
        idDepto = $('#depto_IdEdit').val();

    }
    if (accion == 'Eliminar') {
        idPobla = $('#idPobladoEliminar').val();
        nombrePobla = $('#nombrePobladoEliminar').val();
    }

    if (nombrePobla == "") {
        $('#errorMessage').append("El campo es requerido");

    } else {
        var data = {
            idPobla: idPobla,
            nombrePobla: nombrePobla,
            idAldea: idAldea,
            idMun: idMun,
            idDepto: idDepto,
            accion: accion
        };
        $.ajax({
            url: "/Poblados/GuardarPobladosAjax",
            type: "POST",
            dataType: "json",
            data: data
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



function EditarPoblados(pobl_Id) {

    var data = {
        id: pobl_Id

    };
    $.ajax({
        url: "/Poblados/EditAjax",
        type: "POST",
        dataType: "json",
        data: data
    }).done(function (result) {
        //result = JSON.parse(result);
        if (result != null) {
           
            depto_IdDDL = result.depto_Id;
            muni_IdDDL = result.muni_Id;
            aldea_IdDDL = result.aldea_Id;
            $('#idPobladoEdit').val(result.pobl_Id);
            $('#nombrePobladoEdit').val(result.pobl_Descripcion);
            PobladosDDL('Editar', depto_IdDDL, muni_IdDDL, aldea_IdDDL);
            $('#modalEditar').modal();

        }
    });
};

function EliminarPoblados(pobl_Id) {

    var data = {
        id: pobl_Id

    };
    $.ajax({
        url: "/Poblados/EditAjax",
        type: "POST",
        dataType: "json",
        data: data
    }).done(function (result) {
        //result = JSON.parse(result);
        if (result != null) {

            $('#idPobladoEliminar').val(result.pobl_Id);
            $('#nombrePobladoEliminar').val(result.pobl_Descripcion);
            $('#ModalEliminar').modal();

        }
    });

}



