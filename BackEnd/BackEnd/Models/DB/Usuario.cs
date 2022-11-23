using System;
using System.Collections.Generic;

#nullable disable

namespace BackEnd.Models.DB
{
    public partial class Usuario
    {
        public int Identificador { get; set; }
        public int? IdPersona { get; set; }
        public string Usuario1 { get; set; }
        public string Pass { get; set; }
        public DateTime? FechaCreacion { get; set; }

        public virtual Persona IdPersonaNavigation { get; set; }
    }
}
