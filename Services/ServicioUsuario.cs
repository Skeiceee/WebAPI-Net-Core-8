using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using webapi.Models;

namespace webapi.Services
{
    public class ServicioUsuario : IServicioUsuario
    {
        private string _CadenaConexion;
        private readonly ILogger<ServicioEmpleadoSQL> _log;
        public ServicioUsuario(ConexionBaseDatos conn, ILogger<ServicioEmpleadoSQL> log)
        {
            _CadenaConexion = conn.CadenaConexionSql;
            this._log = log;
        }
        private SqlConnection conexion()
        {
            return new SqlConnection(_CadenaConexion);
        }

        public async Task<UsuarioAPI> GetUsuario(Login login)
        {
            #nullable disable

            SqlConnection sqlConnection = conexion();
            UsuarioAPI usuario = null;
            
            try
            {
                sqlConnection.Open();
                var param = new DynamicParameters();
                param.Add("@Usuario", login.Usuario, DbType.String, ParameterDirection.Input, 255);
                param.Add("@Pass", login.Pass, DbType.String, ParameterDirection.Input, 255);
                usuario = await sqlConnection.QueryFirstOrDefaultAsync<UsuarioAPI>("GetUsuario", param, commandType: CommandType.StoredProcedure);

            }
            catch(Exception ex){
                _log.LogError("ERROR: " + ex.ToString());
                throw new Exception("No se pudo obtener el usuario: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return usuario;
        }

    }
}