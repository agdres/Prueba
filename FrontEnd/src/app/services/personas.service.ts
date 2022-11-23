import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take, Subject ,tap} from 'rxjs';
import { ResGeneral } from '../models/Generales.model';
import { Res_Personas } from '../models/personas.models';
import { Routes_Persona } from '../routes/personas.routes';

@Injectable({
  providedIn: 'root'
})
export class PersonasService {

  constructor(private Http:HttpClient) { }

  // Refrescar automaticamente listados cuando se ingresa una persona

  private _$refrescar = new Subject<void>();

  get refrescar$(){
    return this._$refrescar;
  }


  // Conecta con el BackEnd al controlador Personas, consulta personas
  getPersonas():Observable<Res_Personas[]>{
    return this.Http.get<Res_Personas[]>(Routes_Persona.getPersonas);
  }

  // Conecta con el BackEnd al controlador Personas, inserta personas
  postPersonas(data : Res_Personas):Observable<ResGeneral>{
    return this.Http.post<ResGeneral>(Routes_Persona.postPersonas,data).pipe(tap(()=>{
      this._$refrescar.next();
    }));
  }


}
