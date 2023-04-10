
function MunicipiosDDL(accion, idDepto, idmun, mum_descrip) {
    if (accion == 'Guardar') {
        $('#depto_Id').empty();
        $.getJSON("/Municipios/DepartamentosList", function (data) {

            data = JSON.parse(data);
            $.each(data, function (key, value) {
                $('#depto_Id').append('<option value="' + value.depto_Id + '">' + value.depto_Descripcion + '</option>');
            });
            $('#modalNuevo').modal();
        });


    }
    if (accion == 'Editar' && idDepto != 0 && idmun != 0 && mum_descrip!=0) {
        $('#depto_IdEditar').empty();
        $.getJSON("/Municipios/DepartamentosList", function (data) {

            data = JSON.parse(data);
            $.each(data, function (key, value) {
                if (value.depto_Id == idDepto) {

                    $('#depto_IdEditar').append('<option selected value="' + value.depto_Id + '">' + value.depto_Descripcion + '</option>');
                 
                }
                else {
                    $('#depto_IdEditar').append('<option value="' + value.depto_Id + '">' + value.depto_Descripcion + '</option>');
                }
            });
            elegir(mum_descrip)

        });

    }
}
function elegir(mum_descrip) {
    $("#descripcionMunicipioEditar option[value=" + mum_descrip + "]").attr("selected", true);
}

     



function GuardarMunicipios(accion) {

    $('#errorMessage').empty();

   
    var muni_Id = 0;
    var muni_Codigo;
    var descripcionMunicipio;
    var idDepartamento;


    if (accion == 'Guardar') {
        muni_Codigo = $('#muni_Codigo').val();
        descripcionMunicipio = $('#descripcionMunicipio').val();
        idDepartamento = $('#depto_Id').val();
        
    }
    if (accion == 'Editar') {
        muni_Id = $('#muni_IdEditar').val();
        muni_Codigo = $('#muni_CodigoEditar').val();
        descripcionMunicipio = $('#descripcionMunicipioEditar').val();
        idDepartamento = $('#depto_IdEditar').val();
        

    } if (accion == 'Eliminar') {
        muni_Id = $('#eliminarMuni_Id').val();
        muni_Codigo = $('#eliminar_muni_Codigo').val();
        descripcionMunicipio = $('#eliminarNombreMunicipio').val();
        idDepartamento = $('#eliminarDepartamento').val();
       

    }

    if (muni_Codigo == "") {
        $('#errorMessage').append("El campo es requerido");

    } else if (descripcionMunicipio == "") {
        $('#errorMessage').append("El campo es requerido");
    } else if (idDepartamento == "") {
        $('#errorMessage').append("El campo es requerido");
    }
    
    else {
      
        var data = {
            muni_Id: muni_Id,
            muni_Codigo: muni_Codigo,
            descripcionMunicipio: descripcionMunicipio,
            idDepartamento: idDepartamento,
            accion: accion
        };

        $.ajax({
            url: "/Municipios/GuardarAjax",
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





function EditarMunicipios(muni_Id) {
    var data = {
        id: muni_Id
    };
    $.ajax({
        url: "/Municipios/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {


        if (result != null) {


            var min_id = result.muni_Id;
            var muni_cod = result.muni_Codigo;
            var muni_descrip = result.muni_Descripcion;
            var depto_id = result.depto_Id;

            $('#muni_IdEditar').val(min_id);
            $('#muni_CodigoEditar').val(muni_cod);
            $('#descripcionMunicipioEditar').val(result.muni_Descripcion);
            $('#depto_IdEditar').val(depto_id);
            MunicipiosDDL('Editar', depto_id, muni_cod, muni_descrip);
           
            $('#modalEditar').modal();

            
        }


    });
}


function EliminarMunicipios(muni_Id) {
    var data = {
        id: muni_Id
    };
    $.ajax({
        url: "/Municipios/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {


        if (result != null) {

            $('#eliminarMuni_Id').val(result.muni_Codigo);
            $('#eliminar_muni_Codigo').val(result.muni_Id);
            $('#eliminarNombreMunicipio').val(result.muni_Descripcion);
            $('#eliminarDepartamento').val(result.depto_Id);
            $('#ModalEliminar').modal();

        }


    });
}

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            