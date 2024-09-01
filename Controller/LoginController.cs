using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using webapi.DTO;
using webapi.Models;
using webapi.Services;

namespace webapi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IServicioUsuario _servicioUsuario;
        private readonly IConfiguration _configuration;
        public LoginController(IServicioUsuario servicioUsuario, IConfiguration configuration)
        {
            this._servicioUsuario = servicioUsuario;
            this._configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioDTO>>Login(Login login)
        {
            UsuarioAPI usuario = null;

            usuario = await AutenticarUsuario(login);
            if(usuario is null)
            {
                throw new Exception("Credenciales no validas.");
            }
            else
            {
                usuario = GenerarTokenJWT(usuario); 
            }


            return usuario.ToDTO();
        }

        private async Task<UsuarioAPI> AutenticarUsuario(Login login)
        {
            UsuarioAPI usuarioAPI = await _servicioUsuario.GetUsuario(login);
            return usuarioAPI;
        }

        private UsuarioAPI GenerarTokenJWT(UsuarioAPI usuarioInfo)
        {
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:ClaveSecreta"])
            );

            var _signingCredentials = new SigningCredentials(
                _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
            );

            var _Header = new JwtHeader(_signingCredentials);

            var _Claims = new[] {
                new Claim("usuario", usuarioInfo.Usuario),
                new Claim("email", usuarioInfo.Email),
                new Claim(JwtRegisteredClaimNames.Email, usuarioInfo.Email)
            };

            var _Payload = new JwtPayload(
                issuer: _configuration["JWT:issuer"],
                audience: _configuration["JWT:Audience"],
                claims: _Claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(30)
            );
            
            var _Token = new JwtSecurityToken(
                _Header,
                _Payload
            );

            usuarioInfo.Token = new JwtSecurityTokenHandler().WriteToken(_Token);

            return usuarioInfo;
        }
    }
}