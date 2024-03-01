using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public interface IDaoFactory
    {
        public IGenericDao<T> CreateDao<T>() where T: class;
    }
}
