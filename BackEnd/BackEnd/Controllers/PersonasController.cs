using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//
using BackEnd.Services.Personas;
using BackEnd.Models;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonas _IPersonas;
        public PersonasController(IPersonas personas)
        {
            this._IPersonas = personas;
        }

        /// <summary>
        /// Metodo GET, Consulta todas las personas y sus detalles de usuario
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> getPersonas()
        {
            try
            {
                var Res = await this._IPersonas.consultarPersonas();
                return Ok(Res);
            }
            finally
            {
                GC.Collect();
            }
        }

        [HttpPost]
        public async Task<IActionResult> postPersonas(Res_Personas data)
        {
            try
            {
                var Res = await this._IPersonas.insertarPersona(data);
                return Ok(Res);
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
