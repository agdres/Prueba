using BackEnd.Models.Configuraciones;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models.DB;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models;
using BackEnd.Core;
using Microsoft.Data.SqlClient;

namespace BackEnd.Services.Personas
{
    public class Personas_Serv : IPersonas
    {
        public readonly ConnectionStrings _Conexion;
        public readonly Double_VContext _Context;

        public Personas_Serv(IOptions<ConnectionStrings> conexion, Double_VContext context)
        {
            this._Conexion = conexion.Value;
            this._Context = context;
        }

        public async Task<List<Res_Personas>> consultarPersonas()
        {
            List<Res_Personas> Res = new List<Res_Personas>();
            // Instancia clase que ejecuta procedimientos
            Conexiones conx = new Conexiones(this._Conexion.Conexion);

            try
            {
                await conx.ejecutarProcedimiento("spGetPersonas");

                Res = conx.DataTableToList<Res_Personas>();
                return Res;
            }
            catch (Exception ex)
            {
                return Res;

            }
        }

        /// <summary>
        /// Reg_Pistra personas y su usuario
        /// </summary>
        /// <param name="dataPersona"> Modelo de para la entidad Persona </param>
        /// <param name="usuario"> Usuario de la persona </param>
        /// <param name="pass"> clave de la persona </param>
        /// <returns></returns>
        public async Task<ResGeneral> insertarPersona(Res_Personas dataPersona)
        {
            ResGeneral Res = new ResGeneral();
            try
            {

                // Buscar usuario disponible
                var UserDisponible = await consultarUsuario(dataPersona.Usuario);
                if (UserDisponible)
                {
                    // Instacia nuevo objeto de persona
                    Persona Reg_P = new Persona();
                    Reg_P.Nombres = dataPersona.Nombres;
                    Reg_P.Apellidos = dataPersona.Apellidos;
                    Reg_P.NumeroIdentificacion = dataPersona.Numero_Identificacion;
                    Reg_P.Email = dataPersona.Email;
                    Reg_P.TipoIdentificacion = dataPersona.Tipo_Identificacion;
                    Reg_P.FechaCreacion = DateTime.Now;
                    Reg_P.IdentificacionCompleta = Reg_P.NumeroIdentificacion+" "+ Reg_P.TipoIdentificacion;
                    Reg_P.NombresCompletos = Reg_P.Nombres +" "+Reg_P.Apellidos;

                    await this._Context.Personas.AddAsync(Reg_P);
                    await this._Context.SaveChangesAsync();

                    int IdPersona = Reg_P.Identificador;
                    Usuario Reg_U = new Usuario();
                    Reg_U.Usuario1 = dataPersona.Usuario;
                    Reg_U.Pass = dataPersona.Pass;
                    Reg_U.IdPersona = IdPersona;
                    Reg_U.FechaCreacion = DateTime.Now;

                    await this._Context.Usuarios.AddAsync(Reg_U);
                    await this._Context.SaveChangesAsync();

                }
                else
                {
                    Res.idError = 1;
                    Res.Error = "El usuario digitado no esta disponible";
                }


                return Res;
            }
            catch (Exception ex)
            {
                
                Res.idError = 1;
                Res.Error = "Persona no Reg_Pistrada, intente nuevamente";
                return Res;
            }
        }

        /// <summary>
        /// Consultar si el nombre de usuario esta disponible
        /// </summary>
        /// <returns></returns>
        private async Task<bool> consultarUsuario(string user)
        {
            var Consulta = await this._Context.Usuarios.Where(x => x.Usuario1.Equals(user)).FirstOrDefaultAsync();
            if (Consulta != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
