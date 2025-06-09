using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{

    /*     
     *     Clase u objeto que se ecarga de la comunicacion con la base de datos
     *     Solo ejecuta Store Procedures
     *     
     *     Esta Clase implentya el patron del singleton
     *     para asegurar la existencia de una unica instancia de este objeto
     */
    public class SqlDao
    {

        //Paso 1: Crear una instancia privada y esttica de la clase SqlDao
        private static SqlDao _instance;

        private string _connectionString;
        // Paso 2: Redefinir el contructor default y convertilo en privado

        private SqlDao()
        {
            // Inicializar la cadena de conexión
            _connectionString = @"Data Source=srv-database-net.database.windows.net;Initial Catalog=cenfocinemas-db;Persist Security Info=True;User ID=sysman;Password=Cenfotec123;Trust Server Certificate=True";

            // Reemplaza con tu cadena de conexión real
        }

        // Paso 3: Definir el metodo que expone la instancia 

        public static SqlDao GetInstance()
        {
            // Verifica si la instancia es nula, si es asi crea una nueva instancia
            if (_instance == null)
            {
                _instance = new SqlDao();
            }
            // Retorna la instancia
            return _instance;
        }



        //Metodo para la ejecucion de SP con retorno de datos

        public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperations sqlOperation)
        {

            var lstResults = new List<Dictionary<string, object>>();

            using (var conn = new SqlConnection(_connectionString))

            {
                using (var command = new SqlCommand(sqlOperation.ProcedureName, conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    //Set de los parametros
                    foreach (var param in sqlOperation.Parameters)
                    {
                        command.Parameters.Add(param);
                    }
                    //Ejectura el SP
                    conn.Open();

                    //de aca en adelante la implementacion es distinta con respecto al procedure anterior
                    // sentencia que ejectua el SP y captura el resultado
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            var rowDict = new Dictionary<string, object>();

                            for (var index = 0; index < reader.FieldCount; index++)
                            {
                                var key = reader.GetName(index);
                                var value = reader.GetValue(index);
                                //aca agregamos los valores al diccionario de esta fila
                                rowDict[key] = value;
                            }
                            lstResults.Add(rowDict);
                        }
                    }

                }
            }

            return lstResults;
        }


        public void ExecuteProcedure(SqlOperations sqlOperation)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(sqlOperation.ProcedureName, conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    //Set de los parametros
                    foreach (var param in sqlOperation.Parameters)
                    {
                        command.Parameters.Add(param);
                    }
                    //Ejectura el SP
                    conn.Open();
                    command.ExecuteNonQuery();
                }

            }
        }

    }
}
