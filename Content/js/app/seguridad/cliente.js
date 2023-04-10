$('#btnNuevo').click(function () {
    $('#modalNuevo').modal();
});


function GuardarCliente(accion) {
    $('#errorMessage1').empty();
    $('#errorMessage2').empty();
    $('#errorMessage3').empty();
    $('#errorMessage4').empty();

    var nombreCliente;
    var rtnCliente;
    var telefonoCliente;
    var direccionCliente;
    var idCliente;

    if (accion == 'Nuevo') {
        idCliente = 0;
        nombreCliente = $('#nombreCliente').val();
        rtnCliente = $('#rtnCliente').val();
        telefonoCliente = $('#telefonoCliente').val();
        direccionCliente = $('#direccionCliente').val();
    }
    else {

        nombreCliente = $('#nombreClienteEdit').val();
        rtnCliente = $('#rtnClienteEdit').val();
        telefonoCliente = $('#telefonoClienteEdit').val();
        direccionCliente = $('#direccionClienteEdit').val();
        idCliente = $('#idClienteEdit').val();
    }
   

    if (nombreCliente == "") {
        $('#errorMessage1').append("El campo Nombre es requerido");

    }
    else if (rtnCliente == "") {
        $('#errorMessage2').append("El campo RTN es requerido");

    }
    else if (telefonoCliente == "") {
        $('#errorMessage3').append("El campo Teléfono es requerido");

    }
    else if (direccionCliente == "") {
        $('#errorMessage4').append("El campo Dirección es requerido");

    }else {
        var data = {
            idCliente: idCliente,
            nombreCliente: nombreCliente,
            rtnCliente: rtnCliente,
            telefonoCliente: telefonoCliente,
            direccionCliente: direccionCliente

        };

        $.ajax({
            url: "/Clientes/GuardarAjax",
            type: "POST",
            DataType: "json",
            data: data,
        }).done(function (result) {
            if (result == "Registro ingresado correctamente") {
                location.reload();
            }

        });
    }
}


function EditarCliente(cli_Id) {

    var data = {
        id: cli_Id
        
    };
    $.ajax({
        url: "/Clientes/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {
        //console.log(result);
         if (result != null) {

            $('#idClienteEdit').val(result.cli_Id);
            $('#nombreClienteEdit').val(result.cli_Nombre);
            $('#rtnClienteEdit').val(result.cli_RTN);
            $('#telefonoClienteEdit').val(result.cli_Telefono);
            $('#direccionClienteEdit').val(result.cli_Dirección);
            
            $('#modalEditar').modal();
      
        }
       

    });
}



function EliminarCliente(cli_Id) {

    var data = {
        id: cli_Id

    };
    $.ajax({
        url: "/Clientes/DeleteAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {
        if (result != null) {

            $('#idClienteEdit').val(result.cli_Id);
            $('#nombreClienteEdit').val(result.cli_Nombre);
            $('#rtnClienteEdit').val(result.cli_RTN);
            $('#telefonoClienteEdit').val(result.cli_Telefono);
            $('#direccionClienteEdit').val(result.cli_Dirección);

            $('#modalEliminar').modal();
           

        }
      

    });
}


