using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        
        Task<T> GetByIdAsync(int id);

        /*int*/ Task AddAsync(T Item);
        /*int*/void Update(T Item);
        /*int*/void Delete(T Item);
    }
}
