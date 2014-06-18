function cargarMultiselectDocument() {
    $(document).ready(function () {
        cargarMultiselect();
    });
}
function cargarMultiselectDocumentEtiquetas() {
    $(document).ready(function () {
        cargarMultiselectEtiquetas();
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

function valorNumericoUuarios() {
    alert("Esta busqueda solo acepta valores numericos");
}

function confirmacionAgregarUsuario() {
    var mensaje = "¿Desea guardar el Usuario con la informacion proporcionada?";
    return confirm(mensaje);
}

function confirmacionEditarUsuario(){
var mensaje = "¿Desea modificar el Usuario con la informacion proporcionada?";
return confirm(mensaje);
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


function confirmSalirUsuario() {
    var r = confirm('Los datos que ha ingresado del nuevo usuario se borraran si proceda,\n ¿Confirma que desea cancelar la accion?');
    var url = window.location.pathname;
    var pathArray = url.split('/');        // <-- no need in "string()"
    var host = pathArray[0];
    var newHost = '/Usuarios';
    if (r == true) {
        window.location = host + newHost;
    }
    return false;
}

function confirmSalirGInvestigacion() {
    var r = confirm('¿Confirma que desea cancelar la accion?\nTodos los datos se perderan');
    var url = window.location.pathname;
    var pathArray = url.split('/');        // <-- no need in "string()"
    var host = pathArray[0];
    var newHost = '/GruposInvestigacion';
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

function confirmSalirCargarArchivo() {
    var seleccion = confirm('¿Confirma que desea cancelar la accion?');
    return seleccion;
}

function confirmacionAgregarAsignatura() {
    var nombre = document.getElementById("NOMBREASIGNATURA").value;
    var codigo = document.getElementById("CODIGOASIGNATURA").value;
    var clasificacion = document.getElementById("CLASIFICACIONASIGNATURA").value;
    var mensaje;
    if (nombre.trim() == "" || descripcion.trim() == "" || clasificacion.trim() == "") {
        mensaje = "Hay datos que son requeridos para poder guardar el registro,\n por favor diligencie todos los campos";
        alert(mensaje);
        return true;
    }
    else {
        mensaje = "¿Desea guardar la Asignatura con la siguiente información?";
        return confirm(mensaje);
    }
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
    //var mensaje = "¿Desea guardar el Curso con la informacion proporcionada?";
    //return confirm(mensaje);
    return true;
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
    var nombre = document.getElementById("NOMBRESALON").value;    
    var mensaje;
    if (nombre.trim() == "") {
        mensaje = "Hay datos que son requeridos para poder guardar el registro,\n por favor diligencie todos los campos";
        alert(mensaje);
        return true;
    }
    else {
        mensaje = "¿Desea guardar el Salon con la informacion proporcionada?";
        return confirm(mensaje);
    }
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
function cargarMultiselectEtiquetas() {
    $('#ListaEtiquetas').multiSelect({
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


/* Validacion modulo Programas*/

function comfirmacionPrograma() {
    var codigo = document.getElementById("CODIGOSNIESPROGRAMA").value;
    var nombre = document.getElementById("NOMBREPROGRAMA").value;
    var descripcion = document.getElementById("DESCRIPCIONPROGRAMA").value;
    var facultad = document.getElementById("IDENTIFICADORFACULTAD").value;
    var duracion = document.getElementById("DURACIONPROGRAMA").value;     
    var modalidad = "";
    var jornada = "";
    var admision = "";

    if ($('input[name=MODALIDADPROGRAMA]:checked').length > 0) {
        modalidad = "si";
    }
    if ($('input[name=JORNADAPROGRAMA]:checked').length > 0) {
        jornada = "si";
    }
    if ($('input[name=ADMISIONPROGRAMA]:checked').length > 0) {
        admision = "si";
    }

    var mensaje;
    if (nombre.trim() == "" || codigo.trim() == "" || descripcion.trim() == "" || facultad.trim() == "" ||
        duracion.trim() == "" || modalidad.trim() == "" || jornada.trim() == "" || admision.trim() == "") {
        mensaje = "Hay datos que son requeridos para poder guardar el registro,\n por favor diligencie todos los campos";
        alert(mensaje);
        return true;
    } else {
        mensaje = "¿Confirma que los datos ingresados son correctos y que desea guardarlos en la base de datos?";
        return confirm(mensaje);
    }
}

function confirmarEliminarPrograma() {
    var mensaje = "Este Programa tiene relacion con registros de la base de datos \n ¿Confirma que desea eliminarlo?";
    return confirm(mensaje);
}


/* Fin validacion programas */



/* Validacion modulo Plan de estudio*/

function comfirmacionPlanEstudio() {
    var nombre = document.getElementById("NOMBREPLANESTUDIOS").value;
    var codigo = document.getElementById("CODIGOPLANESTUDIOS").value;
    var descripcion = document.getElementById("DESCRIPCIONPLANESTUDIOS").value;
    var fechaInicio = document.getElementById("FECHAINICIOPLANESTUDIOS").value;
    var fechaFin = document.getElementById("FECHAFINPLANESTUDIOS").value;
    var programa = document.getElementById("IDENTIFICADORPROGRAMA").value;

    var mensaje;
    if (nombre.trim() == "" || codigo.trim() == "" || descripcion.trim() == "" || fechaInicio.trim() == "" || fechaFin.trim() == "" || programa.trim() == "") {
        mensaje = "Hay datos que son requeridos para poder guardar el registro,\n por favor diligencie todos los campos";
        alert(mensaje);
        return true;
    } else {
        mensaje = "¿Confirma que los datos ingresados son correctos y que desea guardarlos en la base de datos?";
        return confirm(mensaje);
    }
}

function confirmarEliminarPlanEstudio() {
    var mensaje = "Este Plan de Estudios tiene relacion con registros de la base de datos \n ¿Confirma que desea eliminarlo?";
    return confirm(mensaje);
}

function validarAgregarAsignaturaPlanEstudio() {
    if ($("#SEMESTRE").val() == "" || $("#IDENTIFICADORASIGNATURA").val() == "") {
        alert("Debe seleccionar el Semestre y la Asignatura para poderlo agregar al plan de estudio ");
        return false;
    }
    return true;
}

function confirmarEliminarAsignaturaPlanEstudio() {
    var mensaje = "¿Desea quitar esta asignatura del plan de estudio?";
    return confirm(mensaje);
}


/* Fin validacion plan de  estudio */


function confirmSalirHorario() {
    var r = confirm('¿Confirma que desea terminar la accion?');
    var url = window.location.pathname;
    var pathArray = url.split('/');        // <-- no need in "string()"
    var host = pathArray[0];
    var newHost = '/Cursos';
    if (r == true) {
        window.location = host + newHost;
    }
    return false;
}

/* Validacion modulo Etiquetas*/

function confirmarEliminarEtiqueta() {
    var mensaje = "Esta Etiqueta tiene relacion con registros de la base de datos \n ¿Confirma que desea eliminarlo?";
    return confirm(mensaje);
}

function comfirmacionEtiqueta() {
    var nombre = document.getElementById("NOMBREETIQUETA").value;
    var desc = document.getElementById("DESCRIPCIONETIQUETA").value;

    var mensaje;
    if (nombre.trim() == "" || desc.trim() == "") {
        mensaje = "Hay datos que son requeridos para poder guardar el registro,\n por favor diligencie todos los campos";
        alert(mensaje);
        return false;
    } else {
        mensaje = "¿Confirma que los datos ingresados son correctos y que desea guardarlos en la base de datos?";
        return confirm(mensaje);
    }
}

/* Fin validacion Etiquetas */

/* Validacion modulo Categorias */

function comfirmacionCategoria() {
    var nombre = document.getElementById("NOMBRECATEGORIA").value;
    var desc = document.getElementById("DESCRIPCIONCATEGORIA").value;

    var mensaje;
    if (nombre.trim() == "" || desc.trim() == "") {
        mensaje = "Hay datos que son requeridos para poder guardar el registro,\n por favor diligencie todos los campos";
        alert(mensaje);
        return true;
    } else {
        mensaje = "¿Confirma que los datos ingresados son correctos y que desea guardarlos en la base de datos?";
        return confirm(mensaje);
    }
}
function confirmSalirCategoria() {
    var r = confirm('¿Confirma que desea terminar la accion?');
    var url = window.location.pathname;
    var pathArray = url.split('/');        // <-- no need in "string()"
    var host = pathArray[0];
    var newHost = '/Categorias';
    if (r == true) {
        window.location = host + newHost;
    }
    return false;
}

/* Fin validacion Categorias */


/* Validacion modulo Asignar Categorias a bloques */

function comfirmacionAsignarCategoriasBloques() {
    var bloque1 = document.getElementById("bloque1").value;
    var bloque2 = document.getElementById("bloque2").value;
    var bloque3 = document.getElementById("bloque3").value;
    var bloque4 = document.getElementById("bloque4").value;

    if (bloque1 == "" || bloque2 == "" || bloque3 == "" || bloque4 == "" ) {
        mensaje = "Hay datos que son requeridos para poder guardar el registro,\n por favor diligencie todos los campos";
        alert(mensaje);
        return false;
    } else {
        mensaje = "¿Confirma que los datos ingresados son correctos y que desea guardarlos en la base de datos?";
        return confirm(mensaje);
    }
}

/* Fin validacion Categorias */

/* VALIDACION CONTENIDOS*/
function confirmacionAgregarContenido() {
    var nombre = document.getElementById("NOMBREASIGNATURA").value;
    var codigo = document.getElementById("CODIGOASIGNATURA").value;
    var clasificacion = document.getElementById("CLASIFICACIONASIGNATURA").value;
    var mensaje;
    if (nombre.trim() == "" || descripcion.trim() == "" || clasificacion.trim() == "") {
        mensaje = "Hay datos que son requeridos para poder guardar el registro,\n por favor diligencie todos los campos";
        alert(mensaje);
        return true;
    }
    else {
        mensaje = "¿Desea guardar el Contenido con la siguiente información?";
        return confirm(mensaje);
    }
}
function confirmSalirContenido() {
    var r = confirm('¿Confirma que desea cancelar la accion?');
    var url = window.location.pathname;
    var pathArray = url.split('/');        // <-- no need in "string()"
    var host = pathArray[0];
    var newHost = '/Contenidos/Index';
    if (r == true) {
        window.location = host + newHost;
    }
    return false;
}
function confirmarEliminarContenido() {
    var mensaje = "Este contenido tiene relacion con registros de la base de datos \n ¿Confirma que desea eliminar el Contenido?";
    return confirm(mensaje);
}
/*FIN VALIDACION CONTENIDOS*/