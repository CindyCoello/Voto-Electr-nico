using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using VotoElectronico.Models;

namespace VotoElectronico.Repositories
{
    public  class MododuloRepository
    {
        public IEnumerable<tbModuloPantallas> ListModuloPantallas(int rolId)
        {
           
            const string query = @"UDP_Modulos_ListadoRol";
            var parameters = new DynamicParameters();
            parameters.Add("@rol_Id", rolId, DbType.Int32, ParameterDirection.Input);



            using(var db = new SqlConnection(Helpers.GetConnectionString()))
            {
                var resultado = db.Query<tbModuloPantallas>(query, parameters, commandType: CommandType.StoredProcedure).ToList();
                return resultado;
            }

        }
    }
}