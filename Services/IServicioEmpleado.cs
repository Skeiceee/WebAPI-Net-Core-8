using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Models;

namespace webapi.Services
{
    public interface IServicioEmpleado
    {
        public IEnumerable<Empleado> AllEmpleados();
        public Empleado GetEmpleado(string nombre);
        public void newEmpleado(Empleado e);
        public void updateEmpleado(Empleado e);
        public void deleteEmpleado(Empleado e);
    }
}