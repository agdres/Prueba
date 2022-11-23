import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MsgSweetalert } from 'src/app/messages/msg.messages';
import { LoginService } from 'src/app/services/login.service';
import { take,pipe } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  public FrmLogin !: FormGroup;
  constructor(private fb : FormBuilder,private Login_Serv : LoginService){
    this.FrmLogin = this.fb.group({
      user: ['',[Validators.required]],
      pass: ['',[Validators.required]]
    })
  }

  // Validamos el ingreso del Usuario
  validarUsuario(){
    if (this.FrmLogin.invalid) {
      MsgSweetalert('Acción Denegada','El usuario y la clave son requeridos','info');;
    }else{
      this.Login_Serv.getValUser(this.FrmLogin.get("user")!.value,this.FrmLogin.get("pass")!.value).pipe(take(1)).subscribe(res =>{
        if (res.error == 0) {
          MsgSweetalert('Usuario Validado','Su Token: '+res.token,'success');
        }else{
          MsgSweetalert('Acción Denegada','Usuario o clave incorrecta, verifique su información','info');
        }
      });
    }
  }
}
