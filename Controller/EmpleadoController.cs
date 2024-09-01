using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapi.Services;
using webapi.DTO;
using webapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace webapi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmpleadoController : ControllerBase
    {
        private readonly IServicioEmpleadoSQL _servicioEmpleado;
        public  EmpleadoController(IServicioEmpleadoSQL servicioEmpleado)
        {
            _servicioEmpleado = servicioEmpleado;
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<EmpleadoDTO>> AllEmpleados()
        {
            var empleados = await _servicioEmpleado.AllEmpleados();

            var listaEmpelados = empleados.Select(e => e.ToDTO());
            return listaEmpelados;
        }

        [HttpGet("{codEmpleado}")]
        [Authorize]
        public async Task<ActionResult<EmpleadoDTO>> GetEmpleado(string codEmpleado)
        {
            var empleado = await _servicioEmpleado.GetEmpleado(codEmpleado);
            var empleadoDTO = empleado.ToDTO();

            if(empleado is null){
                return NotFound();
            }

            return empleadoDTO;
        }
        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<EmpleadoDTO>> newEmpleado(EmpleadoDTO e)
        {
            
            Empleado empleado = new Empleado{
                CodEmpleado = e.CodEmpleado,
                Nombre = e.Nombre,
                Email = e.Email,
                Edad = e.Edad,
                FechaAlta = DateTime.Now
            };

           await _servicioEmpleado.newEmpleado(empleado);
           return empleado.ToDTO();

        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<EmpleadoDTO>> updateEmpleado(EmpleadoDTO empleado)
        {
            var auxEmpleado = await _servicioEmpleado.GetEmpleado(empleado.CodEmpleado);
            if(auxEmpleado is null)
            {
                return NotFound();
            }

            auxEmpleado.CodEmpleado = empleado.CodEmpleado;
            auxEmpleado.Nombre = empleado.Nombre;
            auxEmpleado.Email = empleado.Email;
            auxEmpleado.Edad = empleado.Edad;

            await _servicioEmpleado.updateEmpleado(auxEmpleado);

            return empleado;
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<EmpleadoDTO>> deleteEmpleado(string codEmpleado)
        {
            var auxEmpleado = await _servicioEmpleado.GetEmpleado(codEmpleado);

            if(auxEmpleado is null)
            {
                return NotFound();
            }
  
            await _servicioEmpleado.deleteEmpleado(auxEmpleado);

            return Ok();
        }
    }
}