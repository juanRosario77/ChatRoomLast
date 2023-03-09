using DataAccessLayer.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interface
{   
    public interface IUnitOfWork : IDisposable
    {
        WebApiDbContext Context { get; }
        void Commit();
    }
}
