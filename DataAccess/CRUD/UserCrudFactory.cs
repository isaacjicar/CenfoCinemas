using DataAccess.DAO;
using DTOs;
using System;
using System.Collections.Generic;

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
            sqlOperation.AddStringParameter("P_Name", user.Name);
            sqlOperation.AddStringParameter("P_Email", user.Email);
            sqlOperation.AddStringParameter("P_Password", user.Password);
            sqlOperation.AddStringParameter("P_Status", user.Status);
            sqlOperation.AddDateTimeParam("P_BirthDate", user.BirthDate);

            sqlDao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(baseDTO baseDto)
        {
            var user = baseDto as User;
            var sqlOperation = new SqlOperations() { ProcedureName = "Delete_USER_PR" };
            sqlOperation.AddIntParam("P_Id", user.id);

            sqlDao.ExecuteProcedure(sqlOperation);
        }

        public override List<T> RetrieveAll<T>()
        {
            var listUsers = new List<T>();
            var sqlOperation = new SqlOperations() { ProcedureName = "RET_ALL_USERS_PR" };
            var result = sqlDao.ExecuteQueryProcedure(sqlOperation);

            foreach (var row in result)
            {
                var user = BuildUser(row);
                listUsers.Add((T)Convert.ChangeType(user, typeof(T)));
            }

            return listUsers;
        }

        public override T RetrieveById<T>(int id)
        {
            var sqlOperation = new SqlOperations() { ProcedureName = "RET_ID_USERS_PR" };
            sqlOperation.AddIntParam("P_Id", id);
            var result = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (result.Count > 0)
            {
                var user = BuildUser(result[0]);
                return (T)Convert.ChangeType(user, typeof(T));
            }

            return default(T);
        }

        public T RetrieveByUserCode<T>(User user)
        {
            var sqlOperation = new SqlOperations() { ProcedureName = "RET_UserCode_USERS_PR" };
            sqlOperation.AddStringParameter("P_Usercode", user.UserCode); // ✅ Sin el @
            var result = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (result.Count > 0)
            {
                var userBuilt = BuildUser(result[0]);
                return (T)Convert.ChangeType(userBuilt, typeof(T));
            }

            return default(T);
        }

        public T RetrieveByEmail<T>(User user)
        {
            var sqlOperation = new SqlOperations() { ProcedureName = "RET_Email_USERS_PR" };
            sqlOperation.AddStringParameter("P_Email", user.Email); // ✅ Sin el @
            var result = sqlDao.ExecuteQueryProcedure(sqlOperation);

            if (result.Count > 0)
            {
                var userBuilt = BuildUser(result[0]);
                return (T)Convert.ChangeType(userBuilt, typeof(T));
            }

            return default(T);
        }

        public override T Retrive<T>()
        {
            throw new NotImplementedException();
        }

        public override void Update(baseDTO baseDto)
        {
            var user = baseDto as User;
            var sqlOperation = new SqlOperations() { ProcedureName = "UP_USER_PR" };

            sqlOperation.AddIntParam("P_Id", user.id);
            sqlOperation.AddStringParameter("P_UserCode", user.UserCode);
            sqlOperation.AddStringParameter("P_Name", user.Name);
            sqlOperation.AddStringParameter("P_Email", user.Email);
            sqlOperation.AddStringParameter("P_Password", user.Password);
            sqlOperation.AddStringParameter("P_Status", user.Status);
            sqlOperation.AddDateTimeParam("P_BirthDate", user.BirthDate);

            sqlDao.ExecuteProcedure(sqlOperation);
        }

        private User BuildUser(Dictionary<string, object> row)
        {
            return new User()
            {
                id = (int)row["Id"],
                created = (DateTime)row["Created"],
                UserCode = (string)row["UserCode"],
                Name = (string)row["Name"],
                Email = (string)row["Email"],
                Password = (string)row["Password"],
                Status = (string)row["Status"],
                BirthDate = (DateTime)row["BirthDate"]
            };
        }
    }
}
