using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class BaseManager
    {

        protected void ManagerExection(Exception exception)
        {
            throw exception;
        }
    }
}
