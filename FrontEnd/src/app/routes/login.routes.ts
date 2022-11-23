import { environment } from "src/environment/environment";

let controlador = "Login";
export class Routes_Login{
    public static getValidarUser = environment.myApi+controlador;
}