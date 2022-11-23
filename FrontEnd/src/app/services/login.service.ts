import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Res_Usuario } from '../models/login.models';
import { Routes_Login } from '../routes/login.routes';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private Http:HttpClient) { }

  // Valida las credenciales del usuario
  public getValUser(usuario: string, pass:string):Observable<Res_Usuario>{
    return this.Http.get<Res_Usuario>(Routes_Login.getValidarUser+`/${usuario}/${pass}`);
  }
}
