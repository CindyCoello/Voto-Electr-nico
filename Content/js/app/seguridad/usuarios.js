function Modal() {
    LlenarRoles(0);
    $('#modalNuevo').modal();

}

function LlenarRoles(id) {
    $('#rol_Id').empty();
    $.getJSON("/Usuarios/RolesList", function (data) {
        $('#rol_Id').append('<option>Seleccione Un Rol</option>');
        data = JSON.parse(data);
        $.each(data, function (key, value) {
            if (value.rol_Id == id) {
                $('#rol_Id').append('<option selected value="' + value.rol_Id + '">' + value.rol_Nombre + '</option>');
            } else {
                $('#rol_Id').append('<option value="' + value.rol_Id + '">' + value.rol_Nombre + '</option>');
               
            }

        });
    });
}


function GuardarUsuario(accion) {
    $('#errorMessage').empty();

    var usu_Id=0;
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
        rol_Id = $('#rol_IdEditar').val();
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
        rol_Id = $('#rol_IdDelete').val();
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
            passaword: usu_Contraseña,
            rol_Id: rol_Id,
            usu_EsActivo: usu_EsActivo,
            accion: accion

        };
        console.log(data);
        $.ajax({
            url: "/Usuarios/GuardarAjax",
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






function EditarUsuarios(usu_Id) {
    var data = {
        id: usu_Id
    };
    $.ajax({
        url: "/Usuarios/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {


        if (result != null) {
            //result = JSON.parse(result);

            var   usuId = result.usu_Id;
            var   usuIdentidad = result.usu_Identidad;
            var   usuPrimerNombre = result.usu_PrimerNombre;
            var   usuPrimerApellido = result.usu_PrimerApellido;
            var   usuSegundoNombre = result.usu_SegundoNombre;
            var   usuSegundoApellido = result.usu_SegundoApellido;
            var   usuTelefono = result.usu_Telefono;
            var   rolId = result.rol_Id;
            var   usuEsActivo = result.usu_EsActivo;

            $('#usu_Id').val(usuId);
            $('#identidadEdit').val(usuIdentidad);
            $('#primerNombreEdit').val(usuPrimerNombre);
            $('#PrimerApellidoEdit').val(usuPrimerApellido);
            $('#SegundoNombreEdit').val(usuSegundoNombre);
            $('#SegundoApellidoEdit').val(usuSegundoApellido);
            $('#TelefonoEdit').val(usuTelefono);
            LlenarRoles(rolId);
            $('#usu_EsActivoEdit').val(usuEsActivo);
            
            $('#modalEditar').modal();


        }


    });
}


function EliminarUsuarios(usu_Id) {
    var data = {
        id: usu_Id
    };
    
    $.ajax({
        url: "/Usuarios/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {
       
        if (result != null) {
            $('#usu_IdDelete').val(result.usu_Id);
            $('#usu_IdentidadDelete').val(result.usu_Identidad);
            $('#usu_PrimerNombreDelete').val(result.usu_PrimerNombre);
            $('#usu_PrimerApellidoDelete').val(result.usu_PrimerApellido);
            $('#usu_SegundoNombreDelete').val(result.usu_SegundoNombre);
            $('#usu_SegundoApellidoDelete').val(result.usu_SegundoApellido);
            $('#usu_TelefonoDelete').val(result.usu_Telefono);
            $('#usu_ContraseñaDelete').val(result.usu_Contraseña);
            $('#rol_IdDelete').val(result.rol_Id);
            $('#usu_EsActivoDelete').val(result.usu_EsActivo);

        }


    });
}

function EliminarUsuario(usu_Id) {
    $('#ModalEliminar').modal();
    $('#btnSi').click(function () {
        var data = {
            id: usu_Id
        };
        $.ajax({
            url: "/Usuarios/DeleteAjax",
            type: "POST",
            DataType: "json",
            data: data,
        }).done(function (result) {
            //console.log(result);

            if (result != null) {

                $('#usu_IdDelete').val(result.usu_Id);
                $('#usu_IdentidadDelete').val(result.usu_Identidad);
                $('#usu_PrimerNombreDelete').val(result.usu_PrimerNombre);
                $('#usu_PrimerApellidoDelete').val(result.usu_PrimerApellido);
                $('#usu_SegundoNombreDelete').val(result.usu_SegundoNombre);
                $('#usu_SegundoApellidoDelete').val(result.usu_SegundoApellido);
                $('#usu_TelefonoDelete').val(result.usu_Telefono);
                $('#usu_ContraseñaDelete').val(result.usu_Contraseña);
                $('#rol_IdDelete').val(result.rol_Id);
                $('#usu_EsActivoDelete').val(result.usu_EsActivo);

            }
            if (result == "Eliminar") {
                VotoElectronicoConfig.alert('success', 'Registro Eliminado exitosamente');
                window.setTimeout(function () {
                    $('#datatable').DataTable().ajax.reload();
                    $('#ModalEliminar').modal('hide');
                }, 2000);

            }

        });
    });

}
