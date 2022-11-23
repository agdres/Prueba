export function Letras(e:any):any {
    // Obtener el valor del keypress
    let key = e.keyCode || e.which,
    tecla = String.fromCharCode(key).toLowerCase();

    var valoresAceptados = /^[a-zA-Z \u00E0-\u00FC]*$/;
    if (!tecla.match(valoresAceptados)){
      return false; 
    } else {
      return true;
    }
}


export function Numeros(e:any):any{
let key = e.keyCode || e.which,
    tecla = String.fromCharCode(key).toLowerCase();

    var valoresAceptados = /^[0-9]+$/;
    if (tecla.match(valoresAceptados)){
      return true;
    } else {
      return false;
    }
}