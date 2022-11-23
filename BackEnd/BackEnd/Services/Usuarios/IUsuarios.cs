using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services.Usuarios
{
    public interface IUsuarios
    {
        Task<Res_Usuario> validarUsuarios(string user, string pass);
    }
}
