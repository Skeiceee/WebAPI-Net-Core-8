using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Models;

namespace webapi.Services
{
    public interface IServicioUsuario
    {
        public Task<UsuarioAPI> GetUsuario(Login login);
    }
}