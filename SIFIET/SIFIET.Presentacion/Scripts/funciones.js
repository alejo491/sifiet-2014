function cargarMultiselectDocument() {
    $(document).ready(function () {
        cargarMultiselect();
    });
}


function comfirmarAgregarRol() {
    var nombre = document.getElementById("NOMBREROL").value;
    var descripcion = document.getElementById("DESCRIPCIONROL").value;
    var mensaje;
    if (nombre.trim() == "" || descripcion.trim() == "") {
        mensaje = "Hay datos que son requeridos para poder guardar el registro,\n por favor diligencie todos los campos";
        alert(mensaje);
        return true;
    } else {
        mensaje = "¿Desea Registrar los datos Ingresados?";
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
    if (nombresUsuario != "" && apellidosUsuario != "" && identificacionUsuario != "" && passwordUsuario != "" && emailInstitucionalUsuario != "" ) {
        var mensaje = "¿Desea Registrar los datos Ingresados?";
        return confirm(mensaje);
    }
    else {
        alert("Hay datos que son requeridos para poder guardar el registro,\n por favor diligencie todos los campos");
        return true;
    }
}

function confirmSalirRol() {
    var r = confirm('¿Confirma que desea cacelar la accion?\nTodos los datos se perderan');
    var url = window.location.pathname;
    var pathArray = url.split('/');        // <-- no need in "string()"
    var host = pathArray[0];
    var newHost = '/roles';
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
   var mensaje = "¿Desea guardar la Asignatura con la siguiente información?";
        return confirm(mensaje);
}
function confirmSalirAsignatura() {
    var r = confirm('¿Confirma que desea cancelar la accion?');
    var url = window.location.pathname;
    var pathArray = url.split('/');        // <-- no need in "string()"
    var host = pathArray[0];
    var newHost = '/Asignaturas/Index';
    if (r == true) {
        window.location = host + newHost;
    }
    return false;
}
function confirmarEliminarAsignatura() {
    var mensaje = "Esta asignatura tiene relacion con registros de la base de datos \n ¿Confirma que desea eliminar la Asignatura?";
    return confirm(mensaje);
}
function confirmacionAgregarCurso() {
    var mensaje = "¿Desea guardar el Curso con la informacion proporcionada?";
    return confirm(mensaje);
}
function confirmSalirCurso() {
    var r = confirm('¿Confirma que desea cancelar la accion?');
    var url = window.location.pathname;
    var pathArray = url.split('/');        // <-- no need in "string()"
    var host = pathArray[0];
    var newHost = '/Cursos/Index';
    if (r == true) {
        window.location = host + newHost;
    }
    return false;
}
function confirmarEliminarCurso() {
    var mensaje = "Este curso tiene relacion con registros de la base de datos \n ¿Confirma que desea eliminar el curso?";
    return confirm(mensaje);
}
function confirmacionAgregarSalon() {
    var mensaje = "¿Desea guardar el Salon con la informacion proporcionada?";
    return confirm(mensaje);
}
function confirmSalirSalon() {
    var r = confirm('¿Confirma que desea cancelar la accion?');
    var url = window.location.pathname;
    var pathArray = url.split('/');        // <-- no need in "string()"
    var host = pathArray[0];
    var newHost = '/Salones/Index';
    if (r == true) {
        window.location = host + newHost;
    }
    return false;
}
function confirmarEliminarSalon() {
    var mensaje = "Este salon tiene relacion con registros de la base de datos \n ¿Confirma que desea eliminar el salon?";
    return confirm(mensaje);
}
function confirmacionAgregarGrupo() {
    var mensaje = "¿Desea guardar el Grupo de Investigacion con la informacion proporcionada?";
    return confirm(mensaje);
}

function cargarMultiselect() {
    $('#ListaPrerequisitos').multiSelect({
        selectableHeader: "<input type='text' class='search-input' autocomplete='off' placeholder='buscar \"...\"'>",
        selectionHeader: "<input type='text' class='search-input' autocomplete='off' placeholder='buscar \"...\"'>",
        afterInit: function (ms) {
            var that = this,
                $selectableSearch = that.$selectableUl.prev(),
                $selectionSearch = that.$selectionUl.prev(),
                selectableSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selectable:not(.ms-selected)',
                selectionSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selection.ms-selected';

            that.qs1 = $selectableSearch.quicksearch(selectableSearchString)
            .on('keydown', function (e) {
                if (e.which === 40) {
                    that.$selectableUl.focus();
                    return false;
                }
            });

            that.qs2 = $selectionSearch.quicksearch(selectionSearchString)
            .on('keydown', function (e) {
                if (e.which == 40) {
                    that.$selectionUl.focus();
                    return false;
                }
            });
        },
        afterSelect: function () {
            this.qs1.cache();
            this.qs2.cache();
        },
        afterDeselect: function () {
            this.qs1.cache();
            this.qs2.cache();
        }
    });
    $('#ListaCorrequisitos').multiSelect({
        selectableHeader: "<input type='text' class='search-input' autocomplete='off' placeholder='buscar \"...\"'>",
        selectionHeader: "<input type='text' class='search-input' autocomplete='off' placeholder='buscar \"...\"'>",
        afterInit: function (ms) {
            var that = this,
                $selectableSearch = that.$selectableUl.prev(),
                $selectionSearch = that.$selectionUl.prev(),
                selectableSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selectable:not(.ms-selected)',
                selectionSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selection.ms-selected';

            that.qs1 = $selectableSearch.quicksearch(selectableSearchString)
            .on('keydown', function (e) {
                if (e.which === 40) {
                    that.$selectableUl.focus();
                    return false;
                }
            });

            that.qs2 = $selectionSearch.quicksearch(selectionSearchString)
            .on('keydown', function (e) {
                if (e.which == 40) {
                    that.$selectionUl.focus();
                    return false;
                }
            });
        },
        afterSelect: function () {
            this.qs1.cache();
            this.qs2.cache();
        },
        afterDeselect: function () {
            this.qs1.cache();
            this.qs2.cache();
        }
    });
}

