using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Models;

namespace webapi.Services
{
    public interface IServicioEmpleadoSQL
    {
        public Task<IEnumerable<Empleado>> AllEmpleados();
        public Task<Empleado> GetEmpleado(string nombre);
        public Task newEmpleado(Empleado e);
        public Task updateEmpleado(Empleado e);
        public Task deleteEmpleado(Empleado e);
    }
}