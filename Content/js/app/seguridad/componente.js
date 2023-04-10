

//$('#btnNuevo').click(function () {
//    $('#modalNuevo').modal();
//});

function Modal() {
    $('#modalNuevo').modal();
}


function GuardarComponente(accion) {
    $('#errorMessage').empty();

    var nombreComponente;
    var idComponente;
    if (accion == 'Nuevo') {
        idComponente = 0;
       nombreComponente = $('#nombreComponente').val();
    }
    else {
       
         nombreComponente = $('#nombreComponenteEdit').val();
         idComponente   =    $('#idComponenteEdit').val();
    }
  
    if (nombreComponente == "") {
        $('#errorMessage').append("Este campo es requerido");

    } else {
        var data = {
            idComponente: idComponente,
            nombreComponente: nombreComponente
        };

        $.ajax({
            url: "/Componentes/GuardarAjax",
            type: "POST",
            DataType: "json",
            data: data,
        }).done(function (result) {

            if (result == "Ingresado") {
                VotoElectronicoConfig.alert('success', 'Registro ingresado correctamente');
                window.setTimeout(function () {
                    $('#datatable').DataTable().ajax.reload();
                    $('#modalNuevo').modal('hide');

                }, 1000);

            } else if (result == "Modificado") {
                VotoElectronicoConfig.alert('success', 'Registro actualizado correctamente');
                window.setTimeout(function () {
                    $('#datatable').DataTable().ajax.reload();
                    $('#modalEditar').modal('hide');

                }, 1000);
            }

        });
    }
}


function EditarComponente(comp_Id) {
    var data = {
        id: comp_Id
    };
   
    $.ajax({
        url: "/Componentes/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {
       
        
        if (result != null) {
           
            $('#idComponenteEdit').val(result.comp_Id);
            $('#nombreComponenteEdit').val(result.comp_Nombre);
            $('#modalEditar').modal();
           
        }
        
       

    });
}


function DetalleComponente(comp_Id) {
    $('#modaldetalles').modal();
    var data = {
        id: comp_Id
    };
    $.ajax({
        url: "/Componentes/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {
        console.log(result);

        if (result != null) {

            $('#idComponentedetalle').text(result.comp_Id);
            $('#nombreComponentedetalle').text(result.comp_Nombre);  

        }
       

    });
}




function EliminarComponente(comp_Id) {
    $('#ModalEliminar').modal();
    $('#btnSi').click(function () {
        var data = {
            id: comp_Id
        };
        $.ajax({
            url: "/Componentes/DeleteAjax",
            type: "POST",
            DataType: "json",
            data: data,
        }).done(function (result) {
            //console.log(result);

            if (result != null) {

                $('#eliminarId').val(result.comp_Id);
                $('#eliminarComponente').val(result.comp_Nombre);


            }
            if (result == "Eliminado") {
                VotoElectronicoConfig.alert('success', 'Registro Eliminado exitosamente');
                window.setTimeout(function () {
                    $('#datatable').DataTable().ajax.reload();
                    $('#ModalEliminar').modal('hide');
                }, 2000);

            }

        });
    });

}




