//$('#btnNuevo').click(function () {
//    $('#modalNuevo').modal();
//});

function Modal() {
    $('#modalNuevo').modal();
}


function GuardarDepartamento(accion) {
    $('#errorMessage').empty();
   
    var idDepartamento;
    var nombredepartamento;
    
  
  
    if (accion == 'Guardar') {
        idDepartamento = 0;
        idDepartamento = $('#idDepartamento').val();
        nombredepartamento = $('#nombredepartamento').val();
       
    }
   else if (accion == 'Editar'){

       
         idDepartamento = $('#idDepartamentoEdit').val();
         nombredepartamento = $('#nombredepartamentoEdit').val();
       
    } else if (accion == 'Eliminar') {
       
        idDepartamento = $('#eliminarId').val();
        nombredepartamento = $('#eliminarDepartamento').val();
        
    }

    if (nombredepartamento == "") {
        $('#errorMessage').append("El campo es requerido");

    }
    
    else {
         var data = {
             idDepartamento: idDepartamento,
            nombredepartamento: nombredepartamento,
             accion: accion
        };

         $.ajax({
            url: "/Departamentos/GuardarAjax",
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





function EditarDepartamento(depto_Id) {
    var data = {
        id: depto_Id
    };
    $.ajax({
        url: "/Departamentos/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {


        if (result != null) {

            $('#idDepartamentoEdit').val(result.depto_Id);
            $('#nombredepartamentoEdit').val(result.depto_Descripcion);
            $('#modalEditar').modal();

        }
        

    });
}


function EliminarDepartamento(depto_Id) {
    var data = {
        id: depto_Id
    };
    $.ajax({
        url: "/Departamentos/EditAjax",
        type: "POST",
        DataType: "json",
        data: data,
    }).done(function (result) {


        if (result != null) {

            $('#eliminarId').val(result.depto_Id);
            $('#eliminarDepartamento').val(result.depto_Descripcion);
            $('#ModalEliminar').modal();

        }


    });
}




//function EliminarDepartamento(depto_Id) {
//    $('#ModalEliminar').modal();
//    $('#btnSi').click(function () {
//        var data = {
//            id: depto_Id
//        };
//        $.ajax({
//            url: "/Departamentos/DeleteAjax",
//            type: "POST",
//            DataType: "json",
//            data: data,
//        }).done(function (result) {
//            //console.log(result);

//            if (result != null) {

//                $('#eliminarId').val(result.depto_Id);
//                $('#eliminarDepartamento').val(result.depto_Descripcion);


//            }
//            if (result == "Eliminado") {
//                VotoElectronicoConfig.alert('success', 'Registro Eliminado exitosamente');
//                window.setTimeout(function () {
//                    location.reload();
//                }, 2000);

//            }

//        });
//    });

//}
