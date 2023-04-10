function Modal() {
    LlenarDepartamento(0);
    LlenarMunicipio(0);
    LlenarPartidos(0);
    LlenarMovimiento(0);
    LlenarTipoCandidato(0);
    LlenarCensoElectoral(0);
    $('#modalNuevo').modal();
}


function LlenarDepartamento(id) {
    $('#depto_Id').empty();
    $.getJSON("/Candidatos/DepartamentoList", function (data) {
        $('#depto_Id').append('<option>Seleccione Un Departamento</option>');
        data = JSON.parse(data);
        $.each(data, function (key, value) {
            if (value.depto_Id == id) {
                $('#depto_IdEdit').append('<option selected value="' + value.depto_Id + '">' + value.depto_Descripcion + '</option>');
            } else {
                $('#depto_Id').append('<option value="' + value.depto_Id + '">' + value.depto_Descripcion + '</option>');
                $('#depto_IdEdit').append('<option value="' + value.depto_Id + '">' + value.depto_Descripcion + '</option>');
            }

        });
        //
    });
}



function LlenarTipoCandidato(id) {
    $('#tipcan_Id').empty();
    $.getJSON("/Candidatos/TipoCandidatoList", function (data) {
        $('#tipcan_Id').append('<option>Seleccione Un tipo candidato</option>');
        data = JSON.parse(data);
        $.each(data, function (key, value) {
            if (value.tipcan_Id == id) {
                $('#tipcan_IdEdit').append('<option selected value="' + value.tipcan_Id + '">' + value.tipcan_Descripcion + '</option>');
            } else {
                $('#tipcan_Id').append('<option value="' + value.tipcan_Id + '">' + value.tipcan_Descripcion + '</option>');
                $('#tipcan_IdEdit').append('<option value="' + value.tipcan_Id + '">' + value.tipcan_Descripcion + '</option>');
            }

        });
        //
    });
}


function LlenarMunicipio(id) {
    $('#muni_IdEdit').empty();
   
    $.getJSON("/Candidatos/MunicipiotoList/", function (data) {
        $('#muni_IdEdit').append('<option>Seleccione Un Municipio</option>');
        data = JSON.parse(data);
        $.each(data, function (key, value) {
            if (value.muni_Id == id) {
                console.log("Entro en el if de municipio");
                $('#muni_IdEdit').append('<option selected value="' + value.muni_Id + '">' + value.muni_Descripcion + '</option>');
            } else {
                console.log("Entro en el ekse de municipio");
                $('#muni_IdEdit').append('<option value="' + value.muni_Id + '">' + value.muni_Descripcion + '</option>');
            }

        });

    });
}


function LlenarMovimiento(id) {
    $('#mov_Id').empty();
    $('#mov_IdEdit').empty();
    $.getJSON("/Candidatos/MovimientoList", function (data) {
        $('#mov_Id').append('<option>Seleccione Un Movimiento</option>');
        data = JSON.parse(data);
        $.each(data, function (key, value) {
            if (value.mov_Id == id) {
                $('#mov_IdEdit').append('<option selected value="' + value.mov_Id + '">' + value.mov_Nombre + '</option>');
            } else {
                $('#mov_Id').append('<option value="' + value.mov_Id + '">' + value.mov_Nombre + '</option>');
            }

        });

    });
}


function LlenarPartidos(id) {
    $('#part_Id').empty();
    console.log("Entro La Funcion");
    $.getJSON("/Candidatos/PartidotoList", function (data) {
        $('#part_Id').append('<option>Seleccione Un Partido</option>');
        data = JSON.parse(data);
        console.log("Entro en el Json");
        $.each(data, function (key, value) {
            if (value.part_Id == id) {
                $('#part_IdEdit').append('<option selected value="' + value.part_Id + '">' + value.part_Nombre + '</option>');
            } else {
                $('#part_Id').append('<option value="' + value.part_Id + '">' + value.part_Nombre + '</option>');
                $('#part_IdEdit').append('<option value="' + value.part_Id + '">' + value.part_Nombre + '</option>');
            }

        });
        //
    });
}

function LlenarTipoCandidato(id) {
    $('#tipcan_Id').empty();
    $.getJSON("/Candidatos/TipoCandidatoList", function (data) {
        $('#tipcan_Id').append('<option>Seleccione Un Tipo de Candidato</option>');
        data = JSON.parse(data);
        $.each(data, function (key, value) {
            if (value.tipcan_Id == id) {
                $('#tipcan_IdEdit').append('<option selected value="' + value.tipcan_Id + '">' + value.tipcan_Descripcion + '</option>');
            } else {
                $('#tipcan_IdEdit').append('<option value="' + value.tipcan_Id + '">' + value.tipcan_Descripcion + '</option>');
                //$('#tipcan_IdEdit').append('<option value="' + value.tipcan_Id + '">' + value.tipcan_Descripcion + '</option>');
            }

        });
        //
    });
}


function LlenarCensoElectoral(id) {
    $('#censo_Id').empty();
    
    $.getJSON("/Candidatos/CensoList", function (data) {
        $('#censo_Id').append('<option>Seleccione Un Censo Electoral</option>');
        data = JSON.parse(data);
        console.log("Entro en el Json");
        $.each(data, function (key, value) {
            if (value.censo_Id == id) {
                $('#censo_IdEdit').append('<option selected value="' + value.censo_Id + '">' + value.censo_Identidad + '</option>');
            } else {
                $('#censo_Id').append('<option value="' + value.censo_Id + '">' + value.censo_Identidad + '</option>');
                $('#censo_IdEdit').append('<option value="' + value.censo_Id + '">' + value.censo_Identidad + '</option>');
            }

        });
        //
    });
}


$('#depto_Id').change(function () {
    $('#muni_Id').empty();
    var depto_Id = $('#depto_Id').val();
    $.getJSON("/Candidatos/MunicipioList/" + depto_Id, function (data) {
        data = JSON.parse(data);
        $.each(data, function (key, value) {
            $('#muni_Id').append('<option value="' + value.muni_Id + '">' + value.muni_Descripcion + '</option>');
        });
        //$('#modalNuevo').modal();
    });
});

$('#depto_IdEdit').change(function () {
    $('#muni_IdEdit').empty();
    var depto_Id = $('#depto_IdEdit').val();
    $.getJSON("/Candidatos/MunicipioList/" + depto_Id, function (data) {
        data = JSON.parse(data);
        $.each(data, function (key, value) {
            $('#muni_IdEdit').append('<option value="' + value.muni_Id + '">' + value.muni_Descripcion + '</option>');
        });
        //$('#modalNuevo').modal();
    });
});



$('#part_Id').change(function () {
    $('#mov_Id').empty();
    var part_Id = $('#part_Id').val();
    $.getJSON("/Candidatos/MoviminetosList/" + part_Id, function (data) {
        data = JSON.parse(data);
        $.each(data, function (key, value) {
            $('#mov_Id').append('<option value="' + value.mov_Id + '">' + value.mov_Nombre + '</option>');
        });
        //$('#modalNuevo').modal();
    });
});

$('#part_IdEdit').change(function () {
    $('#mov_IdEdit').empty();
    var part_Id = $('#part_IdEdit').val();
    $.getJSON("/Candidatos/MoviminetosList/" + part_Id, function (data) {
        data = JSON.parse(data);
        $.each(data, function (key, value) {
            $('#mov_IdEdit').append('<option value="' + value.mov_Id + '">' + value.mov_Nombre + '</option>');
        });
        //$('#modalNuevo').modal();
    });
});



function GuardarCandidatos(accion) {
    $('#errorMessage').empty();
    var cand_id;
    var part_id;
    var mov_id;
    var tipcan_id;
    var censo_id;
    var depto_id;
    var muni_id;
    var cand_posicion;
  

    if (accion == 'Guardar') {

        cand_id = 0;
        part_id = $('#part_Id').val();
        mov_id = $('#mov_Id').val();
        tipcan_id = $('#tipcan_Id').val();
        censo_id = $('#censo_Id').val();
        depto_id = $('#depto_Id').val();
        muni_id = $('#muni_Id').val();
        cand_posicion = $('#cand_Posicion').val();
      
    }
    if (accion == 'Editar') {

        cand_id = $('#cand_IdEdit').val();
        part_id = $('#part_IdEdit').val();
        mov_id = $('#mov_IdEdit').val();
        tipcan_id = $('#tipcan_IdEdit').val();
        censo_id = $('#censo_IdEdit').val();
        depto_id = $('#depto_IdEdit').val();
        muni_id = $('#muni_IdEdit').val();
        cand_posicion = $('#cand_PosicionEdit').val();
    }
    if (accion == 'Eliminar') {

        cand_id = $('#eliminarcand_Id').val();
        part_id = $('#eliminar_part_Id').val();
        mov_id = $('#eliminarmov_Id').val();
        tipcan_id = $('#eliminartipcan_Id').val();
        censo_id = $('#eliminarcenso_Id').val();
        depto_id = $('#eliminardepto_Id').val();
        muni_id = $('#eliminarmuni_Id').val();
        cand_posicion = $('#eliminarcand_Posicion').val();
    }


    if (cand_posicion == "") {
        $('#errorMessage').append("El campo es requerido");
        console.log("Entro en el if");
    } else {
      
        var data = {
            cand_id: cand_id,
            part_id: part_id,
            mov_id: mov_id,
            tipcan_id: tipcan_id,
            censo_id: censo_id,
            depto_id: depto_id,
            muni_id: muni_id,
            cand_posicion: cand_posicion,
            accion: accion

        };
        $.ajax({
            url: "/Candidatos/GuardarAjax",
            type: "Post",
            dataType: "json",
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
};


function EditarCandidatos(cenvot_Id) {
    var data = {
        id: cenvot_Id
    };
    $.ajax({
        url: "/Candidatos/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {

        var cand_Id = result.cand_id;
        var part_Id = result.part_Id
        var mov_Id = result.mov_Id;
        var tipcan_Id = result.tipcan_id;
        var censo_Id = result.censo_id;
        var depto_Id = result.depto_Id;
        var muni_Id = result.muni_Id
        var cand_Posicion = result.cand_Posicion;
        if (result != null) {
            $('#cand_IdEdit').val(cand_Id);
            LlenarPartidos(part_Id);
            LlenarMovimiento(mov_Id);
            LlenarTipoCandidato(tipcan_Id);
            LlenarCensoElectoral(censo_Id);
            LlenarDepartamento(depto_Id);
            LlenarMunicipio(muni_Id);
            $('#cand_PosicionEdit').val(cand_Posicion);
            $('#modalEditar').modal();

        }


    });
}


function EliminarCandidatos(cenvot_Id) {
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