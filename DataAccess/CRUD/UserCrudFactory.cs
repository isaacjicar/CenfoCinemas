using DataAccess.DAO;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class UserCrudFactory : CrudFactory
    {
        public UserCrudFactory()
        {
            sqlDao = SqlDao.GetInstance();
        }
        public override void Create(baseDTO baseDto)
        {
            var user = baseDto as User;
            var sqlOperation = new SqlOperations() { ProcedureName = "CRE_USER_PR" };

            sqlOperation.AddStringParameter("P_UserCode", user.UserCode);
            sqlOperation.AddStringParameter("P_Name",user.Name );
            sqlOperation.AddStringParameter("P_Email", user.Email);
            sqlOperation.AddStringParameter("P_Password", user.Password);
            sqlOperation.AddStringParameter("P_Status",user.Status);
            sqlOperation.AddDateTimeParam("P_BirthDate", user.BirthDate );

            sqlDao.ExecuteProcedure(sqlOperation);

        }

        public override void Delete(baseDTO baseDto)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
           var lsUSer = new List<T>();
            var sqlOperation = new SqlOperations() { ProcedureName = "RET_ALL_USERS_PR" };
            var lstResults = sqlDao.ExecuteQueryProcedure(sqlOperation);
            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var user = BuildUser(row);
                    lsUSer.Add((T)Convert.ChangeType(user, typeof(T)));
                }
            }
            return lsUSer;
        }

        public override T RetrieveById<T>()
        {
            throw new NotImplementedException();
        }

        public override T Retrive<T>()
        {
            throw new NotImplementedException();
        }

        public override void Update(baseDTO baseDto)
        {
            throw new NotImplementedException();
        }


        //Metodo que convierte el diccionario en un usuario 
        private User BuildUser(Dictionary<string, object> row)
        {
            return new User()
            {
                id = (int) row["Id"],
                created = (DateTime)row["Created"],
               // Updated = (DateTime)row["Updated"],
                UserCode =(String) row["UserCode"],
                Name = (String)row["Name"],
                Email = (String)row["Email"],
                Password = (String)row["Password"],
                Status = (String)row["Status"],
                BirthDate = (DateTime)row["BirthDate"]
            };
        }
    }
}
