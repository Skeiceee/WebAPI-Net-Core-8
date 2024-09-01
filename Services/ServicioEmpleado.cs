using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Models;

namespace webapi.Services
{
    public class ServicioEmpleado : IServicioEmpleado
    {
        private readonly List<Empleado> listaEmpleados = new() 
        {
            new Empleado{
                Id = 1,
                Nombre = "Victor",
                CodEmpleado = "A001",
                Email = "victoralejandronunezt@gmail.com",
                Edad = 27,
                FechaAlta = DateTime.Now,
                FechaBaja = null
            },
            new Empleado{
                Id = 1,
                Nombre = "Carlos",
                CodEmpleado = "A002",
                Email = "carlos@gmail.com",
                Edad = 47,
                FechaAlta = DateTime.Now,
                FechaBaja = null
            },
            new Empleado{
                Id = 1,
                Nombre = "Juan",
                CodEmpleado = "A003",
                Email = "juan@gmail.com",
                Edad = 45,
                FechaAlta = DateTime.Now,
                FechaBaja = null
            },
            new Empleado{
                Id = 1,
                Nombre = "Hector",
                CodEmpleado = "A004",
                Email = "hector@gmail.com",
                Edad = 27,
                FechaAlta = DateTime.Now,
                FechaBaja = null
            }
        };

        public IEnumerable<Empleado> AllEmpleados(){
            
            return listaEmpleados;
            
        }
        public Empleado GetEmpleado(string codEmpleado){
            var empleado = listaEmpleados.Where(e => e.CodEmpleado == codEmpleado);
            return empleado.SingleOrDefault();
        }
        public void newEmpleado(Empleado empleado){
            listaEmpleados.Add(empleado);
        }
        public void updateEmpleado(Empleado empleado){
            int pos = listaEmpleados.FindIndex(existeEmpleado => existeEmpleado.Id == empleado.Id);
            if(pos != -1){
                listaEmpleados[pos] = empleado;
            }    
        }
        public void deleteEmpleado(Empleado empleado){
            int pos = listaEmpleados.FindIndex(existeEmpleado => existeEmpleado.Id == empleado.Id);
            if(pos != -1){
                listaEmpleados.RemoveAt(pos);
            }    
        }

    }
} 