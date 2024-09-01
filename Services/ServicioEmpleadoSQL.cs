using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient.X.XDevAPI.Common;
using webapi.Models;

namespace webapi.Services
{
    public class ServicioEmpleadoSQL : IServicioEmpleadoSQL
    {
        private string _CadenaConexion;
        private readonly ILogger<ServicioEmpleadoSQL> _log;
        public ServicioEmpleadoSQL(ConexionBaseDatos conn, ILogger<ServicioEmpleadoSQL> log)
        {
            _CadenaConexion = conn.CadenaConexionSql;
            this._log = log;
        }
        private SqlConnection conexion()
        {
            return new SqlConnection(_CadenaConexion);
        }

        public async Task<Empleado> GetEmpleado(string codEmpleado)
        {
            #nullable disable

            SqlConnection sqlConnection = conexion();
            Empleado empleado = null;
            try
            {
                sqlConnection.Open();
                var param = new DynamicParameters();
                param.Add("@CodEmpleado", codEmpleado, DbType.String, ParameterDirection.Input, 4);
                empleado = await sqlConnection.QueryFirstOrDefaultAsync<Empleado>("GetEmpleado", param, commandType: CommandType.StoredProcedure);
            }
            catch(Exception ex)
            {
                _log.LogError("ERROR: " + ex.ToString());
                throw new Exception("No se pudo obtener el empleado" + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return empleado;
        }
        public async Task<IEnumerable<Empleado>> AllEmpleados()
        {
            SqlConnection sqlConnection = conexion();
            List<Empleado> empleados = new List<Empleado>();

            try
            {
                sqlConnection.Open();
                var r = await sqlConnection.QueryAsync<Empleado>("GetEmpleado", commandType: CommandType.StoredProcedure);
                empleados = r.ToList();
            }
            catch(Exception ex)
            {
                _log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al traer todos los empleados" + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();                
            }

            return empleados;
        }
        public async Task newEmpleado(Empleado e)
        {
            SqlConnection sqlConnection = conexion();

            try
            {
                sqlConnection.Open();
                var param = new DynamicParameters();
                param.Add("@Nombre", e.Nombre, DbType.String, ParameterDirection.Input, 120);
                param.Add("@CodEmpleado", e.CodEmpleado, DbType.String, ParameterDirection.Input, 4);
                param.Add("@Email", e.Email, DbType.String, ParameterDirection.Input, 255);
                param.Add("@Edad", e.Edad, DbType.Int32, ParameterDirection.Input);
                param.Add("@FechaAlta", e.FechaAlta, DbType.DateTime, ParameterDirection.Input);

                await sqlConnection.ExecuteScalarAsync("NewEmpleado", param, commandType: CommandType.StoredProcedure);
            }
            catch(Exception ex)
            {
                _log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al agregar el empleado" + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
        public async Task updateEmpleado(Empleado e)
        {
            SqlConnection sqlConnection = conexion();

            try
            {
                sqlConnection.Open();
                var param = new DynamicParameters();
                param.Add("@Nombre", e.Nombre, DbType.String, ParameterDirection.Input, 120);
                param.Add("@CodEmpleado", e.CodEmpleado, DbType.String, ParameterDirection.Input, 4);
                param.Add("@Email", e.Email, DbType.String, ParameterDirection.Input, 255);
                param.Add("@Edad", e.Edad, DbType.Int32, ParameterDirection.Input);

                await sqlConnection.ExecuteScalarAsync("UpdateEmpleado", param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error no se pudo modificar el empleado" + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
        public async Task deleteEmpleado(Empleado e)
        {
            SqlConnection sqlConnection = conexion();

            try
            {
                sqlConnection.Open();
                var param = new DynamicParameters();
                param.Add("@CodEmpleado", e.CodEmpleado, DbType.String, ParameterDirection.Input, 4);
                await sqlConnection.ExecuteScalarAsync("DeleteEmpleado", param, commandType: CommandType.StoredProcedure);
            }
            catch(Exception ex)
            {
                _log.LogError("ERROR: " + ex.ToString());
                throw new Exception("Se produjo un error al borrar un empleado" + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
    }
}