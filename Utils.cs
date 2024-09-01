using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.DTO;
using webapi.Models;

namespace webapi
{
    public static class Utils
    {
        public static EmpleadoDTO ToDTO(this Empleado e)
        {
            if(e != null)
            {
                return new EmpleadoDTO
                {
                    Nombre = e.Nombre,
                    CodEmpleado = e.CodEmpleado,
                    Email = e.Email,
                    Edad = e.Edad
                };
            }

            return null;
        }

        public static UsuarioDTO ToDTO(this UsuarioAPI u)
        {
            if(u != null)
            {
                return new UsuarioDTO
                {
                    Token = u.Token,
                    Usuario = u.Usuario
                };
            }

            return null;
        }
    }
}