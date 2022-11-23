using System;
using System.Collections.Generic;

#nullable disable

namespace BackEnd.Models.DB
{
    public partial class Persona
    {
        public Persona()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int Identificador { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string Email { get; set; }
        public string TipoIdentificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string IdentificacionCompleta { get; set; }
        public string NombresCompletos { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
