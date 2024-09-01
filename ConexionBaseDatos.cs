using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi
{   
    public class ConexionBaseDatos
    {
        private string cadenaConexionSql;
        public string CadenaConexionSql {get => cadenaConexionSql;}
        
        public ConexionBaseDatos(string ConexionSql){
            cadenaConexionSql = ConexionSql;
        }
    }
}