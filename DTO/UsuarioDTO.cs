using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.DTO
{
    public class UsuarioDTO
    {
        public string Usuario { get; set; }
        public string Token { get; internal set; }
    }
}