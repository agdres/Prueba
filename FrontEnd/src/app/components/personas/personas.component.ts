import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Res_Personas } from 'src/app/models/personas.models';
import { PersonasService } from 'src/app/services/personas.service';
import { Observable,take } from 'rxjs';
import { MsgSweetalert } from 'src/app/messages/msg.messages';
import { RgxModel } from 'src/app/functions/rgx.functions';
import { Letras, Numeros } from 'src/app/functions/formatos.functions';

@Component({
  selector: 'app-personas',
  templateUrl: './personas.component.html',
  styleUrls: ['./personas.component.scss']
})
export class PersonasComponent {

  public FrmPersona !: FormGroup;
  // Inicialización FormGroup y inyeccion de dependencias
  constructor(private fb : FormBuilder,private Persona_Serv : PersonasService){
    this.FrmPersona = this.fb.group({
      nombres:['',[Validators.required]],
      apellidos:['',[Validators.required]],
      identificacion:['',[Validators.required]],
      tipoDocumento:[,[Validators.required]],
      email:['',[Validators.required,Validators.pattern(RgxModel.Email)]],
      usuario:['',[Validators.required]],
      clave:['',[Validators.required]],
    });
  }

  // Envia información al servicio para registrar una persona
  postPersona(){
    try {
      if (this.FrmPersona.invalid) {
       
        MsgSweetalert('Acción Denegada','Todos los campos son requeridos','info');
      }else{
        let data : Res_Personas = {
          nombres : this.FrmPersona.get('nombres')!.value,
          apellidos : this.FrmPersona.get('apellidos')!.value,
          numero_Identificacion : this.FrmPersona.get('identificacion')!.value,
          email : this.FrmPersona.get('email')!.value,
          tipo_Identificacion : this.FrmPersona.get('tipoDocumento')!.value,
          usuario: this.FrmPersona.get('usuario')!.value,
          pass:this.FrmPersona.get('clave')!.value 

        }

        this.Persona_Serv.postPersonas(data).pipe(take(1)).subscribe(res =>{
          if (res.idError == 0) {
            MsgSweetalert('Acción Realizada','Persona registrada','success');
            this.Limpiar();
          }else{
            MsgSweetalert('Acción Denegada',res.error,'info');
          }
        });
      }
    } catch (error) {
      
    }
  }

    // Validaciones para inputs, solo numeros o solo letras
  
    InputLetras(e:any){
      return Letras(e);
    }

    InputNumeros(e:any){
      return Numeros(e);
    }

    // Limpiar campos

    Limpiar(){
      this.FrmPersona.reset();
    }

}
