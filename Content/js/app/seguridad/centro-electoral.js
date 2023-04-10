
function EditarCenso(censo_Id) {

    var data = {
        id: censo_Id

    };
    $.ajax({
        url: "/CentroElectoral/MostrarDatosCensoAjax2",
        type: "POST",
        dataType: "json",
        data: data
    }).done(function (data) {
        result = data.data;
        //result = JSON.parse(result);
        if (result != null) {
            depto_IdDDL = result[0].depto_Id;
            muni_IdDDL = result[0].muni_Id;
            estciv_IdDDL = result[0].estciv_Id;
            aldea_IdDDL = result[0].aldea_Id;
            pobla_IdDDL = result[0].pobla_id;
            cenvot_IdDDL = result[0].cenvot_Id;
            $('#idCensoEditar').val(result[0].censo_Id);
            $('#identidadCensoEditar').val(result[0].censo_Identidad);
            $('#primerNombreCensoEditar').val(result[0].censo_PrimerNombre);
            $('#segundoNombreCensoEditar').val(result[0].censo_SegundoNombre);
            $('#primerApellidoCensoEditar').val(result[0].censo_PrimerApellido);
            $('#segundoApellidoCensoEditar').val(result[0].censo_SegundoApellido);

            CensoDDL('Editar', depto_IdDDL, muni_IdDDL, aldea_IdDDL, estciv_IdDDL, pobla_IdDDL, cenvot_IdDDL);

            $('#sexoCensoEditar').val(result[0].censo_CodigoSexo);
            $('#fechaNacCensoEditar').val(result[0].censo_FechaNacimiento);
            $('#codigoAreaCensoEditar').val(result[0].censo_CodigoArea);
            console.log(result[0].censo_CodigoArea);
            $('#sectorElectoralCensoEditar').val(result[0].censo_CodigoSectorElectoral);
            $('#numeroLineaCensoEditar').val(result[0].censo_NumeroLinea);

            $('#ModalEditarCenso').modal();

        }
    });
};

function EliminarCenso(censo_Id) {

    var data = {
        id: censo_Id

    };
    $.ajax({
        url: "/CentroElectoral/MostrarDatosCensoAjax2",
        type: "POST",
        dataType: "json",
        data: data
    }).done(function (data) {
        result = data.data;
        //result = JSON.parse(result);
        if (result != null) {

            $('#idCensoDelete').val(result[0].censo_Id);
            $('#identidadCensoDelete').val(result[0].censo_Identidad);
            //console.log(result.censo_Id);
            $('#ModalDeleteCenso').modal();

        }
    });

}




function CensoDDL(accion, iddepto, idmun, idaldea, idestado, idpoblado, idcentro) {
    if (accion == 'Nuevo') {
        $('#depto_IdNuevo').empty();
        $.getJSON("/CentroElectoral/DeptoList", function (data) {

            //data = JSON.parse(data);
            $.each(data.data, function (key, value) {
                $('#depto_IdNuevo').append('<option value="' + value.depto_Id + '">' + value.depto_Descripcion + '</option>');
            });

        });

        $('#estado_IdNuevo').empty();
        $.getJSON("/CentroElectoral/Estadolist", function (data) {

            //data = JSON.parse(data);
            $.each(data.data, function (key, value) {
                $('#estado_IdNuevo').append('<option value="' + value.estCiv_Id + '">' + value.estCiv_Descripcion + '</option>');
            });

        });
        $('#ModalNuevoCenso').modal();

    }
    if (accion == 'Editar' && iddepto != 0 && idmun != 0 && idaldea != 0 && idestado != 0 && idpoblado != 0 && idcentro != 0) {
        $('#estado_IdEditar').empty();
        $.getJSON("/CentroElectoral/Estadolist", function (data) {

            //data = JSON.parse(data);
            $.each(data.data, function (key, value) {
                if (value.estCiv_Id == idestado) {

                    $('#estado_IdEditar').append('<option selected value="' + value.estCiv_Id + '">' + value.estCiv_Descripcion + '</option>');
                }
                else {
                    $('#estado_IdEditar').append('<option value="' + value.estCiv_Id + '">' + value.estCiv_Descripcion + '</option>');
                }
            });
        });

        $('#depto_IdEditar').empty();
        $.getJSON("/CentroElectoral/DeptoList", function (data) {

            //data = JSON.parse(data);
            $.each(data.data, function (key, value) {
                if (value.depto_Id == iddepto) {

                    $('#depto_IdEditar').append('<option selected value="' + value.depto_Id + '">' + value.depto_Descripcion + '</option>');
                }
                else {
                    $('#depto_IdEditar').append('<option value="' + value.depto_Id + '">' + value.depto_Descripcion + '</option>');
                }
            });
        });

        $('#muni_IdEditar').empty();

        $.getJSON("/CentroElectoral/MunicipiosList/" + iddepto, function (data) {
            //data = JSON.parse(data);
            $.each(data.data, function (key, value) {
                if (value.muni_Id == idmun) {

                    $('#muni_IdEditar').append('<option selected value="' + value.muni_Id + '">' + value.muni_Descripcion + '</option>');
                }
                else {
                    $('#muni_IdEditar').append('<option  value="' + value.muni_Id + '">' + value.muni_Descripcion + '</option>');
                }


            });

        });

        $('#centro_IdEditar').empty();
        $.getJSON("/CentroElectoral/CentroList/" + iddepto, { muni_Id: idmun }, function (data) {
            //data = JSON.parse(data);
            $.each(data.data, function (key, value) {
                if (value.cenvot_Id == idcentro) {

                    $('#centro_IdEditar').append('<option selected value="' + value.cenvot_Id + '">' + value.cenvot_Nombre + '</option>');
                }
                else {
                    $('#centro_IdEditar').append('<option  value="' + value.cenvot_Id + '">' + value.cenvot_Nombre + '</option>');
                }
            });
        });

        $('#aldea_IdEditar').empty();
        $.getJSON("/CentroElectoral/AldeasList/" + iddepto, { muni_Id: idmun }, function (data) {
            //data = JSON.parse(data);
            $.each(data.data, function (key, value) {
                if (value.aldea_Id == idaldea) {

                    $('#aldea_IdEditar').append('<option selected value="' + value.aldea_Id + '">' + value.aldea_Descripcion + '</option>');
                }
                else {
                    $('#aldea_IdEditar').append('<option  value="' + value.aldea_Id + '">' + value.aldea_Descripcion + '</option>');
                }
            });
        });

        $('#poblado_IdEditar').empty();
        $.getJSON("/CentroElectoral/PobladoList/" + iddepto, { idmunicipio: idmun, idaldea: idaldea }, function (data) {
            //data = JSON.parse(data);
            $.each(data.data, function (key, value) {
                if (value.pobl_Id == idpoblado) {

                    $('#poblado_IdEditar').append('<option selected value="' + value.pobl_Id + '">' + value.pobl_Descripcion + '</option>');
                }
                else {
                    $('#poblado_IdEditar').append('<option  value="' + value.pobl_Id + '">' + value.pobl_Descripcion + '</option>');
                }
            });
        });

    }
}



var idDepto
$('#depto_IdNuevo').change(function () {
    var depto_Id = $(this).val();
    $('#muni_IdNuevo').empty();
    $('#aldea_IdNuevo').empty();
    $.getJSON("/CentroElectoral/MunicipiosList/" + depto_Id, function (data) {
        //data = JSON.parse(data);
        $.each(data.data, function (key, value) {

            $('#muni_IdNuevo').append('<option value="' + value.muni_Id + '">' + value.muni_Descripcion + '</option>');

        });
    });
    idDepto = $('#depto_IdNuevo').val();
    console.log(idDepto);
})

$('#depto_IdEditar').change(function () {
    var depto_Id = $(this).val();
    $('#muni_IdEditar').empty();

    $.getJSON("/CentroElectoral/MunicipiosList/" + depto_Id, function (data) {
        //data = JSON.parse(data);
        $.each(data.data, function (key, value) {

            $('#muni_IdEditar').append('<option value="' + value.muni_Id + '">' + value.muni_Descripcion + '</option>');

        });

    });
    idDepto = $('#depto_IdEditar').val();
})

var idMun

$('#muni_IdNuevo').change(function () {
    var depto_Id = $('#depto_IdNuevo').val();
    var muni_Id = $(this).val();
    $('#aldea_IdNuevo').empty();

    $.getJSON("/CentroElectoral/AldeasList/" + depto_Id, { muni_Id: muni_Id }, function (data) {
        //data = JSON.parse(data);
        //console.log(data);
        $.each(data.data, function (key, value) {

            $('#aldea_IdNuevo').append('<option value="' + value.aldea_Id + '">' + value.aldea_Descripcion + '</option>');

        });


    });
    $('#centro_IdNuevo').empty();
    $.getJSON("/CentroElectoral/CentroList/" + depto_Id, { muni_Id: muni_Id }, function (data) {
        //data = JSON.parse(data);
        //console.log(data);
        $.each(data.data, function (key, value) {

            $('#centro_IdNuevo').append('<option value="' + value.cenvot_Id + '">' + value.cenvot_Nombre + '</option>');

        });

    });

    idMun = $('#muni_IdNuevo').val();
    console.log(idMun);
});

$('#muni_IdEditar').change(function () {
    var depto_Id = $('#depto_IdEditar').val();
    var muni_Id = $(this).val();
    $('#aldea_IdEditar').empty();
    $.getJSON("/CentroElectoral/AldeasList/" + depto_Id, { muni_Id: muni_Id }, function (data) {
        //data = JSON.parse(data);
        $.each(data.data, function (key, value) {

            $('#aldea_IdEditar').append('<option value="' + value.aldea_Id + '">' + value.aldea_Descripcion + '</option>');

        });

    });
    $('#centro_IdEditar').empty();
    $.getJSON("/CentroElectoral/CentroList/" + depto_Id, { muni_Id: muni_Id }, function (data) {
        //data = JSON.parse(data);
        //console.log(data);
        $.each(data.data, function (key, value) {

            $('#centro_IdEditar').append('<option value="' + value.cenvot_Id + '">' + value.cenvot_Nombre + '</option>');

        });

    });
    idMun = $('#muni_IdEditar').val();
})


var idAldea
$('#aldea_IdNuevo').change(function () {
    var departamento = $('#depto_IdNuevo').val();
    var municipio = $('#muni_IdNuevo').val();
    //var aldea_Id = $('#aldea_IdNuevo').val();
    var aldea = $(this).val();
    $('#poblado_IdNuevo').empty();

    $.getJSON("/CentroElectoral/PobladoList/" + departamento, { idmunicipio: municipio, idaldea: aldea }, function (data) {
        //data = JSON.parse(data);

        $.each(data.data, function (key, value) {

            $('#poblado_IdNuevo').append('<option value="' + value.pobl_Id + '">' + value.pobl_Descripcion + '</option>');

        });

    });
    idAldea = $('#aldea_IdNuevo').val();

    console.log(departamento);
    console.log(municipio);
    console.log(aldea);


})
$('#aldea_IdEditar').change(function () {

    var depto_Id = $('#depto_IdEditar').val();
    var muni_Id = $('#muni_IdEditar').val();
    var aldea_Id = $(this).val();
    $('#poblado_IdEditar').empty();
    $.getJSON("/CentroElectoral/PobladoList/" + depto_Id, { idmunicipio: muni_Id, idaldea: aldea_Id }, function (data) {
        //data = JSON.parse(data);

        $.each(data.data, function (key, value) {

            $('#poblado_IdEditar').append('<option value="' + value.pobl_Id + '">' + value.pobl_Descripcion + '</option>');

        });

    });
    idAldea = $('#aldea_IdEditar').val();
})

var idPoblado
$('#poblado_IdNuevo').change(function () {
    idPoblado = $('#poblado_IdNuevo').val();
})
$('#poblado_IdEditar').change(function () {
    idPoblado = $('#poblado_IdEditar').val();
})

var idEstado
$('#estado_IdNuevo').change(function () {
    idEstado = $('#estado_IdNuevo').val();
})
$('#estado_IdEditar').change(function () {
    idEstado = $('#estado_IdEditar').val();
})


var idCentro
$('#centro_IdNuevo').change(function () {
    idCentro = $('#centro_IdNuevo').val();
})
$('#centro_IdEditar').change(function () {
    idCentro = $('#centro_IdEditar').val();
})





function GuardarCenso(accion) {
    $('#errorMessage').empty();
    var idCenso;
    var identidadCenso;
    var primerNombreCenso;
    var segundoNombreCenso;
    var primerApellidoCenso;
    var segundoApellidoCenso;
    var sexoCenso;
    var fechaNacCenso;
    //DDL
    var codigoAreaCenso;
    var sectorElectoralCenso;
    var numeroLineaCenso;
    var esHabilitadoCenso;

    //var idDepto;

    if (accion == 'Nuevo') {

        idCenso = 0;
        identidadCenso = $('#identidadCenso').val();
        primerNombreCenso = $('#primerNombreCenso').val();
        segundoNombreCenso = $('#segundoNombreCenso').val();
        primerApellidoCenso = $('#primerApellidoCenso').val();
        segundoApellidoCenso = $('#segundoApellidoCenso').val();
        sexoCenso = $('#sexoCenso').val();
        fechaNacCenso = $('#fechaNacCenso').val();
        idEstado = $('#estado_IdNuevo').val();
        idDepto = $('#depto_IdNuevo').val();
        idMun = $('#muni_IdNuevo').val();
        idAldea = $('#aldea_IdNuevo').val();
        idPoblado = $('#poblado_IdNuevo').val();
        idCentro = $('#centro_IdNuevo').val();
        codigoAreaCenso = $('#codigoAreaCenso').val();
        sectorElectoralCenso = $('#sectorElectoralCenso').val();
        numeroLineaCenso = $('#numeroLineaCenso').val();
        esHabilitadoCenso = true;


    }
    if (accion == 'Editar') {

        idCenso = $('#idCensoEditar').val();;
        identidadCenso = $('#identidadCensoEditar').val();
        primerNombreCenso = $('#primerNombreCensoEditar').val();
        segundoNombreCenso = $('#segundoNombreCensoEditar').val();
        primerApellidoCenso = $('#primerApellidoCensoEditar').val();
        segundoApellidoCenso = $('#segundoApellidoCensoEditar').val();
        sexoCenso = $('#sexoCensoEditar').val();
        fechaNacCenso = $('#fechaNacCensoEditar').val();
        idEstado = $('#estado_IdEditar').val();
        idDepto = $('#depto_IdEditar').val();
        idMun = $('#muni_IdEditar').val();
        idAldea = $('#aldea_IdEditar').val();
        idPoblado = $('#poblado_IdEditar').val();
        idCentro = $('#centro_IdEditar').val();
        codigoAreaCenso = $('#codigoAreaCensoEditar').val();
        console.log(codigoAreaCenso)
        sectorElectoralCenso = $('#sectorElectoralCensoEditar').val();
        numeroLineaCenso = $('#numeroLineaCensoEditar').val();

    }
    if (accion == 'Delete') {
        idCenso = $('#idCensoDelete').val();
        identidadCenso = $('#identidadCensoDelete').val();
        esHabilitadoCenso = false;
        console.log(idCenso)
    }

    if (identidadCenso == "") {
        $('#errorMessage').append("El campo es requerido");

    } else {
        var data = {
            Censoid: idCenso,
            identidadCenso: identidadCenso,
            primerNombreCenso: primerNombreCenso,
            segundoNombreCenso: segundoNombreCenso,
            primerApellidoCenso: primerApellidoCenso,
            segundoApellidoCenso: segundoApellidoCenso,
            sexoCenso: sexoCenso,
            fechaNacCensoE: fechaNacCenso,
            idEstado: idEstado,
            idDepto: idDepto,
            idMun: idMun,
            idAldea: idAldea,
            idPoblado: idPoblado,
            codigoAreaCensoE: codigoAreaCenso,
            sectorElectoralCensoE: sectorElectoralCenso,
            idCentro: idCentro,
            numeroLineaCensoE: numeroLineaCenso,
            esHabilitadoCenso: esHabilitadoCenso,

            accion: accion
        };
        $.ajax({
            url: "/CentroElectoral/GuardarCensoAjax",
            type: "POST",
            dataType: "json",
            data: data
        }).done(function (accion) {

            switch (accion) {

                default:
                case "Nuevo":
                    VotoElectronicoConfig.alert("success", 'Registro ingresado correctamente')
                    $('#ModalNuevoCenso').modal('hide');
                    $('#datatableCenso').DataTable().ajax.reload();
                    //window.setTimeout(function () {
                    //    location.reload();
                    //}, delay);
                    break;

                case "Editar":

                    VotoElectronicoConfig.alert("success", 'Registro actualizado correctamente');
                    $('#ModalEditarCenso').modal('hide');
                    $('#datatableCenso').DataTable().ajax.reload();

                    break;

                case "DeleteLogic":
                    VotoElectronicoConfig.alert("info", 'Registro eliminado correctamente');
                    $('#ModalDeleteCenso').modal('hide');
                    $('#datatableCenso').DataTable().ajax.reload();

                    break;
            }

        });
    }
}