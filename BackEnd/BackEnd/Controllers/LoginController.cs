using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Interfaces
using BackEnd.Services.Usuarios;
//
namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarios _Iusuarios;
        /// <summary>
        /// Inicialización variables
        /// </summary>
        /// <param name="usuarios"></param>
        public LoginController(IUsuarios usuarios)
        {
            this._Iusuarios = usuarios;
        }

        /// <summary>
        /// Metodo GET, Valida si el usuario se logea o no
        /// </summary>
        /// <param name="usuario">usuario persona</param>
        /// <param name="pass">clave</param>
        /// <returns></returns>
        [HttpGet("{usuario}/{pass}")]
        public async Task<IActionResult> getValidarUsuario(string usuario, string pass)
        {
            try
            {
                var Res = await this._Iusuarios.validarUsuarios(usuario,pass);
                return Ok(Res);
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
