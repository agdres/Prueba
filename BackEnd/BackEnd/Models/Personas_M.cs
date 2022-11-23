using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BackEnd.Models
{
    public class Personas_M 
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Numero_Identificacion { get; set; }
        public string Email { get; set; }
        public string Tipo_Identificacion { get; set; }
    }

    public class Res_Personas : Personas_M 
    {
        // Informacion usuario
        public string Usuario { get; set; }
        public string Pass { get; set; }
    }


}
