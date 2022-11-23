import { environment } from "src/environment/environment";

let controlador = "Personas";
export class Routes_Persona{
    public static getPersonas = environment.myApi+controlador;
    public static postPersonas = environment.myApi+controlador;
}