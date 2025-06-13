using DataAccess.DAO;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    //Clase padre o madre abtracta de los crud 
    //Define como se hace cruds en la arquitectura
    public abstract class CrudFactory
    {
        protected SqlDao sqlDao;

        // Definir los metodos que vamos usar
        // El Crud en otras palabras 

        public abstract void Create(baseDTO baseDto);
        public abstract void Update(baseDTO baseDto);
        public abstract void Delete(baseDTO baseDto);
        public abstract T Retrive<T>();
        public abstract T RetrieveById<T>();
        public abstract List<T> RetrieveAll<T>();




    }
}
