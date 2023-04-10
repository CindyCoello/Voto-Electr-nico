function GuardarUsuario(accion) {
    $('#errorMessage').empty();

    var usu_Id;
    var usu_Identidad;
    var usu_PrimerNombre;
    var usu_PrimerApellido;
    var usu_SegundoNombre;
    var usu_SegundoApellido;
    var usu_Telefono;
    var usu_Contraseña;
    var rol_Id;
    var usu_EsActivo;




    if (accion == 'Guardar') {

        usu_Id = 0;
        usu_Identidad = $('#usu_Identidad').val();
        usu_PrimerNombre = $('#usu_PrimerNombre').val();
        usu_PrimerApellido = $('#usu_PrimerApellido').val();
        usu_SegundoNombre = $('#usu_SegundoNombre').val();
        usu_SegundoApellido = $('#usu_SegundoApellido').val();
        usu_Telefono = $('#usu_Telefono').val();
        usu_Contraseña = $('#usu_Contraseña').val();
        rol_Id = $('#rol_Id').val();

    }
    else if (accion == 'Editar') {
        usu_Id = $('#usu_IdEditar').val();
        usu_Identidad = $('#identidadEdit').val();
        usu_PrimerNombre = $('#primerNombreEdit').val();
        usu_PrimerApellido = $('#PrimerApellidoEdit').val();
        usu_SegundoNombre = $('#SegundoNombreEdit').val();
        usu_SegundoApellido = $('#SegundoApellidoEdit').val();
        usu_Telefono = $('#TelefonoEdit').val();
        rol_Id = $('#rol_Id').val();
        usu_EsActivo = $('#usu_EsActivoEdit').val();


    } else if (accion == 'Eliminar') {
        usu_Id = $('#usu_IdDelete').val();
        usu_Identidad = $('#usu_IdentidadDelete').val();
        usu_PrimerNombre = $('#usu_PrimerNombreDelete').val();
        usu_PrimerApellido = $('#usu_PrimerApellidoDelete').val();
        usu_SegundoNombre = $('#usu_SegundoNombreDelete').val();
        usu_SegundoApellido = $('#usu_SegundoApellidoDelete').val();
        usu_Telefono = $('#usu_TelefonoDelete').val();
        usu_Contraseña = $('#usu_ContraseñaDelete').val();
        rol_Id = $('#rol_Id').val();
        usu_EsActivo = $('#usu_EsActivoDelete').val();

    }

    if (usu_Identidad == "") {
        $('#errorMessage').append("El campo es requerido");

    } else if (usu_PrimerNombre == "") {
        $('#errorMessage').append("El campo es requerido");
    }
    else {

        var data = {
            usu_Id: usu_Id,
            usu_Identidad: usu_Identidad,
            usu_PrimerNombre: usu_PrimerNombre,
            usu_PrimerApellido: usu_PrimerApellido,
            usu_SegundoNombre: usu_SegundoNombre,
            usu_SegundoApellido: usu_SegundoApellido,
            usu_Telefono: usu_Telefono,
            usu_Contraseña: usu_Contraseña,
            rol_Id: rol_Id,
            usu_EsActivo: usu_EsActivo,
            accion: accion
        };

        $.ajax({
            url: "/Usuarios/GuardarAjax",
            type: "POST",
            DataType: "json",
            data: data,
        }).done(function(result) {
            switch (result) {

                default:
                case "Ingresado":
                case 0:
                    VotoElectronicoConfig.alert("success", 'Registro ingresado correctamente');


                    window.setTimeout(function() {
                        $('#datatable').DataTable().ajax.reload();
                        $('#modalNuevo').modal('hide');
                    }, 2000);
                    break;

                case "Modificado":
                case 1:
                    VotoElectronicoConfig.alert("success", 'Registro actualizado correctamente');

                    window.setTimeout(function() {
                        $('#datatable').DataTable().ajax.reload();
                        $('#modalNuevo').modal('hide');
                    }, 2000);
                    break;

                case "Eliminado":
                case 2:
                    VotoElectronicoConfig.alert("success", 'Registro eliminado correctamente');

                    window.setTimeout(function() {
                        $('#datatable').DataTable().ajax.reload();
                        $('#modalNuevo').modal('hide');
                    }, 2000);
                    break;


            }


        });
    }
}
