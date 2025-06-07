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
            _connectionString = string.Empty; // Reemplaza con tu cadena de conexión real
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

        //Metodo para la ejecucion de SP sin retorno
        public void ExecuteProduce(SqlOperations operation)
        {
            //conectarse a la base de datos 
            //ejecutar el sp
        }

        //Metodo para la ejecucion de SP con retorno de datos

        public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperations operation)
        {

            //conectarse a la base de datos
            //ejecutar el sp 
            //captura el resultado 
            //convertir en dto
            var list = new List<Dictionary<string, object>>();
            return list;
        }

        public void ExecuteProcedure(SqlOperations sqlOperation)
        {
            using (var conn = new SqlCommand(_connectionString))
            {
                using (var command = new SqlCommand(sqlOperation.ProcedureName, conn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Set de los parámetros
                    foreach (var param in sqlOperation.Parameters)
                    {
                        command.Parameters.Add(param);
                    }

                    // Ejecuta el SP
                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
