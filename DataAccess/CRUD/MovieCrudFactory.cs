using DataAccess.DAO;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class MovieCrudFactory : CrudFactory
    {
        public MovieCrudFactory()
            {
            sqlDao = SqlDao.GetInstance();
            }

        public override void Create(baseDTO baseDto)
        {
            var movie = baseDto as Movie;
            var sqlOperation = new SqlOperations() { ProcedureName = "CRE_MOVIES_PR" };

            sqlOperation.AddStringParameter("P_Title", movie.Title);
            sqlOperation.AddStringParameter("P_Description", movie.description);
            sqlOperation.AddDateTimeParam("P_ReleaseDate", movie.ReleaseDate);
            sqlOperation.AddStringParameter("P_Genre", movie.Genre);
            sqlOperation.AddStringParameter("P_Director", movie.Director);

            sqlDao.ExecuteProcedure(sqlOperation);
        }
        

        public override void Delete(baseDTO baseDto)
        {
            var movie = baseDto as Movie;
            var sqlOperation = new SqlOperations() { ProcedureName = "DEL_MOVIE_PR" };
            sqlOperation.AddIntParam("P_Id", movie.id);

            sqlDao.ExecuteProcedure(sqlOperation);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lsMovie = new List<T>();
            var sqlOperation = new SqlOperations() { ProcedureName = "RET_ALL_Movie_PR" };
            var lstResults = sqlDao.ExecuteQueryProcedure(sqlOperation);
            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var movie = BuildUser(row);
                    lsMovie.Add((T)Convert.ChangeType(movie, typeof(T)));
                }
            }
            return lsMovie;
        }

        public override T RetrieveById<T>(int id)
        {
            var sqlOperation = new SqlOperations() { ProcedureName = "SP_RET_ID_Movie" };
            sqlOperation.AddIntParam("@P_Id", id);
            var lstResults = sqlDao.ExecuteQueryProcedure(sqlOperation);
            if (lstResults.Count > 0)
            {
                var row = lstResults[0];
                var movie = BuildUser(row);
                return (T)Convert.ChangeType(movie, typeof(T));
            }

            return default(T);

        }

        public  T RetrieveByTitle<T>(Movie movie)
        {
            var sqlOperation = new SqlOperations() { ProcedureName = "SP_RET_TITLE_Movie" };
            sqlOperation.AddStringParameter("@P_Title", movie.Title);
            var lstResults = sqlDao.ExecuteQueryProcedure(sqlOperation);
            if (lstResults.Count > 0)
            {
                var row = lstResults[0];
                var Mv = BuildUser(row);
                return (T)Convert.ChangeType(Mv, typeof(T));
            }

            return default(T);

        }

        public override T Retrive<T>()
        {
            throw new NotImplementedException();
        }

        public override void Update(baseDTO baseDto)
        {
            throw new NotImplementedException();
        }
        private Movie BuildUser(Dictionary<string, object> row)
        {
            return new Movie()
            {
                id = (int)row["Id"],
                created = (DateTime)row["Created"],
               // Updated = (DateTime)row["Updated"],
                Title = (String)row["Title"],
                ReleaseDate = (DateTime)row["ReleaseDate"],
                Genre = (String)row["Genre"],
                Director = (String)row["Director"],
                description = (String)row["Description"]
                
                
            };
        }
    }
}
