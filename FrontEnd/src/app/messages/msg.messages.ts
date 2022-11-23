import Swal from 'sweetalert2'

// Mensajes
export function MsgSweetalert(titulo : string, text:string,icon : any){
    Swal.fire({
        title: titulo,
        text: text,
        icon: icon,
        showCancelButton: false,
        showConfirmButton: true
    })
}