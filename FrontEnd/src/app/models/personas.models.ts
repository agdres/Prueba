export interface Personas_M 
{
    nombres : string;
    apellidos : string;
    numero_Identificacion : string;
    email : string;
    tipo_Identificacion : string;
}

export interface Res_Personas extends Personas_M 
{
    // Informacion usuario
    usuario : string;
    pass : string;
}