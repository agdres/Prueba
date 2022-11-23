using BackEnd.Models;
using BackEnd.Models.Configuraciones;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models.DB;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BackEnd.Services.Usuarios
{
    public class Usuarios_Serv : IUsuarios
    {

        private readonly AppSettings _AppSettings;
        private readonly Double_VContext _Context;
        /// <summary>
        /// Inicialización de variables 
        /// </summary>
        /// <param name="appsettings"></param>
        public Usuarios_Serv(IOptions<AppSettings> appsettings,Double_VContext context)
        {
            this._AppSettings = appsettings.Value;
            this._Context = context;
        }

        /// <summary>
        /// Validar ingreso del usuario
        /// </summary>
        /// <param name="user">Usuarios</param>
        /// <param name="pass">Claves</param>
        /// <returns></returns>
        public async Task<Res_Usuario> validarUsuarios(string user, string pass)
        {
            Res_Usuario Res = new Res_Usuario();
            try
            {
                var Consulta = await this._Context.Usuarios.Where(x => x.Usuario1.Equals(user) && x.Pass.Equals(pass)).FirstOrDefaultAsync();
                if (Consulta != null)
                {
                    Res.Error = 0;
                    Res.Token = crearToken(Consulta);
                }
                else
                {
                    Res.Error = 1;
                }

                return Res;
            }
            catch (Exception ex)
            {
                Res.Error = 2;
                return Res;
            }
        }

        /// <summary>
        /// Generar tokend con la información del usuario
        /// </summary>
        /// <param name="data">Información Usuarios</param>
        /// <returns></returns>
        private string crearToken(Usuario data)
        {

            var tokendHandler = new JwtSecurityTokenHandler();

            var llave = Encoding.ASCII.GetBytes(this._AppSettings.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Configuración de Claims
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier,data.IdPersona.ToString()),
                        new Claim(("usuario"),data.Usuario1)
                    }
                    ),
                // Tiempo de expiración del tokend
                Expires = DateTime.UtcNow.AddMinutes(60),
                // Encriptacion de la credencial
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature),
            };
            // Crea el token
            var token = tokendHandler.CreateToken(tokenDescriptor);
            // Sobre escribe el token y lo pasa a string
           return tokendHandler.WriteToken(token);
        }
    }
}
