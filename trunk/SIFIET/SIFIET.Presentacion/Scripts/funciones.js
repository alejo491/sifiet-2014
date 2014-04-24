/*$(document).ready(function () {
    alert('Pagina Cargada Exitosamente');
});*/

function comfirmarAgregarRol() {
    var nombre = document.getElementById("NOMROL").value;
    var descripcion = document.getElementById("DESCROL").value;
    var mensaje;
    if (nombre.trim() == "" || descripcion.trim() == "") {
        mensaje = "Hay datos que son requeridos para poder guardar el registro,\n por favor diligencie todos los campos";
        alert(mensaje);
        return true;
    } else {
        mensaje = 'Nombre: ' + nombre + '\n'
            + 'Descripcion:' + descripcion + '\n\n'
            + 'El Rol Puede: \n'
            + document.getElementById("Plan de Estudios").value + " Plan de Estudios |\t" + document.getElementById("Usuarios").value + ' Usuarios\n'
            + document.getElementById("Programas").value + " Programas |\t" + document.getElementById("Infraestructura").value + ' Infraestructura\n'
            + document.getElementById("Asignaturas").value + " Asignaturas |\t" + document.getElementById("Salones").value + ' Salones\n';
        return confirm(mensaje);
    }
}

function mensajeNoRoles() {
    $(document).ready(function() { alert("No se ha encontrado ningun Rol con la informacion\que se ha ingresado, por favor intentelo nuevamente"); });
}



function confirmacionAgregarUsuario() {
    var nombresUsuario = document.getElementById("NOMBRESUSUARIO").value;
    var apellidosUsuario = document.getElementById("APELLIDOSUSUARIO").value;
    var identificacionUsuario = document.getElementById("IDENTIFICACIONUSUARIO").value;
    var passwordUsuario = document.getElementById("PASSWORDUSUARIO").value;
    var emailInstitucionalUsuario = document.getElementById("EMAILINSTITUCIONALUSUARIO").value;
    var roles = document.getElementsByName("roles");

    /*var result = "";
    var opciones = roles.options;
    var opt;

    for (var i = 0, iLen = opciones.length; i < iLen; i++) {
        opt = opciones[i];

        if (opt.selected) {
            result = result + "\n" + opt.text;
        }
    }*/

  

    if (nombresUsuario != "" && apellidosUsuario != "" && identificacionUsuario != "" && passwordUsuario != "" && emailInstitucionalUsuario != "" ) {
        var mensaje = "¿Desea registrar el usuario con la siguiente información? \n\n Nombres: " + nombresUsuario + "\n\n Apellidos: " + apellidosUsuario + "\n\n Identificación: " + identificacionUsuario + "\n\n Password: " + passwordUsuario + "\n\n Email: " + emailInstitucionalUsuario + "\n\n Roles: ";
        return confirm(mensaje);
    }
    else {
        return false;
    }
}

function confirmSalirRol() {
    var r = confirm('¿Confirma que desea cacelar la accion?\nTodos los datos se perderan');
    var url = window.location.pathname;
    var pathArray = url.split('/');        // <-- no need in "string()"
    var host = pathArray[0];
    var newHost = '/rol';
    if (r == true) {
        window.location = host + newHost;
    }
    return false;
}

function mensajeNoUsuarios() {
    $(document).ready(function () { alert("No se han encontrado registros con el dato indicado, por favor intentelo de nuevo"); });
}

function getSelectValues(select) {
    var result = [];
    var opciones = select.options;
    var opt;

    for (var i = 0, iLen = opciones.length; i < iLen; i++) {
        opt = opciones[i];

        if (opt.selected) {
            result.push(opt.text);
        }
    }
    return result;
}
function confirmacionAgregarAsignatura() {
    /*
    var idAsignatura = document.getElementById("IDASIGNATURA").value;
    var idPlanEstudio = document.getElementById("IDPLANESTUDIOS").value;
    var nombreAsignatura = document.getElementById("NOMADIGNATURA").value;
    //var correquisitos = document.getElementById("CORREQUISITOSASIGNATURA").value;
    //var prerequisitos = document.getElementById("PREREQUISITOSASIGNATURA").value;
    var semestre = document.getElementById("SEMESTREASIGNATURA").value;
    var intensidadhoraria = document.getElementById("INTENSIDADHORARIA").value;
    var modalidad = document.getElementById("MODALIDAD").value;
    var clasificacion = document.getElementById("CLASIFICACION").value;
    var estado = document.getElementById("ESTADOASIGNATURA").value;
    */

    //if (idAsignatura != "" && idPlanEstudio != "" && nombreAsignatura != "" && semestre != "" && intensidadhoraria != "" && modalidad != "" && clasificacion != "" && estado != "") {
        //var mensaje = "¿Desea registrar la Asignatura con la siguiente información? \n\n Codigo: " + idAsignatura + "\n\n Plan Estudio: " + idPlanEstudio + "\n\n Nombre: " + nombreAsignatura + "\n\n Correquisitos: " + correquisitos + "\n\n Prerequisitos: " + prerequisitos + "\n\n Semestre: " + semestre + "\n\n Intensidad Horaria: " + intensidadhoraria + "\n\n Modalidad: " + modalidad + "\n\n Clasificacion: " + clasificacion + "\n\n Estado: " + estado;
        var mensaje = "¿Desea guardar la Asignatura con la siguiente información?";
        return confirm(mensaje);
    //}
    //else {
      //  return false;
    //}
}
function confirmSalirAsignatura() {
    var r = confirm('¿Confirma que desea cancelar la accion?');
    var url = window.location.pathname;
    var pathArray = url.split('/');        // <-- no need in "string()"
    var host = pathArray[0];
    var newHost = '/Asignatura/ConsultarAsignaturas';
    if (r == true) {
        window.location = host + newHost;
    }
    return false;
}
function confirmarEliminarAsignatura() {
    var mensaje = "Esta asignatura tiene relacion con registros de la base de datos \n ¿Confirma que desea eliminar la Asignatura?";
    return confirm(mensaje);
}
