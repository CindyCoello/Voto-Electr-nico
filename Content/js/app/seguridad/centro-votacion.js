

function CentroVotacionDDL(accion, iddepto, idmun) {
    if (accion == 'Guardar') {
        $('#depto_Id').empty();
        $.getJSON("/CentrosVotacions/DepartamentoList", function (data) {

            data = JSON.parse(data);
            $.each(data, function (key, value) {
                $('#depto_Id').append('<option value="' + value.depto_Id + '">' + value.depto_Descripcion + '</option>');
            });
            $('#modalNuevo').modal();
        });


    }
    if (accion == 'Editar' && iddepto != 0 && idmun != 0) {
        $('#depto_IdEdit').empty();
        $.getJSON("/CentrosVotacions/DepartamentoList", function (data) {

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

        $.getJSON("/CentrosVotacions/MunicipioList/" + iddepto, function (data) {
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

        $('#modalEditar').modal();
    }
}


var idDepto
$('#depto_Id').change(function () {
    var depto_Id = $(this).val();
    $('#muni_Id').empty();

    $.getJSON("/CentrosVotacions/MunicipioList/" + depto_Id, function (data) {
        data = JSON.parse(data);
        $.each(data, function (key, value) {

            $('#muni_Id').append('<option value="' + value.muni_Id + '">' + value.muni_Descripcion + '</option>');

        });
    });
    idDepto = $('#depto_Id').val();

})

$('#depto_IdEdit').change(function () {
    var depto_Id = $(this).val();
    $('#muni_IdEdit').empty();

    $.getJSON("/CentrosVotacions/MunicipioList/" + depto_Id, function (data) {
        data = JSON.parse(data);
        $.each(data, function (key, value) {

            $('#muni_IdEdit').append('<option value="' + value.muni_Id + '">' + value.muni_Descripcion + '</option>');

        });

    });
    idDepto = $('#depto_IdEdit').val();
})




function GuardarCentroVotacion(accion) {
    $('#errorMessage').empty();


    var cenvot_Id =0;
    var idDepartamento;
    var idMunicipio;
    var codigoArea;
    var codigoSetorElectoral;
    var nombreCentroVotacion;
    var latitud;
    var longitud;
    var totalMesas;


    if (accion == 'Guardar') {
        idDepartamento = $('#depto_Id').val();
        idMunicipio = $('#muni_Id').val();
        codigoArea = $('#codigoArea').val();
        codigoSetorElectoral = $('#codigoSetorElectoral').val();
        nombreCentroVotacion = $('#nombreCentroVotacion').val();
        latitud = $('#latitud').val();
        longitud = $('#longitud').val();
        totalMesas = $('#totalMesas').val();

    }
    if (accion == 'Editar') {
      
        cenvot_Id = $('#Idcenvot_Id').val();
        idDepartamento = $('#depto_IdEdit').val();
        idMunicipio = $('#muni_IdEdit').val();
        codigoArea = $('#codidoAreaEdit').val();
        codigoSetorElectoral = $('#codigoSectorElectoralEdit').val();
        nombreCentroVotacion = $('#nombreCentroVotacionEdit').val();
        latitud = $('#latitudEdit').val();
        longitud = $('#longitudEdit').val();
        totalMesas = $('#totalMesasEdit').val();

    } if (accion == 'Eliminar') {

        cenvot_Id = $('#eliminarcenvot_Id').val();
        idDepartamento = $('#eliminarDepartamento').val();
        idMunicipio = $('#eliminarMunicipio').val();
        codigoArea = $('#eliminarCodigoArea').val();
        codigoSetorElectoral = $('#eliminarCodigoSectorE').val();
        nombreCentroVotacion = $('#eliminarNombreCentroV').val();
        latitud = $('#eliminarlatitud').val();
        longitud = $('#eliminarlongitud').val();
        totalMesas = $('#eliminarTotalM').val();

    }

    if (idDepartamento == "") {
        $('#errorMessage').append("El campo es requerido");
    }
    else if (idMunicipio == "") {
        $('#errorMessage').append("El campo es requerido");
    }   
    else if (codigoArea == "") {
        $('#errorMessage').append("El campo es requerido");
    }   
    else if (codigoSetorElectoral == "") {
        $('#errorMessage').append("El campo es requerido");
    }   
    else if (nombreCentroVotacion == "") {
        $('#errorMessage').append("El campo es requerido");
    }   
    else if (latitud == "") {
        $('#errorMessage').append("El campo es requerido");
    }   
    else if (longitud == "") {
        $('#errorMessage').append("El campo es requerido");
    } 
    else if (totalMesas == "") {
        $('#errorMessage').append("El campo es requerido");
    } 

    else {
        var data = {
            cenvot_Id: cenvot_Id,
            idDepartamento: idDepartamento,
            idMunicipio: idMunicipio,
            codigoArea: codigoArea,
            codigoSetorElectoral: codigoSetorElectoral,
            nombreCentroVotacion: nombreCentroVotacion,
            latitud: latitud,
            longitud: longitud,
            totalMesas: totalMesas,
            accion: accion
        };

        $.ajax({
            url: "/CentrosVotacions/GuardarAjax",
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





function EditarCentroVotacion(cenvot_Id) {
    var data = {
        id: cenvot_Id
    };
    $.ajax({
        url: "/CentrosVotacions/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {


        if (result != null) {

            var idCentroVotacion = result.cenvot_Id;
            var idDept = result.depto_Id;
            var muni = result.muni_Id;
            var codArea = result.cenvot_CodigoArea
            var censoElect = result.cenvot_CodigoSectorElectoral
            var nombreCentroV = result.cenvot_Nombre;
            var latitud = result.cenvot_Latitud;
            var longitud = result.cenvot_Longitud;
            var mesas = result.cenvot_TotalMesas;

            $('#Idcenvot_Id').val(idCentroVotacion);
            CentroVotacionDDL('Editar', idDept, muni);
            $('#codidoAreaEdit').val(codArea);
            $('#codigoSectorElectoralEdit').val(censoElect);
            $('#nombreCentroVotacionEdit').val(nombreCentroV);
            $('#latitudEdit').val(latitud);
            $('#longitudEdit').val(longitud);
            $('#totalMesasEdit').val(mesas);
            

            $('#modalEditar').modal();

        }
        

    });
}


function EliminarCentroVotacion(cenvot_Id) {
    var data = {
        id: cenvot_Id
    };
    $.ajax({
        url: "/CentrosVotacions/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {


        if (result != null) {

            $('#eliminarcenvot_Id').val(result.cenvot_Id);
            $('#eliminarDepartamento').val(result.depto_Id);
            $('#eliminarMunicipio').val(result.muni_Id);
            $('#eliminarCodigoArea').val(result.cenvot_CodigoArea);
            $('#eliminarCodigoSectorE').val(result.cenvot_CodigoSectorElectoral);
            $('#eliminarNombreCentroV').val(result.cenvot_Nombre);
            $('#eliminarlatitud').val(result.cenvot_Latitud);
            $('#eliminarlongitud').val(result.cenvot_Longitud);
            $('#eliminarTotalM').val(result.cenvot_TotalMesas);
             $('#ModalEliminar').modal();

        }


    });
}



