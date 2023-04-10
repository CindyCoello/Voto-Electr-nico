function GuardarRoles(accion) {
    $('#errorMessage').empty();


    var rol_Id = 0;
    var rol_Nombre;
    var rol_EsActivo;
  


    if (accion == 'Guardar') {
        rol_Nombre = $('#muni_Codigo').val();
        rol_EsActivo = $('#descripcionMunicipio').val();
       

    }
    if (accion == 'Editar') {
        muni_Id = $('#muni_IdEditar').val();
        muni_Codigo = $('#muni_CodigoEditar').val();
        descripcionMunicipio = $('#descripcionMunicipioEditar').val();
        idDepartamento = $('#depto_Id').val();


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
                        $('#modalNuevo').modal('hide');
                    }, 2000);
                    break;

                case "Eliminado":
                case 2:
                    VotoElectronicoConfig.alert("success", 'Registro eliminado correctamente');

                    window.setTimeout(function () {
                        $('#datatable').DataTable().ajax.reload();
                        $('#btnEliminar').modal('hide');
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




            $('#muni_IdEditar').val(muni_Id);
            $('#muni_CodigoEditar').val(result.muni_Codigo);
            $('#descripcionMunicipioEditar').val(muni_Descripcion);
            $('#depto_Id').val(depto_Id);
            LlenarDepartamento(depto_Id);
            $('#modalEditar').modal();


        }


    });
}


function EliminarMunicipios(muni_Id) {
    console.log("entro en la funcion eliminar");
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


            $('#eliminarMuni_Id').val(result.muni_Id);
            $('#eliminar_muni_Codigo').val(result.muni_Codigo);
            $('#eliminarNombreMunicipio').val(result.muni_Descripcion);

            $('#ModalEliminar').modal();

        }


    });
}

