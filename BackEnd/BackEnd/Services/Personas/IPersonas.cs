using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services.Personas
{
    public interface IPersonas
    {
        Task<List<Res_Personas>> consultarPersonas();
        Task<ResGeneral> insertarPersona(Res_Personas dataPersona);
    }
}
