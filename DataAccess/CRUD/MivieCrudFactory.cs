using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    internal class MivieCrudFactory : CrudFactory
    {
        public override void Create(baseDTO baseDto)
        {
            throw new NotImplementedException();
        }

        public override void Delete(baseDTO baseDto)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
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
    }
}
